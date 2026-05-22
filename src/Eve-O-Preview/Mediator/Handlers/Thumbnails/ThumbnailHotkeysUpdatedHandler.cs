using System.Threading;
using System.Threading.Tasks;
using EveOPreview.Mediator.Messages;
using EveOPreview.Services;
using MediatR;

namespace EveOPreview.Mediator.Handlers.Thumbnails
{
	sealed class ThumbnailHotkeysUpdatedHandler : INotificationHandler<ThumbnailHotkeysUpdated>
	{
		private readonly IThumbnailManager _manager;

		public ThumbnailHotkeysUpdatedHandler(IThumbnailManager manager)
		{
			this._manager = manager;
		}

		public Task Handle(ThumbnailHotkeysUpdated notification, CancellationToken cancellationToken)
		{
			this._manager.ReloadHotkeys();
			return Task.CompletedTask;
		}
	}
}
