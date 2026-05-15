using System.Windows;

namespace SamSoarII.Shell;

public interface IGridPenningEllipseArch : IGridPenningEllipse, IGridPenningEntity
{
	bool IsLarge { get; }

	Point Start { get; }

	Point End { get; }

	double StartAngle { get; }

	double EndAngle { get; }
}
