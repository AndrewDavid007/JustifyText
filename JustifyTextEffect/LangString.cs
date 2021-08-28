using System.Globalization;

namespace JustifyTextEffect
{
	internal static class LangString
	{
		private static readonly string UICulture = CultureInfo.CurrentUICulture.Name;

		internal static string DefaultString
		{
			get
			{
				string uICulture = UICulture;
				if (uICulture == "ro")
				{
					return "TASTEAZĂ\r\nTEXTUL";
				}
				return "TYPE\r\nYOUR\r\nTEXT";
			}
		}
	}
}
