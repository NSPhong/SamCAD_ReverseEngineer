using System.Windows;

namespace SamSoarII.Polyline.Expands;

public interface IPolylineExpandCircle
{
	Point C { get; set; }

	double R { get; set; }

	bool IsClockwise { get; set; }
}
