using System;
using System.Drawing;
using System.Windows.Forms;
using ControlExtensions;
using JustifyTextEffect.Properties;
using OptionControls;

internal class OptionTextAlignButtons : OptionControl
{
	private readonly Button _resetButton;

	private readonly RadioButton _leftRadioButton;

	private readonly RadioButton _centerRadioButton;

	private readonly RadioButton _rightRadioButton;

	private readonly RadioButton _justifyRadioButton;

	private readonly Label _justification;

	private byte _textAlign;

	private byte _defaultTextAlign;

	public byte Value
	{
		get
		{
			return _textAlign;
		}
		set
		{
			if (_defaultTextAlign == 0)
			{
				_leftRadioButton.Checked = value == 0;
			}
			if (_defaultTextAlign == 1)
			{
				_centerRadioButton.Checked = value == 1;
			}
			if (_defaultTextAlign == 2)
			{
				_rightRadioButton.Checked = value == 2;
			}
			if (_defaultTextAlign == 3)
			{
				_justifyRadioButton.Checked = value == 3;
			}
			if (_leftRadioButton.Checked)
			{
				_leftRadioButton.Checked = value == 0;
			}
			if (_centerRadioButton.Checked)
			{
				_centerRadioButton.Checked = value == 1;
			}
			if (_rightRadioButton.Checked)
			{
				_rightRadioButton.Checked = value == 2;
			}
			if (_justifyRadioButton.Checked)
			{
				_justifyRadioButton.Checked = value == 3;
			}
			if (value != _textAlign)
			{
				_textAlign = value;
				OnValueChanged();
			}
		}
	}

	public OptionTextAlignButtons(Enum optId, OptionContext optContext, byte defaultTextAlign)
		: base(optId, optContext)
	{
		_defaultTextAlign = defaultTextAlign;
		SuspendLayout();
		_resetButton = CreateResetButton();
		RadioButton radioButton = (_leftRadioButton = new RadioButton
		{
			Name = "LeftButton",
			FlatStyle = FlatStyle.Standard,
			Appearance = Appearance.Button,
			BackgroundImage = Resources.LeftButtonIcon,
			BackColor = SystemColors.Control,
			BackgroundImageLayout = ImageLayout.Center,
			ImageAlign = ContentAlignment.MiddleCenter
		});
		_leftRadioButton.CheckedChanged += TextAlignButton_CheckedChanged;
		base.ToolTip.SetToolTip(_leftRadioButton, optContext.MapKeyToText("Align.Left"));
		RadioButton radioButton2 = (_centerRadioButton = new RadioButton
		{
			Name = "CenterButton",
			FlatStyle = FlatStyle.Standard,
			Appearance = Appearance.Button,
			BackgroundImage = Resources.CenterButtonIcon,
			BackColor = SystemColors.Control,
			BackgroundImageLayout = ImageLayout.Center,
			ImageAlign = ContentAlignment.MiddleCenter
		});
		_centerRadioButton.CheckedChanged += TextAlignButton_CheckedChanged;
		base.ToolTip.SetToolTip(_centerRadioButton, optContext.MapKeyToText("Align.Center"));
		RadioButton radioButton3 = (_rightRadioButton = new RadioButton
		{
			Name = "RightButton",
			FlatStyle = FlatStyle.Standard,
			Appearance = Appearance.Button,
			BackgroundImage = Resources.RightButtonIcon,
			BackColor = SystemColors.Control,
			BackgroundImageLayout = ImageLayout.Center,
			ImageAlign = ContentAlignment.MiddleCenter
		});
		_centerRadioButton.CheckedChanged += TextAlignButton_CheckedChanged;
		base.ToolTip.SetToolTip(_rightRadioButton, optContext.MapKeyToText("Align.Right"));
		RadioButton radioButton4 = (_justifyRadioButton = new RadioButton
		{
			Name = "JustifyButton",
			FlatStyle = FlatStyle.Standard,
			Appearance = Appearance.Button,
			BackgroundImage = Resources.JustifyButtonIcon,
			BackColor = SystemColors.Control,
			BackgroundImageLayout = ImageLayout.Center,
			ImageAlign = ContentAlignment.MiddleCenter
		});
		_justifyRadioButton.CheckedChanged += TextAlignButton_CheckedChanged;
		base.ToolTip.SetToolTip(_justifyRadioButton, optContext.MapKeyToText("Align.Justify"));
		Label label = (_justification = new Label
		{
			Name = "Justification",
			FlatStyle = FlatStyle.Standard,
			TextAlign = ContentAlignment.MiddleLeft
		});
		base.Controls.AddRange(new Control[9] { _displayNameControl, _labelControl, _leftRadioButton, _centerRadioButton, _rightRadioButton, _justifyRadioButton, _justification, _resetButton, _descriptionControl });
		OnReset();
		ResumeLayout(performLayout: false);
		PerformLayout();
	}

