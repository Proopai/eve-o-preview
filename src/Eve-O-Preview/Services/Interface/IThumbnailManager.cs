using EveOPreview.View;

namespace EveOPreview.Services
{
	public interface IThumbnailManager
	{
		void Start();
		void Stop();

		void UpdateCycleGroupIndicator();
		void UpdateThumbnailsSize();
		void ApplyOverwatchSettings();
		void UpdateThumbnailFrames();
		void RefreshPortraitOverlays();

		IThumbnailView GetClientByTitle(string title);
		IThumbnailView GetClientByPointer(System.IntPtr ptr);
		IThumbnailView GetActiveClient();

		void SnapThumbnail(System.IntPtr thumbnailId);
		void NotifyThumbnailDragStarted(System.IntPtr thumbnailId);
		void NotifyThumbnailDragEnded(System.IntPtr thumbnailId);

		void ReloadHotkeys();
		void SuspendGlobalHotkeys();
		void ResumeGlobalHotkeys();
	}
}