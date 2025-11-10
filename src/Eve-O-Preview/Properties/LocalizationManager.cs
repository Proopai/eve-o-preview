using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace EveOPreview.Properties
{
    public static class LocalizationManager
    {
        private static Dictionary<string, Dictionary<string, string>> _localizations;
        private static string _currentLanguage;

        static LocalizationManager()
        {
            // Initialize localizations first
            InitializeLocalizations();

            // Set default language based on system culture, fallback to English
            _currentLanguage = GetDefaultLanguage();
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(_currentLanguage);
        }

        private static void InitializeLocalizations()
        {
            _localizations = new Dictionary<string, Dictionary<string, string>>
            {
                ["en-US"] = new Dictionary<string, string>
                {
                    // Tab pages
                    ["GeneralTabText"] = "General",
                    ["ThumbnailTabText"] = "Thumbnail",
                    ["ZoomTabText"] = "Zoom",
                    ["OverlayTabText"] = "Overlay",
                    ["ActiveClientsTabText"] = "Active Clients",
                    ["AboutTabText"] = "About",
                    ["LanguageTabText"] = "Language",

                    // Menu items
                    ["RestoreMenuItemText"] = "Restore",
                    ["ExitMenuItemText"] = "Exit",
                    ["ApplicationTitleText"] = "EVE-O-Preview",

                    // Checkboxes
                    ["MinimizeInactiveClientsCheckBoxText"] = "Minimize inactive EVE clients",
                    ["EnableClientLayoutTrackingCheckBoxText"] = "Track client locations",
                    ["HideActiveClientThumbnailCheckBoxText"] = "Hide preview of active EVE client",
                    ["ShowThumbnailsAlwaysOnTopCheckBoxText"] = "Previews always on top",
                    ["HideThumbnailsOnLostFocusCheckBoxText"] = "Hide previews when EVE client is not active",
                    ["EnablePerClientThumbnailsLayoutsCheckBoxText"] = "Unique layout for each EVE client",
                    ["MinimizeToTrayCheckBoxText"] = "Minimize to System Tray",
                    ["ThumbnailSnapToGridCheckBoxText"] = "Thumbnail Snap to Grid",
                    ["LockThumbnailLocationCheckboxText"] = "Lock Thumbnail Location",
                    ["EnableThumbnailZoomCheckBoxText"] = "Zoom on hover",
                    ["EnableActiveClientHighlightCheckBoxText"] = "Highlight active client",
                    ["ShowThumbnailOverlaysCheckBoxText"] = "Show overlay",
                    ["ShowThumbnailFramesCheckBoxText"] = "Show frames",

                    // Labels
                    ["AnimationStyleLabelText"] = "Animation Style",
                    ["ThumbnailHeightLabelText"] = "Height",
                    ["ThumbnailWidthLabelText"] = "Width",
                    ["OpacityLabelText"] = "Opacity",
                    ["SnapXLabelText"] = "Snap X",
                    ["SnapYLabelText"] = "Y",
                    ["ZoomFactorLabelText"] = "Zoom Factor",
                    ["ZoomAnchorLabelText"] = "Anchor",
                    ["ThumbnailsListLabelText"] = "Thumbnails (check to force hide)",
                    ["LabelSizeLabelText"] = "Label Size",
                    ["ColorLabelText"] = "Color",
                    ["PositionLabelText"] = "Position",
                    ["HighlightColorLabelText"] = "Color",

                    // Other
                    ["CreditMaintLabelText"] = "Credit to previous maintainer: Phrynohyas Tig-Rah",
                    ["DocumentationLinkLabelText"] = "For more information visit the forum thread:",
                    ["LanguageLabelText"] = "Language",

                    // Language options
                    ["English (en-US)"] = "English (en-US)",
                    ["中文 (zh-CN)"] = "中文 (zh-CN)"
                },

                ["zh-CN"] = new Dictionary<string, string>
                {
                    // Tab pages
                    ["GeneralTabText"] = "通用",
                    ["ThumbnailTabText"] = "缩略图",
                    ["ZoomTabText"] = "缩放",
                    ["OverlayTabText"] = "悬浮窗",
                    ["ActiveClientsTabText"] = "活动客户端",
                    ["AboutTabText"] = "关于",
                    ["LanguageTabText"] = "语言",

                    // Menu items
                    ["RestoreMenuItemText"] = "恢复",
                    ["ExitMenuItemText"] = "退出",
                    ["ApplicationTitleText"] = "EVE-O-Preview",

                    // Checkboxes
                    ["MinimizeInactiveClientsCheckBoxText"] = "最小化非活动的EVE客户端",
                    ["EnableClientLayoutTrackingCheckBoxText"] = "跟踪客户端位置",
                    ["HideActiveClientThumbnailCheckBoxText"] = "隐藏活动EVE客户端的预览",
                    ["ShowThumbnailsAlwaysOnTopCheckBoxText"] = "预览始终置顶",
                    ["HideThumbnailsOnLostFocusCheckBoxText"] = "当EVE客户端不活动时隐藏预览",
                    ["EnablePerClientThumbnailsLayoutsCheckBoxText"] = "为每个EVE客户端设置独特布局",
                    ["MinimizeToTrayCheckBoxText"] = "最小化到系统托盘",
                    ["ThumbnailSnapToGridCheckBoxText"] = "缩略图对齐网格",
                    ["LockThumbnailLocationCheckboxText"] = "锁定缩略图位置",
                    ["EnableThumbnailZoomCheckBoxText"] = "悬停时缩放",
                    ["EnableActiveClientHighlightCheckBoxText"] = "高亮活动客户端",
                    ["ShowThumbnailOverlaysCheckBoxText"] = "显示悬浮窗",
                    ["ShowThumbnailFramesCheckBoxText"] = "显示边框",

                    // Labels
                    ["AnimationStyleLabelText"] = "动画样式",
                    ["ThumbnailHeightLabelText"] = "缩略图高度",
                    ["ThumbnailWidthLabelText"] = "缩略图宽度",
                    ["OpacityLabelText"] = "不透明度",
                    ["SnapXLabelText"] = "对齐 X",
                    ["SnapYLabelText"] = "Y",
                    ["ZoomFactorLabelText"] = "缩放倍率",
                    ["ZoomAnchorLabelText"] = "锚点",
                    ["ThumbnailsListLabelText"] = "缩略图（勾选强制隐藏）",
                    ["LabelSizeLabelText"] = "标签大小",
                    ["ColorLabelText"] = "颜色",
                    ["PositionLabelText"] = "位置",
                    ["HighlightColorLabelText"] = "颜色",

                    // Other
                    ["CreditMaintLabelText"] = "感谢前任维护者：Phrynohyas Tig-Rah",
                    ["DocumentationLinkLabelText"] = "更多信息请访问论坛帖子：",
                    ["LanguageLabelText"] = "语言",

                    // Language options
                    ["English (en-US)"] = "英语 (en-US)",
                    ["中文 (zh-CN)"] = "中文 (zh-CN)"
                }
            };
        }

        // Determine default language - now defaulting to English
        private static string GetDefaultLanguage()
        {
            // Always default to English as requested
            return "en-US";
        }

        // Check if a language is supported
        private static bool IsLanguageSupported(string languageCode)
        {
            // Check if _localizations has been initialized
            if (_localizations == null) return false;
            return _localizations.ContainsKey(languageCode);
        }

        public static string GetCurrentLanguage()
        {
            return _currentLanguage;
        }

        public static void SetLanguage(string languageCode)
        {
            if (_localizations.ContainsKey(languageCode))
            {
                _currentLanguage = languageCode;
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(languageCode);
            }
        }

        public static string GetString(string key)
        {
            // Try to get string in current language
            if (_localizations.ContainsKey(_currentLanguage) &&
                _localizations[_currentLanguage].ContainsKey(key))
            {
                return _localizations[_currentLanguage][key];
            }

            // Fall back to English
            if (_localizations.ContainsKey("en-US") &&
                _localizations["en-US"].ContainsKey(key))
            {
                return _localizations["en-US"][key];
            }

            // Key not found
            return key;
        }
    }
}