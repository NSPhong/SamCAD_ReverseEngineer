using System;
using System.Collections.Generic;
using System.Linq;
using SamSoarII.Dock.Interface;

namespace SamSoarII.Dock.View.Tab;

internal class DockTabBarInfo : IDisposable
{
	private DockTab parent;

	private int start;

	private int end;

	private List<DockTabItemInfo> infos;

	public DockTab Parent => parent;

	public int Start => start;

	public int End => end;

	public IList<DockTabItemInfo> Infos => infos;

	public DockTabBarInfo(DockTab _parent, int _start, double _maxwidth)
	{
		parent = _parent;
		start = _start;
		infos = new List<DockTabItemInfo>();
		double num = 0.0;
		for (int i = start; i < parent.ViewChildren.Count(); i++)
		{
			if (num + DockTabBarDrawer.ItemMinWidth > _maxwidth)
			{
				break;
			}
			IDockBaseView dockBaseView = parent[i];
			DockTabContainer dockTabContainer = (DockTabContainer)dockBaseView.DockContainer;
			dockTabContainer.HeaderText.MaxTextWidth = Math.Min(DockTabBarDrawer.TextMaxWidth, _maxwidth);
			double num2 = 0.0;
			switch (parent.Theme)
			{
			case DockTab.Themes.SamSoarII:
				num2 += DockTabBarDrawer.IconWidth;
				num2 += DockTabBarDrawer.CloseWidth;
				num2 += DockTabBarDrawer.ItemMargin * 4.0;
				break;
			case DockTab.Themes.Classic:
				num2 += DockTabBarDrawer.ItemMargin * 2.0;
				break;
			}
			num2 += dockTabContainer.HeaderText.Width;
			if (num + num2 > _maxwidth)
			{
				dockTabContainer.HeaderText.MaxTextWidth = dockTabContainer.HeaderText.Width - (num + num2) + _maxwidth;
				num2 -= num + num2 - _maxwidth;
				infos.Add(new DockTabItemInfo(this, dockTabContainer, num, num2));
				break;
			}
			infos.Add(new DockTabItemInfo(this, dockTabContainer, num, num2));
			num += num2;
		}
		end = start + infos.Count() - 1;
	}

	public void Dispose()
	{
		parent = null;
		infos = null;
	}
}
