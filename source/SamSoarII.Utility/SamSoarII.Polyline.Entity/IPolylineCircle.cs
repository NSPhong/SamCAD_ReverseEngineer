using System;
using System.ComponentModel;
using System.Windows;
using SamSoarII.Shell;

namespace SamSoarII.Polyline.Entity;

public interface IPolylineCircle : IPolylineEntity, INotifyPropertyChanged, IDisposable, IGridPenningEntity
{
	Point Center { get; set; }

	bool IsClockwise { get; set; }

	double Radius { get; set; }

	void CenterRadius(double _radius);

	void CenterAngle(double _angle);

	void CenterRadiusAngle(double _radius, double _angle);
}
