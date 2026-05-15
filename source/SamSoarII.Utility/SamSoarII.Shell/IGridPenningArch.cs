using System.Windows;

namespace SamSoarII.Shell;

public interface IGridPenningArch : IGridPenningCircle, IGridPenningEntity
{
	Point Start { get; }

	Point End { get; }

	double StartAngle { get; }

	double EndAngle { get; }

	bool IsLarge { get; }
}
