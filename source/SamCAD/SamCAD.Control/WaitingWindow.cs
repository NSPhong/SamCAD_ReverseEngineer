using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace SamCAD.Control;

public class WaitingWindow : UserControl, IComponentConnector
{
	protected static readonly DependencyProperty MessageProperty = DependencyProperty.Register("Message", typeof(string), typeof(WaitingWindow), new PropertyMetadata(string.Empty, OnPropertyChanged_Message));

	protected static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(double), typeof(WaitingWindow), new PropertyMetadata(0.0, OnPropertyChanged_Value));

	internal WaitingWindow This;

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

	public double Value
	{
		get
		{
			return (double)GetValue(ValueProperty);
		}
		set
		{
			SetValue(ValueProperty, value);
		}
	}

	public WaitingWindow()
	{
		InitializeComponent();
	}

	private static void OnPropertyChanged_Message(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
	}

	private static void OnPropertyChanged_Value(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/SamCAD;component/control/waitingwindow.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	void IComponentConnector.Connect(int connectionId, object target)
	{
		if (connectionId == 1)
		{
			This = (WaitingWindow)target;
		}
		else
		{
			_contentLoaded = true;
		}
	}
}
