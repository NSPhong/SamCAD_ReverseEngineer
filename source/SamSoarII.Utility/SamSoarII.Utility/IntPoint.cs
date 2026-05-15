using System;

namespace SamSoarII.Utility;

public struct IntPoint : IComparable
{
	public int X { get; set; }

	public int Y { get; set; }

	public static IntPoint GetIntpointByDouble(double x, double y, int scale)
	{
		int x2 = (int)x / scale;
		int y2 = (int)y / scale;
		return new IntPoint
		{
			X = x2,
			Y = y2
		};
	}

	public static IntPoint GetIntpointByDouble(double x, double y, int scalex, int scaley)
	{
		int x2 = (int)x / scalex;
		int y2 = (int)y / scaley;
		return new IntPoint
		{
			X = x2,
			Y = y2
		};
	}

	public int CompareTo(object obj)
	{
		IntPoint intPoint = (IntPoint)obj;
		if (Y < intPoint.Y)
		{
			return -1;
		}
		if (Y > intPoint.Y)
		{
			return 1;
		}
		if (X < intPoint.X)
		{
			return -1;
		}
		if (X > intPoint.X)
		{
			return 1;
		}
		return 0;
	}
}
