using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using SamSoarII.Polyline.Entity;

namespace SamCAD.Control;

public class CreateEllipseWindow : UserControl, IComponentConnector
{
	private IPolylineEllipse ellipse;

	internal TextBox TX_X;

	internal TextBox TX_Y;

	internal TextBox TX_R1;

	internal TextBox TX_A1;

	internal TextBox TX_R2;

	internal TextBox TX_A2;

	internal RadioButton RB_Clock;

	internal RadioButton RB_CoClock;

	internal Button BN_Yes;

	internal Button BN_No;

	private bool _contentLoaded;

	public IPolylineEllipse Ellipse
	{
		get
		{
			double result = ellipse.Center.X;
			double result2 = ellipse.Center.Y;
			double result3 = ellipse.LongRadius;
			double result4 = Vector.AngleBetween(new Vector(1.0, 0.0), ellipse.Direction);
			double result5 = ellipse.ShortRadius;
			double.TryParse(TX_X.Text, out result);
			double.TryParse(TX_Y.Text, out result2);
			double.TryParse(TX_R1.Text, out result3);
			double.TryParse(TX_A1.Text, out result4);
			double.TryParse(TX_R2.Text, out result5);
			ellipse.Center = new Point(result, result2);
			ellipse.Direction = new Vector(Math.Cos(result4 * Math.PI / 180.0), Math.Sin(result4 * Math.PI / 180.0));
			ellipse.LongRadius = result3;
			ellipse.ShortRadius = result5;
			ellipse.IsClockwise = RB_Clock.IsChecked == true;
			return ellipse;
		}
		set
		{
			ellipse = value;
			if (ellipse != null)
			{
				double num = Vector.AngleBetween(new Vector(1.0, 0.0), ellipse.Direction);
				TX_X.Text = ellipse.Center.X.ToString();
				TX_Y.Text = ellipse.Center.Y.ToString();
				TX_R1.Text = ellipse.LongRadius.ToString();
				TX_R2.Text = ellipse.ShortRadius.ToString();
				TX_A1.Text = num.ToString();
				TX_A2.Text = (num + 90.0).ToString();
				RB_Clock.IsChecked = ellipse.IsClockwise;
				RB_CoClock.IsChecked = !ellipse.IsClockwise;
			}
		}
	}

	public event RoutedEventHandler Yes;

	public event RoutedEventHandler No;

	public CreateEllipseWindow()
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
			Uri resourceLocator = new Uri("/SamCAD;component/control/createellipsewindow.xaml", UriKind.Relative);
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
			TX_R1 = (TextBox)target;
			break;
		case 4:
			TX_A1 = (TextBox)target;
			break;
		case 5:
			TX_R2 = (TextBox)target;
			break;
		case 6:
			TX_A2 = (TextBox)target;
			break;
		case 7:
			RB_Clock = (RadioButton)target;
			break;
		case 8:
			RB_CoClock = (RadioButton)target;
			break;
		case 9:
			BN_Yes = (Button)target;
			BN_Yes.Click += BN_Yes_Click;
			break;
		case 10:
			BN_No = (Button)target;
			BN_No.Click += BN_No_Click;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}
