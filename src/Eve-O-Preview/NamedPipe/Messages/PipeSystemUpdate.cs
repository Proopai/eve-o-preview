using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveOPreview.NamedPipe.Messages
{
	public class PipeSystemUpdate
	{
		public const string MessageType = "ClientSystemUpdate";
		public string Client { get; set; }
		public string SystemName { get; set; }
	}
}
