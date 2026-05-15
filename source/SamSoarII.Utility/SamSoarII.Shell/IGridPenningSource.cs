using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace SamSoarII.Shell;

public interface IGridPenningSource : INotifyPropertyChanged
{
	double XStart { get; }

	double YStart { get; }

	double XLength { get; }

	double YLength { get; }

	string XUnit { get; }

	string YUnit { get; }

	IList<string> XUnitEx { get; }

	IList<string> YUnitEx { get; }

	IList<int> XValueBase { get; }

	IList<int> YValueBase { get; }

	IEnumerable<IGridPenningEntity> GetEntities(Rect rect);
}
