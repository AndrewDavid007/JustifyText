using System;
using System.Drawing;
using System.Drawing.Text;
using OptionBased.Effects;
using OptionControls;
using PaintDotNet;
using PaintDotNet.Effects;

namespace JustifyTextEffect
{
	[PluginSupportInfo(typeof(PluginSupportInfo), DisplayName = "Justify Text")]
	public class JustifyTextEffectPlugin : OptionBasedEffect
	{
		private enum OptionNames
		{
			MainPanel,
			TextBox,
			FontNameAndStyle,
			FontSize,
			TextAlign,
			TextOptions,
			ParagraphIndent,
			ParagraphSpacing,
			LineSpacing,
			TextColor,
			VectorPan,
			SpacingPanel,
			RightEdge,
			SeparatePanel,
			ShowBoundary
		}

		private enum TextAlignmentEnum
		{
			Left,
			Center,
			Right,
			Justify
		}

		private static readonly Image StaticIcon = new Bitmap(typeof(JustifyTextEffectPlugin), "JustifyText.png");

		private string Amount1 = "";

		private FontFamily Amount2 = new FontFamily("Arial");

		private FontStyle Amount3 = FontStyle.Regular;

		private double Amount4 = 35.0;

		private double Amount5 = 0.0;

		private double Amount6 = 0.0;

		private byte Amount7 = 0;

		private double Amount8 = 0.0;

		private double Amount9 = 0.0;

		private double Amount10 = 0.0;

		private Color Amount11 = Color.Black;

		private double Amount12 = 0.0;

		private bool Amount13 = false;

		public JustifyTextEffectPlugin()
			: base(typeof(JustifyTextEffectPlugin), StaticIcon, EffectFlags.Configurable)
		{
		}

		protected override ConfigurationOfUI OnCustomizeUI()
		{
			return new ConfigurationOfUI
			{
				PropertyBasedLook = false
			};
		}

		protected override ConfigurationOfDialog OnCustomizeDialog()
		{
			return new ConfigurationOfDialog
			{
				IsSizable = true,
				WidthScale = 1.0,
				OptionFocusedOnActivation = OptionNames.FontNameAndStyle
			};
		}

