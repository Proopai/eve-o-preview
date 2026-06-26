using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace EveOPreview.Configuration
{
    public interface IThumbnailConfiguration
    {
        // Unlimited, named cycle groups (replaces the former fixed CycleGroup1..5 properties).
        List<CycleGroupConfiguration> CycleGroups { get; set; }

        Dictionary<string, Color> PerClientActiveClientHighlightColor { get; set; }
        Dictionary<string, Color> PerClientPreventPreviewColor { get; set; }
        Dictionary<string, bool> PerClientPreventPreviews { get; set; }
        Dictionary<string, Size> PerClientThumbnailSize { get; set; }
        Dictionary<string, bool> CycleGroupExclusions { get; set; }

        bool MinimizeToTray { get; set; }
        bool DarkMode { get; set; }
        Point MainWindowLocation { get; set; }
        bool MainWindowMinimized { get; set; }
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
        int HideThumbnailsDelay { get; set; }

        Size ThumbnailSize { get; set; }
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
        bool ThumbnailSnapToGrid { get; set; }
        int ThumbnailSnapToGridSizeX { get; set; }
        int ThumbnailSnapToGridSizeY { get; set; }

        bool EnableActiveClientHighlight { get; set; }
        Color ActiveClientHighlightColor { get; set; }
        Color PreventPreviewColor { get; set; }
        int ActiveClientHighlightThickness { get; set; }
        Color OverlayLabelColor { get; set; }
        Font OverlayLabelFont { get; set; }

        string IconName { get; set; }
        List<string> MinimizeAllClientsHotkeys { get; set; }

        Point LoginThumbnailLocation { get; set; }

        Point GetThumbnailLocation(string currentClient, string activeClient, Point defaultLocation);
        Size GetThumbnailSize(string currentClient, string activeClient, Size defaultSize);
        ZoomAnchor GetZoomAnchor(string currentClient, ZoomAnchor defaultZoomAnchor);
        void SetThumbnailLocation(string currentClient, string activeClient, Point location);

        ClientLayout GetClientLayout(string currentClient);
        void SetClientLayout(string currentClient, ClientLayout layout);

        Keys GetClientHotkey(string currentClient);
        void SetClientHotkey(string currentClient, Keys hotkey);
        Keys StringToKey(string hotkey);
        string KeyToString(Keys hotkey);
        bool IsPriorityClient(string currentClient);
        bool IsExecutableToPreview(string processName);

        bool IsThumbnailDisabled(string currentClient);
        void ToggleThumbnail(string currentClient, bool isDisabled);

        void ApplyRestrictions();
    }
}