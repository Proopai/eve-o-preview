using EveOPreview.NamedPipe;
using System;
using System.IO;
using System.IO.Pipes;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

public sealed class PipeServer : IDisposable
{
	private readonly CancellationTokenSource _cts = new();
	private Task? _serverTask;

	public event EventHandler<PipeEnvelope>? MessageReceived;

	public void Start()
	{
		_serverTask = Task.Run(() => RunAsync(_cts.Token));
	}

	private async Task RunAsync(CancellationToken token)
	{
		while (!token.IsCancellationRequested)
		{
			var pipe = new NamedPipeServerStream(
				"EVE-O-Preview-Pipe",
				PipeDirection.InOut,
				NamedPipeServerStream.MaxAllowedServerInstances,
				PipeTransmissionMode.Byte,
				PipeOptions.Asynchronous);

			try
			{
				await pipe.WaitForConnectionAsync(token);

				_ = Task.Run(
					() => HandleClientAsync(pipe, token),
					token);
			}
			catch (OperationCanceledException)
			{
				pipe.Dispose();
				break;
			}
		}
	}

	private async Task HandleClientAsync(
		NamedPipeServerStream pipe,
		CancellationToken token)
	{
		await using (pipe)
		{
			using var reader = new StreamReader(pipe);
			using var writer = new StreamWriter(pipe)
			{
				AutoFlush = true
			};

			while (!token.IsCancellationRequested &&
				   pipe.IsConnected)
			{
				var line = await reader.ReadLineAsync();

				if (line == null)
					break;

				var message =
					JsonSerializer.Deserialize<PipeEnvelope>(line);

				if (message == null)
					continue;

				MessageReceived?.Invoke(this, message);

				var response = new PipeResponseEnvelope(
					message.RequestId,
					true,
					"OK");

				await writer.WriteLineAsync(
					JsonSerializer.Serialize(response));
			}
		}
	}

	public void Dispose()
	{
		_cts.Cancel();

		try
		{
			_serverTask?.Wait(TimeSpan.FromSeconds(2));
		}
		catch
		{
		}

		_cts.Dispose();
	}
}