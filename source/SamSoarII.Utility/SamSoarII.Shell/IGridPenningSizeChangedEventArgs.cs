using System.Windows;

namespace SamSoarII.Shell;

public interface IGridPenningSizeChangedEventArgs
{
	Rect OldSize { get; }

	Rect NewSize { get; }
}
