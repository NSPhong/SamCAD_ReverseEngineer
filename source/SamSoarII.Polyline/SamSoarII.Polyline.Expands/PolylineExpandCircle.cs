using System;
using System.Collections.Generic;
using System.Windows;
using SamSoarII.Polyline.Entity;

namespace SamSoarII.Polyline.Expands;

public class PolylineExpandCircle : PolylineExpand, IPolylineExpandCircle
{
	private Point c;

	private double r;

	private bool isclockwise;

	public Point C
	{
		get
		{
			return c;
		}
		set
		{
			c = value;
		}
	}

	public double R
	{
		get
		{
			return r;
		}
		set
		{
			r = value;
		}
	}

	public bool IsClockwise
	{
		get
		{
			return isclockwise;
		}
		set
		{
			isclockwise = value;
		}
	}

	public PolylineExpandCircle(PolylineImage _parent, int _id)
		: base(_parent, _id)
	{
	}

	public override IPolylineEntity ToEntity(Point _from, IPolylineExpand _that)
	{
		if ((_from - C).Length > R + 0.0001)
		{
			return null;
		}
		Point? connect = GetConnect(_from, _that);
		if (!connect.HasValue)
		{
			return null;
		}
		Point value = connect.Value;
		if (Math.Abs(value.X - _from.X) <= 0.0001 && Math.Abs(value.Y - _from.Y) <= 0.0001)
		{
			return null;
		}
		return new PolylineArch(parent, id, value, C, IsClockwise);
	}

	public override Point? GetConnect(Point _from, IPolylineExpand _that)
	{
		if (R < 0.0)
		{
			return null;
		}
		if (_that is IPolylineExpandLine)
		{
			IPolylineExpandLine polylineExpandLine = (IPolylineExpandLine)_that;
			double num = Vector.CrossProduct(polylineExpandLine.P1 - polylineExpandLine.P0, C - polylineExpandLine.P0) / (polylineExpandLine.P1 - polylineExpandLine.P0).Length;
			if (Math.Abs(num) > R + 0.0001)
			{
				return null;
			}
			num = Math.Min(Math.Max(num, 0.0 - R), R);
			double num2 = Math.Sqrt(Math.Pow(R, 2.0) - Math.Pow(num, 2.0));
			if (double.IsNaN(num) || double.IsNaN(num2))
			{
				return null;
			}
			Vector vector = polylineExpandLine.P1 - polylineExpandLine.P0;
			Vector vector2 = new Vector(0.0 - vector.Y, vector.X);
			vector *= num2 / vector.Length;
			vector2 *= (0.0 - num) / vector2.Length;
			Point point = C + vector2 - vector;
			Point point2 = C + vector2 + vector;
			if (this is IPolylineExpandArch)
			{
				IPolylineExpandArch polylineExpandArch = (IPolylineExpandArch)this;
				Point to = polylineExpandArch.To;
				double value = Vector.AngleBetween(to - C, point - C);
				double value2 = Vector.AngleBetween(to - C, point2 - C);
				return (Math.Abs(value) < Math.Abs(value2)) ? point : point2;
			}
			if (Math.Abs(point.X - _from.X) <= 0.0001 && Math.Abs(point.Y - _from.Y) <= 0.0001)
			{
				return point2;
			}
			if (Math.Abs(point2.X - _from.X) <= 0.0001 && Math.Abs(point2.Y - _from.Y) <= 0.0001)
			{
				return point;
			}
			return (IsClockwise ^ (Vector.CrossProduct(point - C, point2 - C) > 0.0) ^ (Vector.CrossProduct(point - _from, point2 - _from) < 0.0)) ? point2 : point;
		}
		if (_that is IPolylineExpandCircle)
		{
			IPolylineExpandCircle polylineExpandCircle = (IPolylineExpandCircle)_that;
			double num3 = R;
			double num4 = polylineExpandCircle.R;
			double length = (polylineExpandCircle.C - C).Length;
			if (length > num3 + num4 + 0.0001)
			{
				return null;
			}
			if (length < Math.Abs(num3 - num4) - 0.0001)
			{
				return null;
			}
			double num5 = (0.0 - (Math.Pow(num4, 2.0) - Math.Pow(num3, 2.0) - Math.Pow(length, 2.0))) / (2.0 * num3 * length);
			if (double.IsNaN(num5))
			{
				return null;
			}
			num5 = Math.Min(Math.Max(num5, -1.0), 1.0);
			double num6 = Math.Sqrt(1.0 - Math.Pow(num5, 2.0));
			Vector vector3 = polylineExpandCircle.C - C;
			Vector vector4 = new Vector(0.0 - vector3.Y, vector3.X);
			vector3 *= num5 * num3 / vector3.Length;
			vector4 *= num6 * num3 / vector4.Length;
			Point point3 = C + vector3 + vector4;
			Point point4 = C + vector3 - vector4;
			if (this is IPolylineExpandArch)
			{
				IPolylineExpandArch polylineExpandArch2 = (IPolylineExpandArch)this;
				Point to2 = polylineExpandArch2.To;
				double value3 = Vector.AngleBetween(to2 - C, point3 - C);
				double value4 = Vector.AngleBetween(to2 - C, point4 - C);
				return (Math.Abs(value3) < Math.Abs(value4)) ? point3 : point4;
			}
			Point point5 = ((IsClockwise ^ (Vector.CrossProduct(point3 - C, point4 - C) > 0.0) ^ (Vector.CrossProduct(point3 - _from, point4 - _from) <= 0.0)) ? point3 : point4);
			if (_that is IPolylineExpandArch && !((IPolylineExpandArch)_that).IsInArch(point5))
			{
				return null;
			}
			return point5;
		}
		return null;
	}

