using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SamSoarII.Shell;

public class OnlyNumsTextBox : CustomTextBox
{
	public static readonly DependencyProperty TopRangeProperty;

	public static readonly DependencyProperty LowRangeProperty;

	public static readonly DependencyProperty IsNumsOnlyProperty;

	private int oldvalue;

	public int OldValue => oldvalue;

	public bool IsNumsOnly
	{
		get
		{
			return (bool)GetValue(IsNumsOnlyProperty);
		}
		set
		{
			SetValue(IsNumsOnlyProperty, value);
		}
	}

	public int TopRange
	{
		get
		{
			return (int)GetValue(TopRangeProperty);
		}
		set
		{
			SetValue(TopRangeProperty, value);
		}
	}

	public int LowRange
	{
		get
		{
			return (int)GetValue(LowRangeProperty);
		}
		set
		{
			SetValue(LowRangeProperty, value);
		}
	}

	static OnlyNumsTextBox()
	{
		IsNumsOnlyProperty = DependencyProperty.Register("IsNumsOnly", typeof(bool), typeof(OnlyNumsTextBox));
		TopRangeProperty = DependencyProperty.Register("TopRange", typeof(int), typeof(OnlyNumsTextBox));
		LowRangeProperty = DependencyProperty.Register("LowRange", typeof(int), typeof(OnlyNumsTextBox));
	}

	public OnlyNumsTextBox()
	{
		base.GotFocus += OnlyNumsTextBox_GotFocus;
		base.LostFocus += OnlyNumsTextBox_LostFocus;
		base.TextChanged += OnlyNumsTextBox_TextChanged;
		base.PreviewTextInput += OnTextInput;
		base.AllowDrop = false;
		InputMethod.SetIsInputMethodEnabled(this, value: false);
		base.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, delegate
		{
		}, delegate(object sender_can, CanExecuteRoutedEventArgs e1)
		{
			e1.CanExecute = true;
		}));
	}

	private void OnTextInput(object sender, TextCompositionEventArgs e)
	{
		char c = Convert.ToChar(e.Text);
		if (!char.IsDigit(c) && c != '-')
		{
			e.Handled = true;
		}
	}

	private void OnlyNumsTextBox_TextChanged(object sender, TextChangedEventArgs e)
	{
		if (!IsNumsOnly)
		{
			return;
		}
		try
		{
			int value = int.Parse(base.Text);
			if (!AssertRange(value))
			{
				base.Background = Brushes.OrangeRed;
				return;
			}
			oldvalue = value;
			base.Background = Brushes.Transparent;
		}
		catch (Exception)
		{
			base.Background = Brushes.OrangeRed;
		}
	}

	private void OnlyNumsTextBox_LostFocus(object sender, RoutedEventArgs e)
	{
		base.Text = oldvalue.ToString();
	}

	private void OnlyNumsTextBox_GotFocus(object sender, RoutedEventArgs e)
	{
		int num = 0;
		try
		{
			num = int.Parse((sender as OnlyNumsTextBox).Text);
			if (AssertRange(num))
			{
				oldvalue = num;
			}
		}
		catch (Exception)
		{
		}
	}

	private bool AssertRange(int value)
	{
		return value >= LowRange && value <= TopRange;
	}

	private bool AssertOneSideRange(int value)
	{
		return value <= TopRange;
	}
}
