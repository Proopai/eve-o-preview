using EveOPreview.Configuration;
using EveOPreview.Services;
using EveOPreview.View.Implementation;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Security.Policy;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
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
		private bool _showBorder = false;
		private bool _showAggression = false;
		private int _showAggressionSize = 10;
		private bool _showAlert = false;
		private int _showAlertSeconds = 10;
		private int _showAggressionSeconds = 60;
		private int _showAggressionX;
		private int _showAggressionY;
		private Color _showBorderColour = Color.White;
		private Color _showAlertColour = Color.White;
		private Color _showAggressionColour = Color.White;
		private int _showBorderWidth = 1;
		private int _showAlertWidth = 6;
		private Color _fakeBackground = Color.Red;
		private bool _showFakeBackground = false;
		private DateTime _alertTime;
		private DateTime _aggressionTime;
		private int _alertType;
		private int _alertJumps=0;
		private DashStyle _showBorderDashStyle = DashStyle.Solid;
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

			this.DoubleBuffered = true;

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
		public void SetSystemNameLabel(string label)
		{
			this.SystemNameLabel.Text = label;
		}
		public void SetAggression(bool aggression, Color alertColor, int alertSeconds, int size, ZoomAnchor anchor)
		{
			this._showAggression = aggression;
			this._aggressionTime = DateTime.Now;
			this._showAggressionColour = alertColor;
			this._showAggressionSeconds = alertSeconds;
			this._showAggressionSize = size;
			int margin = 2;

			switch (anchor)
			{
				case ZoomAnchor.NW:
					this._showAggressionX = margin;
					this._showAggressionY = margin;
					break;
				case ZoomAnchor.N:
					this._showAggressionX = (this.Width / 2) - (size / 2);
					this._showAggressionY = margin;
					break;
				case ZoomAnchor.NE:
					this._showAggressionX = this.Width - size - margin;
					this._showAggressionY = margin;
					break;
				case ZoomAnchor.W:
					this._showAggressionX = margin;
					this._showAggressionY = (this.Height / 2) - (size / 2);
					break;
				case ZoomAnchor.C:
					this._showAggressionX = (this.Width / 2) - (size / 2);
					this._showAggressionY = (this.Height / 2) - (size / 2);
					break;
				case ZoomAnchor.E:
					this._showAggressionX = this.Width - size - margin;
					this._showAggressionY = (this.Height / 2) - (size / 2);
					break;
				case ZoomAnchor.SW:
					this._showAggressionX = margin;
					this._showAggressionY = this.Height - size - margin;
					break;
				case ZoomAnchor.S:
					this._showAggressionX = (this.Width / 2) - (size / 2);
					this._showAggressionY = this.Height - size - margin;
					break;
				case ZoomAnchor.SE:
					this._showAggressionX = this.Width - size - margin;
					this._showAggressionY = this.Height - size - margin;
					break;
			}

		}

		public void SetAlertClient(int jumps, int type, Color alertColor, int alertBorderWidth, int alertSeconds)
		{
			this._alertTime = DateTime.Now;
			this._alertJumps = jumps;
			this._alertType = type;
			this._showAlert = true;

			this._showAlertColour = alertColor;
			this._showAlertWidth = alertBorderWidth;
			this._showAlertSeconds = alertSeconds;
		}
		public void SetBorder(bool showBorder, Color borderColor, int borderWidth, DashStyle ds)
		{
			this._showBorder = showBorder;
			this._showBorderColour = borderColor;
			this._showBorderWidth = borderWidth;
			this._showBorderDashStyle = ds;

		}

		public void SetCycleGroupIndicator(bool displayCycleGroup, ZoomAnchor anchor)
		{
			if (displayCycleGroup)
			{
				this.CycleGroupIndicator.Visible = true;
				int margin = 2;
				int size = Math.Min(Math.Min(this.Height - margin, this.Width - margin), 40);

				this.CycleGroupIndicator.BackColor = this.OverlayAreaPictureBox.BackColor;
				this.CycleGroupIndicator.Width = size;
				this.CycleGroupIndicator.Height = size;

				this.CycleGroupIndicator.Top = 1;
				this.CycleGroupIndicator.Left = this.Width - size - 2;
				switch (anchor)
				{
					case ZoomAnchor.NW:
						this.CycleGroupIndicator.Left = margin;
						this.CycleGroupIndicator.Top = margin;
						break;
					case ZoomAnchor.N:
						this.CycleGroupIndicator.Left = (this.Width / 2) - (this.CycleGroupIndicator.Width / 2);
						this.CycleGroupIndicator.Top = margin;
						break;
					case ZoomAnchor.NE:
						this.CycleGroupIndicator.Left = this.Width - this.CycleGroupIndicator.Width - margin;
						this.CycleGroupIndicator.Top = margin;
						break;
					case ZoomAnchor.W:
						this.CycleGroupIndicator.Left = margin;
						this.CycleGroupIndicator.Top = (this.Height / 2) - (this.CycleGroupIndicator.Height / 2);
						break;
					case ZoomAnchor.C:
						this.CycleGroupIndicator.Left = (this.Width / 2) - (this.CycleGroupIndicator.Width / 2);
						this.CycleGroupIndicator.Top = (this.Height / 2) - (this.CycleGroupIndicator.Height / 2);
						break;
					case ZoomAnchor.E:
						this.CycleGroupIndicator.Left = this.Width - this.CycleGroupIndicator.Width - margin;
						this.CycleGroupIndicator.Top = (this.Height / 2) - (this.CycleGroupIndicator.Height / 2);
						break;
					case ZoomAnchor.SW:
						this.CycleGroupIndicator.Left = margin;
						this.CycleGroupIndicator.Top = this.Height - this.CycleGroupIndicator.Height - margin;
						break;
					case ZoomAnchor.S:
						this.CycleGroupIndicator.Left = (this.Width / 2) - (this.CycleGroupIndicator.Width / 2);
						this.CycleGroupIndicator.Top = this.Height - this.CycleGroupIndicator.Height - margin;
						break;
					case ZoomAnchor.SE:
						this.CycleGroupIndicator.Left = this.Width - this.CycleGroupIndicator.Width - margin;
						this.CycleGroupIndicator.Top = this.Height - this.CycleGroupIndicator.Height - margin;
						break;
				}


			}
			else
			{
				this.CycleGroupIndicator.Visible = false;
			}
		}

		public void SetPropertiesOverlayLabel(Font font, System.Drawing.Color foregroundColour, System.Drawing.Color outlineColour, int outlineSize, ZoomAnchor anchor)
		{
			SetPropertiesLabel(this.OverlayLabel, font, foregroundColour, outlineColour, outlineSize, anchor);
		}
		public void SetPropertiesSystemNameLabel(Font font, System.Drawing.Color foregroundColour, System.Drawing.Color outlineColour, int outlineSize, ZoomAnchor anchor)
		{
			SetPropertiesLabel(this.SystemNameLabel, font, foregroundColour, outlineColour, outlineSize, anchor);
		}

		private void SetPropertiesLabel(BorderLabel label, Font font, System.Drawing.Color foregroundColour, System.Drawing.Color outlineColour, int outlineSize, ZoomAnchor anchor)
		{
			if (
				label.Font.Size != font.Size ||
				label.Font.FontFamily != font.FontFamily ||
				label.Font.Italic != font.Italic ||
				label.Font.Bold != font.Bold
				)
			{
				label.Font = font;
			}
			label.ForeColor = foregroundColour;
			
			label.BorderColor = outlineColour;
			label.BorderSize = outlineSize;

			int margin = 5;

			switch (anchor)
			{
				case ZoomAnchor.NW:
					label.Left = margin;
					label.Top = margin;
					label.TextAlign = System.Drawing.ContentAlignment.TopLeft;
					break;
				case ZoomAnchor.N:
					label.Left = (this.Width / 2) - (label.Width / 2);
					label.Top = margin;
					label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
					break;
				case ZoomAnchor.NE:
					label.Left = this.Width - label.Width - margin;
					label.Top = margin;
					label.TextAlign = System.Drawing.ContentAlignment.TopRight;
					break;
				case ZoomAnchor.W:
					label.Left = margin;
					label.Top = (this.Height / 2) - (label.Height / 2);
					label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
					break;
				case ZoomAnchor.C:
					label.Left = (this.Width / 2) - (label.Width / 2);
					label.Top = (this.Height / 2) - (label.Height / 2);
					label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
					break;
				case ZoomAnchor.E:
					label.Left = this.Width - label.Width - margin;
					label.Top = (this.Height / 2) - (label.Height / 2);
					label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
					break;
				case ZoomAnchor.SW:
					label.Left = margin;
					label.Top = this.Height - label.Height - margin;
					label.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
					break;
				case ZoomAnchor.S:
					label.Left = (this.Width / 2) - (label.Width / 2);
					label.Top = this.Height - label.Height - margin;
					label.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
					break;
				case ZoomAnchor.SE:
					label.Left = this.Width - label.Width - margin;
					label.Top = this.Height - label.Height - margin;
					label.TextAlign = System.Drawing.ContentAlignment.BottomRight;
					break;
			}
			if (label.Top < 0) label.Top = 0;
			if (label.Left < 0) label.Left = 0;
		}

		public void EnableOverlayLabel(bool enable)
		{
			//this.OverlayLabel.Visible = enable;
			this._showOverlayText = enable;
		}
		public void EnableFakePreview(bool enable, bool resizeForHighlight, int highlightSize, Color bgColor)
		{
			bool IsLocationUpdateRequired(System.Drawing.Point currentLocation, int left, int top)
			{
				return (currentLocation.X != left) || (currentLocation.Y != top);
			}

			bool IsSizeUpdateRequired(System.Drawing.Size currentSize, int width, int height)
			{
				return (currentSize.Width != width) || (currentSize.Height != height);
			}


			if (!enable)
			{
				_showFakeBackground = false;
				this.OverlayAreaPictureBox.BackColor = Color.Transparent;
			}
			else
			{
				_fakeBackground = bgColor;
				this.OverlayAreaPictureBox.BackColor = bgColor;
				_showFakeBackground = true;
			}
		}

		private void PaintDrawText(PaintEventArgs e, BorderLabel l)
		{
			var flags = TextFormatFlags.Right;
			if (l.TextAlign == ContentAlignment.TopLeft || l.TextAlign == ContentAlignment.BottomLeft || l.TextAlign == ContentAlignment.MiddleLeft) flags = TextFormatFlags.Left;
			flags = flags | TextFormatFlags.WordBreak;

			e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
			e.Graphics.CompositingQuality = CompositingQuality.HighQuality;


			if ( l.ForeColor == l.BorderColor || l.BorderSize == 0 )
			{
				TextRenderer.DrawText(e.Graphics, l.Text, l.Font, new Rectangle(l.Left, l.Top, l.Width, l.Height), l.ForeColor, flags);
				return;
			}

			/*

			float fontSize = e.Graphics.DpiY * l.Font.SizeInPoints / 72;
			var drawSize = e.Graphics.MeasureString(l.Text, l.Font, new PointF(), StringFormat.GenericTypographic);
			var drawPath = new GraphicsPath();
			var drawPen = new Pen(new SolidBrush(l.BorderColor), l.BorderSize);
			var forecolorBrush = new SolidBrush(l.ForeColor);
			var point = new System.Drawing.Point();

			int margin = 6;

			if (l.AutoSize)
			{
				point.X = margin;
				point.Y = margin;
			}
			else
			{
				// Text is Left-Aligned:
				if (l.TextAlign == ContentAlignment.TopLeft ||
					l.TextAlign == ContentAlignment.MiddleLeft ||
					l.TextAlign == ContentAlignment.BottomLeft)
					point.X = margin;

				// Text is Center-Aligned
				else if (l.TextAlign == ContentAlignment.TopCenter ||
					l.TextAlign == ContentAlignment.MiddleCenter ||
					l.TextAlign == ContentAlignment.BottomCenter)
					point.X = (int)(l.Width - drawSize.Width) / 2;

				// Text is Right-Aligned
				else point.X = (int)( l.Width - (margin + drawSize.Width));

				// Text is Top-Aligned
				if (l.TextAlign == ContentAlignment.TopLeft ||
					l.TextAlign == ContentAlignment.TopCenter ||
					l.TextAlign == ContentAlignment.TopRight)
					point.Y = margin;

				// Text is Middle-Aligned
				else if (l.TextAlign == ContentAlignment.MiddleLeft ||
					l.TextAlign == ContentAlignment.MiddleCenter ||
					l.TextAlign == ContentAlignment.MiddleRight)
					point.Y = (int)((l.Height - drawSize.Height) / 2);

				// Text is Bottom-Aligned
				else point.Y = (int) (l.Height - (margin + drawSize.Height));
			}
			point.X += l.Location.X;
			point.Y += l.Location.Y;

			drawPath.Reset();
			drawPath.AddString(l.Text, l.Font.FontFamily, (int)l.Font.Style, fontSize,point, StringFormat.GenericTypographic);
			//drawPath.AddString(l.Text, l.Font.FontFamily, (int)l.Font.Style, fontSize, new Rectangle(l.Left, l.Top, l.Width, l.Height), StringFormat.GenericTypographic);

			// And finally, using our pen, all we have to do now
			//  is draw our graphics path to the screen. Voila!
			e.Graphics.FillPath(forecolorBrush, drawPath);
			e.Graphics.DrawPath(drawPen, drawPath);

			drawPath.Dispose();
			drawPen.Dispose();
			forecolorBrush.Dispose();

			*/


				Rectangle bounds = new Rectangle(l.Left, l.Top, l.Width, l.Height);
				// The text is stamped around the base position in a filled square pattern,
				// which produces a solid outline of the requested thickness
				for (int dx = - l.BorderSize; dx <= l.BorderSize; dx++)
				{
					for (int dy = -l.BorderSize; dy <= l.BorderSize; dy++)
					{
						if ((dx == 0) && (dy == 0))
						{
							continue;
						}

						Rectangle outlineBounds = bounds;
						outlineBounds.Offset(dx, dy);
						TextRenderer.DrawText(e.Graphics, l.Text, l.Font, outlineBounds, l.BorderColor, flags);
					}
				}

			TextRenderer.DrawText(e.Graphics, l.Text, l.Font, bounds, l.ForeColor, flags);

		}
		private void ShowFakeBackgroundBoring(PaintEventArgs e)
		{
			using (Brush bb = new SolidBrush(_fakeBackground))
			{
				e.Graphics.FillRectangle(bb, 0, 0, this.ClientSize.Width, this.ClientSize.Height);
			}
		}

		private void ShowFakeBackground(PaintEventArgs e)
		{
			Rectangle rect = ClientRectangle;

			Color top = ControlPaint.Light(_fakeBackground, 0.2f);
			Color bottom = ControlPaint.Dark(_fakeBackground, 0.2f);

			using (var brush = new LinearGradientBrush(rect,top,bottom,LinearGradientMode.Vertical))
				e.Graphics.FillRectangle(brush, rect);

			using (Pen p = new Pen(Color.FromArgb(40, Color.White)))
				e.Graphics.DrawRectangle(p,0,0,ClientSize.Width - 1,ClientSize.Height - 1);
		}

		private void ShowBorder(PaintEventArgs e)
		{
			int halfSize = (int)Math.Round((double)(_showBorderWidth / 2), 0);
			/*
						using (Pen pp = new Pen(_showBorderColour, _showBorderWidth))
						{
							pp.DashStyle = _showBorderDashStyle;
							e.Graphics.DrawRectangle(pp, halfSize, halfSize, this.ClientSize.Width - _showBorderWidth, this.ClientSize.Height - _showBorderWidth);
						}
			*/

			// glow
			for (int i = 4; i >= 1; i--)
			{
				using (Pen glowPen = new Pen(Color.FromArgb(15 / i, _showBorderColour), _showBorderWidth + (i * 2)))
					e.Graphics.DrawRectangle(glowPen, halfSize, halfSize, ClientSize.Width - _showBorderWidth, ClientSize.Height - _showBorderWidth);

				// main border
				using (Pen pp = new Pen(_showBorderColour, _showBorderWidth))
				{
					pp.DashStyle = _showBorderDashStyle;
					e.Graphics.DrawRectangle(pp, halfSize, halfSize, ClientSize.Width - _showBorderWidth, ClientSize.Height - _showBorderWidth);
				}
			}
		}

		private void ShowAlert(PaintEventArgs e)
		{
			double elapsed = (DateTime.Now - _alertTime).TotalSeconds;
			double t = Math.Min(elapsed / _showAlertSeconds, 1.0);
			int insetMore = 2;

			// Fade 50% -> 10%
			int alertAlpha = (int)(255 * (0.5 - (0.4 * t)));

			int left = _showAlertWidth + insetMore;
			int top = _showAlertWidth + insetMore;

			int right = ClientSize.Width - _showAlertWidth - insetMore;
			int bottom = ClientSize.Height - _showAlertWidth - insetMore;

			int width = right - left;
			int height = bottom - top;

			// Hold full rectangle for first 20% of lifetime
			double meltT;
			if (t <= 0.25)
			{
				meltT = 0.0;
			}
			else
			{
				meltT = (t - 0.20) / 0.80;

				// Ease-out so it starts slowly then collapses quicker
				meltT = 1.0 - Math.Pow(1.0 - meltT, 2);
			}

			// Edge lengths retract toward corners
			int hLen = (int)(width * (1.0 - meltT));
			int vLen = (int)(height * (1.0 - meltT));

			// Leave visible corner brackets
			hLen = Math.Max(hLen, 30);
			vLen = Math.Max(vLen, 30);

			// Glow
			for (int i = 5; i >= 1; i--)
			{
				using (Pen glowPen = new Pen(
					Color.FromArgb(alertAlpha / (i * 3), _showAlertColour),
					_showAlertWidth + (i * 2)))
				{
					// TOP
					e.Graphics.DrawLine(glowPen, left, top, left + (hLen / 2), top);
					e.Graphics.DrawLine(glowPen, right - (hLen / 2), top, right, top);

					// BOTTOM
					e.Graphics.DrawLine(glowPen, left, bottom, left + (hLen / 2), bottom);
					e.Graphics.DrawLine(glowPen, right - (hLen / 2), bottom, right, bottom);

					// LEFT
					e.Graphics.DrawLine(glowPen, left, top, left, top + (vLen / 2));
					e.Graphics.DrawLine(glowPen, left, bottom - (vLen / 2), left, bottom);

					// RIGHT
					e.Graphics.DrawLine(glowPen, right, top, right, top + (vLen / 2));
					e.Graphics.DrawLine(glowPen, right, bottom - (vLen / 2), right, bottom);
				}
			}

			// Main border
			using (Pen pp = new Pen(
				Color.FromArgb(alertAlpha, _showAlertColour), _showAlertWidth))
			{
				// TOP
				e.Graphics.DrawLine(pp, left, top, left + (hLen / 2), top);
				e.Graphics.DrawLine(pp, right - (hLen / 2), top, right, top);

				// BOTTOM
				e.Graphics.DrawLine(pp, left, bottom, left + (hLen / 2), bottom);
				e.Graphics.DrawLine(pp, right - (hLen / 2), bottom, right, bottom);

				// LEFT
				e.Graphics.DrawLine(pp, left, top, left, top + (vLen / 2));
				e.Graphics.DrawLine(pp, left, bottom - (vLen / 2), left, bottom);

				// RIGHT
				e.Graphics.DrawLine(pp, right, top, right, top + (vLen / 2));
				e.Graphics.DrawLine(pp, right, bottom - (vLen / 2), right, bottom);
			}

			if ((DateTime.Now - _alertTime).TotalSeconds > _showAlertSeconds)
			{
				this._showAlert = false;
			}
		}

		private void ShowAggression(PaintEventArgs e)
		{

			// alpha based on time left - SUPER nice idea - thank you LemonCreamPie

			int halfFullSize = (int)Math.Round((double)(_showAggressionSize / 2), 0);

			double elapsed = (DateTime.Now - _aggressionTime).TotalSeconds;
			double t = Math.Min(elapsed / _showAggressionSeconds, 1.0);
			double sizePercent = 1.0 - (0.7 * t);
			int aggressionSize = (int)(_showAggressionSize * sizePercent);
			int halfSize = (int)Math.Round((double)(aggressionSize / 2), 0);

			double alphaPercent = 0.10 + (0.40 * Math.Pow(1.0 - t, 3));
			int aggressionAlpha = (int)(255 * (alphaPercent));
			int aggressionAlpha2 = (int)(255 * (alphaPercent)) + 20;

			for (int i = 5; i >= 1; i--)
			{
				int glowSize = aggressionSize + (i * 4);
				int glowAlpha = aggressionAlpha / (i * 4);

				using (Brush glowBrush = new SolidBrush(
					Color.FromArgb(glowAlpha, _showAggressionColour)))
				{
					e.Graphics.FillEllipse(
						glowBrush,
						_showAggressionX - (glowSize / 2),
						_showAggressionY - (glowSize / 2) + halfFullSize,
						glowSize,
						glowSize);
				}
			}

			using (Brush bb = new SolidBrush(Color.FromArgb(aggressionAlpha, _showAggressionColour)))
				e.Graphics.FillEllipse(bb, _showAggressionX - halfSize, _showAggressionY - halfSize + halfFullSize, aggressionSize, aggressionSize);

			if ((DateTime.Now - _aggressionTime).TotalSeconds > _showAggressionSeconds)
			{
				this._showAggression = false;
			}
		}

		private void OverlayAreaPictureBox_Paint(object sender, PaintEventArgs e)
		{

			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

			if (this._showFakeBackground) ShowFakeBackground(e);

			if (this._showOverlayText) PaintDrawText(e, OverlayLabel);

			if (SystemNameLabel.Text != string.Empty) PaintDrawText(e, SystemNameLabel);

			if (this._showBorder) ShowBorder(e);

			if (this._showAlert) ShowAlert(e);

			if (this._showAggression) ShowAggression(e);

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