		protected override OptionControlList OnSetupOptions(OptionContext optContext)
		{
			string defaultValue = "Enter you text here";
			byte defaultTextAlign = 3;
			return new OptionControlList
			{
				new OptionPanelPage(OptionNames.MainPanel, optContext)
				{
					new OptionStringEditBox(OptionNames.TextBox, optContext, defaultValue)
					{
						DisplayName = "",
						Rows = 5,
						ShowResetButton = false
					},
					new OptionTextAlignButtons(OptionNames.TextAlign, optContext, defaultTextAlign)
					{
						DisplayName = "",
						Label = "Text Align",
						ShowResetButton = true
					},
					new OptionFontNameAndStyle(OptionNames.FontNameAndStyle, optContext, "Arial", FontStyle.Regular)
					{
						DisplayName = "",
						ShowResetButton = true,
						Label = "",
						Visible = true
					},
					new OptionDoubleSlider(OptionNames.FontSize, optContext, 12.0, 5.0, 800.0)
					{
						DisplayName = "",
						Label = "Font Size",
						UpDownIncrement = 0.1,
						SliderScaleExponential = false,
						SliderScaleZoom = false,
						DecimalPlaces = 1,
						SliderShowTickMarks = false,
						SliderSmallChange = 0.1,
						SliderLargeChange = 10.0,
						ShowResetButton = true
					},
					new OptionDoubleSlider(OptionNames.ParagraphIndent, optContext, 0.0, 0.0, 0.5)
					{
						DisplayName = "",
						Label = "§ Indent",
						UpDownIncrement = 0.001,
						SliderScaleExponential = false,
						SliderScaleZoom = false,
						DecimalPlaces = 3,
						SliderShowTickMarks = false,
						SliderSmallChange = 0.001,
						SliderLargeChange = 0.01,
						ShowResetButton = true,
						ToolTipText = "Paragraph Indent"
					},
					new OptionPanelBox(OptionNames.SpacingPanel, optContext)
					{
						DisplayName = "Spacing"
					},
					new OptionDoubleSlider(OptionNames.LineSpacing, optContext, 1.0, 0.5, 5.0)
					{
						DisplayName = "",
						Label = "Line",
						UpDownIncrement = 0.01,
						SliderScaleExponential = false,
						SliderScaleZoom = false,
						DecimalPlaces = 2,
						SliderShowTickMarks = false,
						SliderSmallChange = 0.01,
						SliderLargeChange = 0.1,
						ShowResetButton = true
					},
					new OptionDoubleSlider(OptionNames.ParagraphSpacing, optContext, 0.5, 0.0, 10.0)
					{
						DisplayName = "",
						Label = "Paragraph",
						UpDownIncrement = 0.01,
						SliderScaleExponential = false,
						SliderScaleZoom = false,
						DecimalPlaces = 2,
						SliderShowTickMarks = false,
						SliderSmallChange = 0.01,
						SliderLargeChange = 0.1,
						ShowResetButton = true
					},
					new OptionColorsBoxAlpha(OptionNames.TextColor, optContext, Color.Teal, 255)
					{
						DisplayName = "Text Color",
						ShowResetButton = true
					},
					new OptionDoubleVectorPan(OptionNames.VectorPan, optContext, -0.85, -1.0, 1.0, -0.85, -1.0, 1.0)
					{
						DisplayName = "Text Location",
						StaticBitmapUnderlay = base.EnvironmentParameters.SourceSurface.CreateAliasedBitmap(),
						Indent = 0,
						UpDownIncrementX = 0.01,
						UpDownIncrementY = 0.01,
						ShowLineToCenter = true,
						ViewOffset = 0.0,
						ViewFactor = 1.0,
						DecimalPlaces = 2,
						SliderShowTickMarksX = false,
						SliderShowTickMarksY = false,
						ShowResetButton = false,
						SliderShowTickMarks = false,
						SliderSmallChangeX = 0.1,
						SliderSmallChangeY = 0.1,
						SliderLargeChangeX = 0.25,
						SliderLargeChangeY = 0.25,
						GadgetScale = 1.09,
						Visible = true,
						UpDownIncrement = 0.01
					},
					new OptionDoubleSlider(OptionNames.RightEdge, optContext, 0.85, 0.0, 1.0)
					{
						DisplayName = "",
						Label = "Right Edge",
						UpDownIncrement = 0.001,
						SliderScaleExponential = false,
						SliderScaleZoom = false,
						DecimalPlaces = 3,
						SliderShowTickMarks = false,
						SliderSmallChange = 0.001,
						SliderLargeChange = 0.01,
						ShowResetButton = true
					},
					new OptionBooleanCheckBox(OptionNames.ShowBoundary, optContext, defaultValue: true)
					{
						DisplayName = "",
						Label = "",
						Text = "Show Boundary",
						ShowResetButton = false
					}
				}
			};
		}

		protected override void OnAdaptOptions()
		{
			Option(OptionNames.TextAlign).ValueChanged += TextAlign_ValueChanged;
		}

		private void TextAlign_ValueChanged(object sender, EventArgs e)
		{
			ParagraphIndent_Rule();
		}

		private void ParagraphIndent_Rule()
		{
			Option(OptionNames.ParagraphIndent).ReadOnly = ((OptionTextAlignButtons)Option(OptionNames.TextAlign)).Value == 1 || ((OptionTextAlignButtons)Option(OptionNames.TextAlign)).Value == 2;
		}

