namespace SamSoarII.Shell.Windows;

public class MainTabControlEventArgs : IWindowEventArgs
{
	private TabAction action;

	private ITabItem tab;

	public TabAction Action => action;

	public object Tab => tab;

	int IWindowEventArgs.Flags => (int)action;

	object IWindowEventArgs.RelativeObject => tab;

	object IWindowEventArgs.TargetedObject => null;

	public MainTabControlEventArgs(TabAction _action, ITabItem _tab)
	{
		action = _action;
		tab = _tab;
	}
}
