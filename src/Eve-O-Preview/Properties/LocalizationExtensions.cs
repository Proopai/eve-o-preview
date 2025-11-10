using System;
using System.Windows.Forms;


namespace EveOPreview.Properties
{
    public static class LocalizationExtensions
    {
        public static void ApplyLocalization(this Control control)
        {
            // Handle TabControl (and TabPage) separately to avoid type checking conflicts
            if (control is TabControl tabControl)
            {
                // Set text for TabPage controls
                foreach (TabPage tabPage in tabControl.TabPages)
                {
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
                    
                    // Recursively apply localization to TabPage child controls
                    foreach (Control child in tabPage.Controls)
                    {
                        ApplyLocalization(child);
                    }
                }
                return;
            }
            
            // Handle container controls
            if (control is Panel panel)
            {
                // This will catch Panel, FlowLayoutPanel, TableLayoutPanel and other Panel-derived controls
                // Recursively apply localization to child controls for container controls
                foreach (Control child in panel.Controls)
                {
                    ApplyLocalization(child);
                }
                return;
            }
            
            if (control is GroupBox groupBox)
            {
                // Recursively apply localization to child controls for GroupBox
                foreach (Control child in groupBox.Controls)
                {
                    ApplyLocalization(child);
                }
                return;
            }
            
            if (control is UserControl userControl)
            {
                // Recursively apply localization to child controls for UserControl
                foreach (Control child in userControl.Controls)
                {
                    ApplyLocalization(child);
                }
                return;
            }
            
            if (control is SplitContainer splitContainer)
            {
                // Recursively apply localization to child controls for SplitContainer
                // Handle both panels of the SplitContainer
                // Panel1 and Panel2 are never null, they are automatically created with the SplitContainer
                foreach (Control child in splitContainer.Panel1.Controls)
                {
                    ApplyLocalization(child);
                }
                foreach (Control child in splitContainer.Panel2.Controls)
                {
                    ApplyLocalization(child);
                }
                return;
            }
            
            // Handle specific control types that are not in containers
            if (control is CheckBox checkBox)
            {
                switch (checkBox.Name)
                {
                    case "MinimizeInactiveClientsCheckBox":
                        checkBox.Text = LocalizationManager.GetString("MinimizeInactiveClientsCheckBoxText");
                        break;
                    case "EnableClientLayoutTrackingCheckBox":
                        checkBox.Text = LocalizationManager.GetString("EnableClientLayoutTrackingCheckBoxText");
                        break;
                    case "HideActiveClientThumbnailCheckBox":
                        checkBox.Text = LocalizationManager.GetString("HideActiveClientThumbnailCheckBoxText");
                        break;
                    case "ShowThumbnailsAlwaysOnTopCheckBox":
                        checkBox.Text = LocalizationManager.GetString("ShowThumbnailsAlwaysOnTopCheckBoxText");
                        break;
                    case "HideThumbnailsOnLostFocusCheckBox":
                        checkBox.Text = LocalizationManager.GetString("HideThumbnailsOnLostFocusCheckBoxText");
                        break;
                    case "EnablePerClientThumbnailsLayoutsCheckBox":
                        checkBox.Text = LocalizationManager.GetString("EnablePerClientThumbnailsLayoutsCheckBoxText");
                        break;
                    case "MinimizeToTrayCheckBox":
                        checkBox.Text = LocalizationManager.GetString("MinimizeToTrayCheckBoxText");
                        break;
                    case "ThumbnailSnapToGridCheckBox":
                        checkBox.Text = LocalizationManager.GetString("ThumbnailSnapToGridCheckBoxText");
                        break;
                    case "LockThumbnailLocationCheckbox":
                        checkBox.Text = LocalizationManager.GetString("LockThumbnailLocationCheckboxText");
                        break;
                    case "EnableThumbnailZoomCheckBox":
                        checkBox.Text = LocalizationManager.GetString("EnableThumbnailZoomCheckBoxText");
                        break;
                    case "EnableActiveClientHighlightCheckBox":
                        checkBox.Text = LocalizationManager.GetString("EnableActiveClientHighlightCheckBoxText");
                        break;
                    case "ShowThumbnailOverlaysCheckBox":
                        checkBox.Text = LocalizationManager.GetString("ShowThumbnailOverlaysCheckBoxText");
                        break;
                    case "ShowThumbnailFramesCheckBox":
                        checkBox.Text = LocalizationManager.GetString("ShowThumbnailFramesCheckBoxText");
                        break;
                }
            }
            else if (control is Label label)
            {
                switch (label.Name)
                {
                    case "label4":
                        label.Text = LocalizationManager.GetString("AnimationStyleLabelText");
                        break;
                    case "HeightLabel":
                        label.Text = LocalizationManager.GetString("ThumbnailHeightLabelText");
                        break;
                    case "WidthLabel":
                        label.Text = LocalizationManager.GetString("ThumbnailWidthLabelText");
                        break;
                    case "OpacityLabel":
                        label.Text = LocalizationManager.GetString("OpacityLabelText");
                        break;
                    case "SnapXLabel":
                        label.Text = LocalizationManager.GetString("SnapXLabelText");
                        break;
                    case "SnapYLabel":
                        label.Text = LocalizationManager.GetString("SnapYLabelText");
                        break;
                    case "ZoomFactorLabel":
                        label.Text = LocalizationManager.GetString("ZoomFactorLabelText");
                        break;
                    case "ZoomAnchorLabel":
                        label.Text = LocalizationManager.GetString("ZoomAnchorLabelText");
                        break;
                    case "ThumbnailsListLabel":
                        label.Text = LocalizationManager.GetString("ThumbnailsListLabelText");
                        break;
                    case "label1":
                        label.Text = LocalizationManager.GetString("LabelSizeLabelText");
                        break;
                    case "label2":
                        label.Text = LocalizationManager.GetString("ColorLabelText");
                        break;
                    case "label3":
                        label.Text = LocalizationManager.GetString("PositionLabelText");
                        break;
                    case "HighlightColorLabel":
                        label.Text = LocalizationManager.GetString("HighlightColorLabelText");
                        break;
                    case "CreditMaintLabel":
                        label.Text = LocalizationManager.GetString("CreditMaintLabelText");
                        break;
                    case "DocumentationLinkLabel":
                        label.Text = LocalizationManager.GetString("DocumentationLinkLabelText");
                        break;
                    case "LanguageLabel":
                        label.Text = LocalizationManager.GetString("LanguageLabelText");
                        break;
                }
            }
            else if (control is ComboBox comboBox)
            {
                // Special handling for language combo box
                if (comboBox.Name == "LanguageCombo")
                {
                    int selectedIndex = comboBox.SelectedIndex;
                    comboBox.Items.Clear();
                    comboBox.Items.Add(LocalizationManager.GetString("English (en-US)"));
                    comboBox.Items.Add(LocalizationManager.GetString("中文 (zh-CN)"));
                    if (selectedIndex >= 0 && selectedIndex < comboBox.Items.Count)
                    {
                        comboBox.SelectedIndex = selectedIndex;
                    }
                }
                else
                {
                    // Handle other combo boxes if needed
                    switch (comboBox.Name)
                    {
                        case "AnimationStyleCombo":
                            // For combo boxes with DataSource, we need to handle localization differently
                            // The items are bound to an enum, so we don't need to modify the items directly
                            // The text will be displayed using the enum's ToString() method
                            // We can customize the display text by handling the Format event if needed
                            break;
                    }
                }
            }
            else if (control is Button button)
            {
                switch (button.Name)
                {
                    case "ActiveClientHighlightColorButton":
                        button.Text = LocalizationManager.GetString("HighlightColorLabelText");
                        break;
                    case "OverlayLabelColorButton":
                        button.Text = LocalizationManager.GetString("ColorLabelText");
                        break;
                }
            }
            else if (control is NumericUpDown numericUpDown)
            {
                // No specific localization needed for NumericUpDown controls
            }
            else if (control is TrackBar trackBar)
            {
                // No specific localization needed for TrackBar controls
            }
            else if (control is RadioButton radioButton)
            {
                // No specific localization needed for RadioButton controls in this application
            }
            else if (control is ListBox listBox)
            {
                // Special handling for thumbnails list
                if (listBox.Name == "ThumbnailsList")
                {
                    // The list items are IThumbnailDescription objects, their text will be updated when the objects are refreshed
                }
            }
            else if (control is LinkLabel linkLabel)
            {
                switch (linkLabel.Name)
                {
                    case "DocumentationLink":
                        // The text is set from configuration, not localization
                        break;
                }
            }
        }
        
        public static void SetFormLocalization(this Form form)
        {
            form.Text = LocalizationManager.GetString("ApplicationTitleText");
        }
        
        // Separate method to handle ToolStripMenuItems
        public static void ApplyLocalization(this ToolStripItem item)
        {
            if (item is ToolStripMenuItem menuItem)
            {
                switch (menuItem.Name)
                {
                    case "RestoreWindowMenuItem":
                        menuItem.Text = LocalizationManager.GetString("RestoreMenuItemText");
                        break;
                    case "ExitMenuItem":
                        menuItem.Text = LocalizationManager.GetString("ExitMenuItemText");
                        break;
                    case "TitleMenuItem":
                        menuItem.Text = LocalizationManager.GetString("ApplicationTitleText");
                        break;
                }
            }
        }
    }
}