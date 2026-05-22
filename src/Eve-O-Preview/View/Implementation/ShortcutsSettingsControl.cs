using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using EveOPreview.Configuration;

namespace EveOPreview.View
{
	sealed partial class ShortcutsSettingsControl : UserControl
	{
		private const int WM_KEYDOWN = 0x100;
		private const int WM_SYSKEYDOWN = 0x104;
		private const int ButtonPanelWidth = 100;
		private const int RowHeight = 28;

		private readonly List<HotkeyRow> _rows = new List<HotkeyRow>();
		private TableLayoutPanel _layout;
		private HotkeyRow _recordingRow;
		private HotkeyCaptureFilter _captureFilter;
		private bool _suppressChangeNotification;

		public Action SettingsChanged { get; set; }
		public Action SuspendGlobalHotkeys { get; set; }
		public Action ResumeGlobalHotkeys { get; set; }

		public ShortcutsSettingsControl()
		{
			this.InitializeComponent();
			this.BuildLayoutTable();
			this.BuildRows();
			this.UpdateLayoutSize();
			this.ScrollPanel.Resize += this.ScrollPanel_Resize;
			this.Resize += this.ShortcutsSettingsControl_Resize;
		}

		public GlobalShortcutSettings GetSettings()
		{
			var settings = new GlobalShortcutSettings();
			foreach (HotkeyRow row in this._rows)
			{
				row.ApplyTo(settings);
			}

			return settings;
		}

		public void SetSettings(GlobalShortcutSettings settings)
		{
			if (settings == null)
			{
				return;
			}

			this._suppressChangeNotification = true;
			try
			{
				foreach (HotkeyRow row in this._rows)
				{
					row.LoadFrom(settings);
				}
			}
			finally
			{
				this._suppressChangeNotification = false;
			}
		}

		protected override void OnHandleDestroyed(EventArgs e)
		{
			this.StopRecording();
			base.OnHandleDestroyed(e);
		}

		private void BuildLayoutTable()
		{
			this._layout = new TableLayoutPanel
			{
				AutoSize = true,
				AutoSizeMode = AutoSizeMode.GrowAndShrink,
				ColumnCount = 3,
				Dock = DockStyle.Top,
				GrowStyle = TableLayoutPanelGrowStyle.AddRows,
				Location = new Point(0, 0),
				Margin = new Padding(0),
				Padding = new Padding(4, 4, 4, 8)
			};
			this._layout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
			this._layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			this._layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, ButtonPanelWidth));

