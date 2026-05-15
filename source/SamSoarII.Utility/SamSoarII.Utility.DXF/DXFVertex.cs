using System;
using System.Windows;

namespace SamSoarII.Utility.DXF;

public class DXFVertex : IComparable<DXFVertex>
{
	public const double Delta = 0.1;

	private Point p;

	private PointRegion region;

	public Point P => p;

	public PointRegion Region
	{
		get
		{
			return region;
		}
		set
		{
			region = value;
		}
	}

	public DXFVertex(Point p)
	{
		this.p = p;
		region = new PointRegion(GetRegionBound(p.X), GetRegionBound(p.Y));
	}

	public int CompareTo(DXFVertex other)
	{
		return region.CompareTo(other.region);
	}

	private int ComputeOffset(double x1, double x2)
	{
		double num = x1 - x2;
		if (num < 0.1 && num > -0.1)
		{
			return 0;
		}
		if (num > 0.0)
		{
			return 1;
		}
		return -1;
	}

	public static Vector operator -(DXFVertex left, DXFVertex right)
	{
		return left.p - right.p;
	}

	private int GetRegionBound(double value)
	{
		double num = value / 0.1;
		return (int)num + ((num % 1.0 >= 0.5) ? 1 : 0);
	}
}
