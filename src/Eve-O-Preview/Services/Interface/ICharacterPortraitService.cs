using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace EveOPreview.Services
{
	public interface ICharacterPortraitService
	{
		/// <summary>
		/// Ensures the thumbs directory exists, then downloads portraits for configured clients that are missing on disk.
		/// Does not block the caller.
		/// </summary>
		void SyncMissingPortraitsFromConfiguration();

		/// <summary>
		/// Downloads portraits for the given EVE window titles (e.g. "EVE - Character Name").
		/// </summary>
		Task RefreshPortraitsAsync(IEnumerable<string> windowTitles, bool forceRedownload, CancellationToken cancellationToken = default);

		/// <summary>
		/// Re-downloads portraits for every client title referenced in settings.
		/// </summary>
		Task RefreshAllConfiguredPortraitsAsync(CancellationToken cancellationToken = default);

		IReadOnlyList<string> GetConfiguredClientTitles();

		IReadOnlyList<string> GetClientsMissingPortraitFiles();

		bool TryGetPortraitPath(string windowTitle, out string path);

		/// <summary>Loads a copy of the cached portrait for the client, or null if unavailable. Caller must dispose the image.</summary>
		Image TryLoadPortraitImage(string windowTitle);
	}
}
