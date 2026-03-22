using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using EveOPreview.Configuration;
using EveOPreview.Services.Interop;

namespace EveOPreview.Services.Implementation
{
	public class WindowManager : IWindowManager
	{
		#region Private constants
		private const int WINDOW_SIZE_THRESHOLD = 300;
		private const int NO_ANIMATION = 0;
		#endregion

		#region Private fields
#if LINUX
		private readonly bool _enableWineCompatabilityMode;
		private string _bashLocation;
		private string _wmctrlLocation;
#endif
		private const string EXCEPTION_DUMP_FILE_NAME = "EVE-O-Preview.log";
		#endregion


		public WindowManager(IThumbnailConfiguration configuration)
		{
#if LINUX
			this._enableWineCompatabilityMode = configuration.EnableWineCompatibilityMode;
			this._bashLocation = FindLinuxBinLocation("bash");
			this._wmctrlLocation = FindLinuxBinLocation("wmctrl");
#endif
			// Composition is always enabled for Windows 8+
			this.IsCompositionEnabled = 
				((Environment.OSVersion.Version.Major == 6) && (Environment.OSVersion.Version.Minor >= 2)) // Win 8 and Win 8.1
				|| (Environment.OSVersion.Version.Major >= 10) // Win 10
				|| DwmNativeMethods.DwmIsCompositionEnabled(); // In case of Win 7 an API call is requiredWin 7
			_animationParam.cbSize = (System.UInt32)Marshal.SizeOf(typeof(ANIMATIONINFO));
		}
#if LINUX
		private string FindLinuxBinLocation(string command)
		{
			// Check common paths for command
			string[] paths = { "/run/host/usr/bin", "/bin", "/usr/bin" };
			foreach (var path in paths)
			{
			    string locationToCheck = $"{path}/{command}";
				if (System.IO.File.Exists(locationToCheck))
				{
					string binLocation = System.IO.Path.GetDirectoryName(locationToCheck);
					string binLocationUnixStyle = binLocation.Replace("\\", "/");

					return binLocationUnixStyle;
				}
			}

			WriteToLog($"[{DateTime.Now}] Error: {command} not found in expected locations.");
			return null;
		}
#endif

		private void WriteToLog(string message)
		{
			try
			{
				System.IO.File.AppendAllText(EXCEPTION_DUMP_FILE_NAME, message + Environment.NewLine);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Failed to write to log file: {ex.Message}");
			}
		}

		private int? _currentAnimationSetting = null;
		private ANIMATIONINFO _animationParam = new ANIMATIONINFO();

		public bool IsCompositionEnabled { get; }

		public IntPtr GetForegroundWindowHandle()
		{
			return User32NativeMethods.GetForegroundWindow();
		}

		public void TurnOffAnimation()
		{
			var currentAnimationSetup = User32NativeMethods.SystemParametersInfo(User32NativeMethods.SPI_GETANIMATION, (System.Int32)Marshal.SizeOf(typeof(ANIMATIONINFO)), ref _animationParam, 0);
			if (_currentAnimationSetting == null)
			{
				// Store the current Animation Setting
				_currentAnimationSetting = _animationParam.iMinAnimate;
			}

			if (currentAnimationSetup != NO_ANIMATION)
			{
				// Turn off Animation
				_animationParam.iMinAnimate = NO_ANIMATION;
				var animationOffReturn = User32NativeMethods.SystemParametersInfo(User32NativeMethods.SPI_SETANIMATION, (System.Int32)Marshal.SizeOf(typeof(ANIMATIONINFO)), ref _animationParam, 0);
			}
		}

		public void RestoreAnimation()
		{
			var currentAnimationSetup = User32NativeMethods.SystemParametersInfo(User32NativeMethods.SPI_GETANIMATION, (System.Int32)Marshal.SizeOf(typeof(ANIMATIONINFO)), ref _animationParam, 0);
			// Restore current Animation Settings
			if (_animationParam.iMinAnimate != (int)_currentAnimationSetting)
			{
				_animationParam.iMinAnimate = (int)_currentAnimationSetting;
				var animationResetReturn = User32NativeMethods.SystemParametersInfo(User32NativeMethods.SPI_SETANIMATION, (System.Int32)Marshal.SizeOf(typeof(ANIMATIONINFO)), ref _animationParam, 0);
			}
		}

		// if building for LINUX the window handling is slightly different
#if LINUX
		private void WindowsActivateWindow(IntPtr handle)
		{
			User32NativeMethods.SetForegroundWindow(handle);
			User32NativeMethods.SetFocus(handle);

			uint style = User32NativeMethods.GetWindowLong(handle, InteropConstants.GWL_STYLE);

			if ((style & InteropConstants.WS_MINIMIZE) == InteropConstants.WS_MINIMIZE)
			{
				User32NativeMethods.ShowWindowAsync(handle, InteropConstants.SW_RESTORE);
			}
		}

