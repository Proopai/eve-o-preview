using System;
using System.Text.Json;

namespace EveOPreview.NamedPipe
{

	public record PipeEnvelope(
		Guid RequestId,
		string Type,
		JsonElement Payload);

	public record PipeResponseEnvelope(
		Guid RequestId,
		bool Success,
		string Message);
}
