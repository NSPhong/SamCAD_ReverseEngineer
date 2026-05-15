using System;
using System.Collections.Generic;
using System.Windows;

namespace SamSoarII.Polyline.Expands;

public class PolylineExpandArch : PolylineExpandCircle, IPolylineExpandArch, IPolylineExpandCircle
{
	private double a0;

	private double a1;

	public double A0
	{
		get
		{
			return a0;
		}
		set
		{
			a0 = To2PI(value);
		}
	}

	public double A1
	{
		get
		{
			return a1;
		}
		set
		{
			a1 = To2PI(value);
		}
	}

	public Point From => A2P(A0);

	public Point To => A2P(A1);

	public PolylineExpandArch(PolylineImage _parent, int _id)
		: base(_parent, _id)
	{
	}

	protected Point A2P(double _a)
	{
		return base.C + base.R * new Vector(Math.Cos(_a), Math.Sin(_a));
	}

	protected double To2PI(double _a)
	{
		while (_a < 0.0)
		{
			_a += Math.PI * 2.0;
		}
		while (_a >= Math.PI * 2.0)
		{
			_a -= Math.PI * 2.0;
		}
		return _a;
	}

	public bool IsInArch(Point p)
	{
		Vector vector = p - base.C;
		double a = Vector.AngleBetween(new Vector(1.0, 0.0), vector) * Math.PI / 180.0;
		a = To2PI(a);
		if (base.IsClockwise)
		{
			if (a0 > a1 && a >= a1 - 1E-05 && a <= a0 + 1E-05)
			{
				return true;
			}
			if (a0 < a1 && (!(a >= a0 + 1E-05) || !(a <= a1 - 1E-05)))
			{
				return true;
			}
		}
		else
		{
			if (a0 < a1 && a >= a0 - 1E-05 && a <= a1 + 1E-05)
			{
				return true;
			}
			if (a0 > a1 && (!(a >= a1 + 1E-05) || !(a <= a0 - 1E-05)))
			{
				return true;
			}
		}
		return false;
	}

	public override void GetYs(double _x, List<IPolylineExpandHit> _hits)
	{
		if (!(Math.Abs(_x - base.C.X) > base.R + 1E-05))
		{
			double val = _x - base.C.X;
			val = Math.Min(Math.Max(val, 0.0 - base.R), base.R);
			double num = Math.Sqrt(Math.Pow(base.R, 2.0) - Math.Pow(val, 2.0));
			Point p = new Point(_x, base.C.Y - num);
			Point p2 = new Point(_x, base.C.Y + num);
			if (IsInArch(p))
			{
				PolylineExpandHit polylineExpandHit = new PolylineExpandHit(this, p, 0);
				polylineExpandHit.Dir = ((!(Math.Abs(num) <= 1E-05)) ? ((!base.IsClockwise) ? HitDirection.Left : HitDirection.Right) : HitDirection.None);
				_hits.Add(polylineExpandHit);
			}
			if (Math.Abs(num) > 1E-05 && IsInArch(p2))
			{
				PolylineExpandHit polylineExpandHit2 = new PolylineExpandHit(this, p2, 1);
				polylineExpandHit2.Dir = (base.IsClockwise ? HitDirection.Left : HitDirection.Right);
				_hits.Add(polylineExpandHit2);
			}
		}
	}

	public override void GetXs(double _y, List<IPolylineExpandHit> _hits)
	{
		if (!(Math.Abs(_y - base.C.Y) > base.R + 1E-05))
		{
			double val = _y - base.C.Y;
			val = Math.Min(Math.Max(val, 0.0 - base.R), base.R);
			double num = Math.Sqrt(Math.Pow(base.R, 2.0) - Math.Pow(val, 2.0));
			Point p = new Point(base.C.X - num, _y);
			Point p2 = new Point(base.C.X + num, _y);
			if (IsInArch(p))
			{
				PolylineExpandHit polylineExpandHit = new PolylineExpandHit(this, p, 0);
				polylineExpandHit.Dir = ((!(Math.Abs(num) <= 1E-05)) ? (base.IsClockwise ? HitDirection.Up : HitDirection.Down) : HitDirection.None);
				_hits.Add(polylineExpandHit);
			}
			if (Math.Abs(num) > 1E-05 && IsInArch(p2))
			{
				PolylineExpandHit polylineExpandHit2 = new PolylineExpandHit(this, p2, 1);
				polylineExpandHit2.Dir = (base.IsClockwise ? HitDirection.Down : HitDirection.Up);
				_hits.Add(polylineExpandHit2);
			}
		}
	}
}
