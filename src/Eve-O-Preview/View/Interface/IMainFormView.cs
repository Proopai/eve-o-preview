using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace EveOPreview.View
{
    /// <summary>
    /// Main view interface
    /// Presenter uses it to access GUI properties
    /// </summary>
    public interface IMainFormView : IView
    {
        bool MinimizeToTray { get; set; }

        double ThumbnailOpacity { get; set; }

        bool EnableClientLayoutTracking { get; set; }
        bool HideActiveClientThumbnail { get; set; }
        bool MinimizeInactiveClients { get; set; }
        bool HideCaptionOnClients { get; set; }
        ViewAnimationStyle WindowsAnimationStyle { get; set; }
        bool ShowThumbnailsAlwaysOnTop { get; set; }
        bool PreventPreviews { get; set; }
        bool HideThumbnailsOnLostFocus { get; set; }
        bool EnablePerClientThumbnailLayouts { get; set; }

        Size ThumbnailSize { get; set; }

        bool EnableThumbnailZoom { get; set; }
        int ThumbnailZoomFactor { get; set; }
        ViewZoomAnchor ThumbnailZoomAnchor { get; set; }
        ViewZoomAnchor OverlayLabelAnchor { get; set; }
        ViewZoomAnchor CycleGroupIndicatorAnchor { get; set; }

        bool ShowThumbnailOverlays { get; set; }
        bool ShowThumbnailFrames { get; set; }

        bool LockThumbnailLocation { get; set; }
        bool ThumbnailSnapToGrid { get; set; }
        int ThumbnailSnapToGridSizeX { get; set; }
        int ThumbnailSnapToGridSizeY { get; set; }

        bool EnableActiveClientHighlight { get; set; }
        Color ActiveClientHighlightColor { get; set; }
        Color PreventPreviewColor { get; set; }
        Color OverlayLabelColor { get; set; }
        Font OverlayLabelFont { get; set; }

        string IconName { get; set; }

        void SetDocumentationUrl(string url);
        void SetVersionInfo(string version);
        void SetThumbnailSizeLimitations(Size minimumSize, Size maximumSize);

        void Minimize();

        void AddThumbnails(IList<IThumbnailDescription> thumbnails);
        void RemoveThumbnails(IList<IThumbnailDescription> thumbnails);
        void RefreshZoomSettings();

        Action ApplicationExitRequested { get; set; }
        Action FormActivated { get; set; }
        Action FormMinimized { get; set; }
        Action<ViewCloseRequest> FormCloseRequested { get; set; }
        Action ApplicationSettingsChanged { get; set; }
        Action ThumbnailsSizeChanged { get; set; }
        Action<string> ThumbnailStateChanged { get; set; }
        Action DocumentationLinkActivated { get; set; }

        // Raised when the user assigns/clears a per-client hotkey on the Hotkeys tab (issue #13).
        Action<string, Keys> ClientHotkeyChanged { get; set; }

        // Cycle-group master/detail (issue: unlimited named cycle groups).
        // Master list:
        void SetCycleGroups(IReadOnlyList<string> groupNames, int selectedIndex);
        // Detail for the selected group: its forward/back hotkeys and the titles of its member clients.
        void SetCycleGroupDetail(Keys forward, Keys backward, IReadOnlyList<string> memberClients);
        void SetMinimizeAllHotkey(Keys hotkey);

        Action<int> CycleGroupSelected { get; set; }
        Action<string> CycleGroupAddRequested { get; set; }
        Action<int> CycleGroupRemoveRequested { get; set; }
        Action<int, string> CycleGroupRenameRequested { get; set; }
        Action<int, bool, Keys> CycleGroupHotkeyChanged { get; set; }      // (groupIndex, isForward, hotkey)
        Action<int, string, bool> CycleGroupMembershipChanged { get; set; } // (groupIndex, clientTitle, isMember)
        Action<Keys> MinimizeAllHotkeyChanged { get; set; }

        // --- Profiles ---
        Action<string> ProfileActivateRequested { get; set; }
        Action ProfileNewRequested { get; set; }
        Action ProfileSaveRequested { get; set; }
        Action<string> ProfileDeleteRequested { get; set; }
        Action ProfileResetRequested { get; set; }
        Action ProfileExportRequested { get; set; }
        Action ProfileImportRequested { get; set; }
        string PromptExportPath();
        string PromptImportPath();
        Action<bool> ThemeChanged { get; set; }
        void ApplyTheme(bool dark);

        // Window placement persistence (remember position/minimized across restarts).
        Point CurrentWindowLocation { get; }
        bool IsCurrentlyMinimized { get; }
        void RestoreWindowPlacement(Point location, bool minimized);
        void SetProfiles(IReadOnlyList<string> names, string activeName);
        ProfileSwitchChoice PromptSaveBeforeSwitch(string currentProfile);
        string PromptForProfileName();
        bool PromptResetToDefaults();
        void ShowMessage(string message);
    }
}