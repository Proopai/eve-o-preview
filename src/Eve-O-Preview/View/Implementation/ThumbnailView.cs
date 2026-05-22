using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using EveOPreview.Configuration;
using EveOPreview.Services;
using EveOPreview.UI.Hotkeys;

namespace EveOPreview.View
{
	public abstract partial class ThumbnailView : Form, IThumbnailView
	{
		#region Private constants
		private const double OPACITY_THRESHOLD = 0.9;
		private const double OPACITY_EPSILON = 0.1;
		#endregion

		#region Private fields
		private readonly ThumbnailOverlay _overlay;

		// Part of the logic (namely current size / position management)
		// was moved to the view due to the performance reasons
		private bool _isOverlayVisible;
		private bool _isTopMost;
		private bool _isHighlightEnabled;
		private bool _isHighlightRequested;
		private int _highlightWidth;

		private bool _isLocationChanged;
		private bool _isSizeChanged;

		private bool _isCustomMouseModeActive;

		private double _opacity;
		 
		private DateTime _suppressResizeEventsTimestamp;
		private Size _baseZoomSize;
		private Point _baseZoomLocation;
		private Point _baseMousePosition;
		private Size _baseZoomMaximumSize;

		private HotkeyHandler _hotkeyHandler;

		private IThumbnailConfiguration _config;
		private Lazy<Color> _myBorderColor;
		private Color _preventPreviewColorValue;
		private bool _preventPreviewsEnabled;
		private int _appliedPreventHighlightBorder = -1;
		private IThumbnailManager _thumbnailManager;
		private readonly ICharacterPortraitService _characterPortraitService;
		#endregion

		protected ThumbnailView(IWindowManager windowManager, IThumbnailConfiguration config, IThumbnailManager thumbnailManager, ICharacterPortraitService characterPortraitService)
		{
			this._config = config;
			this.SuppressResizeEvent();

			this.WindowManager = windowManager;

			this.IsActive = false;

			this.IsOverlayEnabled = false;
			this._isOverlayVisible = false;
			this.IsExcludedFromCycleGroup = false;

			this._isTopMost = false;
			this._isHighlightEnabled = false;
			this._isHighlightRequested = false;

			this._isLocationChanged = true;
			this._isSizeChanged = true;

			this._isCustomMouseModeActive = false;

			this._opacity = 0.1;

			InitializeComponent();

			this._overlay = new ThumbnailOverlay(this,
				this.MouseEnter_Handler,
				this.MouseLeave_Handler,
				this.MouseDown_Handler,
				this.MouseUp_Handler,
				this.MouseMove_Handler
				);

			this._thumbnailManager = thumbnailManager;
			this._characterPortraitService = characterPortraitService;

			SetDefaultBorderColor();
			SetPreventPreviews();
		}

		public IWindowManager WindowManager { get; }

		public IntPtr Id { get; set; }

		public string Title
		{
			get => this.Text;
			set
			{
				this.Text = value;
				this._overlay.SetOverlayLabel(value.Replace("EVE - ", "").Replace("EVE Frontier - ", "*"));
				this._overlay.SetPropertiesOverlayLabel(_config.OverlayLabelFont, _config.OverlayLabelColor, _config.OverlayLabelAnchor);
				SetDefaultBorderColor();
				SetPreventPreviews();
				this._overlay.SetCycleGroupIndicator(this.IsExcludedFromCycleGroup , _config.CycleGroupIndicatorAnchor);
			}
		}

		public bool IsActive { get; set; }

		public bool IsOverlayEnabled { get; set; }
		public bool IsExcludedFromCycleGroup { get; set; }
		public ZoomAnchor ClientZoomAnchor { get; set; }

		public Point ThumbnailLocation
		{
			get => this.Location;
			set
			{
				this.StartPosition = FormStartPosition.Manual;
				this.Location = value;
			}
		}

		public Size ThumbnailSize
		{
			get => this.ClientSize;
			set => this.ClientSize = value;
		}

		public Action<IntPtr> ThumbnailResized { get; set; }

		public Action<IntPtr> ThumbnailMoved { get; set; }

