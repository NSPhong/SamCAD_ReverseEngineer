using SamSoarII.Dock.Interface;

namespace SamSoarII.Dock.View.ToolBar;

internal class DockToolBarView : DockBaseView
{
	public DockToolBarView(DockManager _parent, IDockContent _content)
		: base(_parent, _content, ViewCommon.BaseViewTypes.Toolbar)
	{
	}
}
