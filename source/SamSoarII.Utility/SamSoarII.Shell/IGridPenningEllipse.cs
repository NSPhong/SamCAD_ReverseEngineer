using System.Windows;

namespace SamSoarII.Shell;

public interface IGridPenningEllipse : IGridPenningEntity
{
	Point Center { get; }

	Vector Direction { get; }

	double LongRadius { get; }

	double ShortRadius { get; }

	bool Clockwise { get; }
}
