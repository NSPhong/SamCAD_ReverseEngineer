using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using System.Windows.Media;

namespace SamSoarII.Shell;

public class FloatTextBox : UserControl, IComponentConnector
{
	public static readonly DependencyProperty MinValueProperty = DependencyProperty.Register("MinValue", typeof(float), typeof(FloatTextBox), new PropertyMetadata(0f, OnPropertyChanged_MinValue));

	public static readonly DependencyProperty MaxValueProperty = DependencyProperty.Register("MaxValue", typeof(float), typeof(FloatTextBox), new PropertyMetadata(100f, OnPropertyChanged_MaxValue));

	public static readonly DependencyProperty NowValueProperty = DependencyProperty.Register("NowValue", typeof(float), typeof(FloatTextBox), new PropertyMetadata(0f, OnPropertyChanged_NowValue));

	public static readonly DependencyProperty StepProperty = DependencyProperty.Register("Step", typeof(float), typeof(FloatTextBox), new PropertyMetadata(0.1f, OnPropertyChanged_Step));

	public static readonly DependencyProperty CanDownProperty = DependencyProperty.Register("CanDown", typeof(bool), typeof(FloatTextBox), new PropertyMetadata(false, OnPropertyChanged_CanDown));

	public static readonly DependencyProperty CanUpProperty = DependencyProperty.Register("CanUp", typeof(bool), typeof(FloatTextBox), new PropertyMetadata(false, OnPropertyChanged_CanUp));

	private bool istextchanging = false;

	internal FloatTextBox This;

	internal MenuItem MI_Minimize;

	internal MenuItem MI_Maximize;

	internal TextBox TB_Val;

	private bool _contentLoaded;

	public float MinValue
	{
		get
		{
			return (float)GetValue(MinValueProperty);
		}
		set
		{
			SetValue(MinValueProperty, value);
		}
	}

	public float MaxValue
	{
		get
		{
			return (float)GetValue(MaxValueProperty);
		}
		set
		{
			SetValue(MaxValueProperty, value);
		}
	}

	public float NowValue
	{
		get
		{
			return (float)GetValue(NowValueProperty);
		}
		set
		{
			SetValue(NowValueProperty, value);
		}
	}

	public float Step
	{
		get
		{
			return (float)GetValue(StepProperty);
		}
		set
		{
			SetValue(StepProperty, value);
		}
	}

	public bool CanDown
	{
		get
		{
			return (bool)GetValue(CanDownProperty);
		}
		private set
		{
			SetValue(CanDownProperty, value);
		}
	}

	public bool CanUp
	{
		get
		{
			return (bool)GetValue(CanUpProperty);
		}
		private set
		{
			SetValue(CanUpProperty, value);
		}
	}

	public FloatTextBox()
	{
		InitializeComponent();
		if (MI_Minimize != null)
		{
			MI_Minimize.Header = $"Minimum value ({MinValue}";
		}
		if (MI_Maximize != null)
		{
			MI_Maximize.Header = $"Maximum value ({MinValue}";
		}
		UpdateEnable();
	}

	private static void OnPropertyChanged_MinValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is FloatTextBox)
		{
			((FloatTextBox)d).OnMinValueChanged(e);
		}
	}

	protected virtual void OnMinValueChanged(DependencyPropertyChangedEventArgs e)
	{
		NowValue = Math.Min(MaxValue, Math.Max(MinValue, NowValue));
		if (MI_Minimize != null)
		{
			MI_Minimize.Header = $"Minimum value ({MinValue}";
		}
		UpdateEnable();
	}

	private static void OnPropertyChanged_MaxValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is FloatTextBox)
		{
			((FloatTextBox)d).OnMaxValueChanged(e);
		}
	}

	protected virtual void OnMaxValueChanged(DependencyPropertyChangedEventArgs e)
	{
		NowValue = Math.Min(MaxValue, Math.Max(MinValue, NowValue));
		if (MI_Maximize != null)
		{
			MI_Maximize.Header = $"Maximum value ({MaxValue}";
		}
		UpdateEnable();
	}

	private static void OnPropertyChanged_NowValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is FloatTextBox)
		{
			((FloatTextBox)d).OnNowValueChanged(e);
		}
	}

	protected virtual void OnNowValueChanged(DependencyPropertyChangedEventArgs e)
	{
		NowValue = Math.Min(MaxValue, Math.Max(MinValue, NowValue));
		if (!istextchanging)
		{
			TB_Val.Text = NowValue.ToString();
		}
		UpdateEnable();
	}

	private static void OnPropertyChanged_Step(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is FloatTextBox)
		{
			((FloatTextBox)d).OnStepChanged(e);
		}
	}

	protected virtual void OnStepChanged(DependencyPropertyChangedEventArgs e)
	{
	}

	private static void OnPropertyChanged_CanDown(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is FloatTextBox)
		{
			((FloatTextBox)d).OnCanDownChanged(e);
		}
	}

	protected virtual void OnCanDownChanged(DependencyPropertyChangedEventArgs e)
	{
	}

	private static void OnPropertyChanged_CanUp(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is FloatTextBox)
		{
			((FloatTextBox)d).OnCanUpChanged(e);
		}
	}

	protected virtual void OnCanUpChanged(DependencyPropertyChangedEventArgs e)
	{
	}

	private void UpdateEnable()
	{
		CanDown = NowValue > MinValue;
		CanUp = NowValue < MaxValue;
	}

	private void OnDownClick(object sender, RoutedEventArgs e)
	{
		NowValue = Math.Max(NowValue - Step, MinValue);
	}

	private void OnUpClick(object sender, RoutedEventArgs e)
	{
		NowValue = Math.Min(NowValue + Step, MaxValue);
	}

	private void OnMinimizeClick(object sender, RoutedEventArgs e)
	{
		NowValue = MinValue;
	}

	private void OnMaximizeClick(object sender, RoutedEventArgs e)
	{
		NowValue = MaxValue;
	}

	private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
	{
		if (!istextchanging && e.Source is TextBox)
		{
			TextBox textBox = (TextBox)e.Source;
			string text = textBox.Text;
			float result = 0f;
			if (float.TryParse(text, out result) && result >= MinValue && result <= MaxValue)
			{
				istextchanging = true;
				NowValue = result;
				TB_Val.Background = Brushes.White;
				istextchanging = false;
			}
			else
			{
				TB_Val.Background = Brushes.OrangeRed;
			}
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/SamSoarII.Utility;component/controls/utils/floattextbox.xaml", UriKind.Relative);
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
			This = (FloatTextBox)target;
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
			TB_Val = (TextBox)target;
			TB_Val.TextChanged += TextBox_TextChanged;
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
