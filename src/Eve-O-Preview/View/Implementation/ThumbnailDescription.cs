using System.Windows.Forms;

namespace EveOPreview.View
{
    sealed class ThumbnailDescription : IThumbnailDescription
    {
        public ThumbnailDescription(string title, bool isDisabled, Keys clientHotkey)
        {
            this.Title = title;
            this.IsDisabled = isDisabled;
            this.ClientHotkey = clientHotkey;
        }

        public string Title { get; set; }
        public bool IsDisabled { get; set; }
        public Keys ClientHotkey { get; set; }
    }
}