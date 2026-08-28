using EveOPreview.NamedPipe.Messages;
using MediatR;
using System.Collections.Generic;
using System.Text.Json;

namespace EveOPreview.Mediator.Messages
{
	sealed class ThumbnailPipeMessage : INotification
	{
		public ThumbnailPipeMessage(string type, JsonElement payload)
		{
			this.Payload = payload;
			this.Type = type;
		}

		public JsonElement Payload { get; }
		public string Type { get; }
	}
}