using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using SamSoarII.Core.Files;
using SamSoarII.Polyline.Expands;
using SamSoarII.Shell;

namespace SamSoarII.Polyline.Entity;

public class PolylineCircle : PolylineEntity, IPolylineCircle, IPolylineEntity, INotifyPropertyChanged, IDisposable, IGridPenningEntity, IGridPenningCircle
{
	protected Point center;

	protected bool isclockwise;

	public override PolylineType Type => PolylineType.Circle;

	public Point Center
	{
		get
		{
			return center;
		}
		set
		{
			center = value;
			InvokePropertyChanged("Center");
			InvokePropertyChanged("Radius");
		}
	}

	public override Point To
	{
		get
		{
			return base.From;
		}
		set
		{
			base.From = value;
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
			InvokePropertyChanged("IsClockwise");
		}
	}

	bool IGridPenningCircle.Clockwise => isclockwise;

	public virtual double Radius
	{
		get
		{
			return (From - Center).Length;
		}
		set
		{
			Center = From + (Center - From) / ((Center - From).Length * value);
		}
	}

	public override Rect Bounding => new Rect(Center.X - Radius, Center.Y - Radius, Radius * 2.0, Radius * 2.0);

	public override Vector Tangent
	{
		get
		{
			Vector vector = From - Center;
			return (!IsClockwise) ? new Vector(0.0 - vector.Y, vector.X) : new Vector(vector.Y, 0.0 - vector.X);
		}
	}

	public override Vector TangentBack
	{
		get
		{
			Vector vector = To - Center;
			return IsClockwise ? new Vector(0.0 - vector.Y, vector.X) : new Vector(vector.Y, 0.0 - vector.X);
		}
	}

	public override IEnumerable<IPolylineControlPoint> ControlPoints
	{
		get
		{
			yield return new PolylineControlPoint(this, 2);
		}
	}

	public PolylineCircle()
	{
	}

	public PolylineCircle(IPolylineImage _parent, int _id, Point _to, Point _center, bool _isclockwise = true)
		: base(_parent, _id, _to)
	{
		center = _center;
		isclockwise = _isclockwise;
	}

	protected override string _GetName()
	{
		return $"{base.ID}. Circle";
	}

	public override LocatedFileHeader AllocHeader()
	{
		return FileFormat.AllocHeader(FileHeaderTypes.PolylineCircle);
	}

	public override void Save(PolylineEntityHeader header)
	{
		base.Save(header);
		header.bEntityType = 2;
		if (header is PolylineCircleHeader)
		{
			PolylineCircleHeader polylineCircleHeader = (PolylineCircleHeader)header;
			polylineCircleHeader.fCenterX = (float)center.X;
			polylineCircleHeader.fCenterY = (float)center.Y;
			polylineCircleHeader.bIsClockwise = (byte)(isclockwise ? 1u : 0u);
		}
	}

	public override void Load(PolylineEntityHeader header)
	{
		base.Load(header);
		if (header is PolylineCircleHeader)
		{
			PolylineCircleHeader polylineCircleHeader = (PolylineCircleHeader)header;
			center = new Point(polylineCircleHeader.fCenterX, polylineCircleHeader.fCenterY);
			isclockwise = polylineCircleHeader.bIsClockwise > 0;
		}
	}

	public override void Save(DownloadWriter dw)
	{
		base.Save(dw);
		dw.Write((byte)((this is IPolylineArch) ? 1u : 2u));
		dw.Write((byte)(base.IsReal ? 1u : 0u));
		dw.Write((float)To.X);
		dw.Write((float)To.Y);
		dw.Write((byte)(isclockwise ? 1u : 0u));
		dw.Write((float)Center.X);
		dw.Write((float)Center.Y);
		SaveArgument(dw);
	}

	public override void Load(UploadReader ur)
	{
		base.Load(ur);
		base.IsReal = ur.Read8() > 0;
		float num = ur.Read32F();
		float num2 = ur.Read32F();
		if (this is PolylineArch)
		{
			To = new Point(num, num2);
		}
		isclockwise = ur.Read8() > 0;
		float num3 = ur.Read32F();
		float num4 = ur.Read32F();
		Center = new Point(num3, num4);
		LoadArgument(ur);
	}

	public override void Save(StringBuilder sb)
	{
		base.Save(sb);
		Vector vector = To - From;
		Vector vector2 = Center - From;
		sb.Append("CIRCLE,");
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
		Center = From + new Vector(x2, y2);
		isclockwise = bool.Parse(array[5]);
		LoadArgument(array, 6);
	}

	public override IPolylineEntity Clone()
	{
		PolylineCircle polylineCircle = new PolylineCircle(base.Parent, base.ID, To, Center, isclockwise);
		polylineCircle.Load(this);
		return polylineCircle;
	}

	public override IPolylineEntity Move(Vector v)
	{
		IPolylineCircle polylineCircle = (IPolylineCircle)base.Move(v);
		polylineCircle.Center += v;
		return polylineCircle;
	}

