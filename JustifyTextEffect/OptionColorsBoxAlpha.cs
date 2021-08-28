using System;
using System.Drawing;
using System.Windows.Forms;
using ControlExtensions;
using OptionBased.Properties;
using OptionControls;
using ToolStripExtensions;

namespace JustifyTextEffect
{
	internal class OptionColorsBoxAlpha : OptionControl
	{
		private readonly ControlColorRectangle _colorRectangle;

		private readonly ControlToolButton _paletteButton;

		private readonly ControlColorGradient _alphaSlider;

		private readonly ControlNumericUpDown _alphaNud;

		private int _changingStack;

		private readonly Button _resetColorButton;

		private readonly Button _resetAlphaButton;

		private Color _valueColor;

		private Color _defaultColorValue;

		private Color _otherColorValue;

		private int _valueAlpha;

		private int _defaultAlphaValue;

		private int _inOnPropertyValueChanged;

		public Color ValueColor
		{
			get
			{
				return _valueColor;
			}
			set
			{
				if (_valueColor.ToArgb() != value.ToArgb())
				{
					_valueColor = Color.FromArgb(value.ToArgb());
					OnValueChanged();
				}
			}
		}

		public OptionColorsBoxAlpha(Enum optId, OptionContext optContext, Color defaultColorValue, int defaultAlphaValue)
			: base(optId, optContext)
		{
			_defaultColorValue = defaultColorValue;
			_otherColorValue = defaultColorValue;
			_defaultAlphaValue = defaultAlphaValue;
			_valueAlpha = defaultAlphaValue;
			SuspendLayout();
			_resetColorButton = CreateResetButton(ResetColorButtonClick);
			base.ToolTip.SetToolTip(_resetColorButton, Ui.MapKeyToText("Reset color").Replace("&", ""));
			_resetAlphaButton = CreateResetButton(ResetAlphaButtonClick);
			base.ToolTip.SetToolTip(_resetAlphaButton, Ui.MapKeyToText("Reset alpha").Replace("&", ""));
			ControlColorRectangle controlColorRectangle = (_colorRectangle = new ControlColorRectangle
			{
				Name = "colorRectangle",
				TabStop = false
			});
			base.ToolTip.SetToolTip(_colorRectangle, Ui.MapKeyToText("Color").Replace("&", ""));
			_paletteButton = new ControlToolButton(Resources.Palette_16x16, Ui.MapKeyToText("Palette").Replace("&", ""));
			_paletteButton.Click += PaletteButton_Click;
			ControlColorGradient controlColorGradient = (_alphaSlider = new ControlColorGradient
			{
				Name = "alphaSlider",
				Orientation = Orientation.Horizontal,
				DrawNearNub = false,
				DrawFarNub = true
			});
			_alphaSlider.ValueChanged += AlphaSliderValueChanged;
			_alphaSlider.TabStop = false;
			base.ToolTip.SetToolTip(_alphaSlider, Ui.MapKeyToText("Alpha").Replace("&", ""));
			ControlNumericUpDown controlNumericUpDown = (_alphaNud = new ControlNumericUpDown
			{
				DecimalPlaces = 0,
				Name = "alphaNum",
				TextAlign = HorizontalAlignment.Right,
				Minimum = 0m,
				Maximum = 255m
			});
			_alphaNud.ValueChanged += AlphaNudValueChanged;
			base.ToolTip.SetToolTip(_alphaNud, Ui.MapKeyToText("Alpha value").Replace("&", ""));
			base.Controls.AddRange(new Control[9] { _displayNameControl, _labelControl, _colorRectangle, _paletteButton, _resetColorButton, _alphaSlider, _alphaNud, _resetAlphaButton, _descriptionControl });
			ValueColor = _defaultColorValue;
			OnReset();
			ResumeLayout(performLayout: false);
			PerformLayout();
		}

