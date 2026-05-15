using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using SamSoarII.Polyline.Entity;

namespace SamCAD.Control;

public class CreateFreeRectWindow : UserControl, IComponentConnector
{
	private IPolylineFreeRect rect;

	internal CreateFreeRectWindow This;

	internal TextBox TX_X;

	internal TextBox TX_Y;

	internal TextBox TX_H;

	internal TextBox TX_HA;

	internal TextBox TX_W;

	internal TextBox TX_WA;

	internal RadioButton RB_Clock;

	internal RadioButton RB_CoClock;

	internal RadioButton RB_Default;

	internal RadioButton RB_Round;

	internal TextBox TX_Round;

	internal RadioButton RB_Bevel;

	internal TextBox TX_Bevel;

	internal Button BN_Yes;

	internal Button BN_No;

	private bool _contentLoaded;

	public IPolylineFreeRect Rect
	{
		get
		{
			double result = rect.From.X;
			double result2 = rect.From.Y;
			Vector vector = rect.P1 - rect.From;
			Vector vector2 = rect.P2 - rect.From;
			double result3 = vector.Length;
			double result4 = Vector.AngleBetween(new Vector(1.0, 0.0), vector);
			double result5 = vector2.Length;
			double num = Vector.CrossProduct(vector, vector2);
			double.TryParse(TX_X.Text, out result);
			double.TryParse(TX_Y.Text, out result2);
			double.TryParse(TX_H.Text, out result3);
			double.TryParse(TX_HA.Text, out result4);
			double.TryParse(TX_W.Text, out result5);
			double num2 = Math.Sin(result4 * Math.PI / 180.0);
			double num3 = Math.Cos(result4 * Math.PI / 180.0);
			double y = ((num >= 0.0) ? num3 : (0.0 - num3));
			double x = ((num >= 0.0) ? (0.0 - num2) : num2);
			vector = new Vector(num3, num2) * result3;
			vector2 = new Vector(x, y) * result5;
			rect.From = new Point(result, result2);
			rect.P1 = rect.From + vector;
			rect.P2 = rect.From + vector2;
			return rect;
		}
		set
		{
			rect = value;
			if (rect != null)
			{
				Vector vector = rect.P1 - rect.From;
				Vector vector2 = rect.P2 - rect.From;
				TX_X.Text = rect.From.X.ToString();
				TX_Y.Text = rect.From.Y.ToString();
				TX_H.Text = vector.Length.ToString();
				TX_W.Text = vector2.Length.ToString();
				TX_HA.Text = Vector.AngleBetween(new Vector(1.0, 0.0), vector).ToString();
				TX_WA.Text = Vector.AngleBetween(new Vector(1.0, 0.0), vector2).ToString();
			}
		}
	}

	public bool IsClockwise => RB_Clock.IsChecked == true;

	public bool IsRound => RB_Round.IsChecked == true;

	public bool IsBevel => RB_Bevel.IsChecked == true;

	public double RoundRadius
	{
		get
		{
			double result = 0.0;
			double.TryParse(TX_Round.Text, out result);
			return result;
		}
	}

	public double BevelRadius
	{
		get
		{
			double result = 0.0;
			double.TryParse(TX_Bevel.Text, out result);
			return result;
		}
	}

	public event RoutedEventHandler Yes;

	public event RoutedEventHandler No;

	public CreateFreeRectWindow()
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
			Uri resourceLocator = new Uri("/SamCAD;component/control/createfreerectwindow.xaml", UriKind.Relative);
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
			This = (CreateFreeRectWindow)target;
			break;
		case 2:
			TX_X = (TextBox)target;
			break;
		case 3:
			TX_Y = (TextBox)target;
			break;
		case 4:
			TX_H = (TextBox)target;
			break;
		case 5:
			TX_HA = (TextBox)target;
			break;
		case 6:
			TX_W = (TextBox)target;
			break;
		case 7:
			TX_WA = (TextBox)target;
			break;
		case 8:
			RB_Clock = (RadioButton)target;
			break;
		case 9:
			RB_CoClock = (RadioButton)target;
			break;
		case 10:
			RB_Default = (RadioButton)target;
			break;
		case 11:
			RB_Round = (RadioButton)target;
			break;
		case 12:
			TX_Round = (TextBox)target;
			break;
		case 13:
			RB_Bevel = (RadioButton)target;
			break;
		case 14:
			TX_Bevel = (TextBox)target;
			break;
		case 15:
			BN_Yes = (Button)target;
			BN_Yes.Click += BN_Yes_Click;
			break;
		case 16:
			BN_No = (Button)target;
			BN_No.Click += BN_No_Click;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}