		public Action<IntPtr> ThumbnailFocused { get; set; }

		public Action<IntPtr> ThumbnailLostFocus { get; set; }

		public Action<IntPtr> ThumbnailActivated { get; set; }

		public Action<IntPtr, bool> ThumbnailDeactivated { get; set; }
		public Action<IntPtr> ThumbnailFocusedOverwatchToggle { get; set; }
		public Action<IntPtr> ThumbnailToggleCycleGroup { get; set; }

		private bool WindowMoved = false;

		public void SetDefaultBorderColor()
		{
			this._myBorderColor = new Lazy<Color>(() =>
			{
				if (this._config.PerClientActiveClientHighlightColor.Any(x => x.Key == this.Title))
				{
					return this._config.PerClientActiveClientHighlightColor[Title];
				}
				else
				{
					return _config.ActiveClientHighlightColor;
				}
			});
		}

		public bool IsPreventPreviews()
		{
			return this._preventPreviewsEnabled;
		}

		public void SetPreventPreviews()
		{
			if (this._config.PerClientPreventPreviews.TryGetValue(this.Title, out bool perClientPrevent))
			{
				this._preventPreviewsEnabled = perClientPrevent;
			}
			else
			{
				this._preventPreviewsEnabled = this._config.PreventPreviews;
			}

			if (this._config.PerClientPreventPreviewColor.TryGetValue(this.Title, out Color perClientColor))
			{
				this._preventPreviewColorValue = perClientColor;
			}
			else
			{
				this._preventPreviewColorValue = this._config.PreventPreviewColor;
			}

			this._appliedPreventHighlightBorder = -1;
			this.OnPreventPreviewsChanged();
		}

		public void RefreshPortraitOverlay()
		{
			if (!this.IsPreventPreviews())
			{
				this._overlay.ClearPortrait();
				return;
			}

			Image portrait = this._characterPortraitService.TryLoadPortraitImage(this.Title);
			try
			{
				this._overlay.SetPortraitImage(portrait == null ? null : (Image)portrait.Clone());
			}
			finally
			{
				portrait?.Dispose();
			}
		}

		protected virtual void OnPreventPreviewsChanged()
		{
			this.Refresh(true);
		}

		private void ApplyPreventPreviewVisuals(int highlightBorderWidth)
		{
			Color highlightColor = highlightBorderWidth > 0 ? this._myBorderColor.Value : this._preventPreviewColorValue;
			this._overlay.EnableFakePreview(
				true,
				false,
				0,
				0,
				0,
				0,
				this._preventPreviewColorValue,
				highlightBorderWidth,
				highlightColor);
		}

		private void ApplyPreventPreviewState(bool fullRepaint)
		{
			int border = this._isHighlightRequested ? this._highlightWidth : 0;

			if (fullRepaint || border != this._appliedPreventHighlightBorder)
			{
				this.ResizeThumbnail(this.ClientSize.Width, this.ClientSize.Height, 0, 0, 0, 0);
				this.ApplyPreventPreviewVisuals(border);
				this._appliedPreventHighlightBorder = border;
			}

			if (fullRepaint)
			{
				this.RefreshPortraitOverlay();
				this._overlay.BringToFront();
			}
		}

		public new void Show()
		{
			this.SuppressResizeEvent();

			base.Show();

			this._isLocationChanged = true;
			this._isSizeChanged = true;
			this._isOverlayVisible = false;

			this.Refresh(true);

			this.IsActive = true;
		}

		public new void Hide()
		{
			this.SuppressResizeEvent();

			this.IsActive = false;

			this._isOverlayVisible = false;
			this._overlay.Hide();
			base.Hide();
		}

		public new virtual void Close()
		{
			this.SuppressResizeEvent();

			this.IsActive = false;
			this._overlay.Close();
			base.Close();
		}

		// This method is used to determine if the provided Handle is related to client or its thumbnail
		public bool IsKnownHandle(IntPtr handle)
		{
			return (this.Id == handle) || (this.Handle == handle) || (this._overlay.Handle == handle);
		}