		protected override void OnSetRenderInfo(OptionBasedEffectConfigToken newToken, RenderArgs dstArgs, RenderArgs srcArgs)
		{
			Amount1 = OptionStringEditBox.GetOptionValue(OptionNames.TextBox, newToken.Items);
			Amount2 = OptionFontNameAndStyle.GetOptionValueFontName(OptionNames.FontNameAndStyle, newToken.Items);
			Amount3 = OptionFontNameAndStyle.GetOptionValueFontStyle(OptionNames.FontNameAndStyle, newToken.Items);
			Amount4 = OptionTypeSlider<double>.GetOptionValue(OptionNames.FontSize, newToken.Items);
			Amount5 = OptionDoubleVectorPan.GetOptionValueX(OptionNames.VectorPan, newToken.Items);
			Amount6 = OptionDoubleVectorPan.GetOptionValueY(OptionNames.VectorPan, newToken.Items);
			Amount8 = OptionTypeSlider<double>.GetOptionValue(OptionNames.ParagraphIndent, newToken.Items);
			Amount9 = OptionTypeSlider<double>.GetOptionValue(OptionNames.LineSpacing, newToken.Items);
			Amount10 = OptionTypeSlider<double>.GetOptionValue(OptionNames.ParagraphSpacing, newToken.Items);
			Amount11 = OptionColorsBoxAlpha.GetOptionValue(OptionNames.TextColor, newToken.Items);
			Amount12 = OptionTypeSlider<double>.GetOptionValue(OptionNames.RightEdge, newToken.Items);
			Amount13 = OptionBooleanCheckBox.GetOptionValue(OptionNames.ShowBoundary, newToken.Items);
			switch (OptionTextAlignButtons.GetOptionValue(OptionNames.TextAlign, newToken.Items))
			{
			case 0:
				Amount7 = 0;
				break;
			case 1:
				Amount7 = 1;
				break;
			case 2:
				Amount7 = 2;
				break;
			case 3:
				Amount7 = 3;
				break;
			}
			base.OnSetRenderInfo(newToken, dstArgs, srcArgs);
		}

		protected override void OnRender(Rectangle[] rois, int startIndex, int length)
		{
			if (length != 0)
			{
				for (int i = startIndex; i < startIndex + length; i++)
				{
					Render(base.DstArgs.Surface, base.SrcArgs.Surface, rois[i]);
				}
			}
		}

		private void Render(Surface dst, Surface src, Rectangle rect)
		{
			Rectangle boundsInt = EnvironmentParameters.GetSelectionAsPdnRegion().GetBoundsInt();
			dst.CopySurface(src, rect.Location, rect);
			float indent = (float)Amount8;
			float line_spacing = (float)Amount9;
			float paragraph_spacing = (float)Amount10;
			using (RenderArgs renderArgs = new RenderArgs(dst))
			{
				string amount = Amount1;
				Graphics graphics = renderArgs.Graphics;
				graphics.Clip = new Region(rect);
				graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
				float num = (float)Math.Round((Amount5 + 1.0) / 2.0 * (double)(boundsInt.Right - boundsInt.Left));
				float num2 = (float)Math.Round((Amount6 + 1.0) / 2.0 * (double)(boundsInt.Bottom - boundsInt.Top));
				RectangleF rect2 = new RectangleF((float)boundsInt.Left + num, (float)boundsInt.Top + num2, (float)boundsInt.Left + (float)boundsInt.Right * (float)Amount12, (float)boundsInt.Height - num2);
				TextAlignmentEnum textAlignmentEnum = TextAlignmentEnum.Left;
				if (Amount7 == 1)
				{
					textAlignmentEnum = TextAlignmentEnum.Center;
				}
				else if (Amount7 == 2)
				{
					textAlignmentEnum = TextAlignmentEnum.Right;
				}
				else if (Amount7 == 3)
				{
					textAlignmentEnum = TextAlignmentEnum.Justify;
				}
				using (Font font = new Font(Amount2, (float)Amount4, Amount3))
				{
					using (SolidBrush brush = new SolidBrush(Amount11))
					{
						if (textAlignmentEnum == TextAlignmentEnum.Right || textAlignmentEnum == TextAlignmentEnum.Center)
						{
							rect2 = DrawParagraphs(graphics, rect2, font, brush, amount, textAlignmentEnum, line_spacing, 0f, paragraph_spacing);
						}
						else
						{
							rect2 = DrawParagraphs(graphics, rect2, font, brush, amount, textAlignmentEnum, line_spacing, indent, paragraph_spacing);
						}
					}
				}
				rect2 = new RectangleF((float)boundsInt.Left + num, (float)boundsInt.Top + num2, (float)boundsInt.Left + (float)boundsInt.Right * (float)Amount12, (float)boundsInt.Height - num2);
				if (Amount13)
				{
					graphics.DrawRectangle(Pens.Black, Rectangle.Round(rect2));
				}
			}
		}

