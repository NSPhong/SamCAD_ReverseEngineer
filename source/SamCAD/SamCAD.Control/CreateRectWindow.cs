using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace SamCAD.Control;

public class CreateRectWindow : UserControl, IComponentConnector
{
	private Rect oldrect;

	internal TextBox TX_Left;

	internal TextBox TX_Top;

	internal TextBox TX_Right;

	internal TextBox TX_Bottom;

	internal RadioButton RB_Clock;

	internal RadioButton RB_CoClock;

	internal Button BN_Yes;

	internal Button BN_No;

	private bool _contentLoaded;

	public Rect OldRect
	{
		get
		{
			return oldrect;
		}
		set
		{
			oldrect = value;
			TX_Top.Text = oldrect.Top.ToString();
			TX_Left.Text = oldrect.Left.ToString();
			TX_Right.Text = oldrect.Right.ToString();
			TX_Bottom.Text = oldrect.Bottom.ToString();
		}
	}

	public Rect NewRect
	{
		get
		{
			double result = oldrect.Top;
			double result2 = oldrect.Left;
			double result3 = oldrect.Bottom;
			double result4 = oldrect.Right;
			double.TryParse(TX_Top.Text, out result);
			double.TryParse(TX_Left.Text, out result2);
			double.TryParse(TX_Bottom.Text, out result3);
			double.TryParse(TX_Right.Text, out result4);
			return new Rect(result2, result, result4 - result2, result3 - result);
		}
	}

	public bool IsClockwise => RB_Clock.IsChecked == true;

	public event RoutedEventHandler Yes;

	public event RoutedEventHandler No;

	public CreateRectWindow()
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
			Uri resourceLocator = new Uri("/SamCAD;component/control/createrectwindow.xaml", UriKind.Relative);
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
			TX_Left = (TextBox)target;
			break;
		case 2:
			TX_Top = (TextBox)target;
			break;
		case 3:
			TX_Right = (TextBox)target;
			break;
		case 4:
			TX_Bottom = (TextBox)target;
			break;
		case 5:
			RB_Clock = (RadioButton)target;
			break;
		case 6:
			RB_CoClock = (RadioButton)target;
			break;
		case 7:
			BN_Yes = (Button)target;
			BN_Yes.Click += BN_Yes_Click;
			break;
		case 8:
			BN_No = (Button)target;
			BN_No.Click += BN_No_Click;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}
