using System;
using System.ComponentModel;
using System.Windows;
using SamSoarII.Shell;

namespace SamSoarII.Polyline.Entity;

public class PolylineEllipseArch : PolylineEllipse, IPolylineEllipseArch, IPolylineEllipse, IPolylineEntity, INotifyPropertyChanged, IDisposable, IGridPenningEntity, IGridPenningEllipseArch, IGridPenningEllipse
{
	public override PolylineType Type => PolylineType.EllipseArch;

	public bool IsLarge
	{
		get
		{
			Vector vector = To - From;
			Vector vector2 = base.Center - From;
			double num = Vector.CrossProduct(vector, vector2);
			return num == 0.0 || (num > 0.0 && base.IsClockwise) || (num < 0.0 && !base.IsClockwise);
		}
		set
		{
		}
	}

	Point IGridPenningEllipseArch.Start => From;

	Point IGridPenningEllipseArch.End => To;

	double IGridPenningEllipseArch.StartAngle => Vector.AngleBetween(new Vector(1.0, 0.0), From - base.Center) / 180.0 * Math.PI;

	double IGridPenningEllipseArch.EndAngle => Vector.AngleBetween(new Vector(1.0, 0.0), To - base.Center) / 180.0 * Math.PI;

	public PolylineEllipseArch()
	{
	}

	public PolylineEllipseArch(IPolylineImage _image, int _id, Point _to, Point _center, Vector _direction, double _longradius, double _shortradius, bool _isclockwise)
		: base(_image, _id, _to, _center, _direction, _longradius, _shortradius, _isclockwise)
	{
	}

	protected override string _GetName()
	{
		return $"{base.ID}. Ellipse弧";
	}

	public bool CenterFrom()
	{
		Vector vector = To - From;
		Vector vector2 = base.Center - From;
		Point point = From + vector * 0.5;
		double lengthSquared = vector2.LengthSquared;
		double num = Vector.CrossProduct(vector, vector2);
		Vector vector3 = point - From;
		Vector vector4 = default(Vector);
		vector4 = ((num >= 0.0) ? new Vector(0.0 - vector3.Y, vector3.X) : new Vector(vector3.Y, 0.0 - vector3.X));
		if (lengthSquared < vector3.LengthSquared)
		{
			base.Center = point;
		}
		else
		{
			base.Center = point + vector4 * Math.Sqrt((lengthSquared - vector3.LengthSquared) / vector4.LengthSquared);
		}
		base.Direction = From - base.Center;
		double shortRadius = (base.LongRadius = base.Direction.Length);
		base.ShortRadius = shortRadius;
		return true;
	}

	public bool CenterTo()
	{
		Vector vector = From - To;
		Vector vector2 = base.Center - To;
		Point point = To + vector * 0.5;
		double lengthSquared = vector2.LengthSquared;
		double num = Vector.CrossProduct(vector, vector2);
		Vector vector3 = point - To;
		Vector vector4 = default(Vector);
		vector4 = ((num >= 0.0) ? new Vector(0.0 - vector3.Y, vector3.X) : new Vector(vector3.Y, 0.0 - vector3.X));
		if (lengthSquared < vector3.LengthSquared)
		{
			base.Center = point;
		}
		else
		{
			base.Center = point + vector4 * Math.Sqrt((lengthSquared - vector3.LengthSquared) / vector4.LengthSquared);
		}
		base.Direction = To - base.Center;
		double shortRadius = (base.LongRadius = base.Direction.Length);
		base.ShortRadius = shortRadius;
		return true;
	}

	public bool RadiusFrom()
	{
		Vector vector = From - base.Center;
		double angle = Vector.AngleBetween(new Vector(1.0, 0.0), vector);
		return RadiusAngle(angle);
	}

	public bool RadiusTo()
	{
		Vector vector = To - base.Center;
		double angle = Vector.AngleBetween(new Vector(1.0, 0.0), vector);
		return RadiusAngle(angle);
	}

	public bool RadiusAngle(double angle)
	{
		Point rotatePoint = GetRotatePoint(From, base.Center, 0.0 - angle);
		Point rotatePoint2 = GetRotatePoint(To, base.Center, 0.0 - angle);
		Vector vector = rotatePoint - base.Center;
		Vector vector2 = rotatePoint2 - base.Center;
		double num = vector.X * vector.X;
		double num2 = vector2.X * vector2.X;
		double num3 = vector.Y * vector.Y;
		double num4 = vector2.Y * vector2.Y;
		double num5 = 1.0;
		double num6 = 1.0;
		if (Math.Abs(num3) < 1E-09)
		{
			if (num == 0.0)
			{
				return false;
			}
			if (num4 == 0.0)
			{
				return false;
			}
			num5 = Math.Sqrt(1.0 / num);
			num6 = Math.Sqrt((1.0 - num5 * num5 * num2) / num4);
		}
		else if (Math.Abs(num4) < 1E-09)
		{
			if (num2 == 0.0)
			{
				return false;
			}
			if (num3 == 0.0)
			{
				return false;
			}
			num5 = Math.Sqrt(1.0 / num2);
			num6 = Math.Sqrt((1.0 - num5 * num5 * num) / num3);
		}
		else
		{
			double num7 = num4 / num3;
			num5 = Math.Sqrt((1.0 - num7) / (num2 - num * num7));
			num6 = Math.Sqrt((1.0 - num5 * num5 * num) / num3);
		}
		if (double.IsNaN(num5) || double.IsNaN(num6))
		{
			return false;
		}
		Point rotatePoint3 = GetRotatePoint(new Point(1.0, 0.0), new Point(0.0, 0.0), angle);
		base.Direction = new Vector(rotatePoint3.X, rotatePoint3.Y);
		base.LongRadius = 1.0 / num5;
		base.ShortRadius = 1.0 / num6;
		return true;
	}
}
