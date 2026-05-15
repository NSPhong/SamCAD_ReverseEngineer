using System.Collections.Generic;
using System.Windows;
using SamSoarII.Polyline.Entity;

namespace SamSoarII.Polyline.Expands;

public interface IPolylineExpand
{
	IPolylineImage Parent { get; set; }

	int ID { get; set; }

	double AngleFrom { get; set; }

	double AngleTo { get; set; }

	IPolylineEntity ToEntity(Point _from, IPolylineExpand _that);

	Point? GetConnect(Point _from, IPolylineExpand _that);

	void GetYs(double _x, List<IPolylineExpandHit> _hits);

	void GetXs(double _y, List<IPolylineExpandHit> _hits);
}
