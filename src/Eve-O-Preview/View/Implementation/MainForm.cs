using EveOPreview.Configuration;
using EveOPreview.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EveOPreview.View
{
    public partial class MainForm : Form, IMainFormView
    {
        #region Private fields
        private readonly ApplicationContext _context;
        private readonly Dictionary<ViewZoomAnchor, RadioButton> _zoomAnchorMap;
        private readonly Dictionary<ViewZoomAnchor, RadioButton> _overlayLabelMap;
        private readonly Dictionary<ViewZoomAnchor, RadioButton> _cycleGroupIndicatorMap;
        private ViewZoomAnchor _cachedThumbnailZoomAnchor;
        private ViewZoomAnchor _cachedOverlayLabelAnchor;
        private ViewZoomAnchor _cachedCycleGroupIndicatorAnchor;
        private bool _suppressEvents;
        private Size _minimumSize;
        private Size _maximumSize;
        private string _iconName;

        // Hotkeys tab (issue #13): live per-client hotkey rows, keyed by client title.
        private FlowLayoutPanel _hotkeyRowsPanel;
        private readonly Dictionary<string, Panel> _clientHotkeyRows = new Dictionary<string, Panel>();
        // Cycle-group master/detail controls (issue: unlimited named cycle groups).
        private ListBox _cycleGroupsList;
        private HotkeyInputControl _cycleForwardControl;
        private HotkeyInputControl _cycleBackwardControl;
        private CheckedListBox _cycleMembersList;
        private HotkeyInputControl _minimizeAllControl;
        private bool _suppressCycleEvents;
        private int _hotkeyTopOffset;
        // Profiles tab + tray submenu.
        private ListBox _profilesList;
        private Label _activeProfileLabel;
        private ToolStripMenuItem _profilesTrayMenuItem;
        // Base (smallest) client size that fits the standard tabs; the window auto-grows from here.
        private Size _baseClientSize;
        // Upper bound on auto-grow so a large client list can't push the window off-screen.
        private const int MAX_AUTO_GROW_WIDTH = 700;
        private const int MAX_AUTO_GROW_HEIGHT = 750;
        // Theme (light/dark) controls and owner-drawn tab colors.
        private CheckBox _darkModeCheckBox;
        private ToolStripMenuItem _darkModeTrayItem;
        private bool _suppressThemeEvents;
        private Color _tabSelectedBack = SystemColors.Control;
        private Color _tabUnselectedBack = SystemColors.ControlDark;
        private Color _tabTextColor = SystemColors.ActiveCaptionText;
        #endregion

        public MainForm(ApplicationContext context)
        {
            this._context = context;
            this._zoomAnchorMap = new Dictionary<ViewZoomAnchor, RadioButton>();
            this._overlayLabelMap = new Dictionary<ViewZoomAnchor, RadioButton>();
            this._cycleGroupIndicatorMap = new Dictionary<ViewZoomAnchor, RadioButton>();
            this._cachedThumbnailZoomAnchor = ViewZoomAnchor.NW;
            this._suppressEvents = false;
            this._minimumSize = new Size(20, 20);
            this._maximumSize = new Size(20, 20);

            InitializeComponent();

            this.ThumbnailsList.DisplayMember = "Title";

            this.InitZoomAnchorMap();
            this.InitOverlayLabelMap();
            this.InitCycleGroupIndicatorMap();
            this.InitFormSize();

            this.AnimationStyleCombo.DataSource = Enum.GetValues(typeof(AnimationStyle));

            this.InitHotkeysTab();
            this.InitProfilesTab();
            this.InitThemeControls();
            this.InitTabAutoSizing();
        }

        public bool MinimizeToTray
        {
            get => this.MinimizeToTrayCheckBox.Checked;
            set => this.MinimizeToTrayCheckBox.Checked = value;
        }

        public string IconName
        {
            get => this._iconName;
            set
            {


                this._iconName = value;

                // Set Icon 
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
                if (this._iconName == null || ((resources.GetObject(this._iconName))) == null)
                {
                    this._iconName = "IconOriginal";
                }

                // pull icon from resources
                try
                {
                    var iconBytes = (byte[])resources.GetObject(this._iconName);
                    using (MemoryStream ms = new MemoryStream(iconBytes))
                    {
                        this.Icon = new Icon(ms);
                        this.NotifyIcon.Icon = this.Icon;
                    }
                }
                catch (Exception)
                {
                    // Log ?
                }

                if (value != "")
                {
                    this.ApplicationSettingsChanged?.Invoke();
                }
            }
        }

        public double ThumbnailOpacity
        {
            get => Math.Min(this.ThumbnailOpacityTrackBar.Value / 100.00, 1.00);
            set
            {
                int barValue = (int)(100.0 * value);
                if (barValue > 100)
                {
                    barValue = 100;
                }
                else if (barValue < 10)
                {
                    barValue = 10;
                }

                this.ThumbnailOpacityTrackBar.Value = barValue;
            }
        }

        public bool EnableClientLayoutTracking
        {
            get => this.EnableClientLayoutTrackingCheckBox.Checked;
            set => this.EnableClientLayoutTrackingCheckBox.Checked = value;
        }

        public bool HideActiveClientThumbnail
        {
            get => this.HideActiveClientThumbnailCheckBox.Checked;
            set => this.HideActiveClientThumbnailCheckBox.Checked = value;
        }

        public bool MinimizeInactiveClients
        {
            get => this.MinimizeInactiveClientsCheckBox.Checked;
            set => this.MinimizeInactiveClientsCheckBox.Checked = value;
        }
        public bool HideCaptionOnClients
        {
            get => this.HideCaptionOnClientsCheckBox.Checked;
            set => this.HideCaptionOnClientsCheckBox.Checked = value;
        }
        public ViewAnimationStyle WindowsAnimationStyle
        {
            get => (ViewAnimationStyle)this.AnimationStyleCombo.SelectedItem;
            set => this.AnimationStyleCombo.SelectedIndex = (int)value;
        }

        public bool ShowThumbnailsAlwaysOnTop
        {
            get => this.ShowThumbnailsAlwaysOnTopCheckBox.Checked;
            set => this.ShowThumbnailsAlwaysOnTopCheckBox.Checked = value;
        }
        public bool PreventPreviews
        {
            get => this.PreventPreviewsCheckBox.Checked;
            set => this.PreventPreviewsCheckBox.Checked = value;
        }

        public bool HideThumbnailsOnLostFocus
        {
            get => this.HideThumbnailsOnLostFocusCheckBox.Checked;
            set => this.HideThumbnailsOnLostFocusCheckBox.Checked = value;
        }

        public bool EnablePerClientThumbnailLayouts
        {
            get => this.EnablePerClientThumbnailsLayoutsCheckBox.Checked;
            set => this.EnablePerClientThumbnailsLayoutsCheckBox.Checked = value;
        }

        public Size ThumbnailSize
        {
            get => new Size((int)this.ThumbnailsWidthNumericEdit.Value, (int)this.ThumbnailsHeightNumericEdit.Value);
            set
            {
                this.ThumbnailsWidthNumericEdit.Value = value.Width;
                this.ThumbnailsHeightNumericEdit.Value = value.Height;
            }
        }

        public bool EnableThumbnailZoom
        {
            get => this.EnableThumbnailZoomCheckBox.Checked;
            set
            {
                this.EnableThumbnailZoomCheckBox.Checked = value;
                this.RefreshZoomSettings();
            }
        }

        public int ThumbnailZoomFactor
        {
            get => (int)this.ThumbnailZoomFactorNumericEdit.Value;
            set => this.ThumbnailZoomFactorNumericEdit.Value = value;
        }

        public ViewZoomAnchor ThumbnailZoomAnchor
        {
            get
            {
                if (this._zoomAnchorMap[this._cachedThumbnailZoomAnchor].Checked)
                {
                    return this._cachedThumbnailZoomAnchor;
                }

                foreach (KeyValuePair<ViewZoomAnchor, RadioButton> valuePair in this._zoomAnchorMap)
                {
                    if (!valuePair.Value.Checked)
                    {
                        continue;
                    }

                    this._cachedThumbnailZoomAnchor = valuePair.Key;
                    return this._cachedThumbnailZoomAnchor;
                }

                // Default value
                return ViewZoomAnchor.NW;
            }
            set
            {
                this._cachedThumbnailZoomAnchor = value;
                this._zoomAnchorMap[this._cachedThumbnailZoomAnchor].Checked = true;
            }
        }

        public ViewZoomAnchor OverlayLabelAnchor
        {
            get
            {
                if (this._overlayLabelMap[this._cachedOverlayLabelAnchor].Checked)
                {
                    return this._cachedOverlayLabelAnchor;
                }

                foreach (KeyValuePair<ViewZoomAnchor, RadioButton> valuePair in this._overlayLabelMap)
                {
                    if (!valuePair.Value.Checked)
                    {
                        continue;
                    }

                    this._cachedOverlayLabelAnchor = valuePair.Key;
                    return this._cachedOverlayLabelAnchor;
                }

                // Default Value
                return ViewZoomAnchor.NW;
            }
            set
            {
                this._cachedOverlayLabelAnchor = value;
                this._overlayLabelMap[this._cachedOverlayLabelAnchor].Checked = true;
            }
        }

        public ViewZoomAnchor CycleGroupIndicatorAnchor
        {
            get
            {
                if (this._cycleGroupIndicatorMap[this._cachedCycleGroupIndicatorAnchor].Checked)
                {
                    return this._cachedCycleGroupIndicatorAnchor;
                }

                foreach (KeyValuePair<ViewZoomAnchor, RadioButton> valuePair in this._cycleGroupIndicatorMap)
                {
                    if (!valuePair.Value.Checked)
                    {
                        continue;
                    }

                    this._cachedCycleGroupIndicatorAnchor = valuePair.Key;
                    return this._cachedCycleGroupIndicatorAnchor;
                }

                // Default Value
                return ViewZoomAnchor.NW;
            }
            set
            {
                this._cachedCycleGroupIndicatorAnchor = value;
                this._cycleGroupIndicatorMap[this._cachedCycleGroupIndicatorAnchor].Checked = true;
            }
        }

        public bool ShowThumbnailOverlays
        {
            get => this.ShowThumbnailOverlaysCheckBox.Checked;
            set => this.ShowThumbnailOverlaysCheckBox.Checked = value;
        }

        public bool ShowThumbnailFrames
        {
            get => this.ShowThumbnailFramesCheckBox.Checked;
            set => this.ShowThumbnailFramesCheckBox.Checked = value;
        }
        public bool LockThumbnailLocation
        {
            get => this.LockThumbnailLocationCheckbox.Checked;
            set => this.LockThumbnailLocationCheckbox.Checked = value;
        }
        public bool ThumbnailSnapToGrid
        {
            get => this.ThumbnailSnapToGridCheckBox.Checked;
            set => this.ThumbnailSnapToGridCheckBox.Checked = value;
        }
        public int ThumbnailSnapToGridSizeX
        {
            get => (int)ThumbnailSnapToGridSizeXNumericEdit.Value;
            set => ThumbnailSnapToGridSizeXNumericEdit.Value = value;
        }
        public int ThumbnailSnapToGridSizeY
        {
            get => (int)ThumbnailSnapToGridSizeYNumericEdit.Value;
            set => ThumbnailSnapToGridSizeYNumericEdit.Value = value;
        }

        public bool EnableActiveClientHighlight
        {
            get => this.EnableActiveClientHighlightCheckBox.Checked;
            set => this.EnableActiveClientHighlightCheckBox.Checked = value;
        }

        public Color ActiveClientHighlightColor
        {
            get => this._activeClientHighlightColor;
            set
            {
                this._activeClientHighlightColor = value;
                this.ActiveClientHighlightColorButton.BackColor = value;
            }
        }
        private Color _activeClientHighlightColor;

        public Color PreventPreviewColor
        {
            get => this._preventPreviewColor;
            set
            {
                this._preventPreviewColor = value;
                this.PreventPreviewColorButton.BackColor = value;
            }
        }
        private Color _preventPreviewColor;

        public Color OverlayLabelColor
        {
            get => this._OverlayLabelColor;
            set
            {
                this._OverlayLabelColor = value;
                this.OverlayLabelColorButton.BackColor = value;
            }
        }
        private Color _OverlayLabelColor;

        public Font OverlayLabelFont
        {
            get => (Font)this._OverlayLabelFont;
            set
            {
                this._OverlayLabelFont = value;
                this.LabelOverlayLabelFont.Font = value;
            }
        }
        private Font _OverlayLabelFont;

        public new void Show()
        {
            // Registers the current instance as the application's Main Form
            this._context.MainForm = this;

            this._suppressEvents = true;
            this.FormActivated?.Invoke();
            this._suppressEvents = false;

            Application.Run(this._context);
        }

        public void SetThumbnailSizeLimitations(Size minimumSize, Size maximumSize)
        {
            this._minimumSize = minimumSize;
            this._maximumSize = maximumSize;
        }

        public void Minimize()
        {
            this.WindowState = FormWindowState.Minimized;
        }

        public void SetVersionInfo(string version)
        {
            this.VersionLabel.Text = version;
        }

        public void SetDocumentationUrl(string url)
        {
            this.DocumentationLink.Text = url;
        }

        public void AddThumbnails(IList<IThumbnailDescription> thumbnails)
        {
            this.ThumbnailsList.BeginUpdate();

            foreach (IThumbnailDescription view in thumbnails)
            {
                this.ThumbnailsList.SetItemChecked(this.ThumbnailsList.Items.Add(view), view.IsDisabled);
            }

            this.ThumbnailsList.EndUpdate();

            // Keep the Hotkeys tab (issue #13) in sync with the live client list.
            foreach (IThumbnailDescription view in thumbnails)
            {
                if (!this._clientHotkeyRows.ContainsKey(view.Title))
                {
                    Panel row = this.CreateHotkeyRow(view.Title, view.ClientHotkey);
                    this._clientHotkeyRows[view.Title] = row;
                    this._hotkeyRowsPanel?.Controls.Add(row);
                }
            }
        }

        public void RemoveThumbnails(IList<IThumbnailDescription> thumbnails)
        {
            this.ThumbnailsList.BeginUpdate();

            foreach (IThumbnailDescription view in thumbnails)
            {
                this.ThumbnailsList.Items.Remove(view);
            }

            this.ThumbnailsList.EndUpdate();

            // Remove the matching Hotkeys-tab rows (issue #13).
            foreach (IThumbnailDescription view in thumbnails)
            {
                if (this._clientHotkeyRows.TryGetValue(view.Title, out Panel row))
                {
                    this._hotkeyRowsPanel?.Controls.Remove(row);
                    row.Dispose();
                    this._clientHotkeyRows.Remove(view.Title);
                }
            }
        }

        // Builds the "Hotkeys" tab in code so the large generated designer file is left untouched.
        // The tab control is located by its Name because the designer declares it as a local.
        private void InitHotkeysTab()
        {
            TabControl tabControl = this.Controls.Find("ContentTabControl", true).FirstOrDefault() as TabControl;
            if (tabControl == null)
            {
                return;
            }

            TabPage hotkeysTab = new TabPage
            {
                Name = "HotkeysTabPage",
                Text = "Hotkeys",
                UseVisualStyleBackColor = true,
                Padding = new Padding(6)
            };

            // Per-client switch hotkeys: scrollable area that fills the space below the cycle UI.
            // Rows are appended/removed by AddThumbnails / RemoveThumbnails.
            this._hotkeyRowsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true
            };
            this._hotkeyRowsPanel.Controls.Add(this.CreateSectionLabel("Per-Client Switch Hotkeys"));

            Panel minimizePanel = this.BuildMinimizeAllRow();
            Panel cyclePanel = this.BuildCycleGroupsPanel();
            this._hotkeyTopOffset = cyclePanel.Height + minimizePanel.Height;

            // Add Fill first, then the docked-Top panels (last added sits on top).
            hotkeysTab.Controls.Add(this._hotkeyRowsPanel);
            hotkeysTab.Controls.Add(minimizePanel);
            hotkeysTab.Controls.Add(cyclePanel);

            tabControl.TabPages.Add(hotkeysTab);
        }

        private Label CreateSectionLabel(string text)
        {
            return new Label
            {
                Text = text,
                AutoSize = false,
                Width = 360,
                Height = 22,
                Padding = new Padding(3, 4, 3, 0),
                Font = new Font(this.Font, FontStyle.Bold)
            };
        }

        // Master/detail editor for unlimited named cycle groups.
        private Panel BuildCycleGroupsPanel()
        {
            Panel panel = new Panel { Dock = DockStyle.Top, Height = 212 };

            Label header = new Label
            {
                Text = "Cycle Groups",
                Font = new Font(this.Font, FontStyle.Bold),
                Location = new Point(3, 3),
                AutoSize = true
            };

            this._cycleGroupsList = new ListBox
            {
                Location = new Point(6, 26),
                Size = new Size(140, 118),
                IntegralHeight = false
            };
            this._cycleGroupsList.SelectedIndexChanged += (sender, e) =>
            {
                if (!this._suppressCycleEvents)
                {
                    this.CycleGroupSelected?.Invoke(this._cycleGroupsList.SelectedIndex);
                }
            };

            Button addButton = new Button { Text = "Add", Location = new Point(6, 148), Size = new Size(44, 24) };
            addButton.Click += (sender, e) =>
            {
                string name = this.ShowTextInputDialog("Cycle group name:", "Add Cycle Group");
                if (!string.IsNullOrWhiteSpace(name))
                {
                    this.CycleGroupAddRequested?.Invoke(name);
                }
            };

            Button removeButton = new Button { Text = "Remove", Location = new Point(52, 148), Size = new Size(54, 24) };
            removeButton.Click += (sender, e) =>
            {
                if (this._cycleGroupsList.SelectedIndex >= 0)
                {
                    this.CycleGroupRemoveRequested?.Invoke(this._cycleGroupsList.SelectedIndex);
                }
            };

            Button renameButton = new Button { Text = "Rename", Location = new Point(108, 148), Size = new Size(58, 24) };
            renameButton.Click += (sender, e) =>
            {
                if (this._cycleGroupsList.SelectedIndex >= 0)
                {
                    string name = this.ShowTextInputDialog("New name:", "Rename Cycle Group");
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        this.CycleGroupRenameRequested?.Invoke(this._cycleGroupsList.SelectedIndex, name);
                    }
                }
            };

            Label forwardLabel = new Label { Text = "Forward:", Location = new Point(176, 28), AutoSize = true };
            this._cycleForwardControl = new HotkeyInputControl { Location = new Point(238, 25), Size = new Size(200, 23) };
            this._cycleForwardControl.HotkeyChanged += (sender, e) =>
            {
                if (!this._suppressCycleEvents && (this._cycleGroupsList.SelectedIndex >= 0))
                {
                    this.CycleGroupHotkeyChanged?.Invoke(this._cycleGroupsList.SelectedIndex, true, this._cycleForwardControl.Hotkey);
                }
            };

            Label backLabel = new Label { Text = "Back:", Location = new Point(176, 56), AutoSize = true };
            this._cycleBackwardControl = new HotkeyInputControl { Location = new Point(238, 53), Size = new Size(200, 23) };
            this._cycleBackwardControl.HotkeyChanged += (sender, e) =>
            {
                if (!this._suppressCycleEvents && (this._cycleGroupsList.SelectedIndex >= 0))
                {
                    this.CycleGroupHotkeyChanged?.Invoke(this._cycleGroupsList.SelectedIndex, false, this._cycleBackwardControl.Hotkey);
                }
            };

            Label membersLabel = new Label { Text = "Members:", Location = new Point(176, 84), AutoSize = true };
            this._cycleMembersList = new CheckedListBox
            {
                Location = new Point(176, 102),
                Size = new Size(262, 100),
                CheckOnClick = true,
                IntegralHeight = false
            };
            this._cycleMembersList.ItemCheck += (sender, e) =>
            {
                if (this._suppressCycleEvents || (this._cycleGroupsList.SelectedIndex < 0))
                {
                    return;
                }
                string title = this._cycleMembersList.Items[e.Index].ToString();
                this.CycleGroupMembershipChanged?.Invoke(this._cycleGroupsList.SelectedIndex, title, e.NewValue == CheckState.Checked);
            };

            panel.Controls.Add(header);
            panel.Controls.Add(this._cycleGroupsList);
            panel.Controls.Add(addButton);
            panel.Controls.Add(removeButton);
            panel.Controls.Add(renameButton);
            panel.Controls.Add(forwardLabel);
            panel.Controls.Add(this._cycleForwardControl);
            panel.Controls.Add(backLabel);
            panel.Controls.Add(this._cycleBackwardControl);
            panel.Controls.Add(membersLabel);
            panel.Controls.Add(this._cycleMembersList);

            return panel;
        }

        private Panel BuildMinimizeAllRow()
        {
            Panel panel = new Panel { Dock = DockStyle.Top, Height = 34 };

            Label label = new Label { Text = "Minimize all clients:", Location = new Point(6, 9), AutoSize = true };
            this._minimizeAllControl = new HotkeyInputControl { Location = new Point(176, 6), Size = new Size(262, 23) };
            this._minimizeAllControl.HotkeyChanged += (sender, e) =>
            {
                if (!this._suppressCycleEvents)
                {
                    this.MinimizeAllHotkeyChanged?.Invoke(this._minimizeAllControl.Hotkey);
                }
            };

            panel.Controls.Add(label);
            panel.Controls.Add(this._minimizeAllControl);
            return panel;
        }

        public void SetCycleGroups(IReadOnlyList<string> groupNames, int selectedIndex)
        {
            if (this._cycleGroupsList == null)
            {
                return;
            }

            this._suppressCycleEvents = true;
            this._cycleGroupsList.BeginUpdate();
            this._cycleGroupsList.Items.Clear();
            foreach (string name in groupNames)
            {
                this._cycleGroupsList.Items.Add(name);
            }
            if ((selectedIndex >= 0) && (selectedIndex < this._cycleGroupsList.Items.Count))
            {
                this._cycleGroupsList.SelectedIndex = selectedIndex;
            }
            this._cycleGroupsList.EndUpdate();
            this._suppressCycleEvents = false;
        }

        public void SetCycleGroupDetail(Keys forward, Keys backward, IReadOnlyList<string> memberClients)
        {
            if (this._cycleForwardControl == null)
            {
                return;
            }

            this._suppressCycleEvents = true;

            this._cycleForwardControl.SetHotkeySilently(forward);
            this._cycleBackwardControl.SetHotkeySilently(backward);

            // Show every known client title (live rows + any members not currently running) and
            // tick the ones that belong to the selected group.
            HashSet<string> members = new HashSet<string>(memberClients ?? new List<string>(), StringComparer.OrdinalIgnoreCase);
            SortedSet<string> titles = new SortedSet<string>(this._clientHotkeyRows.Keys, StringComparer.OrdinalIgnoreCase);
            foreach (string member in members)
            {
                titles.Add(member);
            }

            this._cycleMembersList.BeginUpdate();
            this._cycleMembersList.Items.Clear();
            foreach (string title in titles)
            {
                this._cycleMembersList.Items.Add(title, members.Contains(title));
            }
            this._cycleMembersList.EndUpdate();

            this._suppressCycleEvents = false;
        }

        public void SetMinimizeAllHotkey(Keys hotkey)
        {
            this._minimizeAllControl?.SetHotkeySilently(hotkey);
        }

        public Action<bool> ThemeChanged { get; set; }

        // Adds the "Dark mode" checkbox to the General tab and a matching tray-menu toggle.
        private void InitThemeControls()
        {
            Panel panel = this.Controls.Find("GeneralSettingsPanel", true).FirstOrDefault() as Panel;
            if (panel != null)
            {
                int y = 8;
                foreach (Control existing in panel.Controls)
                {
                    y = Math.Max(y, existing.Bottom);
                }
                y += 10;

                this._darkModeCheckBox = new CheckBox
                {
                    Text = "Dark mode",
                    AutoSize = true,
                    Location = new Point(9, y)
                };
                this._darkModeCheckBox.CheckedChanged += (sender, e) =>
                {
                    if (!this._suppressThemeEvents)
                    {
                        this.ThemeChanged?.Invoke(this._darkModeCheckBox.Checked);
                    }
                };

                panel.AutoScroll = true;
                panel.Controls.Add(this._darkModeCheckBox);
            }

            if (this.NotifyIcon?.ContextMenuStrip != null)
            {
                this._darkModeTrayItem = new ToolStripMenuItem("Dark Mode");
                this._darkModeTrayItem.Click += (sender, e) =>
                {
                    if (!this._suppressThemeEvents)
                    {
                        this.ThemeChanged?.Invoke(!this._darkModeTrayItem.Checked);
                    }
                };
                this.NotifyIcon.ContextMenuStrip.Items.Insert(0, this._darkModeTrayItem);
            }
        }

        // Recolors the whole window for the chosen theme. WinForms has no built-in theming, so
        // this walks the control tree applying a light/dark palette.
        public void ApplyTheme(bool dark)
        {
            this._suppressThemeEvents = true;

            Color back = dark ? Color.FromArgb(37, 37, 38) : SystemColors.Control;
            Color fore = dark ? Color.Gainsboro : SystemColors.ControlText;
            Color inputBack = dark ? Color.FromArgb(51, 51, 55) : SystemColors.Window;
            Color inputFore = dark ? Color.Gainsboro : SystemColors.WindowText;

            this.SuspendLayout();
            this.BackColor = back;
            this.ForeColor = fore;
            foreach (Control child in this.Controls)
            {
                this.ApplyThemeToControl(child, back, fore, inputBack, inputFore, dark);
            }
            this.ResumeLayout();

            // Owner-drawn tab colors (used by ContentTabControl_DrawItem).
            this._tabSelectedBack = dark ? Color.FromArgb(45, 45, 48) : SystemColors.Control;
            this._tabUnselectedBack = dark ? Color.FromArgb(28, 28, 30) : SystemColors.ControlDark;
            this._tabTextColor = dark ? Color.Gainsboro : SystemColors.ActiveCaptionText;
            TabControl tabControl = this.Controls.Find("ContentTabControl", true).FirstOrDefault() as TabControl;
            tabControl?.Invalidate();

            if (this._darkModeCheckBox != null)
            {
                this._darkModeCheckBox.Checked = dark;
            }
            if (this._darkModeTrayItem != null)
            {
                this._darkModeTrayItem.Checked = dark;
            }

            this.ApplyTitleBarTheme(dark);

            this._suppressThemeEvents = false;
        }

        private void ApplyThemeToControl(Control control, Color back, Color fore, Color inputBack, Color inputFore, bool dark)
        {
            switch (control)
            {
                case TextBox _:
                case NumericUpDown _:
                case ListBox _: // also covers CheckedListBox (a ListBox subclass)
                case ComboBox _:
                    control.BackColor = inputBack;
                    control.ForeColor = inputFore;
                    break;
                case Button button:
                    button.BackColor = dark ? Color.FromArgb(62, 62, 66) : SystemColors.Control;
                    button.ForeColor = fore;
                    button.FlatStyle = dark ? FlatStyle.Flat : FlatStyle.Standard;
                    break;
                case LinkLabel link:
                    link.BackColor = back;
                    link.ForeColor = fore;
                    link.LinkColor = dark ? Color.FromArgb(120, 170, 255) : Color.Blue;
                    break;
                default:
                    control.BackColor = back;
                    control.ForeColor = fore;
                    break;
            }

            foreach (Control child in control.Controls)
            {
                this.ApplyThemeToControl(child, back, fore, inputBack, inputFore, dark);
            }
        }

        // Darkens/lightens the OS title bar on Windows 10+ (harmless no-op elsewhere).
        private void ApplyTitleBarTheme(bool dark)
        {
            try
            {
                int useDark = dark ? 1 : 0;
                // DWMWA_USE_IMMERSIVE_DARK_MODE = 20 on Win10 2004+, 19 on older builds.
                if (DwmSetWindowAttribute(this.Handle, 20, ref useDark, sizeof(int)) != 0)
                {
                    DwmSetWindowAttribute(this.Handle, 19, ref useDark, sizeof(int));
                }
            }
            catch
            {
                // dwmapi is unavailable on Linux/Wine — ignore.
            }
        }

        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attribute, ref int value, int size);

        // --- Window placement persistence ---
        // When minimized/maximized, Location is the minimized position, so use RestoreBounds.
        public Point CurrentWindowLocation =>
            (this.WindowState == FormWindowState.Normal) ? this.Location : this.RestoreBounds.Location;

        public bool IsCurrentlyMinimized => this.WindowState == FormWindowState.Minimized;

        public void RestoreWindowPlacement(Point location, bool minimized)
        {
            if (IsLocationVisible(location))
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = location;
            }

            if (minimized)
            {
                this.WindowState = FormWindowState.Minimized;
            }
        }

        // True if a sliver of the title bar at this location lands on some monitor's work area.
        private static bool IsLocationVisible(Point location)
        {
            Rectangle titleBarSliver = new Rectangle(location, new Size(80, 30));
            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen.WorkingArea.IntersectsWith(titleBarSliver))
                {
                    return true;
                }
            }
            return false;
        }

        // --- Profiles tab + tray submenu ---
        private void InitProfilesTab()
        {
            TabControl tabControl = this.Controls.Find("ContentTabControl", true).FirstOrDefault() as TabControl;
            if (tabControl == null)
            {
                return;
            }

            this._activeProfileLabel = new Label
            {
                Dock = DockStyle.Top,
                Height = 24,
                Padding = new Padding(3),
                Text = "Active profile:"
            };

            this._profilesList = new ListBox
            {
                Dock = DockStyle.Fill,
                IntegralHeight = false
            };
            this._profilesList.DoubleClick += (sender, e) => this.RaiseActivateSelected();

            FlowLayoutPanel buttons = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true
            };

            Button activateButton = new Button { Text = "Activate", Width = 80, Margin = new Padding(3) };
            activateButton.Click += (sender, e) => this.RaiseActivateSelected();

            Button newButton = new Button { Text = "New", Width = 70, Margin = new Padding(3) };
            newButton.Click += (sender, e) => this.ProfileNewRequested?.Invoke();

            Button saveButton = new Button { Text = "Save", Width = 70, Margin = new Padding(3) };
            saveButton.Click += (sender, e) => this.ProfileSaveRequested?.Invoke();

            Button deleteButton = new Button { Text = "Delete", Width = 70, Margin = new Padding(3) };
            deleteButton.Click += (sender, e) =>
            {
                if (this._profilesList.SelectedItem != null)
                {
                    this.ProfileDeleteRequested?.Invoke(this._profilesList.SelectedItem.ToString());
                }
            };

            Button resetButton = new Button { Text = "Reset to Defaults", Width = 120, Margin = new Padding(3) };
            resetButton.Click += (sender, e) => this.ProfileResetRequested?.Invoke();

            Button exportButton = new Button { Text = "Export", Width = 70, Margin = new Padding(3) };
            exportButton.Click += (sender, e) => this.ProfileExportRequested?.Invoke();

            Button importButton = new Button { Text = "Import", Width = 70, Margin = new Padding(3) };
            importButton.Click += (sender, e) => this.ProfileImportRequested?.Invoke();

            buttons.Controls.Add(activateButton);
            buttons.Controls.Add(newButton);
            buttons.Controls.Add(saveButton);
            buttons.Controls.Add(deleteButton);
            buttons.Controls.Add(resetButton);
            buttons.Controls.Add(exportButton);
            buttons.Controls.Add(importButton);

            TabPage profilesTab = new TabPage
            {
                Name = "ProfilesTabPage",
                Text = "Profiles",
                UseVisualStyleBackColor = true,
                Padding = new Padding(6)
            };

            // Add Fill first so it claims the remaining space after the docked edges.
            profilesTab.Controls.Add(this._profilesList);
            profilesTab.Controls.Add(buttons);
            profilesTab.Controls.Add(this._activeProfileLabel);

            tabControl.TabPages.Add(profilesTab);
        }

        // Re-runs the form sizing now that the Hotkeys/Profiles tabs exist, then makes the window
        // auto-grow to fit each tab's content as the user switches tabs.
        private void InitTabAutoSizing()
        {
            // InitFormSize ran in the constructor before the extra tabs were added, so the left
            // tab strip was sized for fewer tabs. Re-run it now that all tabs are present.
            this.InitFormSize();
            this._baseClientSize = this.ClientSize;

            TabControl tabControl = this.Controls.Find("ContentTabControl", true).FirstOrDefault() as TabControl;
            if (tabControl != null)
            {
                tabControl.SelectedIndexChanged += (sender, e) => this.ResizeToActiveTab();
            }

            this.ResizeToActiveTab();
        }

        // Grows the window to fit the active tab's content (the scrollable Hotkeys tab can exceed
        // the base size) and shrinks back to the base size for tabs that fit. Never smaller than
        // the base size, never larger than the screen's working area.
        private void ResizeToActiveTab()
        {
            if (this._baseClientSize.IsEmpty)
            {
                return;
            }

            TabControl tabControl = this.Controls.Find("ContentTabControl", true).FirstOrDefault() as TabControl;
            if ((tabControl == null) || (tabControl.SelectedTab == null))
            {
                return;
            }

            Size target = this._baseClientSize;

            // The Hotkeys tab is the one whose content can grow beyond the base size.
            if ((tabControl.SelectedTab.Name == "HotkeysTabPage") && (this._hotkeyRowsPanel != null))
            {
                Size content = this._hotkeyRowsPanel.PreferredSize;
                // The per-client list scrolls below the fixed cycle-group + minimize-all panels,
                // so reserve their height and ensure the width fits the cycle detail (~366px).
                int neededWidth = Math.Max(content.Width, 460);
                int neededHeight = content.Height + this._hotkeyTopOffset;
                int extraWidth = this.ClientSize.Width - tabControl.DisplayRectangle.Width + 20;
                int extraHeight = this.ClientSize.Height - tabControl.DisplayRectangle.Height + 20;

                target = new Size(
                    Math.Max(this._baseClientSize.Width, neededWidth + extraWidth),
                    Math.Max(this._baseClientSize.Height, neededHeight + extraHeight));
            }

            // Cap growth so a large number of clients can't push the window off-screen.
            // Beyond this the Hotkeys panel scrolls (it is AutoScroll).
            Rectangle workingArea = Screen.FromControl(this).WorkingArea;
            int maxWidth = Math.Min(MAX_AUTO_GROW_WIDTH, workingArea.Width - 40);
            int maxHeight = Math.Min(MAX_AUTO_GROW_HEIGHT, workingArea.Height - 60);
            target.Width = Math.Min(target.Width, maxWidth);
            target.Height = Math.Min(target.Height, maxHeight);

            if (this.ClientSize != target)
            {
                this.ClientSize = target;
            }
        }

        private void RaiseActivateSelected()
        {
            if (this._profilesList?.SelectedItem != null)
            {
                this.ProfileActivateRequested?.Invoke(this._profilesList.SelectedItem.ToString());
            }
        }

        public void SetProfiles(IReadOnlyList<string> names, string activeName)
        {
            if (this._profilesList != null)
            {
                this._profilesList.BeginUpdate();
                this._profilesList.Items.Clear();
                foreach (string name in names)
                {
                    this._profilesList.Items.Add(name);
                }
                this._profilesList.SelectedItem = activeName;
                this._profilesList.EndUpdate();
            }

            if (this._activeProfileLabel != null)
            {
                this._activeProfileLabel.Text = "Active profile: " + activeName;
            }

            this.RebuildProfilesTrayMenu(names, activeName);
        }

        private void RebuildProfilesTrayMenu(IReadOnlyList<string> names, string activeName)
        {
            if (this.NotifyIcon?.ContextMenuStrip == null)
            {
                return;
            }

            if (this._profilesTrayMenuItem == null)
            {
                this._profilesTrayMenuItem = new ToolStripMenuItem("Profiles");
                this.NotifyIcon.ContextMenuStrip.Items.Insert(0, this._profilesTrayMenuItem);
            }

            this._profilesTrayMenuItem.DropDownItems.Clear();
            foreach (string name in names)
            {
                string captured = name;
                ToolStripMenuItem item = new ToolStripMenuItem(name)
                {
                    Checked = string.Equals(name, activeName, StringComparison.OrdinalIgnoreCase)
                };
                item.Click += (sender, e) => this.ProfileActivateRequested?.Invoke(captured);
                this._profilesTrayMenuItem.DropDownItems.Add(item);
            }
        }

        public ProfileSwitchChoice PromptSaveBeforeSwitch(string currentProfile)
        {
            DialogResult result = MessageBox.Show(this,
                "Save changes to the current profile \"" + currentProfile + "\" before switching?",
                "Switch Profile", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            switch (result)
            {
                case DialogResult.Yes: return ProfileSwitchChoice.Save;
                case DialogResult.No: return ProfileSwitchChoice.Discard;
                default: return ProfileSwitchChoice.Cancel;
            }
        }

        public string PromptForProfileName()
        {
            return this.ShowTextInputDialog("Profile name:", "New Profile");
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(this, message, "EVE-O Preview", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public bool PromptResetToDefaults()
        {
            return MessageBox.Show(this,
                "Reset the current profile to default settings? This cannot be undone.",
                "Reset to Defaults", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
        }

        public string PromptExportPath()
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Title = "Export Configuration";
                dialog.Filter = "EVE-O Preview config (*.json)|*.json|All files (*.*)|*.*";
                dialog.FileName = "eve-o-preview-config.json";
                return (dialog.ShowDialog(this) == DialogResult.OK) ? dialog.FileName : null;
            }
        }

        public string PromptImportPath()
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Import Configuration";
                dialog.Filter = "EVE-O Preview config (*.json)|*.json|All files (*.*)|*.*";
                dialog.CheckFileExists = true;
                return (dialog.ShowDialog(this) == DialogResult.OK) ? dialog.FileName : null;
            }
        }

        // Minimal modal text-input dialog (WinForms has no built-in InputBox).
        private string ShowTextInputDialog(string prompt, string title)
        {
            using (Form dialog = new Form())
            {
                dialog.Text = title;
                dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                dialog.StartPosition = FormStartPosition.CenterParent;
                dialog.ClientSize = new Size(320, 110);
                dialog.MinimizeBox = false;
                dialog.MaximizeBox = false;
                dialog.ShowInTaskbar = false;

                Label label = new Label { Text = prompt, AutoSize = true };
                label.SetBounds(9, 14, 300, 16);

                TextBox textBox = new TextBox();
                textBox.SetBounds(12, 34, 296, 23);

                Button okButton = new Button { Text = "OK", DialogResult = DialogResult.OK };
                okButton.SetBounds(150, 70, 75, 26);

                Button cancelButton = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel };
                cancelButton.SetBounds(233, 70, 75, 26);

                dialog.Controls.Add(label);
                dialog.Controls.Add(textBox);
                dialog.Controls.Add(okButton);
                dialog.Controls.Add(cancelButton);
                dialog.AcceptButton = okButton;
                dialog.CancelButton = cancelButton;

                return dialog.ShowDialog(this) == DialogResult.OK ? textBox.Text.Trim() : null;
            }
        }

        // One row = client title + a HotkeyInputControl that reports changes back to the presenter.
        private Panel CreateHotkeyRow(string title, Keys hotkey)
        {
            Panel row = new Panel
            {
                Width = 360,
                Height = 28,
                Margin = new Padding(2)
            };

            Label titleLabel = new Label
            {
                Text = title,
                AutoEllipsis = true,
                Location = new Point(3, 6),
                Size = new Size(170, 20)
            };

            HotkeyInputControl hotkeyControl = new HotkeyInputControl
            {
                Location = new Point(178, 3),
                Size = new Size(178, 23)
            };
            hotkeyControl.SetHotkeySilently(hotkey);
            hotkeyControl.HotkeyChanged += (sender, e) =>
                this.ClientHotkeyChanged?.Invoke(title, hotkeyControl.Hotkey);

            row.Controls.Add(titleLabel);
            row.Controls.Add(hotkeyControl);

            return row;
        }

        public void RefreshZoomSettings()
        {
            bool enableControls = this.EnableThumbnailZoom;
            this.ThumbnailZoomFactorNumericEdit.Enabled = enableControls;
            this.ZoomAnchorPanel.Enabled = enableControls;
        }

        public Action ApplicationExitRequested { get; set; }

        public Action FormActivated { get; set; }

        public Action FormMinimized { get; set; }

        public Action<ViewCloseRequest> FormCloseRequested { get; set; }

        public Action ApplicationSettingsChanged { get; set; }

        public Action ThumbnailsSizeChanged { get; set; }

        public Action<string> ThumbnailStateChanged { get; set; }

        public Action DocumentationLinkActivated { get; set; }

        public Action<string, Keys> ClientHotkeyChanged { get; set; }

        public Action<int> CycleGroupSelected { get; set; }
        public Action<string> CycleGroupAddRequested { get; set; }
        public Action<int> CycleGroupRemoveRequested { get; set; }
        public Action<int, string> CycleGroupRenameRequested { get; set; }
        public Action<int, bool, Keys> CycleGroupHotkeyChanged { get; set; }
        public Action<int, string, bool> CycleGroupMembershipChanged { get; set; }
        public Action<Keys> MinimizeAllHotkeyChanged { get; set; }

        public Action<string> ProfileActivateRequested { get; set; }
        public Action ProfileNewRequested { get; set; }
        public Action ProfileSaveRequested { get; set; }
        public Action<string> ProfileDeleteRequested { get; set; }
        public Action ProfileResetRequested { get; set; }
        public Action ProfileExportRequested { get; set; }
        public Action ProfileImportRequested { get; set; }

        #region UI events
        private void ContentTabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabControl control = (TabControl)sender;
            TabPage page = control.TabPages[e.Index];
            Rectangle bounds = control.GetTabRect(e.Index);

            Graphics graphics = e.Graphics;

            Brush textBrush = new SolidBrush(this._tabTextColor);
            Brush backgroundBrush = (e.State == DrawItemState.Selected)
                                        ? new SolidBrush(this._tabSelectedBack)
                                        : new SolidBrush(this._tabUnselectedBack);
            graphics.FillRectangle(backgroundBrush, e.Bounds);

            // Use our own font
            Font font = new Font("Arial", this.Font.Size * 1.5f, FontStyle.Bold, GraphicsUnit.Pixel);

            // Draw string and center the text
            StringFormat stringFlags = new StringFormat();
            stringFlags.Alignment = StringAlignment.Center;
            stringFlags.LineAlignment = StringAlignment.Center;

            graphics.DrawString(page.Text, font, textBrush, bounds, stringFlags);
        }

        private void OptionChanged_Handler(object sender, EventArgs e)
        {
            if (this._suppressEvents)
            {
                return;
            }

            this.ApplicationSettingsChanged?.Invoke();
        }

        private void ThumbnailSizeChanged_Handler(object sender, EventArgs e)
        {
            if (this._suppressEvents)
            {
                return;
            }

            // Perform some View work that is not properly done in the Control
            this._suppressEvents = true;
            Size thumbnailSize = this.ThumbnailSize;
            thumbnailSize.Width = Math.Min(Math.Max(thumbnailSize.Width, this._minimumSize.Width), this._maximumSize.Width);
            thumbnailSize.Height = Math.Min(Math.Max(thumbnailSize.Height, this._minimumSize.Height), this._maximumSize.Height);
            this.ThumbnailSize = thumbnailSize;
            this._suppressEvents = false;

            this.ThumbnailsSizeChanged?.Invoke();
        }

        private void ActiveClientHighlightColorButton_Click(object sender, EventArgs e)
        {
            using (ColorDialog dialog = new ColorDialog())
            {
                dialog.Color = this.ActiveClientHighlightColor;

                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                this.ActiveClientHighlightColor = dialog.Color;
            }

            this.OptionChanged_Handler(sender, e);
        }

        private void OverlayLabelColorButton_Click(object sender, EventArgs e)
        {
            using (ColorDialog dialog = new ColorDialog())
            {
                dialog.Color = this.OverlayLabelColor;

                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                this.OverlayLabelColor = dialog.Color;
            }

            this.OptionChanged_Handler(sender, e);
        }

        private void ThumbnailsList_ItemCheck_Handler(object sender, ItemCheckEventArgs e)
        {
            if (!(this.ThumbnailsList.Items[e.Index] is IThumbnailDescription selectedItem))
            {
                return;
            }

            selectedItem.IsDisabled = (e.NewValue == CheckState.Checked);

            this.ThumbnailStateChanged?.Invoke(selectedItem.Title);
        }

        private void DocumentationLinkClicked_Handler(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.DocumentationLinkActivated?.Invoke();
        }

        private void MainFormResize_Handler(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
            {
                return;
            }

            this.FormMinimized?.Invoke();
        }

        private void MainFormClosing_Handler(object sender, FormClosingEventArgs e)
        {
            ViewCloseRequest request = new ViewCloseRequest();

            this.FormCloseRequested?.Invoke(request);

            e.Cancel = !request.Allow;
        }

        private void RestoreMainForm_Handler(object sender, EventArgs e)
        {
            // This is form's GUI lifecycle event that is invariant to the Form data
            base.Show();
            this.WindowState = FormWindowState.Normal;
            this.BringToFront();
        }

        private void ExitMenuItemClick_Handler(object sender, EventArgs e)
        {
            this.ApplicationExitRequested?.Invoke();
        }
        #endregion

        private void InitZoomAnchorMap()
        {
            this._zoomAnchorMap[ViewZoomAnchor.NW] = this.ZoomAanchorNWRadioButton;
            this._zoomAnchorMap[ViewZoomAnchor.N] = this.ZoomAanchorNRadioButton;
            this._zoomAnchorMap[ViewZoomAnchor.NE] = this.ZoomAanchorNERadioButton;
            this._zoomAnchorMap[ViewZoomAnchor.W] = this.ZoomAanchorWRadioButton;
            this._zoomAnchorMap[ViewZoomAnchor.C] = this.ZoomAanchorCRadioButton;
            this._zoomAnchorMap[ViewZoomAnchor.E] = this.ZoomAanchorERadioButton;
            this._zoomAnchorMap[ViewZoomAnchor.SW] = this.ZoomAanchorSWRadioButton;
            this._zoomAnchorMap[ViewZoomAnchor.S] = this.ZoomAanchorSRadioButton;
            this._zoomAnchorMap[ViewZoomAnchor.SE] = this.ZoomAanchorSERadioButton;
        }
        private void InitOverlayLabelMap()
        {
            this._overlayLabelMap[ViewZoomAnchor.NW] = this.OverlayLabelNWRadioButton;
            this._overlayLabelMap[ViewZoomAnchor.N] = this.OverlayLabelNRadioButton;
            this._overlayLabelMap[ViewZoomAnchor.NE] = this.OverlayLabelNERadioButton;
            this._overlayLabelMap[ViewZoomAnchor.W] = this.OverlayLabelWRadioButton;
            this._overlayLabelMap[ViewZoomAnchor.C] = this.OverlayLabelCRadioButton;
            this._overlayLabelMap[ViewZoomAnchor.E] = this.OverlayLabelERadioButton;
            this._overlayLabelMap[ViewZoomAnchor.SW] = this.OverlayLabelSWRadioButton;
            this._overlayLabelMap[ViewZoomAnchor.S] = this.OverlayLabelSRadioButton;
            this._overlayLabelMap[ViewZoomAnchor.SE] = this.OverlayLabelSERadioButton;
        }
        private void InitCycleGroupIndicatorMap()
        {
            this._cycleGroupIndicatorMap[ViewZoomAnchor.NW] = this.CycleGroupIndicatorNWRadioButton;
            this._cycleGroupIndicatorMap[ViewZoomAnchor.N] = this.CycleGroupIndicatorNRadioButton;
            this._cycleGroupIndicatorMap[ViewZoomAnchor.NE] = this.CycleGroupIndicatorNERadioButton;
            this._cycleGroupIndicatorMap[ViewZoomAnchor.W] = this.CycleGroupIndicatorWRadioButton;
            this._cycleGroupIndicatorMap[ViewZoomAnchor.C] = this.CycleGroupIndicatorCRadioButton;
            this._cycleGroupIndicatorMap[ViewZoomAnchor.E] = this.CycleGroupIndicatorERadioButton;
            this._cycleGroupIndicatorMap[ViewZoomAnchor.SW] = this.CycleGroupIndicatorSWRadioButton;
            this._cycleGroupIndicatorMap[ViewZoomAnchor.S] = this.CycleGroupIndicatorSRadioButton;
            this._cycleGroupIndicatorMap[ViewZoomAnchor.SE] = this.CycleGroupIndicatorSERadioButton;
        }

        private void InitFormSize()
        {
            const int BUFFER_PIXEL_AMOUNT = 8;
            // resize form height based on tabbed control item height
            var tabControl = (System.Windows.Forms.TabControl)this.Controls.Find("ContentTabControl", false).First();
            if (tabControl != null)
            {
                var furnitureSize = this.Height - tabControl.Height;
                var calculatedHeight = (tabControl.ItemSize.Width * tabControl.Controls.Count) + furnitureSize + BUFFER_PIXEL_AMOUNT;
                if (this.Height < calculatedHeight)
                {
                    this.Height = calculatedHeight;
                }
            }
        }

        private void btnLabelFont_Click(object sender, EventArgs e)
        {
            FontDialog fontSelector = new FontDialog();
            fontSelector.Font = OverlayLabelFont;
            fontSelector.ShowColor = false;
            fontSelector.ShowApply = false;
            fontSelector.ShowHelp = false;
            if (fontSelector.ShowDialog() != DialogResult.Cancel)
            {
                OverlayLabelFont = fontSelector.Font;
                LabelOverlayLabelFont.Font = fontSelector.Font;
                this.OptionChanged_Handler(sender, e);
            }
        }

        private void PreventPreviewColorButton_Click(object sender, EventArgs e)
        {
            using (ColorDialog dialog = new ColorDialog())
            {
                dialog.Color = this.PreventPreviewColor;

                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                this.PreventPreviewColor = dialog.Color;
            }

            this.OptionChanged_Handler(sender, e);

        }
    }
}