using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
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
		private readonly bool _enableWineCompatabilityMode;
		private string _bashLocation;
		private string _wmctrlLocation;
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
	        List<string> paths = new List<string> { "/run/host/usr/bin", "/bin", "/usr/bin" };
	        string searchPaths = Environment.GetEnvironmentVariable("WMCTRL_PATH");
			if (searchPaths != null) {
				paths.AddRange(searchPaths.split(":"))
			}
			
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

		private void TurnOffAnimation()
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

		private void RestoreAnimation()
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

			int style = User32NativeMethods.GetWindowLong(handle, InteropConstants.GWL_STYLE);

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
                    cmd = $"-c \"flatpak-spawn --host {this._wmctrlLocation}/wmctrl -a \"\"" + windowName + "\"\"\"";
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

			int style = User32NativeMethods.GetWindowLong(handle, InteropConstants.GWL_STYLE);

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

		public bool IsWindowMaximized(IntPtr handle)
		{
			return User32NativeMethods.IsZoomed(handle);
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
			var sourceContext = User32NativeMethods.GetDC(source);

			User32NativeMethods.GetClientRect(source, out RECT windowRect);

			var width = windowRect.Right - windowRect.Left;
			var height = windowRect.Bottom - windowRect.Top;

			// Check if there is anything to make thumbnail of
			if ((width < WINDOW_SIZE_THRESHOLD) || (height < WINDOW_SIZE_THRESHOLD))
			{
				return null;
			}

			var destContext = Gdi32NativeMethods.CreateCompatibleDC(sourceContext);
			var bitmap = Gdi32NativeMethods.CreateCompatibleBitmap(sourceContext, width, height);

			var oldBitmap = Gdi32NativeMethods.SelectObject(destContext, bitmap);
			Gdi32NativeMethods.BitBlt(destContext, 0, 0, width, height, sourceContext, 0, 0, Gdi32NativeMethods.SRCCOPY);
			Gdi32NativeMethods.SelectObject(destContext, oldBitmap);
			Gdi32NativeMethods.DeleteDC(destContext);
			User32NativeMethods.ReleaseDC(source, sourceContext);

			Image image = Image.FromHbitmap(bitmap);
			Gdi32NativeMethods.DeleteObject(bitmap);

			return image;
		}
	}
}
