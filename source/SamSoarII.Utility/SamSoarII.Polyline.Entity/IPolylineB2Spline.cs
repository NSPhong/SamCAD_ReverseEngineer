using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using SamSoarII.Shell;

namespace SamSoarII.Polyline.Entity;

public interface IPolylineB2Spline : IPolylineEntity, INotifyPropertyChanged, IDisposable, IGridPenningEntity
{
	IList<Point> Points { get; }

	IList<bool> Locks { get; }

	void Add();

	void TryMove(int index, ref Point p);
}
