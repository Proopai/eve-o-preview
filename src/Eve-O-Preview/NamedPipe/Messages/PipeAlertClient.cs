using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveOPreview.NamedPipe.Messages
{
	public class PipeAlertClient
	{
		public const string MessageType = "AlertClient";
		public string Client { get; set; }
		public string SystemName { get; set; }
		public int AlertType { get; set; }

		public const int AlertTypeIntel = 0;
		public const int AlertTypeKill = 1;
		public const int AlertTypeDecloak = 2;
		public const int AlertTypeFaction = 3;
		public const int AlertTypeMiningOver = 4;

		public int AlertJumps { get; set; }
		public string AlertText { get; set; }
	}
}
