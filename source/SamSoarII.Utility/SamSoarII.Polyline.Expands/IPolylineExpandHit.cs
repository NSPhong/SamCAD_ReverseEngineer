using System.Windows;

namespace SamSoarII.Polyline.Expands;

public interface IPolylineExpandHit
{
	IPolylineExpand Parent { get; set; }

	int ID { get; set; }

	Point P { get; set; }

	HitDirection Dir { get; set; }
}
