namespace SamSoarII.Dock.Interface;

internal interface IDockContainer : IDockView
{
	IDockBaseView ViewContent { get; set; }
}
