using EveOPreview.Configuration;
using EveOPreview.Services;
using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using Rectangle = System.Drawing.Rectangle;

namespace EveOPreview.View
{
	public partial class ThumbnailOverlay : Form
	{
		#region Private fields
		private readonly Action<object, EventArgs> _areaMouseEnterAction;
		private readonly Action<object, EventArgs> _areaMouseLeaveAction;
		private readonly Action<object, MouseEventArgs> _areaMouseDownAction;
		private readonly Action<object, MouseEventArgs> _areaMouseUpAction;
		private readonly Action<object, MouseEventArgs> _areaMouseMoveAction;
		private bool _showOverlayText = true;
		#endregion

		public ThumbnailOverlay(Form owner,
			Action<object, EventArgs> areaMouseEnterAction,
			Action<object, EventArgs> areaMouseLeaveAction,
			Action<object, MouseEventArgs> areaMouseDownAction,
			Action<object, MouseEventArgs> areaMouseUpAction,
			Action<object, MouseEventArgs> areaMouseMoveAction
			)
		{
			this.Owner = owner;
			this._areaMouseEnterAction = areaMouseEnterAction;
			this._areaMouseLeaveAction = areaMouseLeaveAction;
			this._areaMouseDownAction = areaMouseDownAction;
			this._areaMouseUpAction = areaMouseUpAction;
			this._areaMouseMoveAction = areaMouseMoveAction;

			InitializeComponent();
		}

		private void OverlayArea_MouseEnter(object sender, EventArgs e)
		{
			this._areaMouseEnterAction(this, e);
		}
		private void OverlayArea_MouseLeave(object sender, EventArgs e)
		{
			this._areaMouseLeaveAction(this, e);
		}
		private void OverlayArea_MouseDown(object sender, MouseEventArgs e)
		{
			this._areaMouseDownAction(this, e);
		}
		private void OverlayArea_MouseUp(object sender, MouseEventArgs e)
		{
			this._areaMouseUpAction(this, e);
		}
		private void OverlayArea_MouseMove(object sender, MouseEventArgs e)
		{
			this._areaMouseMoveAction(this, e);
		}