	public override IPolylineEntity Reverse()
	{
		IPolylineCircle polylineCircle = (IPolylineCircle)base.Reverse();
		polylineCircle.IsClockwise = !isclockwise;
		return polylineCircle;
	}

	public override IPolylineEntity Mirror(Point p, Vector v)
	{
		IPolylineCircle polylineCircle = (IPolylineCircle)base.Mirror(p, v);
		polylineCircle.IsClockwise = !polylineCircle.IsClockwise;
		polylineCircle.Center = GetMirrorPoint(Center, p, v);
		return polylineCircle;
	}

	public override IPolylineEntity Rotate(Point s, double a)
	{
		IPolylineCircle polylineCircle = (IPolylineCircle)base.Rotate(s, a);
		polylineCircle.Center = GetRotatePoint(Center, s, a);
		return polylineCircle;
	}

	public override IPolylineEntity Scale(Point s, double xs, double ys)
	{
		IPolylineCircle polylineCircle = (IPolylineCircle)base.Scale(s, xs, ys);
		polylineCircle.Center = GetScalePoint(Center, s, xs, ys);
		return polylineCircle;
	}

	public override void Load(IPolylineEntity that)
	{
		base.Load(that);
		if (that is IPolylineCircle)
		{
			IPolylineCircle polylineCircle = (IPolylineCircle)that;
			To = polylineCircle.To;
			Center = polylineCircle.Center;
			IsClockwise = polylineCircle.IsClockwise;
		}
	}

	public override double GetDist(Point p)
	{
		Vector vector = p - Center;
		Vector vector2 = From - Center;
		return Math.Abs(vector.Length - vector2.Length);
	}

	public override Point ReflectInline(Point p, bool xlock, bool ylock)
	{
		if (xlock && ylock)
		{
			return base.ReflectInline(p, xlock, ylock);
		}
		double num = Center.X - Radius;
		double num2 = Center.X + Radius;
		double num3 = Center.Y - Radius;
		double num4 = Center.Y + Radius;
		if (xlock)
		{
			if (p.X < num || p.X > num2)
			{
				return base.ReflectInline(p, xlock, ylock);
			}
			double num5 = (p.X - Center.X) / Radius;
			double num6 = Math.Sqrt(1.0 - num5 * num5);
			p.Y = Center.Y + num6 * Radius * (double)((p.Y - Center.Y > 0.0) ? 1 : (-1));
			return p;
		}
		if (ylock)
		{
			if (p.Y < num3 || p.Y > num4)
			{
				return base.ReflectInline(p, xlock, ylock);
			}
			double num7 = (p.Y - Center.Y) / Radius;
			double num8 = Math.Sqrt(1.0 - num7 * num7);
			p.X = Center.X + num8 * Radius * (double)((p.X - Center.X > 0.0) ? 1 : (-1));
			return p;
		}
		Vector vector = p - Center;
		double length = vector.Length;
		double num9 = vector.Y / length;
		double num10 = vector.X / length;
		p.X = Center.X + num9 * Radius;
		p.Y = Center.Y + num10 * Radius;
		return p;
	}

	public virtual void CenterRadius(double _radius)
	{
		Vector vector = Center - From;
		if (vector.Length == 0.0)
		{
			CenterRadiusAngle(_radius, 0.0);
			return;
		}
		vector *= _radius / vector.Length;
		Center = From + vector;
	}

	public virtual void CenterAngle(double _angle)
	{
		double radius = Radius;
		CenterRadiusAngle(radius, _angle);
	}

	public virtual void CenterRadiusAngle(double _radius, double _angle)
	{
		Vector vector = new Vector(Math.Cos(_angle * Math.PI / 180.0) * _radius, Math.Sin(_angle * Math.PI / 180.0) * _radius);
		Center = From + vector;
	}

	public override IPolylineExpand Expand(double r)
	{
		return new PolylineExpandCircle((PolylineImage)base.Parent, base.ID)
		{
			C = Center,
			R = Radius + ((isclockwise ^ (r > 0.0)) ? r : (0.0 - r)),
			IsClockwise = IsClockwise
		};
	}

	public virtual Vector? Lawer(double r, Point p)
	{
		Vector vector = p - Center;
		return vector * (r / vector.Length);
	}

	public override Vector? LawerFrom(double r)
	{
		return Lawer(r, From);
	}

	public override Vector? LawerTo(double r)
	{
		return Lawer(r, To);
	}

	public virtual Vector? Horizon(Point p)
	{
		Vector vector = p - Center;
		return IsClockwise ? new Vector(vector.Y, 0.0 - vector.X) : new Vector(0.0 - vector.Y, vector.X);
	}

	public override Vector? HorizonFrom()
	{
		return Horizon(From);
	}

	public override Vector? HorizonTo()
	{
		return Horizon(To);
	}
}
