using System.ComponentModel;

namespace SamSoarII.Shell;

public interface IGridPenningSourceEX : IGridPenningSource, INotifyPropertyChanged
{
	bool IsScanXEnabled { get; }

	bool IsScanYEnabled { get; }

	double ScanX { get; }

	double ScanY { get; }

	event GridPenningEntityChangedEventHandler EntityChanged;

	event GridPenningSizeChangedEventHandler SizeChanged;

	string GetXUnit(double xvalue);

	string GetYUnit(double yvalue);
}
