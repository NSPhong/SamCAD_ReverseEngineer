using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using SamSoarII.Core.Files;
using SamSoarII.Shell;

namespace SamSoarII.Polyline.Entity;

public class PolylineEllipse : PolylineEntity, IPolylineEllipse, IPolylineEntity, INotifyPropertyChanged, IDisposable, IGridPenningEntity, IGridPenningEllipse
{
	private Point center;

	private Vector direction;

	private double longradius;

	private double shortradius;

	private bool isclockwise;

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
		}
	}

	public Vector Direction
	{
		get
		{
			return direction;
		}
		set
		{
			direction = value;
			InvokePropertyChanged("Direction");
		}
	}

	public double LongRadius
	{
		get
		{
			return longradius;
		}
		set
		{
			longradius = value;
			InvokePropertyChanged("LongRadius");
		}
	}

	public double ShortRadius
	{
		get
		{
			return shortradius;
		}
		set
		{
			shortradius = value;
			InvokePropertyChanged("ShortRadius");
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

	bool IGridPenningEllipse.Clockwise => IsClockwise;

	public override PolylineType Type => PolylineType.Ellipse;

	public override bool IsSpecial => true;

	public override IEnumerable<IPolylineControlPoint> ControlPoints
	{
		get
		{
			yield break;
		}
	}

	public PolylineEllipse()
	{
	}

	public PolylineEllipse(IPolylineImage _image, int _id, Point _to, Point _center, Vector _direction, double _longradius, double _shortradius, bool _isclockwise)
		: base(_image, _id, _to)
	{
		center = _center;
		direction = _direction;
		longradius = _longradius;
		shortradius = _shortradius;
		isclockwise = _isclockwise;
	}

	protected override string _GetName()
	{
		return $"{base.ID}. Ellipse";
	}

	public Point GetCrossPoint(double angle)
	{
		double num = Vector.AngleBetween(new Vector(1.0, 0.0), Direction) * Math.PI / 180.0;
		Vector vector = new Vector(Math.Cos(angle), Math.Sin(angle));
		vector = new Vector(vector.X * Math.Cos(0.0 - num) - vector.Y * Math.Sin(0.0 - num), vector.X * Math.Sin(0.0 - num) + vector.Y * Math.Cos(0.0 - num));
		vector.X /= LongRadius;
		vector.Y /= ShortRadius;
		angle = Vector.AngleBetween(new Vector(1.0, 0.0), vector) * Math.PI / 180.0;
		Vector vector2 = new Vector(LongRadius * Math.Cos(angle), ShortRadius * Math.Sin(angle));
		vector2 = new Vector(vector2.X * Math.Cos(num) - vector2.Y * Math.Sin(num), vector2.X * Math.Sin(num) + vector2.Y * Math.Cos(num));
		return center + vector2;
	}

	public override double GetDist(Point p)
	{
		Vector vector = p - center;
		double angle = Vector.AngleBetween(new Vector(1.0, 0.0), vector) * Math.PI / 180.0;
		Point crossPoint = GetCrossPoint(angle);
		return (p - crossPoint).Length;
	}

	public override IPolylineEntity Clone()
	{
		return new PolylineEllipse(base.Parent, base.ID, To, Center, Direction, LongRadius, ShortRadius, IsClockwise);
	}

	public override IPolylineEntity Move(Vector v)
	{
		return new PolylineEllipse(base.Parent, base.ID, To + v, Center + v, Direction, LongRadius, ShortRadius, IsClockwise);
	}

	public override IPolylineEntity Reverse()
	{
		return new PolylineEllipse(base.Parent, base.ID, From, Center, Direction, LongRadius, ShortRadius, !IsClockwise);
	}

	public override IPolylineEntity Mirror(Point p, Vector v)
	{
		return new PolylineEllipse(base.Parent, base.ID, GetMirrorPoint(To, p, v), GetMirrorPoint(Center, p, v), GetMirrorVector(Direction, v), LongRadius, ShortRadius, !IsClockwise);
	}

	public override IPolylineEntity Rotate(Point s, double a)
	{
		return new PolylineEllipse(base.Parent, base.ID, GetRotatePoint(To, s, a), GetRotatePoint(Center, s, a), GetRotateVector(Direction, a), LongRadius, ShortRadius, IsClockwise);
	}

	public override IPolylineEntity Scale(Point s, double xs, double ys)
	{
		Vector vector = Direction;
		Point p = Center;
		Point p2 = Center + vector * LongRadius / vector.Length;
		vector = new Vector(0.0 - vector.Y, vector.X);
		Point p3 = Center + vector * ShortRadius / vector.Length;
		p = GetScalePoint(p, s, xs, ys);
		p2 = GetScalePoint(p2, s, xs, ys);
		p3 = GetScalePoint(p3, s, xs, ys);
		return new PolylineEllipse(base.Parent, base.ID, GetScalePoint(To, s, xs, ys), p, p2 - p, (p2 - p).Length, (p3 - p).Length, IsClockwise);
	}

	public override void Save(DownloadWriter dw)
	{
		base.Save(dw);
	}

	public override void Save(PolylineEntityHeader header)
	{
		base.Save(header);
		header.bEntityType = 4;
		if (header is PolylineEllipseHeader)
		{
			PolylineEllipseHeader polylineEllipseHeader = (PolylineEllipseHeader)header;
			polylineEllipseHeader.fCenterX = (float)Center.X;
			polylineEllipseHeader.fCenterY = (float)Center.Y;
			polylineEllipseHeader.bIsClockwise = (byte)(isclockwise ? 1u : 0u);
			polylineEllipseHeader.fDirectX = (float)Direction.X;
			polylineEllipseHeader.fDirectY = (float)Direction.Y;
			polylineEllipseHeader.fShortRadius = (float)ShortRadius;
			polylineEllipseHeader.fLongRadius = (float)LongRadius;
		}
	}

	public override void Save(StringBuilder sb)
	{
		base.Save(sb);
		Vector vector = Center - From;
		sb.Append("ELLIPSE,");
		sb.Append(vector.X);
		sb.Append(",");
		sb.Append(vector.Y);
		sb.Append(",");
		sb.Append(direction.X);
		sb.Append(",");
		sb.Append(direction.Y);
		sb.Append(",");
		sb.Append(isclockwise);
		sb.Append(",");
		sb.Append(shortradius);
		sb.Append(",");
		sb.Append(longradius);
		sb.Append(",");
		SaveArgument(sb);
	}

	public override void Load(UploadReader ur)
	{
		base.Load(ur);
	}

	public override void Load(PolylineEntityHeader header)
	{
		base.Load(header);
		if (header is PolylineEllipseHeader)
		{
			PolylineEllipseHeader polylineEllipseHeader = (PolylineEllipseHeader)header;
			center = new Point(polylineEllipseHeader.fCenterX, polylineEllipseHeader.fCenterY);
			direction = new Vector(polylineEllipseHeader.fDirectX, polylineEllipseHeader.fDirectY);
			isclockwise = polylineEllipseHeader.bIsClockwise > 0;
			shortradius = polylineEllipseHeader.fShortRadius;
			longradius = polylineEllipseHeader.fLongRadius;
		}
	}

	public override void Load(string text)
	{
		base.Load(text);
		string[] array = text.Split(',');
		double x = double.Parse(array[1]);
		double y = double.Parse(array[2]);
		double x2 = double.Parse(array[3]);
		double y2 = double.Parse(array[4]);
		center = From + new Vector(x, y);
		direction = new Vector(x2, y2);
		isclockwise = bool.Parse(array[5]);
		shortradius = double.Parse(array[6]);
		longradius = double.Parse(array[7]);
		LoadArgument(array, 8);
	}

	public override void Load(IPolylineEntity that)
	{
		base.Load(that);
		if (that is IPolylineEllipse)
		{
			IPolylineEllipse polylineEllipse = (IPolylineEllipse)that;
			center = polylineEllipse.Center;
			isclockwise = polylineEllipse.IsClockwise;
			direction = polylineEllipse.Direction;
			shortradius = polylineEllipse.ShortRadius;
			longradius = polylineEllipse.LongRadius;
		}
	}
}
