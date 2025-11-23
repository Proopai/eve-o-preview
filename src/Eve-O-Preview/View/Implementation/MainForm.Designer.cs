using System.Drawing;
using System.Windows.Forms;
using EveOPreview.Properties;

namespace EveOPreview.View
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>s
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			ToolStripMenuItem RestoreWindowMenuItem;
			ToolStripMenuItem ExitMenuItem;
			ToolStripMenuItem TitleMenuItem;
			ToolStripSeparator SeparatorMenuItem;
			TabControl ContentTabControl;
			TabPage GeneralTabPage;
			Panel GeneralSettingsPanel;
			Label label4;
			TabPage ThumbnailTabPage;
			Panel ThumbnailSettingsPanel;
			Label HeightLabel;
			Label WidthLabel;
			Label OpacityLabel;
			Panel ZoomSettingsPanel;
			Label ZoomFactorLabel;
			Label ZoomAnchorLabel;
			TabPage OverlayTabPage;
			Panel OverlaySettingsPanel;
			TabPage ClientsTabPage;
			Panel ClientsPanel;
			Label ThumbnailsListLabel;
			TabPage AboutTabPage;
			Panel AboutPanel;
			Label CreditMaintLabel;
			Label DocumentationLinkLabel;
			Label DescriptionLabel;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			Label NameLabel;
			AnimationStyleCombo = new ComboBox();
			MinimizeInactiveClientsCheckBox = new CheckBox();
			EnableClientLayoutTrackingCheckBox = new CheckBox();
			HideActiveClientThumbnailCheckBox = new CheckBox();
			ShowThumbnailsAlwaysOnTopCheckBox = new CheckBox();
			HideThumbnailsOnLostFocusCheckBox = new CheckBox();
			EnablePerClientThumbnailsLayoutsCheckBox = new CheckBox();
			MinimizeToTrayCheckBox = new CheckBox();
			ThumbnailSnapToGridCheckBox = new CheckBox();
			ThumbnailSnapToGridSizeYNumericEdit = new NumericUpDown();
			SnapYLabel = new Label();
			ThumbnailSnapToGridSizeXNumericEdit = new NumericUpDown();
			SnapXLabel = new Label();
			LockThumbnailLocationCheckbox = new CheckBox();
			ThumbnailsWidthNumericEdit = new NumericUpDown();
			ThumbnailsHeightNumericEdit = new NumericUpDown();
			ThumbnailOpacityTrackBar = new TrackBar();
			ZoomTabPage = new TabPage();
			ZoomAnchorPanel = new Panel();
			ZoomAanchorNWRadioButton = new RadioButton();
			ZoomAanchorNRadioButton = new RadioButton();
			ZoomAanchorNERadioButton = new RadioButton();
			ZoomAanchorWRadioButton = new RadioButton();
			ZoomAanchorSERadioButton = new RadioButton();
			ZoomAanchorCRadioButton = new RadioButton();
			ZoomAanchorSRadioButton = new RadioButton();
			ZoomAanchorERadioButton = new RadioButton();
			ZoomAanchorSWRadioButton = new RadioButton();
			EnableThumbnailZoomCheckBox = new CheckBox();
			ThumbnailZoomFactorNumericEdit = new NumericUpDown();
			label3 = new Label();
			label2 = new Label();
			OverlayLabelColorButton = new Panel();
			OverlayLabelSizeNumericEdit = new NumericUpDown();
			panel1 = new Panel();
			OverlayLabelNWRadioButton = new RadioButton();
			OverlayLabelNRadioButton = new RadioButton();
			OverlayLabelNERadioButton = new RadioButton();
			OverlayLabelWRadioButton = new RadioButton();
			OverlayLabelSERadioButton = new RadioButton();
			OverlayLabelCRadioButton = new RadioButton();
			OverlayLabelSRadioButton = new RadioButton();
			OverlayLabelERadioButton = new RadioButton();
			OverlayLabelSWRadioButton = new RadioButton();
			label1 = new Label();
			HighlightColorLabel = new Label();
			ActiveClientHighlightColorButton = new Panel();
			EnableActiveClientHighlightCheckBox = new CheckBox();
			ShowThumbnailOverlaysCheckBox = new CheckBox();
			ShowThumbnailFramesCheckBox = new CheckBox();
			ThumbnailsList = new CheckedListBox();
			VersionLabel = new Label();
			DocumentationLink = new LinkLabel();
			NotifyIcon = new NotifyIcon(components);
			TrayMenu = new ContextMenuStrip(components);
			RestoreWindowMenuItem = new ToolStripMenuItem();
			ExitMenuItem = new ToolStripMenuItem();
			TitleMenuItem = new ToolStripMenuItem();
			SeparatorMenuItem = new ToolStripSeparator();
			ContentTabControl = new TabControl();
			GeneralTabPage = new TabPage();
			GeneralSettingsPanel = new Panel();
			label4 = new Label();
			ThumbnailTabPage = new TabPage();
			ThumbnailSettingsPanel = new Panel();
			HeightLabel = new Label();
			WidthLabel = new Label();
			OpacityLabel = new Label();
			ZoomSettingsPanel = new Panel();
			ZoomFactorLabel = new Label();
			ZoomAnchorLabel = new Label();
			OverlayTabPage = new TabPage();
			OverlaySettingsPanel = new Panel();
			ClientsTabPage = new TabPage();
			ClientsPanel = new Panel();
			ThumbnailsListLabel = new Label();
			AboutTabPage = new TabPage();
			AboutPanel = new Panel();
			CreditMaintLabel = new Label();
			DocumentationLinkLabel = new Label();
			DescriptionLabel = new Label();
			NameLabel = new Label();
			//
			//Add a language selection control
			//
			LanguageLabel = new Label();
			LanguageCombo = new ComboBox();
			LanguageTabPage = new TabPage();
			LanguageSettingsPanel = new Panel();
			
			ContentTabControl.SuspendLayout();
			GeneralTabPage.SuspendLayout();
			GeneralSettingsPanel.SuspendLayout();
			ThumbnailTabPage.SuspendLayout();
			ThumbnailSettingsPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ThumbnailSnapToGridSizeYNumericEdit).BeginInit();
			((System.ComponentModel.ISupportInitialize)ThumbnailSnapToGridSizeXNumericEdit).BeginInit();
			((System.ComponentModel.ISupportInitialize)ThumbnailsWidthNumericEdit).BeginInit();
			((System.ComponentModel.ISupportInitialize)ThumbnailsHeightNumericEdit).BeginInit();
			((System.ComponentModel.ISupportInitialize)ThumbnailOpacityTrackBar).BeginInit();
			ZoomTabPage.SuspendLayout();
			ZoomSettingsPanel.SuspendLayout();
			ZoomAnchorPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ThumbnailZoomFactorNumericEdit).BeginInit();
			OverlayTabPage.SuspendLayout();
			OverlaySettingsPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)OverlayLabelSizeNumericEdit).BeginInit();
			panel1.SuspendLayout();
			ClientsTabPage.SuspendLayout();
			ClientsPanel.SuspendLayout();
			AboutTabPage.SuspendLayout();
			AboutPanel.SuspendLayout();
			TrayMenu.SuspendLayout();
			SuspendLayout();
			// 
			// RestoreWindowMenuItem
			// 
			RestoreWindowMenuItem.Name = "RestoreWindowMenuItem";
			RestoreWindowMenuItem.Size = new Size(201, 32);
			RestoreWindowMenuItem.Text = LocalizationManager.GetString("RestoreMenuItemText");
			RestoreWindowMenuItem.Click += RestoreMainForm_Handler;
			// 
			// ExitMenuItem
			// 
			ExitMenuItem.Name = "ExitMenuItem";
			ExitMenuItem.Size = new Size(201, 32);
			ExitMenuItem.Text = LocalizationManager.GetString("ExitMenuItemText");
			ExitMenuItem.Click += ExitMenuItemClick_Handler;
			// 
			// TitleMenuItem
			// 
			TitleMenuItem.Enabled = false;
			TitleMenuItem.Name = "TitleMenuItem";
			TitleMenuItem.Size = new Size(201, 32);
			TitleMenuItem.Text = "EVE-O-Preview";
			// 
			// SeparatorMenuItem
			// 
			SeparatorMenuItem.Name = "SeparatorMenuItem";
			SeparatorMenuItem.Size = new Size(198, 6);
			// 
			// ContentTabControl
			// 
			ContentTabControl.Alignment = TabAlignment.Left;
			ContentTabControl.Controls.Add(GeneralTabPage);
			ContentTabControl.Controls.Add(ThumbnailTabPage);
			ContentTabControl.Controls.Add(ZoomTabPage);
			ContentTabControl.Controls.Add(OverlayTabPage);
			ContentTabControl.Controls.Add(ClientsTabPage);
			ContentTabControl.Controls.Add(LanguageTabPage);
			ContentTabControl.Controls.Add(AboutTabPage);
			ContentTabControl.Dock = DockStyle.Fill;
			ContentTabControl.DrawMode = TabDrawMode.OwnerDrawFixed;
			ContentTabControl.ItemSize = new Size(35, 120);
			ContentTabControl.Location = new Point(0, 0);
			ContentTabControl.Margin = new Padding(5, 6, 5, 6);
			ContentTabControl.Multiline = true;
			ContentTabControl.Name = "ContentTabControl";
			ContentTabControl.SelectedIndex = 0;
			ContentTabControl.Size = new Size(650, 419);
			ContentTabControl.SizeMode = TabSizeMode.Fixed;
			ContentTabControl.TabIndex = 6;
			ContentTabControl.DrawItem += ContentTabControl_DrawItem;
			// 
			// GeneralTabPage
			// 
			GeneralTabPage.BackColor = SystemColors.Control;
			GeneralTabPage.Controls.Add(GeneralSettingsPanel);
			GeneralTabPage.Location = new Point(124, 4);
			GeneralTabPage.Margin = new Padding(5, 6, 5, 6);
			GeneralTabPage.Name = "GeneralTabPage";
			GeneralTabPage.Padding = new Padding(5, 6, 5, 6);
			GeneralTabPage.Size = new Size(522, 411);
			GeneralTabPage.TabIndex = 0;
			GeneralTabPage.Text = LocalizationManager.GetString("GeneralTabText");
			// 
			// GeneralSettingsPanel
			// 
			GeneralSettingsPanel.BorderStyle = BorderStyle.FixedSingle;
			// Removed language controls from General tab
			GeneralSettingsPanel.Controls.Add(label4);
			GeneralSettingsPanel.Controls.Add(AnimationStyleCombo);
			GeneralSettingsPanel.Controls.Add(MinimizeInactiveClientsCheckBox);
			GeneralSettingsPanel.Controls.Add(EnableClientLayoutTrackingCheckBox);
			GeneralSettingsPanel.Controls.Add(HideActiveClientThumbnailCheckBox);
			GeneralSettingsPanel.Controls.Add(ShowThumbnailsAlwaysOnTopCheckBox);
			GeneralSettingsPanel.Controls.Add(HideThumbnailsOnLostFocusCheckBox);
			GeneralSettingsPanel.Controls.Add(EnablePerClientThumbnailsLayoutsCheckBox);
			GeneralSettingsPanel.Controls.Add(MinimizeToTrayCheckBox);
			GeneralSettingsPanel.Dock = DockStyle.Fill;
			GeneralSettingsPanel.Location = new Point(5, 6);
			GeneralSettingsPanel.Margin = new Padding(5, 6, 5, 6);
			GeneralSettingsPanel.Name = "GeneralSettingsPanel";
			GeneralSettingsPanel.Size = new Size(512, 399);
			GeneralSettingsPanel.TabIndex = 18;
			GeneralSettingsPanel.Paint += GeneralSettingsPanel_Paint;
			// 
			// LanguageLabel and LanguageCombo have been moved to LanguageTabPage
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Location = new Point(8, 192);
			label4.Margin = new Padding(5, 0, 5, 0);
			label4.Name = "label4";
			label4.Size = new Size(136, 25);
			label4.TabIndex = 27;
			label4.Text = LocalizationManager.GetString("AnimationStyleLabelText");
			// 
			// AnimationStyleCombo
			// 
			AnimationStyleCombo.DropDownStyle = ComboBoxStyle.DropDownList;
			AnimationStyleCombo.FormattingEnabled = true;
			AnimationStyleCombo.Location = new Point(150, 187);
			AnimationStyleCombo.Margin = new Padding(5, 6, 5, 6);
			AnimationStyleCombo.Name = "AnimationStyleCombo";
			AnimationStyleCombo.Size = new Size(251, 33);
			AnimationStyleCombo.TabIndex = 26;
			AnimationStyleCombo.SelectedIndexChanged += OptionChanged_Handler;
			// 
			// MinimizeInactiveClientsCheckBox
			// 
			MinimizeInactiveClientsCheckBox.AutoSize = true;
			MinimizeInactiveClientsCheckBox.Location = new Point(13, 150);
			MinimizeInactiveClientsCheckBox.Margin = new Padding(5, 6, 5, 6);
			MinimizeInactiveClientsCheckBox.Name = "MinimizeInactiveClientsCheckBox";
			MinimizeInactiveClientsCheckBox.Size = new Size(261, 29);
			MinimizeInactiveClientsCheckBox.TabIndex = 24;
			MinimizeInactiveClientsCheckBox.Text = LocalizationManager.GetString("MinimizeInactiveClientsCheckBoxText");
			MinimizeInactiveClientsCheckBox.UseVisualStyleBackColor = true;
			MinimizeInactiveClientsCheckBox.CheckedChanged += OptionChanged_Handler;
			// 
			// EnableClientLayoutTrackingCheckBox
			// 
			EnableClientLayoutTrackingCheckBox.AutoSize = true;
			EnableClientLayoutTrackingCheckBox.Location = new Point(13, 60);
			EnableClientLayoutTrackingCheckBox.Margin = new Padding(5, 6, 5, 6);
			EnableClientLayoutTrackingCheckBox.Name = "EnableClientLayoutTrackingCheckBox";
			EnableClientLayoutTrackingCheckBox.Size = new Size(199, 29);
			EnableClientLayoutTrackingCheckBox.TabIndex = 19;
			EnableClientLayoutTrackingCheckBox.Text = LocalizationManager.GetString("EnableClientLayoutTrackingCheckBoxText");
			EnableClientLayoutTrackingCheckBox.UseVisualStyleBackColor = true;
			EnableClientLayoutTrackingCheckBox.CheckedChanged += OptionChanged_Handler;
			// 
			// HideActiveClientThumbnailCheckBox
			// 
			HideActiveClientThumbnailCheckBox.AutoSize = true;
			HideActiveClientThumbnailCheckBox.Checked = true;
			HideActiveClientThumbnailCheckBox.CheckState = CheckState.Checked;
			HideActiveClientThumbnailCheckBox.Location = new Point(13, 106);
			HideActiveClientThumbnailCheckBox.Margin = new Padding(5, 6, 5, 6);
			HideActiveClientThumbnailCheckBox.Name = "HideActiveClientThumbnailCheckBox";
			HideActiveClientThumbnailCheckBox.Size = new Size(293, 29);
			HideActiveClientThumbnailCheckBox.TabIndex = 20;
			HideActiveClientThumbnailCheckBox.Text = LocalizationManager.GetString("HideActiveClientThumbnailCheckBoxText");
			HideActiveClientThumbnailCheckBox.UseVisualStyleBackColor = true;
			HideActiveClientThumbnailCheckBox.CheckedChanged += OptionChanged_Handler;
			// 
			// ShowThumbnailsAlwaysOnTopCheckBox
			// 
			ShowThumbnailsAlwaysOnTopCheckBox.AutoSize = true;
			ShowThumbnailsAlwaysOnTopCheckBox.Checked = true;
			ShowThumbnailsAlwaysOnTopCheckBox.CheckState = CheckState.Checked;
			ShowThumbnailsAlwaysOnTopCheckBox.Location = new Point(13, 238);
			ShowThumbnailsAlwaysOnTopCheckBox.Margin = new Padding(5, 6, 5, 6);
			ShowThumbnailsAlwaysOnTopCheckBox.Name = "ShowThumbnailsAlwaysOnTopCheckBox";
			ShowThumbnailsAlwaysOnTopCheckBox.RightToLeft = RightToLeft.No;
			ShowThumbnailsAlwaysOnTopCheckBox.Size = new Size(222, 29);
			ShowThumbnailsAlwaysOnTopCheckBox.TabIndex = 21;
			ShowThumbnailsAlwaysOnTopCheckBox.Text = LocalizationManager.GetString("ShowThumbnailsAlwaysOnTopCheckBoxText");
			ShowThumbnailsAlwaysOnTopCheckBox.UseVisualStyleBackColor = true;
			ShowThumbnailsAlwaysOnTopCheckBox.CheckedChanged += OptionChanged_Handler;
			// 
			// HideThumbnailsOnLostFocusCheckBox
			// 
			HideThumbnailsOnLostFocusCheckBox.AutoSize = true;
			HideThumbnailsOnLostFocusCheckBox.Checked = true;
			HideThumbnailsOnLostFocusCheckBox.CheckState = CheckState.Checked;
			HideThumbnailsOnLostFocusCheckBox.Location = new Point(13, 285);
			HideThumbnailsOnLostFocusCheckBox.Margin = new Padding(5, 6, 5, 6);
			HideThumbnailsOnLostFocusCheckBox.Name = "HideThumbnailsOnLostFocusCheckBox";
			HideThumbnailsOnLostFocusCheckBox.Size = new Size(375, 29);
			HideThumbnailsOnLostFocusCheckBox.TabIndex = 22;
			HideThumbnailsOnLostFocusCheckBox.Text = LocalizationManager.GetString("HideThumbnailsOnLostFocusCheckBoxText");
			HideThumbnailsOnLostFocusCheckBox.UseVisualStyleBackColor = true;
			HideThumbnailsOnLostFocusCheckBox.CheckedChanged += OptionChanged_Handler;
			// 
			// EnablePerClientThumbnailsLayoutsCheckBox
			// 
			EnablePerClientThumbnailsLayoutsCheckBox.AutoSize = true;
			EnablePerClientThumbnailsLayoutsCheckBox.Checked = true;
			EnablePerClientThumbnailsLayoutsCheckBox.CheckState = CheckState.Checked;
			EnablePerClientThumbnailsLayoutsCheckBox.Location = new Point(13, 331);
			EnablePerClientThumbnailsLayoutsCheckBox.Margin = new Padding(5, 6, 5, 6);
			EnablePerClientThumbnailsLayoutsCheckBox.Name = "EnablePerClientThumbnailsLayoutsCheckBox";
			EnablePerClientThumbnailsLayoutsCheckBox.Size = new Size(297, 29);
			EnablePerClientThumbnailsLayoutsCheckBox.TabIndex = 23;
			EnablePerClientThumbnailsLayoutsCheckBox.Text = LocalizationManager.GetString("EnablePerClientThumbnailsLayoutsCheckBoxText");
			EnablePerClientThumbnailsLayoutsCheckBox.UseVisualStyleBackColor = true;
			EnablePerClientThumbnailsLayoutsCheckBox.CheckedChanged += OptionChanged_Handler;
			// 
			// MinimizeToTrayCheckBox
			// 
			MinimizeToTrayCheckBox.AutoSize = true;
			MinimizeToTrayCheckBox.Location = new Point(13, 13);
			MinimizeToTrayCheckBox.Margin = new Padding(5, 6, 5, 6);
			MinimizeToTrayCheckBox.Name = "MinimizeToTrayCheckBox";
			MinimizeToTrayCheckBox.Size = new Size(229, 29);
			MinimizeToTrayCheckBox.TabIndex = 18;
			MinimizeToTrayCheckBox.Text = LocalizationManager.GetString("MinimizeToTrayCheckBoxText");
			MinimizeToTrayCheckBox.UseVisualStyleBackColor = true;
			MinimizeToTrayCheckBox.CheckedChanged += OptionChanged_Handler;
			// 
			// ThumbnailTabPage
			// 
			ThumbnailTabPage.BackColor = SystemColors.Control;
			ThumbnailTabPage.Controls.Add(ThumbnailSettingsPanel);
			ThumbnailTabPage.Location = new Point(124, 4);
			ThumbnailTabPage.Margin = new Padding(5, 6, 5, 6);
			ThumbnailTabPage.Name = "ThumbnailTabPage";
			ThumbnailTabPage.Padding = new Padding(5, 6, 5, 6);
			ThumbnailTabPage.Size = new Size(522, 411);
			ThumbnailTabPage.TabIndex = 1;
			ThumbnailTabPage.Text = LocalizationManager.GetString("ThumbnailTabText");
			// 
			// ThumbnailSettingsPanel
			// 
			ThumbnailSettingsPanel.BorderStyle = BorderStyle.FixedSingle;
			ThumbnailSettingsPanel.Controls.Add(ThumbnailSnapToGridCheckBox);
			ThumbnailSettingsPanel.Controls.Add(ThumbnailSnapToGridSizeYNumericEdit);
			ThumbnailSettingsPanel.Controls.Add(SnapYLabel);
			ThumbnailSettingsPanel.Controls.Add(ThumbnailSnapToGridSizeXNumericEdit);
			ThumbnailSettingsPanel.Controls.Add(SnapXLabel);
			ThumbnailSettingsPanel.Controls.Add(LockThumbnailLocationCheckbox);
			ThumbnailSettingsPanel.Controls.Add(HeightLabel);
			ThumbnailSettingsPanel.Controls.Add(WidthLabel);
			ThumbnailSettingsPanel.Controls.Add(ThumbnailsWidthNumericEdit);
			ThumbnailSettingsPanel.Controls.Add(ThumbnailsHeightNumericEdit);
			ThumbnailSettingsPanel.Controls.Add(ThumbnailOpacityTrackBar);
			ThumbnailSettingsPanel.Controls.Add(OpacityLabel);
			ThumbnailSettingsPanel.Dock = DockStyle.Fill;
			ThumbnailSettingsPanel.Location = new Point(5, 6);
			ThumbnailSettingsPanel.Margin = new Padding(5, 6, 5, 6);
			ThumbnailSettingsPanel.Name = "ThumbnailSettingsPanel";
			ThumbnailSettingsPanel.Size = new Size(512, 399);
			ThumbnailSettingsPanel.TabIndex = 19;
			// 
			// ThumbnailSnapToGridCheckBox
			// 
			ThumbnailSnapToGridCheckBox.AutoSize = true;
			ThumbnailSnapToGridCheckBox.Location = new Point(18, 200);
			ThumbnailSnapToGridCheckBox.Margin = new Padding(5, 6, 5, 6);
			ThumbnailSnapToGridCheckBox.Name = "ThumbnailSnapToGridCheckBox";
			ThumbnailSnapToGridCheckBox.Size = new Size(226, 29);
			ThumbnailSnapToGridCheckBox.TabIndex = 32;
			ThumbnailSnapToGridCheckBox.Text = LocalizationManager.GetString("ThumbnailSnapToGridCheckBoxText");
			ThumbnailSnapToGridCheckBox.UseVisualStyleBackColor = true;
			ThumbnailSnapToGridCheckBox.CheckedChanged += OptionChanged_Handler;
			// 
			// ThumbnailSnapToGridSizeYNumericEdit
			// 
			ThumbnailSnapToGridSizeYNumericEdit.BackColor = SystemColors.Window;
			ThumbnailSnapToGridSizeYNumericEdit.BorderStyle = BorderStyle.FixedSingle;
			ThumbnailSnapToGridSizeYNumericEdit.CausesValidation = false;
			ThumbnailSnapToGridSizeYNumericEdit.Increment = new decimal(new int[] { 10, 0, 0, 0 });
			ThumbnailSnapToGridSizeYNumericEdit.Location = new Point(217, 235);
			ThumbnailSnapToGridSizeYNumericEdit.Margin = new Padding(5, 6, 5, 6);
			ThumbnailSnapToGridSizeYNumericEdit.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
			ThumbnailSnapToGridSizeYNumericEdit.Name = "ThumbnailSnapToGridSizeYNumericEdit";
			ThumbnailSnapToGridSizeYNumericEdit.Size = new Size(80, 31);
			ThumbnailSnapToGridSizeYNumericEdit.TabIndex = 31;
			ThumbnailSnapToGridSizeYNumericEdit.Value = new decimal(new int[] { 100, 0, 0, 0 });
			ThumbnailSnapToGridSizeYNumericEdit.ValueChanged += OptionChanged_Handler;
			// 
			// SnapYLabel
			// 
			SnapYLabel.AutoSize = true;
			SnapYLabel.Location = new Point(183, 238);
			SnapYLabel.Margin = new Padding(5, 0, 5, 0);
			SnapYLabel.Name = "SnapYLabel";
			SnapYLabel.Size = new Size(22, 25);
			SnapYLabel.TabIndex = 30;
			SnapYLabel.Text = "Y";
			// 
			// ThumbnailSnapToGridSizeXNumericEdit
			// 
			ThumbnailSnapToGridSizeXNumericEdit.BackColor = SystemColors.Window;
			ThumbnailSnapToGridSizeXNumericEdit.BorderStyle = BorderStyle.FixedSingle;
			ThumbnailSnapToGridSizeXNumericEdit.CausesValidation = false;
			ThumbnailSnapToGridSizeXNumericEdit.Increment = new decimal(new int[] { 10, 0, 0, 0 });
			ThumbnailSnapToGridSizeXNumericEdit.Location = new Point(93, 235);
			ThumbnailSnapToGridSizeXNumericEdit.Margin = new Padding(5, 6, 5, 6);
			ThumbnailSnapToGridSizeXNumericEdit.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
			ThumbnailSnapToGridSizeXNumericEdit.Name = "ThumbnailSnapToGridSizeXNumericEdit";
			ThumbnailSnapToGridSizeXNumericEdit.Size = new Size(80, 31);
			ThumbnailSnapToGridSizeXNumericEdit.TabIndex = 29;
			ThumbnailSnapToGridSizeXNumericEdit.Value = new decimal(new int[] { 100, 0, 0, 0 });
			ThumbnailSnapToGridSizeXNumericEdit.ValueChanged += OptionChanged_Handler;
			// 
			// SnapXLabel
			// 
			SnapXLabel.AutoSize = true;
			SnapXLabel.Location = new Point(13, 238);
			SnapXLabel.Margin = new Padding(5, 0, 5, 0);
			SnapXLabel.Name = "SnapXLabel";
			SnapXLabel.Size = new Size(68, 25);
			SnapXLabel.TabIndex = 28;
			SnapXLabel.Text = "Snap X";
			// 
			// LockThumbnailLocationCheckbox
			// 
			LockThumbnailLocationCheckbox.AutoSize = true;
			LockThumbnailLocationCheckbox.Location = new Point(18, 156);
			LockThumbnailLocationCheckbox.Margin = new Padding(5, 6, 5, 6);
			LockThumbnailLocationCheckbox.Name = "LockThumbnailLocationCheckbox";
			LockThumbnailLocationCheckbox.Size = new Size(234, 29);
			LockThumbnailLocationCheckbox.TabIndex = 26;
			LockThumbnailLocationCheckbox.Text = LocalizationManager.GetString("LockThumbnailLocationCheckboxText");
			LockThumbnailLocationCheckbox.UseVisualStyleBackColor = true;
			LockThumbnailLocationCheckbox.CheckedChanged += OptionChanged_Handler;
			// 
			// HeightLabel
			// 
			HeightLabel.AutoSize = true;
			HeightLabel.Location = new Point(13, 110);
			HeightLabel.Margin = new Padding(5, 0, 5, 0);
			HeightLabel.Name = "HeightLabel";
			HeightLabel.Size = new Size(153, 25);
			HeightLabel.TabIndex = 24;
			HeightLabel.Text = LocalizationManager.GetString("ThumbnailHeightLabelText");
			// 
			// WidthLabel
			// 
			WidthLabel.AutoSize = true;
			WidthLabel.Location = new Point(13, 63);
			WidthLabel.Margin = new Padding(5, 0, 5, 0);
			WidthLabel.Name = "WidthLabel";
			WidthLabel.Size = new Size(148, 25);
			WidthLabel.TabIndex = 23;
			WidthLabel.Text = LocalizationManager.GetString("ThumbnailWidthLabelText");
			// 
			// ThumbnailsWidthNumericEdit
			// 
			ThumbnailsWidthNumericEdit.BackColor = SystemColors.Window;
			ThumbnailsWidthNumericEdit.BorderStyle = BorderStyle.FixedSingle;
			ThumbnailsWidthNumericEdit.CausesValidation = false;
			ThumbnailsWidthNumericEdit.Increment = new decimal(new int[] { 10, 0, 0, 0 });
			ThumbnailsWidthNumericEdit.Location = new Point(175, 60);
			ThumbnailsWidthNumericEdit.Margin = new Padding(5, 6, 5, 6);
			ThumbnailsWidthNumericEdit.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
			ThumbnailsWidthNumericEdit.Name = "ThumbnailsWidthNumericEdit";
			ThumbnailsWidthNumericEdit.Size = new Size(80, 31);
			ThumbnailsWidthNumericEdit.TabIndex = 21;
			ThumbnailsWidthNumericEdit.Value = new decimal(new int[] { 100, 0, 0, 0 });
			ThumbnailsWidthNumericEdit.ValueChanged += ThumbnailSizeChanged_Handler;
			// 
			// ThumbnailsHeightNumericEdit
			// 
			ThumbnailsHeightNumericEdit.BackColor = SystemColors.Window;
			ThumbnailsHeightNumericEdit.BorderStyle = BorderStyle.FixedSingle;
			ThumbnailsHeightNumericEdit.CausesValidation = false;
			ThumbnailsHeightNumericEdit.Increment = new decimal(new int[] { 10, 0, 0, 0 });
			ThumbnailsHeightNumericEdit.Location = new Point(175, 106);
			ThumbnailsHeightNumericEdit.Margin = new Padding(5, 6, 5, 6);
			ThumbnailsHeightNumericEdit.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
			ThumbnailsHeightNumericEdit.Name = "ThumbnailsHeightNumericEdit";
			ThumbnailsHeightNumericEdit.Size = new Size(80, 31);
			ThumbnailsHeightNumericEdit.TabIndex = 22;
			ThumbnailsHeightNumericEdit.Value = new decimal(new int[] { 70, 0, 0, 0 });
			ThumbnailsHeightNumericEdit.ValueChanged += ThumbnailSizeChanged_Handler;
			// 
			// ThumbnailOpacityTrackBar
			// 
			ThumbnailOpacityTrackBar.AutoSize = false;
			ThumbnailOpacityTrackBar.LargeChange = 10;
			ThumbnailOpacityTrackBar.Location = new Point(102, 12);
			ThumbnailOpacityTrackBar.Margin = new Padding(5, 6, 5, 6);
			ThumbnailOpacityTrackBar.Maximum = 100;
			ThumbnailOpacityTrackBar.Minimum = 20;
			ThumbnailOpacityTrackBar.Name = "ThumbnailOpacityTrackBar";
			ThumbnailOpacityTrackBar.Size = new Size(318, 42);
			ThumbnailOpacityTrackBar.TabIndex = 20;
			ThumbnailOpacityTrackBar.TickFrequency = 10;
			ThumbnailOpacityTrackBar.Value = 20;
			ThumbnailOpacityTrackBar.ValueChanged += OptionChanged_Handler;
			// 
			// OpacityLabel
			// 
			OpacityLabel.AutoSize = true;
			OpacityLabel.Location = new Point(13, 17);
			OpacityLabel.Margin = new Padding(5, 0, 5, 0);
			OpacityLabel.Name = "OpacityLabel";
			OpacityLabel.Size = new Size(73, 25);
			OpacityLabel.TabIndex = 19;
			OpacityLabel.Text = LocalizationManager.GetString("OpacityLabelText");
			// 
			// ZoomTabPage
			// 
			ZoomTabPage.BackColor = SystemColors.Control;
			ZoomTabPage.Controls.Add(ZoomSettingsPanel);
			ZoomTabPage.Location = new Point(124, 4);
			ZoomTabPage.Margin = new Padding(5, 6, 5, 6);
			ZoomTabPage.Name = "ZoomTabPage";
			ZoomTabPage.Size = new Size(522, 411);
			ZoomTabPage.TabIndex = 2;
			ZoomTabPage.Text = LocalizationManager.GetString("ZoomTabText");
			// 
			// ZoomSettingsPanel
			// 
			ZoomSettingsPanel.BorderStyle = BorderStyle.FixedSingle;
			ZoomSettingsPanel.Controls.Add(ZoomFactorLabel);
			ZoomSettingsPanel.Controls.Add(ZoomAnchorPanel);
			ZoomSettingsPanel.Controls.Add(ZoomAnchorLabel);
			ZoomSettingsPanel.Controls.Add(EnableThumbnailZoomCheckBox);
			ZoomSettingsPanel.Controls.Add(ThumbnailZoomFactorNumericEdit);
			ZoomSettingsPanel.Dock = DockStyle.Fill;
			ZoomSettingsPanel.Location = new Point(0, 0);
			ZoomSettingsPanel.Margin = new Padding(5, 6, 5, 6);
			ZoomSettingsPanel.Name = "ZoomSettingsPanel";
			ZoomSettingsPanel.Size = new Size(522, 411);
			ZoomSettingsPanel.TabIndex = 36;
			// 
			// ZoomFactorLabel
			// 
			ZoomFactorLabel.AutoSize = true;
			ZoomFactorLabel.Location = new Point(13, 63);
			ZoomFactorLabel.Margin = new Padding(5, 0, 5, 0);
			ZoomFactorLabel.Name = "ZoomFactorLabel";
			ZoomFactorLabel.Size = new Size(113, 25);
			ZoomFactorLabel.TabIndex = 39;
			ZoomFactorLabel.Text = LocalizationManager.GetString("ZoomFactorLabelText");
			// 
			// ZoomAnchorPanel
			// 
			ZoomAnchorPanel.BorderStyle = BorderStyle.FixedSingle;
			ZoomAnchorPanel.Controls.Add(ZoomAanchorNWRadioButton);
			ZoomAnchorPanel.Controls.Add(ZoomAanchorNRadioButton);
			ZoomAnchorPanel.Controls.Add(ZoomAanchorNERadioButton);
			ZoomAnchorPanel.Controls.Add(ZoomAanchorWRadioButton);
			ZoomAnchorPanel.Controls.Add(ZoomAanchorSERadioButton);
			ZoomAnchorPanel.Controls.Add(ZoomAanchorCRadioButton);
			ZoomAnchorPanel.Controls.Add(ZoomAanchorSRadioButton);
			ZoomAnchorPanel.Controls.Add(ZoomAanchorERadioButton);
			ZoomAnchorPanel.Controls.Add(ZoomAanchorSWRadioButton);
			ZoomAnchorPanel.Location = new Point(135, 104);
			ZoomAnchorPanel.Margin = new Padding(5, 6, 5, 6);
			ZoomAnchorPanel.Name = "ZoomAnchorPanel";
			ZoomAnchorPanel.Size = new Size(127, 139);
			ZoomAnchorPanel.TabIndex = 38;
			// 
			// ZoomAanchorNWRadioButton
			// 
			ZoomAanchorNWRadioButton.AutoSize = true;
			ZoomAanchorNWRadioButton.Location = new Point(5, 6);
			ZoomAanchorNWRadioButton.Margin = new Padding(5, 6, 5, 6);
			ZoomAanchorNWRadioButton.Name = "ZoomAanchorNWRadioButton";
			ZoomAanchorNWRadioButton.Size = new Size(21, 20);
			ZoomAanchorNWRadioButton.TabIndex = 0;
			ZoomAanchorNWRadioButton.TabStop = true;
			ZoomAanchorNWRadioButton.UseVisualStyleBackColor = true;
			ZoomAanchorNWRadioButton.CheckedChanged += OptionChanged_Handler;
			// 
			// ZoomAanchorNRadioButton
			// 
			ZoomAanchorNRadioButton.AutoSize = true;
			ZoomAanchorNRadioButton.Location = new Point(52, 6);
			ZoomAanchorNRadioButton.Margin = new Padding(5, 6, 5, 6);
			ZoomAanchorNRadioButton.Name = "ZoomAanchorNRadioButton";
			ZoomAanchorNRadioButton.Size = new Size(21, 20);
			ZoomAanchorNRadioButton.TabIndex = 1;
			ZoomAanchorNRadioButton.TabStop = true;
			ZoomAanchorNRadioButton.UseVisualStyleBackColor = true;
			ZoomAanchorNRadioButton.CheckedChanged += OptionChanged_Handler;
			// 
			// ZoomAanchorNERadioButton
			// 
			ZoomAanchorNERadioButton.AutoSize = true;
			ZoomAanchorNERadioButton.Location = new Point(98, 6);
			ZoomAanchorNERadioButton.Margin = new Padding(5, 6, 5, 6);
			ZoomAanchorNERadioButton.Name = "ZoomAanchorNERadioButton";
			ZoomAanchorNERadioButton.Size = new Size(21, 20);
			ZoomAanchorNERadioButton.TabIndex = 2;
			ZoomAanchorNERadioButton.TabStop = true;
			ZoomAanchorNERadioButton.UseVisualStyleBackColor = true;
			ZoomAanchorNERadioButton.CheckedChanged += OptionChanged_Handler;
			// 
			// ZoomAanchorWRadioButton
			// 
			ZoomAanchorWRadioButton.AutoSize = true;
			ZoomAanchorWRadioButton.Location = new Point(5, 56);
			ZoomAanchorWRadioButton.Margin = new Padding(5, 6, 5, 6);
			ZoomAanchorWRadioButton.Name = "ZoomAanchorWRadioButton";
			ZoomAanchorWRadioButton.Size = new Size(21, 20);
			ZoomAanchorWRadioButton.TabIndex = 3;
			ZoomAanchorWRadioButton.TabStop = true;
			ZoomAanchorWRadioButton.UseVisualStyleBackColor = true;
			ZoomAanchorWRadioButton.CheckedChanged += OptionChanged_Handler;
			// 
			// ZoomAanchorSERadioButton
			// 
			ZoomAanchorSERadioButton.AutoSize = true;
			ZoomAanchorSERadioButton.Location = new Point(98, 106);
			ZoomAanchorSERadioButton.Margin = new Padding(5, 6, 5, 6);
			ZoomAanchorSERadioButton.Name = "ZoomAanchorSERadioButton";
			ZoomAanchorSERadioButton.Size = new Size(21, 20);
			ZoomAanchorSERadioButton.TabIndex = 8;
			ZoomAanchorSERadioButton.TabStop = true;
			ZoomAanchorSERadioButton.UseVisualStyleBackColor = true;
			ZoomAanchorSERadioButton.CheckedChanged += OptionChanged_Handler;
			// 
			// ZoomAanchorCRadioButton
			// 
			ZoomAanchorCRadioButton.AutoSize = true;
			ZoomAanchorCRadioButton.Location = new Point(52, 56);
			ZoomAanchorCRadioButton.Margin = new Padding(5, 6, 5, 6);
			ZoomAanchorCRadioButton.Name = "ZoomAanchorCRadioButton";
			ZoomAanchorCRadioButton.Size = new Size(21, 20);
			ZoomAanchorCRadioButton.TabIndex = 4;
			ZoomAanchorCRadioButton.TabStop = true;
			ZoomAanchorCRadioButton.UseVisualStyleBackColor = true;
			ZoomAanchorCRadioButton.CheckedChanged += OptionChanged_Handler;
			// 
			// ZoomAanchorSRadioButton
			// 
			ZoomAanchorSRadioButton.AutoSize = true;
			ZoomAanchorSRadioButton.Location = new Point(52, 106);
			ZoomAanchorSRadioButton.Margin = new Padding(5, 6, 5, 6);
			ZoomAanchorSRadioButton.Name = "ZoomAanchorSRadioButton";
			ZoomAanchorSRadioButton.Size = new Size(21, 20);
			ZoomAanchorSRadioButton.TabIndex = 7;
			ZoomAanchorSRadioButton.TabStop = true;
			ZoomAanchorSRadioButton.UseVisualStyleBackColor = true;
			ZoomAanchorSRadioButton.CheckedChanged += OptionChanged_Handler;
			// 
			// ZoomAanchorERadioButton
			// 
			ZoomAanchorERadioButton.AutoSize = true;
			ZoomAanchorERadioButton.Location = new Point(98, 56);
			ZoomAanchorERadioButton.Margin = new Padding(5, 6, 5, 6);
			ZoomAanchorERadioButton.Name = "ZoomAanchorERadioButton";
			ZoomAanchorERadioButton.Size = new Size(21, 20);
			ZoomAanchorERadioButton.TabIndex = 5;
			ZoomAanchorERadioButton.TabStop = true;
			ZoomAanchorERadioButton.UseVisualStyleBackColor = true;
			ZoomAanchorERadioButton.CheckedChanged += OptionChanged_Handler;
			// 
			// ZoomAanchorSWRadioButton
			// 
			ZoomAanchorSWRadioButton.AutoSize = true;
			ZoomAanchorSWRadioButton.Location = new Point(5, 106);
			ZoomAanchorSWRadioButton.Margin = new Padding(5, 6, 5, 6);
			ZoomAanchorSWRadioButton.Name = "ZoomAanchorSWRadioButton";
			ZoomAanchorSWRadioButton.Size = new Size(21, 20);
			ZoomAanchorSWRadioButton.TabIndex = 6;
			ZoomAanchorSWRadioButton.TabStop = true;
			ZoomAanchorSWRadioButton.UseVisualStyleBackColor = true;
			ZoomAanchorSWRadioButton.CheckedChanged += OptionChanged_Handler;
			// 
			// ZoomAnchorLabel
			// 
			ZoomAnchorLabel.AutoSize = true;
			ZoomAnchorLabel.Location = new Point(13, 110);
			ZoomAnchorLabel.Margin = new Padding(5, 0, 5, 0);
			ZoomAnchorLabel.Name = "ZoomAnchorLabel";
			ZoomAnchorLabel.Size = new Size(69, 25);
			ZoomAnchorLabel.TabIndex = 40;
			ZoomAnchorLabel.Text = LocalizationManager.GetString("ZoomAnchorLabelText");
			// 
			// EnableThumbnailZoomCheckBox
			// 
			EnableThumbnailZoomCheckBox.AutoSize = true;
			EnableThumbnailZoomCheckBox.Checked = true;
			EnableThumbnailZoomCheckBox.CheckState = CheckState.Checked;
			EnableThumbnailZoomCheckBox.Location = new Point(13, 13);
			EnableThumbnailZoomCheckBox.Margin = new Padding(5, 6, 5, 6);
			EnableThumbnailZoomCheckBox.Name = "EnableThumbnailZoomCheckBox";
			EnableThumbnailZoomCheckBox.RightToLeft = RightToLeft.No;
			EnableThumbnailZoomCheckBox.Size = new Size(162, 29);
			EnableThumbnailZoomCheckBox.TabIndex = 36;
			EnableThumbnailZoomCheckBox.Text = LocalizationManager.GetString("EnableThumbnailZoomCheckBoxText");
			EnableThumbnailZoomCheckBox.UseVisualStyleBackColor = true;
			EnableThumbnailZoomCheckBox.CheckedChanged += OptionChanged_Handler;
			// 
			// ThumbnailZoomFactorNumericEdit
			// 
			ThumbnailZoomFactorNumericEdit.BackColor = SystemColors.Window;
			ThumbnailZoomFactorNumericEdit.BorderStyle = BorderStyle.FixedSingle;
			ThumbnailZoomFactorNumericEdit.Location = new Point(135, 60);
			ThumbnailZoomFactorNumericEdit.Margin = new Padding(5, 6, 5, 6);
			ThumbnailZoomFactorNumericEdit.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
			ThumbnailZoomFactorNumericEdit.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
			ThumbnailZoomFactorNumericEdit.Name = "ThumbnailZoomFactorNumericEdit";
			ThumbnailZoomFactorNumericEdit.Size = new Size(63, 31);
			ThumbnailZoomFactorNumericEdit.TabIndex = 37;
			ThumbnailZoomFactorNumericEdit.Value = new decimal(new int[] { 2, 0, 0, 0 });
			ThumbnailZoomFactorNumericEdit.ValueChanged += OptionChanged_Handler;
			// 
			// OverlayTabPage
			// 
			OverlayTabPage.BackColor = SystemColors.Control;
			OverlayTabPage.Controls.Add(OverlaySettingsPanel);
			OverlayTabPage.Location = new Point(124, 4);
			OverlayTabPage.Margin = new Padding(5, 6, 5, 6);
			OverlayTabPage.Name = "OverlayTabPage";
			OverlayTabPage.Size = new Size(522, 411);
			OverlayTabPage.TabIndex = 3;
			OverlayTabPage.Text = LocalizationManager.GetString("OverlayTabText");
			// 
			// OverlaySettingsPanel
			// 
			OverlaySettingsPanel.BorderStyle = BorderStyle.FixedSingle;
			OverlaySettingsPanel.Controls.Add(label3);
			OverlaySettingsPanel.Controls.Add(label2);
			OverlaySettingsPanel.Controls.Add(OverlayLabelColorButton);
			OverlaySettingsPanel.Controls.Add(OverlayLabelSizeNumericEdit);
			OverlaySettingsPanel.Controls.Add(panel1);
			OverlaySettingsPanel.Controls.Add(label1);
			OverlaySettingsPanel.Controls.Add(HighlightColorLabel);
			OverlaySettingsPanel.Controls.Add(ActiveClientHighlightColorButton);
			OverlaySettingsPanel.Controls.Add(EnableActiveClientHighlightCheckBox);
			OverlaySettingsPanel.Controls.Add(ShowThumbnailOverlaysCheckBox);
			OverlaySettingsPanel.Controls.Add(ShowThumbnailFramesCheckBox);
			OverlaySettingsPanel.Dock = DockStyle.Fill;
			OverlaySettingsPanel.Location = new Point(0, 0);
			OverlaySettingsPanel.Margin = new Padding(5, 6, 5, 6);
			OverlaySettingsPanel.Name = "OverlaySettingsPanel";
			OverlaySettingsPanel.Size = new Size(522, 411);
			OverlaySettingsPanel.TabIndex = 25;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(8, 281);
			label3.Margin = new Padding(5, 0, 5, 0);
			label3.Name = "label3";
			label3.Size = new Size(75, 25);
			label3.TabIndex = 43;
			label3.Text = LocalizationManager.GetString("PositionLabelText");
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(8, 244);
			label2.Margin = new Padding(5, 0, 5, 0);
			label2.Name = "label2";
			label2.Size = new Size(55, 25);
			label2.TabIndex = 42;
			label2.Text = LocalizationManager.GetString("ColorLabelText");
			// 
			// OverlayLabelColorButton
			// 
			OverlayLabelColorButton.BorderStyle = BorderStyle.FixedSingle;
			OverlayLabelColorButton.Location = new Point(70, 242);
			OverlayLabelColorButton.Margin = new Padding(5, 6, 5, 6);
			OverlayLabelColorButton.Name = "OverlayLabelColorButton";
			OverlayLabelColorButton.Size = new Size(154, 31);
			OverlayLabelColorButton.TabIndex = 41;
			OverlayLabelColorButton.Click += OverlayLabelColorButton_Click;
			// 
			// OverlayLabelSizeNumericEdit
			// 
			OverlayLabelSizeNumericEdit.BackColor = SystemColors.Window;
			OverlayLabelSizeNumericEdit.BorderStyle = BorderStyle.FixedSingle;
			OverlayLabelSizeNumericEdit.CausesValidation = false;
			OverlayLabelSizeNumericEdit.Location = new Point(145, 192);
			OverlayLabelSizeNumericEdit.Margin = new Padding(5, 6, 5, 6);
			OverlayLabelSizeNumericEdit.Maximum = new decimal(new int[] { 30, 0, 0, 0 });
			OverlayLabelSizeNumericEdit.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
			OverlayLabelSizeNumericEdit.Name = "OverlayLabelSizeNumericEdit";
			OverlayLabelSizeNumericEdit.Size = new Size(80, 31);
			OverlayLabelSizeNumericEdit.TabIndex = 40;
			OverlayLabelSizeNumericEdit.Value = new decimal(new int[] { 30, 0, 0, 0 });
			OverlayLabelSizeNumericEdit.ValueChanged += OptionChanged_Handler;
			// 
			// panel1
			// 
			panel1.BorderStyle = BorderStyle.FixedSingle;
			panel1.Controls.Add(OverlayLabelNWRadioButton);
			panel1.Controls.Add(OverlayLabelNRadioButton);
			panel1.Controls.Add(OverlayLabelNERadioButton);
			panel1.Controls.Add(OverlayLabelWRadioButton);
			panel1.Controls.Add(OverlayLabelSERadioButton);
			panel1.Controls.Add(OverlayLabelCRadioButton);
			panel1.Controls.Add(OverlayLabelSRadioButton);
			panel1.Controls.Add(OverlayLabelERadioButton);
			panel1.Controls.Add(OverlayLabelSWRadioButton);
			panel1.Location = new Point(120, 287);
			panel1.Margin = new Padding(5, 6, 5, 6);
			panel1.Name = "panel1";
			panel1.Size = new Size(104, 108);
			panel1.TabIndex = 39;
			// 
			// OverlayLabelNWRadioButton
			// 
			OverlayLabelNWRadioButton.AutoSize = true;
			OverlayLabelNWRadioButton.Location = new Point(5, 6);
			OverlayLabelNWRadioButton.Margin = new Padding(5, 6, 5, 6);
			OverlayLabelNWRadioButton.Name = "OverlayLabelNWRadioButton";
			OverlayLabelNWRadioButton.Size = new Size(21, 20);
			OverlayLabelNWRadioButton.TabIndex = 0;
			OverlayLabelNWRadioButton.TabStop = true;
			OverlayLabelNWRadioButton.UseVisualStyleBackColor = true;
			OverlayLabelNWRadioButton.CheckedChanged += OptionChanged_Handler;
			// 
			// OverlayLabelNRadioButton
			// 
			OverlayLabelNRadioButton.AutoSize = true;
			OverlayLabelNRadioButton.Location = new Point(38, 6);
			OverlayLabelNRadioButton.Margin = new Padding(5, 6, 5, 6);
			OverlayLabelNRadioButton.Name = "OverlayLabelNRadioButton";
			OverlayLabelNRadioButton.Size = new Size(21, 20);
			OverlayLabelNRadioButton.TabIndex = 1;
			OverlayLabelNRadioButton.TabStop = true;
			OverlayLabelNRadioButton.UseVisualStyleBackColor = true;
			OverlayLabelNRadioButton.CheckedChanged += OptionChanged_Handler;
			// 
			// OverlayLabelNERadioButton
			// 
			OverlayLabelNERadioButton.AutoSize = true;
			OverlayLabelNERadioButton.Location = new Point(72, 6);
			OverlayLabelNERadioButton.Margin = new Padding(5, 6, 5, 6);
			OverlayLabelNERadioButton.Name = "OverlayLabelNERadioButton";
			OverlayLabelNERadioButton.Size = new Size(21, 20);
			OverlayLabelNERadioButton.TabIndex = 2;
			OverlayLabelNERadioButton.TabStop = true;
			OverlayLabelNERadioButton.UseVisualStyleBackColor = true;
			OverlayLabelNERadioButton.CheckedChanged += OptionChanged_Handler;
			// 
			// OverlayLabelWRadioButton
			// 
			OverlayLabelWRadioButton.AutoSize = true;
			OverlayLabelWRadioButton.Location = new Point(5, 42);
			OverlayLabelWRadioButton.Margin = new Padding(5, 6, 5, 6);
			OverlayLabelWRadioButton.Name = "OverlayLabelWRadioButton";
			OverlayLabelWRadioButton.Size = new Size(21, 20);
			OverlayLabelWRadioButton.TabIndex = 3;
			OverlayLabelWRadioButton.TabStop = true;
			OverlayLabelWRadioButton.UseVisualStyleBackColor = true;
			OverlayLabelWRadioButton.CheckedChanged += OptionChanged_Handler;
			// 
			// OverlayLabelSERadioButton
			// 
			OverlayLabelSERadioButton.AutoSize = true;
			OverlayLabelSERadioButton.Location = new Point(72, 77);
			OverlayLabelSERadioButton.Margin = new Padding(5, 6, 5, 6);
			OverlayLabelSERadioButton.Name = "OverlayLabelSERadioButton";
			OverlayLabelSERadioButton.Size = new Size(21, 20);
			OverlayLabelSERadioButton.TabIndex = 8;
			OverlayLabelSERadioButton.TabStop = true;
			OverlayLabelSERadioButton.UseVisualStyleBackColor = true;
			OverlayLabelSERadioButton.CheckedChanged += OptionChanged_Handler;
			// 
			// OverlayLabelCRadioButton
			// 
			OverlayLabelCRadioButton.AutoSize = true;
			OverlayLabelCRadioButton.Location = new Point(38, 42);
			OverlayLabelCRadioButton.Margin = new Padding(5, 6, 5, 6);
			OverlayLabelCRadioButton.Name = "OverlayLabelCRadioButton";
			OverlayLabelCRadioButton.Size = new Size(21, 20);
			OverlayLabelCRadioButton.TabIndex = 4;
			OverlayLabelCRadioButton.TabStop = true;
			OverlayLabelCRadioButton.UseVisualStyleBackColor = true;
			OverlayLabelCRadioButton.CheckedChanged += OptionChanged_Handler;
			// 
			// OverlayLabelSRadioButton
			// 
			OverlayLabelSRadioButton.AutoSize = true;
			OverlayLabelSRadioButton.Location = new Point(38, 77);
			OverlayLabelSRadioButton.Margin = new Padding(5, 6, 5, 6);
			OverlayLabelSRadioButton.Name = "OverlayLabelSRadioButton";
			OverlayLabelSRadioButton.Size = new Size(21, 20);
			OverlayLabelSRadioButton.TabIndex = 7;
			OverlayLabelSRadioButton.TabStop = true;
			OverlayLabelSRadioButton.UseVisualStyleBackColor = true;
			OverlayLabelSRadioButton.CheckedChanged += OptionChanged_Handler;
			// 
			// OverlayLabelERadioButton
			// 
			OverlayLabelERadioButton.AutoSize = true;
			OverlayLabelERadioButton.Location = new Point(72, 42);
			OverlayLabelERadioButton.Margin = new Padding(5, 6, 5, 6);
			OverlayLabelERadioButton.Name = "OverlayLabelERadioButton";
			OverlayLabelERadioButton.Size = new Size(21, 20);
			OverlayLabelERadioButton.TabIndex = 5;
			OverlayLabelERadioButton.TabStop = true;
			OverlayLabelERadioButton.UseVisualStyleBackColor = true;
			OverlayLabelERadioButton.CheckedChanged += OptionChanged_Handler;
			// 
			// OverlayLabelSWRadioButton
			// 
			OverlayLabelSWRadioButton.AutoSize = true;
			OverlayLabelSWRadioButton.Location = new Point(5, 77);
			OverlayLabelSWRadioButton.Margin = new Padding(5, 6, 5, 6);
			OverlayLabelSWRadioButton.Name = "OverlayLabelSWRadioButton";
			OverlayLabelSWRadioButton.Size = new Size(21, 20);
			OverlayLabelSWRadioButton.TabIndex = 6;
			OverlayLabelSWRadioButton.TabStop = true;
			OverlayLabelSWRadioButton.UseVisualStyleBackColor = true;
			OverlayLabelSWRadioButton.CheckedChanged += OptionChanged_Handler;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(8, 196);
			label1.Margin = new Padding(5, 0, 5, 0);
			label1.Name = "label1";
			label1.Size = new Size(89, 25);
			label1.TabIndex = 30;
			label1.Text = LocalizationManager.GetString("LabelSizeLabelText");
			// 
			// HighlightColorLabel
			// 
			HighlightColorLabel.AutoSize = true;
			HighlightColorLabel.Location = new Point(8, 150);
			HighlightColorLabel.Margin = new Padding(5, 0, 5, 0);
			HighlightColorLabel.Name = "HighlightColorLabel";
			HighlightColorLabel.Size = new Size(55, 25);
			HighlightColorLabel.TabIndex = 29;
			HighlightColorLabel.Text = LocalizationManager.GetString("HighlightColorLabelText");
			// 
			// ActiveClientHighlightColorButton
			// 
			ActiveClientHighlightColorButton.BorderStyle = BorderStyle.FixedSingle;
			ActiveClientHighlightColorButton.Location = new Point(70, 148);
			ActiveClientHighlightColorButton.Margin = new Padding(5, 6, 5, 6);
			ActiveClientHighlightColorButton.Name = "ActiveClientHighlightColorButton";
			ActiveClientHighlightColorButton.Size = new Size(154, 31);
			ActiveClientHighlightColorButton.TabIndex = 28;
			ActiveClientHighlightColorButton.Click += ActiveClientHighlightColorButton_Click;
			// 
			// EnableActiveClientHighlightCheckBox
			// 
			EnableActiveClientHighlightCheckBox.AutoSize = true;
			EnableActiveClientHighlightCheckBox.Checked = true;
			EnableActiveClientHighlightCheckBox.CheckState = CheckState.Checked;
			EnableActiveClientHighlightCheckBox.Location = new Point(13, 106);
			EnableActiveClientHighlightCheckBox.Margin = new Padding(5, 6, 5, 6);
			EnableActiveClientHighlightCheckBox.Name = "EnableActiveClientHighlightCheckBox";
			EnableActiveClientHighlightCheckBox.RightToLeft = RightToLeft.No;
			EnableActiveClientHighlightCheckBox.Size = new Size(207, 29);
			EnableActiveClientHighlightCheckBox.TabIndex = 27;
			EnableActiveClientHighlightCheckBox.Text = LocalizationManager.GetString("EnableActiveClientHighlightCheckBoxText");
			EnableActiveClientHighlightCheckBox.UseVisualStyleBackColor = true;
			EnableActiveClientHighlightCheckBox.CheckedChanged += OptionChanged_Handler;
			// 
			// ShowThumbnailOverlaysCheckBox
			// 
			ShowThumbnailOverlaysCheckBox.AutoSize = true;
			ShowThumbnailOverlaysCheckBox.Checked = true;
			ShowThumbnailOverlaysCheckBox.CheckState = CheckState.Checked;
			ShowThumbnailOverlaysCheckBox.Location = new Point(13, 13);
			ShowThumbnailOverlaysCheckBox.Margin = new Padding(5, 6, 5, 6);
			ShowThumbnailOverlaysCheckBox.Name = "ShowThumbnailOverlaysCheckBox";
			ShowThumbnailOverlaysCheckBox.RightToLeft = RightToLeft.No;
			ShowThumbnailOverlaysCheckBox.Size = new Size(144, 29);
			ShowThumbnailOverlaysCheckBox.TabIndex = 25;
			ShowThumbnailOverlaysCheckBox.Text = LocalizationManager.GetString("ShowThumbnailOverlaysCheckBoxText");
			ShowThumbnailOverlaysCheckBox.UseVisualStyleBackColor = true;
			ShowThumbnailOverlaysCheckBox.CheckedChanged += OptionChanged_Handler;
			// 
			// ShowThumbnailFramesCheckBox
			// 
			ShowThumbnailFramesCheckBox.AutoSize = true;
			ShowThumbnailFramesCheckBox.Checked = true;
			ShowThumbnailFramesCheckBox.CheckState = CheckState.Checked;
			ShowThumbnailFramesCheckBox.Location = new Point(13, 60);
			ShowThumbnailFramesCheckBox.Margin = new Padding(5, 6, 5, 6);
			ShowThumbnailFramesCheckBox.Name = "ShowThumbnailFramesCheckBox";
			ShowThumbnailFramesCheckBox.RightToLeft = RightToLeft.No;
			ShowThumbnailFramesCheckBox.Size = new Size(141, 29);
			ShowThumbnailFramesCheckBox.TabIndex = 26;
			ShowThumbnailFramesCheckBox.Text = LocalizationManager.GetString("ShowThumbnailFramesCheckBoxText");
			ShowThumbnailFramesCheckBox.UseVisualStyleBackColor = true;
			ShowThumbnailFramesCheckBox.CheckedChanged += OptionChanged_Handler;
			// 
			// ClientsTabPage
			// 
			ClientsTabPage.BackColor = SystemColors.Control;
			ClientsTabPage.Controls.Add(ClientsPanel);
			ClientsTabPage.Location = new Point(124, 4);
			ClientsTabPage.Margin = new Padding(5, 6, 5, 6);
			ClientsTabPage.Name = "ClientsTabPage";
			ClientsTabPage.Size = new Size(522, 411);
			ClientsTabPage.TabIndex = 4;
			ClientsTabPage.Text = LocalizationManager.GetString("ActiveClientsTabText");
			// 
			// ClientsPanel
			// 
			ClientsPanel.BorderStyle = BorderStyle.FixedSingle;
			ClientsPanel.Controls.Add(ThumbnailsList);
			ClientsPanel.Controls.Add(ThumbnailsListLabel);
			ClientsPanel.Dock = DockStyle.Fill;
			ClientsPanel.Location = new Point(0, 0);
			ClientsPanel.Margin = new Padding(5, 6, 5, 6);
			ClientsPanel.Name = "ClientsPanel";
			ClientsPanel.Size = new Size(522, 411);
			ClientsPanel.TabIndex = 32;
			// 
			// ThumbnailsList
			// 
			ThumbnailsList.BackColor = SystemColors.Window;
			ThumbnailsList.BorderStyle = BorderStyle.FixedSingle;
			ThumbnailsList.CheckOnClick = true;
			ThumbnailsList.Dock = DockStyle.Bottom;
			ThumbnailsList.FormattingEnabled = true;
			ThumbnailsList.IntegralHeight = false;
			ThumbnailsList.Location = new Point(0, 65);
			ThumbnailsList.Margin = new Padding(5, 6, 5, 6);
			ThumbnailsList.Name = "ThumbnailsList";
			ThumbnailsList.Size = new Size(520, 344);
			ThumbnailsList.TabIndex = 34;
			ThumbnailsList.ItemCheck += ThumbnailsList_ItemCheck_Handler;
			// 
			// ThumbnailsListLabel
			// 
			ThumbnailsListLabel.AutoSize = true;
			ThumbnailsListLabel.Location = new Point(13, 17);
			ThumbnailsListLabel.Margin = new Padding(5, 0, 5, 0);
			ThumbnailsListLabel.Name = "ThumbnailsListLabel";
			ThumbnailsListLabel.Size = new Size(268, 25);
			ThumbnailsListLabel.TabIndex = 33;
			ThumbnailsListLabel.Text = LocalizationManager.GetString("ThumbnailsListLabelText");
			// 
			// LanguageTabPage
			// 
			LanguageTabPage.BackColor = SystemColors.Control;
			LanguageTabPage.Controls.Add(LanguageSettingsPanel);
			LanguageTabPage.Location = new Point(124, 4);
			LanguageTabPage.Margin = new Padding(5, 6, 5, 6);
			LanguageTabPage.Name = "LanguageTabPage";
			LanguageTabPage.Size = new Size(522, 411);
			LanguageTabPage.TabIndex = 6;
			LanguageTabPage.Text = LocalizationManager.GetString("LanguageTabText");
			// 
			// LanguageSettingsPanel
			// 
			LanguageSettingsPanel.BorderStyle = BorderStyle.FixedSingle;
			LanguageSettingsPanel.Controls.Add(LanguageCombo);
			LanguageSettingsPanel.Controls.Add(LanguageLabel);
			LanguageSettingsPanel.Dock = DockStyle.Fill;
			LanguageSettingsPanel.Location = new Point(5, 6);
			LanguageSettingsPanel.Margin = new Padding(5, 6, 5, 6);
			LanguageSettingsPanel.Name = "LanguageSettingsPanel";
			LanguageSettingsPanel.Size = new Size(512, 399);
			LanguageSettingsPanel.TabIndex = 19;
			// 
			// LanguageLabel
			// 
			LanguageLabel.AutoSize = true;
			LanguageLabel.Location = new Point(23, 31);
			LanguageLabel.Margin = new Padding(5, 0, 5, 0);
			LanguageLabel.Name = "LanguageLabel";
			LanguageLabel.Size = new Size(92, 25);
			LanguageLabel.TabIndex = 1;
			LanguageLabel.Text = LocalizationManager.GetString("LanguageLabelText");
			// 
			// LanguageCombo
			// 
			LanguageCombo.DropDownStyle = ComboBoxStyle.DropDownList;
			LanguageCombo.FormattingEnabled = true;
			LanguageCombo.Location = new Point(150, 28);
			LanguageCombo.Margin = new Padding(5, 6, 5, 6);
			LanguageCombo.Name = "LanguageCombo";
			LanguageCombo.Size = new Size(251, 33);
			LanguageCombo.TabIndex = 0;
			LanguageCombo.SelectedIndexChanged += LanguageChanged_Handler;
			// 
			// AboutTabPage
			// 
			AboutTabPage.BackColor = SystemColors.Control;
			AboutTabPage.Controls.Add(AboutPanel);
			AboutTabPage.Location = new Point(124, 4);
			AboutTabPage.Margin = new Padding(5, 6, 5, 6);
			AboutTabPage.Name = "AboutTabPage";
			AboutTabPage.Size = new Size(522, 411);
			AboutTabPage.TabIndex = 5;
			AboutTabPage.Text = LocalizationManager.GetString("AboutTabText");
			// 
			// AboutPanel
			// 
			AboutPanel.BackColor = Color.Transparent;
			AboutPanel.BorderStyle = BorderStyle.FixedSingle;
			AboutPanel.Controls.Add(CreditMaintLabel);
			AboutPanel.Controls.Add(DocumentationLinkLabel);
			AboutPanel.Controls.Add(DescriptionLabel);
			AboutPanel.Controls.Add(VersionLabel);
			AboutPanel.Controls.Add(NameLabel);
			AboutPanel.Controls.Add(DocumentationLink);
			AboutPanel.Dock = DockStyle.Fill;
			AboutPanel.Location = new Point(0, 0);
			AboutPanel.Margin = new Padding(5, 6, 5, 6);
			AboutPanel.Name = "AboutPanel";
			AboutPanel.Size = new Size(522, 411);
			AboutPanel.TabIndex = 2;
			// 
			// CreditMaintLabel
			// 
			CreditMaintLabel.AutoSize = true;
			CreditMaintLabel.Location = new Point(0, 275);
			CreditMaintLabel.Margin = new Padding(5, 0, 5, 0);
			CreditMaintLabel.Name = "CreditMaintLabel";
			CreditMaintLabel.Padding = new Padding(13, 6, 13, 6);
			CreditMaintLabel.Size = new Size(435, 37);
			CreditMaintLabel.TabIndex = 7;
			CreditMaintLabel.Text = LocalizationManager.GetString("CreditMaintLabelText");
			// 
			// DocumentationLinkLabel
			// 
			DocumentationLinkLabel.AutoSize = true;
			DocumentationLinkLabel.Location = new Point(0, 313);
			DocumentationLinkLabel.Margin = new Padding(5, 0, 5, 0);
			DocumentationLinkLabel.Name = "DocumentationLinkLabel";
			DocumentationLinkLabel.Padding = new Padding(13, 6, 13, 6);
			DocumentationLinkLabel.Size = new Size(389, 37);
			DocumentationLinkLabel.TabIndex = 6;
			DocumentationLinkLabel.Text = LocalizationManager.GetString("DocumentationLinkLabelText");
			// 
			// DescriptionLabel
			// 
			DescriptionLabel.BackColor = Color.Transparent;
			DescriptionLabel.Location = new Point(0, 56);
			DescriptionLabel.Margin = new Padding(5, 0, 5, 0);
			DescriptionLabel.Name = "DescriptionLabel";
			DescriptionLabel.Padding = new Padding(13, 6, 13, 6);
			DescriptionLabel.Size = new Size(435, 279);
			DescriptionLabel.TabIndex = 5;
			DescriptionLabel.Text = resources.GetString("DescriptionLabel.Text");
			// 
			// VersionLabel
			// 
			VersionLabel.AutoSize = true;
			VersionLabel.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
			VersionLabel.Location = new Point(222, 17);
			VersionLabel.Margin = new Padding(5, 0, 5, 0);
			VersionLabel.Name = "VersionLabel";
			VersionLabel.Size = new Size(69, 29);
			VersionLabel.TabIndex = 4;
			VersionLabel.Text = "1.0.0";
			// 
			// NameLabel
			// 
			NameLabel.AutoSize = true;
			NameLabel.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
			NameLabel.Location = new Point(7, 17);
			NameLabel.Margin = new Padding(5, 0, 5, 0);
			NameLabel.Name = "NameLabel";
			NameLabel.Size = new Size(195, 29);
			NameLabel.TabIndex = 3;
			NameLabel.Text = "EVE-O-Preview";
			// 
			// DocumentationLink
			// 
			DocumentationLink.Location = new Point(0, 340);
			DocumentationLink.Margin = new Padding(50, 6, 5, 6);
			DocumentationLink.Name = "DocumentationLink";
			DocumentationLink.Padding = new Padding(13, 6, 13, 6);
			DocumentationLink.Size = new Size(437, 63);
			DocumentationLink.TabIndex = 2;
			DocumentationLink.TabStop = true;
			DocumentationLink.Text = LocalizationManager.GetString("DocumentationLinkText");
			DocumentationLink.LinkClicked += DocumentationLinkClicked_Handler;
			// 
			// NotifyIcon
			// 
			NotifyIcon.ContextMenuStrip = TrayMenu;
			NotifyIcon.Icon = (Icon)resources.GetObject("NotifyIcon.Icon");
			NotifyIcon.Text = "EVE-O-Preview";
			NotifyIcon.Visible = true;
			NotifyIcon.MouseDoubleClick += RestoreMainForm_Handler;
			// 
			// TrayMenu
			// 
			TrayMenu.ImageScalingSize = new Size(24, 24);
			TrayMenu.Items.AddRange(new ToolStripItem[] { TitleMenuItem, RestoreWindowMenuItem, SeparatorMenuItem, ExitMenuItem });
			TrayMenu.Name = "contextMenuStrip1";
			TrayMenu.Size = new Size(202, 106);
			// 
			// MainForm
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = SystemColors.Control;
			ClientSize = new Size(650, 419);
			Controls.Add(ContentTabControl);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			Icon = (Icon)resources.GetObject("$this.Icon");
			Margin = new Padding(0);
			MaximizeBox = false;
			Name = "MainForm";
			Text = "EVE-O-Preview";
			TopMost = true;
			FormClosing += MainFormClosing_Handler;
			Load += MainFormResize_Handler;
			Resize += MainFormResize_Handler;
			ContentTabControl.ResumeLayout(false);
			GeneralTabPage.ResumeLayout(false);
			GeneralSettingsPanel.ResumeLayout(false);
			GeneralSettingsPanel.PerformLayout();
			ThumbnailTabPage.ResumeLayout(false);
			ThumbnailSettingsPanel.ResumeLayout(false);
			ThumbnailSettingsPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ThumbnailSnapToGridSizeYNumericEdit).EndInit();
			((System.ComponentModel.ISupportInitialize)ThumbnailSnapToGridSizeXNumericEdit).EndInit();
			((System.ComponentModel.ISupportInitialize)ThumbnailsWidthNumericEdit).EndInit();
			((System.ComponentModel.ISupportInitialize)ThumbnailsHeightNumericEdit).EndInit();
			((System.ComponentModel.ISupportInitialize)ThumbnailOpacityTrackBar).EndInit();
			ZoomTabPage.ResumeLayout(false);
			ZoomSettingsPanel.ResumeLayout(false);
			ZoomSettingsPanel.PerformLayout();
			ZoomAnchorPanel.ResumeLayout(false);
			ZoomAnchorPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ThumbnailZoomFactorNumericEdit).EndInit();
			OverlayTabPage.ResumeLayout(false);
			OverlaySettingsPanel.ResumeLayout(false);
			OverlaySettingsPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)OverlayLabelSizeNumericEdit).EndInit();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			ClientsTabPage.ResumeLayout(false);
			ClientsPanel.ResumeLayout(false);
			ClientsPanel.PerformLayout();
			LanguageTabPage.ResumeLayout(false);
			LanguageSettingsPanel.ResumeLayout(false);
			LanguageSettingsPanel.PerformLayout();
			AboutTabPage.ResumeLayout(false);
			AboutPanel.ResumeLayout(false);
			AboutPanel.PerformLayout();
			TrayMenu.ResumeLayout(false);
			ResumeLayout(false);

		}

		#endregion
		private NotifyIcon NotifyIcon;
		private ContextMenuStrip TrayMenu;
		private TabPage ZoomTabPage;
		private CheckBox EnableClientLayoutTrackingCheckBox;
		private CheckBox HideActiveClientThumbnailCheckBox;
		private CheckBox ShowThumbnailsAlwaysOnTopCheckBox;
		private CheckBox HideThumbnailsOnLostFocusCheckBox;
		private CheckBox EnablePerClientThumbnailsLayoutsCheckBox;
		private CheckBox MinimizeToTrayCheckBox;
		private NumericUpDown ThumbnailsWidthNumericEdit;
		private NumericUpDown ThumbnailsHeightNumericEdit;
		private TrackBar ThumbnailOpacityTrackBar;
		private Panel ZoomAnchorPanel;
		private RadioButton ZoomAanchorNWRadioButton;
		private RadioButton ZoomAanchorNRadioButton;
		private RadioButton ZoomAanchorNERadioButton;
		private RadioButton ZoomAanchorWRadioButton;
		private RadioButton ZoomAanchorSERadioButton;
		private RadioButton ZoomAanchorCRadioButton;
		private RadioButton ZoomAanchorSRadioButton;
		private RadioButton ZoomAanchorERadioButton;
		private RadioButton ZoomAanchorSWRadioButton;
		private CheckBox EnableThumbnailZoomCheckBox;
		private NumericUpDown ThumbnailZoomFactorNumericEdit;
		private Label HighlightColorLabel;
		private Panel ActiveClientHighlightColorButton;
		private CheckBox EnableActiveClientHighlightCheckBox;
		private CheckBox ShowThumbnailOverlaysCheckBox;
		private CheckBox ShowThumbnailFramesCheckBox;
		private CheckedListBox ThumbnailsList;
		private LinkLabel DocumentationLink;
		private Label VersionLabel;
		private CheckBox MinimizeInactiveClientsCheckBox;
        private CheckBox LockThumbnailLocationCheckbox;
        private NumericUpDown ThumbnailSnapToGridSizeYNumericEdit;
        private Label SnapYLabel;
        private NumericUpDown ThumbnailSnapToGridSizeXNumericEdit;
        private Label SnapXLabel;
        private CheckBox ThumbnailSnapToGridCheckBox;
        private Label label3;
        private Label label2;
        private Panel OverlayLabelColorButton;
        private NumericUpDown OverlayLabelSizeNumericEdit;
        private Panel panel1;
        private RadioButton OverlayLabelNWRadioButton;
        private RadioButton OverlayLabelNRadioButton;
        private RadioButton OverlayLabelNERadioButton;
        private RadioButton OverlayLabelWRadioButton;
        private RadioButton OverlayLabelSERadioButton;
        private RadioButton OverlayLabelCRadioButton;
        private RadioButton OverlayLabelSRadioButton;
        private RadioButton OverlayLabelERadioButton;
        private RadioButton OverlayLabelSWRadioButton;
        private Label label1;
		private ComboBox AnimationStyleCombo;
		private Label LanguageLabel;
		private ComboBox LanguageCombo;
		private TabPage LanguageTabPage;
		private Panel LanguageSettingsPanel;
	}
}