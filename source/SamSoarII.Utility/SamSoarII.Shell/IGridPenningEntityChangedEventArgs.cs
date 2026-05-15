using System.Collections.Generic;
using System.Windows;

namespace SamSoarII.Shell;

public interface IGridPenningEntityChangedEventArgs
{
	Rect ChangedZone { get; }

	IList<IGridPenningEntity> Entities { get; }

	GridPenningEntityChangedAction Action { get; }
}
