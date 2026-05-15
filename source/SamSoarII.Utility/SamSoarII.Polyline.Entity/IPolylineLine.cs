using System;
using System.ComponentModel;
using SamSoarII.Shell;

namespace SamSoarII.Polyline.Entity;

public interface IPolylineLine : IPolylineEntity, INotifyPropertyChanged, IDisposable, IGridPenningEntity
{
}
