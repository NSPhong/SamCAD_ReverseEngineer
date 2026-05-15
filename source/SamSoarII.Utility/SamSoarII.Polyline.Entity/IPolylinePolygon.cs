using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using SamSoarII.Shell;

namespace SamSoarII.Polyline.Entity;

public interface IPolylinePolygon : IPolylineEntity, INotifyPropertyChanged, IDisposable, IGridPenningEntity
{
	IList<Point> Points { get; }

	IList<double> RoundRadius { get; }
}
