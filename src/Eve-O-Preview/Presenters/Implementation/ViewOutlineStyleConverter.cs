using EveOPreview.Configuration;
using EveOPreview.View;

namespace EveOPreview.Presenters.Implementation
{
	static class ViewOutlineStyleConverter
	{
		public static OutlineStyle Convert(ViewOutlineStyle value)
		{
			// Cheat based on fact that the order and byte values of both enums are the same
			return (OutlineStyle)((int)value);
		}

		public static ViewOutlineStyle Convert(OutlineStyle value)
		{
			// Cheat based on fact that the order and byte values of both enums are the same
			return (ViewOutlineStyle)((int)value);
		}
	}
}
