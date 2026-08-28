using EveOPreview.Mediator.Messages;
using EveOPreview.View;
using System.Text.Json;

namespace EveOPreview.Services
{
    public interface IThumbnailManager
    {
        void Start();
        void Stop();

        void UpdateCycleGroupIndicator();
        void UpdateThumbnailsSize();
        void UpdateThumbnailFrames();
		void ApplyAllClientLayouts();
		void ApplyAllCoreAffinities();
		void UpdateClientLayouts();

		void RefreshHotkeys();

        void ProcessPipeMessage(string type, JsonElement payload);

		IThumbnailView GetClientByTitle(string title);
        IThumbnailView GetClientByPointer(System.IntPtr ptr);
        IThumbnailView GetActiveClient();
    }
}