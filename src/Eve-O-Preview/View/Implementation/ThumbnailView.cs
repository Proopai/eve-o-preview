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
		private const int CLICK_DRAG_THRESHOLD = 4;
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
		private bool _isCropPanModeActive;
		private bool _isCropPanMoved;

		private double _opacity;
		 
		private DateTime _suppressResizeEventsTimestamp;
		private Size _baseZoomSize;
		private Point _baseZoomLocation;
		private Point _baseMousePosition;
		private Size _baseZoomMaximumSize;
		private Point _baseCropMousePosition;
		private Rectangle _baseCropRegion;
		private Point _rightMouseDownPosition;
		private bool _rightMouseMoved;
		private bool _rightClickPending;

		private HotkeyHandler _hotkeyHandler;

		private IThumbnailConfiguration _config;
		private Lazy<Color> _myBorderColor;
		private Lazy<Color> _preventPreviewColor;
		private Lazy<bool> _preventPreviews;
		private IThumbnailManager _thumbnailManager;
		#endregion

		protected ThumbnailView(IWindowManager windowManager, IThumbnailConfiguration config, IThumbnailManager thumbnailManager)
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
			this._isCropPanModeActive = false;
			this._isCropPanMoved = false;

			this._opacity = 0.1;

			InitializeComponent();

			this._overlay = new ThumbnailOverlay(this,
				this.MouseEnter_Handler,
				this.MouseLeave_Handler,
				this.MouseDown_Handler,
				this.MouseUp_Handler,
				this.MouseMove_Handler,
				this.MouseWheel_Handler
				);

			SetDefaultBorderColor();
			SetPreventPreviews();
			this._overlay.EnableFakePreview(this._preventPreviews.Value, false, 0, SystemColors.Control);
			this._thumbnailManager = thumbnailManager;
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
				this._overlay.EnableFakePreview(this._preventPreviews.Value, false, 0, SystemColors.Control);
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
		public Action<IntPtr, Point> ThumbnailSingleClicked { get; set; }
		public Action<IntPtr, Point> ThumbnailSingleRightClicked { get; set; }

		public Action<IntPtr, bool> ThumbnailDeactivated { get; set; }
		public Action<IntPtr> ThumbnailToggleCycleGroup { get; set; }
		public Action PreviewCropChanged { get; set; }

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
			return this._preventPreviews.Value;
		}
		public void SetPreventPreviews()
		{
			this._preventPreviews = new Lazy<bool>(() =>
			{
				if (this._config.PerClientPreventPreviews.Any(x => x.Key == this.Title))
				{
					return this._config.PerClientPreventPreviews[Title];
				}
				else
				{
					return _config.PreventPreviews;
				}
			});

			this._preventPreviewColor = new Lazy<Color>(() =>
			{
				if (this._config.PerClientPreventPreviewColor.Any(x => x.Key == this.Title))
				{
					return this._config.PerClientPreventPreviewColor[Title];
				}
				else
				{
					return _config.PreventPreviewColor;
				}
			});
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
			if (this._isTopMost == enableTopmost)
			{
				return;
			}

			this._overlay.TopMost = enableTopmost;
			this.TopMost = enableTopmost;

			this._isTopMost = enableTopmost;
		}

		public void SetHighlight()
		{
			SetHighlight(_config.EnableActiveClientHighlight, _config.ActiveClientHighlightThickness);
		}

		public void SetHighlight(bool enabled, int width)
		{
			if (this._isHighlightRequested == enabled)
			{
				return;
			}

			if (enabled)
			{
				this._isHighlightRequested = true;
				this._highlightWidth = width;
				this.BackColor = _myBorderColor.Value;
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
			this.SetHighlight(false, 0);
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
			this.RefreshThumbnail(forceRefresh);
			this.HighlightThumbnail(forceRefresh || this._isSizeChanged);
			this.RefreshOverlay(forceRefresh || this._isSizeChanged || this._isLocationChanged);

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
				this._overlay.EnableFakePreview(this._preventPreviews.Value,false, 0, this._preventPreviewColor.Value);
				return;
			}

			double baseAspectRatio = ((double)baseWidth) / baseHeight;

			int actualHeight = baseHeight - 2 * this._highlightWidth;
			double desiredWidth = actualHeight * baseAspectRatio;
			int actualWidth = (int)Math.Round(desiredWidth, MidpointRounding.AwayFromZero);
			int highlightWidthLeft = (baseWidth - actualWidth) / 2;
			int highlightWidthRight = baseWidth - actualWidth - highlightWidthLeft;

			this._overlay.EnableFakePreview(this._preventPreviews.Value, true, this._highlightWidth, this._preventPreviewColor.Value);
			this.ResizeThumbnail(this.ClientSize.Width, this.ClientSize.Height, this._highlightWidth, highlightWidthRight, this._highlightWidth, highlightWidthLeft);
		}

		private void RefreshOverlay(bool forceRefresh)
		{
			if (this._isOverlayVisible && !forceRefresh)
			{
				// No need to update anything. Everything is already set up
				return;
			}

			// Only show overlay if enabled AND thumbnail is active/visible.
			this._overlay.EnableOverlayLabel(this.IsOverlayEnabled && this.Visible);

			if (!this._isOverlayVisible && ((this.IsOverlayEnabled && this.Visible) || this.IsPreventPreviews() ) && !_config.IsThumbnailDisabled(this.Title) )
			{
				// One-time action to show the Overlay before it is set up
				// Otherwise its position won't be set
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

			this._overlay.SetPropertiesOverlayLabel(_config.OverlayLabelFont, _config.OverlayLabelColor, _config.OverlayLabelAnchor);

			this._overlay.Location = overlayLocation;
			this._overlay.Refresh();
		}

		private void SuppressResizeEvent()
		{
			// Workaround for WinForms issue with the Resize event being fired with inconsistent ClientSize value
			// Any Resize events fired before this timestamp will be ignored
			this._suppressResizeEventsTimestamp = DateTime.UtcNow.AddMilliseconds(_config.ThumbnailResizeTimeoutPeriod);
		}

		private bool IsCropEditEnabled()
		{
			return this._config.EnablePreviewCrop && !this.IsPreventPreviews();
		}

		private Rectangle GetOrCreateCropRegion(Size clientSize)
		{
			var configured = this._config.GetPreviewCropRegion(this.Title, Rectangle.Empty);
			if (configured.Width <= 0 || configured.Height <= 0)
			{
				var initialized = new Rectangle(0, 0, clientSize.Width, clientSize.Height);
				if (!string.IsNullOrEmpty(this.Title))
				{
					this._config.SetPreviewCropRegion(this.Title, initialized);
				}
				return initialized;
			}

			return this.ClampCropRegion(configured, clientSize);
		}

		private Rectangle ClampCropRegion(Rectangle region, Size clientSize)
		{
			var width = Math.Max(1, Math.Min(region.Width, clientSize.Width));
			var height = Math.Max(1, Math.Min(region.Height, clientSize.Height));

			var x = Math.Max(0, Math.Min(region.X, clientSize.Width - width));
			var y = Math.Max(0, Math.Min(region.Y, clientSize.Height - height));

			return new Rectangle(x, y, width, height);
		}

		private void ApplyCropRegion(Rectangle region, bool persist)
		{
			var clientSize = this.WindowManager.GetClientSize(this.Id);
			if (clientSize.Width <= 0 || clientSize.Height <= 0)
			{
				return;
			}

			var clamped = this.ClampCropRegion(region, clientSize);
			if (this._config.GetPreviewCropRegion(this.Title, Rectangle.Empty) == clamped)
			{
				return;
			}

			this._config.SetPreviewCropRegion(this.Title, clamped);
			this.Refresh(true);

			if (persist)
			{
				this.PreviewCropChanged?.Invoke();
			}
		}

		private Point MapPreviewToClientPoint(Point previewPoint)
		{
			var clientSize = this.WindowManager.GetClientSize(this.Id);
			if (clientSize.Width <= 0 || clientSize.Height <= 0 || this.ClientSize.Width <= 0 || this.ClientSize.Height <= 0)
			{
				return new Point(0, 0);
			}

			Rectangle viewport = this.GetPreviewViewport();
			if (viewport.Width <= 0 || viewport.Height <= 0)
			{
				viewport = new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);
			}

			int px = Math.Max(viewport.Left, Math.Min(previewPoint.X, viewport.Right - 1));
			int py = Math.Max(viewport.Top, Math.Min(previewPoint.Y, viewport.Bottom - 1));

			int viewX = px - viewport.Left;
			int viewY = py - viewport.Top;

			Rectangle sourceRegion = this.GetOrCreateCropRegion(clientSize);
			if (!this._config.EnablePreviewCrop)
			{
				sourceRegion = new Rectangle(0, 0, clientSize.Width, clientSize.Height);
			}

			var clientX = sourceRegion.X + (int)Math.Round(((double)viewX / viewport.Width) * sourceRegion.Width, MidpointRounding.AwayFromZero);
			var clientY = sourceRegion.Y + (int)Math.Round(((double)viewY / viewport.Height) * sourceRegion.Height, MidpointRounding.AwayFromZero);

			clientX = Math.Max(0, Math.Min(clientX, Math.Max(0, clientSize.Width - 1)));
			clientY = Math.Max(0, Math.Min(clientY, Math.Max(0, clientSize.Height - 1)));

			return new Point(clientX, clientY);
		}

		protected virtual Rectangle GetPreviewViewport()
		{
			return new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);
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
			if (e.Button.HasFlag(MouseButtons.Right) && this._rightClickPending && !this._isCustomMouseModeActive)
			{
				this.UpdateRightClickTracking();
				if (this._rightMouseMoved)
				{
					this.EnterCustomMouseMode();
				}
			}

			if (this._isCustomMouseModeActive && e.Button.HasFlag(MouseButtons.Right))
			{
				this.UpdateRightClickTracking();
			}

			if (this._isCropPanModeActive && Control.MouseButtons.HasFlag(MouseButtons.Left))
			{
				this.ProcessCropPanMode();
				return;
			}

			if (this._isCustomMouseModeActive)
			{
				this.ProcessCustomMouseMode(e.Button.HasFlag(MouseButtons.Left), e.Button.HasFlag(MouseButtons.Right));
			}
		}

		private void MouseUp_Handler(object sender, MouseEventArgs e)
		{
			if ((e.Button == MouseButtons.Left) && this._isCropPanModeActive)
			{
				bool moved = this._isCropPanMoved;
				this.ExitCropPanMode();
				if (moved)
				{
					this.PreviewCropChanged?.Invoke();
				}
				else
				{
					this.ActivateThumbnailFromPreview(this.PointToClient(Control.MousePosition), MouseButtons.Left, true);
				}
				return;
			}

			if (e.Button == MouseButtons.Right)
			{
				this.ExitCropPanMode();
				this._rightClickPending = false;

				if (this._isCustomMouseModeActive)
				{
					this.ExitCustomMouseMode();

					// Snap to Grid on release of mouse (if moved)
					if (_config.ThumbnailSnapToGrid && this.WindowMoved)
					{
						var x = (int)Math.Round((double)this.Location.X / (double)_config.ThumbnailSnapToGridSizeX) * _config.ThumbnailSnapToGridSizeX;
	                    var y = (int)Math.Round((double)this.Location.Y / (double)_config.ThumbnailSnapToGridSizeY) * _config.ThumbnailSnapToGridSizeY;
						this.Location = new Point(x, y);
						this._baseZoomLocation = this.Location;

						this.WindowMoved = false;

					}
				}
				else
				{
					this.ActivateThumbnailFromPreview(this.PointToClient(Control.MousePosition), MouseButtons.Right, true);
				}

				this._rightMouseMoved = false;
			}
		}

		private void MouseWheel_Handler(object sender, MouseEventArgs e)
		{
			if (!this.IsCropEditEnabled() || (e.Delta == 0))
			{
				return;
			}

			var clientSize = this.WindowManager.GetClientSize(this.Id);
			if (clientSize.Width <= 1 || clientSize.Height <= 1 || this.ClientSize.Width <= 1 || this.ClientSize.Height <= 1)
			{
				return;
			}

			var currentRegion = this.GetOrCreateCropRegion(clientSize);

			var pointer = this.PointToClient(Control.MousePosition);
			pointer.X = Math.Max(0, Math.Min(pointer.X, this.ClientSize.Width));
			pointer.Y = Math.Max(0, Math.Min(pointer.Y, this.ClientSize.Height));

			var sourcePointerX = currentRegion.X + ((double)pointer.X / this.ClientSize.Width) * currentRegion.Width;
			var sourcePointerY = currentRegion.Y + ((double)pointer.Y / this.ClientSize.Height) * currentRegion.Height;

			var scale = e.Delta > 0 ? 0.9 : 1.1;
			var targetWidth = Math.Max(64, (int)Math.Round(currentRegion.Width * scale, MidpointRounding.AwayFromZero));
			var targetHeight = Math.Max(36, (int)Math.Round(currentRegion.Height * scale, MidpointRounding.AwayFromZero));

			var targetX = (int)Math.Round(sourcePointerX - ((double)pointer.X / this.ClientSize.Width) * targetWidth, MidpointRounding.AwayFromZero);
			var targetY = (int)Math.Round(sourcePointerY - ((double)pointer.Y / this.ClientSize.Height) * targetHeight, MidpointRounding.AwayFromZero);

			this.ApplyCropRegion(new Rectangle(targetX, targetY, targetWidth, targetHeight), true);
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
		}

		private void BeginRightClickTracking()
		{
			this._rightMouseDownPosition = Control.MousePosition;
			this._rightMouseMoved = false;
			this._rightClickPending = true;
		}

		private void UpdateRightClickTracking()
		{
			if (this._rightMouseMoved)
			{
				return;
			}

			var current = Control.MousePosition;
			int dx = Math.Abs(current.X - this._rightMouseDownPosition.X);
			int dy = Math.Abs(current.Y - this._rightMouseDownPosition.Y);
			if (dx >= CLICK_DRAG_THRESHOLD || dy >= CLICK_DRAG_THRESHOLD)
			{
				this._rightMouseMoved = true;
			}
		}

		private void EnterCropPanMode()
		{
			var clientSize = this.WindowManager.GetClientSize(this.Id);
			if (clientSize.Width <= 0 || clientSize.Height <= 0)
			{
				return;
			}

			this._isCropPanModeActive = true;
			this._isCropPanMoved = false;
			this._baseCropMousePosition = Control.MousePosition;
			this._baseCropRegion = this.GetOrCreateCropRegion(clientSize);
		}

		private void ProcessCropPanMode()
		{
			var clientSize = this.WindowManager.GetClientSize(this.Id);
			if (clientSize.Width <= 0 || clientSize.Height <= 0 || this.ClientSize.Width <= 0 || this.ClientSize.Height <= 0)
			{
				return;
			}

			Point mousePosition = Control.MousePosition;
			int offsetX = mousePosition.X - this._baseCropMousePosition.X;
			int offsetY = mousePosition.Y - this._baseCropMousePosition.Y;

			var sourceOffsetX = -(int)Math.Round(((double)offsetX / this.ClientSize.Width) * this._baseCropRegion.Width, MidpointRounding.AwayFromZero);
			var sourceOffsetY = -(int)Math.Round(((double)offsetY / this.ClientSize.Height) * this._baseCropRegion.Height, MidpointRounding.AwayFromZero);

			var movedRegion = new Rectangle(
				this._baseCropRegion.X + sourceOffsetX,
				this._baseCropRegion.Y + sourceOffsetY,
				this._baseCropRegion.Width,
				this._baseCropRegion.Height
			);

			if (sourceOffsetX != 0 || sourceOffsetY != 0)
			{
				this._isCropPanMoved = true;
			}

			this.ApplyCropRegion(movedRegion, false);
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
		}

		private void ExitCropPanMode()
		{
			this._isCropPanModeActive = false;
			this._isCropPanMoved = false;
		}
		#endregion

		#region Custom GUI events
		private void ActivateThumbnailFromPreview(Point previewPoint, MouseButtons clickButton, bool isSingleClick)
		{
			var oldWindow = this._thumbnailManager.GetActiveClient();
			if (isSingleClick)
			{
				Point mappedPoint = this.MapPreviewToClientPoint(previewPoint);
				if (clickButton == MouseButtons.Left)
				{
					this.ThumbnailSingleClicked?.Invoke(this.Id, mappedPoint);
				}
				else if (clickButton == MouseButtons.Right)
				{
					this.ThumbnailSingleRightClicked?.Invoke(this.Id, mappedPoint);
				}
			}
			this.ThumbnailActivated?.Invoke(this.Id);
			this.SetHighlight();
			this.Refresh(true);

			oldWindow?.ClearBorder();
		}

		protected virtual void MouseDownEventHandler(MouseButtons mouseButtons, Keys modifierKeys)
		{
			switch (mouseButtons)
			{
				case MouseButtons.Left when modifierKeys == Keys.Control:
					this.ThumbnailDeactivated?.Invoke(this.Id, false);
					break;
				case MouseButtons.Left when modifierKeys == Keys.Shift:
					this.ThumbnailToggleCycleGroup?.Invoke(this.Id);
					break;
				case MouseButtons.Left when modifierKeys == (Keys.Control | Keys.Shift):
					this.ThumbnailDeactivated?.Invoke(this.Id, true);
					break;
				case MouseButtons.Left:
					if (this.IsCropEditEnabled())
					{
						this.EnterCropPanMode();
						break;
					}
					this.ActivateThumbnailFromPreview(this.PointToClient(Control.MousePosition), MouseButtons.Left, true);
					break;
				case MouseButtons.Right:
					this.ExitCropPanMode();
					this.BeginRightClickTracking();
					break;
				case MouseButtons.Left | MouseButtons.Right:
					this.EnterCustomMouseMode();
					break;
			}
		}
		#endregion
	}
}
