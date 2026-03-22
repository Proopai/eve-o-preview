using EveOPreview.Configuration;
using System;
using System.Drawing;

namespace EveOPreview.Services
{
	public interface IWindowManager
	{
		bool IsCompositionEnabled { get; }

		IntPtr GetForegroundWindowHandle();
#if LINUX
		void ActivateWindow(IntPtr handle, string windowName);
#else
		void ActivateWindow(IntPtr handle, AnimationStyle animation);
#endif
		void MinimizeWindow(IntPtr handle, AnimationStyle animation, bool enableAnimation);
		void MoveWindow(IntPtr handle, int left, int top, int width, int height);
		void MaximizeWindow(IntPtr handle);
		(int Left, int Top, int Right, int Bottom) GetWindowPosition(IntPtr handle);
		Size GetClientSize(IntPtr handle);
		void PropagateLeftClick(IntPtr handle, Point clientPoint);
		void PropagateRightClick(IntPtr handle, Point clientPoint);
		bool IsWindowMaximized(IntPtr handle);
		bool IsWindowMinimized(IntPtr handle);
		IDwmThumbnail GetLiveThumbnail(IntPtr destination, IntPtr source);
		Image GetStaticThumbnail(IntPtr source);
		Image GetStaticThumbnail(IntPtr source, Rectangle sourceRect);
	}
}
