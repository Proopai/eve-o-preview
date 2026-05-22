using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using EveOPreview.Configuration;
using EveOPreview.Mediator.Messages;
using MediatR;

namespace EveOPreview.Services.Implementation
{
	sealed class CharacterPortraitService : ICharacterPortraitService, IDisposable
	{
		private const string DefaultClientTitle = "EVE";
		private const string ThumbsFolderName = "thumbs";
		private const string PortraitLogFileName = "portrait-fetch.log";
		private const string UserAgent = "EVE-O-Preview/8.0 (character portrait cache; contact via GitHub eve-o-preview)";
		private const int MaxParallelDownloads = 6;

		private static readonly HttpClient SharedHttpClient = CreateHttpClient();

		private readonly IThumbnailConfiguration _configuration;
		private readonly IConfigurationStorage _configurationStorage;
		private readonly IMediator _mediator;
		private readonly object _syncRoot = new object();
		private readonly SemaphoreSlim _refreshGate = new SemaphoreSlim(1, 1);

		public CharacterPortraitService(IThumbnailConfiguration configuration, IConfigurationStorage configurationStorage, IMediator mediator)
		{
			this._configuration = configuration;
			this._configurationStorage = configurationStorage;
			this._mediator = mediator;
		}

		public void SyncMissingPortraitsFromConfiguration()
		{
			_ = Task.Run(async () =>
			{
				try
				{
					var missing = this.GetClientsMissingPortraitFiles();
					if (missing.Count == 0)
					{
						this.Log("Startup sync: all configured clients already have portrait files.");
						return;
					}

					this.Log($"Startup sync: downloading {missing.Count} missing portrait(s).");
					await this.RefreshPortraitsAsync(missing, forceRedownload: false).ConfigureAwait(false);
				}
				catch (Exception ex)
				{
					this.Log($"Startup sync failed: {ex.Message}");
					Debug.WriteLine(ex);
				}
			});
		}

		public Task RefreshAllConfiguredPortraitsAsync(CancellationToken cancellationToken = default)
		{
			return this.RefreshPortraitsAsync(this.GetConfiguredClientTitles(), forceRedownload: true, cancellationToken);
		}

		public async Task RefreshPortraitsAsync(IEnumerable<string> windowTitles, bool forceRedownload, CancellationToken cancellationToken = default)
		{
			var titles = windowTitles?
				.Where(title => this.IsPortraitClientTitle(title))
				.Distinct(StringComparer.OrdinalIgnoreCase)
				.ToList() ?? new List<string>();

			if (titles.Count == 0)
			{
				return;
			}

			await this._refreshGate.WaitAsync(cancellationToken).ConfigureAwait(false);
			try
			{
				string thumbsDirectory = this.EnsureThumbsDirectory();
				this.Log(forceRedownload
					? $"Manual refresh: re-downloading {titles.Count} portrait(s)."
					: $"Downloading {titles.Count} portrait(s).");

				using var throttler = new SemaphoreSlim(MaxParallelDownloads, MaxParallelDownloads);
				var downloadTasks = titles.Select(title => this.DownloadPortraitWithThrottleAsync(
					throttler,
					title,
					thumbsDirectory,
					forceRedownload,
					cancellationToken));

				await Task.WhenAll(downloadTasks).ConfigureAwait(false);

				lock (this._syncRoot)
				{
					this._configurationStorage.Save();
				}

				await this._mediator.Publish(new ThumbnailPortraitsUpdated(), cancellationToken).ConfigureAwait(false);

				this.Log("Portrait refresh completed.");
			}
			finally
			{
				this._refreshGate.Release();
			}
		}

		public IReadOnlyList<string> GetConfiguredClientTitles()
		{
			var titles = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

			void AddKeys(IEnumerable<string> keys)
			{
				if (keys == null)
				{
					return;
				}

				foreach (string key in keys)
				{
					if (this.IsPortraitClientTitle(key))
					{
						titles.Add(key);
					}
				}
			}

			AddKeys(this._configuration.ClientPortraitPaths?.Keys);
			AddKeys(this._configuration.PerClientPreventPreviews?.Keys);
			AddKeys(this._configuration.PerClientPreventPreviewColor?.Keys);
			AddKeys(this._configuration.PerClientActiveClientHighlightColor?.Keys);
			AddKeys(this._configuration.PerClientThumbnailSize?.Keys);
			AddKeys(this._configuration.CycleGroup1ClientsOrder?.Keys);
			AddKeys(this._configuration.CycleGroup2ClientsOrder?.Keys);
			AddKeys(this._configuration.CycleGroup3ClientsOrder?.Keys);
			AddKeys(this._configuration.CycleGroup4ClientsOrder?.Keys);
			AddKeys(this._configuration.CycleGroup5ClientsOrder?.Keys);
			AddKeys(this._configuration.CycleGroupExclusions?.Keys);

			return titles.OrderBy(title => title, StringComparer.OrdinalIgnoreCase).ToList();
		}

