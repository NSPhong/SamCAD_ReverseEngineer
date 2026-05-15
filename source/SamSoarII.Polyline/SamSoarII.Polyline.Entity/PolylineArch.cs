using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using SamSoarII.Core.Files;
using SamSoarII.Polyline.Expands;
using SamSoarII.Shell;

namespace SamSoarII.Polyline.Entity;

public class PolylineArch : PolylineCircle, IPolylineArch, IPolylineCircle, IPolylineEntity, INotifyPropertyChanged, IDisposable, IGridPenningEntity, IGridPenningArch, IGridPenningCircle
{
	public override PolylineType Type => PolylineType.Arch;

	public override Point To
	{
		get
		{
			return to;
		}
		set
		{
			to = value;
			InvokePropertyChanged("To");
		}
	}

	public bool IsLarge
	{
		get
		{
			Vector vector = To - From;
			Vector vector2 = base.Center - From;
			double num = Vector.CrossProduct(vector, vector2);
			return num == 0.0 || (num > 0.0 && isclockwise) || (num < 0.0 && !isclockwise);
		}
		set
		{
		}
	}

	public override double Radius
	{
		get
		{
			return base.Radius;
		}
		set
		{
			CenterRadius(value);
		}
	}

	public override Rect Bounding
	{
		get
		{
			double num = ((IGridPenningArch)this).StartAngle;
			double num2 = ((IGridPenningArch)this).EndAngle;
			Rect result = new Rect(From, To);
			for (; num < 0.0; num += Math.PI * 2.0)
			{
			}
			while (num > Math.PI * 2.0)
			{
				num -= Math.PI * 2.0;
			}
			for (; num2 < 0.0; num2 += Math.PI * 2.0)
			{
			}
			while (num2 > Math.PI * 2.0)
			{
				num2 -= Math.PI * 2.0;
			}
			int quad = GetQuad(num);
			int quad2 = GetQuad(num2);
			if (!base.IsClockwise)
			{
				for (int num3 = quad; num3 != quad2; num3 = (num3 + 1) & 3)
				{
					switch (num3)
					{
					case 0:
						result.Union(new Point(base.Center.X, base.Center.Y + Radius));
						break;
					case 1:
						result.Union(new Point(base.Center.X - Radius, base.Center.Y));
						break;
					case 2:
						result.Union(new Point(base.Center.X, base.Center.Y - Radius));
						break;
					case 3:
						result.Union(new Point(base.Center.X + Radius, base.Center.Y));
						break;
					}
				}
			}
			else
			{
				for (int num4 = quad; num4 != quad2; num4 = (num4 - 1) & 3)
				{
					switch (num4)
					{
					case 0:
						result.Union(new Point(base.Center.X + Radius, base.Center.Y));
						break;
					case 3:
						result.Union(new Point(base.Center.X, base.Center.Y - Radius));
						break;
					case 2:
						result.Union(new Point(base.Center.X - Radius, base.Center.Y));
						break;
					case 1:
						result.Union(new Point(base.Center.X, base.Center.Y + Radius));
						break;
					}
				}
			}
			return result;
		}
	}

	public override IEnumerable<IPolylineControlPoint> ControlPoints
	{
		get
		{
			yield return new PolylineControlPoint(this, 1);
			yield return new PolylineControlPoint(this, 2);
		}
	}

	Point IGridPenningArch.Start => From;

	Point IGridPenningArch.End => To;

	double IGridPenningArch.StartAngle => Vector.AngleBetween(new Vector(1.0, 0.0), From - base.Center) / 180.0 * Math.PI;

	double IGridPenningArch.EndAngle => Vector.AngleBetween(new Vector(1.0, 0.0), To - base.Center) / 180.0 * Math.PI;

	bool IGridPenningCircle.Clockwise => base.IsClockwise;

	public PolylineArch()
	{
	}

	public PolylineArch(IPolylineImage _parent, int _id, Point _to, Point _center, bool _isclockwise = true, bool _islarge = false)
		: base(_parent, _id, _to, _center, _isclockwise)
	{
	}

	protected override string _GetName()
	{
		return $"{base.ID}. Circle弧";
	}

	protected bool InQuad(double angle, int id)
	{
		return id switch
		{
			0 => angle <= Math.PI / 2.0, 
			1 => angle >= Math.PI / 2.0 && angle <= Math.PI, 
			2 => angle >= Math.PI && angle <= 4.71238898038469, 
			3 => angle >= 4.71238898038469 && angle <= Math.PI * 2.0, 
			_ => false, 
		};
	}

	protected int GetQuad(double angle)
	{
		for (int i = 0; i < 4; i++)
		{
			if (InQuad(angle, i))
			{
				return i;
			}
		}
		return 0;
	}

	public override LocatedFileHeader AllocHeader()
	{
		return FileFormat.AllocHeader(FileHeaderTypes.PolylineArch);
	}

	public override void Save(PolylineEntityHeader header)
	{
		base.Save(header);
		header.bEntityType = 3;
		if (header is PolylineArchHeader)
		{
			PolylineArchHeader polylineArchHeader = (PolylineArchHeader)header;
			polylineArchHeader.fTargetX = (float)To.X;
			polylineArchHeader.fTargetY = (float)To.Y;
		}
	}

