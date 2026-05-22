using System.Threading;
using System.Threading.Tasks;
using EveOPreview.Mediator.Messages;
using EveOPreview.Services;
using MediatR;

namespace EveOPreview.Mediator.Handlers.Thumbnails
{
	sealed class ThumbnailOverwatchSettingsUpdatedHandler : INotificationHandler<ThumbnailOverwatchSettingsUpdated>
	{
		private readonly IThumbnailManager _manager;

		public ThumbnailOverwatchSettingsUpdatedHandler(IThumbnailManager manager)
		{
			this._manager = manager;
		}

		public Task Handle(ThumbnailOverwatchSettingsUpdated notification, CancellationToken cancellationToken)
		{
			this._manager.ApplyOverwatchSettings();
			return Task.CompletedTask;
		}
	}
}
