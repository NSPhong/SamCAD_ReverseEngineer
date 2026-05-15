using System.Windows;

namespace SamSoarII.Shell;

public class GridPenningLine : GridPenningEntity, IGridPenningLine, IGridPenningEntity
{
	private Point start;

	private Point end;

	private bool isreal;

	public Point Start => start;

	public Point End => end;

	public bool IsReal => isreal;

	public GridPenningLine(Point _start, Point _end, bool _isreal = true)
	{
		base.ColorID = 0;
		start = _start;
		end = _end;
		isreal = _isreal;
	}
}