	public override void Load(PolylineEntityHeader header)
	{
		base.Load(header);
		if (header is PolylineArchHeader)
		{
			PolylineArchHeader polylineArchHeader = (PolylineArchHeader)header;
			To = new Point(polylineArchHeader.fTargetX, polylineArchHeader.fTargetY);
		}
	}

	public override void Save(StringBuilder sb)
	{
		Vector vector = To - From;
		Vector vector2 = base.Center - From;
		sb.Append("ARCH,");
		sb.Append(vector.X);
		sb.Append(",");
		sb.Append(vector.Y);
		sb.Append(",");
		sb.Append(vector2.X);
		sb.Append(",");
		sb.Append(vector2.Y);
		sb.Append(",");
		sb.Append(isclockwise);
		sb.Append(",");
		SaveArgument(sb);
	}

	public override void Load(string text)
	{
		base.Load(text);
		string[] array = text.Split(',');
		double x = double.Parse(array[1]);
		double y = double.Parse(array[2]);
		double x2 = double.Parse(array[3]);
		double y2 = double.Parse(array[4]);
		To = From + new Vector(x, y);
		base.Center = From + new Vector(x2, y2);
		isclockwise = bool.Parse(array[5]);
		LoadArgument(array, 6);
	}

	public override IPolylineEntity Clone()
	{
		PolylineArch polylineArch = new PolylineArch(base.Parent, base.ID, To, base.Center, base.IsClockwise, IsLarge);
		polylineArch.Load(this);
		return polylineArch;
	}

	public override double GetDist(Point p)
	{
		Vector vector = From - base.Center;
		Vector vector2 = To - base.Center;
		Vector vector3 = p - base.Center;
		Vector vector4 = To - From;
		double num = Vector.CrossProduct(vector, vector3);
		double num2 = Vector.CrossProduct(vector2, vector3);
		double num3 = Vector.CrossProduct(vector4, vector);
		if (isclockwise)
		{
			if (num3 >= 0.0)
			{
				if (num > 0.0 || num2 < 0.0)
				{
					return double.MaxValue;
				}
			}
			else if (num >= 0.0 && num2 <= 0.0)
			{
				return double.MaxValue;
			}
		}
		else if (num3 >= 0.0)
		{
			if (num <= 0.0 && num2 >= 0.0)
			{
				return double.MaxValue;
			}
		}
		else if (num < 0.0 || num2 > 0.0)
		{
			return double.MaxValue;
		}
		return base.GetDist(p);
	}

	public void CenterFrom()
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
	}

	public void CenterTo()
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
	}

	public override void CenterRadius(double _radius)
	{
		Vector vector = From - To;
		Vector vector2 = base.Center - To;
		Point point = To + vector * 0.5;
		Vector vector3 = point - To;
		double num = _radius * _radius;
		double lengthSquared = vector3.LengthSquared;
		double num2 = Vector.CrossProduct(vector, vector2);
		Vector vector4 = ((num2 >= 0.0) ? new Vector(0.0 - vector3.Y, vector3.X) : new Vector(vector3.Y, 0.0 - vector3.X));
		if (num < lengthSquared)
		{
			base.Center = point;
		}
		else
		{
			base.Center = point + vector4 * Math.Sqrt((num - lengthSquared) / vector4.LengthSquared);
		}
	}

	public override void CenterAngle(double _angle)
	{
		base.CenterAngle(_angle);
	}

	public override void CenterRadiusAngle(double _radius, double _angle)
	{
		CenterRadius(_radius);
	}

	public override IPolylineExpand Expand(double r)
	{
		Point point = From + (To - From) / 2.0;
		Vector vector = To - From;
		vector = new Vector(0.0 - vector.Y, vector.X);
		if (!base.IsClockwise)
		{
			vector *= -1.0;
		}
		vector *= Radius / vector.Length;
		Point point2 = base.Center + vector;
		bool flag = true;
		flag ^= Vector.CrossProduct(point - From, point2 - From) < 0.0;
		return new PolylineExpandArch((PolylineImage)base.Parent, base.ID)
		{
			C = base.Center,
			R = Radius + r * (double)(flag ? 1 : (-1)),
			IsClockwise = base.IsClockwise,
			A0 = Vector.AngleBetween(new Vector(1.0, 0.0), From - base.Center) * Math.PI / 180.0,
			A1 = Vector.AngleBetween(new Vector(1.0, 0.0), To - base.Center) * Math.PI / 180.0
		};
	}

	public override Vector? Lawer(double r, Point p)
	{
		Point point = From + (To - From) / 2.0;
		Vector vector = To - From;
		vector = new Vector(0.0 - vector.Y, vector.X);
		if (!base.IsClockwise)
		{
			vector *= -1.0;
		}
		vector *= Radius / vector.Length;
		Point point2 = base.Center + vector;
		bool flag = true;
		flag ^= Vector.CrossProduct(point - From, point2 - From) < 0.0;
		Vector vector2 = p - base.Center;
		return vector2 * (flag ? r : (0.0 - r)) / vector2.Length;
	}

	public override double GetLength(Point p)
	{
		Vector vector = p - base.Center;
		Vector vector2 = To - base.Center;
		double y = Vector.CrossProduct(vector, vector2);
		double x = vector * vector2;
		double num = Math.Atan2(y, x);
		if (num < 0.0)
		{
			num += Math.PI * 2.0;
		}
		if (base.IsClockwise)
		{
			num = Math.PI * 2.0 - num;
		}
		return Radius * num;
	}
}
