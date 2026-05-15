using System.Windows;

namespace SamSoarII.Shell;

public interface IGridPenningLine : IGridPenningEntity
{
	Point Start { get; }

	Point End { get; }

	bool IsReal { get; }
}
