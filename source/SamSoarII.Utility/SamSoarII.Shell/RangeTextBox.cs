using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using SamSoarII.Properties;
using SamSoarII.Utility;

namespace SamSoarII.Shell;

public class RangeTextBox : UserControl, INotifyPropertyChanged, IComponentConnector
{
	public static readonly Key[] NumberKeys = new Key[26]
	{
		Key.NumPad0,
		Key.NumPad1,
		Key.NumPad2,
		Key.NumPad3,
		Key.NumPad4,
		Key.NumPad5,
		Key.NumPad6,
		Key.NumPad7,
		Key.NumPad8,
		Key.NumPad9,
		Key.D0,
		Key.D1,
		Key.D2,
		Key.D3,
		Key.D4,
		Key.D5,
		Key.D6,
		Key.D7,
		Key.D8,
		Key.D9,
		Key.A,
		Key.B,
		Key.C,
		Key.D,
		Key.E,
		Key.F
	};

	public static readonly int[] NumberKeyValues = new int[26]
	{
		0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
		0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
		10, 11, 12, 13, 14, 15
	};

	public static readonly Key[] ControlKeys = new Key[8]
	{
		Key.Up,
		Key.Down,
		Key.Left,
		Key.Right,
		Key.Return,
		Key.Escape,
		Key.Tab,
		Key.Back
	};

	public static readonly DependencyProperty TopRangeProperty = DependencyProperty.Register("TopRange", typeof(int), typeof(RangeTextBox), new FrameworkPropertyMetadata(100, OnTopRangeChanged)
	{
		BindsTwoWayByDefault = true
	});

	public static readonly DependencyProperty LowRangeProperty = DependencyProperty.Register("LowRange", typeof(int), typeof(RangeTextBox), new FrameworkPropertyMetadata(0, OnLowRangeChanged)
	{
		BindsTwoWayByDefault = true
	});

	public static readonly DependencyProperty NowValueProperty = DependencyProperty.Register("NowValue", typeof(int), typeof(RangeTextBox), new FrameworkPropertyMetadata(0, OnNowValueChanged)
	{
		BindsTwoWayByDefault = true
	});

	public static readonly DependencyProperty IsInputValidProperty = DependencyProperty.Register("IsInputValid", typeof(bool), typeof(RangeTextBox), new FrameworkPropertyMetadata(false, OnIsInputValidChanged)
	{
		BindsTwoWayByDefault = false
	});

	public static readonly DependencyProperty NumberBaseProperty = DependencyProperty.Register("NumberBase", typeof(int), typeof(RangeTextBox), new FrameworkPropertyMetadata(10, OnNumberBaseChanged)
	{
		BindsTwoWayByDefault = true
	});

	internal RangeTextBox This;

	internal MenuItem MI_Minimize;

	internal MenuItem MI_Maximize;

	internal TextBox textbox;

	private bool _contentLoaded;

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

	public int NowValue
	{
		get
		{
			return (int)GetValue(NowValueProperty);
		}
		set
		{
			SetValue(NowValueProperty, Math.Max(Math.Min(value, TopRange), LowRange));
		}
	}

	public bool IsInputValid
	{
		get
		{
			return (bool)GetValue(IsInputValidProperty);
		}
		protected set
		{
			SetValue(IsInputValidProperty, value);
		}
	}

	public int NumberBase
	{
		get
		{
			return Math.Max(2, Math.Min(16, (int)GetValue(NumberBaseProperty)));
		}
		set
		{
			SetValue(NumberBaseProperty, Math.Min(16, Math.Max(2, value)));
		}
	}

	public string Text => textbox.Text;

	public int CaretIndex => textbox.CaretIndex;

	public bool CanUp => NowValue < TopRange;

	public bool CanDown => NowValue > LowRange;

	public bool CanLeft => CaretIndex > 0;

	public bool CanRight => CaretIndex < Text.Length;

