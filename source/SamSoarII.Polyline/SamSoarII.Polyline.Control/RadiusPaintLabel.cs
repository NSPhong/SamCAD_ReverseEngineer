using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace SamSoarII.Polyline.Control;

public class RadiusPaintLabel : UserControl, IComponentConnector
{
	private static readonly Brush BorderBrush_NotEdit = new SolidColorBrush(new Color
	{
		A = byte.MaxValue,
		R = 224,
		G = 224,
		B = 224
	});

	private static readonly Brush BorderBrush_Edit = new SolidColorBrush(new Color
	{
		A = byte.MaxValue,
		R = 128,
		G = byte.MaxValue,
		B = 128
	});

	protected static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register("IsActive", typeof(bool), typeof(RadiusPaintLabel), new PropertyMetadata(false, OnPropertyChanged_IsActive));

	protected static readonly DependencyProperty MessageProperty = DependencyProperty.Register("Message", typeof(string), typeof(RadiusPaintLabel), new PropertyMetadata("调整Radius", OnPropertyChanged_Message));

	protected static readonly DependencyProperty RadiusProperty = DependencyProperty.Register("Radius", typeof(double), typeof(RadiusPaintLabel), new PropertyMetadata(0.0, OnPropertyChanged_Radius));

	protected static readonly DependencyProperty AngleProperty = DependencyProperty.Register("Angle", typeof(double), typeof(RadiusPaintLabel), new PropertyMetadata(0.0, OnPropertyChanged_Angle));

	protected static readonly DependencyProperty REditProperty = DependencyProperty.Register("REdit", typeof(bool), typeof(RadiusPaintLabel), new PropertyMetadata(false, OnPropertyChanged_REdit));

	protected static readonly DependencyProperty AEditProperty = DependencyProperty.Register("AEdit", typeof(bool), typeof(RadiusPaintLabel), new PropertyMetadata(false, OnPropertyChanged_AEdit));

	protected static readonly DependencyProperty RLockProperty = DependencyProperty.Register("RLock", typeof(bool), typeof(RadiusPaintLabel), new PropertyMetadata(false, OnPropertyChanged_RLock));

	protected static readonly DependencyProperty ALockProperty = DependencyProperty.Register("ALock", typeof(bool), typeof(RadiusPaintLabel), new PropertyMetadata(false, OnPropertyChanged_ALock));

	private bool tx_r_changed = false;

	private bool tx_a_changed = false;

	private bool tx_r_pointchange = false;

	private bool tx_a_pointchange = false;

	internal RadiusPaintLabel This;

	internal Border BD_R;

	internal TextBox TX_R;

	internal Border BD_A;

	internal TextBox TX_A;

	private bool _contentLoaded;

	public bool IsActive
	{
		get
		{
			return (bool)GetValue(IsActiveProperty);
		}
		set
		{
			SetValue(IsActiveProperty, value);
		}
	}

	public string Message
	{
		get
		{
			return (string)GetValue(MessageProperty);
		}
		set
		{
			SetValue(MessageProperty, value);
		}
	}

	public double Radius
	{
		get
		{
			return (double)GetValue(RadiusProperty);
		}
		set
		{
			SetValue(RadiusProperty, value);
		}
	}

	public double Angle
	{
		get
		{
			return (double)GetValue(AngleProperty);
		}
		set
		{
			SetValue(AngleProperty, value);
		}
	}

	public bool REdit
	{
		get
		{
			return (bool)GetValue(REditProperty);
		}
		set
		{
			SetValue(REditProperty, value);
		}
	}

	public bool AEdit
	{
		get
		{
			return (bool)GetValue(AEditProperty);
		}
		set
		{
			SetValue(AEditProperty, value);
		}
	}

	public bool RLock
	{
		get
		{
			return (bool)GetValue(RLockProperty);
		}
		set
		{
			SetValue(RLockProperty, value);
		}
	}

	public bool ALock
	{
		get
		{
			return (bool)GetValue(ALockProperty);
		}
		set
		{
			SetValue(ALockProperty, value);
		}
	}

	public event RoutedEventHandler Submit;

	public event RoutedEventHandler Escape;

