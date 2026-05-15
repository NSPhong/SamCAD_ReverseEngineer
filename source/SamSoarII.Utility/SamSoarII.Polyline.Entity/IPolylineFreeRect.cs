using System;
using System.ComponentModel;
using System.Windows;
using SamSoarII.Shell;

namespace SamSoarII.Polyline.Entity;

public interface IPolylineFreeRect : IPolylinePolygon, IPolylineEntity, INotifyPropertyChanged, IDisposable, IGridPenningEntity
{
	Point P1 { get; set; }

	Point P2 { get; set; }
}
