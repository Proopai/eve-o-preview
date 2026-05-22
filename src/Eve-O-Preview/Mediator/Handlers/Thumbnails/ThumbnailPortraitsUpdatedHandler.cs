using System.Threading;
using System.Threading.Tasks;
using EveOPreview.Mediator.Messages;
using EveOPreview.Services;
using MediatR;

namespace EveOPreview.Mediator.Handlers.Thumbnails
{
	sealed class ThumbnailPortraitsUpdatedHandler : INotificationHandler<ThumbnailPortraitsUpdated>
	{
		private readonly IThumbnailManager _manager;

		public ThumbnailPortraitsUpdatedHandler(IThumbnailManager manager)
		{
			this._manager = manager;
		}

		public Task Handle(ThumbnailPortraitsUpdated notification, CancellationToken cancellationToken)
		{
			this._manager.RefreshPortraitOverlays();

			return Task.CompletedTask;
		}
	}
}
