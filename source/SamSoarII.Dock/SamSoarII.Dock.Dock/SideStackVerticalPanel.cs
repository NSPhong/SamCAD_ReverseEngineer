using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using SamSoarII.Dock.AutoHide;
using SamSoarII.Dock.Interface;

namespace SamSoarII.Dock.Dock;

internal class SideStackVerticalPanel : DockStackVerticalPanel, ISideStackPanel, IDockCollection, IDockView
{
	private AutoHideCommon.Sides side;

	public AutoHideCommon.Sides Side => side;

	public FrameworkElement Content
	{
		get
		{
			if (base.Count <= 0)
			{
				return null;
			}
			FrameworkElement result = null;
			switch (side)
			{
			case AutoHideCommon.Sides.Top:
				result = viewchildren.Last();
				break;
			case AutoHideCommon.Sides.Bottom:
				result = viewchildren.First();
				break;
			}
			return result;
		}
	}

	public ISideStackPanel Next => (Content is ISideStackPanel) ? ((ISideStackPanel)Content) : null;

	public SideStackVerticalPanel(DockManager _parent, AutoHideCommon.Sides _side)
		: base(_parent)
	{
		side = _side;
	}

	protected override void HeightUpdate(double _height, int lockid)
	{
		double num = 0.0;
		double num2 = _height;
		double num3 = 0.0;
		foreach (FrameworkElement child in base.Children)
		{
			if (child != Content)
			{
				if (child is DockStackSeperator)
				{
					num2 -= DockStackSeperator.Thickness;
				}
				else
				{
					num += child.Height;
				}
			}
		}
		if (Content != null)
		{
			double minHeight = Content.MinHeight;
			minHeight = Math.Max(minHeight, base.Height / 2.0);
			if (num2 - num < minHeight)
			{
				num3 = minHeight - (num2 - num);
				List<FrameworkElement> list = viewchildren.Where((FrameworkElement v) => v != Content).ToList();
				list.Sort((FrameworkElement fele1, FrameworkElement fele2) => (fele1.Height - fele1.MinHeight).CompareTo(fele2.Height - fele2.MinHeight));
				for (int num4 = 0; num4 < list.Count(); num4++)
				{
					double val = list[num4].Height - list[num4].MinHeight;
					val = Math.Min(val, num3 / (double)(list.Count() - num4));
					for (int num5 = num4; num5 < list.Count(); num5++)
					{
						list[num5].Height -= val;
						num3 -= val;
					}
					if (num3 <= 0.001)
					{
						break;
					}
				}
				Content.Height = Math.Max(Content.MinHeight, minHeight - num3);
			}
			else
			{
				Content.Height = Math.Max(Content.MinHeight, num2 - num);
			}
		}
		SizeUpdate();
	}
}
