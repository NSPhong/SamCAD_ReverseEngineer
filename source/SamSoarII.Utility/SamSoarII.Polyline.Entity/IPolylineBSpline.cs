using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using SamSoarII.Shell;

namespace SamSoarII.Polyline.Entity;

public interface IPolylineBSpline : IPolylineEntity, INotifyPropertyChanged, IDisposable, IGridPenningEntity
{
	IList<Point> Points { get; }

	IList<Point> Nodes { get; }

	IList<double> Offsets { get; }

	IList<double> Weights { get; }
}
