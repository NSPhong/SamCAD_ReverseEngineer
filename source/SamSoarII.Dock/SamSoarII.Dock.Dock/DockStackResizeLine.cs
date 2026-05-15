using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using SamSoarII.Dock.Interface;

namespace SamSoarII.Dock.Dock;

internal class DockStackResizeLine : Popup, IDockView
{
	public enum Sides
	{
		Left,
		Right,
		Top,
		Bottom
	}

	public static readonly double Thickness = 8.0;

	private DockManager parent;

	private UserControl rect;

	private DockStackSeperator target;

	private double xmin = 0.0;

	private double xmax = 0.0;

	private double ymin = 0.0;

	private double ymax = 0.0;

	private double hoffset = 0.0;

	private double voffset = 0.0;

	public DockManager ViewParent => parent;

	IDockView IDockView.ViewParent => ViewParent;

	public DockStackSeperator Target => target;

	public DockStackResizeLine(DockManager _parent)
	{
		parent = _parent;
		rect = new UserControl();
		rect.Background = new SolidColorBrush(new Color
		{
			A = 96,
			R = 0,
			G = 0,
			B = 0
		});
		base.Child = rect;
		base.AllowsTransparency = true;
	}

	public void Start(DockStackSeperator _target)
	{
		if (target != null)
		{
			return;
		}
		target = _target;
		base.Placement = PlacementMode.Relative;
		base.PlacementTarget = target;
		base.VerticalOffset = (hoffset = 0.0);
		base.HorizontalOffset = (voffset = 0.0);
		base.Width = target.ActualWidth;
		base.Height = target.ActualHeight;
		rect.Width = base.Width;
		rect.Height = base.Height;
		if (target.Orientation == Orientation.Horizontal)
		{
			xmin = ((target.Last == null) ? 0.0 : (target.Last.MinWidth - target.Last.ActualWidth));
			xmax = ((target.Next == null) ? 0.0 : (target.Next.ActualWidth - target.Next.MinWidth));
			if (target.Next != null)
			{
				xmin = Math.Max(xmin, target.Next.ActualWidth - target.Next.MaxWidth);
			}
			if (target.Last != null)
			{
				xmax = Math.Min(xmax, target.Last.MaxWidth - target.Last.ActualWidth);
			}
		}
		if (target.Orientation == Orientation.Vertical)
		{
			ymin = ((target.Last == null) ? 0.0 : (target.Last.MinHeight - target.Last.ActualHeight));
			ymax = ((target.Next == null) ? 0.0 : (target.Next.ActualHeight - target.Next.MinHeight));
			if (target.Next != null)
			{
				ymin = Math.Max(ymin, target.Next.ActualHeight - target.Next.MaxHeight);
			}
			if (target.Last != null)
			{
				ymax = Math.Min(ymax, target.Last.MaxHeight - target.Last.ActualHeight);
			}
		}
		base.IsOpen = true;
	}

	public void End()
	{
		if (target == null)
		{
			return;
		}
		if (target.Last != null)
		{
			if (target.Orientation == Orientation.Horizontal)
			{
				target.Last.Width = target.Last.ActualWidth + hoffset;
			}
			if (target.Orientation == Orientation.Vertical)
			{
				target.Last.Height = target.Last.ActualHeight + voffset;
			}
		}
		if (target.Next != null)
		{
			if (target.Orientation == Orientation.Horizontal)
			{
				target.Next.Width = target.Next.ActualWidth - hoffset;
			}
			if (target.Orientation == Orientation.Vertical)
			{
				target.Next.Height = target.Next.ActualHeight - voffset;
			}
		}
		if (target.ViewParent is DockStackPanel)
		{
			((DockStackPanel)target.ViewParent).SizeUpdate();
		}
		target = null;
		base.Placement = PlacementMode.Absolute;
		base.PlacementTarget = null;
		base.IsOpen = false;
	}

	public void SetHorizontalOffset(double _hoffset)
	{
		_hoffset = Math.Max(_hoffset, xmin);
		_hoffset = Math.Min(_hoffset, xmax);
		base.HorizontalOffset = (hoffset = _hoffset);
	}

	public void SetVerticalOffset(double _voffset)
	{
		_voffset = Math.Max(_voffset, ymin);
		_voffset = Math.Min(_voffset, ymax);
		base.VerticalOffset = (voffset = _voffset);
	}
}
