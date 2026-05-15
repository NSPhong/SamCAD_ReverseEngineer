using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace SamCAD.Control;

public class UnderBar : UserControl, IComponentConnector
{
	protected static readonly DependencyProperty MessageProperty = DependencyProperty.Register("Message", typeof(string), typeof(UnderBar), new PropertyMetadata("Ready", OnPropertyChanged_Message));

	protected static readonly DependencyProperty PointProperty = DependencyProperty.Register("Point", typeof(Point), typeof(UnderBar), new PropertyMetadata(default(Point), OnPropertyChanged_Point));

	internal UnderBar This;

	internal TextBlock TX_M;

	internal TextBlock TX_X;

	internal TextBlock TX_Y;

	private bool _contentLoaded;

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

	public UnderBar()
	{
		InitializeComponent();
	}

	private static void OnPropertyChanged_Message(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
	}

	private static void OnPropertyChanged_Point(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/SamCAD;component/control/underbar.xaml", UriKind.Relative);
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
			This = (UnderBar)target;
			break;
		case 2:
			TX_M = (TextBlock)target;
			break;
		case 3:
			TX_X = (TextBlock)target;
			break;
		case 4:
			TX_Y = (TextBlock)target;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}