		public void SetSizeLimitations(Size minimumSize, Size maximumSize)
		{
			this.MinimumSize = minimumSize;
			this.MaximumSize = maximumSize;
		}

		public void SetOpacity(double opacity)
		{
			if (opacity >= OPACITY_THRESHOLD)
			{
				opacity = 1.0;
			}

			if (Math.Abs(opacity - this._opacity) < OPACITY_EPSILON)
			{
				return;
			}

			try
			{
				this.Opacity = opacity;

				// Overlay opacity settings
				// Of the thumbnail's opacity is almost full then set the overlay's one to
				// full. Otherwise set it to half of the thumbnail opacity
				// Opacity value is stored even if the overlay is not displayed atm
				this._overlay.Opacity = opacity > 0.8 ? 1.0 : 1.0 - (1.0 - opacity) / 2;

				this._opacity = opacity;
			}
			catch (Win32Exception)
			{
				// Something went wrong in WinForms internals
				// Opacity will be updated in the next cycle
			}
		}

		public void SetFrames(bool enable)
		{
			FormBorderStyle style = enable ? FormBorderStyle.SizableToolWindow : FormBorderStyle.None;

			// No need to change the borders style if it is ALREADY correct
			if (this.FormBorderStyle == style)
			{
				return;
			}

			this.SuppressResizeEvent();

			this.FormBorderStyle = style;
		}
		public void SetOverlayLabel()
		{
		}
		public void SetCycleGroupIndicator(bool displayCycleGroup, ZoomAnchor anchor)
		{
			this._overlay.SetCycleGroupIndicator(displayCycleGroup, anchor);
		}

		public void SetTopMost(bool enableTopmost)
		{
			this._overlay.TopMost = enableTopmost;

			if (this._isTopMost == enableTopmost)
			{
				return;
			}

			this.TopMost = enableTopmost;
			this._isTopMost = enableTopmost;
		}

		public void SetHighlight()
		{
			SetHighlight(_config.EnableActiveClientHighlight, _config.ActiveClientHighlightThickness);
		}

		public void SetHighlight(bool enabled, int width)
		{
			if (this._isHighlightRequested == enabled && (!enabled || this._highlightWidth == width))
			{
				return;
			}

			if (enabled)
			{
				this._isHighlightRequested = true;
				this._highlightWidth = width;
				this.BackColor = this.IsPreventPreviews() ? Color.Black : _myBorderColor.Value;
			}
			else
			{
				this._isHighlightRequested = false;
				this.BackColor = Color.Black;
			}

			this._isSizeChanged = true;
		}

		public void ClearBorder()
		{
			if (this._isHighlightRequested)
			{
				this.SetHighlight(false, 0);
			}
			else if (this.IsPreventPreviews())
			{
				this._isSizeChanged = true;
			}

			this.Refresh(true);
		}

		public void ZoomIn(ViewZoomAnchor anchor, int zoomFactor)
		{
			int oldWidth = this._baseZoomSize.Width;
			int oldHeight = this._baseZoomSize.Height;

			int locationX = this.Location.X;
			int locationY = this.Location.Y;

			int clientSizeWidth = this.ClientSize.Width;
			int clientSizeHeight = this.ClientSize.Height;
			int newWidth = (zoomFactor * clientSizeWidth) + (this.Size.Width - clientSizeWidth);
			int newHeight = (zoomFactor * clientSizeHeight) + (this.Size.Height - clientSizeHeight);

			// First change size, THEN move the window
			// Otherwise there is a chance to fail in a loop
			// Zoom required -> Moved the windows 1st -> Focus is lost -> Window is moved back -> Focus is back on -> Zoom required -> ...
			this.MaximumSize = new Size(0, 0);
			this.Size = new Size(newWidth, newHeight);

			switch (anchor)
			{
				case ViewZoomAnchor.NW:
					break;
				case ViewZoomAnchor.N:
					this.Location = new Point(locationX - newWidth / 2 + oldWidth / 2, locationY);
					break;
				case ViewZoomAnchor.NE:
					this.Location = new Point(locationX - newWidth + oldWidth, locationY);
					break;

				case ViewZoomAnchor.W:
					this.Location = new Point(locationX, locationY - newHeight / 2 + oldHeight / 2);
					break;
				case ViewZoomAnchor.C:
					this.Location = new Point(locationX - newWidth / 2 + oldWidth / 2, locationY - newHeight / 2 + oldHeight / 2);
					break;
				case ViewZoomAnchor.E:
					this.Location = new Point(locationX - newWidth + oldWidth, locationY - newHeight / 2 + oldHeight / 2);
					break;

				case ViewZoomAnchor.SW:
					this.Location = new Point(locationX, locationY - newHeight + this._baseZoomSize.Height);
					break;
				case ViewZoomAnchor.S:
					this.Location = new Point(locationX - newWidth / 2 + oldWidth / 2, locationY - newHeight + oldHeight);
					break;
				case ViewZoomAnchor.SE:
					this.Location = new Point(locationX - newWidth + oldWidth, locationY - newHeight + oldHeight);
					break;
			}
		}

