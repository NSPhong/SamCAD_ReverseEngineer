using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using SamSoarII.Polyline.Entity;

namespace SamSoarII.Polyline.Control;

public class PointWidget : UserControl, IComponentConnector
{
	protected static readonly DependencyProperty PointNameProperty = DependencyProperty.Register("PointName", typeof(string), typeof(PointWidget), new PropertyMetadata("A point", OnPropertyChanged_PointName));

	protected static readonly DependencyProperty PointProperty = DependencyProperty.Register("Point", typeof(Point), typeof(PointWidget), new PropertyMetadata(default(Point), OnPropertyChanged_Point));

	private bool issubmit = false;

	private bool issubmitx = false;

	private bool issubmity = false;

	internal PointWidget This;

	internal TextBox TO_X;

	internal TextBox TO_Y;

	private bool _contentLoaded;

	public string PointName
	{
		get
		{
			return (string)GetValue(PointNameProperty);
		}
		set
		{
			SetValue(PointNameProperty, value);
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

	public event PointWidgetEventHandler PointChanged;

	public PointWidget()
	{
		InitializeComponent();
	}

	private static void OnPropertyChanged_PointName(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
	}

	private static void OnPropertyChanged_Point(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PointWidget)
		{
			((PointWidget)d).OnPointChanged(e);
		}
	}

	protected virtual void OnPointChanged(DependencyPropertyChangedEventArgs e)
	{
		if (TO_X != null)
		{
			TO_X.Text = Point.X.ToString();
		}
		if (TO_Y != null)
		{
			TO_Y.Text = Point.Y.ToString();
		}
		if (issubmit || issubmitx || issubmity)
		{
			this.PointChanged?.Invoke(this, new PointWidgetEventArgs((e.OldValue is Point) ? ((Point)e.OldValue) : default(Point), (e.NewValue is Point) ? ((Point)e.NewValue) : default(Point), issubmitx ? ChangedFlags.OnlyX : (issubmity ? ChangedFlags.OnlyY : ChangedFlags.None)));
		}
	}

	public void Submit()
	{
		issubmit = true;
		double result = Point.X;
		double result2 = Point.Y;
		double.TryParse(TO_X.Text, out result);
		double.TryParse(TO_Y.Text, out result2);
		Point = new Point(result, result2);
		issubmit = false;
	}

	public void SubmitX()
	{
		issubmitx = true;
		double result = Point.X;
		double y = Point.Y;
		double.TryParse(TO_X.Text, out result);
		Point = new Point(result, y);
		issubmitx = false;
	}

	public void SubmitY()
	{
		issubmity = true;
		double x = Point.X;
		double result = Point.Y;
		double.TryParse(TO_Y.Text, out result);
		Point = new Point(x, result);
		issubmity = false;
	}

	private void TO_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
	{
		if (sender is TextBox)
		{
			TextBox textBox = (TextBox)sender;
			textBox.SelectAll();
		}
	}

	private void TO_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
	{
		Submit();
	}

	private void TO_PreviewKeyDown(object sender, KeyEventArgs e)
	{
		if (e.Key == Key.Return)
		{
			e.Handled = true;
			if (sender == TO_X)
			{
				Keyboard.Focus(TO_Y);
			}
			else
			{
				Submit();
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
			Uri resourceLocator = new Uri("/SamSoarII.Polyline;component/control/pointwidget.xaml", UriKind.Relative);
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
			This = (PointWidget)target;
			break;
		case 2:
			TO_X = (TextBox)target;
			TO_X.GotKeyboardFocus += TO_GotKeyboardFocus;
			TO_X.LostKeyboardFocus += TO_LostKeyboardFocus;
			TO_X.PreviewKeyDown += TO_PreviewKeyDown;
			break;
		case 3:
			TO_Y = (TextBox)target;
			TO_Y.GotKeyboardFocus += TO_GotKeyboardFocus;
			TO_Y.LostKeyboardFocus += TO_LostKeyboardFocus;
			TO_Y.PreviewKeyDown += TO_PreviewKeyDown;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}
