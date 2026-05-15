using System.Collections.Generic;
using System.Windows;

namespace SamSoarII.Polyline.Entity;

public interface IPolylineAction
{
	int Index { get; }

	IList<IPolylineEntity> RemovedItems { get; }

	IList<IPolylineEntity> AddedItems { get; }

	ChangedTarget Target { get; }

	ChangedFlags Flag { get; }

	string Message { get; set; }

	object OldValue { get; }

	object NewValue { get; }

	bool OldBool { get; }

	bool NewBool { get; }

	double OldDouble { get; }

	double NewDouble { get; }

	int OldInt { get; }

	int NewInt { get; }

	Point OldPoint { get; }

	Point NewPoint { get; }
}
