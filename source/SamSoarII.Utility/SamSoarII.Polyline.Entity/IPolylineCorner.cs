using System;
using System.ComponentModel;
using System.Windows;
using SamSoarII.Shell;

namespace SamSoarII.Polyline.Entity;

public interface IPolylineCorner : IPolylineEntity, INotifyPropertyChanged, IDisposable, IGridPenningEntity
{
	Point Corner { get; set; }
}
