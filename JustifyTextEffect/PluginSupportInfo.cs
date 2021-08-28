using System;
using System.Reflection;
using PaintDotNet;

namespace JustifyTextEffect
{
	public class PluginSupportInfo : IPluginSupportInfo
	{
		public string Author => GetType().Assembly.GetCustomAttribute<AssemblyCopyrightAttribute>().Copyright;

		public string Copyright => GetType().Assembly.GetCustomAttribute<AssemblyDescriptionAttribute>().Description;

		public string DisplayName => GetType().Assembly.GetCustomAttribute<AssemblyProductAttribute>().Product;

		public Version Version => GetType().Assembly.GetName().Version;

		public Uri WebsiteUri => new Uri("https://forums.getpaint.net");
	}
}
