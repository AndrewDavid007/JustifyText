using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace ControlExtensions
{
	public class ControlFontComboBox : ComboBox
	{
		internal static class FontUtil
		{
			internal static readonly FontFamily[] UsableFontFamilies;

			internal static int FindFontIndex(string familyName)
			{
				for (int i = 0; i < UsableFontFamilies.Length; i++)
				{
					if (UsableFontFamilies[i].Name == familyName)
					{
						return i;
					}
				}
				return 0;
			}

			static FontUtil()
			{
				List<FontFamily> list = new List<FontFamily>();
				using (InstalledFontCollection installedFontCollection = new InstalledFontCollection())
				{
					FontFamily[] families = installedFontCollection.Families;
					foreach (FontFamily fontFamily in families)
					{
						if (fontFamily.IsStyleAvailable(FontStyle.Regular))
						{
							list.Add(fontFamily);
						}
					}
				}
				UsableFontFamilies = list.ToArray();
			}
		}

		private int _itemHeight;

		private int _previewFontSize;

		public string FontName;

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new DrawMode DrawMode
		{
			get
			{
				return base.DrawMode;
			}
			set
			{
				base.DrawMode = value;
			}
		}

		[DefaultValue(12)]
		[Category("Appearance")]
		public int PreviewFontSize
		{
			get
			{
				return _previewFontSize;
			}
			set
			{
				_previewFontSize = value;
				OnPreviewFontSizeChanged(EventArgs.Empty);
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool Sorted
		{
			get
			{
				return base.Sorted;
			}
			set
			{
				base.Sorted = value;
			}
		}

		public event EventHandler PreviewFontSizeChanged;

		public ControlFontComboBox()
		{
			DrawMode = DrawMode.Normal;
			Sorted = true;
		}

		private void CalculateLayout()
		{
			using (Font font = new Font(Font.FontFamily, PreviewFontSize))
			{
				_itemHeight = TextRenderer.MeasureText("yY", font).Height + 2;
			}
		}

		public virtual void LoadFontFamilies()
		{
			FontFamily[] usableFontFamilies = FontUtil.UsableFontFamilies;
			foreach (FontFamily fontFamily in usableFontFamilies)
			{
				base.Items.Add(fontFamily.Name);
			}
		}

		protected virtual void OnPreviewFontSizeChanged(EventArgs e)
		{
			this.PreviewFontSizeChanged?.Invoke(this, e);
			CalculateLayout();
		}
	}
}