	protected override void OnLayout(LayoutEventArgs e)
	{
		int num = LayoutBegin();
		int num2 = LayoutLabel(num, out var bounds);
		LayoutResetButton(_resetButton, num);
		_leftRadioButton.Bounds = new Rectangle(num2, num, Ui.baseIconButtonHeight, Ui.baseIconButtonHeight);
		num2 += Ui.baseIconButtonHeight;
		_centerRadioButton.Bounds = new Rectangle(num2, num, Ui.baseIconButtonHeight, Ui.baseIconButtonHeight);
		num2 += Ui.baseIconButtonHeight;
		_rightRadioButton.Bounds = new Rectangle(num2, num, Ui.baseIconButtonHeight, Ui.baseIconButtonHeight);
		num2 += Ui.baseIconButtonHeight;
		_justifyRadioButton.Bounds = new Rectangle(num2, num, Ui.baseIconButtonHeight, Ui.baseIconButtonHeight);
		num2 += Ui.baseIconButtonHeight;
		_justification.Bounds = new Rectangle(num2 + 2 * Ui.hButtonMargin, num - 1, _resetButton.Left - num2 - Ui.baseIconButtonHeight + Ui.hMargin, Ui.baseIconButtonHeight);
		num2 += Ui.baseIconButtonHeight;
		num2 += Ui.hMargin;
		num2 += Ui.baseIconButtonHeight;
		_resetButton.Height = Ui.baseIconButtonHeight;
		bounds.Y += (_resetButton.Height - bounds.Height) / 2;
		_labelControl.Bounds = bounds;
		LayoutEnd(_resetButton.Bottom);
		base.OnLayout(e);
	}

	private void TextAlignButton_CheckedChanged(object sender, EventArgs e)
	{
		byte value = _defaultTextAlign;
		if (_leftRadioButton.Checked)
		{
			value = 0;
			_justification.Text = "Left";
		}
		if (_centerRadioButton.Checked)
		{
			value = 1;
			_justification.Text = "Center";
		}
		if (_rightRadioButton.Checked)
		{
			value = 2;
			_justification.Text = "Right";
		}
		if (_justifyRadioButton.Checked)
		{
			value = 3;
			_justification.Text = "Justify";
		}
		Value = value;
	}

	public static byte GetOptionValue(Enum optId, OptionDictionary values)
	{
		return (byte)values[string.Concat(optId, ".Align")];
	}

	protected override void OnReset()
	{
		Value = _defaultTextAlign;
	}

	public override void OptionDefaultToValues(OptionDictionary values)
	{
		values[base.Id + ".Align"] = _defaultTextAlign;
	}

	public override void OptionToValues(OptionDictionary values)
	{
		values[base.Id + ".Align"] = Value;
	}

	public override void ValuesToOption(OptionDictionary values)
	{
		Value = (byte)values[base.Id + ".Align"];
		if (_defaultTextAlign == 0)
		{
			_justification.Text = "Left";
		}
		if (_defaultTextAlign == 1)
		{
			_justification.Text = "Center";
		}
		if (_defaultTextAlign == 2)
		{
			_justification.Text = "Right";
		}
		if (_defaultTextAlign == 3)
		{
			_justification.Text = "Justify";
		}
	}
}
