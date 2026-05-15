using System;
using System.Windows;
using System.Windows.Controls;
using SamSoarII.Dock.Interface;
using SamSoarII.Dock.View;

namespace SamSoarII.Dock.Dock;

internal class DockStackVerticalPanel : DockStackPanel
{
	public override Orientation Orientation => Orientation.Vertical;

	public DockStackVerticalPanel(DockManager _parent)
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
				Canvas.SetTop(dockStackSeperator, num2);
				Canvas.SetLeft(dockStackSeperator, 0.0);
				num2 += DockStackSeperator.Thickness;
				num3 += DockStackSeperator.Thickness;
				base.Children.Add(dockStackSeperator);
			}
			Canvas.SetTop(viewchild, num2);
			Canvas.SetLeft(viewchild, 0.0);
			num2 += viewchild.Height;
			num3 += viewchild.MinHeight;
			num4 = Math.Max(num4, viewchild.MinWidth);
			base.Children.Add(viewchild);
			flag = false;
			num++;
		}
		base.MinWidth = num4;
		base.MinHeight = num3;
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
			Canvas.SetTop(child, num);
			Canvas.SetLeft(child, 0.0);
			num = ((!(child is DockStackSeperator)) ? (num + child.Height) : (num + DockStackSeperator.Thickness));
		}
		InfoUpdate();
	}

	protected override void WidthUpdate(double _width, int lockid)
	{
		base.WidthUpdate(_width, lockid);
		foreach (FrameworkElement child in base.Children)
		{
			child.Width = _width;
		}
		InfoUpdate();
	}

	protected override void HeightUpdate(double _height, int lockid)
	{
		base.HeightUpdate(_height, lockid);
		FrameworkElement frameworkElement = null;
		double num = 0.0;
		double num2 = _height;
		double num3 = 0.0;
		if (lockid >= 0 && lockid < base.Count && base.Count > 1)
		{
			frameworkElement = viewchildren[lockid];
			if (frameworkElement.Height > _height - base.MinHeight)
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
				num2 -= child.Height;
			}
			else
			{
				num += child.Height;
			}
		}
		if (num > 0.0 && num2 > 0.0)
		{
			foreach (FrameworkElement viewchild in viewchildren)
			{
				if (viewchild != frameworkElement)
				{
					double num4 = viewchild.Height * num2 / num;
					num4 -= num3;
					num3 = 0.0;
					if (num4 < viewchild.MinHeight)
					{
						num3 += viewchild.MinHeight - num4;
						num4 = viewchild.MinHeight;
					}
					viewchild.Height = num4;
				}
			}
			if (num3 > 0.0)
			{
				foreach (FrameworkElement viewchild2 in viewchildren)
				{
					double val = viewchild2.Height - viewchild2.MinHeight;
					val = Math.Min(val, num3);
					num3 -= val;
					viewchild2.Height -= val;
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
}
