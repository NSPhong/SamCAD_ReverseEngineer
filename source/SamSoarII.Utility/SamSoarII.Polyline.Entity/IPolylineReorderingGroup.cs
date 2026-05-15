using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using SamSoarII.Shell;

namespace SamSoarII.Polyline.Entity;

public interface IPolylineReorderingGroup : IPolylineGroup, IPolylineEntity, INotifyPropertyChanged, IDisposable, IGridPenningEntity, IEnumerable<IPolylineEntity>, IEnumerable
{
	IPolylineGroup Core { get; }

	int OldGID { get; }

	int NewGID { get; set; }

	bool IsMoved { get; set; }

	FrameworkElement View { get; set; }
}
