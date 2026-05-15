using System;
using System.ComponentModel;
using System.Text;
using System.Windows;
using SamSoarII.Core.Files;
using SamSoarII.Polyline.Expands;
using SamSoarII.Shell;

namespace SamSoarII.Polyline.Entity;

public class PolylineLine : PolylineEntity, IPolylineLine, IPolylineEntity, INotifyPropertyChanged, IDisposable, IGridPenningEntity, IGridPenningLine
{
	public override PolylineType Type => PolylineType.Line;

	Point IGridPenningLine.Start => From;

	Point IGridPenningLine.End => To;

	public PolylineLine()
	{
	}

	public PolylineLine(IPolylineImage _parent, int _id, Point _to, bool _isreal = true)
		: base(_parent, _id, _to, _isreal)
	{
	}

	protected override string _GetName()
	{
		return string.Format("{0}.{1}", base.ID, base.IsReal ? "Straight line" : "Dotted line");
	}

	public override LocatedFileHeader AllocHeader()
	{
		return FileFormat.AllocHeader(FileHeaderTypes.PolylineLine);
	}

	public override void Save(PolylineEntityHeader header)
	{
		base.Save(header);
		header.bEntityType = 1;
		if (header is PolylineLineHeader)
		{
			PolylineLineHeader polylineLineHeader = (PolylineLineHeader)header;
			polylineLineHeader.fTargetX = (float)To.X;
			polylineLineHeader.fTargetY = (float)To.Y;
		}
	}

	public override void Load(PolylineEntityHeader header)
	{
		base.Load(header);
		if (header is PolylineLineHeader)
		{
			PolylineLineHeader polylineLineHeader = (PolylineLineHeader)header;
			To = new Point(polylineLineHeader.fTargetX, polylineLineHeader.fTargetY);
		}
	}

	public override void Save(DownloadWriter dw)
	{
		dw.Write((byte)0);
		dw.Write((byte)(base.IsReal ? 1u : 0u));
		dw.Write((float)To.X);
		dw.Write((float)To.Y);
		SaveArgument(dw);
	}

	public override void Load(UploadReader ur)
	{
		base.Load(ur);
		base.IsReal = ur.Read8() > 0;
		float num = ur.Read32F();
		float num2 = ur.Read32F();
		To = new Point(num, num2);
		LoadArgument(ur);
	}

	public override void Save(StringBuilder sb)
	{
		base.Save(sb);
		Vector vector = To - From;
		sb.Append("LINE,");
		sb.Append(base.IsReal);
		sb.Append(",");
		sb.Append(vector.X);
		sb.Append(",");
		sb.Append(vector.Y);
		sb.Append(",");
		SaveArgument(sb);
	}

	public override void Load(string text)
	{
		base.Load(text);
		string[] array = text.Split(',');
		base.IsReal = bool.Parse(array[1]);
		double x = double.Parse(array[2]);
		double y = double.Parse(array[3]);
		Vector vector = new Vector(x, y);
		To = From + vector;
		LoadArgument(array, 4);
	}

	public override IPolylineEntity Clone()
	{
		PolylineLine polylineLine = new PolylineLine(base.Parent, base.ID, To, base.IsReal);
		polylineLine.Load(this);
		return polylineLine;
	}

	public override void Load(IPolylineEntity that)
	{
		base.Load(that);
		To = that.To;
		base.IsReal = that.IsReal;
		base.IsSlot = that.IsSlot;
	}

	public override double GetDist(Point p)
	{
		Vector vector = To - From;
		Vector vector2 = new Vector(0.0 - vector.Y, vector.X);
		double length = vector.Length;
		Vector vector3 = p - From;
		Vector vector4 = p - To;
		double num = Vector.CrossProduct(vector2, vector3);
		double num2 = Vector.CrossProduct(vector2, vector4);
		if (num * num2 > 0.0)
		{
			return double.MaxValue;
		}
		if (length == 0.0)
		{
			return vector3.Length;
		}
		double value = Vector.CrossProduct(vector, vector3);
		value = Math.Abs(value);
		return value / vector.Length;
	}

	public override Point ReflectInline(Point p, bool xlock, bool ylock)
	{
		if (xlock && ylock)
		{
			return base.ReflectInline(p, xlock, ylock);
		}
		Vector vector = To - From;
		double num = Math.Min(From.X, To.X);
		double num2 = Math.Max(From.X, To.X);
		double num3 = Math.Min(From.Y, To.Y);
		double num4 = Math.Max(From.Y, To.Y);
		if (xlock)
		{
			if (p.X < num || p.X > num2)
			{
				return base.ReflectInline(p, xlock, ylock);
			}
			p.Y = From.Y + (To.Y - From.Y) * (p.X - From.X) / (To.X - From.X);
			return p;
		}
		if (ylock)
		{
			if (p.Y < num3 || p.Y > num4)
			{
				return base.ReflectInline(p, xlock, ylock);
			}
			p.X = From.X + (To.X - From.X) * (p.Y - From.Y) / (To.Y - From.Y);
			return p;
		}
		vector /= vector.Length;
		Vector vector2 = p - From;
		double num5 = Vector.CrossProduct(vector, vector2);
		Vector vector3 = new Vector(vector.Y, 0.0 - vector.X) * num5;
		return p + vector3;
	}

	public override IPolylineExpand Expand(double r)
	{
		Vector value = LawerFrom(r).Value;
		return new PolylineExpandLine((PolylineImage)base.Parent, base.ID)
		{
			P0 = From + value,
			P1 = To + value
		};
	}

	public override Vector? LawerFrom(double r)
	{
		Vector vector = To - From;
		vector = new Vector(0.0 - vector.Y, vector.X);
		return vector * (r / vector.Length);
	}

	public override Vector? LawerTo(double r)
	{
		return LawerFrom(r);
	}
}
