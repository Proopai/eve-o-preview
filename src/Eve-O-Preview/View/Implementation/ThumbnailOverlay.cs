using EveOPreview.Configuration;
using EveOPreview.Services;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
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
		private static readonly Color LiveOverlayTransparencyKey = Color.FromArgb(0, 0, 1);
		private static readonly Color OpaqueOverlayTransparencyKey = Color.FromArgb(1, 0, 1);

		private bool _showOverlayText = true;
		private bool _fakePreviewEnabled;
		private Image _portraitImage;
		private Color _fakePreviewBackgroundColor;
		private int _fakePreviewBorderWidth;
		private Color _fakePreviewBorderColor;
		private bool _fakePreviewLayoutApplied;
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
		public void SetPortraitImage(Image image)
		{
			this._portraitImage?.Dispose();
			this._portraitImage = image;
			this.OverlayAreaPictureBox.Invalidate();
		}

		public void ClearPortrait()
		{
			this._portraitImage?.Dispose();
			this._portraitImage = null;
			this.OverlayAreaPictureBox.Invalidate();
		}

		public void EnableFakePreview(bool enable, bool resizeForHighlight, int insetTop, int insetRight, int insetBottom, int insetLeft, Color bgColor, int opaqueBorderWidth = 0, Color opaqueBorderColor = default)
		{
			int borderWidth = Math.Max(0, opaqueBorderWidth);

			if (!enable)
			{
				this._fakePreviewLayoutApplied = false;
				this._fakePreviewEnabled = false;
				this.TransparencyKey = ThumbnailOverlay.LiveOverlayTransparencyKey;
				this.BackColor = ThumbnailOverlay.LiveOverlayTransparencyKey;
				OverlayAreaPictureBox.BackColor = ThumbnailOverlay.LiveOverlayTransparencyKey;
				OverlayLabel.BackColor = ThumbnailOverlay.LiveOverlayTransparencyKey;
				this.ClearPortrait();
				this.ApplyLiveOverlayPictureBoxLayout(resizeForHighlight, insetTop, insetRight, insetBottom, insetLeft);
				return;
			}

			if (this._fakePreviewLayoutApplied
				&& this._fakePreviewEnabled
				&& this._fakePreviewBackgroundColor == bgColor
				&& this._fakePreviewBorderWidth == borderWidth
				&& this._fakePreviewBorderColor == opaqueBorderColor
				&& OverlayAreaPictureBox.Dock == DockStyle.Fill)
			{
				return;
			}

			this._fakePreviewEnabled = true;
			this._fakePreviewBackgroundColor = bgColor;
			this._fakePreviewBorderWidth = borderWidth;
			this._fakePreviewBorderColor = opaqueBorderColor;
			this._fakePreviewLayoutApplied = true;

			// Portrait / prevent-preview: opaque surface — do not use TransparencyKey holes for the highlight border.
			this.TransparencyKey = ThumbnailOverlay.OpaqueOverlayTransparencyKey;
			this.BackColor = bgColor;
			OverlayAreaPictureBox.BackColor = bgColor;
			OverlayLabel.BackColor = Color.Transparent;
			OverlayAreaPictureBox.Dock = DockStyle.Fill;
			OverlayAreaPictureBox.Location = Point.Empty;
			OverlayAreaPictureBox.Size = this.ClientSize;
			this.OverlayAreaPictureBox.Invalidate();
		}

		private void ApplyLiveOverlayPictureBoxLayout(bool resizeForHighlight, int insetTop, int insetRight, int insetBottom, int insetLeft)
		{
			if (!resizeForHighlight)
			{
				OverlayAreaPictureBox.Dock = DockStyle.Fill;
				this.OverlayAreaPictureBox.Invalidate();
				return;
			}

			OverlayAreaPictureBox.Dock = DockStyle.None;

			int left = insetLeft;
			int top = insetTop;
			int width = Math.Max(0, this.ClientSize.Width - insetLeft - insetRight);
			int height = Math.Max(0, this.ClientSize.Height - insetTop - insetBottom);

			if (OverlayAreaPictureBox.Location.X != left || OverlayAreaPictureBox.Location.Y != top)
			{
				OverlayAreaPictureBox.Location = new Point(left, top);
			}

			if (OverlayAreaPictureBox.Size.Width != width || OverlayAreaPictureBox.Size.Height != height)
			{
				OverlayAreaPictureBox.Size = new Size(width, height);
			}

			this.OverlayAreaPictureBox.Invalidate();
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
			if (this._fakePreviewEnabled)
			{
				this.PaintPortrait(e);
			}

			if (this._showOverlayText)
			{
				PaintDrawText(e, OverlayLabel);
			}
		}

		private void PaintPortrait(PaintEventArgs e)
		{
			Rectangle bounds = this.OverlayAreaPictureBox.ClientRectangle;
			if (bounds.Width <= 0 || bounds.Height <= 0)
			{
				return;
			}

			int border = this._fakePreviewBorderWidth;
			if (border > 0)
			{
				using (SolidBrush borderBrush = new SolidBrush(this._fakePreviewBorderColor))
				{
					e.Graphics.FillRectangle(borderBrush, bounds);
				}

				bounds = Rectangle.Inflate(bounds, -border, -border);
				if (bounds.Width <= 0 || bounds.Height <= 0)
				{
					return;
				}
			}

			using (SolidBrush backgroundBrush = new SolidBrush(this._fakePreviewBackgroundColor))
			{
				e.Graphics.FillRectangle(backgroundBrush, bounds);
			}

			if (this._portraitImage == null)
			{
				return;
			}

			float imageAspect = (float)this._portraitImage.Width / this._portraitImage.Height;
			float boxAspect = (float)bounds.Width / bounds.Height;
			int drawWidth;
			int drawHeight;

			if (imageAspect > boxAspect)
			{
				drawWidth = bounds.Width;
				drawHeight = Math.Max(1, (int)Math.Round(bounds.Width / imageAspect));
			}
			else
			{
				drawHeight = bounds.Height;
				drawWidth = Math.Max(1, (int)Math.Round(bounds.Height * imageAspect));
			}

			int x = bounds.X + (bounds.Width - drawWidth) / 2;
			int y = bounds.Y + (bounds.Height - drawHeight) / 2;

			e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
			e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
			e.Graphics.CompositingMode = CompositingMode.SourceOver;
			e.Graphics.DrawImage(this._portraitImage, x, y, drawWidth, drawHeight);
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
