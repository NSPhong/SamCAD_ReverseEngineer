using SamSoarII.Dock.AutoHide;

namespace SamSoarII.Dock.Interface;

internal interface ISideStackPanel : IDockCollection, IDockView
{
	AutoHideCommon.Sides Side { get; }

	ISideStackPanel Next { get; }
}
