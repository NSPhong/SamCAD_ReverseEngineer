using System.Windows;

namespace SamSoarII.Shell;

public interface IGridPenningPolylinePath
{
	IGridPenningLine GetLines(Rect dwrect);
}