		public void ZoomOut()
		{
			this.RestoreWindowSizeAndLocation();
		}

		public void RegisterHotkey(Keys hotkey)
		{
			if (this._hotkeyHandler != null)
			{
				this.UnregisterHotkey();
			}

			if (hotkey == Keys.None)
			{
				return;
			}

			this._hotkeyHandler = new HotkeyHandler(this.Handle, hotkey);
			this._hotkeyHandler.Pressed += HotkeyPressed_Handler;
			this._hotkeyHandler.Register();
		}

		public void UnregisterHotkey()
		{
			if (this._hotkeyHandler == null)
			{
				return;
			}

			this._hotkeyHandler.Unregister();
			this._hotkeyHandler.Pressed -= HotkeyPressed_Handler;
			this._hotkeyHandler.Dispose();
			this._hotkeyHandler = null;
		}

		public void Refresh(bool forceRefresh)
		{
			if (this.IsPreventPreviews())
			{
				bool fullUpdate = forceRefresh || this._isSizeChanged || this._isLocationChanged;
				this.RefreshThumbnail(fullUpdate);
				this.SyncOverlay(fullUpdate);
				this.ApplyPreventPreviewState(fullUpdate);
				this._isLocationChanged = false;
				this._isSizeChanged = false;
				return;
			}

			this.RefreshThumbnail(forceRefresh);
			this.HighlightThumbnail(forceRefresh || this._isSizeChanged);
			this.SyncOverlay(forceRefresh || this._isSizeChanged || this._isLocationChanged);
			this._isSizeChanged = false;
		}

		protected abstract void RefreshThumbnail(bool forceRefresh);

		protected abstract void ResizeThumbnail(int baseWidth, int baseHeight, int highlightWidthTop, int highlightWidthRight, int highlightWidthBottom, int highlightWidthLeft);

		private void HighlightThumbnail(bool forceRefresh)
		{
			if (!forceRefresh && (this._isHighlightRequested == this._isHighlightEnabled))
			{
				// Nothing to do here
				return;
			}

			this._isHighlightEnabled = this._isHighlightRequested;

			int baseWidth = this.ClientSize.Width;
			int baseHeight = this.ClientSize.Height;

			if (!this._isHighlightRequested)
			{
				//No highlighting enabled, so no math required
				this.ResizeThumbnail(baseWidth, baseHeight, 0, 0, 0, 0);
				this._overlay.EnableFakePreview(false, false, 0, 0, 0, 0, SystemColors.Control);
				return;
			}

			double baseAspectRatio = ((double)baseWidth) / baseHeight;

			int actualHeight = baseHeight - 2 * this._highlightWidth;
			double desiredWidth = actualHeight * baseAspectRatio;
			int actualWidth = (int)Math.Round(desiredWidth, MidpointRounding.AwayFromZero);
			int highlightWidthLeft = (baseWidth - actualWidth) / 2;
			int highlightWidthRight = baseWidth - actualWidth - highlightWidthLeft;

			this._overlay.EnableFakePreview(false, true, this._highlightWidth, highlightWidthRight, this._highlightWidth, highlightWidthLeft, SystemColors.Control);
			this.ResizeThumbnail(this.ClientSize.Width, this.ClientSize.Height, this._highlightWidth, highlightWidthRight, this._highlightWidth, highlightWidthLeft);
		}

