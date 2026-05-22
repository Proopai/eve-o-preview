using System.Drawing;
using System.Windows.Forms;

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
			ToolStripMenuItem CloseAllEveClientsMenuItem;
			ToolStripMenuItem ExitMenuItem;
			ToolStripMenuItem TitleMenuItem;
			ToolStripSeparator SeparatorMenuItem;
			TabControl ContentTabControl;
			TabPage GeneralTabPage;
			Panel GeneralSettingsPanel;
			Label label4;
			TabPage ThumbnailTabPage;
			Panel ThumbnailSettingsPanel;
			Label HeigthLabel;
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
			HideCaptionOnClientsCheckBox = new CheckBox();
			AnimationStyleCombo = new ComboBox();
			MinimizeInactiveClientsCheckBox = new CheckBox();
			EnableClientLayoutTrackingCheckBox = new CheckBox();
			HideActiveClientThumbnailCheckBox = new CheckBox();
			ShowThumbnailsAlwaysOnTopCheckBox = new CheckBox();
			HideThumbnailsOnLostFocusCheckBox = new CheckBox();
			OnlyRegisterCycleHotkeysWhenEveFocusedCheckBox = new CheckBox();
			DynamicCycleGroupCheckBox = new CheckBox();
			EnablePerClientThumbnailsLayoutsCheckBox = new CheckBox();
			MinimizeToTrayCheckBox = new CheckBox();
			CloseAllEveClientsButton = new Button();
			label1 = new Label();
			PreventPreviewColorButton = new Panel();
			PreventPreviewsCheckBox = new CheckBox();
			ThumbnailSnapToGridCheckBox = new CheckBox();
			ThumbnailSnapToGridSizeYNumericEdit = new NumericUpDown();
			SnapYLabel = new Label();
			ThumbnailSnapToGridSizeXNumericEdit = new NumericUpDown();
			SnapXLabel = new Label();
			LockThumbnailLocationCheckbox = new CheckBox();
			ThumbnailsWidthNumericEdit = new NumericUpDown();
			ThumbnailsHeightNumericEdit = new NumericUpDown();
			OverwatchWidthLabel = new Label();
			OverwatchHeightLabel = new Label();
			FocusedThumbnailWidthNumericEdit = new NumericUpDown();
			FocusedThumbnailHeightNumericEdit = new NumericUpDown();
			OverwatchPosXLabel = new Label();
			OverwatchPosYLabel = new Label();
			FocusedThumbnailLocationXNumericEdit = new NumericUpDown();
			FocusedThumbnailLocationYNumericEdit = new NumericUpDown();
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
			label5 = new Label();
			panel2 = new Panel();
			CycleGroupIndicatorNWRadioButton = new RadioButton();
			CycleGroupIndicatorNRadioButton = new RadioButton();
			CycleGroupIndicatorNERadioButton = new RadioButton();
			CycleGroupIndicatorWRadioButton = new RadioButton();
			CycleGroupIndicatorSERadioButton = new RadioButton();
			CycleGroupIndicatorCRadioButton = new RadioButton();
			CycleGroupIndicatorSRadioButton = new RadioButton();
			CycleGroupIndicatorERadioButton = new RadioButton();
			CycleGroupIndicatorSWRadioButton = new RadioButton();
			LabelOverlayLabelFont = new Label();
			btnLabelFont = new Button();
			label3 = new Label();
			label2 = new Label();
			OverlayLabelColorButton = new Panel();
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
			CloseAllEveClientsMenuItem = new ToolStripMenuItem();
			ExitMenuItem = new ToolStripMenuItem();
			TitleMenuItem = new ToolStripMenuItem();
			SeparatorMenuItem = new ToolStripSeparator();
			ContentTabControl = new TabControl();
			GeneralTabPage = new TabPage();
			GeneralSettingsPanel = new Panel();
			label4 = new Label();
			ThumbnailTabPage = new TabPage();
			ThumbnailSettingsPanel = new Panel();
			HeigthLabel = new Label();
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
			ContentTabControl.SuspendLayout();
			GeneralTabPage.SuspendLayout();
			GeneralSettingsPanel.SuspendLayout();
			ThumbnailTabPage.SuspendLayout();
			ThumbnailSettingsPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ThumbnailSnapToGridSizeYNumericEdit).BeginInit();
			((System.ComponentModel.ISupportInitialize)ThumbnailSnapToGridSizeXNumericEdit).BeginInit();
			((System.ComponentModel.ISupportInitialize)ThumbnailsWidthNumericEdit).BeginInit();
			((System.ComponentModel.ISupportInitialize)ThumbnailsHeightNumericEdit).BeginInit();
			((System.ComponentModel.ISupportInitialize)FocusedThumbnailWidthNumericEdit).BeginInit();
			((System.ComponentModel.ISupportInitialize)FocusedThumbnailHeightNumericEdit).BeginInit();
			((System.ComponentModel.ISupportInitialize)FocusedThumbnailLocationXNumericEdit).BeginInit();
			((System.ComponentModel.ISupportInitialize)FocusedThumbnailLocationYNumericEdit).BeginInit();
			((System.ComponentModel.ISupportInitialize)ThumbnailOpacityTrackBar).BeginInit();
			ZoomTabPage.SuspendLayout();
			ZoomSettingsPanel.SuspendLayout();
			ZoomAnchorPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ThumbnailZoomFactorNumericEdit).BeginInit();
			OverlayTabPage.SuspendLayout();
			OverlaySettingsPanel.SuspendLayout();
			panel2.SuspendLayout();
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
			RestoreWindowMenuItem.Size = new Size(153, 22);
			RestoreWindowMenuItem.Text = "Restore";
			RestoreWindowMenuItem.Click += RestoreMainForm_Handler;
			// 
			// CloseAllEveClientsMenuItem
			// 
			CloseAllEveClientsMenuItem.Name = "CloseAllEveClientsMenuItem";
			CloseAllEveClientsMenuItem.Size = new Size(153, 22);
			CloseAllEveClientsMenuItem.Text = "Close all EVE clients";
			CloseAllEveClientsMenuItem.Click += CloseAllEveClients_Handler;
			// 
			// ExitMenuItem
			// 
			ExitMenuItem.Name = "ExitMenuItem";
			ExitMenuItem.Size = new Size(153, 22);
			ExitMenuItem.Text = "Exit";
			ExitMenuItem.Click += ExitMenuItemClick_Handler;
			// 
			// TitleMenuItem
			// 
			TitleMenuItem.Enabled = false;
			TitleMenuItem.Name = "TitleMenuItem";
			TitleMenuItem.Size = new Size(153, 22);
			TitleMenuItem.Text = "EVE-O-Preview";
			// 
			// SeparatorMenuItem
			// 
			SeparatorMenuItem.Name = "SeparatorMenuItem";
			SeparatorMenuItem.Size = new Size(150, 6);
			// 
			// ContentTabControl
			// 
			ContentTabControl.Alignment = TabAlignment.Left;
			ContentTabControl.Controls.Add(GeneralTabPage);
			ContentTabControl.Controls.Add(ThumbnailTabPage);
			ContentTabControl.Controls.Add(ZoomTabPage);
			ContentTabControl.Controls.Add(OverlayTabPage);
			ContentTabControl.Controls.Add(ClientsTabPage);
			ContentTabControl.Controls.Add(AboutTabPage);
			ContentTabControl.Dock = DockStyle.Fill;
			ContentTabControl.DrawMode = TabDrawMode.OwnerDrawFixed;
			ContentTabControl.ItemSize = new Size(35, 120);
			ContentTabControl.Location = new Point(0, 0);
			ContentTabControl.Margin = new Padding(4);
			ContentTabControl.Multiline = true;
			ContentTabControl.Name = "ContentTabControl";
			ContentTabControl.SelectedIndex = 0;
			ContentTabControl.Size = new Size(455, 340);
			ContentTabControl.SizeMode = TabSizeMode.Fixed;
			ContentTabControl.TabIndex = 6;
			ContentTabControl.DrawItem += ContentTabControl_DrawItem;
			// 
			// GeneralTabPage
			// 
			GeneralTabPage.BackColor = SystemColors.Control;
			GeneralTabPage.Controls.Add(GeneralSettingsPanel);
			GeneralTabPage.Location = new Point(124, 4);
			GeneralTabPage.Margin = new Padding(4);
			GeneralTabPage.Name = "GeneralTabPage";
			GeneralTabPage.Padding = new Padding(4);
			GeneralTabPage.Size = new Size(327, 353);
			GeneralTabPage.TabIndex = 0;
			GeneralTabPage.Text = "General";
			// 
			// GeneralSettingsPanel
			// 
			GeneralSettingsPanel.BorderStyle = BorderStyle.FixedSingle;
			GeneralSettingsPanel.Controls.Add(HideCaptionOnClientsCheckBox);
			GeneralSettingsPanel.Controls.Add(label4);
			GeneralSettingsPanel.Controls.Add(AnimationStyleCombo);
			GeneralSettingsPanel.Controls.Add(MinimizeInactiveClientsCheckBox);
			GeneralSettingsPanel.Controls.Add(EnableClientLayoutTrackingCheckBox);
			GeneralSettingsPanel.Controls.Add(HideActiveClientThumbnailCheckBox);
			GeneralSettingsPanel.Controls.Add(ShowThumbnailsAlwaysOnTopCheckBox);
			GeneralSettingsPanel.Controls.Add(HideThumbnailsOnLostFocusCheckBox);
			GeneralSettingsPanel.Controls.Add(OnlyRegisterCycleHotkeysWhenEveFocusedCheckBox);
			GeneralSettingsPanel.Controls.Add(DynamicCycleGroupCheckBox);
			GeneralSettingsPanel.Controls.Add(EnablePerClientThumbnailsLayoutsCheckBox);
			GeneralSettingsPanel.Controls.Add(MinimizeToTrayCheckBox);
			GeneralSettingsPanel.Controls.Add(CloseAllEveClientsButton);
			GeneralSettingsPanel.Dock = DockStyle.Fill;
			GeneralSettingsPanel.Location = new Point(4, 4);
			GeneralSettingsPanel.Margin = new Padding(4);
			GeneralSettingsPanel.Name = "GeneralSettingsPanel";
			GeneralSettingsPanel.Size = new Size(319, 291);
			GeneralSettingsPanel.TabIndex = 18;
			// 
			// HideCaptionOnClientsCheckBox
			// 
			HideCaptionOnClientsCheckBox.AutoSize = true;
			HideCaptionOnClientsCheckBox.Location = new Point(9, 121);
			HideCaptionOnClientsCheckBox.Margin = new Padding(4);
			HideCaptionOnClientsCheckBox.Name = "HideCaptionOnClientsCheckBox";
			HideCaptionOnClientsCheckBox.Size = new Size(168, 19);
			HideCaptionOnClientsCheckBox.TabIndex = 28;
			HideCaptionOnClientsCheckBox.Text = "Hide caption bar on clients";
			HideCaptionOnClientsCheckBox.UseVisualStyleBackColor = true;
			HideCaptionOnClientsCheckBox.CheckedChanged += OptionChanged_Handler;
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Location = new Point(6, 96);
			label4.Margin = new Padding(4, 0, 4, 0);
			label4.Name = "label4";
			label4.Size = new Size(91, 15);
			label4.TabIndex = 27;
			label4.Text = "Animation Style";
			// 
			// AnimationStyleCombo
			// 
			AnimationStyleCombo.DropDownStyle = ComboBoxStyle.DropDownList;
			AnimationStyleCombo.FormattingEnabled = true;
			AnimationStyleCombo.Location = new Point(105, 93);
			AnimationStyleCombo.Margin = new Padding(4);
			AnimationStyleCombo.Name = "AnimationStyleCombo";
			AnimationStyleCombo.Size = new Size(177, 23);
			AnimationStyleCombo.TabIndex = 26;
			AnimationStyleCombo.SelectedIndexChanged += OptionChanged_Handler;
			// 
			// MinimizeInactiveClientsCheckBox
			// 
			MinimizeInactiveClientsCheckBox.AutoSize = true;
			MinimizeInactiveClientsCheckBox.Location = new Point(9, 73);
			MinimizeInactiveClientsCheckBox.Margin = new Padding(4);
			MinimizeInactiveClientsCheckBox.Name = "MinimizeInactiveClientsCheckBox";
			MinimizeInactiveClientsCheckBox.Size = new Size(178, 19);
			MinimizeInactiveClientsCheckBox.TabIndex = 24;
			MinimizeInactiveClientsCheckBox.Text = "Minimize inactive EVE clients";
			MinimizeInactiveClientsCheckBox.UseVisualStyleBackColor = true;
			MinimizeInactiveClientsCheckBox.CheckedChanged += OptionChanged_Handler;
			// 
			// EnableClientLayoutTrackingCheckBox
			// 
			EnableClientLayoutTrackingCheckBox.AutoSize = true;
			EnableClientLayoutTrackingCheckBox.Location = new Point(9, 30);
			EnableClientLayoutTrackingCheckBox.Margin = new Padding(4);
			EnableClientLayoutTrackingCheckBox.Name = "EnableClientLayoutTrackingCheckBox";
			EnableClientLayoutTrackingCheckBox.Size = new Size(137, 19);
			EnableClientLayoutTrackingCheckBox.TabIndex = 19;
			EnableClientLayoutTrackingCheckBox.Text = "Track client locations";
			EnableClientLayoutTrackingCheckBox.UseVisualStyleBackColor = true;
			EnableClientLayoutTrackingCheckBox.CheckedChanged += OptionChanged_Handler;
			// 
			// HideActiveClientThumbnailCheckBox
			// 
			HideActiveClientThumbnailCheckBox.AutoSize = true;
			HideActiveClientThumbnailCheckBox.Checked = true;
			HideActiveClientThumbnailCheckBox.CheckState = CheckState.Checked;
			HideActiveClientThumbnailCheckBox.Location = new Point(9, 52);
			HideActiveClientThumbnailCheckBox.Margin = new Padding(4);
			HideActiveClientThumbnailCheckBox.Name = "HideActiveClientThumbnailCheckBox";
			HideActiveClientThumbnailCheckBox.Size = new Size(197, 19);
			HideActiveClientThumbnailCheckBox.TabIndex = 20;
			HideActiveClientThumbnailCheckBox.Text = "Hide preview of active EVE client";
			HideActiveClientThumbnailCheckBox.UseVisualStyleBackColor = true;
			HideActiveClientThumbnailCheckBox.CheckedChanged += OptionChanged_Handler;
			// 
			// ShowThumbnailsAlwaysOnTopCheckBox
			// 
			ShowThumbnailsAlwaysOnTopCheckBox.AutoSize = true;
			ShowThumbnailsAlwaysOnTopCheckBox.Checked = true;
			ShowThumbnailsAlwaysOnTopCheckBox.CheckState = CheckState.Checked;
			ShowThumbnailsAlwaysOnTopCheckBox.Location = new Point(9, 142);
			ShowThumbnailsAlwaysOnTopCheckBox.Margin = new Padding(4);
			ShowThumbnailsAlwaysOnTopCheckBox.Name = "ShowThumbnailsAlwaysOnTopCheckBox";
			ShowThumbnailsAlwaysOnTopCheckBox.RightToLeft = RightToLeft.No;
			ShowThumbnailsAlwaysOnTopCheckBox.Size = new Size(148, 19);
			ShowThumbnailsAlwaysOnTopCheckBox.TabIndex = 21;
			ShowThumbnailsAlwaysOnTopCheckBox.Text = "Previews always on top";
			ShowThumbnailsAlwaysOnTopCheckBox.UseVisualStyleBackColor = true;
			ShowThumbnailsAlwaysOnTopCheckBox.CheckedChanged += OptionChanged_Handler;
			// 
			// HideThumbnailsOnLostFocusCheckBox
			// 
			HideThumbnailsOnLostFocusCheckBox.AutoSize = true;
			HideThumbnailsOnLostFocusCheckBox.Checked = true;
			HideThumbnailsOnLostFocusCheckBox.CheckState = CheckState.Checked;
			HideThumbnailsOnLostFocusCheckBox.Location = new Point(9, 163);
			HideThumbnailsOnLostFocusCheckBox.Margin = new Padding(4);
			HideThumbnailsOnLostFocusCheckBox.Name = "HideThumbnailsOnLostFocusCheckBox";
			HideThumbnailsOnLostFocusCheckBox.Size = new Size(252, 19);
			HideThumbnailsOnLostFocusCheckBox.TabIndex = 22;
			HideThumbnailsOnLostFocusCheckBox.Text = "Hide previews when EVE client is not active";
			HideThumbnailsOnLostFocusCheckBox.UseVisualStyleBackColor = true;
			HideThumbnailsOnLostFocusCheckBox.CheckedChanged += OptionChanged_Handler;
			// 
			// OnlyRegisterCycleHotkeysWhenEveFocusedCheckBox
			// 
			OnlyRegisterCycleHotkeysWhenEveFocusedCheckBox.AutoSize = true;
			OnlyRegisterCycleHotkeysWhenEveFocusedCheckBox.Checked = true;
			OnlyRegisterCycleHotkeysWhenEveFocusedCheckBox.CheckState = CheckState.Checked;
			OnlyRegisterCycleHotkeysWhenEveFocusedCheckBox.Location = new Point(9, 184);
			OnlyRegisterCycleHotkeysWhenEveFocusedCheckBox.Margin = new Padding(4);
			OnlyRegisterCycleHotkeysWhenEveFocusedCheckBox.Name = "OnlyRegisterCycleHotkeysWhenEveFocusedCheckBox";
			OnlyRegisterCycleHotkeysWhenEveFocusedCheckBox.Size = new Size(262, 19);
			OnlyRegisterCycleHotkeysWhenEveFocusedCheckBox.TabIndex = 23;
			OnlyRegisterCycleHotkeysWhenEveFocusedCheckBox.Text = "Only register cycle hotkeys when EVE is active";
			OnlyRegisterCycleHotkeysWhenEveFocusedCheckBox.UseVisualStyleBackColor = true;
			OnlyRegisterCycleHotkeysWhenEveFocusedCheckBox.CheckedChanged += OptionChanged_Handler;
			// 
			// DynamicCycleGroupCheckBox
			// 
			DynamicCycleGroupCheckBox.AutoSize = true;
			DynamicCycleGroupCheckBox.Location = new Point(9, 205);
			DynamicCycleGroupCheckBox.Margin = new Padding(4);
			DynamicCycleGroupCheckBox.Name = "DynamicCycleGroupCheckBox";
			DynamicCycleGroupCheckBox.Size = new Size(140, 19);
			DynamicCycleGroupCheckBox.TabIndex = 30;
			DynamicCycleGroupCheckBox.Text = "Dynamic cycle group";
			DynamicCycleGroupCheckBox.UseVisualStyleBackColor = true;
			DynamicCycleGroupCheckBox.CheckedChanged += OptionChanged_Handler;
			// 
			// EnablePerClientThumbnailsLayoutsCheckBox
			// 
			EnablePerClientThumbnailsLayoutsCheckBox.AutoSize = true;
			EnablePerClientThumbnailsLayoutsCheckBox.Checked = true;
			EnablePerClientThumbnailsLayoutsCheckBox.CheckState = CheckState.Checked;
			EnablePerClientThumbnailsLayoutsCheckBox.Location = new Point(9, 226);
			EnablePerClientThumbnailsLayoutsCheckBox.Margin = new Padding(4);
			EnablePerClientThumbnailsLayoutsCheckBox.Name = "EnablePerClientThumbnailsLayoutsCheckBox";
			EnablePerClientThumbnailsLayoutsCheckBox.Size = new Size(200, 19);
			EnablePerClientThumbnailsLayoutsCheckBox.TabIndex = 24;
			EnablePerClientThumbnailsLayoutsCheckBox.Text = "Unique layout for each EVE client";
			EnablePerClientThumbnailsLayoutsCheckBox.UseVisualStyleBackColor = true;
			EnablePerClientThumbnailsLayoutsCheckBox.CheckedChanged += OptionChanged_Handler;
			// 
			// MinimizeToTrayCheckBox
			// 
			MinimizeToTrayCheckBox.AutoSize = true;
			MinimizeToTrayCheckBox.Location = new Point(9, 8);
			MinimizeToTrayCheckBox.Margin = new Padding(4);
			MinimizeToTrayCheckBox.Name = "MinimizeToTrayCheckBox";
			MinimizeToTrayCheckBox.Size = new Size(155, 19);
			MinimizeToTrayCheckBox.TabIndex = 18;
			MinimizeToTrayCheckBox.Text = "Minimize to System Tray";
			MinimizeToTrayCheckBox.UseVisualStyleBackColor = true;
			MinimizeToTrayCheckBox.CheckedChanged += OptionChanged_Handler;
			// 
			// CloseAllEveClientsButton
			// 
			CloseAllEveClientsButton.Location = new Point(9, 253);
			CloseAllEveClientsButton.Margin = new Padding(4);
			CloseAllEveClientsButton.Name = "CloseAllEveClientsButton";
			CloseAllEveClientsButton.Size = new Size(280, 28);
			CloseAllEveClientsButton.TabIndex = 29;
			CloseAllEveClientsButton.Text = "Close all EVE clients";
			CloseAllEveClientsButton.UseVisualStyleBackColor = true;
			CloseAllEveClientsButton.Click += CloseAllEveClients_Handler;
			// 
			// ThumbnailTabPage
			// 
			ThumbnailTabPage.BackColor = SystemColors.Control;
			ThumbnailTabPage.Controls.Add(ThumbnailSettingsPanel);
			ThumbnailTabPage.Location = new Point(124, 4);
			ThumbnailTabPage.Margin = new Padding(4);
			ThumbnailTabPage.Name = "ThumbnailTabPage";
			ThumbnailTabPage.Padding = new Padding(4);
			ThumbnailTabPage.Size = new Size(327, 332);
			ThumbnailTabPage.TabIndex = 1;
			ThumbnailTabPage.Text = "Thumbnail";
			// 
			// ThumbnailSettingsPanel
			// 
			ThumbnailSettingsPanel.BorderStyle = BorderStyle.FixedSingle;
			ThumbnailSettingsPanel.Controls.Add(OverwatchPosYLabel);
			ThumbnailSettingsPanel.Controls.Add(FocusedThumbnailLocationYNumericEdit);
			ThumbnailSettingsPanel.Controls.Add(OverwatchPosXLabel);
			ThumbnailSettingsPanel.Controls.Add(FocusedThumbnailLocationXNumericEdit);
			ThumbnailSettingsPanel.Controls.Add(OverwatchHeightLabel);
			ThumbnailSettingsPanel.Controls.Add(FocusedThumbnailHeightNumericEdit);
			ThumbnailSettingsPanel.Controls.Add(OverwatchWidthLabel);
			ThumbnailSettingsPanel.Controls.Add(FocusedThumbnailWidthNumericEdit);
			ThumbnailSettingsPanel.Controls.Add(label1);
			ThumbnailSettingsPanel.Controls.Add(PreventPreviewColorButton);
			ThumbnailSettingsPanel.Controls.Add(PreventPreviewsCheckBox);
			ThumbnailSettingsPanel.Controls.Add(ThumbnailSnapToGridCheckBox);
			ThumbnailSettingsPanel.Controls.Add(ThumbnailSnapToGridSizeYNumericEdit);
			ThumbnailSettingsPanel.Controls.Add(SnapYLabel);
			ThumbnailSettingsPanel.Controls.Add(ThumbnailSnapToGridSizeXNumericEdit);
			ThumbnailSettingsPanel.Controls.Add(SnapXLabel);
			ThumbnailSettingsPanel.Controls.Add(LockThumbnailLocationCheckbox);
			ThumbnailSettingsPanel.Controls.Add(HeigthLabel);
			ThumbnailSettingsPanel.Controls.Add(WidthLabel);
			ThumbnailSettingsPanel.Controls.Add(ThumbnailsWidthNumericEdit);
			ThumbnailSettingsPanel.Controls.Add(ThumbnailsHeightNumericEdit);
			ThumbnailSettingsPanel.Controls.Add(ThumbnailOpacityTrackBar);
			ThumbnailSettingsPanel.Controls.Add(OpacityLabel);
			ThumbnailSettingsPanel.Dock = DockStyle.Fill;
			ThumbnailSettingsPanel.Location = new Point(4, 4);
			ThumbnailSettingsPanel.Margin = new Padding(4);
			ThumbnailSettingsPanel.Name = "ThumbnailSettingsPanel";
			ThumbnailSettingsPanel.Size = new Size(319, 324);
			ThumbnailSettingsPanel.TabIndex = 19;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(175, 253);
			label1.Margin = new Padding(4, 0, 4, 0);
			label1.Name = "label1";
			label1.Size = new Size(36, 15);
			label1.TabIndex = 35;
			label1.Text = "Color";
			// 
			// PreventPreviewColorButton
			// 
			PreventPreviewColorButton.BorderStyle = BorderStyle.FixedSingle;
			PreventPreviewColorButton.Location = new Point(218, 252);
			PreventPreviewColorButton.Margin = new Padding(4);
			PreventPreviewColorButton.Name = "PreventPreviewColorButton";
			PreventPreviewColorButton.Size = new Size(58, 19);
			PreventPreviewColorButton.TabIndex = 34;
			PreventPreviewColorButton.Click += PreventPreviewColorButton_Click;
			// 
			// PreventPreviewsCheckBox
			// 
			PreventPreviewsCheckBox.AutoSize = true;
			PreventPreviewsCheckBox.Location = new Point(13, 252);
			PreventPreviewsCheckBox.Margin = new Padding(4);
			PreventPreviewsCheckBox.Name = "PreventPreviewsCheckBox";
			PreventPreviewsCheckBox.Size = new Size(151, 19);
			PreventPreviewsCheckBox.TabIndex = 33;
			PreventPreviewsCheckBox.Text = "Do not display previews";
			PreventPreviewsCheckBox.UseVisualStyleBackColor = true;
			PreventPreviewsCheckBox.CheckedChanged += OptionChanged_Handler;
			// 
			// ThumbnailSnapToGridCheckBox
			// 
			ThumbnailSnapToGridCheckBox.AutoSize = true;
			ThumbnailSnapToGridCheckBox.Location = new Point(13, 204);
			ThumbnailSnapToGridCheckBox.Margin = new Padding(4);
			ThumbnailSnapToGridCheckBox.Name = "ThumbnailSnapToGridCheckBox";
			ThumbnailSnapToGridCheckBox.Size = new Size(152, 19);
			ThumbnailSnapToGridCheckBox.TabIndex = 32;
			ThumbnailSnapToGridCheckBox.Text = "Thumbnail Snap to Grid";
			ThumbnailSnapToGridCheckBox.UseVisualStyleBackColor = true;
			ThumbnailSnapToGridCheckBox.CheckedChanged += OptionChanged_Handler;
			// 
			// ThumbnailSnapToGridSizeYNumericEdit
			// 
			ThumbnailSnapToGridSizeYNumericEdit.BackColor = SystemColors.Window;
			ThumbnailSnapToGridSizeYNumericEdit.BorderStyle = BorderStyle.FixedSingle;
			ThumbnailSnapToGridSizeYNumericEdit.CausesValidation = false;
			ThumbnailSnapToGridSizeYNumericEdit.Increment = new decimal(new int[] { 10, 0, 0, 0 });
			ThumbnailSnapToGridSizeYNumericEdit.Location = new Point(152, 225);
			ThumbnailSnapToGridSizeYNumericEdit.Margin = new Padding(4);
			ThumbnailSnapToGridSizeYNumericEdit.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
			ThumbnailSnapToGridSizeYNumericEdit.Name = "ThumbnailSnapToGridSizeYNumericEdit";
			ThumbnailSnapToGridSizeYNumericEdit.Size = new Size(56, 23);
			ThumbnailSnapToGridSizeYNumericEdit.TabIndex = 31;
			ThumbnailSnapToGridSizeYNumericEdit.Value = new decimal(new int[] { 100, 0, 0, 0 });
			ThumbnailSnapToGridSizeYNumericEdit.ValueChanged += OptionChanged_Handler;
			// 
			// SnapYLabel
			// 
			SnapYLabel.AutoSize = true;
			SnapYLabel.Location = new Point(128, 227);
			SnapYLabel.Margin = new Padding(4, 0, 4, 0);
			SnapYLabel.Name = "SnapYLabel";
			SnapYLabel.Size = new Size(14, 15);
			SnapYLabel.TabIndex = 30;
			SnapYLabel.Text = "Y";
			// 
			// ThumbnailSnapToGridSizeXNumericEdit
			// 
			ThumbnailSnapToGridSizeXNumericEdit.BackColor = SystemColors.Window;
			ThumbnailSnapToGridSizeXNumericEdit.BorderStyle = BorderStyle.FixedSingle;
			ThumbnailSnapToGridSizeXNumericEdit.CausesValidation = false;
			ThumbnailSnapToGridSizeXNumericEdit.Increment = new decimal(new int[] { 10, 0, 0, 0 });
			ThumbnailSnapToGridSizeXNumericEdit.Location = new Point(65, 225);
			ThumbnailSnapToGridSizeXNumericEdit.Margin = new Padding(4);
			ThumbnailSnapToGridSizeXNumericEdit.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
			ThumbnailSnapToGridSizeXNumericEdit.Name = "ThumbnailSnapToGridSizeXNumericEdit";
			ThumbnailSnapToGridSizeXNumericEdit.Size = new Size(56, 23);
			ThumbnailSnapToGridSizeXNumericEdit.TabIndex = 29;
			ThumbnailSnapToGridSizeXNumericEdit.Value = new decimal(new int[] { 100, 0, 0, 0 });
			ThumbnailSnapToGridSizeXNumericEdit.ValueChanged += OptionChanged_Handler;
			// 
			// SnapXLabel
			// 
			SnapXLabel.AutoSize = true;
			SnapXLabel.Location = new Point(9, 227);
			SnapXLabel.Margin = new Padding(4, 0, 4, 0);
			SnapXLabel.Name = "SnapXLabel";
			SnapXLabel.Size = new Size(43, 15);
			SnapXLabel.TabIndex = 28;
			SnapXLabel.Text = "Snap X";
			// 
			// LockThumbnailLocationCheckbox
			// 
			LockThumbnailLocationCheckbox.AutoSize = true;
			LockThumbnailLocationCheckbox.Location = new Point(13, 178);
			LockThumbnailLocationCheckbox.Margin = new Padding(4);
			LockThumbnailLocationCheckbox.Name = "LockThumbnailLocationCheckbox";
			LockThumbnailLocationCheckbox.Size = new Size(161, 19);
			LockThumbnailLocationCheckbox.TabIndex = 26;
			LockThumbnailLocationCheckbox.Text = "Lock Thumbnail Location";
			LockThumbnailLocationCheckbox.UseVisualStyleBackColor = true;
			LockThumbnailLocationCheckbox.CheckedChanged += OptionChanged_Handler;
			// 
			// HeigthLabel
			// 
			HeigthLabel.AutoSize = true;
			HeigthLabel.Location = new Point(9, 66);
			HeigthLabel.Margin = new Padding(4, 0, 4, 0);
			HeigthLabel.Name = "HeigthLabel";
			HeigthLabel.Size = new Size(104, 15);
			HeigthLabel.TabIndex = 24;
			HeigthLabel.Text = "Thumbnail Height";
			// 
			// WidthLabel
			// 
			WidthLabel.AutoSize = true;
			WidthLabel.Location = new Point(9, 38);
			WidthLabel.Margin = new Padding(4, 0, 4, 0);
			WidthLabel.Name = "WidthLabel";
			WidthLabel.Size = new Size(100, 15);
			WidthLabel.TabIndex = 23;
			WidthLabel.Text = "Thumbnail Width";
			// 
			// ThumbnailsWidthNumericEdit
			// 
			ThumbnailsWidthNumericEdit.BackColor = SystemColors.Window;
			ThumbnailsWidthNumericEdit.BorderStyle = BorderStyle.FixedSingle;
			ThumbnailsWidthNumericEdit.CausesValidation = false;
			ThumbnailsWidthNumericEdit.Increment = new decimal(new int[] { 10, 0, 0, 0 });
			ThumbnailsWidthNumericEdit.Location = new Point(122, 36);
			ThumbnailsWidthNumericEdit.Margin = new Padding(4);
			ThumbnailsWidthNumericEdit.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
			ThumbnailsWidthNumericEdit.Name = "ThumbnailsWidthNumericEdit";
			ThumbnailsWidthNumericEdit.Size = new Size(56, 23);
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
			ThumbnailsHeightNumericEdit.Location = new Point(122, 64);
			ThumbnailsHeightNumericEdit.Margin = new Padding(4);
			ThumbnailsHeightNumericEdit.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
			ThumbnailsHeightNumericEdit.Name = "ThumbnailsHeightNumericEdit";
			ThumbnailsHeightNumericEdit.Size = new Size(56, 23);
			ThumbnailsHeightNumericEdit.TabIndex = 22;
			ThumbnailsHeightNumericEdit.Value = new decimal(new int[] { 70, 0, 0, 0 });
			ThumbnailsHeightNumericEdit.ValueChanged += ThumbnailSizeChanged_Handler;
			// 
			// OverwatchWidthLabel
			// 
			OverwatchWidthLabel.AutoSize = true;
			OverwatchWidthLabel.Location = new Point(9, 94);
			OverwatchWidthLabel.Margin = new Padding(4, 0, 4, 0);
			OverwatchWidthLabel.Name = "OverwatchWidthLabel";
			OverwatchWidthLabel.Size = new Size(95, 15);
			OverwatchWidthLabel.TabIndex = 40;
			OverwatchWidthLabel.Text = "Overwatch width";
			// 
			// FocusedThumbnailWidthNumericEdit
			// 
			FocusedThumbnailWidthNumericEdit.BackColor = SystemColors.Window;
			FocusedThumbnailWidthNumericEdit.BorderStyle = BorderStyle.FixedSingle;
			FocusedThumbnailWidthNumericEdit.CausesValidation = false;
			FocusedThumbnailWidthNumericEdit.Increment = new decimal(new int[] { 10, 0, 0, 0 });
			FocusedThumbnailWidthNumericEdit.Location = new Point(122, 92);
			FocusedThumbnailWidthNumericEdit.Margin = new Padding(4);
			FocusedThumbnailWidthNumericEdit.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
			FocusedThumbnailWidthNumericEdit.Name = "FocusedThumbnailWidthNumericEdit";
			FocusedThumbnailWidthNumericEdit.Size = new Size(56, 23);
			FocusedThumbnailWidthNumericEdit.TabIndex = 41;
			FocusedThumbnailWidthNumericEdit.Value = new decimal(new int[] { 960, 0, 0, 0 });
			FocusedThumbnailWidthNumericEdit.ValueChanged += OptionChanged_Handler;
			// 
			// OverwatchHeightLabel
			// 
			OverwatchHeightLabel.AutoSize = true;
			OverwatchHeightLabel.Location = new Point(9, 122);
			OverwatchHeightLabel.Margin = new Padding(4, 0, 4, 0);
			OverwatchHeightLabel.Name = "OverwatchHeightLabel";
			OverwatchHeightLabel.Size = new Size(99, 15);
			OverwatchHeightLabel.TabIndex = 42;
			OverwatchHeightLabel.Text = "Overwatch height";
			// 
			// FocusedThumbnailHeightNumericEdit
			// 
			FocusedThumbnailHeightNumericEdit.BackColor = SystemColors.Window;
			FocusedThumbnailHeightNumericEdit.BorderStyle = BorderStyle.FixedSingle;
			FocusedThumbnailHeightNumericEdit.CausesValidation = false;
			FocusedThumbnailHeightNumericEdit.Increment = new decimal(new int[] { 10, 0, 0, 0 });
			FocusedThumbnailHeightNumericEdit.Location = new Point(122, 120);
			FocusedThumbnailHeightNumericEdit.Margin = new Padding(4);
			FocusedThumbnailHeightNumericEdit.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
			FocusedThumbnailHeightNumericEdit.Name = "FocusedThumbnailHeightNumericEdit";
			FocusedThumbnailHeightNumericEdit.Size = new Size(56, 23);
			FocusedThumbnailHeightNumericEdit.TabIndex = 43;
			FocusedThumbnailHeightNumericEdit.Value = new decimal(new int[] { 540, 0, 0, 0 });
			FocusedThumbnailHeightNumericEdit.ValueChanged += OptionChanged_Handler;
			// 
			// OverwatchPosXLabel
			// 
			OverwatchPosXLabel.AutoSize = true;
			OverwatchPosXLabel.Location = new Point(9, 150);
			OverwatchPosXLabel.Margin = new Padding(4, 0, 4, 0);
			OverwatchPosXLabel.Name = "OverwatchPosXLabel";
			OverwatchPosXLabel.Size = new Size(86, 15);
			OverwatchPosXLabel.TabIndex = 44;
			OverwatchPosXLabel.Text = "Overwatch X";
			// 
			// FocusedThumbnailLocationXNumericEdit
			// 
			FocusedThumbnailLocationXNumericEdit.BackColor = SystemColors.Window;
			FocusedThumbnailLocationXNumericEdit.BorderStyle = BorderStyle.FixedSingle;
			FocusedThumbnailLocationXNumericEdit.CausesValidation = false;
			FocusedThumbnailLocationXNumericEdit.Location = new Point(122, 148);
			FocusedThumbnailLocationXNumericEdit.Margin = new Padding(4);
			FocusedThumbnailLocationXNumericEdit.Maximum = new decimal(50000);
			FocusedThumbnailLocationXNumericEdit.Minimum = new decimal(-50000);
			FocusedThumbnailLocationXNumericEdit.Name = "FocusedThumbnailLocationXNumericEdit";
			FocusedThumbnailLocationXNumericEdit.Size = new Size(56, 23);
			FocusedThumbnailLocationXNumericEdit.TabIndex = 45;
			FocusedThumbnailLocationXNumericEdit.Value = new decimal(new int[] { 100, 0, 0, 0 });
			FocusedThumbnailLocationXNumericEdit.ValueChanged += OptionChanged_Handler;
			// 
			// OverwatchPosYLabel
			// 
			OverwatchPosYLabel.AutoSize = true;
			OverwatchPosYLabel.Location = new Point(188, 150);
			OverwatchPosYLabel.Margin = new Padding(4, 0, 4, 0);
			OverwatchPosYLabel.Name = "OverwatchPosYLabel";
			OverwatchPosYLabel.Size = new Size(14, 15);
			OverwatchPosYLabel.TabIndex = 46;
			OverwatchPosYLabel.Text = "Y";
			// 
			// FocusedThumbnailLocationYNumericEdit
			// 
			FocusedThumbnailLocationYNumericEdit.BackColor = SystemColors.Window;
			FocusedThumbnailLocationYNumericEdit.BorderStyle = BorderStyle.FixedSingle;
			FocusedThumbnailLocationYNumericEdit.CausesValidation = false;
			FocusedThumbnailLocationYNumericEdit.Location = new Point(210, 148);
			FocusedThumbnailLocationYNumericEdit.Margin = new Padding(4);
			FocusedThumbnailLocationYNumericEdit.Maximum = new decimal(50000);
			FocusedThumbnailLocationYNumericEdit.Minimum = new decimal(-50000);
			FocusedThumbnailLocationYNumericEdit.Name = "FocusedThumbnailLocationYNumericEdit";
			FocusedThumbnailLocationYNumericEdit.Size = new Size(56, 23);
			FocusedThumbnailLocationYNumericEdit.TabIndex = 47;
			FocusedThumbnailLocationYNumericEdit.Value = new decimal(new int[] { 100, 0, 0, 0 });
			FocusedThumbnailLocationYNumericEdit.ValueChanged += OptionChanged_Handler;
			// 
			// ThumbnailOpacityTrackBar
			// 
			ThumbnailOpacityTrackBar.AutoSize = false;
			ThumbnailOpacityTrackBar.LargeChange = 10;
			ThumbnailOpacityTrackBar.Location = new Point(71, 7);
			ThumbnailOpacityTrackBar.Margin = new Padding(4);
			ThumbnailOpacityTrackBar.Maximum = 100;
			ThumbnailOpacityTrackBar.Minimum = 20;
			ThumbnailOpacityTrackBar.Name = "ThumbnailOpacityTrackBar";
			ThumbnailOpacityTrackBar.Size = new Size(223, 25);
			ThumbnailOpacityTrackBar.TabIndex = 20;
			ThumbnailOpacityTrackBar.TickFrequency = 10;
			ThumbnailOpacityTrackBar.Value = 20;
			ThumbnailOpacityTrackBar.ValueChanged += OptionChanged_Handler;
			// 
			// OpacityLabel
			// 
			OpacityLabel.AutoSize = true;
			OpacityLabel.Location = new Point(9, 10);
			OpacityLabel.Margin = new Padding(4, 0, 4, 0);
			OpacityLabel.Name = "OpacityLabel";
			OpacityLabel.Size = new Size(48, 15);
			OpacityLabel.TabIndex = 19;
			OpacityLabel.Text = "Opacity";
			// 
			// ZoomTabPage
			// 
			ZoomTabPage.BackColor = SystemColors.Control;
			ZoomTabPage.Controls.Add(ZoomSettingsPanel);
			ZoomTabPage.Location = new Point(124, 4);
			ZoomTabPage.Margin = new Padding(4);
			ZoomTabPage.Name = "ZoomTabPage";
			ZoomTabPage.Size = new Size(327, 243);
			ZoomTabPage.TabIndex = 2;
			ZoomTabPage.Text = "Zoom";
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
			ZoomSettingsPanel.Margin = new Padding(4);
			ZoomSettingsPanel.Name = "ZoomSettingsPanel";
			ZoomSettingsPanel.Size = new Size(327, 243);
			ZoomSettingsPanel.TabIndex = 36;
			// 
			// ZoomFactorLabel
			// 
			ZoomFactorLabel.AutoSize = true;
			ZoomFactorLabel.Location = new Point(9, 38);
			ZoomFactorLabel.Margin = new Padding(4, 0, 4, 0);
			ZoomFactorLabel.Name = "ZoomFactorLabel";
			ZoomFactorLabel.Size = new Size(75, 15);
			ZoomFactorLabel.TabIndex = 39;
			ZoomFactorLabel.Text = "Zoom Factor";
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
			ZoomAnchorPanel.Location = new Point(94, 62);
			ZoomAnchorPanel.Margin = new Padding(4);
			ZoomAnchorPanel.Name = "ZoomAnchorPanel";
			ZoomAnchorPanel.Size = new Size(90, 84);
			ZoomAnchorPanel.TabIndex = 38;
			// 
			// ZoomAanchorNWRadioButton
			// 
			ZoomAanchorNWRadioButton.AutoSize = true;
			ZoomAanchorNWRadioButton.Location = new Point(4, 4);
			ZoomAanchorNWRadioButton.Margin = new Padding(4);
			ZoomAanchorNWRadioButton.Name = "ZoomAanchorNWRadioButton";
			ZoomAanchorNWRadioButton.Size = new Size(14, 13);
			ZoomAanchorNWRadioButton.TabIndex = 0;
			ZoomAanchorNWRadioButton.TabStop = true;
			ZoomAanchorNWRadioButton.UseVisualStyleBackColor = true;
			ZoomAanchorNWRadioButton.CheckedChanged += OptionChanged_Handler;
			// 
			// ZoomAanchorNRadioButton
			// 
			ZoomAanchorNRadioButton.AutoSize = true;
			ZoomAanchorNRadioButton.Location = new Point(36, 4);
			ZoomAanchorNRadioButton.Margin = new Padding(4);
			ZoomAanchorNRadioButton.Name = "ZoomAanchorNRadioButton";
			ZoomAanchorNRadioButton.Size = new Size(14, 13);
			ZoomAanchorNRadioButton.TabIndex = 1;
			ZoomAanchorNRadioButton.TabStop = true;
			ZoomAanchorNRadioButton.UseVisualStyleBackColor = true;
			ZoomAanchorNRadioButton.CheckedChanged += OptionChanged_Handler;
			// 
			// ZoomAanchorNERadioButton
			// 
			ZoomAanchorNERadioButton.AutoSize = true;
			ZoomAanchorNERadioButton.Location = new Point(69, 4);
			ZoomAanchorNERadioButton.Margin = new Padding(4);
			ZoomAanchorNERadioButton.Name = "ZoomAanchorNERadioButton";
			ZoomAanchorNERadioButton.Size = new Size(14, 13);
			ZoomAanchorNERadioButton.TabIndex = 2;
			ZoomAanchorNERadioButton.TabStop = true;
			ZoomAanchorNERadioButton.UseVisualStyleBackColor = true;
			ZoomAanchorNERadioButton.CheckedChanged += OptionChanged_Handler;
			// 
			// ZoomAanchorWRadioButton
			// 
			ZoomAanchorWRadioButton.AutoSize = true;
			ZoomAanchorWRadioButton.Location = new Point(4, 34);
			ZoomAanchorWRadioButton.Margin = new Padding(4);
			ZoomAanchorWRadioButton.Name = "ZoomAanchorWRadioButton";
			ZoomAanchorWRadioButton.Size = new Size(14, 13);
			ZoomAanchorWRadioButton.TabIndex = 3;
			ZoomAanchorWRadioButton.TabStop = true;
			ZoomAanchorWRadioButton.UseVisualStyleBackColor = true;
			ZoomAanchorWRadioButton.CheckedChanged += OptionChanged_Handler;
			// 
			// ZoomAanchorSERadioButton
			// 
			ZoomAanchorSERadioButton.AutoSize = true;
			ZoomAanchorSERadioButton.Location = new Point(69, 64);
			ZoomAanchorSERadioButton.Margin = new Padding(4);
			ZoomAanchorSERadioButton.Name = "ZoomAanchorSERadioButton";
			ZoomAanchorSERadioButton.Size = new Size(14, 13);
			ZoomAanchorSERadioButton.TabIndex = 8;
			ZoomAanchorSERadioButton.TabStop = true;
			ZoomAanchorSERadioButton.UseVisualStyleBackColor = true;
			ZoomAanchorSERadioButton.CheckedChanged += OptionChanged_Handler;
			// 
			// ZoomAanchorCRadioButton
			// 
			ZoomAanchorCRadioButton.AutoSize = true;
			ZoomAanchorCRadioButton.Location = new Point(36, 34);
			ZoomAanchorCRadioButton.Margin = new Padding(4);
			ZoomAanchorCRadioButton.Name = "ZoomAanchorCRadioButton";
			ZoomAanchorCRadioButton.Size = new Size(14, 13);
			ZoomAanchorCRadioButton.TabIndex = 4;
			ZoomAanchorCRadioButton.TabStop = true;
			ZoomAanchorCRadioButton.UseVisualStyleBackColor = true;
			ZoomAanchorCRadioButton.CheckedChanged += OptionChanged_Handler;
			// 
			// ZoomAanchorSRadioButton
			// 
			ZoomAanchorSRadioButton.AutoSize = true;
			ZoomAanchorSRadioButton.Location = new Point(36, 64);
			ZoomAanchorSRadioButton.Margin = new Padding(4);
			ZoomAanchorSRadioButton.Name = "ZoomAanchorSRadioButton";
			ZoomAanchorSRadioButton.Size = new Size(14, 13);
			ZoomAanchorSRadioButton.TabIndex = 7;
			ZoomAanchorSRadioButton.TabStop = true;
			ZoomAanchorSRadioButton.UseVisualStyleBackColor = true;
			ZoomAanchorSRadioButton.CheckedChanged += OptionChanged_Handler;
			// 
			// ZoomAanchorERadioButton
			// 
			ZoomAanchorERadioButton.AutoSize = true;
			ZoomAanchorERadioButton.Location = new Point(69, 34);
			ZoomAanchorERadioButton.Margin = new Padding(4);
			ZoomAanchorERadioButton.Name = "ZoomAanchorERadioButton";
			ZoomAanchorERadioButton.Size = new Size(14, 13);
			ZoomAanchorERadioButton.TabIndex = 5;
			ZoomAanchorERadioButton.TabStop = true;
			ZoomAanchorERadioButton.UseVisualStyleBackColor = true;
			ZoomAanchorERadioButton.CheckedChanged += OptionChanged_Handler;
			// 
			// ZoomAanchorSWRadioButton
			// 
			ZoomAanchorSWRadioButton.AutoSize = true;
			ZoomAanchorSWRadioButton.Location = new Point(4, 64);
			ZoomAanchorSWRadioButton.Margin = new Padding(4);
			ZoomAanchorSWRadioButton.Name = "ZoomAanchorSWRadioButton";
			ZoomAanchorSWRadioButton.Size = new Size(14, 13);
			ZoomAanchorSWRadioButton.TabIndex = 6;
			ZoomAanchorSWRadioButton.TabStop = true;
			ZoomAanchorSWRadioButton.UseVisualStyleBackColor = true;
			ZoomAanchorSWRadioButton.CheckedChanged += OptionChanged_Handler;
			// 
			// ZoomAnchorLabel
			// 
			ZoomAnchorLabel.AutoSize = true;
			ZoomAnchorLabel.Location = new Point(9, 66);
			ZoomAnchorLabel.Margin = new Padding(4, 0, 4, 0);
			ZoomAnchorLabel.Name = "ZoomAnchorLabel";
			ZoomAnchorLabel.Size = new Size(46, 15);
			ZoomAnchorLabel.TabIndex = 40;
			ZoomAnchorLabel.Text = "Anchor";
			// 
			// EnableThumbnailZoomCheckBox
			// 
			EnableThumbnailZoomCheckBox.AutoSize = true;
			EnableThumbnailZoomCheckBox.Checked = true;
			EnableThumbnailZoomCheckBox.CheckState = CheckState.Checked;
			EnableThumbnailZoomCheckBox.Location = new Point(9, 8);
			EnableThumbnailZoomCheckBox.Margin = new Padding(4);
			EnableThumbnailZoomCheckBox.Name = "EnableThumbnailZoomCheckBox";
			EnableThumbnailZoomCheckBox.RightToLeft = RightToLeft.No;
			EnableThumbnailZoomCheckBox.Size = new Size(108, 19);
			EnableThumbnailZoomCheckBox.TabIndex = 36;
			EnableThumbnailZoomCheckBox.Text = "Zoom on hover";
			EnableThumbnailZoomCheckBox.UseVisualStyleBackColor = true;
			EnableThumbnailZoomCheckBox.CheckedChanged += OptionChanged_Handler;
			// 
			// ThumbnailZoomFactorNumericEdit
			// 
			ThumbnailZoomFactorNumericEdit.BackColor = SystemColors.Window;
			ThumbnailZoomFactorNumericEdit.BorderStyle = BorderStyle.FixedSingle;
			ThumbnailZoomFactorNumericEdit.Location = new Point(94, 36);
			ThumbnailZoomFactorNumericEdit.Margin = new Padding(4);
			ThumbnailZoomFactorNumericEdit.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
			ThumbnailZoomFactorNumericEdit.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
			ThumbnailZoomFactorNumericEdit.Name = "ThumbnailZoomFactorNumericEdit";
			ThumbnailZoomFactorNumericEdit.Size = new Size(44, 23);
			ThumbnailZoomFactorNumericEdit.TabIndex = 37;
			ThumbnailZoomFactorNumericEdit.Value = new decimal(new int[] { 2, 0, 0, 0 });
			ThumbnailZoomFactorNumericEdit.ValueChanged += OptionChanged_Handler;
			// 
			// OverlayTabPage
			// 
			OverlayTabPage.BackColor = SystemColors.Control;
			OverlayTabPage.Controls.Add(OverlaySettingsPanel);
			OverlayTabPage.Location = new Point(124, 4);
			OverlayTabPage.Margin = new Padding(4);
			OverlayTabPage.Name = "OverlayTabPage";
			OverlayTabPage.Size = new Size(327, 243);
			OverlayTabPage.TabIndex = 3;
			OverlayTabPage.Text = "Overlay";
			// 
			// OverlaySettingsPanel
			// 
			OverlaySettingsPanel.BorderStyle = BorderStyle.FixedSingle;
			OverlaySettingsPanel.Controls.Add(label5);
			OverlaySettingsPanel.Controls.Add(panel2);
			OverlaySettingsPanel.Controls.Add(LabelOverlayLabelFont);
			OverlaySettingsPanel.Controls.Add(btnLabelFont);
			OverlaySettingsPanel.Controls.Add(label3);
			OverlaySettingsPanel.Controls.Add(label2);
			OverlaySettingsPanel.Controls.Add(OverlayLabelColorButton);
			OverlaySettingsPanel.Controls.Add(panel1);
			OverlaySettingsPanel.Controls.Add(HighlightColorLabel);
			OverlaySettingsPanel.Controls.Add(ActiveClientHighlightColorButton);
			OverlaySettingsPanel.Controls.Add(EnableActiveClientHighlightCheckBox);
			OverlaySettingsPanel.Controls.Add(ShowThumbnailOverlaysCheckBox);
			OverlaySettingsPanel.Controls.Add(ShowThumbnailFramesCheckBox);
			OverlaySettingsPanel.Dock = DockStyle.Fill;
			OverlaySettingsPanel.Location = new Point(0, 0);
			OverlaySettingsPanel.Margin = new Padding(4);
			OverlaySettingsPanel.Name = "OverlaySettingsPanel";
			OverlaySettingsPanel.Size = new Size(327, 243);
			OverlaySettingsPanel.TabIndex = 25;
			// 
			// label5
			// 
			label5.AutoSize = true;
			label5.Location = new Point(121, 31);
			label5.Margin = new Padding(4, 0, 4, 0);
			label5.Name = "label5";
			label5.Size = new Size(168, 15);
			label5.TabIndex = 47;
			label5.Text = "Cycle Group Indicator Position";
			// 
			// panel2
			// 
			panel2.BorderStyle = BorderStyle.FixedSingle;
			panel2.Controls.Add(CycleGroupIndicatorNWRadioButton);
			panel2.Controls.Add(CycleGroupIndicatorNRadioButton);
			panel2.Controls.Add(CycleGroupIndicatorNERadioButton);
			panel2.Controls.Add(CycleGroupIndicatorWRadioButton);
			panel2.Controls.Add(CycleGroupIndicatorSERadioButton);
			panel2.Controls.Add(CycleGroupIndicatorCRadioButton);
			panel2.Controls.Add(CycleGroupIndicatorSRadioButton);
			panel2.Controls.Add(CycleGroupIndicatorERadioButton);
			panel2.Controls.Add(CycleGroupIndicatorSWRadioButton);
			panel2.Location = new Point(214, 50);
			panel2.Margin = new Padding(4);
			panel2.Name = "panel2";
			panel2.Size = new Size(73, 66);
			panel2.TabIndex = 46;
			// 
			// CycleGroupIndicatorNWRadioButton
			// 
			CycleGroupIndicatorNWRadioButton.AutoSize = true;
			CycleGroupIndicatorNWRadioButton.Location = new Point(4, 4);
			CycleGroupIndicatorNWRadioButton.Margin = new Padding(4);
			CycleGroupIndicatorNWRadioButton.Name = "CycleGroupIndicatorNWRadioButton";
			CycleGroupIndicatorNWRadioButton.Size = new Size(14, 13);
			CycleGroupIndicatorNWRadioButton.TabIndex = 0;
			CycleGroupIndicatorNWRadioButton.TabStop = true;
			CycleGroupIndicatorNWRadioButton.UseVisualStyleBackColor = true;
			CycleGroupIndicatorNWRadioButton.CheckedChanged += OptionChanged_Handler;
			// 
			// CycleGroupIndicatorNRadioButton
			// 
			CycleGroupIndicatorNRadioButton.AutoSize = true;
			CycleGroupIndicatorNRadioButton.Location = new Point(27, 4);
			CycleGroupIndicatorNRadioButton.Margin = new Padding(4);
			CycleGroupIndicatorNRadioButton.Name = "CycleGroupIndicatorNRadioButton";
			CycleGroupIndicatorNRadioButton.Size = new Size(14, 13);
			CycleGroupIndicatorNRadioButton.TabIndex = 1;
			CycleGroupIndicatorNRadioButton.TabStop = true;
			CycleGroupIndicatorNRadioButton.UseVisualStyleBackColor = true;
			CycleGroupIndicatorNRadioButton.CheckedChanged += OptionChanged_Handler;
			// 
			// CycleGroupIndicatorNERadioButton
			// 
			CycleGroupIndicatorNERadioButton.AutoSize = true;
			CycleGroupIndicatorNERadioButton.Location = new Point(50, 4);
			CycleGroupIndicatorNERadioButton.Margin = new Padding(4);
			CycleGroupIndicatorNERadioButton.Name = "CycleGroupIndicatorNERadioButton";
			CycleGroupIndicatorNERadioButton.Size = new Size(14, 13);
			CycleGroupIndicatorNERadioButton.TabIndex = 2;
			CycleGroupIndicatorNERadioButton.TabStop = true;
			CycleGroupIndicatorNERadioButton.UseVisualStyleBackColor = true;
			CycleGroupIndicatorNERadioButton.CheckedChanged += OptionChanged_Handler;
			// 
			// CycleGroupIndicatorWRadioButton
			// 
			CycleGroupIndicatorWRadioButton.AutoSize = true;
			CycleGroupIndicatorWRadioButton.Location = new Point(4, 25);
			CycleGroupIndicatorWRadioButton.Margin = new Padding(4);
			CycleGroupIndicatorWRadioButton.Name = "CycleGroupIndicatorWRadioButton";
			CycleGroupIndicatorWRadioButton.Size = new Size(14, 13);
			CycleGroupIndicatorWRadioButton.TabIndex = 3;
			CycleGroupIndicatorWRadioButton.TabStop = true;
			CycleGroupIndicatorWRadioButton.UseVisualStyleBackColor = true;
			CycleGroupIndicatorWRadioButton.CheckedChanged += OptionChanged_Handler;
			// 
			// CycleGroupIndicatorSERadioButton
			// 
			CycleGroupIndicatorSERadioButton.AutoSize = true;
			CycleGroupIndicatorSERadioButton.Location = new Point(50, 46);
			CycleGroupIndicatorSERadioButton.Margin = new Padding(4);
			CycleGroupIndicatorSERadioButton.Name = "CycleGroupIndicatorSERadioButton";
			CycleGroupIndicatorSERadioButton.Size = new Size(14, 13);
			CycleGroupIndicatorSERadioButton.TabIndex = 8;
			CycleGroupIndicatorSERadioButton.TabStop = true;
			CycleGroupIndicatorSERadioButton.UseVisualStyleBackColor = true;
			CycleGroupIndicatorSERadioButton.CheckedChanged += OptionChanged_Handler;
			// 
			// CycleGroupIndicatorCRadioButton
			// 
			CycleGroupIndicatorCRadioButton.AutoSize = true;
			CycleGroupIndicatorCRadioButton.Location = new Point(27, 25);
			CycleGroupIndicatorCRadioButton.Margin = new Padding(4);
			CycleGroupIndicatorCRadioButton.Name = "CycleGroupIndicatorCRadioButton";
			CycleGroupIndicatorCRadioButton.Size = new Size(14, 13);
			CycleGroupIndicatorCRadioButton.TabIndex = 4;
			CycleGroupIndicatorCRadioButton.TabStop = true;
			CycleGroupIndicatorCRadioButton.UseVisualStyleBackColor = true;
			CycleGroupIndicatorCRadioButton.CheckedChanged += OptionChanged_Handler;
			// 
			// CycleGroupIndicatorSRadioButton
			// 
			CycleGroupIndicatorSRadioButton.AutoSize = true;
			CycleGroupIndicatorSRadioButton.Location = new Point(27, 46);
			CycleGroupIndicatorSRadioButton.Margin = new Padding(4);
			CycleGroupIndicatorSRadioButton.Name = "CycleGroupIndicatorSRadioButton";
			CycleGroupIndicatorSRadioButton.Size = new Size(14, 13);
			CycleGroupIndicatorSRadioButton.TabIndex = 7;
			CycleGroupIndicatorSRadioButton.TabStop = true;
			CycleGroupIndicatorSRadioButton.UseVisualStyleBackColor = true;
			CycleGroupIndicatorSRadioButton.CheckedChanged += OptionChanged_Handler;
			// 
			// CycleGroupIndicatorERadioButton
			// 
			CycleGroupIndicatorERadioButton.AutoSize = true;
			CycleGroupIndicatorERadioButton.Location = new Point(50, 25);
			CycleGroupIndicatorERadioButton.Margin = new Padding(4);
			CycleGroupIndicatorERadioButton.Name = "CycleGroupIndicatorERadioButton";
			CycleGroupIndicatorERadioButton.Size = new Size(14, 13);
			CycleGroupIndicatorERadioButton.TabIndex = 5;
			CycleGroupIndicatorERadioButton.TabStop = true;
			CycleGroupIndicatorERadioButton.UseVisualStyleBackColor = true;
			CycleGroupIndicatorERadioButton.CheckedChanged += OptionChanged_Handler;
			// 
			// CycleGroupIndicatorSWRadioButton
			// 
			CycleGroupIndicatorSWRadioButton.AutoSize = true;
			CycleGroupIndicatorSWRadioButton.Location = new Point(4, 46);
			CycleGroupIndicatorSWRadioButton.Margin = new Padding(4);
			CycleGroupIndicatorSWRadioButton.Name = "CycleGroupIndicatorSWRadioButton";
			CycleGroupIndicatorSWRadioButton.Size = new Size(14, 13);
			CycleGroupIndicatorSWRadioButton.TabIndex = 6;
			CycleGroupIndicatorSWRadioButton.TabStop = true;
			CycleGroupIndicatorSWRadioButton.UseVisualStyleBackColor = true;
			CycleGroupIndicatorSWRadioButton.CheckedChanged += OptionChanged_Handler;
			// 
			// LabelOverlayLabelFont
			// 
			LabelOverlayLabelFont.AutoSize = true;
			LabelOverlayLabelFont.Location = new Point(9, 142);
			LabelOverlayLabelFont.Margin = new Padding(4, 0, 4, 0);
			LabelOverlayLabelFont.Name = "LabelOverlayLabelFont";
			LabelOverlayLabelFont.Size = new Size(47, 15);
			LabelOverlayLabelFont.TabIndex = 45;
			LabelOverlayLabelFont.Text = "Overlay";
			// 
			// btnLabelFont
			// 
			btnLabelFont.Location = new Point(6, 114);
			btnLabelFont.Margin = new Padding(2);
			btnLabelFont.Name = "btnLabelFont";
			btnLabelFont.Size = new Size(87, 26);
			btnLabelFont.TabIndex = 44;
			btnLabelFont.Text = "Label Font";
			btnLabelFont.UseVisualStyleBackColor = true;
			btnLabelFont.Click += btnLabelFont_Click;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(236, 142);
			label3.Margin = new Padding(4, 0, 4, 0);
			label3.Name = "label3";
			label3.Size = new Size(50, 15);
			label3.TabIndex = 43;
			label3.Text = "Position";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(6, 207);
			label2.Margin = new Padding(4, 0, 4, 0);
			label2.Name = "label2";
			label2.Size = new Size(36, 15);
			label2.TabIndex = 42;
			label2.Text = "Color";
			// 
			// OverlayLabelColorButton
			// 
			OverlayLabelColorButton.BorderStyle = BorderStyle.FixedSingle;
			OverlayLabelColorButton.Location = new Point(49, 206);
			OverlayLabelColorButton.Margin = new Padding(4);
			OverlayLabelColorButton.Name = "OverlayLabelColorButton";
			OverlayLabelColorButton.Size = new Size(108, 19);
			OverlayLabelColorButton.TabIndex = 41;
			OverlayLabelColorButton.Click += OverlayLabelColorButton_Click;
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
			panel1.Location = new Point(216, 159);
			panel1.Margin = new Padding(4);
			panel1.Name = "panel1";
			panel1.Size = new Size(73, 66);
			panel1.TabIndex = 39;
			// 
			// OverlayLabelNWRadioButton
			// 
			OverlayLabelNWRadioButton.AutoSize = true;
			OverlayLabelNWRadioButton.Location = new Point(4, 4);
			OverlayLabelNWRadioButton.Margin = new Padding(4);
			OverlayLabelNWRadioButton.Name = "OverlayLabelNWRadioButton";
			OverlayLabelNWRadioButton.Size = new Size(14, 13);
			OverlayLabelNWRadioButton.TabIndex = 0;
			OverlayLabelNWRadioButton.TabStop = true;
			OverlayLabelNWRadioButton.UseVisualStyleBackColor = true;
			OverlayLabelNWRadioButton.CheckedChanged += OptionChanged_Handler;
			// 
			// OverlayLabelNRadioButton
			// 
			OverlayLabelNRadioButton.AutoSize = true;
			OverlayLabelNRadioButton.Location = new Point(27, 4);
			OverlayLabelNRadioButton.Margin = new Padding(4);
			OverlayLabelNRadioButton.Name = "OverlayLabelNRadioButton";
			OverlayLabelNRadioButton.Size = new Size(14, 13);
			OverlayLabelNRadioButton.TabIndex = 1;
			OverlayLabelNRadioButton.TabStop = true;
			OverlayLabelNRadioButton.UseVisualStyleBackColor = true;
			OverlayLabelNRadioButton.CheckedChanged += OptionChanged_Handler;
			// 
			// OverlayLabelNERadioButton
			// 
			OverlayLabelNERadioButton.AutoSize = true;
			OverlayLabelNERadioButton.Location = new Point(50, 4);
			OverlayLabelNERadioButton.Margin = new Padding(4);
			OverlayLabelNERadioButton.Name = "OverlayLabelNERadioButton";
			OverlayLabelNERadioButton.Size = new Size(14, 13);
			OverlayLabelNERadioButton.TabIndex = 2;
			OverlayLabelNERadioButton.TabStop = true;
			OverlayLabelNERadioButton.UseVisualStyleBackColor = true;
			OverlayLabelNERadioButton.CheckedChanged += OptionChanged_Handler;
			// 
			// OverlayLabelWRadioButton
			// 
			OverlayLabelWRadioButton.AutoSize = true;
			OverlayLabelWRadioButton.Location = new Point(4, 25);
			OverlayLabelWRadioButton.Margin = new Padding(4);
			OverlayLabelWRadioButton.Name = "OverlayLabelWRadioButton";
			OverlayLabelWRadioButton.Size = new Size(14, 13);
			OverlayLabelWRadioButton.TabIndex = 3;
			OverlayLabelWRadioButton.TabStop = true;
			OverlayLabelWRadioButton.UseVisualStyleBackColor = true;
			OverlayLabelWRadioButton.CheckedChanged += OptionChanged_Handler;
			// 
			// OverlayLabelSERadioButton
			// 
			OverlayLabelSERadioButton.AutoSize = true;
			OverlayLabelSERadioButton.Location = new Point(50, 46);
			OverlayLabelSERadioButton.Margin = new Padding(4);
			OverlayLabelSERadioButton.Name = "OverlayLabelSERadioButton";
			OverlayLabelSERadioButton.Size = new Size(14, 13);
			OverlayLabelSERadioButton.TabIndex = 8;
			OverlayLabelSERadioButton.TabStop = true;
			OverlayLabelSERadioButton.UseVisualStyleBackColor = true;
			OverlayLabelSERadioButton.CheckedChanged += OptionChanged_Handler;
			// 
			// OverlayLabelCRadioButton
			// 
			OverlayLabelCRadioButton.AutoSize = true;
			OverlayLabelCRadioButton.Location = new Point(27, 25);
			OverlayLabelCRadioButton.Margin = new Padding(4);
			OverlayLabelCRadioButton.Name = "OverlayLabelCRadioButton";
			OverlayLabelCRadioButton.Size = new Size(14, 13);
			OverlayLabelCRadioButton.TabIndex = 4;
			OverlayLabelCRadioButton.TabStop = true;
			OverlayLabelCRadioButton.UseVisualStyleBackColor = true;
			OverlayLabelCRadioButton.CheckedChanged += OptionChanged_Handler;
			// 
			// OverlayLabelSRadioButton
			// 
			OverlayLabelSRadioButton.AutoSize = true;
			OverlayLabelSRadioButton.Location = new Point(27, 46);
			OverlayLabelSRadioButton.Margin = new Padding(4);
			OverlayLabelSRadioButton.Name = "OverlayLabelSRadioButton";
			OverlayLabelSRadioButton.Size = new Size(14, 13);
			OverlayLabelSRadioButton.TabIndex = 7;
			OverlayLabelSRadioButton.TabStop = true;
			OverlayLabelSRadioButton.UseVisualStyleBackColor = true;
			OverlayLabelSRadioButton.CheckedChanged += OptionChanged_Handler;
			// 
			// OverlayLabelERadioButton
			// 
			OverlayLabelERadioButton.AutoSize = true;
			OverlayLabelERadioButton.Location = new Point(50, 25);
			OverlayLabelERadioButton.Margin = new Padding(4);
			OverlayLabelERadioButton.Name = "OverlayLabelERadioButton";
			OverlayLabelERadioButton.Size = new Size(14, 13);
			OverlayLabelERadioButton.TabIndex = 5;
			OverlayLabelERadioButton.TabStop = true;
			OverlayLabelERadioButton.UseVisualStyleBackColor = true;
			OverlayLabelERadioButton.CheckedChanged += OptionChanged_Handler;
			// 
			// OverlayLabelSWRadioButton
			// 
			OverlayLabelSWRadioButton.AutoSize = true;
			OverlayLabelSWRadioButton.Location = new Point(4, 46);
			OverlayLabelSWRadioButton.Margin = new Padding(4);
			OverlayLabelSWRadioButton.Name = "OverlayLabelSWRadioButton";
			OverlayLabelSWRadioButton.Size = new Size(14, 13);
			OverlayLabelSWRadioButton.TabIndex = 6;
			OverlayLabelSWRadioButton.TabStop = true;
			OverlayLabelSWRadioButton.UseVisualStyleBackColor = true;
			OverlayLabelSWRadioButton.CheckedChanged += OptionChanged_Handler;
			// 
			// HighlightColorLabel
			// 
			HighlightColorLabel.AutoSize = true;
			HighlightColorLabel.Location = new Point(6, 90);
			HighlightColorLabel.Margin = new Padding(4, 0, 4, 0);
			HighlightColorLabel.Name = "HighlightColorLabel";
			HighlightColorLabel.Size = new Size(36, 15);
			HighlightColorLabel.TabIndex = 29;
			HighlightColorLabel.Text = "Color";
			// 
			// ActiveClientHighlightColorButton
			// 
			ActiveClientHighlightColorButton.BorderStyle = BorderStyle.FixedSingle;
			ActiveClientHighlightColorButton.Location = new Point(49, 89);
			ActiveClientHighlightColorButton.Margin = new Padding(4);
			ActiveClientHighlightColorButton.Name = "ActiveClientHighlightColorButton";
			ActiveClientHighlightColorButton.Size = new Size(108, 19);
			ActiveClientHighlightColorButton.TabIndex = 28;
			ActiveClientHighlightColorButton.Click += ActiveClientHighlightColorButton_Click;
			// 
			// EnableActiveClientHighlightCheckBox
			// 
			EnableActiveClientHighlightCheckBox.AutoSize = true;
			EnableActiveClientHighlightCheckBox.Checked = true;
			EnableActiveClientHighlightCheckBox.CheckState = CheckState.Checked;
			EnableActiveClientHighlightCheckBox.Location = new Point(9, 64);
			EnableActiveClientHighlightCheckBox.Margin = new Padding(4);
			EnableActiveClientHighlightCheckBox.Name = "EnableActiveClientHighlightCheckBox";
			EnableActiveClientHighlightCheckBox.RightToLeft = RightToLeft.No;
			EnableActiveClientHighlightCheckBox.Size = new Size(142, 19);
			EnableActiveClientHighlightCheckBox.TabIndex = 27;
			EnableActiveClientHighlightCheckBox.Text = "Highlight active client";
			EnableActiveClientHighlightCheckBox.UseVisualStyleBackColor = true;
			EnableActiveClientHighlightCheckBox.CheckedChanged += OptionChanged_Handler;
			// 
			// ShowThumbnailOverlaysCheckBox
			// 
			ShowThumbnailOverlaysCheckBox.AutoSize = true;
			ShowThumbnailOverlaysCheckBox.Checked = true;
			ShowThumbnailOverlaysCheckBox.CheckState = CheckState.Checked;
			ShowThumbnailOverlaysCheckBox.Location = new Point(9, 8);
			ShowThumbnailOverlaysCheckBox.Margin = new Padding(4);
			ShowThumbnailOverlaysCheckBox.Name = "ShowThumbnailOverlaysCheckBox";
			ShowThumbnailOverlaysCheckBox.RightToLeft = RightToLeft.No;
			ShowThumbnailOverlaysCheckBox.Size = new Size(96, 19);
			ShowThumbnailOverlaysCheckBox.TabIndex = 25;
			ShowThumbnailOverlaysCheckBox.Text = "Show overlay";
			ShowThumbnailOverlaysCheckBox.UseVisualStyleBackColor = true;
			ShowThumbnailOverlaysCheckBox.CheckedChanged += OptionChanged_Handler;
			// 
			// ShowThumbnailFramesCheckBox
			// 
			ShowThumbnailFramesCheckBox.AutoSize = true;
			ShowThumbnailFramesCheckBox.Checked = true;
			ShowThumbnailFramesCheckBox.CheckState = CheckState.Checked;
			ShowThumbnailFramesCheckBox.Location = new Point(9, 36);
			ShowThumbnailFramesCheckBox.Margin = new Padding(4);
			ShowThumbnailFramesCheckBox.Name = "ShowThumbnailFramesCheckBox";
			ShowThumbnailFramesCheckBox.RightToLeft = RightToLeft.No;
			ShowThumbnailFramesCheckBox.Size = new Size(94, 19);
			ShowThumbnailFramesCheckBox.TabIndex = 26;
			ShowThumbnailFramesCheckBox.Text = "Show frames";
			ShowThumbnailFramesCheckBox.UseVisualStyleBackColor = true;
			ShowThumbnailFramesCheckBox.CheckedChanged += OptionChanged_Handler;
			// 
			// ClientsTabPage
			// 
			ClientsTabPage.BackColor = SystemColors.Control;
			ClientsTabPage.Controls.Add(ClientsPanel);
			ClientsTabPage.Location = new Point(124, 4);
			ClientsTabPage.Margin = new Padding(4);
			ClientsTabPage.Name = "ClientsTabPage";
			ClientsTabPage.Size = new Size(327, 243);
			ClientsTabPage.TabIndex = 4;
			ClientsTabPage.Text = "Active Clients";
			// 
			// ClientsPanel
			// 
			ClientsPanel.BorderStyle = BorderStyle.FixedSingle;
			ClientsPanel.Controls.Add(ThumbnailsList);
			ClientsPanel.Controls.Add(ThumbnailsListLabel);
			ClientsPanel.Dock = DockStyle.Fill;
			ClientsPanel.Location = new Point(0, 0);
			ClientsPanel.Margin = new Padding(4);
			ClientsPanel.Name = "ClientsPanel";
			ClientsPanel.Size = new Size(327, 243);
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
			ThumbnailsList.Location = new Point(0, 34);
			ThumbnailsList.Margin = new Padding(4);
			ThumbnailsList.Name = "ThumbnailsList";
			ThumbnailsList.Size = new Size(325, 207);
			ThumbnailsList.TabIndex = 34;
			ThumbnailsList.ItemCheck += ThumbnailsList_ItemCheck_Handler;
			// 
			// ThumbnailsListLabel
			// 
			ThumbnailsListLabel.AutoSize = true;
			ThumbnailsListLabel.Location = new Point(9, 10);
			ThumbnailsListLabel.Margin = new Padding(4, 0, 4, 0);
			ThumbnailsListLabel.Name = "ThumbnailsListLabel";
			ThumbnailsListLabel.Size = new Size(182, 15);
			ThumbnailsListLabel.TabIndex = 33;
			ThumbnailsListLabel.Text = "Thumbnails (check to force hide)";
			// 
			// AboutTabPage
			// 
			AboutTabPage.BackColor = SystemColors.Control;
			AboutTabPage.Controls.Add(AboutPanel);
			AboutTabPage.Location = new Point(124, 4);
			AboutTabPage.Margin = new Padding(4);
			AboutTabPage.Name = "AboutTabPage";
			AboutTabPage.Size = new Size(327, 243);
			AboutTabPage.TabIndex = 5;
			AboutTabPage.Text = "About";
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
			AboutPanel.Margin = new Padding(4);
			AboutPanel.Name = "AboutPanel";
			AboutPanel.Size = new Size(327, 243);
			AboutPanel.TabIndex = 2;
			// 
			// CreditMaintLabel
			// 
			CreditMaintLabel.AutoSize = true;
			CreditMaintLabel.Location = new Point(0, 165);
			CreditMaintLabel.Margin = new Padding(4, 0, 4, 0);
			CreditMaintLabel.Name = "CreditMaintLabel";
			CreditMaintLabel.Padding = new Padding(9, 4, 9, 4);
			CreditMaintLabel.Size = new Size(292, 23);
			CreditMaintLabel.TabIndex = 7;
			CreditMaintLabel.Text = "Credit to previous maintainer: Phrynohyas Tig-Rah";
			// 
			// DocumentationLinkLabel
			// 
			DocumentationLinkLabel.AutoSize = true;
			DocumentationLinkLabel.Location = new Point(0, 188);
			DocumentationLinkLabel.Margin = new Padding(4, 0, 4, 0);
			DocumentationLinkLabel.Name = "DocumentationLinkLabel";
			DocumentationLinkLabel.Padding = new Padding(9, 4, 9, 4);
			DocumentationLinkLabel.Size = new Size(259, 23);
			DocumentationLinkLabel.TabIndex = 6;
			DocumentationLinkLabel.Text = "For more information visit the forum thread:";
			// 
			// DescriptionLabel
			// 
			DescriptionLabel.BackColor = Color.Transparent;
			DescriptionLabel.Location = new Point(0, 34);
			DescriptionLabel.Margin = new Padding(4, 0, 4, 0);
			DescriptionLabel.Name = "DescriptionLabel";
			DescriptionLabel.Padding = new Padding(9, 4, 9, 4);
			DescriptionLabel.Size = new Size(304, 167);
			DescriptionLabel.TabIndex = 5;
			DescriptionLabel.Text = resources.GetString("DescriptionLabel.Text");
			// 
			// VersionLabel
			// 
			VersionLabel.AutoSize = true;
			VersionLabel.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
			VersionLabel.Location = new Point(155, 10);
			VersionLabel.Margin = new Padding(4, 0, 4, 0);
			VersionLabel.Name = "VersionLabel";
			VersionLabel.Size = new Size(49, 20);
			VersionLabel.TabIndex = 4;
			VersionLabel.Text = "1.0.0";
			// 
			// NameLabel
			// 
			NameLabel.AutoSize = true;
			NameLabel.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
			NameLabel.Location = new Point(5, 10);
			NameLabel.Margin = new Padding(4, 0, 4, 0);
			NameLabel.Name = "NameLabel";
			NameLabel.Size = new Size(131, 20);
			NameLabel.TabIndex = 3;
			NameLabel.Text = "EVE-O-Preview";
			// 
			// DocumentationLink
			// 
			DocumentationLink.Location = new Point(0, 204);
			DocumentationLink.Margin = new Padding(35, 4, 4, 4);
			DocumentationLink.Name = "DocumentationLink";
			DocumentationLink.Padding = new Padding(9, 4, 9, 4);
			DocumentationLink.Size = new Size(306, 38);
			DocumentationLink.TabIndex = 2;
			DocumentationLink.TabStop = true;
			DocumentationLink.Text = "to be set from prresenter to be set from prresenter to be set from prresenter to be set from prresenter";
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
			TrayMenu.Items.AddRange(new ToolStripItem[] { TitleMenuItem, RestoreWindowMenuItem, CloseAllEveClientsMenuItem, SeparatorMenuItem, ExitMenuItem });
			TrayMenu.Name = "contextMenuStrip1";
			TrayMenu.Size = new Size(154, 99);
			// 
			// MainForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = SystemColors.Control;
			ClientSize = new Size(455, 340);
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
			((System.ComponentModel.ISupportInitialize)FocusedThumbnailWidthNumericEdit).EndInit();
			((System.ComponentModel.ISupportInitialize)FocusedThumbnailHeightNumericEdit).EndInit();
			((System.ComponentModel.ISupportInitialize)FocusedThumbnailLocationXNumericEdit).EndInit();
			((System.ComponentModel.ISupportInitialize)FocusedThumbnailLocationYNumericEdit).EndInit();
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
			panel2.ResumeLayout(false);
			panel2.PerformLayout();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			ClientsTabPage.ResumeLayout(false);
			ClientsPanel.ResumeLayout(false);
			ClientsPanel.PerformLayout();
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
		private CheckBox OnlyRegisterCycleHotkeysWhenEveFocusedCheckBox;
		private CheckBox DynamicCycleGroupCheckBox;
		private CheckBox EnablePerClientThumbnailsLayoutsCheckBox;
		private CheckBox MinimizeToTrayCheckBox;
		private Button CloseAllEveClientsButton;
		private NumericUpDown ThumbnailsWidthNumericEdit;
		private NumericUpDown ThumbnailsHeightNumericEdit;
		private Label OverwatchWidthLabel;
		private Label OverwatchHeightLabel;
		private NumericUpDown FocusedThumbnailWidthNumericEdit;
		private NumericUpDown FocusedThumbnailHeightNumericEdit;
		private Label OverwatchPosXLabel;
		private Label OverwatchPosYLabel;
		private NumericUpDown FocusedThumbnailLocationXNumericEdit;
		private NumericUpDown FocusedThumbnailLocationYNumericEdit;
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
		private ComboBox AnimationStyleCombo;
		private CheckBox HideCaptionOnClientsCheckBox;
		private Button btnLabelFont;
		private Label LabelOverlayLabelFont;
		private CheckBox PreventPreviewsCheckBox;
		private Label label1;
		private Panel PreventPreviewColorButton;
		private Label label5;
		private Panel panel2;
		private RadioButton CycleGroupIndicatorNWRadioButton;
		private RadioButton CycleGroupIndicatorNRadioButton;
		private RadioButton CycleGroupIndicatorNERadioButton;
		private RadioButton CycleGroupIndicatorWRadioButton;
		private RadioButton CycleGroupIndicatorSERadioButton;
		private RadioButton CycleGroupIndicatorCRadioButton;
		private RadioButton CycleGroupIndicatorSRadioButton;
		private RadioButton CycleGroupIndicatorERadioButton;
		private RadioButton CycleGroupIndicatorSWRadioButton;
	}
}