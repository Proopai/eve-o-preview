using EveOPreview.Mediator.Messages;
using EveOPreview.NamedPipe.Messages;
using EveOPreview.Services;
using MediatR;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace EveOPreview.Mediator.Handlers.Thumbnails
{
	sealed class ThumbnailPipeMessageHandler : INotificationHandler<ThumbnailPipeMessage>
	{
		private readonly IThumbnailManager _manager;

		public ThumbnailPipeMessageHandler(IThumbnailManager manager)
		{
			this._manager = manager;
		}

		public Task Handle(ThumbnailPipeMessage message, CancellationToken cancellationToken)
		{

			try
			{
				_manager.ProcessPipeMessage(message.Type, message.Payload);
			}
			catch (Exception ex)
			{ 
			}

			return Task.CompletedTask;
		}
	}
}