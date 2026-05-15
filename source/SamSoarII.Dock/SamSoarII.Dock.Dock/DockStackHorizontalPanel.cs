using System;
using System.Windows;
using System.Windows.Controls;
using SamSoarII.Dock.Interface;
using SamSoarII.Dock.View;

namespace SamSoarII.Dock.Dock;

internal class DockStackHorizontalPanel : DockStackPanel
{
	public override Orientation Orientation => Orientation.Horizontal;

	public DockStackHorizontalPanel(DockManager _parent)
		: base(_parent)
	{
	}

	public override void BaseUpdate(int lockid)
	{
		base.BaseUpdate(lockid);
		bool flag = true;
		int num = 0;
		double num2 = 0.0;
		double num3 = 0.0;
		double num4 = 0.0;
		DockStackSeperator dockStackSeperator = null;
		foreach (FrameworkElement viewchild in viewchildren)
		{
			if (!flag)
			{
				dockStackSeperator = new DockStackSeperator(this, num);
				Canvas.SetLeft(dockStackSeperator, num2);
				Canvas.SetTop(dockStackSeperator, 0.0);
				num2 += DockStackSeperator.Thickness;
				num3 += DockStackSeperator.Thickness;
				base.Children.Add(dockStackSeperator);
			}
			Canvas.SetLeft(viewchild, num2);
			Canvas.SetTop(viewchild, 0.0);
			num2 += viewchild.Width;
			num3 += viewchild.MinWidth;
			num4 = Math.Max(num4, viewchild.MinHeight);
			base.Children.Add(viewchild);
			flag = false;
			num++;
		}
		base.MinWidth = num3;
		base.MinHeight = num4;
		WidthUpdate(base.Width, lockid);
		HeightUpdate(base.Height, lockid);
		FrameworkElement fele = this;
		IDockCollection dockCollection = ViewCommon.GetDockCollection(ref fele);
		if (dockCollection is DockStackPanel)
		{
			DockStackPanel dockStackPanel = (DockStackPanel)dockCollection;
			dockStackPanel.BaseUpdate(-1);
		}
	}

	public override void SizeUpdate()
	{
		base.SizeUpdate();
		double num = 0.0;
		foreach (FrameworkElement child in base.Children)
		{
			Canvas.SetLeft(child, num);
			Canvas.SetTop(child, 0.0);
			num = ((!(child is DockStackSeperator)) ? (num + child.Width) : (num + DockStackSeperator.Thickness));
		}
		InfoUpdate();
	}

	protected override void WidthUpdate(double _width, int lockid)
	{
		base.WidthUpdate(_width, lockid);
		FrameworkElement frameworkElement = null;
		double num = 0.0;
		double num2 = _width;
		double num3 = 0.0;
		if (lockid >= 0 && lockid < base.Count && base.Count > 1)
		{
			frameworkElement = viewchildren[lockid];
			if (frameworkElement.Width > _width - base.MinWidth)
			{
				frameworkElement = null;
				lockid = -1;
			}
		}
		else
		{
			frameworkElement = null;
			lockid = -1;
		}
		foreach (FrameworkElement child in base.Children)
		{
			if (child is DockStackSeperator)
			{
				num2 -= DockStackSeperator.Thickness;
			}
			else if (child == frameworkElement)
			{
				num2 -= child.Width;
			}
			else
			{
				num += child.Width;
			}
		}
		if (num > 0.0 && num2 > 0.0)
		{
			foreach (FrameworkElement viewchild in viewchildren)
			{
				if (viewchild != frameworkElement)
				{
					double num4 = viewchild.Width * num2 / num;
					num4 -= num3;
					num3 = 0.0;
					if (num4 < viewchild.MinWidth)
					{
						num3 += viewchild.MinWidth - num4;
						num4 = viewchild.MinWidth;
					}
					viewchild.Width = num4;
				}
			}
			if (num3 > 0.0)
			{
				foreach (FrameworkElement viewchild2 in viewchildren)
				{
					double val = viewchild2.Width - viewchild2.MinWidth;
					val = Math.Min(val, num3);
					num3 -= val;
					viewchild2.Width -= val;
					if (num3 <= 0.0)
					{
						break;
					}
				}
			}
			SizeUpdate();
		}
		InfoUpdate();
	}

	protected override void HeightUpdate(double _height, int lockid)
	{
		base.HeightUpdate(_height, lockid);
		foreach (FrameworkElement child in base.Children)
		{
			child.Height = _height;
		}
		InfoUpdate();
	}
}
