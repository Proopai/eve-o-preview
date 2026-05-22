using System.Collections.Generic;

namespace EveOPreview.Configuration
{
	public sealed class GlobalShortcutSettings
	{
		public string CycleGroup1Forward { get; set; } = string.Empty;
		public string CycleGroup1Backward { get; set; } = string.Empty;
		public string CycleGroup2Forward { get; set; } = string.Empty;
		public string CycleGroup2Backward { get; set; } = string.Empty;
		public string CycleGroup3Forward { get; set; } = string.Empty;
		public string CycleGroup3Backward { get; set; } = string.Empty;
		public string CycleGroup4Forward { get; set; } = string.Empty;
		public string CycleGroup4Backward { get; set; } = string.Empty;
		public string CycleGroup5Forward { get; set; } = string.Empty;
		public string CycleGroup5Backward { get; set; } = string.Empty;
		public string DynamicCycleForward { get; set; } = string.Empty;
		public string DynamicCycleBackward { get; set; } = string.Empty;
		public string MinimizeAllClients { get; set; } = string.Empty;
	}
}
