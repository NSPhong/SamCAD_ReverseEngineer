using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using SamSoarII.Dock.Interface;

namespace SamSoarII.Dock.Dock;

internal class DockStackSeperator : UserControl, IDisposable, IDockView
{
	public static double Thickness = 6.0;

	private IDockCollection parent;

	private int id;

	private bool isdraging = false;

	private Point lastp = default(Point);

	private Point nextp = default(Point);

	private double movex = 0.0;

	private double movey = 0.0;

	public IDockCollection ViewParent => parent;

	IDockView IDockView.ViewParent => ViewParent;

	public int ID => id;

	public DockManager DockManager
	{
		get
		{
			if (parent is DockStackPanel)
			{
				DockStackPanel dockStackPanel = (DockStackPanel)parent;
				return dockStackPanel.ViewParent;
			}
			return null;
		}
	}

	public Orientation Orientation
	{
		get
		{
			if (parent is DockStackPanel)
			{
				if (parent is DockStackHorizontalPanel)
				{
					return Orientation.Horizontal;
				}
				if (parent is DockStackVerticalPanel)
				{
					return Orientation.Vertical;
				}
			}
			return Orientation.Horizontal;
		}
	}

	public FrameworkElement Last
	{
		get
		{
			if (parent is DockStackPanel)
			{
				DockStackPanel dockStackPanel = (DockStackPanel)parent;
				return (id > 0) ? dockStackPanel.ViewChildren[id - 1] : null;
			}
			return null;
		}
	}

	public FrameworkElement Next
	{
		get
		{
			if (parent is DockStackPanel)
			{
				DockStackPanel dockStackPanel = (DockStackPanel)parent;
				return (id < dockStackPanel.ViewChildren.Count) ? dockStackPanel.ViewChildren[id] : null;
			}
			return null;
		}
	}

	public DockStackSeperator(IDockCollection _parent, int _id)
	{
		parent = _parent;
		id = _id;
		base.MinWidth = Thickness;
		base.MinHeight = Thickness;
		base.Background = new SolidColorBrush(new Color
		{
			A = 1,
			R = byte.MaxValue,
			G = byte.MaxValue,
			B = byte.MaxValue
		});
		if (parent is DockStackPanel)
		{
			if (parent is DockStackHorizontalPanel)
			{
				base.Cursor = Cursors.SizeWE;
			}
			if (parent is DockStackVerticalPanel)
			{
				base.Cursor = Cursors.SizeNS;
			}
		}
	}

	public void Dispose()
	{
		parent = null;
	}

	protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
	{
		base.OnMouseLeftButtonDown(e);
		DockManager.ResizeLine.Start(this);
		lastp = e.GetPosition(DockManager.WindowOwner);
		isdraging = true;
		movex = 0.0;
		movey = 0.0;
		CaptureMouse();
	}

	protected override void OnMouseMove(MouseEventArgs e)
	{
		base.OnMouseMove(e);
		if (isdraging)
		{
			nextp = e.GetPosition(DockManager.WindowOwner);
			movex += nextp.X - lastp.X;
			movey += nextp.Y - lastp.Y;
			if (Orientation == Orientation.Horizontal)
			{
				DockManager.ResizeLine.SetHorizontalOffset(movex);
			}
			if (Orientation == Orientation.Vertical)
			{
				DockManager.ResizeLine.SetVerticalOffset(movey);
			}
			lastp = nextp;
		}
	}

	protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
	{
		base.OnMouseLeftButtonUp(e);
		if (isdraging)
		{
			DockManager.ResizeLine.End();
			ReleaseMouseCapture();
		}
	}
}
