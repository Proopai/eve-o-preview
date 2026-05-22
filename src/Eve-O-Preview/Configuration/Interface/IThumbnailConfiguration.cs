using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace EveOPreview.Configuration
{
	public interface IThumbnailConfiguration
	{
		List<string> CycleGroup1ForwardHotkeys { get; set; }
		List<string> CycleGroup1BackwardHotkeys { get; set; }
		Dictionary<string, int> CycleGroup1ClientsOrder { get; set; }

		List<string> CycleGroup2ForwardHotkeys { get; set; }
		List<string> CycleGroup2BackwardHotkeys { get; set; }
		Dictionary<string, int> CycleGroup2ClientsOrder { get; set; }

		List<string> CycleGroup3ForwardHotkeys { get; set; }
		List<string> CycleGroup3BackwardHotkeys { get; set; }
		Dictionary<string, int> CycleGroup3ClientsOrder { get; set; }

		List<string> CycleGroup4ForwardHotkeys { get; set; }
		List<string> CycleGroup4BackwardHotkeys { get; set; }
		Dictionary<string, int> CycleGroup4ClientsOrder { get; set; }

		List<string> CycleGroup5ForwardHotkeys { get; set; }
		List<string> CycleGroup5BackwardHotkeys { get; set; }
		Dictionary<string, int> CycleGroup5ClientsOrder { get; set; }

		List<string> DynamicCycleForwardHotkeys { get; set; }
		List<string> DynamicCycleBackwardHotkeys { get; set; }

		Dictionary<string, Color> PerClientActiveClientHighlightColor { get; set; }
		Dictionary<string, Color> PerClientPreventPreviewColor { get; set; }
		Dictionary<string, bool> PerClientPreventPreviews { get; set; }
		Dictionary<string, Size> PerClientThumbnailSize { get; set; }
		Dictionary<string, bool> CycleGroupExclusions { get; set; }

		bool MinimizeToTray { get; set; }
		int ThumbnailRefreshPeriod { get; set; }
		int ThumbnailResizeTimeoutPeriod { get; set; }
		bool EnableWineCompatibilityMode { get; set; }

		double ThumbnailOpacity { get; set; }

		bool EnableClientLayoutTracking { get; set; }
		bool HideActiveClientThumbnail { get; set; }
		bool HideLoginClientThumbnail { get; set; }
		bool MinimizeInactiveClients { get; set; }
		bool HideCaptionOnClients { get; set; }
		AnimationStyle WindowsAnimationStyle { get; set; }
		bool ShowThumbnailsAlwaysOnTop { get; set; }
		bool EnablePerClientThumbnailLayouts { get; set; }

		bool PreventPreviews { get; set; }
		bool HideThumbnailsOnLostFocus { get; set; }
		bool OnlyRegisterCycleHotkeysWhenEveFocused { get; set; }
		bool DynamicCycleGroup { get; set; }
		int HideThumbnailsDelay { get; set; }

		Size ThumbnailSize { get; set; }
		bool EnableOverwatchMode { get; set; }
		Size FocusedThumbnailSize { get; set; }
		Point FocusedThumbnailLocation { get; set; }
		Size ThumbnailMinimumSize { get; set; }
		Size ThumbnailMaximumSize { get; set; }

		bool EnableThumbnailSnap { get; set; }

		bool ThumbnailZoomEnabled { get; set; }
		int ThumbnailZoomFactor { get; set; }
		ZoomAnchor ThumbnailZoomAnchor { get; set; }
		ZoomAnchor OverlayLabelAnchor { get; set; }
		ZoomAnchor CycleGroupIndicatorAnchor { get; set; }

		bool ShowThumbnailOverlays { get; set; }
		bool ShowThumbnailFrames { get; set; }
		bool LockThumbnailLocation { get; set; }
		bool ThumbnailSnapToGrid {  get; set; }
		int ThumbnailSnapToGridSizeX { get; set; }
		int ThumbnailSnapToGridSizeY { get; set; }
		bool ThumbnailSnapToEdges { get; set; }

		bool EnableActiveClientHighlight { get; set; }
		Color ActiveClientHighlightColor { get; set; }
		Color PreventPreviewColor { get; set; }
		int ActiveClientHighlightThickness { get; set; }
		Color OverlayLabelColor { get; set; }
		Font OverlayLabelFont { get; set; }

		string IconName { get; set; }
		List<string> MinimizeAllClientsHotkeys { get; set; }

		Point LoginThumbnailLocation { get; set; }

		/// <summary>Full path to the directory where cached character portrait images are stored (typically "thumbs" next to the executable).</summary>
		string PortraitThumbnailsDirectory { get; set; }

		/// <summary>Maps EVE window title (e.g. "EVE - Character Name") to the full path of the cached portrait image file.</summary>
		Dictionary<string, string> ClientPortraitPaths { get; set; }

		Point GetThumbnailLocation(string currentClient, string activeClient, Point defaultLocation);
		Size GetThumbnailSize(string currentClient, string activeClient, Size defaultSize);
		ZoomAnchor GetZoomAnchor(string currentClient, ZoomAnchor defaultZoomAnchor);
		void SetThumbnailLocation(string currentClient, string activeClient, Point location);

		ClientLayout GetClientLayout(string currentClient);
		void SetClientLayout(string currentClient, ClientLayout layout);

		Keys GetClientHotkey(string currentClient);
		void SetClientHotkey(string currentClient, Keys hotkey);
		Keys StringToKey(string hotkey);
		bool IsPriorityClient(string currentClient);
		bool IsExecutableToPreview(string processName);

		bool IsThumbnailDisabled(string currentClient);
		void ToggleThumbnail(string currentClient, bool isDisabled);

		void ApplyRestrictions();
	}
}