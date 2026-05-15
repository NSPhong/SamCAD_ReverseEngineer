using System.Windows;

namespace SamSoarII.Shell;

public interface IGridPenningCircle : IGridPenningEntity
{
	Point Center { get; }

	double Radius { get; }

	bool Clockwise { get; }
}