		public void SetOverlayLabel(string label)
		{
			this.OverlayLabel.Text = label;
		}
		public void SetCycleGroupIndicator(bool displayCycleGroup, ZoomAnchor anchor)
		{
			if (!displayCycleGroup)
			{
				this.CycleGroupIndicator.Visible = false;
				return;
			}

			this.SuspendLayout();
			try
			{
				// Child of the preview surface so Left/Top are in the same space as the live thumbnail (no form vs. box drift).
				PictureBox host = this.OverlayAreaPictureBox;
				if (this.CycleGroupIndicator.Parent != host)
				{
					this.CycleGroupIndicator.Parent?.Controls.Remove(this.CycleGroupIndicator);
					host.Controls.Add(this.CycleGroupIndicator);
				}

				this.CycleGroupIndicator.BringToFront();
				this.CycleGroupIndicator.Visible = true;

				this.PerformLayout();
				host.PerformLayout();

				int margin = 2;
				int cw = host.ClientSize.Width;
				int ch = host.ClientSize.Height;
				int innerW = Math.Max(0, cw - 2 * margin);
				int innerH = Math.Max(0, ch - 2 * margin);
				int size = Math.Max(16, Math.Min(innerW, innerH));

				this.CycleGroupIndicator.BackColor = host.BackColor;
				this.CycleGroupIndicator.Width = size;
				this.CycleGroupIndicator.Height = size;

				// Center on the character-name label so a large badge stays on the text (config “corner”
				// anchors were designed for a small icon and read as horizontally shifted otherwise).
				Rectangle labelInHost = new Rectangle(
					this.OverlayLabel.Left - host.Left,
					this.OverlayLabel.Top - host.Top,
					this.OverlayLabel.Width,
					this.OverlayLabel.Height);
				bool labelOk = !string.IsNullOrEmpty(this.OverlayLabel.Text)
					&& labelInHost.Width > 0 && labelInHost.Height > 0;

				if (labelOk)
				{
					int left = labelInHost.Left + (labelInHost.Width - size) / 2;
					int top = labelInHost.Top + (labelInHost.Height - size) / 2;
					this.CycleGroupIndicator.Left = Math.Max(0, Math.Min(left, cw - size));
					this.CycleGroupIndicator.Top = Math.Max(0, Math.Min(top, ch - size));
				}
				else
				{
					switch (anchor)
					{
						case ZoomAnchor.NW:
							this.CycleGroupIndicator.Left = margin;
							this.CycleGroupIndicator.Top = margin;
							break;
						case ZoomAnchor.N:
							this.CycleGroupIndicator.Left = (cw - size) / 2;
							this.CycleGroupIndicator.Top = margin;
							break;
						case ZoomAnchor.NE:
							this.CycleGroupIndicator.Left = cw - size - margin;
							this.CycleGroupIndicator.Top = margin;
							break;
						case ZoomAnchor.W:
							this.CycleGroupIndicator.Left = margin;
							this.CycleGroupIndicator.Top = (ch - size) / 2;
							break;
						case ZoomAnchor.C:
							this.CycleGroupIndicator.Left = (cw - size) / 2;
							this.CycleGroupIndicator.Top = (ch - size) / 2;
							break;
						case ZoomAnchor.E:
							this.CycleGroupIndicator.Left = cw - size - margin;
							this.CycleGroupIndicator.Top = (ch - size) / 2;
							break;
						case ZoomAnchor.SW:
							this.CycleGroupIndicator.Left = margin;
							this.CycleGroupIndicator.Top = ch - size - margin;
							break;
						case ZoomAnchor.S:
							this.CycleGroupIndicator.Left = (cw - size) / 2;
							this.CycleGroupIndicator.Top = ch - size - margin;
							break;
						case ZoomAnchor.SE:
							this.CycleGroupIndicator.Left = cw - size - margin;
							this.CycleGroupIndicator.Top = ch - size - margin;
							break;
					}
				}
			}
			finally
			{
				this.ResumeLayout(performLayout: false);
			}
		}

		public void SetPropertiesOverlayLabel(Font f, System.Drawing.Color c, ZoomAnchor anchor)
		{
			if (
				this.OverlayLabel.Font.Size != f.Size ||
				this.OverlayLabel.Font.FontFamily != f.FontFamily ||
				this.OverlayLabel.Font.Italic != f.Italic ||
				this.OverlayLabel.Font.Bold != f.Bold
				)
			{
				this.OverlayLabel.Font = f;
			}
			this.OverlayLabel.ForeColor = c;

			int margin = 5;

			switch (anchor)
			{
				case ZoomAnchor.NW:
					this.OverlayLabel.Left = margin;
					this.OverlayLabel.Top = margin;
					this.OverlayLabel.TextAlign = System.Drawing.ContentAlignment.TopLeft;
					break;
				case ZoomAnchor.N:
					this.OverlayLabel.Left = (this.Width / 2) - (this.OverlayLabel.Width / 2);
					this.OverlayLabel.Top = margin;
					this.OverlayLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
					break;
				case ZoomAnchor.NE:
					this.OverlayLabel.Left = this.Width - this.OverlayLabel.Width - margin;
					this.OverlayLabel.Top = margin;
					this.OverlayLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
					break;
				case ZoomAnchor.W:
					this.OverlayLabel.Left = margin;
					this.OverlayLabel.Top = (this.Height / 2) - (this.OverlayLabel.Height / 2);
					this.OverlayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
					break;
				case ZoomAnchor.C:
					this.OverlayLabel.Left = (this.Width / 2) - (this.OverlayLabel.Width / 2);
					this.OverlayLabel.Top = (this.Height / 2) - (this.OverlayLabel.Height / 2);
					this.OverlayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
					break;
				case ZoomAnchor.E:
					this.OverlayLabel.Left = this.Width - this.OverlayLabel.Width - margin;
					this.OverlayLabel.Top = (this.Height / 2) - (this.OverlayLabel.Height / 2);
					this.OverlayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
					break;
				case ZoomAnchor.SW:
					this.OverlayLabel.Left = margin;
					this.OverlayLabel.Top = this.Height - this.OverlayLabel.Height - margin;
					this.OverlayLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
					break;
				case ZoomAnchor.S:
					this.OverlayLabel.Left = (this.Width / 2) - (this.OverlayLabel.Width / 2);
					this.OverlayLabel.Top = this.Height - this.OverlayLabel.Height - margin;
					this.OverlayLabel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
					break;
				case ZoomAnchor.SE:
					this.OverlayLabel.Left = this.Width - this.OverlayLabel.Width - margin;
					this.OverlayLabel.Top = this.Height - this.OverlayLabel.Height - margin;
					this.OverlayLabel.TextAlign = System.Drawing.ContentAlignment.BottomRight;
					break;
			}
		}

