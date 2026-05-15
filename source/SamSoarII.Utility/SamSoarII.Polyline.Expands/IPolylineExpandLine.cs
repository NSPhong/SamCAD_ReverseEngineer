using System.Windows;

namespace SamSoarII.Polyline.Expands;

public interface IPolylineExpandLine : IPolylineExpand
{
	Point P0 { get; set; }

	Point P1 { get; set; }
}
