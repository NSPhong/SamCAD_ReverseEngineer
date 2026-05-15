using System;
using System.ComponentModel;
using System.Windows;
using SamSoarII.Shell;

namespace SamSoarII.Polyline.Entity;

public interface IPolylineEllipse : IPolylineEntity, INotifyPropertyChanged, IDisposable, IGridPenningEntity
{
	Point Center { get; set; }

	bool IsClockwise { get; set; }

	Vector Direction { get; set; }

	double LongRadius { get; set; }

	double ShortRadius { get; set; }

	Point GetCrossPoint(double angle);
}
