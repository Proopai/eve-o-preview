using System;
using System.Drawing;

namespace EveOPreview.Services
{
	public interface IDwmThumbnail
	{
		void Register(IntPtr destination, IntPtr source);
		void Unregister();

		void Move(int left, int top, int right, int bottom);
		void SetSource(Rectangle source);
		void ClearSource();
		void Update();
	}
}
