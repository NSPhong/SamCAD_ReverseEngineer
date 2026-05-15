using System.Windows.Media;

namespace SamSoarII.Dock.Float;

internal class FloatHeaderDrawer : DrawingVisual
{
	private FloatHeaderContainer parent;

	public FloatHeaderContainer ViewParent => parent;

	public FloatHeaderDrawer(FloatHeaderContainer _parent)
	{
		parent = _parent;
	}
}
