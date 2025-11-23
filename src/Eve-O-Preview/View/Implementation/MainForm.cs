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
        private ViewZoomAnchor _cachedThumbnailZoomAnchor;
        private ViewZoomAnchor _cachedOverlayLabelAnchor;
        private bool _suppressEvents;
        private Size _minimumSize;
        private Size _maximumSize;
        private string _iconName;

        #endregion

        public MainForm(ApplicationContext context)
        {
            this._context = context;
            this._zoomAnchorMap = new Dictionary<ViewZoomAnchor, RadioButton>();
            this._overlayLabelMap = new Dictionary<ViewZoomAnchor, RadioButton>();
            this._cachedThumbnailZoomAnchor = ViewZoomAnchor.NW;
            this._suppressEvents = false;
            this._minimumSize = new Size(20, 20);
            this._maximumSize = new Size(20, 20);

            InitializeComponent();

            this.ThumbnailsList.DisplayMember = "Title";

            this.InitZoomAnchorMap();
            this.InitOverlayLabelMap();
            this.InitFormSize();

            this.AnimationStyleCombo.DataSource = Enum.GetValues(typeof(AnimationStyle));
            
            // Note: Language combo initialization and localization application 
            // will be handled by the presenter after loading configuration
            // Initialize language combo
            // InitializeLanguageCombo();

            // Apply localization
            // ApplyLocalization();
        }

        private void InitializeLanguageCombo()
        {
            System.Diagnostics.Debug.WriteLine($"InitializeLanguageCombo: Current language = {LocalizationManager.GetCurrentLanguage()}");
			// Add language options
			LanguageCombo.Items.Add(LocalizationManager.GetString("English (en-US)"));
			LanguageCombo.Items.Add(LocalizationManager.GetString("中文 (zh-CN)"));
			
			// Set default option based on current language
			string currentLanguage = LocalizationManager.GetCurrentLanguage();
			LanguageCombo.SelectedIndex = currentLanguage == "zh-CN" ? 1 : 0;
			System.Diagnostics.Debug.WriteLine($"InitializeLanguageCombo: Selected index = {LanguageCombo.SelectedIndex}");
		}
		
		private void LanguageChanged_Handler(object sender, EventArgs e)
		{
			System.Diagnostics.Debug.WriteLine("LanguageChanged_Handler: Start");
			// Prevent recursive calls
			LanguageCombo.SelectedIndexChanged -= LanguageChanged_Handler;
			
			int selectedIndex = LanguageCombo.SelectedIndex;
			string languageCode = selectedIndex == 1 ? "zh-CN" : "en-US";
			
			System.Diagnostics.Debug.WriteLine($"LanguageChanged_Handler: Language changed to: {languageCode}");
			
			SwitchLanguage(languageCode);
			
			// Save configuration to persist language settings
			ApplicationSettingsChanged?.Invoke();
			
			// Update the language combo selection to match the current language
			LanguageCombo.SelectedIndex = languageCode == "zh-CN" ? 1 : 0;
			
			// Rebind event handler
			LanguageCombo.SelectedIndexChanged += LanguageChanged_Handler;
			System.Diagnostics.Debug.WriteLine("LanguageChanged_Handler: End");
		}

		// Switch language
		public void SwitchLanguage(string languageCode)
		{
			System.Diagnostics.Debug.WriteLine($"SwitchLanguage: Start, languageCode = {languageCode}");
			LocalizationManager.SetLanguage(languageCode);
			System.Diagnostics.Debug.WriteLine($"SwitchLanguage: Language set, current = {LocalizationManager.GetCurrentLanguage()}");
			ApplyLocalization();
			System.Diagnostics.Debug.WriteLine("SwitchLanguage: ApplyLocalization completed");
			
			// Reset NotifyIcon text as ApplyLocalization does not handle it
			this.NotifyIcon.Text = LocalizationManager.GetString("ApplicationTitleText");
			
			// Refresh the entire form to ensure all controls are updated correctly
			this.PerformLayout();
			this.Refresh();
			System.Diagnostics.Debug.WriteLine("SwitchLanguage: End");
		}
		
		// Initialize language combo after configuration is loaded
		public void InitializeLanguageControls()
		{
			System.Diagnostics.Debug.WriteLine($"InitializeLanguageControls: Start, current language = {LocalizationManager.GetCurrentLanguage()}");
			
			// Initialize language combo
			InitializeLanguageCombo();
			
			// Apply localization
			ApplyLocalization();
			System.Diagnostics.Debug.WriteLine("InitializeLanguageControls: End");
		}

		public void ApplyLocalization()
		{
			System.Diagnostics.Debug.WriteLine($"ApplyLocalization: Start, current language = {LocalizationManager.GetCurrentLanguage()}");
			
			// Apply localization to the main form itself
			LocalizationExtensions.SetFormLocalization(this);
			
			// Apply localization to all controls using the extension method
			LocalizationExtensions.ApplyLocalization(this);
			
			// Apply localization to menu items
			foreach (ToolStripItem item in this.TrayMenu.Items)
			{
				item.ApplyLocalization();
			}
			
			// Special cases for controls not handled by the extension method
			this.NotifyIcon.Text = LocalizationManager.GetString("ApplicationTitleText");
			
			// Special handling for language combo box to ensure option text is also properly localized
			// but avoid triggering SelectedIndexChanged event causing recursive calls
			if (LanguageCombo != null)
			{
				int selectedIndex = LanguageCombo.SelectedIndex;
				LanguageCombo.Items.Clear();
				LanguageCombo.Items.Add(LocalizationManager.GetString("English (en-US)"));
				LanguageCombo.Items.Add(LocalizationManager.GetString("中文 (zh-CN)"));
				
				// Ensure the selected index matches the current language
				string currentLanguage = LocalizationManager.GetCurrentLanguage();
				LanguageCombo.SelectedIndex = currentLanguage == "zh-CN" ? 1 : 0;
			}
			
			// Force refresh of all tab pages to ensure all controls are properly localized
			// Find the ContentTabControl control
			TabControl contentTabControl = null;
			foreach (Control control in this.Controls)
			{
				if (control is TabControl tc && tc.Name == "ContentTabControl")
				{
					contentTabControl = tc;
					break;
				}
			}
			
			if (contentTabControl != null)
			{
				// First, re-apply localization to all tab pages
				foreach (TabPage tabPage in contentTabControl.TabPages)
				{
					// Localize the tab page itself
					switch (tabPage.Name)
					{
						case "GeneralTabPage":
							tabPage.Text = LocalizationManager.GetString("GeneralTabText");
							break;
						case "ThumbnailTabPage":
							tabPage.Text = LocalizationManager.GetString("ThumbnailTabText");
							break;
						case "ZoomTabPage":
							tabPage.Text = LocalizationManager.GetString("ZoomTabText");
							break;
						case "OverlayTabPage":
							tabPage.Text = LocalizationManager.GetString("OverlayTabText");
							break;
						case "ClientsTabPage":
							tabPage.Text = LocalizationManager.GetString("ActiveClientsTabText");
							break;
						case "AboutTabPage":
							tabPage.Text = LocalizationManager.GetString("AboutTabText");
							break;
						case "LanguageTabPage":
							tabPage.Text = LocalizationManager.GetString("LanguageTabText");
							break;
					}
					
					// Then recursively apply localization to all child controls
					foreach (Control control in tabPage.Controls)
					{
						ApplyLocalizationRecursive(control);
					}
				}
			}
		}
		
		// Recursive method to apply localization to all controls
		private void ApplyLocalizationRecursive(Control control)
		{
			// Apply localization to the current control
			LocalizationExtensions.ApplyLocalization(control);
			
			// If this is a container control, recursively apply to its children
			if (control is Panel panel)
			{
				foreach (Control child in panel.Controls)
				{
					ApplyLocalizationRecursive(child);
				}
			}
			else if (control is GroupBox groupBox)
			{
				foreach (Control child in groupBox.Controls)
				{
					ApplyLocalizationRecursive(child);
				}
			}
			else if (control is TabControl tabControl)
			{
				foreach (TabPage tabPage in tabControl.TabPages)
				{
					// Localize the tab page itself
					switch (tabPage.Name)
					{
						case "GeneralTabPage":
							tabPage.Text = LocalizationManager.GetString("GeneralTabText");
							break;
						case "ThumbnailTabPage":
							tabPage.Text = LocalizationManager.GetString("ThumbnailTabText");
							break;
						case "ZoomTabPage":
							tabPage.Text = LocalizationManager.GetString("ZoomTabText");
							break;
						case "OverlayTabPage":
							tabPage.Text = LocalizationManager.GetString("OverlayTabText");
							break;
						case "ClientsTabPage":
							tabPage.Text = LocalizationManager.GetString("ActiveClientsTabText");
							break;
						case "AboutTabPage":
							tabPage.Text = LocalizationManager.GetString("AboutTabText");
							break;
						case "LanguageTabPage":
							tabPage.Text = LocalizationManager.GetString("LanguageTabText");
							break;
					}
					
					// Then recursively apply to children
					foreach (Control child in tabPage.Controls)
					{
						ApplyLocalizationRecursive(child);
					}
				}
			}
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
                System.ComponentModel.ComponentResourceManager resources =
                    new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
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
                catch (Exception ex)
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

        public int OverlayLabelSize
        {
            get => (int)this.OverlayLabelSizeNumericEdit.Value;
            set { this.OverlayLabelSizeNumericEdit.Value = value; }
        }

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
        }

        public void RemoveThumbnails(IList<IThumbnailDescription> thumbnails)
        {
            this.ThumbnailsList.BeginUpdate();

            foreach (IThumbnailDescription view in thumbnails)
            {
                this.ThumbnailsList.Items.Remove(view);
            }

            this.ThumbnailsList.EndUpdate();
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

        #region UI events

        private void ContentTabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabControl control = (TabControl)sender;
            TabPage page = control.TabPages[e.Index];
            Rectangle bounds = control.GetTabRect(e.Index);

            Graphics graphics = e.Graphics;

            Brush textBrush = new SolidBrush(SystemColors.ActiveCaptionText);
            Brush backgroundBrush = (e.State == DrawItemState.Selected)
                ? new SolidBrush(SystemColors.Control)
                : new SolidBrush(SystemColors.ControlDark);
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
            thumbnailSize.Width = Math.Min(Math.Max(thumbnailSize.Width, this._minimumSize.Width),
                this._maximumSize.Width);
            thumbnailSize.Height = Math.Min(Math.Max(thumbnailSize.Height, this._minimumSize.Height),
                this._maximumSize.Height);
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

        private void InitFormSize()
        {
            const int BUFFER_PIXEL_AMOUNT = 8;
            // resize form height based on tabbed control item height
            var tabControl = (System.Windows.Forms.TabControl)this.Controls.Find("ContentTabControl", false).First();
            if (tabControl != null)
            {
                var furnitureSize = this.Height - tabControl.Height;
                var calculatedHeight = (tabControl.ItemSize.Width * tabControl.Controls.Count) + furnitureSize +
                                       BUFFER_PIXEL_AMOUNT;
                if (this.Height < calculatedHeight)
                {
                    this.Height = calculatedHeight;
                }
            }
        }

        private void AnimationStyleCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void GeneralSettingsPanel_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}