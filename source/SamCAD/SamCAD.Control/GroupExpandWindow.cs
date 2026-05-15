using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace SamCAD.Control;

public class GroupExpandWindow : UserControl, IComponentConnector
{
	internal TextBox TX_R;

	internal RadioButton RB_Out;

	internal RadioButton RB_In;

	internal Button BN_Yes;

	internal Button BN_No;

	private bool _contentLoaded;

	public double R
	{
		get
		{
			double result = 5.0;
			double.TryParse(TX_R.Text, out result);
			if (RB_In.IsChecked == true)
			{
				result *= -1.0;
			}
			return result;
		}
	}

	public event RoutedEventHandler Yes;

	public event RoutedEventHandler No;

	public GroupExpandWindow()
	{
		InitializeComponent();
	}

	private void BN_Yes_Click(object sender, RoutedEventArgs e)
	{
		this.Yes?.Invoke(this, e);
	}

	private void BN_No_Click(object sender, RoutedEventArgs e)
	{
		this.No?.Invoke(this, e);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/SamCAD;component/control/groupexpandwindow.xaml", UriKind.Relative);
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
			TX_R = (TextBox)target;
			break;
		case 2:
			RB_Out = (RadioButton)target;
			break;
		case 3:
			RB_In = (RadioButton)target;
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
