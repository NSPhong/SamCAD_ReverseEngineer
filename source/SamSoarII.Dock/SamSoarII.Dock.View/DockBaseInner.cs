using System.Windows.Controls;
using SamSoarII.Dock.Interface;

namespace SamSoarII.Dock.View;

internal class DockBaseInner : ContentControl
{
	private IDockBaseView parent;

	public IDockBaseView ViewParent => parent;

	public DockBaseInner(IDockBaseView _parent)
	{
		parent = _parent;
		Panel.SetZIndex(this, 2);
	}
}