	public override void GetYs(double _x, List<IPolylineExpandHit> _hits)
	{
		if (!(Math.Abs(_x - C.X) >= R))
		{
			double x = _x - C.X;
			double num = Math.Sqrt(Math.Pow(R, 2.0) - Math.Pow(x, 2.0));
			PolylineExpandHit polylineExpandHit = new PolylineExpandHit(this, _x, C.Y - num, 0);
			polylineExpandHit.Dir = ((!(Math.Abs(num) <= 1E-05)) ? ((!IsClockwise) ? HitDirection.Left : HitDirection.Right) : HitDirection.None);
			_hits.Add(polylineExpandHit);
			if (Math.Abs(num) > 1E-05)
			{
				PolylineExpandHit polylineExpandHit2 = new PolylineExpandHit(this, _x, C.Y + num, 0);
				polylineExpandHit2.Dir = (IsClockwise ? HitDirection.Left : HitDirection.Right);
				_hits.Add(polylineExpandHit2);
			}
		}
	}

	public override void GetXs(double _y, List<IPolylineExpandHit> _hits)
	{
		if (!(Math.Abs(_y - C.Y) >= R))
		{
			double x = _y - C.Y;
			double num = Math.Sqrt(Math.Pow(R, 2.0) - Math.Pow(x, 2.0));
			PolylineExpandHit polylineExpandHit = new PolylineExpandHit(this, C.X - num, _y, 0);
			polylineExpandHit.Dir = ((!(Math.Abs(num) <= 1E-05)) ? (IsClockwise ? HitDirection.Up : HitDirection.Down) : HitDirection.None);
			_hits.Add(polylineExpandHit);
			if (Math.Abs(num) > 1E-05)
			{
				PolylineExpandHit polylineExpandHit2 = new PolylineExpandHit(this, C.X + num, _y, 1);
				polylineExpandHit2.Dir = (IsClockwise ? HitDirection.Down : HitDirection.Up);
				_hits.Add(polylineExpandHit2);
			}
		}
	}
}
