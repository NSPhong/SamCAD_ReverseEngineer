using System;
using System.Windows;

namespace SamSoarII.Utility.DXF;

public struct PointRegion : IComparable<PointRegion>
{
	private int x;

	private int y;

	public int X => x;

	public int Y => y;

	public PointRegion(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	public int CompareTo(PointRegion other)
	{
		if (y < other.y)
		{
			return -1;
		}
		if (y > other.y)
		{
			return 1;
		}
		if (x < other.x)
		{
			return -1;
		}
		if (x > other.x)
		{
			return 1;
		}
		return 0;
	}

	public DXFVertex GetVertex()
	{
		return new DXFVertex(new Point((double)x * 0.1, (double)y * 0.1));
	}
}
