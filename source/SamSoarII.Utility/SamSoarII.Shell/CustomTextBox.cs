using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SamSoarII.Shell;

public class CustomTextBox : TextBox, INotifyPropertyChanged
{
	public static readonly DependencyProperty EmptyToolTipProperty;

	public static readonly DependencyProperty EmptyToolTipForegroundProperty;

	public string EmptyToolTip
	{
		get
		{
			return (string)GetValue(EmptyToolTipProperty);
		}
		set
		{
			SetValue(EmptyToolTipProperty, value);
		}
	}

	public Brush EmptyToolTipForeground
	{
		get
		{
			return (Brush)GetValue(EmptyToolTipForegroundProperty);
		}
		set
		{
			SetValue(EmptyToolTipForegroundProperty, value);
		}
	}

	public bool IsTextEmpty => string.IsNullOrEmpty(base.Text) && base.IsEnabled;

	public event PropertyChangedEventHandler PropertyChanged = delegate
	{
	};

	static CustomTextBox()
	{
		EmptyToolTipProperty = DependencyProperty.Register("EmptyToolTip", typeof(string), typeof(CustomTextBox), new FrameworkPropertyMetadata(string.Empty));
		EmptyToolTipForegroundProperty = DependencyProperty.Register("EmptyToolTipForeground", typeof(Brush), typeof(CustomTextBox));
		FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomTextBox), new FrameworkPropertyMetadata((object)null));
	}

	public CustomTextBox()
	{
		base.Style = (Style)FindResource("CustomTextBoxStyle");
		base.IsEnabledChanged += OnIsEnabledChanged;
		CommandBinding commandBinding = new CommandBinding(ApplicationCommands.Cut);
		commandBinding.CanExecute += OnCutCanExecute;
		commandBinding.Executed += OnCutExecuted;
		base.CommandBindings.Add(commandBinding);
	}

	private void OnCutCanExecute(object sender, CanExecuteRoutedEventArgs e)
	{
		e.CanExecute = base.SelectedText != null && base.SelectedText.Length > 0;
	}

	private void OnCutExecuted(object sender, ExecutedRoutedEventArgs e)
	{
		DataObject data = new DataObject(base.SelectedText);
		try
		{
			Clipboard.SetDataObject(data, copy: true);
		}
		catch (ExternalException)
		{
		}
		base.Text = base.Text.Remove(base.CaretIndex, base.SelectedText.Length);
	}

	private void OnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
	{
		this.PropertyChanged(this, new PropertyChangedEventArgs("IsTextEmpty"));
	}

	protected override void OnTextChanged(TextChangedEventArgs e)
	{
		this.PropertyChanged(this, new PropertyChangedEventArgs("IsTextEmpty"));
		base.OnTextChanged(e);
	}
}
