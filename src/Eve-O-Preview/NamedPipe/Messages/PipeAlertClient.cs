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

		public const int AlertTypeIntel = 1;
		public const int AlertTypeKill = 2;
		public const int AlertTypeDecloak = 3;
		public const int AlertTypeFaction = 4;
		public const int AlertTypeMiningOver = 5;

		public int AlertJumps { get; set; }
		public string AlertText { get; set; }
	}
}
