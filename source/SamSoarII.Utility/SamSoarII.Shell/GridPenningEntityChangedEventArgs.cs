using System.Collections.Generic;
using System.Windows;

namespace SamSoarII.Shell;

public class GridPenningEntityChangedEventArgs : IGridPenningEntityChangedEventArgs
{
	public Rect ChangedZone { get; private set; }

	public IList<IGridPenningEntity> Entities { get; private set; }

	public GridPenningEntityChangedAction Action { get; private set; }

	public GridPenningEntityChangedEventArgs(Rect _changedzone, IList<IGridPenningEntity> _entities, GridPenningEntityChangedAction _action)
	{
		ChangedZone = _changedzone;
		Entities = _entities;
		Action = _action;
	}
}
