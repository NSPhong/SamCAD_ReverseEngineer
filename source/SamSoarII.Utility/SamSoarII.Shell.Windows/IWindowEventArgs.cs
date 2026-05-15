namespace SamSoarII.Shell.Windows;

public interface IWindowEventArgs
{
	int Flags { get; }

	object TargetedObject { get; }

	object RelativeObject { get; }
}
