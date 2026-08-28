using System;
using System.Runtime.InteropServices;

namespace EveOPreview.Services.Interop
{
	static class User32NativeMethods
	{
		public const int SW_HIDE = 0;
		public const int SW_SHOWNORMAL = 1;
		public const int SW_NORMAL = 1;
		public const int SW_SHOWMINIMIZED = 2;
		public const int SW_SHOWMAXIMIZED = 3;
		public const int SW_MAXIMIZE = 3;
		public const int SW_SHOWNOACTIVATE = 4;
		public const int SW_SHOW = 5;
		public const int SW_MINIMIZE = 6;
		public const int SW_SHOWMINNOACTIVE = 7;
		public const int SW_SHOWNA = 8;
		public const int SW_RESTORE = 9;
		public const int SW_SHOWDEFAULT = 10;
		public const int SW_FORCEMINIMIZE = 11; 

		public const uint SPI_SETANIMATION = 0x0049;
		public const uint SPI_GETANIMATION = 0x0048;

		public static readonly IntPtr HWND_BROADCAST = new IntPtr(0xFFFF);

		public static readonly IntPtr HWND_TOP = new IntPtr(0);
		public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

		[DllImport("user32.dll")]
		public static extern IntPtr GetForegroundWindow();

		[DllImport("user32.dll")]
		public static extern bool SetForegroundWindow(IntPtr window);

		[DllImport("user32.dll")]
		public static extern void SetFocus(IntPtr window);

		[DllImport("user32.dll")]
		public static extern void EnableWindow(IntPtr window, bool isEnabled);

		[DllImport("user32.dll")]
		public static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

		[DllImport("User32.dll")]
		public static extern bool ReleaseCapture();

		[DllImport("User32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern UInt32 GetWindowLong(IntPtr hWnd, int nIndex);

		[DllImport("user32.dll")]
		public static extern int SetWindowLong(IntPtr hWnd, int nIndex, UInt32 dwNewLong);

		[DllImport("user32.dll")]
		public static extern int GetWindowRect(IntPtr hWnd, out RECT rect);

		[DllImport("user32.dll")]
		public static extern bool GetClientRect(IntPtr hWnd, out RECT rect);

		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

		[DllImport("user32.dll")]
		public static extern bool SetWindowPlacement(IntPtr hWnd, [In] ref WINDOWPLACEMENT lpwndpl);

		[DllImport("user32.dll")]
		public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

		[DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern uint RegisterWindowMessage(string message);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool PostMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool IsIconic(IntPtr hWnd);

		[DllImport("user32.dll")]
		public static extern bool IsZoomed(IntPtr hWnd);

		[DllImport("user32.dll")]
		public static extern IntPtr GetWindowDC(IntPtr hWnd);

		[DllImport("user32.dll")]
		public static extern IntPtr GetDC(IntPtr hWnd);

		[DllImport("user32.dll")]
		public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hdc);

		[DllImport("user32.dll")]
		public static extern long SystemParametersInfo(long uAction, int lpvParam, ref ANIMATIONINFO uParam, int fuWinIni);

	}
}