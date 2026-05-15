using System;
using System.ComponentModel;
using SamSoarII.Shell;

namespace SamSoarII.Polyline.Entity;

public interface IPolylineArch : IPolylineCircle, IPolylineEntity, INotifyPropertyChanged, IDisposable, IGridPenningEntity
{
	bool IsLarge { get; set; }

	void CenterFrom();

	void CenterTo();
}