		public IReadOnlyList<string> GetClientsMissingPortraitFiles()
		{
			this.EnsureThumbsDirectory();

			var missing = new List<string>();
			foreach (string title in this.GetConfiguredClientTitles())
			{
				if (!this.TryGetPortraitPath(title, out string path) || !File.Exists(path))
				{
					missing.Add(title);
				}
			}

			return missing;
		}

		public void Dispose()
		{
			this._refreshGate.Dispose();
		}

		private static HttpClient CreateHttpClient()
		{
			var client = new HttpClient
			{
				Timeout = TimeSpan.FromSeconds(30)
			};
			client.DefaultRequestHeaders.UserAgent.ParseAdd(UserAgent);
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			return client;
		}

		private async Task DownloadPortraitWithThrottleAsync(
			SemaphoreSlim throttler,
			string windowTitle,
			string thumbsDirectory,
			bool forceRedownload,
			CancellationToken cancellationToken)
		{
			await throttler.WaitAsync(cancellationToken).ConfigureAwait(false);
			try
			{
				await this.DownloadPortraitForClientAsync(windowTitle, thumbsDirectory, forceRedownload, cancellationToken)
					.ConfigureAwait(false);
			}
			finally
			{
				throttler.Release();
			}
		}

		private async Task DownloadPortraitForClientAsync(
			string windowTitle,
			string thumbsDirectory,
			bool forceRedownload,
			CancellationToken cancellationToken)
		{
			if (!CharacterPortraitNaming.TryGetCharacterName(windowTitle, out string characterName))
			{
				this.Log($"Skipped '{windowTitle}': not a logged-in character window.");
				return;
			}

			try
			{
				if (!forceRedownload && this.TryGetPortraitPath(windowTitle, out string existingPath) && File.Exists(existingPath))
				{
					this.Log($"Skipped '{windowTitle}': portrait already exists at {existingPath}");
					return;
				}

				int? characterId = await this.ResolveCharacterIdAsync(characterName, cancellationToken).ConfigureAwait(false);
				if (characterId == null)
				{
					this.Log($"Failed '{windowTitle}': could not resolve ESI character id for '{characterName}'.");
					return;
				}

				string portraitUrl = await this.GetPortraitUrlAsync(characterId.Value, cancellationToken).ConfigureAwait(false);
				if (string.IsNullOrEmpty(portraitUrl))
				{
					this.Log($"Failed '{windowTitle}': ESI returned no portrait URL for id {characterId.Value}.");
					return;
				}

				string destinationPath = Path.Combine(thumbsDirectory, $"{characterId.Value}.png");
				await this.DownloadFileAsync(portraitUrl, destinationPath, cancellationToken).ConfigureAwait(false);

				lock (this._syncRoot)
				{
					this._configuration.ClientPortraitPaths[windowTitle] = destinationPath;
				}

				this.Log($"Saved '{windowTitle}' -> {destinationPath}");
			}
			catch (Exception ex) when (ex is not OperationCanceledException)
			{
				this.Log($"Failed '{windowTitle}': {ex.Message}");
				Debug.WriteLine(ex);
			}
		}

		private async Task<int?> ResolveCharacterIdAsync(string characterName, CancellationToken cancellationToken)
		{
			using var request = new HttpRequestMessage(HttpMethod.Post, "https://esi.evetech.net/latest/universe/ids/?datasource=tranquility")
			{
				Content = new StringContent(JsonSerializer.Serialize(new[] { characterName }), Encoding.UTF8, "application/json")
			};

			using HttpResponseMessage response = await SharedHttpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
			response.EnsureSuccessStatusCode();

			await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
			using JsonDocument document = await JsonDocument.ParseAsync(stream, cancellationToken: cancellationToken).ConfigureAwait(false);

			if (!document.RootElement.TryGetProperty("characters", out JsonElement characters) || characters.ValueKind != JsonValueKind.Array)
			{
				return null;
			}

			foreach (JsonElement entry in characters.EnumerateArray())
			{
				if (!entry.TryGetProperty("id", out JsonElement idElement))
				{
					continue;
				}

				if (entry.TryGetProperty("name", out JsonElement nameElement)
					&& string.Equals(nameElement.GetString(), characterName, StringComparison.OrdinalIgnoreCase))
				{
					return idElement.GetInt32();
				}
			}

			if (characters.GetArrayLength() > 0 && characters[0].TryGetProperty("id", out JsonElement fallbackId))
			{
				return fallbackId.GetInt32();
			}

			return null;
		}