	public RadiusPaintLabel()
	{
		InitializeComponent();
		Canvas.SetTop(this, 0.0);
		Canvas.SetLeft(this, 0.0);
	}

	private static void OnPropertyChanged_IsActive(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is RadiusPaintLabel)
		{
			((RadiusPaintLabel)d).OnIsActiveChanged(e);
		}
	}

	protected virtual void OnIsActiveChanged(DependencyPropertyChangedEventArgs e)
	{
	}

	private static void OnPropertyChanged_Message(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is RadiusPaintLabel)
		{
			((RadiusPaintLabel)d).OnMessageChanged(e);
		}
	}

	protected virtual void OnMessageChanged(DependencyPropertyChangedEventArgs e)
	{
	}

	private static void OnPropertyChanged_Radius(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is RadiusPaintLabel)
		{
			((RadiusPaintLabel)d).OnRadiusChanged(e);
		}
	}

	protected virtual void OnRadiusChanged(DependencyPropertyChangedEventArgs e)
	{
		if (TX_R.IsVisible && !RLock)
		{
			tx_r_pointchange = true;
			TX_R.Text = Radius.ToString();
			TX_R.SelectAll();
			tx_r_pointchange = false;
		}
	}

	private static void OnPropertyChanged_Angle(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is RadiusPaintLabel)
		{
			((RadiusPaintLabel)d).OnAngleChanged(e);
		}
	}

	protected virtual void OnAngleChanged(DependencyPropertyChangedEventArgs e)
	{
		if (TX_A.IsVisible && !ALock)
		{
			tx_a_pointchange = true;
			TX_A.Text = Angle.ToString();
			TX_A.SelectAll();
			tx_a_pointchange = false;
		}
	}

	private static void OnPropertyChanged_REdit(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is RadiusPaintLabel)
		{
			((RadiusPaintLabel)d).OnREditChanged(e);
		}
	}

	protected virtual void OnREditChanged(DependencyPropertyChangedEventArgs e)
	{
		BD_R.BorderBrush = (REdit ? BorderBrush_Edit : BorderBrush_NotEdit);
	}

	private static void OnPropertyChanged_AEdit(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is RadiusPaintLabel)
		{
			((RadiusPaintLabel)d).OnAEditChanged(e);
		}
	}

	protected virtual void OnAEditChanged(DependencyPropertyChangedEventArgs e)
	{
		BD_A.BorderBrush = (AEdit ? BorderBrush_Edit : BorderBrush_NotEdit);
	}

	private static void OnPropertyChanged_RLock(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is RadiusPaintLabel)
		{
			((RadiusPaintLabel)d).OnRLockChanged(e);
		}
	}

	protected virtual void OnRLockChanged(DependencyPropertyChangedEventArgs e)
	{
	}

	private static void OnPropertyChanged_ALock(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is RadiusPaintLabel)
		{
			((RadiusPaintLabel)d).OnALockChanged(e);
		}
	}

	protected virtual void OnALockChanged(DependencyPropertyChangedEventArgs e)
	{
	}

	public void Begin(string _message)
	{
		IsActive = true;
		Message = _message;
		REdit = true;
		base.Visibility = Visibility.Visible;
	}

	public void End()
	{
		IsActive = false;
		bool rLock = (ALock = false);
		RLock = rLock;
		rLock = (AEdit = false);
		REdit = rLock;
		base.Visibility = Visibility.Hidden;
	}

	public void Follow(MouseEventArgs e, Size sz)
	{
		Point position = e.GetPosition(this);
		double left = Canvas.GetLeft(this);
		double top = Canvas.GetTop(this);
		left += position.X;
		top += position.Y;
		left = Math.Max(left, 0.0);
		left = Math.Min(left, sz.Width - base.ActualWidth);
		top = Math.Max(top, 0.0);
		top = Math.Min(top, sz.Height - base.ActualHeight);
		Canvas.SetTop(this, top);
		Canvas.SetLeft(this, left);
	}

	private void TextBox_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
	{
		if (!(sender is TextBox) || !(e.OldValue is bool) || !(e.NewValue is bool))
		{
			return;
		}
		TextBox textBox = (TextBox)sender;
		if (!(bool)e.OldValue && (bool)e.NewValue)
		{
			if (textBox == TX_R)
			{
				tx_r_changed = false;
				tx_r_pointchange = true;
				TX_R.Text = Radius.ToString();
				Keyboard.Focus(TX_R);
				TX_R.SelectAll();
				tx_r_pointchange = false;
			}
			if (textBox == TX_A)
			{
				tx_a_changed = false;
				tx_a_pointchange = true;
				TX_A.Text = Angle.ToString();
				Keyboard.Focus(TX_A);
				TX_A.SelectAll();
				tx_a_pointchange = false;
			}
		}
	}

	private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
	{
		if (sender is TextBox)
		{
			TextBox textBox = (TextBox)sender;
			if (textBox == TX_R && !tx_r_pointchange)
			{
				tx_r_changed = true;
			}
			if (textBox == TX_A && !tx_a_pointchange)
			{
				tx_a_changed = true;
			}
		}
	}

	private void TextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
	{
		if (!(sender is TextBox))
		{
			return;
		}
		TextBox textBox = (TextBox)sender;
		if (textBox == TX_R)
		{
			if (IsActive && !AEdit)
			{
				this.Escape?.Invoke(this, new RoutedEventArgs());
				return;
			}
			double result = 0.0;
			REdit = false;
			if (double.TryParse(TX_R.Text, out result))
			{
				RLock = RLock || tx_r_changed;
				Radius = result;
			}
			else
			{
				RLock = false;
			}
		}
		if (textBox != TX_A)
		{
			return;
		}
		if (IsActive && !REdit)
		{
			this.Escape?.Invoke(this, new RoutedEventArgs());
			return;
		}
		double result2 = 0.0;
		AEdit = false;
		if (double.TryParse(TX_A.Text, out result2))
		{
			ALock = ALock || tx_a_changed;
			Angle = result2;
		}
		else
		{
			ALock = false;
		}
	}

	private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
	{
		if (!(sender is TextBox))
		{
			return;
		}
		TextBox textBox = (TextBox)sender;
		if (e.Key == Key.Tab)
		{
			e.Handled = true;
			if (textBox == TX_R)
			{
				AEdit = true;
			}
			if (textBox == TX_A)
			{
				REdit = true;
			}
		}
		if (e.Key == Key.Return)
		{
			e.Handled = true;
			if (textBox == TX_R || textBox == TX_A)
			{
				double result = Radius;
				double result2 = Angle;
				if (TX_R.IsVisible)
				{
					double.TryParse(TX_R.Text, out result);
				}
				if (TX_A.IsVisible)
				{
					double.TryParse(TX_A.Text, out result2);
				}
				Radius = result;
				Angle = result2;
				bool rEdit = (AEdit = false);
				REdit = rEdit;
				this.Submit?.Invoke(this, new RoutedEventArgs());
			}
		}
		if (e.Key == Key.Escape)
		{
			e.Handled = true;
			if (textBox == TX_R || textBox == TX_A)
			{
				bool rEdit = (AEdit = false);
				REdit = rEdit;
				this.Escape?.Invoke(this, new RoutedEventArgs());
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
			Uri resourceLocator = new Uri("/SamSoarII.Polyline;component/control/radiuspaintlabel.xaml", UriKind.Relative);
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
			This = (RadiusPaintLabel)target;
			break;
		case 2:
			BD_R = (Border)target;
			break;
		case 3:
			TX_R = (TextBox)target;
			TX_R.IsVisibleChanged += TextBox_IsVisibleChanged;
			TX_R.LostKeyboardFocus += TextBox_LostKeyboardFocus;
			TX_R.TextChanged += TextBox_TextChanged;
			TX_R.PreviewKeyDown += TextBox_PreviewKeyDown;
			break;
		case 4:
			BD_A = (Border)target;
			break;
		case 5:
			TX_A = (TextBox)target;
			TX_A.IsVisibleChanged += TextBox_IsVisibleChanged;
			TX_A.LostKeyboardFocus += TextBox_LostKeyboardFocus;
			TX_A.TextChanged += TextBox_TextChanged;
			TX_A.PreviewKeyDown += TextBox_PreviewKeyDown;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}
