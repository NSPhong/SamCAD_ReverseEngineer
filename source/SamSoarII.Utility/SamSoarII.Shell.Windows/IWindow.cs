namespace SamSoarII.Shell.Windows;

public interface IWindow
{
	IInteractionFacade IFParent { get; }

	event IWindowEventHandler Post;
}