		private void SyncOverlay(bool updateGeometry)
		{
			if (!this.IsPreventPreviews() && this._isOverlayVisible && !updateGeometry)
			{
				return;
			}

			bool shouldShowOverlay = ((this.IsOverlayEnabled && this.Visible) || this.IsPreventPreviews())
				&& !this._config.IsThumbnailDisabled(this.Title);

			if (!shouldShowOverlay)
			{
				if (this._isOverlayVisible)
				{
					this._overlay.Hide();
					this._isOverlayVisible = false;
				}

				return;
			}

			this._overlay.EnableOverlayLabel(this.IsOverlayEnabled && this.Visible);

			if (!this._isOverlayVisible)
			{
				this._overlay.Show();
				this._isOverlayVisible = true;
			}

			Size overlaySize = this.ClientSize;
			Point overlayLocation = this.Location;

			int borderWidth = (this.Size.Width - this.ClientSize.Width) / 2;
			overlayLocation.X += borderWidth;
			overlayLocation.Y += (this.Size.Height - this.ClientSize.Height) - borderWidth;

			this._isLocationChanged = false;
			this._overlay.Size = overlaySize;
			this._overlay.SetPropertiesOverlayLabel(this._config.OverlayLabelFont, this._config.OverlayLabelColor, this._config.OverlayLabelAnchor);
			this._overlay.Location = overlayLocation;
			this._overlay.TopMost = this.TopMost;

			if (updateGeometry)
			{
				this._overlay.BringToFront();
			}
		}

		private void SuppressResizeEvent()
		{
			// Workaround for WinForms issue with the Resize event being fired with inconsistent ClientSize value
			// Any Resize events fired before this timestamp will be ignored
			this._suppressResizeEventsTimestamp = DateTime.UtcNow.AddMilliseconds(_config.ThumbnailResizeTimeoutPeriod);
		}

		#region GUI events
		protected override CreateParams CreateParams
		{
			get
			{
				var Params = base.CreateParams;
				Params.ExStyle |= (int)InteropConstants.WS_EX_TOOLWINDOW;
				return Params;
			}
		}

		private void Move_Handler(object sender, EventArgs e)
		{
			this._isLocationChanged = true;
			this.ThumbnailMoved?.Invoke(this.Id);
		}

		private void Resize_Handler(object sender, EventArgs e)
		{
			if (DateTime.UtcNow < this._suppressResizeEventsTimestamp)
			{
				return;
			}

			this._isSizeChanged = true;

			this.ThumbnailResized?.Invoke(this.Id);
		}

		private void MouseEnter_Handler(object sender, EventArgs e)
		{
			this.ExitCustomMouseMode();
			this.SaveWindowSizeAndLocation();

			this.ThumbnailFocused?.Invoke(this.Id);
		}

		private void MouseLeave_Handler(object sender, EventArgs e)
		{
			this.ThumbnailLostFocus?.Invoke(this.Id);
		}

		private void MouseDown_Handler(object sender, MouseEventArgs e)
		{
			this.MouseDownEventHandler(e.Button, Control.ModifierKeys);
		}

		private void MouseMove_Handler(object sender, MouseEventArgs e)
		{
			if (this._isCustomMouseModeActive)
			{
				this.ProcessCustomMouseMode(e.Button.HasFlag(MouseButtons.Left), e.Button.HasFlag(MouseButtons.Right));
			}
		}

		private void MouseUp_Handler(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				if (this.WindowMoved && _config.ThumbnailSnapToEdges)
				{
					this._thumbnailManager.SnapThumbnail(this.Id);
				}

				this.ExitCustomMouseMode();

				if (this.WindowMoved)
				{
					if (_config.ThumbnailSnapToEdges)
					{
						this.ThumbnailMoved?.Invoke(this.Id);
					}
					else if (_config.ThumbnailSnapToGrid)
					{
						var x = (int)Math.Round((double)this.Location.X / (double)_config.ThumbnailSnapToGridSizeX) * _config.ThumbnailSnapToGridSizeX;
						var y = (int)Math.Round((double)this.Location.Y / (double)_config.ThumbnailSnapToGridSizeY) * _config.ThumbnailSnapToGridSizeY;
						this.Location = new Point(x, y);
						this._baseZoomLocation = this.Location;
					}

					this.WindowMoved = false;
				}
			}
		}

