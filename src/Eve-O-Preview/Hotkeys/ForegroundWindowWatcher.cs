using System;
using System.Runtime.InteropServices;

namespace EveOPreview.UI.Hotkeys
{
	// Raises ForegroundChanged whenever the OS foreground window changes, delivering the handle of the new
	// foreground window.
	//
	// This is used to register the cycle / minimize hotkeys (via RegisterHotKey) only while an EVE client or
	// an EVE-O-Preview window is focused, and to unregister them otherwise so the keys pass through to their
	// native action. Doing it this way keeps the implicit foreground-activation right that RegisterHotKey
	// grants (a low-level keyboard hook does not), which is what makes window switching fast and reliable.
	sealed class ForegroundWindowWatcher : IDisposable
	{
		private const uint EVENT_SYSTEM_FOREGROUND = 0x0003;
		private const uint WINEVENT_OUTOFCONTEXT = 0x0000;

		private delegate void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);

		[DllImport("user32.dll")]
		private static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventProc lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool UnhookWinEvent(IntPtr hWinEventHook);

		// Keep a reference to the delegate so it is not garbage collected while the hook is installed
		private readonly WinEventProc _proc;

		private IntPtr _hook;

		public ForegroundWindowWatcher()
		{
			this._proc = this.Callback;
			this._hook = IntPtr.Zero;
		}

		public event Action<IntPtr> ForegroundChanged;

		// Must be called from a thread that pumps Windows messages (the UI thread). The WINEVENT_OUTOFCONTEXT
		// callback is delivered on that thread's message loop.
		public void Start()
		{
			if (this._hook != IntPtr.Zero)
			{
				return;
			}

			this._hook = SetWinEventHook(EVENT_SYSTEM_FOREGROUND, EVENT_SYSTEM_FOREGROUND, IntPtr.Zero, this._proc, 0, 0, WINEVENT_OUTOFCONTEXT);
		}

		public void Stop()
		{
			if (this._hook == IntPtr.Zero)
			{
				return;
			}

			UnhookWinEvent(this._hook);
			this._hook = IntPtr.Zero;
		}

		private void Callback(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
		{
			if ((eventType != EVENT_SYSTEM_FOREGROUND) || (hwnd == IntPtr.Zero))
			{
				return;
			}

			try
			{
				this.ForegroundChanged?.Invoke(hwnd);
			}
			catch
			{
				// Never let an exception escape into the WinEvent callback chain
			}
		}

		public void Dispose()
		{
			this.Stop();
		}
	}
}
