using System;
using System.ComponentModel;
using SamSoarII.Shell;

namespace SamSoarII.Polyline.Entity;

public interface IPolylineEllipseArch : IPolylineEllipse, IPolylineEntity, INotifyPropertyChanged, IDisposable, IGridPenningEntity
{
	bool IsLarge { get; set; }

	bool CenterFrom();

	bool CenterTo();

	bool RadiusFrom();

	bool RadiusTo();

	bool RadiusAngle(double angle);
}
