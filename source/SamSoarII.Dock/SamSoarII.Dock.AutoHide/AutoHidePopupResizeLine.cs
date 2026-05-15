using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SamSoarII.Dock.Interface;

namespace SamSoarII.Dock.AutoHide;

internal class AutoHidePopupResizeLine : UserControl, IDockView
{
	private AutoHidePopupResizer parent;

	private UserControl rect;

	private double hstart;

	private double vstart;

	private double hoffset;

	private double voffset;

	public AutoHidePopupResizer ViewParent => parent;

	IDockView IDockView.ViewParent => ViewParent;

	public DockManager DockManager => ViewParent?.DockManager;

	public Canvas ResizeCanvas => DockManager.CV_Resize;

	public double HorizontalOffset
	{
		get
		{
			return hoffset;
		}
		set
		{
			hoffset = value;
			Canvas.SetLeft(this, hstart + hoffset);
		}
	}

	public double VerticalOffset
	{
		get
		{
			return voffset;
		}
		set
		{
			voffset = value;
			Canvas.SetTop(this, vstart + voffset);
		}
	}

	public AutoHidePopupResizeLine(AutoHidePopupResizer _parent)
	{
		parent = _parent;
		base.Background = new SolidColorBrush(new Color
		{
			A = 96,
			R = 0,
			G = 0,
			B = 0
		});
		base.Visibility = Visibility.Hidden;
		ResizeCanvas.Children.Add(this);
	}

	public void Start()
	{
		Point point = parent.TranslatePoint(new Point(0.0, 0.0), ResizeCanvas);
		hstart = point.X;
		vstart = point.Y;
		base.Width = parent.ActualWidth;
		base.Height = parent.ActualHeight;
		VerticalOffset = 0.0;
		HorizontalOffset = 0.0;
		base.Visibility = Visibility.Visible;
	}

	public void End()
	{
		base.Visibility = Visibility.Hidden;
	}
}