		private void WineActivateWindow(string windowName)
		{
			// On Wine it is not possible to manipulate windows directly.
			// They are managed by native Window Manager
			// So a separate command-line utility is used
			if (string.IsNullOrEmpty(windowName))
			{
				return;
			}

            string cmd = "";
			try
			{
                // If we are in a flatpak, then use flatpak-spawn to run wmctrl outside the sandbox
                if (Environment.GetEnvironmentVariable("container") == "flatpak")
                {
                    cmd = $"-c \"flatpak-spawn --host wmctrl -a \"\"" + windowName + "\"\"\"";
                } 
                else 
                {
                    cmd = $"-c \"{this._wmctrlLocation}/wmctrl -a \"\"" + windowName + "\"\"\"";
                }

				// Configure and start the process
				var processStartInfo = new System.Diagnostics.ProcessStartInfo
				{
					FileName = $"{this._bashLocation}/bash",
					Arguments = cmd,
					UseShellExecute = false,
					CreateNoWindow = false
				};

				using (var process = System.Diagnostics.Process.Start(processStartInfo))
				{
					process.WaitForExit();
				}
			}
			catch (Exception ex)
			{
				WriteToLog($"[{DateTime.Now}] executing wmctrl - Exception: {ex.Message}");
			}
		}

        public void ActivateWindow(IntPtr handle, string windowName)
        {
            if (this._enableWineCompatabilityMode)
            {
                this.WineActivateWindow(windowName);
            }
            else
            {
                this.WindowsActivateWindow(handle);
            }
        }

        public void MinimizeWindow(IntPtr handle, bool enableAnimation)
		{
			if (enableAnimation)
			{
				User32NativeMethods.SendMessage(handle, InteropConstants.WM_SYSCOMMAND, InteropConstants.SC_MINIMIZE, 0);
			}
			else
			{
				WINDOWPLACEMENT param = new WINDOWPLACEMENT();
				param.length = Marshal.SizeOf(typeof(WINDOWPLACEMENT));
				User32NativeMethods.GetWindowPlacement(handle, ref param);
				param.showCmd = WINDOWPLACEMENT.SW_MINIMIZE;
				User32NativeMethods.SetWindowPlacement(handle, ref param);
			}
		}

#endif

#if WINDOWS
		public void ActivateWindow(IntPtr handle, AnimationStyle animation)
		{
			User32NativeMethods.SetForegroundWindow(handle);
			User32NativeMethods.SetFocus(handle);

			uint style = User32NativeMethods.GetWindowLong(handle, InteropConstants.GWL_STYLE);

			if ((style & InteropConstants.WS_MINIMIZE) == InteropConstants.WS_MINIMIZE)
			{
				switch (animation)
				{
					case AnimationStyle.OriginalAnimation:
						User32NativeMethods.ShowWindowAsync(handle, InteropConstants.SW_RESTORE);
						break;
					case AnimationStyle.NoAnimation:
						TurnOffAnimation();
						User32NativeMethods.ShowWindowAsync(handle, InteropConstants.SW_RESTORE);
						RestoreAnimation();
						break;
				}
			}
		}

		public void MinimizeWindow(IntPtr handle, AnimationStyle animation, bool enableAnimation)
		{
			if (enableAnimation)
			{
				switch (animation)
				{
					case AnimationStyle.OriginalAnimation:
						User32NativeMethods.SendMessage(handle, InteropConstants.WM_SYSCOMMAND, InteropConstants.SC_MINIMIZE, 0);
						break;
					case AnimationStyle.NoAnimation:
						TurnOffAnimation();
						User32NativeMethods.SendMessage(handle, InteropConstants.WM_SYSCOMMAND, InteropConstants.SC_MINIMIZE, 0);
						RestoreAnimation();
						break;
				}
			}
			else
			{
				switch (animation)
				{
					case AnimationStyle.OriginalAnimation:
						WINDOWPLACEMENT param = new WINDOWPLACEMENT();
						param.length = Marshal.SizeOf(typeof(WINDOWPLACEMENT));
						User32NativeMethods.GetWindowPlacement(handle, ref param);
						param.showCmd = WINDOWPLACEMENT.SW_MINIMIZE;
						User32NativeMethods.SetWindowPlacement(handle, ref param);
						break;
					case AnimationStyle.NoAnimation:
						TurnOffAnimation();
						User32NativeMethods.SendMessage(handle, InteropConstants.WM_SYSCOMMAND, InteropConstants.SC_MINIMIZE, 0);
						RestoreAnimation();
						break;
				}
			}
		}
#endif

		public void MoveWindow(IntPtr handle, int left, int top, int width, int height)
		{
			User32NativeMethods.MoveWindow(handle, left, top, width, height, true);
		}

