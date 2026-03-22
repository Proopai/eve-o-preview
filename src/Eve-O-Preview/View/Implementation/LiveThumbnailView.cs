using System;
using System.Drawing;
using EveOPreview.Configuration;
using EveOPreview.Services;

namespace EveOPreview.View
{
	sealed class LiveThumbnailView : ThumbnailView
	{
		#region Private fields
		private IDwmThumbnail _thumbnail;
		private Point _startLocation;
		private Point _endLocation;
		private IThumbnailConfiguration _config;
		private Rectangle _cachedSourceRect;
		private bool _cachedUseSourceRect;
		#endregion

		public LiveThumbnailView(IWindowManager windowManager, IThumbnailConfiguration config, IThumbnailManager thumbnailManager)
			: base(windowManager, config, thumbnailManager)
		{
			this._startLocation = new Point(0, 0);
			this._endLocation = new Point(this.ClientSize);
			this._config = config;
			this._cachedSourceRect = Rectangle.Empty;
			this._cachedUseSourceRect = false;
		}

		protected override void RefreshThumbnail(bool forceRefresh)
		{
			// To prevent flickering the old broken thumbnail is removed AFTER the new shiny one is created
			IDwmThumbnail obsoleteThumbnail = forceRefresh ? this._thumbnail : null;

			if ((this._thumbnail == null) || forceRefresh && ! this.IsPreventPreviews() )
			{
				this.RegisterThumbnail();
			}

			if (this._thumbnail != null)
			{
				var sourceRect = this.GetConfiguredSourceRectangle();
				var useSourceRect = sourceRect.Width > 0 && sourceRect.Height > 0;

				if ((useSourceRect != this._cachedUseSourceRect) || (useSourceRect && sourceRect != this._cachedSourceRect))
				{
					if (useSourceRect)
					{
						this._thumbnail.SetSource(sourceRect);
					}
					else
					{
						this._thumbnail.ClearSource();
					}

					this._cachedUseSourceRect = useSourceRect;
					this._cachedSourceRect = sourceRect;
					this._thumbnail.Update();
				}
			}
			
			obsoleteThumbnail?.Unregister();
		}

		protected override void ResizeThumbnail(int baseWidth, int baseHeight, int highlightWidthTop, int highlightWidthRight, int highlightWidthBottom, int highlightWidthLeft)
		{
			var left = 0 + highlightWidthLeft;
			var top = 0 + highlightWidthTop;
			var right = baseWidth - highlightWidthRight;
			var bottom = baseHeight - highlightWidthBottom;

			if ((this._startLocation.X == left) && (this._startLocation.Y == top) && (this._endLocation.X == right) && (this._endLocation.Y == bottom))
			{
				return; // No update required
			}
			this._startLocation = new Point(left, top);
			this._endLocation = new Point(right, bottom);

			this._thumbnail.Move(left, top, right, bottom);
			this._thumbnail.Update();
		}

		protected override Rectangle GetPreviewViewport()
		{
			int width = Math.Max(1, this._endLocation.X - this._startLocation.X);
			int height = Math.Max(1, this._endLocation.Y - this._startLocation.Y);
			return new Rectangle(this._startLocation.X, this._startLocation.Y, width, height);
		}

		private void RegisterThumbnail()
		{
			this._thumbnail = this.WindowManager.GetLiveThumbnail(this.Handle, this.Id);
			this._cachedSourceRect = Rectangle.Empty;
			this._cachedUseSourceRect = false;
			this._thumbnail.Move(this._startLocation.X, this._startLocation.Y, this._endLocation.X, this._endLocation.Y);
			this._thumbnail.Update();
		}

		private Rectangle GetConfiguredSourceRectangle()
		{
			if (!this._config.EnablePreviewCrop)
			{
				return Rectangle.Empty;
			}

			var clientSize = this.WindowManager.GetClientSize(this.Id);
			var raw = this._config.GetPreviewCropRegion(this.Title, Rectangle.Empty);

			var left = Math.Max(0, raw.Left);
			var top = Math.Max(0, raw.Top);
			var width = raw.Width;
			var height = raw.Height;

			if ((width <= 0) || (height <= 0) || (left >= clientSize.Width) || (top >= clientSize.Height))
			{
				return Rectangle.Empty;
			}

			width = Math.Min(width, clientSize.Width - left);
			height = Math.Min(height, clientSize.Height - top);

			return new Rectangle(left, top, width, height);
		}
	}
}