		private RectangleF DrawParagraphs(Graphics g, RectangleF rect, Font font, Brush brush, string text, TextAlignmentEnum justification, float line_spacing, float indent, float paragraph_spacing)
		{
			string[] array = text.Split('\n');
			string[] array2 = array;
			foreach (string text2 in array2)
			{
				rect = DrawParagraph(g, rect, font, brush, text2, justification, line_spacing, indent, paragraph_spacing);
				if (rect.Height < font.Size)
				{
					break;
				}
			}
			return rect;
		}

		private RectangleF DrawParagraph(Graphics g, RectangleF rect, Font font, Brush brush, string text, TextAlignmentEnum justification, float line_spacing, float indent, float extra_paragraph_spacing)
		{
			float num = rect.Top;
			string[] array = text.Split(' ');
			int num2 = 0;
			while (true)
			{
				string text2 = array[num2];
				int i;
				for (i = num2 + 1; i < array.Length; i++)
				{
					string text3 = text2 + " " + array[i];
					if (g.MeasureString(text3, font).Width + rect.Width * indent > rect.Width)
					{
						i--;
						break;
					}
					text2 = text3;
				}
				if (i == array.Length && justification == TextAlignmentEnum.Justify)
				{
					DrawLine(g, text2, font, brush, rect.Left + rect.Width * indent, num, rect.Width - rect.Width * indent, TextAlignmentEnum.Left);
				}
				else
				{
					DrawLine(g, text2, font, brush, rect.Left + rect.Width * indent, num, rect.Width - rect.Width * indent, justification);
				}
				num += (float)font.Height * line_spacing;
				if (font.Size > rect.Height)
				{
					break;
				}
				num2 = i + 1;
				if (num2 >= array.Length)
				{
					break;
				}
				indent = 0f;
			}
			num += (float)font.Height * extra_paragraph_spacing;
			float num3 = rect.Bottom - num;
			if (num3 < 0f)
			{
				num3 = 0f;
			}
			return new RectangleF(rect.X, num, rect.Width, num3);
		}

		private void DrawLine(Graphics g, string line, Font font, Brush brush, float x, float y, float width, TextAlignmentEnum justification)
		{
			RectangleF rectangleF = new RectangleF(x, y, width, font.Height);
			if (justification == TextAlignmentEnum.Justify)
			{
				DrawJustifiedLine(g, rectangleF, font, brush, line);
				return;
			}
			using (StringFormat stringFormat = new StringFormat())
			{
				switch (justification)
				{
				case TextAlignmentEnum.Left:
					stringFormat.Alignment = StringAlignment.Near;
					break;
				case TextAlignmentEnum.Right:
					stringFormat.Alignment = StringAlignment.Far;
					break;
				case TextAlignmentEnum.Center:
					stringFormat.Alignment = StringAlignment.Center;
					break;
				}
				g.DrawString(line, font, brush, rectangleF, stringFormat);
			}
		}

		private void DrawJustifiedLine(Graphics g, RectangleF rect, Font font, Brush brush, string text)
		{
			string[] array = text.Split(' ');
			float[] array2 = new float[array.Length];
			float num = 0f;
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = g.MeasureString(array[i], font).Width;
				num += array2[i];
			}
			float num2 = rect.Width - num;
			int num3 = array.Length - 1;
			if (array.Length > 1)
			{
				num2 /= (float)num3;
			}
			float num4 = rect.Left;
			float top = rect.Top;
			for (int j = 0; j < array.Length; j++)
			{
				g.DrawString(array[j], font, brush, num4, top);
				num4 += array2[j] + num2;
			}
		}
	}
}
