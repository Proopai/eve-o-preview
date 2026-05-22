using System.Collections.Generic;

namespace EveOPreview.Services
{
	public interface IProcessMonitor
	{
		IProcessInfo GetMainProcess();
		ICollection<IProcessInfo> GetAllProcesses();
		void GetUpdatedProcesses(out ICollection<IProcessInfo> addedProcesses, out ICollection<IProcessInfo> updatedProcesses, out ICollection<IProcessInfo> removedProcesses);

		/// <summary>Force-terminates every running EVE Online game client process (exefile), regardless of preview settings.</summary>
		void CloseAllMonitoredClients();
	}
}