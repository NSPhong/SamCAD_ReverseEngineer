using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Shapes;

namespace SamSoarII.Dock.View;

public class DockDropSuit : UserControl, IComponentConnector
{
	public enum Status
	{
		Null,
		None,
		Top,
		Bottom,
		Left,
		Right,
		CenterTop,
		CenterBottom,
		CenterLeft,
		CenterRight,
		Center
	}

	private Status state;

	internal DockDropSuit UC_This;

	internal Grid GD_Main;

	internal Image IM_Top;

	internal Image IM_Bottom;

	internal Image IM_Left;

	internal Image IM_Right;

	internal Image IM_CPanel;

	internal Image IM_CTop;

	internal Image IM_CBottom;

	internal Image IM_CLeft;

	internal Image IM_CRight;

	internal Image IM_Center;

	internal Grid GD_PlaceIn;

	internal Rectangle RN_PlaceIn;

	internal Grid GD_PlaceOut;

	internal Rectangle RN_PlaceOut;

	private bool _contentLoaded;

	internal FrameworkElement PlaceOut => GD_PlaceOut;

	public Status State => state;

	public DockDropSuit()
	{
		InitializeComponent();
		GD_Main.Children.Remove(GD_PlaceOut);
	}

	public void Reset()
	{
		state = Status.Null;
		RN_PlaceIn.Visibility = Visibility.Hidden;
		RN_PlaceOut.Visibility = Visibility.Hidden;
	}

	public void SetPlaceIn(int row, int rowspan, int column, int columnspan)
	{
		Grid.SetRow(RN_PlaceIn, row);
		Grid.SetRowSpan(RN_PlaceIn, rowspan);
		Grid.SetColumn(RN_PlaceIn, column);
		Grid.SetColumnSpan(RN_PlaceIn, columnspan);
		RN_PlaceIn.Visibility = Visibility.Visible;
		RN_PlaceOut.Visibility = Visibility.Hidden;
	}

	public void SetPlaceOut(int row, int rowspan, int column, int columnspan)
	{
		Grid.SetRow(RN_PlaceOut, row);
		Grid.SetRowSpan(RN_PlaceOut, rowspan);
		Grid.SetColumn(RN_PlaceOut, column);
		Grid.SetColumnSpan(RN_PlaceOut, columnspan);
		RN_PlaceIn.Visibility = Visibility.Hidden;
		RN_PlaceOut.Visibility = Visibility.Visible;
	}

	internal void InvokeMouse(MouseEventArgs e)
	{
		if (_IsMouseOver(IM_Top, e))
		{
			state = Status.Top;
			SetPlaceOut(0, 1, 0, 2);
		}
		else if (_IsMouseOver(IM_Bottom, e))
		{
			state = Status.Bottom;
			SetPlaceOut(1, 1, 0, 2);
		}
		else if (_IsMouseOver(IM_Left, e))
		{
			state = Status.Left;
			SetPlaceOut(0, 2, 0, 1);
		}
		else if (_IsMouseOver(IM_Right, e))
		{
			state = Status.Right;
			SetPlaceOut(0, 2, 1, 1);
		}
		else if (_IsMouseOver(IM_Center, e))
		{
			Point position = e.GetPosition(IM_Center);
			double actualWidth = IM_Center.ActualWidth;
			double actualHeight = IM_Center.ActualHeight;
			if (IM_CTop.Visibility == Visibility.Visible && position.X >= actualWidth * 0.33 && position.X <= actualWidth * 0.66 && position.Y >= 0.0 && position.Y <= actualHeight * 0.33)
			{
				state = Status.CenterTop;
				SetPlaceIn(0, 1, 0, 2);
			}
			else if (IM_CBottom.Visibility == Visibility.Visible && position.X >= actualWidth * 0.33 && position.X <= actualWidth * 0.66 && position.Y >= actualHeight * 0.66 && position.Y <= actualHeight)
			{
				state = Status.CenterBottom;
				SetPlaceIn(1, 1, 0, 2);
			}
			else if (IM_CLeft.Visibility == Visibility.Visible && position.X >= 0.0 && position.X <= actualWidth * 0.33 && position.Y >= actualHeight * 0.33 && position.Y <= actualHeight * 0.66)
			{
				state = Status.CenterLeft;
				SetPlaceIn(0, 2, 0, 1);
			}
			else if (IM_CRight.Visibility == Visibility.Visible && position.X >= actualWidth * 0.66 && position.X <= actualWidth && position.Y >= actualHeight * 0.33 && position.Y <= actualHeight * 0.66)
			{
				state = Status.CenterRight;
				SetPlaceIn(0, 2, 1, 1);
			}
			else if (IM_Center.Visibility == Visibility.Visible && position.X >= actualWidth * 0.33 && position.X <= actualWidth * 0.66 && position.Y >= actualHeight * 0.33 && position.Y <= actualHeight * 0.66)
			{
				state = Status.Center;
				SetPlaceIn(0, 2, 0, 2);
			}
			else
			{
				state = Status.None;
				RN_PlaceIn.Visibility = Visibility.Hidden;
				RN_PlaceOut.Visibility = Visibility.Hidden;
			}
		}
		else
		{
			state = Status.None;
			RN_PlaceIn.Visibility = Visibility.Hidden;
			RN_PlaceOut.Visibility = Visibility.Hidden;
		}
	}

	private bool _IsMouseOver(FrameworkElement fele, MouseEventArgs e)
	{
		if (fele.Visibility != Visibility.Visible)
		{
			return false;
		}
		Point position = e.GetPosition(fele);
		return position.X >= 0.0 && position.Y >= 0.0 && position.X <= fele.ActualWidth && position.Y <= fele.ActualHeight;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/SamSoarII.Dock;component/view/dockdropsuit.xaml", UriKind.Relative);
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
			UC_This = (DockDropSuit)target;
			break;
		case 2:
			GD_Main = (Grid)target;
			break;
		case 3:
			IM_Top = (Image)target;
			break;
		case 4:
			IM_Bottom = (Image)target;
			break;
		case 5:
			IM_Left = (Image)target;
			break;
		case 6:
			IM_Right = (Image)target;
			break;
		case 7:
			IM_CPanel = (Image)target;
			break;
		case 8:
			IM_CTop = (Image)target;
			break;
		case 9:
			IM_CBottom = (Image)target;
			break;
		case 10:
			IM_CLeft = (Image)target;
			break;
		case 11:
			IM_CRight = (Image)target;
			break;
		case 12:
			IM_Center = (Image)target;
			break;
		case 13:
			GD_PlaceIn = (Grid)target;
			break;
		case 14:
			RN_PlaceIn = (Rectangle)target;
			break;
		case 15:
			GD_PlaceOut = (Grid)target;
			break;
		case 16:
			RN_PlaceOut = (Rectangle)target;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}