		public void MaximizeWindow(IntPtr handle)
		{
			User32NativeMethods.ShowWindowAsync(handle, InteropConstants.SW_SHOWMAXIMIZED);
        }

		public (int Left, int Top, int Right, int Bottom) GetWindowPosition(IntPtr handle)
		{
			User32NativeMethods.GetWindowRect(handle, out RECT windowRectangle);

			return (windowRectangle.Left, windowRectangle.Top, windowRectangle.Right, windowRectangle.Bottom);
		}

		public Size GetClientSize(IntPtr handle)
		{
			User32NativeMethods.GetClientRect(handle, out RECT windowRect);
			return new Size(windowRect.Right - windowRect.Left, windowRect.Bottom - windowRect.Top);
		}

		public bool IsWindowMaximized(IntPtr handle)
		{
			return User32NativeMethods.IsZoomed(handle);
		}

		public void PropagateLeftClick(IntPtr handle, Point clientPoint)
		{
			this.PropagateMouseClick(handle, clientPoint, InteropConstants.MOUSEEVENTF_LEFTDOWN, InteropConstants.MOUSEEVENTF_LEFTUP);
		}

		public void PropagateRightClick(IntPtr handle, Point clientPoint)
		{
			this.PropagateMouseClick(handle, clientPoint, InteropConstants.MOUSEEVENTF_RIGHTDOWN, InteropConstants.MOUSEEVENTF_RIGHTUP);
		}

		private void PropagateMouseClick(IntPtr handle, Point clientPoint, uint downFlag, uint upFlag)
		{
			POINT screenPoint = new POINT(Math.Max(0, clientPoint.X), Math.Max(0, clientPoint.Y));
			if (!User32NativeMethods.ClientToScreen(handle, ref screenPoint))
			{
				return;
			}

			User32NativeMethods.GetCursorPos(out POINT originalCursorPoint);

			User32NativeMethods.SetCursorPos(screenPoint.X, screenPoint.Y);
			User32NativeMethods.mouse_event(downFlag, 0, 0, 0, UIntPtr.Zero);
			Thread.Sleep(8);
			User32NativeMethods.mouse_event(upFlag, 0, 0, 0, UIntPtr.Zero);
			Thread.Sleep(8);
			User32NativeMethods.SetCursorPos(originalCursorPoint.X, originalCursorPoint.Y);
		}

		public bool IsWindowMinimized(IntPtr handle)
		{
			return User32NativeMethods.IsIconic(handle);
		}

		public IDwmThumbnail GetLiveThumbnail(IntPtr destination, IntPtr source)
		{
			IDwmThumbnail thumbnail = new DwmThumbnail(this);
			thumbnail.Register(destination, source);

			return thumbnail;
		}

		public Image GetStaticThumbnail(IntPtr source)
		{
			return this.GetStaticThumbnail(source, Rectangle.Empty);
		}

		public Image GetStaticThumbnail(IntPtr source, Rectangle sourceRect)
		{
			var sourceContext = User32NativeMethods.GetDC(source);

			User32NativeMethods.GetClientRect(source, out RECT windowRect);

			var maxWidth = windowRect.Right - windowRect.Left;
			var maxHeight = windowRect.Bottom - windowRect.Top;

			int left = sourceRect.Left;
			int top = sourceRect.Top;
			int width = sourceRect.Width;
			int height = sourceRect.Height;

			if ((width <= 0) || (height <= 0))
			{
				left = 0;
				top = 0;
				width = maxWidth;
				height = maxHeight;
			}

			left = Math.Max(0, left);
			top = Math.Max(0, top);

			if (left >= maxWidth || top >= maxHeight)
			{
				User32NativeMethods.ReleaseDC(source, sourceContext);
				return null;
			}

			width = Math.Min(width, maxWidth - left);
			height = Math.Min(height, maxHeight - top);

			// Check if there is anything to make thumbnail of
			if ((width < WINDOW_SIZE_THRESHOLD) || (height < WINDOW_SIZE_THRESHOLD))
			{
                User32NativeMethods.ReleaseDC(source, sourceContext);

                return null;
			}

			var destContext = Gdi32NativeMethods.CreateCompatibleDC(sourceContext);
			var bitmap = Gdi32NativeMethods.CreateCompatibleBitmap(sourceContext, width, height);

			var oldBitmap = Gdi32NativeMethods.SelectObject(destContext, bitmap);
			Gdi32NativeMethods.BitBlt(destContext, 0, 0, width, height, sourceContext, left, top, Gdi32NativeMethods.SRCCOPY);
			Gdi32NativeMethods.SelectObject(destContext, oldBitmap);
			Gdi32NativeMethods.DeleteDC(destContext);
			User32NativeMethods.ReleaseDC(source, sourceContext);

			Image image = Image.FromHbitmap(bitmap);
			Gdi32NativeMethods.DeleteObject(bitmap);

			return image;
		}
	}
}