			this.ScrollPanel.Controls.Add(this._layout);
			this.UpdateLayoutSize();
		}

		private void ScrollPanel_Resize(object sender, EventArgs e)
		{
			this.UpdateLayoutSize();
		}

		private void ShortcutsSettingsControl_Resize(object sender, EventArgs e)
		{
			this.UpdateLayoutSize();
		}

		private void UpdateLayoutSize()
		{
			if (this._layout == null || this.ScrollPanel == null)
			{
				return;
			}

			int width = Math.Max(200, this.ScrollPanel.ClientSize.Width);
			if (this.ScrollPanel.VerticalScroll.Visible)
			{
				width -= SystemInformation.VerticalScrollBarWidth;
			}

			this._layout.Width = width;
			this._layout.PerformLayout();

			// PreferredSize on TableLayoutPanel can be wildly inflated; use actual height only.
			int contentHeight = this._layout.Height + this._layout.Padding.Vertical;
			if (contentHeight > this.ScrollPanel.ClientSize.Height)
			{
				this.ScrollPanel.AutoScrollMinSize = new Size(0, contentHeight);
			}
			else
			{
				this.ScrollPanel.AutoScrollMinSize = Size.Empty;
			}
		}

		private void BuildRows()
		{
			this.AddSectionHeader("Cycle groups");
			this.AddCycleGroupRow("Group 1 forward", HotkeyRowKind.CycleGroup1Forward);
			this.AddCycleGroupRow("Group 1 backward", HotkeyRowKind.CycleGroup1Backward);
			this.AddCycleGroupRow("Group 2 forward", HotkeyRowKind.CycleGroup2Forward);
			this.AddCycleGroupRow("Group 2 backward", HotkeyRowKind.CycleGroup2Backward);
			this.AddCycleGroupRow("Group 3 forward", HotkeyRowKind.CycleGroup3Forward);
			this.AddCycleGroupRow("Group 3 backward", HotkeyRowKind.CycleGroup3Backward);
			this.AddCycleGroupRow("Group 4 forward", HotkeyRowKind.CycleGroup4Forward);
			this.AddCycleGroupRow("Group 4 backward", HotkeyRowKind.CycleGroup4Backward);
			this.AddCycleGroupRow("Group 5 forward", HotkeyRowKind.CycleGroup5Forward);
			this.AddCycleGroupRow("Group 5 backward", HotkeyRowKind.CycleGroup5Backward);

			this.AddSectionHeader("Dynamic cycle (layout order; ignores General setting)");
			this.AddCycleGroupRow("Dynamic forward", HotkeyRowKind.DynamicCycleForward);
			this.AddCycleGroupRow("Dynamic backward", HotkeyRowKind.DynamicCycleBackward);

			this.AddSectionHeader("Other");
			this.AddCycleGroupRow("Minimize all clients", HotkeyRowKind.MinimizeAllClients);

			var note = new Label
			{
				AutoSize = true,
				Dock = DockStyle.Fill,
				Margin = new Padding(0, 8, 0, 0),
				Text = "Uses configured group order unless Dynamic cycle group is enabled on General. Per-client activation hotkeys are set on the Clients tab (Ctrl+click a character)."
			};
			this.AddFullWidthControl(note, SizeType.AutoSize);
		}

		private void AddSectionHeader(string text)
		{
			var header = new Label
			{
				AutoSize = true,
				Dock = DockStyle.Fill,
				Font = new Font(this.Font, FontStyle.Bold),
				Margin = new Padding(0, 10, 0, 4),
				Text = text
			};
			this.AddFullWidthControl(header, SizeType.AutoSize);
		}

		private void AddFullWidthControl(Control control, SizeType rowSizeType)
		{
			int rowIndex = this._layout.RowCount;
			this._layout.RowStyles.Add(new RowStyle(rowSizeType));
			this._layout.RowCount++;
			this._layout.Controls.Add(control, 0, rowIndex);
			this._layout.SetColumnSpan(control, 3);
		}

		private void AddCycleGroupRow(string labelText, HotkeyRowKind kind)
		{
			int rowIndex = this._layout.RowCount;
			this._layout.RowStyles.Add(new RowStyle(SizeType.Absolute, RowHeight));
			this._layout.RowCount++;

			var label = new Label
			{
				Anchor = AnchorStyles.Left,
				AutoSize = true,
				Margin = new Padding(0, 6, 4, 0),
				Text = labelText,
				TextAlign = ContentAlignment.MiddleLeft
			};

			var textBox = new TextBox
			{
				Dock = DockStyle.Fill,
				Margin = new Padding(0, 2, 0, 2),
				ReadOnly = true
			};

			var setButton = new Button
			{
				Margin = new Padding(0, 0, 2, 0),
				Size = new Size(46, 23),
				Text = "Set",
				UseVisualStyleBackColor = true
			};

			var clearButton = new Button
			{
				Margin = new Padding(0),
				Size = new Size(46, 23),
				Text = "Clear",
				UseVisualStyleBackColor = true
			};

			var buttonPanel = new FlowLayoutPanel
			{
				AutoSize = false,
				Dock = DockStyle.Fill,
				FlowDirection = FlowDirection.LeftToRight,
				Margin = new Padding(0, 2, 0, 2),
				Padding = new Padding(0),
				WrapContents = false
			};
			buttonPanel.Controls.Add(setButton);
			buttonPanel.Controls.Add(clearButton);

			this._layout.Controls.Add(label, 0, rowIndex);
			this._layout.Controls.Add(textBox, 1, rowIndex);
			this._layout.Controls.Add(buttonPanel, 2, rowIndex);

			var row = new HotkeyRow(kind, textBox, setButton);
			this._rows.Add(row);
			setButton.Click += (_, _) => this.SetButton_Click(row);
			clearButton.Click += (_, _) => this.ClearButton_Click(row);
		}

		private void ClearButton_Click(HotkeyRow row)
		{
			if (this._recordingRow == row)
			{
				this.StopRecording();
			}

			row.TextBox.Text = string.Empty;
			this.NotifySettingsChanged();
		}

		private void SetButton_Click(HotkeyRow row)
		{
			if (this._recordingRow == row)
			{
				this.StopRecording();
				return;
			}

			this.StartRecording(row);
		}

		private void StartRecording(HotkeyRow row)
		{
			this.StopRecording();
			this._recordingRow = row;
			row.SetButton.Text = "Cancel";
			row.TextBox.BackColor = SystemColors.Info;
			this.SuspendGlobalHotkeys?.Invoke();
			this._captureFilter = new HotkeyCaptureFilter(this.OnHotkeyCaptured, this.OnHotkeyCaptureCancelled);
			Application.AddMessageFilter(this._captureFilter);
		}

		private void StopRecording()
		{
			if (this._captureFilter != null)
			{
				Application.RemoveMessageFilter(this._captureFilter);
				this._captureFilter = null;
			}

			if (this._recordingRow != null)
			{
				this._recordingRow.SetButton.Text = "Set";
				this._recordingRow.TextBox.BackColor = SystemColors.Window;
				this._recordingRow = null;
			}

			this.ResumeGlobalHotkeys?.Invoke();
		}

		private void OnHotkeyCaptured(Keys keys)
		{
			HotkeyRow row = this._recordingRow;
			this.StopRecording();
			if (row == null)
			{
				return;
			}

			row.TextBox.Text = keys == Keys.None ? string.Empty : HotkeyFormatting.ToDisplayString(keys);
			this.NotifySettingsChanged();
		}

		private void OnHotkeyCaptureCancelled()
		{
			this.StopRecording();
		}

		private void NotifySettingsChanged()
		{
			if (this._suppressChangeNotification)
			{
				return;
			}

			this.SettingsChanged?.Invoke();
		}

		private sealed class HotkeyCaptureFilter : IMessageFilter
		{
			private readonly Action<Keys> _onCaptured;
			private readonly Action _onCancelled;

			public HotkeyCaptureFilter(Action<Keys> onCaptured, Action onCancelled)
			{
				this._onCaptured = onCaptured;
				this._onCancelled = onCancelled;
			}

			public bool PreFilterMessage(ref Message m)
			{
				if (m.Msg != WM_KEYDOWN && m.Msg != WM_SYSKEYDOWN)
				{
					return false;
				}

				Keys keyCode = (Keys)(m.WParam.ToInt64() & 0xFFFF);
				if (keyCode == Keys.Escape)
				{
					this._onCancelled();
					return true;
				}

				if (keyCode == Keys.Delete || keyCode == Keys.Back)
				{
					this._onCaptured(Keys.None);
					return true;
				}

				if (IsModifierKey(keyCode))
				{
					return true;
				}

				Keys keys = keyCode | Control.ModifierKeys;
				this._onCaptured(keys);
				return true;
			}

			private static bool IsModifierKey(Keys keyCode)
			{
				return keyCode == Keys.ShiftKey
					|| keyCode == Keys.ControlKey
					|| keyCode == Keys.Menu
					|| keyCode == Keys.LShiftKey
					|| keyCode == Keys.RShiftKey
					|| keyCode == Keys.LControlKey
					|| keyCode == Keys.RControlKey
					|| keyCode == Keys.LMenu
					|| keyCode == Keys.RMenu;
			}
		}

		private enum HotkeyRowKind
		{
			CycleGroup1Forward,
			CycleGroup1Backward,
			CycleGroup2Forward,
			CycleGroup2Backward,
			CycleGroup3Forward,
			CycleGroup3Backward,
			CycleGroup4Forward,
			CycleGroup4Backward,
			CycleGroup5Forward,
			CycleGroup5Backward,
			DynamicCycleForward,
			DynamicCycleBackward,
			MinimizeAllClients
		}

		private sealed class HotkeyRow
		{
			public HotkeyRow(HotkeyRowKind kind, TextBox textBox, Button setButton)
			{
				this.Kind = kind;
				this.TextBox = textBox;
				this.SetButton = setButton;
			}

			public HotkeyRowKind Kind { get; }
			public TextBox TextBox { get; }
			public Button SetButton { get; }

			public void LoadFrom(GlobalShortcutSettings settings)
			{
				this.TextBox.Text = this.Kind switch
				{
					HotkeyRowKind.CycleGroup1Forward => settings.CycleGroup1Forward,
					HotkeyRowKind.CycleGroup1Backward => settings.CycleGroup1Backward,
					HotkeyRowKind.CycleGroup2Forward => settings.CycleGroup2Forward,
					HotkeyRowKind.CycleGroup2Backward => settings.CycleGroup2Backward,
					HotkeyRowKind.CycleGroup3Forward => settings.CycleGroup3Forward,
					HotkeyRowKind.CycleGroup3Backward => settings.CycleGroup3Backward,
					HotkeyRowKind.CycleGroup4Forward => settings.CycleGroup4Forward,
					HotkeyRowKind.CycleGroup4Backward => settings.CycleGroup4Backward,
					HotkeyRowKind.CycleGroup5Forward => settings.CycleGroup5Forward,
					HotkeyRowKind.CycleGroup5Backward => settings.CycleGroup5Backward,
					HotkeyRowKind.DynamicCycleForward => settings.DynamicCycleForward,
					HotkeyRowKind.DynamicCycleBackward => settings.DynamicCycleBackward,
					HotkeyRowKind.MinimizeAllClients => settings.MinimizeAllClients,
					_ => string.Empty
				};
			}

			public void ApplyTo(GlobalShortcutSettings settings)
			{
				switch (this.Kind)
				{
					case HotkeyRowKind.CycleGroup1Forward:
						settings.CycleGroup1Forward = this.TextBox.Text;
						break;
					case HotkeyRowKind.CycleGroup1Backward:
						settings.CycleGroup1Backward = this.TextBox.Text;
						break;
					case HotkeyRowKind.CycleGroup2Forward:
						settings.CycleGroup2Forward = this.TextBox.Text;
						break;
					case HotkeyRowKind.CycleGroup2Backward:
						settings.CycleGroup2Backward = this.TextBox.Text;
						break;
					case HotkeyRowKind.CycleGroup3Forward:
						settings.CycleGroup3Forward = this.TextBox.Text;
						break;
					case HotkeyRowKind.CycleGroup3Backward:
						settings.CycleGroup3Backward = this.TextBox.Text;
						break;
					case HotkeyRowKind.CycleGroup4Forward:
						settings.CycleGroup4Forward = this.TextBox.Text;
						break;
					case HotkeyRowKind.CycleGroup4Backward:
						settings.CycleGroup4Backward = this.TextBox.Text;
						break;
					case HotkeyRowKind.CycleGroup5Forward:
						settings.CycleGroup5Forward = this.TextBox.Text;
						break;
					case HotkeyRowKind.CycleGroup5Backward:
						settings.CycleGroup5Backward = this.TextBox.Text;
						break;
					case HotkeyRowKind.DynamicCycleForward:
						settings.DynamicCycleForward = this.TextBox.Text;
						break;
					case HotkeyRowKind.DynamicCycleBackward:
						settings.DynamicCycleBackward = this.TextBox.Text;
						break;
					case HotkeyRowKind.MinimizeAllClients:
						settings.MinimizeAllClients = this.TextBox.Text;
						break;
				}
			}
		}
	}
}
