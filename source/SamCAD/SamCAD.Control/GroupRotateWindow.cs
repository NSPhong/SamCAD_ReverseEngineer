using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace SamCAD.Control;

public class GroupRotateWindow : UserControl, IComponentConnector
{
	public RoutedEventHandler Yes;

	public RoutedEventHandler No;

	internal TextBox TX_X;

	internal TextBox TX_Y;

	internal TextBox TX_A;

	internal Button BN_Yes;

	internal Button BN_No;

	private bool _contentLoaded;

	public Point Start
	{
		get
		{
			double result = 0.0;
			double result2 = 0.0;
			double.TryParse(TX_X.Text, out result);
			double.TryParse(TX_Y.Text, out result2);
			return new Point(result, result2);
		}
	}

	public double Angle
	{
		get
		{
			double result = 0.0;
			double.TryParse(TX_A.Text, out result);
			return result;
		}
	}

	public GroupRotateWindow()
	{
		InitializeComponent();
	}

	private void BN_Yes_Click(object sender, RoutedEventArgs e)
	{
		Yes?.Invoke(this, e);
	}

	private void BN_No_Click(object sender, RoutedEventArgs e)
	{
		No?.Invoke(this, e);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/SamCAD;component/control/grouprotatewindow.xaml", UriKind.Relative);
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
			TX_X = (TextBox)target;
			break;
		case 2:
			TX_Y = (TextBox)target;
			break;
		case 3:
			TX_A = (TextBox)target;
			break;
		case 4:
			BN_Yes = (Button)target;
			BN_Yes.Click += BN_Yes_Click;
			break;
		case 5:
			BN_No = (Button)target;
			BN_No.Click += BN_No_Click;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}