		private void HotkeyPressed_Handler(object sender, HandledEventArgs e)
		{
			this.SetHighlight();
			this.ThumbnailActivated?.Invoke(this.Id);

			e.Handled = true;
		}
		#endregion

		#region Custom Mouse mode
		// This pair of methods saves/restores certain window properties
		// Methods are used to remove the 'Zoom' effect (if any) when the
		// custom resize/move mode is activated
		// Methods are kept on this level because moving to the presenter
		// the code that responds to the mouse events like movement
		// seems like a huge overkill
		private void SaveWindowSizeAndLocation()
		{
			this._baseZoomSize = this.Size;
			this._baseZoomLocation = this.Location;
			this._baseZoomMaximumSize = this.MaximumSize;
		}

		private void RestoreWindowSizeAndLocation()
		{
			this.Size = this._baseZoomSize;
			this.MaximumSize = this._baseZoomMaximumSize;
			this.Location = this._baseZoomLocation;
		}

		private void EnterCustomMouseMode()
		{
			this.RestoreWindowSizeAndLocation();

			this._isCustomMouseModeActive = true;
			this._baseMousePosition = Control.MousePosition;
			this._thumbnailManager.NotifyThumbnailDragStarted(this.Id);
		}

		private void ProcessCustomMouseMode(bool leftButton, bool rightButton)
		{
			Point mousePosition = Control.MousePosition;
			int offsetX = mousePosition.X - this._baseMousePosition.X;
			int offsetY = mousePosition.Y - this._baseMousePosition.Y;
			this._baseMousePosition = mousePosition;

			if (!_config.LockThumbnailLocation)
			{
                // Left + Right buttons trigger thumbnail resize
                // Right button only trigger thumbnail movement
                if (leftButton && rightButton)
                {
                    this.Size = new Size(this.Size.Width + offsetX, this.Size.Height + offsetY);
                    this._baseZoomSize = this.Size;
                }
                else
                {
                    this.Location = new Point(this.Location.X + offsetX, this.Location.Y + offsetY);
                    this._baseZoomLocation = this.Location;
					this.WindowMoved = true;
                }
            }
		}

		private void ExitCustomMouseMode()
		{
			this._isCustomMouseModeActive = false;
			this._thumbnailManager.NotifyThumbnailDragEnded(this.Id);
		}
		#endregion

		#region Custom GUI events
		protected virtual void MouseDownEventHandler(MouseButtons mouseButtons, Keys modifierKeys)
		{
			switch (mouseButtons)
			{
				case MouseButtons.Left when modifierKeys == (Keys.Control | Keys.Shift):
					this.ThumbnailDeactivated?.Invoke(this.Id, true);
					break;
				case MouseButtons.Left when modifierKeys == (Keys.Control | Keys.Alt):
					this.ThumbnailDeactivated?.Invoke(this.Id, false);
					break;
				case MouseButtons.Left when modifierKeys == Keys.Control:
					this.ThumbnailFocusedOverwatchToggle?.Invoke(this.Id);
					break;
				case MouseButtons.Left when modifierKeys == Keys.Shift:
					this.ThumbnailToggleCycleGroup?.Invoke(this.Id);
					break;
				case MouseButtons.Left:
					var oldWindow = this._thumbnailManager.GetActiveClient();
					this.ThumbnailActivated?.Invoke(this.Id);
					this.SetHighlight();
					this.Refresh(true);

					oldWindow?.ClearBorder();
					break;
				case MouseButtons.Right:
				case MouseButtons.Left | MouseButtons.Right:
					this.EnterCustomMouseMode();
					break;
			}
		}
		#endregion
	}
}