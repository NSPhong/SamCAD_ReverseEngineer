using System;
using System.Collections.Generic;
using System.Windows;
using SamSoarII.Polyline.Entity;

namespace SamSoarII.Polyline.Expands;

public class PolylineExpandLine : PolylineExpand, IPolylineExpandLine, IPolylineExpand
{
	private Point p0;

	private Point p1;

	public Point P0
	{
		get
		{
			return p0;
		}
		set
		{
			p0 = value;
		}
	}

	public Point P1
	{
		get
		{
			return p1;
		}
		set
		{
			p1 = value;
		}
	}

	public PolylineExpandLine(PolylineImage _parent, int _id)
		: base(_parent, _id)
	{
	}

	public override IPolylineEntity ToEntity(Point _from, IPolylineExpand _that)
	{
		Point? connect = GetConnect(_from, _that);
		if (!connect.HasValue)
		{
			return null;
		}
		Point value = connect.Value;
		if (value.Equals(_from))
		{
			return null;
		}
		return new PolylineLine(parent, id, value);
	}

	public override Point? GetConnect(Point _from, IPolylineExpand _that)
	{
		if (_that is IPolylineExpandLine)
		{
			IPolylineExpandLine polylineExpandLine = (IPolylineExpandLine)_that;
			double num = Vector.CrossProduct(polylineExpandLine.P1 - polylineExpandLine.P0, P0 - polylineExpandLine.P0);
			double num2 = Vector.CrossProduct(polylineExpandLine.P1 - polylineExpandLine.P0, P1 - polylineExpandLine.P0);
			return (num > num2) ? (P0 + (P0 - P1) * num / (num2 - num)) : ((num < num2) ? (P1 + (P1 - P0) * num2 / (num - num2)) : P1);
		}
		if (_that is IPolylineExpandCircle)
		{
			IPolylineExpandCircle polylineExpandCircle = (IPolylineExpandCircle)_that;
			double num3 = Vector.CrossProduct(P1 - P0, polylineExpandCircle.C - P0) / (P1 - P0).Length;
			if (Math.Abs(num3) > polylineExpandCircle.R + 0.0001)
			{
				return null;
			}
			num3 = Math.Max(Math.Min(num3, polylineExpandCircle.R), 0.0 - polylineExpandCircle.R);
			double num4 = Math.Sqrt((polylineExpandCircle.C - P0).LengthSquared - Math.Pow(num3, 2.0));
			double num5 = Math.Sqrt(Math.Pow(polylineExpandCircle.R, 2.0) - Math.Pow(num3, 2.0));
			if (double.IsNaN(num3) || double.IsNaN(num5) || double.IsNaN(num4))
			{
				return null;
			}
			Vector vector = P1 - P0;
			Vector vector2 = vector;
			vector *= (num4 - num5) / vector.Length;
			vector2 *= (num4 + num5) / vector2.Length;
			if ((Vector.CrossProduct(P0 - polylineExpandCircle.C, P1 - polylineExpandCircle.C) > 0.0) ^ (num3 > 0.0))
			{
				vector *= -1.0;
				vector2 *= -1.0;
			}
			Point point = P0 + vector;
			Point point2 = P0 + vector2;
			if (_that is IPolylineExpandArch)
			{
				IPolylineExpandArch polylineExpandArch = (IPolylineExpandArch)_that;
				if (polylineExpandArch.IsInArch(point))
				{
					return point;
				}
				if (polylineExpandArch.IsInArch(point2))
				{
					return point2;
				}
				return null;
			}
			return point;
		}
		return null;
	}

	public override void GetYs(double _x, List<IPolylineExpandHit> _hits)
	{
		if (!(Math.Abs(P0.X - P1.X) <= 1E-05) && !(_x < Math.Min(P0.X, P1.X) - 1E-05) && !(_x > Math.Max(P0.X, P1.X) + 1E-05))
		{
			PolylineExpandHit polylineExpandHit = new PolylineExpandHit(this, P0 + (P1 - P0) * (_x - P0.X) / (P1.X - P0.X), 0);
			polylineExpandHit.Dir = ((!(P0.X < P1.X)) ? HitDirection.Left : HitDirection.Right);
			_hits.Add(polylineExpandHit);
		}
	}

	public override void GetXs(double _y, List<IPolylineExpandHit> _hits)
	{
		if (!(Math.Abs(P0.Y - P1.Y) <= 1E-05) && !(_y < Math.Min(P0.Y, P1.Y) - 1E-05) && !(_y > Math.Max(P0.Y, P1.Y) + 1E-05))
		{
			PolylineExpandHit polylineExpandHit = new PolylineExpandHit(this, P0 + (P1 - P0) * (_y - P0.Y) / (P1.Y - P0.Y), 0);
			polylineExpandHit.Dir = ((P0.Y < P1.Y) ? HitDirection.Up : HitDirection.Down);
			_hits.Add(polylineExpandHit);
		}
	}
}