	public string MenuHeader_LowRange => string.Format(SamSoarII.Properties.Resources.RangeTextBox_ToLowRange, ValueConverter.NBase_ToString(LowRange, NumberBase));

	public string MenuHeader_TopRange => string.Format(SamSoarII.Properties.Resources.RangeTextBox_ToTopRange, ValueConverter.NBase_ToString(TopRange, NumberBase));

	public event PropertyChangedEventHandler PropertyChanged = delegate
	{
	};

	public event DependencyPropertyChangedEventHandler ValueChanged;

	public bool AssertKey(Key key, string text)
	{
		return AssertKey(key, text, Keyboard.PrimaryDevice);
	}

	public bool AssertKey(Key key, string text, KeyboardDevice device)
	{
		if (NumberKeys.Contains(key))
		{
			return NumberKeyValues[Array.IndexOf(NumberKeys, key)] < NumberBase && device.Modifiers == ModifierKeys.None && text.Length < 10;
		}
		if (ControlKeys.Contains(key))
		{
			return true;
		}
		return false;
	}

	public RangeTextBox()
	{
		InitializeComponent();
		InputMethod.SetIsInputMethodEnabled(textbox, value: false);
		base.Loaded += RangeTextBox_Loaded;
		base.GotFocus += RangeTextBox_GotFocus;
		base.GotKeyboardFocus += RangeTextBox_GotKeyboardFocus;
		MI_Minimize.Header = MenuHeader_LowRange;
		MI_Maximize.Header = MenuHeader_TopRange;
		textbox.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, delegate
		{
		}, delegate(object sender_can, CanExecuteRoutedEventArgs e1)
		{
			e1.CanExecute = true;
		}));
	}

	protected void InvokePropertyChanged(string propertyname)
	{
		this.PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
	}

	private static void OnTopRangeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is RangeTextBox)
		{
			RangeTextBox rangeTextBox = (RangeTextBox)d;
			rangeTextBox.OnTopRangeChanged(e);
		}
	}

	protected virtual void OnTopRangeChanged(DependencyPropertyChangedEventArgs e)
	{
		NowValue = NowValue;
		IsInputValid = NowValue >= LowRange && NowValue <= TopRange;
		InvokePropertyChanged("TopRange");
		InvokePropertyChanged("CanUp");
		InvokePropertyChanged("MenuHeader_TopRange");
		MI_Maximize.Header = MenuHeader_TopRange;
	}

	private static void OnLowRangeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is RangeTextBox)
		{
			RangeTextBox rangeTextBox = (RangeTextBox)d;
			rangeTextBox.OnLowRangeChanged(e);
		}
	}

	protected virtual void OnLowRangeChanged(DependencyPropertyChangedEventArgs e)
	{
		NowValue = NowValue;
		IsInputValid = NowValue >= LowRange && NowValue <= TopRange;
		InvokePropertyChanged("LowRange");
		InvokePropertyChanged("CanDown");
		InvokePropertyChanged("MenuHeader_LowRange");
		MI_Minimize.Header = MenuHeader_LowRange;
	}

	private static void OnNowValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is RangeTextBox)
		{
			RangeTextBox rangeTextBox = (RangeTextBox)d;
			rangeTextBox.OnNowValueChanged(e);
		}
	}

	protected virtual void OnNowValueChanged(DependencyPropertyChangedEventArgs e)
	{
		if (base.IsLoaded && textbox != null)
		{
			textbox.Text = ValueConverter.NBase_ToString(NowValue, NumberBase);
		}
		IsInputValid = NowValue >= LowRange && NowValue <= TopRange;
		this.ValueChanged?.Invoke(this, e);
		InvokePropertyChanged("NowValue");
		InvokePropertyChanged("CanUp");
		InvokePropertyChanged("CanDown");
	}

	private static void OnIsInputValidChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is RangeTextBox)
		{
			RangeTextBox rangeTextBox = (RangeTextBox)d;
			rangeTextBox.OnIsInputValidChanged(e);
		}
	}

	protected virtual void OnIsInputValidChanged(DependencyPropertyChangedEventArgs e)
	{
		if (e.NewValue is bool)
		{
			if ((bool)e.NewValue)
			{
				textbox.Background = Brushes.Transparent;
			}
			else
			{
				textbox.Background = Brushes.OrangeRed;
			}
		}
		InvokePropertyChanged("IsInputValid");
	}

	private static void OnNumberBaseChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is RangeTextBox)
		{
			RangeTextBox rangeTextBox = (RangeTextBox)d;
			rangeTextBox.OnNumberBaseChanged(e);
		}
	}

	protected virtual void OnNumberBaseChanged(DependencyPropertyChangedEventArgs e)
	{
		if (base.IsLoaded && textbox != null)
		{
			textbox.Text = ValueConverter.NBase_ToString(NowValue, NumberBase);
		}
		MI_Maximize.Header = MenuHeader_TopRange;
		MI_Minimize.Header = MenuHeader_LowRange;
	}

	private void RangeTextBox_Loaded(object sender, RoutedEventArgs e)
	{
		textbox.Text = ValueConverter.NBase_ToString(NowValue, NumberBase);
	}

	private void RangeTextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
	{
		Keyboard.Focus(textbox);
	}

	private void RangeTextBox_GotFocus(object sender, RoutedEventArgs e)
	{
		textbox.Focus();
	}

	private void OnTextboxPreviewKeyDown(object sender, KeyEventArgs e)
	{
		if (!AssertKey(e.Key, textbox.Text, e.KeyboardDevice))
		{
			e.Handled = true;
		}
	}

	private void OnTextboxTextChanged(object sender, TextChangedEventArgs e)
	{
		if (ValueConverter.NBase_Assert(textbox.Text, NumberBase))
		{
			int num = ValueConverter.NBase_Parse(textbox.Text, NumberBase);
			if (num >= LowRange && num <= TopRange)
			{
				NowValue = num;
				IsInputValid = true;
			}
			else if (num > TopRange)
			{
				textbox.Text = ValueConverter.NBase_ToString(TopRange, NumberBase);
				textbox.CaretIndex = textbox.Text.Length;
				IsInputValid = true;
			}
			else
			{
				IsInputValid = false;
			}
		}
		else
		{
			IsInputValid = false;
		}
	}

	private void OnTextboxLostFocus(object sender, RoutedEventArgs e)
	{
		textbox.Text = ValueConverter.NBase_ToString(NowValue, NumberBase);
	}

	private void OnUpClick(object sender, RoutedEventArgs e)
	{
		if (CanUp)
		{
			NowValue++;
		}
	}

	private void OnDownClick(object sender, RoutedEventArgs e)
	{
		if (CanDown)
		{
			NowValue--;
		}
	}

	private void OnMinimizeClick(object sender, RoutedEventArgs e)
	{
		NowValue = LowRange;
	}

	private void OnMaximizeClick(object sender, RoutedEventArgs e)
	{
		NowValue = TopRange;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/SamSoarII.Utility;component/controls/utils/rangetextbox.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	void IComponentConnector.Connect(int connectionId, object target)
	{
		switch (connectionId)
		{
		case 1:
			This = (RangeTextBox)target;
			break;
		case 2:
			MI_Minimize = (MenuItem)target;
			MI_Minimize.Click += OnMinimizeClick;
			break;
		case 3:
			MI_Maximize = (MenuItem)target;
			MI_Maximize.Click += OnMaximizeClick;
			break;
		case 4:
			textbox = (TextBox)target;
			textbox.PreviewKeyDown += OnTextboxPreviewKeyDown;
			textbox.TextChanged += OnTextboxTextChanged;
			textbox.LostFocus += OnTextboxLostFocus;
			break;
		case 5:
			((RepeatButton)target).Click += OnUpClick;
			break;
		case 6:
			((RepeatButton)target).Click += OnDownClick;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}
