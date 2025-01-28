using System;
using System.IO;
using System.Drawing;
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
        #endregion

        public WindowManager(IThumbnailConfiguration configuration)
		{
			this._enableWineCompatabilityMode = configuration.EnableWineCompatibilityMode;

			// Composition is always enabled for Windows 8+
			this.IsCompositionEnabled = 
				((Environment.OSVersion.Version.Major == 6) && (Environment.OSVersion.Version.Minor >= 2)) // Win 8 and Win 8.1
				|| (Environment.OSVersion.Version.Major >= 10) // Win 10
				|| DwmNativeMethods.DwmIsCompositionEnabled(); // In case of Win 7 an API call is requiredWin 7
		}

		public bool IsCompositionEnabled { get; }

		public IntPtr GetForegroundWindowHandle()
		{
			return User32NativeMethods.GetForegroundWindow();
		}

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

		private void WriteToLog(string filePath, string message)
		{
			try
			{
				System.IO.File.AppendAllText(filePath, message + Environment.NewLine);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Failed to write to log file: {ex.Message}");
			}
		}

		private void WineActivateWindow(string windowName)
		{
			if (string.IsNullOrEmpty(windowName))
			{
				return;
			}

			string logFilePath = "logs.txt";
			string bashPath = FindBashPath();
			if (bashPath == null)
			{
				File.AppendAllText(logFilePath, $"[{DateTime.Now}] Error: bash not found in expected locations.\n");
				return;
			}
			string binLocation = System.IO.Path.GetDirectoryName(bashPath);
			string binLocationUnixStyle = binLocation.Replace("\\", "/");

			var cmd = $"-c \"{binLocationUnixStyle}/wmctrl -a \\\"{windowName}\\\"\"";

			try
			{
				// Configure and start the process
				var processStartInfo = new System.Diagnostics.ProcessStartInfo
				{
					FileName = $"{binLocationUnixStyle}/bash",
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
				WriteToLog(logFilePath, $"[{DateTime.Now}] Exception: {ex.Message}");
			}
		}

		private string FindBashPath()
		{
			// Check common paths for bash
			string[] paths = { "/run/host/usr/bin/bash", "/bin/bash" };
			foreach (var path in paths)
			{
				if (System.IO.File.Exists(path))
				{
					return path;
				}
			}
			return null;
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