using System.Windows.Forms;

namespace EveOPreview.View
{
    public interface IThumbnailDescription
    {
        string Title { get; set; }
        bool IsDisabled { get; set; }

        // Current per-client switch hotkey, surfaced so the Hotkeys tab can show it (issue #13).
        Keys ClientHotkey { get; set; }
    }
}