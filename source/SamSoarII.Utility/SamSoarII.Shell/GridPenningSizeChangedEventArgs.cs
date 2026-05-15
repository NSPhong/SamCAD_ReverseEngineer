using System.Windows;

namespace SamSoarII.Shell;

public class GridPenningSizeChangedEventArgs : IGridPenningSizeChangedEventArgs
{
	public Rect OldSize { get; private set; }

	public Rect NewSize { get; private set; }

	public GridPenningSizeChangedEventArgs(Rect _oldsize, Rect _newsize)
	{
		OldSize = _oldsize;
		NewSize = _newsize;
	}
}
