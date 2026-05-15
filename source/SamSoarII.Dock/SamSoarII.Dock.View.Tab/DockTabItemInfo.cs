namespace SamSoarII.Dock.View.Tab;

internal class DockTabItemInfo
{
	private DockTabBarInfo parent;

	private DockTabContainer container;

	private double left;

	private double width;

	public DockTabBarInfo Parent => parent;

	public DockTabContainer Container => container;

	public double Left => left;

	public double Width => width;

	public double Right => left + width;

	public DockTabItemInfo(DockTabBarInfo _parent, DockTabContainer _container, double _left, double _width)
	{
		parent = _parent;
		container = _container;
		left = _left;
		width = _width;
	}

	public void Dispose()
	{
		parent = null;
		container = null;
	}

	public bool IsOver(double value)
	{
		return value >= Left && value <= Right;
	}

	public bool IsCloseOver(double value)
	{
		DockTabBarInfo dockTabBarInfo = parent;
		return dockTabBarInfo != null && dockTabBarInfo.Parent?.Theme == DockTab.Themes.SamSoarII && value <= Right - DockTabBarDrawer.ItemMargin && value >= Right - DockTabBarDrawer.ItemMargin - DockTabBarDrawer.CloseWidth;
	}
}
