using System;
using System.Windows;

namespace SamSoarII.Utility.DXF;

public class DXFHelper
{
	public static double ComputeRotateAngle(Point p1, Point p2)
	{
		if (System.Math.Abs(p1.Y - p2.Y) < 1E-10)
		{
			return 0.0;
		}
		return System.Math.Atan2(p2.Y - p1.Y, p1.X - p2.X);
	}

	public static double ComputeLength(Point p1, Point p2)
	{
		return System.Math.Sqrt(System.Math.Pow(p1.X - p2.X, 2.0) + System.Math.Pow(p1.Y - p2.Y, 2.0));
	}

	public static Point ComputePoint(Point source, double radius, double angle)
	{
		return new Point
		{
			X = source.X + radius * System.Math.Cos(angle / 180.0 * System.Math.PI),
			Y = source.Y + radius * System.Math.Sin(angle / 180.0 * System.Math.PI)
		};
	}
}
