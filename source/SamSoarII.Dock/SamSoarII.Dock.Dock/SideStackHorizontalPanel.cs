using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using SamSoarII.Dock.AutoHide;
using SamSoarII.Dock.Interface;

namespace SamSoarII.Dock.Dock;

internal class SideStackHorizontalPanel : DockStackHorizontalPanel, ISideStackPanel, IDockCollection, IDockView
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
			case AutoHideCommon.Sides.Left:
				result = viewchildren.Last();
				break;
			case AutoHideCommon.Sides.Right:
				result = viewchildren.First();
				break;
			}
			return result;
		}
	}

	public ISideStackPanel Next => (Content is ISideStackPanel) ? ((ISideStackPanel)Content) : null;

	public SideStackHorizontalPanel(DockManager _parent, AutoHideCommon.Sides _side)
		: base(_parent)
	{
		side = _side;
	}

	protected override void WidthUpdate(double _width, int lockid)
	{
		double num = 0.0;
		double num2 = _width;
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
					num += child.Width;
				}
			}
		}
		if (Content != null)
		{
			double minWidth = Content.MinWidth;
			minWidth = Math.Max(minWidth, base.Width / 2.0);
			if (num2 - num < minWidth)
			{
				num3 = minWidth - (num2 - num);
				List<FrameworkElement> list = viewchildren.Where((FrameworkElement v) => v != Content).ToList();
				list.Sort((FrameworkElement fele1, FrameworkElement fele2) => (fele1.Width - fele1.MinWidth).CompareTo(fele2.Width - fele2.MinWidth));
				for (int num4 = 0; num4 < list.Count(); num4++)
				{
					double val = list[num4].Width - list[num4].MinWidth;
					val = Math.Min(val, num3 / (double)(list.Count() - num4));
					for (int num5 = num4; num5 < list.Count(); num5++)
					{
						list[num5].Width -= val;
						num3 -= val;
					}
					if (num3 <= 0.001)
					{
						break;
					}
				}
				Content.Width = Math.Max(Content.MinWidth, minWidth - num3);
			}
			else
			{
				Content.Width = Math.Max(Content.MinWidth, num2 - num);
			}
		}
		SizeUpdate();
	}
}
