using System;
using System.Drawing;
using System.Windows.Forms;
using EveOPreview.Configuration;
using EveOPreview.Services;

namespace EveOPreview.View
{
	public interface IThumbnailView : IView
	{
		IntPtr Id { get; set; }
		string Title { get; set; }

		bool IsActive { get; set; }
		Point ThumbnailLocation { get; set; }
		Size ThumbnailSize { get; set; }
		bool IsOverlayEnabled { get; set; }
		bool IsExcludedFromCycleGroup { get; set; }

		ZoomAnchor ClientZoomAnchor { get; set; }
		bool IsKnownHandle(IntPtr handle);

		void SetSizeLimitations(Size minimumSize, Size maximumSize);
		void SetOpacity(double opacity);
		void SetFrames(bool enable);
		void SetOverlayLabel();
		void SetCycleGroupIndicator(bool displayCycleGroup, ZoomAnchor anchor);
		void SetTopMost(bool enableTopmost);
		void SetHighlight();
		void SetHighlight(bool enabled, int width);
		bool IsPreventPreviews();
		void SetPreventPreviews();
		void ZoomIn(ViewZoomAnchor anchor, int zoomFactor);
		void ZoomOut();

		void RegisterHotkey(Keys hotkey);
		void UnregisterHotkey();

		void Refresh(bool forceRefresh);

		Action<IntPtr> ThumbnailResized { get; set; }
		Action<IntPtr> ThumbnailMoved { get; set; }
		Action<IntPtr> ThumbnailFocused { get; set; }
		Action<IntPtr> ThumbnailLostFocus { get; set; }

		Action<IntPtr> ThumbnailActivated { get; set; }
		Action<IntPtr, bool> ThumbnailDeactivated { get; set; }
		Action<IntPtr> ThumbnailFocusedOverwatchToggle { get; set; }
		Action<IntPtr> ThumbnailToggleCycleGroup { get; set; }

		IWindowManager WindowManager { get; }
		void SetDefaultBorderColor();
		void ClearBorder();
	}
}