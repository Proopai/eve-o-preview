using System.Collections.Generic;

namespace EveOPreview.Configuration
{
    // A single named cycle group: its switch hotkeys plus the ordered set of client titles that
    // belong to it (ClientsOrder maps a client title to its 1-based position in the cycle).
    public sealed class CycleGroupConfiguration
    {
        public CycleGroupConfiguration()
        {
            this.Name = "";
            this.ForwardHotkeys = new List<string>();
            this.BackwardHotkeys = new List<string>();
            this.ClientsOrder = new Dictionary<string, int>();
        }

        public string Name { get; set; }
        public List<string> ForwardHotkeys { get; set; }
        public List<string> BackwardHotkeys { get; set; }
        public Dictionary<string, int> ClientsOrder { get; set; }
    }
}