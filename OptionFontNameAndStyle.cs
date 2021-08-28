using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using ControlExtensions;
using OptionControls;

internal class OptionFontNameAndStyle : OptionControl
{
	private static class FontUtil
	{
		internal static readonly string[] UsableFontFamilies;

		internal static int FindFontIndex(string familyName)
		{
			for (int i = 0; i < UsableFontFamilies.Length; i++)
			{
				if (UsableFontFamilies[i].Equals(familyName, StringComparison.OrdinalIgnoreCase))
				{
					return i;
				}
			}
			return 0;
		}

		static FontUtil()
		{
			List<string> list = new List<string>();
			using (InstalledFontCollection installedFontCollection = new InstalledFontCollection())
			{
				FontFamily[] families = installedFontCollection.Families;
				foreach (FontFamily fontFamily in families)
				{
					if (fontFamily.IsStyleAvailable(FontStyle.Regular))
					{
						list.Add(fontFamily.Name);
					}
				}
			}
			UsableFontFamilies = list.ToArray();
		}
	}

	private readonly Button _resetButton;

	private readonly ControlFontComboBox _fontNameComboBox;

	private string _fontName;

	private string _defaultFontName;

	private int defaultIndex;

	private readonly CheckBox _boldButton;

	private readonly CheckBox _italicButton;

	private readonly CheckBox _underlineButton;

	private readonly CheckBox _strikeoutButton;

	private FontStyle _fontStyle;

	private FontStyle _defaultFontStyle;

	public FontStyle ValueFontStyle
	{
		get
		{
			return _fontStyle;
		}
		set
		{
			_boldButton.Checked = (value & FontStyle.Bold) != 0;
			_italicButton.Checked = (value & FontStyle.Italic) != 0;
			_underlineButton.Checked = (value & FontStyle.Underline) != 0;
			_strikeoutButton.Checked = (value & FontStyle.Strikeout) != 0;
			if (value != _fontStyle)
			{
				_fontStyle = value;
				OnValueChanged();
			}
		}
	}

	public OptionFontNameAndStyle(Enum optId, OptionContext optContext)
		: this(optId, optContext, "Arial", FontStyle.Regular)
	{
	}

	public OptionFontNameAndStyle(Enum optId, OptionContext optContext, string defaultFontName, FontStyle defaultFontStyle)
		: base(optId, optContext)
	{
		_defaultFontName = defaultFontName;
		_defaultFontStyle = defaultFontStyle;
		SuspendLayout();
		_resetButton = CreateResetButton();
		string familyName = "Georgia";
		base.AutoScaleMode = AutoScaleMode.Dpi;
		Font = SystemFonts.MenuFont;
		ControlFontComboBox fontNameComboBox = new ControlFontComboBox
		{
			Name = "FontNameComboBox",
			FlatStyle = FlatStyle.System,
			DropDownStyle = ComboBoxStyle.DropDownList
		};
		defaultIndex = FontUtil.FindFontIndex(defaultFontName);
		_fontNameComboBox = fontNameComboBox;
		_fontNameComboBox.LoadFontFamilies();
		_fontNameComboBox.SelectedIndexChanged += FontNameComboBox_SelectedIndexChanged;
		CheckBox checkBox = (_boldButton = new CheckBox
		{
			Name = "BoldButton",
			FlatStyle = FlatStyle.System,
			Appearance = Appearance.Button,
			Text = "B",
			TextAlign = ContentAlignment.MiddleCenter
		});
		_boldButton.Font = new Font(familyName, _boldButton.Font.Size, FontStyle.Bold);
		_boldButton.CheckedChanged += FontStyleButton_CheckedChanged;
		base.ToolTip.SetToolTip(_boldButton, optContext.MapKeyToText("FontStyle.Bold"));
		CheckBox checkBox2 = (_italicButton = new CheckBox
		{
			Name = "italicButton",
			FlatStyle = FlatStyle.System,
			Appearance = Appearance.Button,
			Text = " I ",
			TextAlign = ContentAlignment.MiddleCenter
		});
		_italicButton.Font = new Font(familyName, _italicButton.Font.Size, FontStyle.Italic);
		_italicButton.CheckedChanged += FontStyleButton_CheckedChanged;
		base.ToolTip.SetToolTip(_italicButton, optContext.MapKeyToText("FontStyle.Italic"));
		CheckBox checkBox3 = (_underlineButton = new CheckBox
		{
			Name = "UnderlineButton",
			FlatStyle = FlatStyle.System,
			Appearance = Appearance.Button,
			Text = "U",
			TextAlign = ContentAlignment.MiddleCenter
		});
		_underlineButton.Font = new Font(familyName, _underlineButton.Font.Size, FontStyle.Underline);
		_underlineButton.CheckedChanged += FontStyleButton_CheckedChanged;
		base.ToolTip.SetToolTip(_underlineButton, optContext.MapKeyToText("FontStyle.Underline"));
		CheckBox checkBox4 = (_strikeoutButton = new CheckBox
		{
			Name = "StrikeoutButton",
			FlatStyle = FlatStyle.System,
			Appearance = Appearance.Button,
			Text = " S ",
			TextAlign = ContentAlignment.MiddleCenter
		});
		_strikeoutButton.Font = new Font(familyName, _strikeoutButton.Font.Size, FontStyle.Strikeout);
		_strikeoutButton.CheckedChanged += FontStyleButton_CheckedChanged;
		base.ToolTip.SetToolTip(_strikeoutButton, optContext.MapKeyToText("FontStyle.Strikeout"));
		base.Controls.AddRange(new Control[9] { _displayNameControl, _labelControl, _fontNameComboBox, _boldButton, _italicButton, _underlineButton, _strikeoutButton, _resetButton, _descriptionControl });
		OnReset();
		ResumeLayout(performLayout: false);
		PerformLayout();
	}

