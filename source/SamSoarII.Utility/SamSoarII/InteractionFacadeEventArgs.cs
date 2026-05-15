using SamSoarII.Shell.Windows;

namespace SamSoarII;

public class InteractionFacadeEventArgs : IWindowEventArgs
{
	public enum Types
	{
		DiagramModified,
		FuncBlockModified,
		DeviceModified,
		ProjectPropertyChanged
	}

	private Types flags;

	private object targetedobject;

	private object relativeobject;

	public Types Flags => flags;

	int IWindowEventArgs.Flags => (int)Flags;

	object IWindowEventArgs.TargetedObject => targetedobject;

	object IWindowEventArgs.RelativeObject => relativeobject;

	public InteractionFacadeEventArgs(Types _flags, object _targetedobject, object _relativeobject)
	{
		flags = _flags;
		targetedobject = _targetedobject;
		relativeobject = _relativeobject;
	}
}
