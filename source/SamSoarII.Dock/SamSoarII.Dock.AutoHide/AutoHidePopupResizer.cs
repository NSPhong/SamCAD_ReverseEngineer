using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using SamSoarII.Dock.Interface;

namespace SamSoarII.Dock.AutoHide;

internal class AutoHidePopupResizer : UserControl, IDockView
{
	private AutoHidePopup parent;

	private AutoHidePopupResizeLine resizeline;

	private bool ismoving = false;

	private Point lastp = default(Point);

	private Point nextp = default(Point);

	private double movex = 0.0;

	private double movey = 0.0;

	private double _movex = 0.0;

	private double _movey = 0.0;

	public AutoHidePopup ViewParent => parent;

	IDockView IDockView.ViewParent => ViewParent;

	public DockManager DockManager => ViewParent?.ViewParent;

	public bool IsMoving => ismoving;

	public AutoHidePopupResizer(AutoHidePopup _parent)
	{
		parent = _parent;
		resizeline = new AutoHidePopupResizeLine(this);
		base.Background = new SolidColorBrush(new Color
		{
			A = 1,
			R = byte.MaxValue,
			G = byte.MaxValue,
			B = byte.MaxValue
		});
		base.MinWidth = 8.0;
		base.MinHeight = 8.0;
	}

	protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
	{
		base.OnMouseLeftButtonDown(e);
		AutoHideCommon.Sides side = ViewParent.Side;
		ismoving = true;
		lastp = e.GetPosition(DockManager.WindowOwner);
		movex = (_movex = 0.0);
		movey = (_movey = 0.0);
		resizeline.Start();
		CaptureMouse();
	}

	protected override void OnMouseMove(MouseEventArgs e)
	{
		base.OnMouseMove(e);
		if (ismoving)
		{
			nextp = e.GetPosition(DockManager.WindowOwner);
			switch (ViewParent.Side)
			{
			case AutoHideCommon.Sides.Left:
				_movex += nextp.X - lastp.X;
				movex = Math.Max(_movex, ViewParent.MinWidth - ViewParent.Width);
				resizeline.HorizontalOffset = movex;
				break;
			case AutoHideCommon.Sides.Right:
				_movex += nextp.X - lastp.X;
				movex = Math.Min(_movex, ViewParent.Width - ViewParent.MinWidth);
				resizeline.HorizontalOffset = movex;
				break;
			case AutoHideCommon.Sides.Top:
				_movey += nextp.Y - lastp.Y;
				movey = Math.Max(_movey, ViewParent.MinHeight - ViewParent.Height);
				resizeline.VerticalOffset = movey;
				break;
			case AutoHideCommon.Sides.Bottom:
				_movey += nextp.Y - lastp.Y;
				movey = Math.Min(_movey, ViewParent.Height - ViewParent.MinHeight);
				resizeline.VerticalOffset = movey;
				break;
			}
			lastp = nextp;
		}
	}

	protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
	{
		base.OnMouseLeftButtonUp(e);
		ReleaseMouseCapture();
		resizeline.End();
		AutoHideCommon.Sides side = ViewParent.Side;
		AutoHideCommon.Sides sides = side;
		if (sides == AutoHideCommon.Sides.Top || sides == AutoHideCommon.Sides.Left)
		{
			ViewParent.Width += movex;
			ViewParent.Height += movey;
		}
		else
		{
			ViewParent.Width -= movex;
			ViewParent.Height -= movey;
		}
		ismoving = false;
	}
}
