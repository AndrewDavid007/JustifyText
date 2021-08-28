using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace JustifyTextEffect.Properties
{
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		private static ResourceManager resourceMan;

		private static CultureInfo resourceCulture;

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (resourceMan == null)
				{
					ResourceManager resourceManager = (resourceMan = new ResourceManager("JustifyTextEffect.Properties.Resources", typeof(Resources).Assembly));
				}
				return resourceMan;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return resourceCulture;
			}
			set
			{
				resourceCulture = value;
			}
		}

		internal static Bitmap CenterButtonIcon
		{
			get
			{
				object @object = ResourceManager.GetObject("CenterButtonIcon", resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal static Bitmap JustifyButtonIcon
		{
			get
			{
				object @object = ResourceManager.GetObject("JustifyButtonIcon", resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal static Bitmap LeftButtonIcon
		{
			get
			{
				object @object = ResourceManager.GetObject("LeftButtonIcon", resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal static Bitmap RightButtonIcon
		{
			get
			{
				object @object = ResourceManager.GetObject("RightButtonIcon", resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal Resources()
		{
		}
	}
}
