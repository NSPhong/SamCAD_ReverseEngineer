using System.Windows.Controls;
using System.Windows.Media;

namespace SamSoarII.Dock.Float;

internal class FloatHeaderContainer : Panel
{
	private FloatStackPanel parent;

	private FloatHeaderDrawer drawer;

	public FloatStackPanel ViewParent => parent;

	public FloatHeaderDrawer Drawer => drawer;

	protected override int VisualChildrenCount => (drawer != null) ? 1 : 0;

	public FloatHeaderContainer(FloatStackPanel _parent)
	{
		Panel.SetZIndex(this, 1);
		parent = _parent;
		drawer = new FloatHeaderDrawer(this);
		AddLogicalChild(drawer);
		AddVisualChild(drawer);
	}

	protected override Visual GetVisualChild(int index)
	{
		return drawer;
	}
}
