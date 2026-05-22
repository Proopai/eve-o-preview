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
		#endregion

		public LiveThumbnailView(IWindowManager windowManager, IThumbnailConfiguration config, IThumbnailManager thumbnailManager, ICharacterPortraitService characterPortraitService)
			: base(windowManager, config, thumbnailManager, characterPortraitService)
		{
			this._startLocation = new Point(0, 0);
			this._endLocation = new Point(this.ClientSize);
		}

		protected override void RefreshThumbnail(bool forceRefresh)
		{
			if (this.IsPreventPreviews())
			{
				this.UnregisterThumbnail();
				return;
			}

			// To prevent flickering the old broken thumbnail is removed AFTER the new shiny one is created
			IDwmThumbnail obsoleteThumbnail = forceRefresh ? this._thumbnail : null;

			if ((this._thumbnail == null) || (forceRefresh && !this.IsPreventPreviews()))
			{
				this.RegisterThumbnail();
			}

			obsoleteThumbnail?.Unregister();
		}

		protected override void ResizeThumbnail(int baseWidth, int baseHeight, int highlightWidthTop, int highlightWidthRight, int highlightWidthBottom, int highlightWidthLeft)
		{
			if (this.IsPreventPreviews() || this._thumbnail == null)
			{
				return;
			}

			var left = 0 + highlightWidthLeft;
			var top = 0 + highlightWidthTop;
			var right = baseWidth - highlightWidthRight;
			var bottom = baseHeight - highlightWidthBottom;

			if ((this._startLocation.X == left) && (this._startLocation.Y == top) && (this._endLocation.X == right) && (this._endLocation.Y == bottom))
			{
				return;
			}
			this._startLocation = new Point(left, top);
			this._endLocation = new Point(right, bottom);

			this._thumbnail.Move(left, top, right, bottom);
			this._thumbnail.Update();
		}

		private void RegisterThumbnail()
		{
			this._thumbnail = this.WindowManager.GetLiveThumbnail(this.Handle, this.Id);
			this._thumbnail.Move(this._startLocation.X, this._startLocation.Y, this._endLocation.X, this._endLocation.Y);
			this._thumbnail.Update();
		}

		private void UnregisterThumbnail()
		{
			if (this._thumbnail == null)
			{
				return;
			}

			this._thumbnail.Unregister();
			this._thumbnail = null;
		}
	}
}