		public void EnableOverlayLabel(bool enable)
		{
			//this.OverlayLabel.Visible = enable;
			this._showOverlayText = enable;
		}
		public void EnableFakePreview(bool enable, bool resizeForHighlight, int insetTop, int insetRight, int insetBottom, int insetLeft, Color bgColor)
		{
			bool IsLocationUpdateRequired(Point currentLocation, int left, int top)
			{
				return (currentLocation.X != left) || (currentLocation.Y != top);
			}

			bool IsSizeUpdateRequired(Size currentSize, int width, int height)
			{
				return (currentSize.Width != width) || (currentSize.Height != height);
			}

			if (!enable)
			{
				OverlayAreaPictureBox.BackColor = Color.Transparent;
				OverlayLabel.BackColor = Color.Transparent;
			}
			else
			{
				OverlayAreaPictureBox.BackColor = bgColor;
				OverlayLabel.BackColor = Color.Transparent;
			}

			if (!resizeForHighlight)
			{
				OverlayAreaPictureBox.Dock = DockStyle.Fill;
				return;
			}

			OverlayAreaPictureBox.Dock = DockStyle.None;

			var left = insetLeft;
			var top = insetTop;
			var width = Math.Max(0, this.ClientSize.Width - insetLeft - insetRight);
			var height = Math.Max(0, this.ClientSize.Height - insetTop - insetBottom);

			if (IsLocationUpdateRequired(OverlayAreaPictureBox.Location, left, top))
			{
				OverlayAreaPictureBox.Location = new Point(left, top);
			}

			if (IsSizeUpdateRequired(OverlayAreaPictureBox.Size, width, height))
			{
				OverlayAreaPictureBox.Size = new Size(width, height);
			}
		}

		private void PaintDrawText(PaintEventArgs e, System.Windows.Forms.Label l)
		{
			var flags = TextFormatFlags.Right;
			if (l.TextAlign == ContentAlignment.TopLeft || l.TextAlign == ContentAlignment.BottomLeft || l.TextAlign == ContentAlignment.MiddleLeft) flags = TextFormatFlags.Left;
			flags = flags | TextFormatFlags.WordBreak;

			e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

			// Label is positioned on the form; paint uses picturebox graphics (letterboxed preview).
			var pb = this.OverlayAreaPictureBox;
			var textRect = new Rectangle(l.Left - pb.Left, l.Top - pb.Top, l.Width, l.Height);
			TextRenderer.DrawText(e.Graphics, l.Text, l.Font, textRect, l.ForeColor, flags);
		}

		private void OverlayAreaPictureBox_Paint(object sender, PaintEventArgs e)
		{
			if (this._showOverlayText) PaintDrawText(e, OverlayLabel);
		}

		protected override CreateParams CreateParams
		{
			get
			{
				var Params = base.CreateParams;
				Params.ExStyle |= (int)InteropConstants.WS_EX_TOOLWINDOW;
				return Params;
			}
		}
	}
}
