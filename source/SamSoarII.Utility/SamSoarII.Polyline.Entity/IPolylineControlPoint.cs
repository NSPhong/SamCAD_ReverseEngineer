using System.Windows;

namespace SamSoarII.Polyline.Entity;

public interface IPolylineControlPoint
{
	IPolylineEntity Parent { get; }

	int ID { get; }

	Point Point { get; set; }

	IPolylineControlPointView View { get; set; }
}
