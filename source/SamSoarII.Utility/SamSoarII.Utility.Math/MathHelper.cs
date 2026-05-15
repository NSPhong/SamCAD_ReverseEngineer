using System.Windows;

namespace SamSoarII.Utility.Math;

public class MathHelper
{
	public static Point GetCross(Point p1, Point p2, Point p3, Point p4)
	{
		using GaussLinear gaussLinear = new GaussLinear(new double[2][]
		{
			new double[3]
			{
				p1.Y - p2.Y,
				p2.X - p1.X,
				p1.X * p2.Y - p2.X * p1.Y
			},
			new double[3]
			{
				p3.Y - p4.Y,
				p4.X - p3.X,
				p3.X * p4.Y - p4.X * p3.Y
			}
		});
		double[] array = gaussLinear.Caculate();
		return new Point(array[0], array[1]);
	}
}