		private async Task<string> GetPortraitUrlAsync(int characterId, CancellationToken cancellationToken)
		{
			string url = $"https://esi.evetech.net/latest/characters/{characterId}/portrait/?datasource=tranquility";
			using HttpResponseMessage response = await SharedHttpClient.GetAsync(url, cancellationToken).ConfigureAwait(false);
			response.EnsureSuccessStatusCode();

			await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
			using JsonDocument document = await JsonDocument.ParseAsync(stream, cancellationToken: cancellationToken).ConfigureAwait(false);

			if (document.RootElement.TryGetProperty("px256x256", out JsonElement large))
			{
				return large.GetString();
			}

			if (document.RootElement.TryGetProperty("px128x128", out JsonElement medium))
			{
				return medium.GetString();
			}

			if (document.RootElement.TryGetProperty("px64x64", out JsonElement small))
			{
				return small.GetString();
			}

			return null;
		}

		private async Task DownloadFileAsync(string url, string destinationPath, CancellationToken cancellationToken)
		{
			if (File.Exists(destinationPath))
			{
				File.Delete(destinationPath);
			}

			using HttpResponseMessage response = await SharedHttpClient.GetAsync(url, cancellationToken).ConfigureAwait(false);
			response.EnsureSuccessStatusCode();

			await using Stream source = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
			await using FileStream destination = File.Create(destinationPath);
			await source.CopyToAsync(destination, cancellationToken).ConfigureAwait(false);
		}

		private string EnsureThumbsDirectory()
		{
			lock (this._syncRoot)
			{
				if (this._configuration.ClientPortraitPaths == null)
				{
					this._configuration.ClientPortraitPaths = new Dictionary<string, string>();
				}

				if (string.IsNullOrWhiteSpace(this._configuration.PortraitThumbnailsDirectory))
				{
					string exeDirectory = AppContext.BaseDirectory;
					this._configuration.PortraitThumbnailsDirectory = Path.Combine(exeDirectory, ThumbsFolderName);
				}

				Directory.CreateDirectory(this._configuration.PortraitThumbnailsDirectory);
				return this._configuration.PortraitThumbnailsDirectory;
			}
		}

		public bool TryGetPortraitPath(string windowTitle, out string path)
		{
			if (this._configuration.ClientPortraitPaths != null
				&& this._configuration.ClientPortraitPaths.TryGetValue(windowTitle, out path)
				&& !string.IsNullOrWhiteSpace(path))
			{
				return true;
			}

			path = null;
			return false;
		}

		public Image TryLoadPortraitImage(string windowTitle)
		{
			if (!this.TryGetPortraitPath(windowTitle, out string path) || !File.Exists(path))
			{
				return null;
			}

			try
			{
				using FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
				return Image.FromStream(stream);
			}
			catch (IOException ex)
			{
				this.Log($"Failed to load portrait for '{windowTitle}': {ex.Message}");
				return null;
			}
		}

		private bool IsPortraitClientTitle(string title)
		{
			return !string.IsNullOrWhiteSpace(title)
				&& !string.Equals(title, DefaultClientTitle, StringComparison.OrdinalIgnoreCase)
				&& CharacterPortraitNaming.TryGetCharacterName(title, out _);
		}

		private void Log(string message)
		{
			string line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
			Debug.WriteLine(line);

			try
			{
				string logPath = Path.Combine(this.EnsureThumbsDirectory(), PortraitLogFileName);
				File.AppendAllText(logPath, line + Environment.NewLine);
			}
			catch (IOException)
			{
				// Ignore log write failures.
			}
		}
	}

	static class CharacterPortraitNaming
	{
		public static bool TryGetCharacterName(string windowTitle, out string characterName)
		{
			const string evePrefix = "EVE - ";
			const string frontierPrefix = "EVE Frontier - ";

			if (windowTitle.StartsWith(frontierPrefix, StringComparison.OrdinalIgnoreCase))
			{
				characterName = windowTitle.Substring(frontierPrefix.Length).Trim();
				return characterName.Length > 0;
			}

			if (windowTitle.StartsWith(evePrefix, StringComparison.OrdinalIgnoreCase))
			{
				characterName = windowTitle.Substring(evePrefix.Length).Trim();
				return characterName.Length > 0;
			}

			characterName = null;
			return false;
		}
	}
}
