using System.Runtime.InteropServices;

namespace EveOPreview.Services.Interop
{
	[StructLayout(LayoutKind.Sequential)]
	struct POINT
	{
		public int X;
		public int Y;

		public POINT(int x, int y)
		{
			this.X = x;
			this.Y = y;
		}
	}
}
