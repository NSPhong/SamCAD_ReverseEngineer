using System;
using System.ComponentModel;
using System.Windows;
using SamSoarII.Shell;

namespace SamSoarII.Polyline.Entity;

public interface IPolylineRect : IPolylinePolygon, IPolylineEntity, INotifyPropertyChanged, IDisposable, IGridPenningEntity
{
	Rect Rect { get; set; }
}
