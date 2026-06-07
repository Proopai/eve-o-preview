using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EveOPreview.UI.Hotkeys
{
	public enum MouseButton
	{
		Middle,
		XButton1, // "M4"
		XButton2  // "M5"
	}

	/// <summary>
	/// Owns a single system-wide low-level mouse hook (WH_MOUSE_LL) and dispatches
	/// configured (button + modifier) combinations to registered callbacks.
	/// This is the mouse-side analogue of <see cref="HotkeyHandler"/>: keyboard hotkeys
	/// use RegisterHotKey (keyboard only), whereas mouse buttons can only be captured
	/// globally through a low-level hook.
	/// </summary>
	sealed class MouseHookHandler : IDisposable
	{
		private sealed class Binding
		{
			public MouseButton Button;
			public Keys Modifiers; // exact set of Control/Shift/Alt required
			public Action Callback;
		}

		#region Private fields
		// The delegate MUST be kept referenced for the lifetime of the hook,
		// otherwise the GC collects it and the native callback crashes the process.
		private readonly MouseHookNativeMethods.LowLevelMouseProc _hookProc;
		private readonly List<Binding> _bindings;

		private IntPtr _hookId;

		// When a bound button-down is swallowed we latch the button so its paired
		// button-up can be swallowed too, leaving no dangling button state in the
		// target application.
		private MouseButton? _swallowedButton;
		#endregion

		public MouseHookHandler()
		{
			this._hookProc = this.HookCallback;
			this._bindings = new List<Binding>();
			this._hookId = IntPtr.Zero;
			this._swallowedButton = null;
		}

		public bool HasBindings => this._bindings.Count > 0;

		public void AddBinding(MouseButton button, Keys modifiers, Action callback)
		{
			this._bindings.Add(new Binding { Button = button, Modifiers = modifiers, Callback = callback });
		}

		/// <summary>Installs the hook if there is at least one binding. Idempotent.</summary>
		public void Hook()
		{
			if ((this._hookId != IntPtr.Zero) || (this._bindings.Count == 0))
			{
				return;
			}

			// WH_MOUSE_LL is a global hook; passing the executable's module handle is
			// sufficient. The callback is delivered to this (the installing) thread's
			// message queue - which is the WinForms UI thread, so callbacks may touch
			// UI safely without marshaling.
			IntPtr module = MouseHookNativeMethods.GetModuleHandle(null);
			this._hookId = MouseHookNativeMethods.SetWindowsHookEx(MouseHookNativeMethods.WH_MOUSE_LL, this._hookProc, module, 0);
		}

		public void Unhook()
		{
			if (this._hookId == IntPtr.Zero)
			{
				return;
			}

			MouseHookNativeMethods.UnhookWindowsHookEx(this._hookId);
			this._hookId = IntPtr.Zero;
		}

		public void Dispose()
		{
			this.Unhook();
			GC.SuppressFinalize(this);
		}

		~MouseHookHandler()
		{
			this.Unhook();
		}

		private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
		{
			if (nCode != MouseHookNativeMethods.HC_ACTION)
			{
				return MouseHookNativeMethods.CallNextHookEx(this._hookId, nCode, wParam, lParam);
			}

			int message = wParam.ToInt32();

			// Swallow the button-up that pairs with a button-down we already consumed
			if ((message == MouseHookNativeMethods.WM_MBUTTONUP) || (message == MouseHookNativeMethods.WM_XBUTTONUP))
			{
				if (this._swallowedButton.HasValue && this.TryGetButton(message, lParam, out MouseButton releasedButton) && (releasedButton == this._swallowedButton.Value))
				{
					this._swallowedButton = null;
					return (IntPtr)1;
				}

				return MouseHookNativeMethods.CallNextHookEx(this._hookId, nCode, wParam, lParam);
			}

			if ((message != MouseHookNativeMethods.WM_MBUTTONDOWN) && (message != MouseHookNativeMethods.WM_XBUTTONDOWN))
			{
				return MouseHookNativeMethods.CallNextHookEx(this._hookId, nCode, wParam, lParam);
			}

			if (!this.TryGetButton(message, lParam, out MouseButton button))
			{
				return MouseHookNativeMethods.CallNextHookEx(this._hookId, nCode, wParam, lParam);
			}

			Keys modifiers = MouseHookHandler.GetCurrentModifiers();

			Action callback = null;
			foreach (Binding binding in this._bindings)
			{
				if ((binding.Button == button) && (binding.Modifiers == modifiers))
				{
					callback = binding.Callback;
					break;
				}
			}

			if (callback == null)
			{
				// Not bound - let the click pass through to whatever is underneath
				return MouseHookNativeMethods.CallNextHookEx(this._hookId, nCode, wParam, lParam);
			}

			this._swallowedButton = button;

			try
			{
				callback();
			}
			catch
			{
				// A failure in the cycle logic must never break the global mouse hook
			}

			// Consume the event so it does not reach the focused application (e.g. EVE)
			return (IntPtr)1;
		}

		private bool TryGetButton(int message, IntPtr lParam, out MouseButton button)
		{
			if ((message == MouseHookNativeMethods.WM_MBUTTONDOWN) || (message == MouseHookNativeMethods.WM_MBUTTONUP))
			{
				button = MouseButton.Middle;
				return true;
			}

			// X button: which one is encoded in the high word of mouseData
			MouseHookNativeMethods.MSLLHOOKSTRUCT data = System.Runtime.InteropServices.Marshal.PtrToStructure<MouseHookNativeMethods.MSLLHOOKSTRUCT>(lParam);
			int xButton = (int)((data.mouseData >> 16) & 0xFFFF);

			switch (xButton)
			{
				case MouseHookNativeMethods.XBUTTON1:
					button = MouseButton.XButton1;
					return true;
				case MouseHookNativeMethods.XBUTTON2:
					button = MouseButton.XButton2;
					return true;
				default:
					button = MouseButton.Middle;
					return false;
			}
		}

		private static Keys GetCurrentModifiers()
		{
			Keys modifiers = Keys.None;

			if (MouseHookHandler.IsKeyDown(MouseHookNativeMethods.VK_CONTROL))
			{
				modifiers |= Keys.Control;
			}

			if (MouseHookHandler.IsKeyDown(MouseHookNativeMethods.VK_SHIFT))
			{
				modifiers |= Keys.Shift;
			}

			if (MouseHookHandler.IsKeyDown(MouseHookNativeMethods.VK_MENU))
			{
				modifiers |= Keys.Alt;
			}

			return modifiers;
		}

		private static bool IsKeyDown(int virtualKey)
		{
			return (MouseHookNativeMethods.GetKeyState(virtualKey) & 0x8000) != 0;
		}

		/// <summary>
		/// Attempts to interpret a configuration token (e.g. "M4", "Control+M5",
		/// "XButton1", "Middle") as a mouse-button binding. Returns false for any
		/// token whose key portion is not a recognised mouse button, in which case
		/// the token should be treated as a keyboard hotkey instead.
		/// </summary>
		public static bool TryParse(string token, out MouseButton button, out Keys modifiers)
		{
			button = MouseButton.Middle;
			modifiers = Keys.None;

			if (string.IsNullOrWhiteSpace(token))
			{
				return false;
			}

			string[] parts = token.Split('+');
			string keyPart = parts[parts.Length - 1].Trim();

			if (!MouseHookHandler.TryParseButton(keyPart, out button))
			{
				return false;
			}

			for (int i = 0; i < parts.Length - 1; i++)
			{
				switch (parts[i].Trim().ToLowerInvariant())
				{
					case "control":
					case "ctrl":
						modifiers |= Keys.Control;
						break;
					case "shift":
						modifiers |= Keys.Shift;
						break;
					case "alt":
						modifiers |= Keys.Alt;
						break;
					default:
						// Unknown modifier - reject so it is not silently misbound
						return false;
				}
			}

			return true;
		}

		private static bool TryParseButton(string value, out MouseButton button)
		{
			switch (value.ToLowerInvariant())
			{
				case "mbutton":
				case "middle":
				case "m3":
					button = MouseButton.Middle;
					return true;
				case "xbutton1":
				case "mouse4":
				case "m4":
					button = MouseButton.XButton1;
					return true;
				case "xbutton2":
				case "mouse5":
				case "m5":
					button = MouseButton.XButton2;
					return true;
				default:
					button = MouseButton.Middle;
					return false;
			}
		}
	}
}
