using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EveOPreview.Configuration
{
	public static class HotkeyFormatting
	{
		private static readonly KeysConverter KeysConverter = new KeysConverter();

		public static string ToDisplayString(Keys keys)
		{
			if (keys == Keys.None)
			{
				return string.Empty;
			}

			return KeysConverter.ConvertToInvariantString(keys) ?? string.Empty;
		}

		public static Keys FromDisplayString(string hotkey)
		{
			if (string.IsNullOrWhiteSpace(hotkey))
			{
				return Keys.None;
			}

			object rawValue = KeysConverter.ConvertFromInvariantString(hotkey.Trim());
			return rawValue is Keys keys ? keys : Keys.None;
		}

		public static string GetPrimaryHotkey(IList<string> hotkeys)
		{
			if (hotkeys == null)
			{
				return string.Empty;
			}

			return hotkeys.FirstOrDefault(h => !string.IsNullOrWhiteSpace(h)) ?? string.Empty;
		}

		public static void SetPrimaryHotkey(List<string> hotkeys, string value)
		{
			if (hotkeys == null)
			{
				return;
			}

			hotkeys.Clear();
			if (!string.IsNullOrWhiteSpace(value))
			{
				hotkeys.Add(value.Trim());
			}
		}
	}
}
