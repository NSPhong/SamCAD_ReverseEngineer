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

public class PointPaintLabel : UserControl, IComponentConnector
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

	protected static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register("IsActive", typeof(bool), typeof(PointPaintLabel), new PropertyMetadata(false, OnPropertyChanged_IsActive));

	protected static readonly DependencyProperty MessageProperty = DependencyProperty.Register("Message", typeof(string), typeof(PointPaintLabel), new PropertyMetadata(string.Empty, OnPropertyChanged_Message));

	protected static readonly DependencyProperty PointProperty = DependencyProperty.Register("Point", typeof(Point), typeof(PointPaintLabel), new PropertyMetadata(default(Point), OnPropertyChanged_Point));

	protected static readonly DependencyProperty XEditProperty = DependencyProperty.Register("XEdit", typeof(bool), typeof(PointPaintLabel), new PropertyMetadata(false, OnPropertyChanged_XEdit));

	protected static readonly DependencyProperty YEditProperty = DependencyProperty.Register("YEdit", typeof(bool), typeof(PointPaintLabel), new PropertyMetadata(false, OnPropertyChanged_YEdit));

	protected static readonly DependencyProperty XLockProperty = DependencyProperty.Register("XLock", typeof(bool), typeof(PointPaintLabel), new PropertyMetadata(false, OnPropertyChanged_XLock));

	protected static readonly DependencyProperty YLockProperty = DependencyProperty.Register("YLock", typeof(bool), typeof(PointPaintLabel), new PropertyMetadata(false, OnPropertyChanged_YLock));

	private bool tx_x_changed = false;

	private bool tx_y_changed = false;

	private bool tx_x_pointchange = false;

	private bool tx_y_pointchange = false;

	internal PointPaintLabel This;

	internal Border BD_X;

	internal TextBox TX_X;

	internal Border BD_Y;

	internal TextBox TX_Y;

	private bool _contentLoaded;

	public bool IsActive
	{
		get
		{
			return (bool)GetValue(IsActiveProperty);
		}
		protected set
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

	public Point Point
	{
		get
		{
			return (Point)GetValue(PointProperty);
		}
		set
		{
			SetValue(PointProperty, value);
		}
	}

	public bool XEdit
	{
		get
		{
			return (bool)GetValue(XEditProperty);
		}
		set
		{
			SetValue(XEditProperty, value);
		}
	}

	public bool YEdit
	{
		get
		{
			return (bool)GetValue(YEditProperty);
		}
		set
		{
			SetValue(YEditProperty, value);
		}
	}

	public bool XLock
	{
		get
		{
			return (bool)GetValue(XLockProperty);
		}
		set
		{
			SetValue(XLockProperty, value);
		}
	}

	public bool YLock
	{
		get
		{
			return (bool)GetValue(YLockProperty);
		}
		set
		{
			SetValue(YLockProperty, value);
		}
	}

	public event RoutedEventHandler Submit;

	public event RoutedEventHandler Escape;

	public PointPaintLabel()
	{
		InitializeComponent();
		Canvas.SetTop(this, 0.0);
		Canvas.SetLeft(this, 0.0);
	}

	private static void OnPropertyChanged_IsActive(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PointPaintLabel)
		{
			((PointPaintLabel)d).OnIsActiveChanged(e);
		}
	}

	protected virtual void OnIsActiveChanged(DependencyPropertyChangedEventArgs e)
	{
	}

	private static void OnPropertyChanged_Message(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PointPaintLabel)
		{
			((PointPaintLabel)d).OnMessageChanged(e);
		}
	}

	protected virtual void OnMessageChanged(DependencyPropertyChangedEventArgs e)
	{
	}

	private static void OnPropertyChanged_Point(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PointPaintLabel)
		{
			((PointPaintLabel)d).OnPointChanged(e);
		}
	}

	protected virtual void OnPointChanged(DependencyPropertyChangedEventArgs e)
	{
		if (TX_X.IsVisible && !XLock)
		{
			tx_x_pointchange = true;
			TX_X.Text = Point.X.ToString();
			TX_X.SelectAll();
			tx_x_pointchange = false;
		}
		if (TX_Y.IsVisible && !YLock)
		{
			tx_y_pointchange = true;
			TX_Y.Text = Point.Y.ToString();
			TX_Y.SelectAll();
			tx_y_pointchange = false;
		}
	}

	private static void OnPropertyChanged_XEdit(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PointPaintLabel)
		{
			((PointPaintLabel)d).OnXEditChanged(e);
		}
	}

	protected virtual void OnXEditChanged(DependencyPropertyChangedEventArgs e)
	{
		BD_X.BorderBrush = (XEdit ? BorderBrush_Edit : BorderBrush_NotEdit);
	}

	private static void OnPropertyChanged_YEdit(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PointPaintLabel)
		{
			((PointPaintLabel)d).OnYEditChanged(e);
		}
	}

	protected virtual void OnYEditChanged(DependencyPropertyChangedEventArgs e)
	{
		BD_Y.BorderBrush = (YEdit ? BorderBrush_Edit : BorderBrush_NotEdit);
	}

	private static void OnPropertyChanged_XLock(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PointPaintLabel)
		{
			((PointPaintLabel)d).OnXLockChanged(e);
		}
	}

	protected virtual void OnXLockChanged(DependencyPropertyChangedEventArgs e)
	{
	}

	private static void OnPropertyChanged_YLock(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PointPaintLabel)
		{
			((PointPaintLabel)d).OnYLockChanged(e);
		}
	}

	protected virtual void OnYLockChanged(DependencyPropertyChangedEventArgs e)
	{
	}

	public void Begin(string _message)
	{
		IsActive = true;
		Message = _message;
		XEdit = true;
		base.Visibility = Visibility.Visible;
	}

	public void End()
	{
		IsActive = false;
		bool xLock = (YLock = false);
		XLock = xLock;
		xLock = (YEdit = false);
		XEdit = xLock;
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
			if (textBox == TX_X)
			{
				tx_x_changed = false;
				tx_x_pointchange = true;
				TX_X.Text = Point.X.ToString();
				Keyboard.Focus(TX_X);
				TX_X.SelectAll();
				tx_x_pointchange = false;
			}
			if (textBox == TX_Y)
			{
				tx_y_changed = false;
				tx_y_pointchange = true;
				TX_Y.Text = Point.Y.ToString();
				Keyboard.Focus(TX_Y);
				TX_Y.SelectAll();
				tx_y_pointchange = false;
			}
		}
	}

	private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
	{
		if (sender is TextBox)
		{
			TextBox textBox = (TextBox)sender;
			if (textBox == TX_X && !tx_x_pointchange)
			{
				tx_x_changed = true;
			}
			if (textBox == TX_Y && !tx_y_pointchange)
			{
				tx_y_changed = true;
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
		if (textBox == TX_X)
		{
			if (IsActive && !YEdit)
			{
				this.Escape?.Invoke(this, new RoutedEventArgs());
				return;
			}
			double result = 0.0;
			XEdit = false;
			if (double.TryParse(TX_X.Text, out result))
			{
				XLock = XLock || tx_x_changed;
				Point = new Point(result, Point.Y);
			}
			else
			{
				XLock = false;
			}
		}
		if (textBox != TX_Y)
		{
			return;
		}
		if (IsActive && !XEdit)
		{
			this.Escape?.Invoke(this, new RoutedEventArgs());
			return;
		}
		double result2 = 0.0;
		YEdit = false;
		if (double.TryParse(TX_Y.Text, out result2))
		{
			YLock = YLock || tx_y_changed;
			Point = new Point(Point.X, result2);
		}
		else
		{
			YLock = false;
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
			if (textBox == TX_X)
			{
				YEdit = true;
			}
			if (textBox == TX_Y)
			{
				XEdit = true;
			}
		}
		if (e.Key == Key.Return)
		{
			e.Handled = true;
			if (textBox == TX_X || textBox == TX_Y)
			{
				double result = Point.X;
				double result2 = Point.Y;
				if (TX_X.IsVisible)
				{
					double.TryParse(TX_X.Text, out result);
				}
				if (TX_Y.IsVisible)
				{
					double.TryParse(TX_Y.Text, out result2);
				}
				Point = new Point(result, result2);
				bool xEdit = (YEdit = false);
				XEdit = xEdit;
				this.Submit?.Invoke(this, new RoutedEventArgs());
			}
		}
		if (e.Key == Key.Escape)
		{
			e.Handled = true;
			if (textBox == TX_X || textBox == TX_Y)
			{
				bool xEdit = (YEdit = false);
				XEdit = xEdit;
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
			Uri resourceLocator = new Uri("/SamSoarII.Polyline;component/control/pointpaintlabel.xaml", UriKind.Relative);
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
			This = (PointPaintLabel)target;
			break;
		case 2:
			BD_X = (Border)target;
			break;
		case 3:
			TX_X = (TextBox)target;
			TX_X.IsVisibleChanged += TextBox_IsVisibleChanged;
			TX_X.LostKeyboardFocus += TextBox_LostKeyboardFocus;
			TX_X.TextChanged += TextBox_TextChanged;
			TX_X.PreviewKeyDown += TextBox_PreviewKeyDown;
			break;
		case 4:
			BD_Y = (Border)target;
			break;
		case 5:
			TX_Y = (TextBox)target;
			TX_Y.IsVisibleChanged += TextBox_IsVisibleChanged;
			TX_Y.LostKeyboardFocus += TextBox_LostKeyboardFocus;
			TX_Y.TextChanged += TextBox_TextChanged;
			TX_Y.PreviewKeyDown += TextBox_PreviewKeyDown;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}
