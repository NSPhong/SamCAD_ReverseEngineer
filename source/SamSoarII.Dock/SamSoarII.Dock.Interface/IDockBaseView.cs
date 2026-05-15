using SamSoarII.Dock.View;

namespace SamSoarII.Dock.Interface;

internal interface IDockBaseView : IUserFocus, IDockView
{
	DockManager DockManager { get; }

	IDockContent DockContent { get; }

	IDockContainer DockContainer { get; set; }

	ViewCommon.BaseViewTypes Type { get; }

	ViewCommon.HeaderTypes HeaderType { get; }
}