	private void FontNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
	{
		int selectedIndex = _fontNameComboBox.SelectedIndex;
		if (selectedIndex >= 0 && selectedIndex < _fontNameComboBox.Items.Count)
		{
			_fontName = (string)_fontNameComboBox.Items[selectedIndex];
		}
		else
		{
			_fontName = "";
		}
		OnValueChanged();
	}

	protected override void OnLayout(LayoutEventArgs e)
	{
		int vPos = LayoutBegin();
		Rectangle bounds;
		int num = LayoutLabel(vPos, out bounds);
		_fontNameComboBox.Location = new Point(num, vPos);
		int num2 = LayoutResetButton(_resetButton, vPos);
		int num3 = num2 - Ui.baseIconButtonHeight;
		_strikeoutButton.Bounds = new Rectangle(num3, vPos, Ui.baseIconButtonHeight, Ui.baseIconButtonHeight);
		num3 -= Ui.baseIconButtonHeight;
		_underlineButton.Bounds = new Rectangle(num3, vPos, Ui.baseIconButtonHeight, Ui.baseIconButtonHeight);
		num3 -= Ui.baseIconButtonHeight;
		_italicButton.Bounds = new Rectangle(num3, vPos, Ui.baseIconButtonHeight, Ui.baseIconButtonHeight);
		num3 -= Ui.baseIconButtonHeight;
		_boldButton.Bounds = new Rectangle(num3, vPos, Ui.baseIconButtonHeight, Ui.baseIconButtonHeight);
		num3 -= Ui.hMargin;
		_fontNameComboBox.Width = num2 - num - Ui.hMargin - 4 * Ui.baseIconButtonHeight;
		_labelControl.Bounds = bounds;
		_resetButton.Height = Ui.baseIconButtonHeight;
		LayoutEnd(_fontNameComboBox.Bottom, _resetButton.Bottom, _strikeoutButton.Bottom, _underlineButton.Bottom, _italicButton.Bottom, _boldButton.Bottom);
		base.OnLayout(e);
	}

	protected override void OnReset()
	{
		_fontNameComboBox.SelectedIndex = defaultIndex;
		ValueFontStyle = _defaultFontStyle;
	}

	public override void OptionDefaultToValues(OptionDictionary values)
	{
		values[base.Id] = FontUtil.UsableFontFamilies[defaultIndex];
		values[base.Id + ".FontStyle"] = _defaultFontStyle;
	}

	public override void OptionToValues(OptionDictionary values)
	{
		values[base.Id] = FontUtil.UsableFontFamilies[_fontNameComboBox.SelectedIndex];
		values[base.Id + ".FontStyle"] = ValueFontStyle;
	}

	public override void ValuesToOption(OptionDictionary values)
	{
		string familyName = (string)values[base.Id];
		_fontNameComboBox.SelectedIndex = FontUtil.FindFontIndex(familyName);
		ValueFontStyle = (FontStyle)values[base.Id + ".FontStyle"];
	}

	internal static FontFamily GetOptionValueFontName(Enum optId, OptionDictionary values)
	{
		return new FontFamily(Convert.ToString(values[optId.ToString()]));
	}

	public static FontStyle GetOptionValueFontStyle(Enum optId, OptionDictionary values)
	{
		return (FontStyle)values[string.Concat(optId, ".FontStyle")];
	}

	private void FontStyleButton_CheckedChanged(object sender, EventArgs e)
	{
		FontStyle fontStyle = FontStyle.Regular;
		if (_boldButton.Checked)
		{
			fontStyle |= FontStyle.Bold;
		}
		if (_italicButton.Checked)
		{
			fontStyle |= FontStyle.Italic;
		}
		if (_underlineButton.Checked)
		{
			fontStyle |= FontStyle.Underline;
		}
		if (_strikeoutButton.Checked)
		{
			fontStyle |= FontStyle.Strikeout;
		}
		ValueFontStyle = fontStyle;
	}
}
