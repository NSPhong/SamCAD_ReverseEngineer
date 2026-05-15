using System.Windows;

namespace SamSoarII.Polyline.Expands;

public interface IPolylineExpandArch : IPolylineExpandCircle
{
	double A0 { get; set; }

	double A1 { get; set; }

	Point From { get; }

	Point To { get; }

	bool IsInArch(Point p);
}