		protected override void OnLayout(LayoutEventArgs e)
		{
			int num = LayoutBegin();
			Rectangle bounds;
			int num2 = LayoutLabel(num, out bounds);
			_labelControl.Bounds = bounds;
			int num3 = LayoutResetButton(_resetAlphaButton, num);
			Size size = new Size(SystemInformation.VerticalScrollBarWidth + 2, SystemInformation.VerticalScrollBarWidth + 2);
			_colorRectangle.Bounds = new Rectangle(LayoutIndent() + (_labelControl.Visible ? num2 : 0), num, size.Width, size.Height);
			_paletteButton.Bounds = new Rectangle(Ui.baseButtonWidth, num, size.Width, size.Height);
			_paletteButton.Location = new Point(_colorRectangle.Right + Ui.hMargin, num - 1);
			_resetColorButton.Bounds = new Rectangle(Ui.baseButtonWidth, num, size.Width, size.Height);
			_resetColorButton.Location = new Point(_paletteButton.Right + Ui.hMargin, num);
			_alphaSlider.Location = new Point(_resetColorButton.Right + Ui.hMargin, num);
			_alphaSlider.Width = num3 - num2 - _alphaNud.Width - _resetAlphaButton.Width - _resetColorButton.Width - _paletteButton.Width - _colorRectangle.Width + Ui.hMargin;
			_alphaSlider.Height = SystemInformation.VerticalScrollBarWidth;
			_alphaNud.Bounds = new Rectangle(_alphaSlider.Right, num, Ui.baseNudWidth, size.Height);
			_alphaNud.Location = new Point(_alphaSlider.Right + Ui.hMargin, num);
			_alphaNud.Size = Ui.ScaleSize(new Size(Ui.baseNudWidth / 2 + size.Width / 2, size.Height));
			_resetAlphaButton.Bounds = new Rectangle(Ui.baseButtonWidth, num, size.Height, size.Height);
			_resetAlphaButton.Location = new Point(_alphaNud.Right + Ui.hMargin, num);
			_resetAlphaButton.Height = _alphaNud.Height;
			_resetAlphaButton.Width = SystemInformation.VerticalScrollBarWidth + 4;
			LayoutEnd(_labelControl.Bottom, _colorRectangle.Bottom, _paletteButton.Bottom, _resetColorButton.Bottom, _alphaSlider.Bottom, _alphaNud.Bottom, _resetAlphaButton.Bottom);
			base.OnLayout(e);
		}

		protected override void OnReset()
		{
			ValueColor = _defaultColorValue;
			_alphaSlider.Value = _defaultAlphaValue;
		}

		protected void ResetColorButtonClick(object sender, EventArgs e)
		{
			ValueColor = Color.FromArgb(ValueColor.A, _defaultColorValue);
		}

		protected void ResetAlphaButtonClick(object sender, EventArgs e)
		{
			_alphaSlider.Value = _defaultAlphaValue;
		}

		protected override void OnValueChanged()
		{
			_inOnPropertyValueChanged++;
			try
			{
				int num = ValueColor.ToArgb();
				int num2 = (num >> 24) & 0xFF;
				SetPropertyValueFromAlpha(num2);
				_colorRectangle.RectangleColor = Color.FromArgb(num2, ValueColor);
			}
			finally
			{
				_inOnPropertyValueChanged--;
			}
			base.OnValueChanged();
		}

		private void AlphaNudValueChanged(object sender, EventArgs e)
		{
			if (_changingStack == 0)
			{
				_changingStack++;
				SetPropertyValueFromAlpha((int)_alphaNud.Value);
				_changingStack--;
			}
		}

		private void AlphaSliderValueChanged(object sender, Ui.IndexEventArgs ce)
		{
			if (_changingStack == 0)
			{
				_changingStack++;
				SetPropertyValueFromAlpha(_alphaSlider.Value);
				_changingStack--;
			}
		}

		public static Color GetOptionValue(Enum optId, OptionDictionary values)
		{
			return Color.FromArgb((int)values[string.Concat(optId, ".Palette").ToString()]);
		}

		public override void OptionDefaultToValues(OptionDictionary values)
		{
			values[base.Id + ".Palette"] = _defaultColorValue.ToArgb();
		}

		public override void OptionToValues(OptionDictionary values)
		{
			values[base.Id + ".Palette"] = ValueColor.ToArgb();
		}

		public override void ValuesToOption(OptionDictionary values)
		{
			ValueColor = (values.TryGetValue(base.Id + ".Palette", out var value) ? Color.FromArgb((int)value) : _defaultColorValue);
		}

		private void PaletteButton_Click(object sender, EventArgs e)
		{
			ToolStripDropDownColorPicker toolStripDropDownColorPicker = new ToolStripDropDownColorPicker(null, ValueColor, _defaultColorValue, _otherColorValue);
			toolStripDropDownColorPicker.ColorClicked += PaletteButton_ColorClicked;
			toolStripDropDownColorPicker.Show(PointToScreen(new Point(_paletteButton.Right, _paletteButton.Top)));
		}

		private void PaletteButton_ColorClicked(object sender, ToolStripDropDownColorPicker.ColorPickerEventArgs e)
		{
			_otherColorValue = e.OtherColor;
			ValueColor = Color.FromArgb(ValueColor.A, e.Color);
		}

		private void SetPropertyValueFromAlpha(int alpha)
		{
			if (_alphaNud.Value != (decimal)alpha)
			{
				_alphaNud.Value = alpha;
			}
			if (_inOnPropertyValueChanged == 0 && ValueColor != Color.FromArgb(alpha, ValueColor))
			{
				ValueColor = Color.FromArgb(alpha, ValueColor);
			}
			_alphaSlider.Value = alpha;
			_alphaSlider.MinColor = Color.FromArgb(0, ValueColor);
			_alphaSlider.MaxColor = Color.FromArgb(255, ValueColor);
		}
	}
}
