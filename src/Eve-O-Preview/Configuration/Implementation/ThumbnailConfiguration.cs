using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace EveOPreview.Configuration.Implementation
{
    sealed class ThumbnailConfiguration : IThumbnailConfiguration
    {
        #region Private fields
        private bool _enablePerClientThumbnailLayouts;
        private bool _enableClientLayoutTracking;
        #endregion

        public ThumbnailConfiguration()
        {
            this.ConfigVersion = 1;

            this.CycleGroup1ForwardHotkeys = new List<string> { "F14", "Control+F14" };
            this.CycleGroup1BackwardHotkeys = new List<string> { "F13", "Control+F13" };
            this.CycleGroup1ClientsOrder = new Dictionary<string, int>
            {
                { "EVE - Example DPS Toon 1", 1 },
                { "EVE - Example DPS Toon 2", 2 },
                { "EVE - Example DPS Toon 3", 3 }
            };

            this.CycleGroup2ForwardHotkeys = new List<string> { "F16", "Control+F16" };
            this.CycleGroup2BackwardHotkeys = new List<string> { "F15", "Control+F15" };
            this.CycleGroup2ClientsOrder = new Dictionary<string, int>
            {
                { "EVE - Example Logi Toon 1", 1 },
                { "EVE - Example Scout Toon 2", 2 },
                { "EVE - Example Tackle Toon 3", 3 }
            };

            this.CycleGroup3ForwardHotkeys = new List<string> { "" };
            this.CycleGroup3BackwardHotkeys = new List<string> { "" };
            this.CycleGroup3ClientsOrder = new Dictionary<string, int>
            {
                { "EVE - cycle group 3", 1 },
            };
            this.CycleGroup4ForwardHotkeys = new List<string> { "" };
            this.CycleGroup4BackwardHotkeys = new List<string> { "" };
            this.CycleGroup4ClientsOrder = new Dictionary<string, int>
            {
                { "EVE - cycle group 4", 1 },
            };
            this.CycleGroup5ForwardHotkeys = new List<string> { "" };
            this.CycleGroup5BackwardHotkeys = new List<string> { "" };
            this.CycleGroup5ClientsOrder = new Dictionary<string, int>
            {
                { "EVE - cycle group 5", 1 },
            };

            this.PerClientActiveClientHighlightColor = new Dictionary<string, Color>
            {
                {"EVE - Example Toon 1", Color.Red},
                {"EVE - Example Toon 2", Color.Green}
            };
            this.PerClientPreventPreviewColor = new Dictionary<string, Color>
            {
                {"EVE - Example Toon 1", Color.Red},
                {"EVE - Example Toon 2", Color.Green}
            };
            this.PerClientPreventPreviews = new Dictionary<string, bool>
            {
                {"EVE - Example Toon 1", false},
                {"EVE - Example Toon 2", true}
            };

            this.PerClientThumbnailSize = new Dictionary<string, Size>
            {
                {"EVE - Example Toon 1", new Size(200, 200)},
                {"EVE - Example Toon 2", new Size(200, 200)}
            };

            this.PerClientZoomAnchor = new Dictionary<string, ZoomAnchor>
            {
                {"EVE - Example Toon 1", ZoomAnchor.N },
                {"EVE - Example Toon 2", ZoomAnchor.S}
            };

            this.PerClientLayout = new Dictionary<string, Dictionary<string, Point>>();
            this.FlatLayout = new Dictionary<string, Point>();
            this.ClientLayout = new Dictionary<string, ClientLayout>();
            this.ClientHotkey = new Dictionary<string, string>();
            this.MinimizeAllClientsHotkeys = new List<string> { "Control+F22" };
            this.DisableThumbnail = new Dictionary<string, bool>();
            this.PriorityClients = new List<string>();

            this.ExecutablesToPreview = new List<string> { "exefile" };

            this.MinimizeToTray = false;
            this.ThumbnailRefreshPeriod = 500;
            this.ThumbnailResizeTimeoutPeriod = 500;

#if LINUX
			this.EnableWineCompatibilityMode = true;
#else
            this.EnableWineCompatibilityMode = false;
#endif

            this.ThumbnailOpacity = 0.5;

            this.EnableClientLayoutTracking = false;
            this.HideActiveClientThumbnail = false;
            this.HideLoginClientThumbnail = false;
            this.MinimizeInactiveClients = false;
            this.HideCaptionOnClients = false;
            this.WindowsAnimationStyle = AnimationStyle.NoAnimation;
            this.ShowThumbnailsAlwaysOnTop = true;
            this.EnablePerClientThumbnailLayouts = false;

            this.HideThumbnailsOnLostFocus = false;
            this.PreventPreviews = false;
            this.HideThumbnailsDelay = 2; // 2 thumbnails refresh cycles (1.0 sec)

            this.ThumbnailSize = new Size(384, 216);
            this.ThumbnailMinimumSize = new Size(192, 108);
            this.ThumbnailMaximumSize = new Size(960, 540);

            this.EnableThumbnailSnap = true;

            this.ThumbnailZoomEnabled = false;
            this.ThumbnailZoomFactor = 2;
            this.ThumbnailZoomAnchor = ZoomAnchor.NW;
            this.OverlayLabelAnchor = ZoomAnchor.NW;
            this.CycleGroupIndicatorAnchor = ZoomAnchor.NW;

            this.ShowThumbnailOverlays = true;
            this.ShowThumbnailFrames = false;
            this.LockThumbnailLocation = false;

            this.ThumbnailSnapToGrid = true;
            this.ThumbnailSnapToGridSizeX = 100;
            this.ThumbnailSnapToGridSizeY = 50;

            this.EnableActiveClientHighlight = false;
            this.ActiveClientHighlightColor = Color.GreenYellow;
            this.PreventPreviewColor = Color.Purple;
            this.ActiveClientHighlightThickness = 3;

            this.OverlayLabelColor = Color.Orange;
            this.OverlayLabelFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Bold);

            this.IconName = "";

            this.LoginThumbnailLocation = new Point(5, 5);
            // Sentinel = "no saved location yet" (off all screens); restored only once set on close.
            this.MainWindowLocation = new Point(-32000, -32000);
        }


        [JsonProperty("ConfigVersion")]
        public int ConfigVersion { get; set; }

        [JsonIgnore]
        public Dictionary<string, bool> CycleGroupExclusions { get; set; }

        [JsonProperty("CycleGroup1ForwardHotkeys")]
        public List<string> CycleGroup1ForwardHotkeys { get; set; }

        [JsonProperty("CycleGroup1BackwardHotkeys")]
        public List<string> CycleGroup1BackwardHotkeys { get; set; }

        [JsonProperty("CycleGroup1ClientsOrder")]
        public Dictionary<string, int> CycleGroup1ClientsOrder { get; set; }

        [JsonProperty("CycleGroup2ForwardHotkeys")]
        public List<string> CycleGroup2ForwardHotkeys { get; set; }

        [JsonProperty("CycleGroup2BackwardHotkeys")]
        public List<string> CycleGroup2BackwardHotkeys { get; set; }

        [JsonProperty("CycleGroup2ClientsOrder")]
        public Dictionary<string, int> CycleGroup2ClientsOrder { get; set; }

        [JsonProperty("CycleGroup3ForwardHotkeys")]
        public List<string> CycleGroup3ForwardHotkeys { get; set; }

        [JsonProperty("CycleGroup3BackwardHotkeys")]
        public List<string> CycleGroup3BackwardHotkeys { get; set; }

        [JsonProperty("CycleGroup3ClientsOrder")]
        public Dictionary<string, int> CycleGroup3ClientsOrder { get; set; }

        [JsonProperty("CycleGroup4ForwardHotkeys")]
        public List<string> CycleGroup4ForwardHotkeys { get; set; }

        [JsonProperty("CycleGroup4BackwardHotkeys")]
        public List<string> CycleGroup4BackwardHotkeys { get; set; }

        [JsonProperty("CycleGroup4ClientsOrder")]
        public Dictionary<string, int> CycleGroup4ClientsOrder { get; set; }

        [JsonProperty("CycleGroup5ForwardHotkeys")]
        public List<string> CycleGroup5ForwardHotkeys { get; set; }

        [JsonProperty("CycleGroup5BackwardHotkeys")]
        public List<string> CycleGroup5BackwardHotkeys { get; set; }

        [JsonProperty("CycleGroup5ClientsOrder")]
        public Dictionary<string, int> CycleGroup5ClientsOrder { get; set; }

        // New dynamic, named cycle groups. The legacy CycleGroup1..5 properties above are kept
        // for one-time migration of old configs only and are no longer serialized.
        [JsonProperty("CycleGroups")]
        public List<CycleGroupConfiguration> CycleGroups { get; set; }

        public bool ShouldSerializeCycleGroup1ForwardHotkeys() => false;
        public bool ShouldSerializeCycleGroup1BackwardHotkeys() => false;
        public bool ShouldSerializeCycleGroup1ClientsOrder() => false;
        public bool ShouldSerializeCycleGroup2ForwardHotkeys() => false;
        public bool ShouldSerializeCycleGroup2BackwardHotkeys() => false;
        public bool ShouldSerializeCycleGroup2ClientsOrder() => false;
        public bool ShouldSerializeCycleGroup3ForwardHotkeys() => false;
        public bool ShouldSerializeCycleGroup3BackwardHotkeys() => false;
        public bool ShouldSerializeCycleGroup3ClientsOrder() => false;
        public bool ShouldSerializeCycleGroup4ForwardHotkeys() => false;
        public bool ShouldSerializeCycleGroup4BackwardHotkeys() => false;
        public bool ShouldSerializeCycleGroup4ClientsOrder() => false;
        public bool ShouldSerializeCycleGroup5ForwardHotkeys() => false;
        public bool ShouldSerializeCycleGroup5BackwardHotkeys() => false;
        public bool ShouldSerializeCycleGroup5ClientsOrder() => false;

        [JsonProperty("PerClientPreventPreviewColor")]
        public Dictionary<string, Color> PerClientPreventPreviewColor { get; set; }

        [JsonProperty("PerClientActiveClientHighlightColor")]
        public Dictionary<string, Color> PerClientActiveClientHighlightColor { get; set; }

        [JsonProperty("PerClientPreventPreviews")]
        public Dictionary<string, bool> PerClientPreventPreviews { get; set; }

        [JsonProperty("PerClientThumbnailSize")]
        public Dictionary<string, Size> PerClientThumbnailSize { get; set; }

        [JsonProperty("PerClientZoomAnchor")]
        public Dictionary<string, ZoomAnchor> PerClientZoomAnchor { get; set; }
        public bool MinimizeToTray { get; set; }
        [JsonProperty("DarkMode")]
        public bool DarkMode { get; set; }
        [JsonProperty("MainWindowLocation")]
        public Point MainWindowLocation { get; set; }
        [JsonProperty("MainWindowMinimized")]
        public bool MainWindowMinimized { get; set; }
        public int ThumbnailRefreshPeriod { get; set; }
        public int ThumbnailResizeTimeoutPeriod { get; set; }

        [JsonProperty("WineCompatibilityMode")]
        public bool EnableWineCompatibilityMode { get; set; }

        [JsonProperty("ThumbnailsOpacity")]
        public double ThumbnailOpacity { get; set; }

        public bool EnableClientLayoutTracking
        {
            get => this._enableClientLayoutTracking;
            set
            {
                if (!value)
                {
                    this.ClientLayout.Clear();
                }

                this._enableClientLayoutTracking = value;
            }
        }

        public bool HideActiveClientThumbnail { get; set; }
        public bool HideLoginClientThumbnail { get; set; }
        public bool MinimizeInactiveClients { get; set; }
        public bool HideCaptionOnClients { get; set; }
        public AnimationStyle WindowsAnimationStyle { get; set; }
        public bool ShowThumbnailsAlwaysOnTop { get; set; }

        public bool EnablePerClientThumbnailLayouts
        {
            get => this._enablePerClientThumbnailLayouts;
            set
            {
                if (!value)
                {
                    this.PerClientLayout.Clear();
                }

                this._enablePerClientThumbnailLayouts = value;
            }
        }

        public bool PreventPreviews { get; set; }
        public bool HideThumbnailsOnLostFocus { get; set; }
        public int HideThumbnailsDelay { get; set; }

        public Size ThumbnailSize { get; set; }
        public Size ThumbnailMaximumSize { get; set; }
        public Size ThumbnailMinimumSize { get; set; }

        public bool EnableThumbnailSnap { get; set; }

        [JsonProperty("EnableThumbnailZoom")]
        public bool ThumbnailZoomEnabled { get; set; }
        public int ThumbnailZoomFactor { get; set; }
        public ZoomAnchor ThumbnailZoomAnchor { get; set; }
        public ZoomAnchor OverlayLabelAnchor { get; set; }
        public ZoomAnchor CycleGroupIndicatorAnchor { get; set; }

        public bool ShowThumbnailOverlays { get; set; }
        public bool ShowThumbnailFrames { get; set; }
        public bool LockThumbnailLocation { get; set; }
        public bool ThumbnailSnapToGrid { get; set; }
        public int ThumbnailSnapToGridSizeX { get; set; }
        public int ThumbnailSnapToGridSizeY { get; set; }

        public bool EnableActiveClientHighlight { get; set; }

        public Color ActiveClientHighlightColor { get; set; }
        public Color PreventPreviewColor { get; set; }
        public Color OverlayLabelColor { get; set; }

        [JsonProperty]
        public Font OverlayLabelFont { get; set; }
        public string IconName { get; set; }

        public int ActiveClientHighlightThickness { get; set; }

        [JsonProperty("LoginThumbnailLocation")]
        public Point LoginThumbnailLocation { get; set; }

        [JsonProperty]
        private Dictionary<string, Dictionary<string, Point>> PerClientLayout { get; set; }
        [JsonProperty]
        private Dictionary<string, Point> FlatLayout { get; set; }
        [JsonProperty]
        private Dictionary<string, ClientLayout> ClientLayout { get; set; }
        [JsonProperty]
        private Dictionary<string, string> ClientHotkey { get; set; }
        [JsonProperty]
        public List<string> MinimizeAllClientsHotkeys { get; set; }
        [JsonProperty]
        private Dictionary<string, bool> DisableThumbnail { get; set; }
        [JsonProperty]
        private List<string> PriorityClients { get; set; }
        [JsonProperty]
        private List<string> ExecutablesToPreview { get; set; }

        public Point GetThumbnailLocation(string currentClient, string activeClient, Point defaultLocation)
        {
            Point location;

            // What this code does:
            // If Per-Client layouts are enabled
            //    and client name is known
            //    and there is a separate thumbnails layout for this client
            //    and this layout contains an entry for the current client
            // then return that entry
            // otherwise try to get client layout from the flat all-clients layout
            // If there is no layout too then use the default one
            if (this.EnablePerClientThumbnailLayouts && !string.IsNullOrEmpty(activeClient))
            {
                Dictionary<string, Point> layoutSource;
                if (this.PerClientLayout.TryGetValue(activeClient, out layoutSource) && layoutSource.TryGetValue(currentClient, out location))
                {
                    return location;
                }
            }

            return this.FlatLayout.TryGetValue(currentClient, out location) ? location : defaultLocation;
        }

        public Size GetThumbnailSize(string currentClient, string activeClient, Size defaultSize)
        {
            Size sizeOfThumbnail;
            return this.PerClientThumbnailSize.TryGetValue(currentClient, out sizeOfThumbnail) ? sizeOfThumbnail : defaultSize;
        }
        public ZoomAnchor GetZoomAnchor(string currentClient, ZoomAnchor defaultZoomAnchor)
        {
            ZoomAnchor zoomAnchor;
            return this.PerClientZoomAnchor.TryGetValue(currentClient, out zoomAnchor) ? zoomAnchor : defaultZoomAnchor;
        }

        public void SetThumbnailLocation(string currentClient, string activeClient, Point location)
        {
            Dictionary<string, Point> layoutSource;

            if (this.EnablePerClientThumbnailLayouts)
            {
                if (string.IsNullOrEmpty(activeClient))
                {
                    return;
                }

                if (!this.PerClientLayout.TryGetValue(activeClient, out layoutSource))
                {
                    layoutSource = new Dictionary<string, Point>();
                    this.PerClientLayout[activeClient] = layoutSource;
                }
            }
            else
            {
                layoutSource = this.FlatLayout;
            }

            layoutSource[currentClient] = location;
        }

        public ClientLayout GetClientLayout(string currentClient)
        {
            ClientLayout layout;
            this.ClientLayout.TryGetValue(currentClient, out layout);

            return layout;
        }

        public void SetClientLayout(string currentClient, ClientLayout layout)
        {
            this.ClientLayout[currentClient] = layout;
        }

        // Single shared converter used by all hotkey (de)serialization below.
        private static readonly KeysConverter _hotkeyConverter = new KeysConverter();

        public Keys GetClientHotkey(string currentClient)
        {
            return this.ClientHotkey.TryGetValue(currentClient, out string hotkey)
                ? ParseHotkey(hotkey)
                : Keys.None;
        }

        public void SetClientHotkey(string currentClient, Keys hotkey)
        {
            this.ClientHotkey[currentClient] = _hotkeyConverter.ConvertToInvariantString(hotkey);
        }

        public Keys StringToKey(string hotkey)
        {
            return ParseHotkey(hotkey);
        }

        public string KeyToString(Keys hotkey)
        {
            return _hotkeyConverter.ConvertToInvariantString(hotkey);
        }

        // Exception-safe + case-tolerant hotkey parsing (issue #15).
        // KeysConverter.ConvertFromInvariantString THROWS on an unknown token (e.g. lowercase
        // "f1" or "a") rather than returning null, so the old "!= null" guard was dead code and
        // the exception bubbled up during startup hotkey registration, crashing the app.
        // This never throws: an unparseable entry is recovered if possible, otherwise ignored.
        private static Keys ParseHotkey(string hotkey)
        {
            if (string.IsNullOrWhiteSpace(hotkey))
            {
                return Keys.None;
            }

            // 1) Honor the exact string first (preserves every existing valid config verbatim).
            if (TryConvert(hotkey, out Keys keys))
            {
                return keys;
            }

            // 2) Recover bad casing by canonicalizing each token against the Keys enum ("f1" -> "F1").
            string normalized = NormalizeHotkey(hotkey);
            if ((normalized != null) && TryConvert(normalized, out keys))
            {
                return keys;
            }

            // 3) Give up gracefully instead of throwing.
            return Keys.None;
        }

        private static bool TryConvert(string hotkey, out Keys keys)
        {
            try
            {
                object raw = _hotkeyConverter.ConvertFromInvariantString(hotkey);
                keys = raw != null ? (Keys)raw : Keys.None;
                return raw != null;
            }
            catch (Exception)
            {
                keys = Keys.None;
                return false;
            }
        }

        private static string NormalizeHotkey(string hotkey)
        {
            string[] parts = hotkey.Split('+');
            for (int i = 0; i < parts.Length; i++)
            {
                string token = parts[i].Trim();
                if (token.Length == 0)
                {
                    return null;
                }

                // Canonicalize via the enum where possible; leave modifier words (Ctrl/Alt/Shift)
                // untouched for KeysConverter to interpret.
                parts[i] = Enum.TryParse(token, ignoreCase: true, out Keys k) && Enum.IsDefined(typeof(Keys), k)
                    ? k.ToString()
                    : token;
            }

            return string.Join("+", parts);
        }

        public bool IsPriorityClient(string currentClient)
        {
            return this.PriorityClients.Contains(currentClient);
        }
        public bool IsExecutableToPreview(string processName)
        {
            return this.ExecutablesToPreview.Any(s => s.Equals(processName, StringComparison.OrdinalIgnoreCase));
        }

        public bool IsThumbnailDisabled(string currentClient)
        {
            return this.DisableThumbnail.TryGetValue(currentClient, out bool isDisabled) && isDisabled;
        }

        public void ToggleThumbnail(string currentClient, bool isDisabled)
        {
            this.DisableThumbnail[currentClient] = isDisabled;
        }

        /// <summary>
        /// Applies restrictions to different parameters of the config
        /// </summary>
        public void ApplyRestrictions()
        {
            this.MigrateCycleGroupsIfNeeded();
#if LINUX
			this.ThumbnailRefreshPeriod = ThumbnailConfiguration.ApplyRestrictions(this.ThumbnailRefreshPeriod, 10, 1000);
#else
            this.ThumbnailRefreshPeriod = ThumbnailConfiguration.ApplyRestrictions(this.ThumbnailRefreshPeriod, 300, 1000);
#endif
            this.ThumbnailResizeTimeoutPeriod = ThumbnailConfiguration.ApplyRestrictions(this.ThumbnailResizeTimeoutPeriod, 200, 5000);
            this.ThumbnailSize = new Size(ThumbnailConfiguration.ApplyRestrictions(this.ThumbnailSize.Width, this.ThumbnailMinimumSize.Width, this.ThumbnailMaximumSize.Width),
                ThumbnailConfiguration.ApplyRestrictions(this.ThumbnailSize.Height, this.ThumbnailMinimumSize.Height, this.ThumbnailMaximumSize.Height));
            this.ThumbnailOpacity = ThumbnailConfiguration.ApplyRestrictions((int)(this.ThumbnailOpacity * 100.00), 20, 100) / 100.00;
            this.ThumbnailZoomFactor = ThumbnailConfiguration.ApplyRestrictions(this.ThumbnailZoomFactor, 2, 10);
            this.ActiveClientHighlightThickness = ThumbnailConfiguration.ApplyRestrictions(this.ActiveClientHighlightThickness, 1, 6);
        }

        // Converts the legacy fixed CycleGroup1..5 properties into the dynamic CycleGroups list
        // the first time an old config (ConfigVersion < 2) is loaded.
        private void MigrateCycleGroupsIfNeeded()
        {
            if (this.CycleGroups == null)
            {
                this.CycleGroups = new List<CycleGroupConfiguration>();
            }

            if (this.ConfigVersion >= 2)
            {
                return;
            }

            this.CycleGroups = new List<CycleGroupConfiguration>();
            this.AddLegacyCycleGroup("Group 1", this.CycleGroup1ForwardHotkeys, this.CycleGroup1BackwardHotkeys, this.CycleGroup1ClientsOrder);
            this.AddLegacyCycleGroup("Group 2", this.CycleGroup2ForwardHotkeys, this.CycleGroup2BackwardHotkeys, this.CycleGroup2ClientsOrder);
            this.AddLegacyCycleGroup("Group 3", this.CycleGroup3ForwardHotkeys, this.CycleGroup3BackwardHotkeys, this.CycleGroup3ClientsOrder);
            this.AddLegacyCycleGroup("Group 4", this.CycleGroup4ForwardHotkeys, this.CycleGroup4BackwardHotkeys, this.CycleGroup4ClientsOrder);
            this.AddLegacyCycleGroup("Group 5", this.CycleGroup5ForwardHotkeys, this.CycleGroup5BackwardHotkeys, this.CycleGroup5ClientsOrder);

            this.ConfigVersion = 2;
        }

        private void AddLegacyCycleGroup(string name, List<string> forward, List<string> backward, Dictionary<string, int> order)
        {
            this.CycleGroups.Add(new CycleGroupConfiguration
            {
                Name = name,
                ForwardHotkeys = CleanHotkeyList(forward),
                BackwardHotkeys = CleanHotkeyList(backward),
                ClientsOrder = (order != null) ? new Dictionary<string, int>(order) : new Dictionary<string, int>()
            });
        }

        private static List<string> CleanHotkeyList(List<string> source)
        {
            // Drop the empty placeholder entries the legacy defaults used.
            return (source == null)
                ? new List<string>()
                : source.Where(hotkey => !string.IsNullOrWhiteSpace(hotkey)).ToList();
        }

        private static int ApplyRestrictions(int value, int minimum, int maximum)
        {
            if (value <= minimum)
            {
                return minimum;
            }

            if (value >= maximum)
            {
                return maximum;
            }

            return value;
        }
    }
}