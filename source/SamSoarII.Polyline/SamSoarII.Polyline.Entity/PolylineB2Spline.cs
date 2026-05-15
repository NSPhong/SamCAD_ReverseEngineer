using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using SamSoarII.Shell;
using SamSoarII.Utility.Math;

namespace SamSoarII.Polyline.Entity;

public class PolylineB2Spline : PolylineEntity, IPolylineB2Spline, IPolylineEntity, INotifyPropertyChanged, IDisposable, IGridPenningEntity
{
	private List<Point> points;

	private List<bool> locks;

	public override PolylineType Type => PolylineType.B2Spline;

	public override Point To
	{
		get
		{
			return base.To;
		}
		set
		{
			Point p = value;
			TryMove(points.Count() - 1, ref p);
			base.To = p;
		}
	}

	public IList<Point> Points => points;

	public IList<bool> Locks => locks;

	public override IEnumerable<IPolylineControlPoint> ControlPoints
	{
		get
		{
			for (int i = 0; i < points.Count(); i++)
			{
				yield return new PolylineControlPoint(this, i + 2);
			}
		}
	}

	public PolylineB2Spline()
	{
		points = new List<Point>();
		locks = new List<bool>();
		Add();
	}

	public PolylineB2Spline(IPolylineImage _image, int _id, Point _to)
		: base(_image, _id, _to)
	{
		points = new List<Point>();
		locks = new List<bool>();
		Add();
	}

	protected override string _GetName()
	{
		return $"{base.ID}.B2 spline";
	}

	public void Add()
	{
		Point point = ((points.Count() > 0) ? points.LastOrDefault() : From);
		Vector vector = ((points.Count() > 0) ? (points[points.Count() - 1] - points[points.Count() - 2]) : new Vector(1.0, 0.0));
		points.Add(point + vector);
		points.Add(point + vector + vector);
		locks.Add(item: false);
		locks.Add(item: false);
	}

	public void TryMove(int index, ref Point p)
	{
		if ((index & 1) == 0)
		{
			Point point = ((index == 0) ? From : points[index - 1]);
			Point point2 = points[index + 1];
			bool flag = index == 0 || locks[index - 1];
			bool flag2 = locks[index + 1];
			Point point3 = ((index == 0) ? default(Point) : points[index - 2]);
			Point point4 = ((index + 2 >= points.Count()) ? default(Point) : points[index + 2]);
			bool flag3 = index != 0 && locks[index - 2];
			bool flag4 = index + 2 < points.Count() && locks[index + 2];
			if (flag3 && flag4)
			{
				p = points[index];
				return;
			}
			if (flag3 && !base.Parent.IsEqualP(point, point3))
			{
				Vector vector = point - point3;
				Vector vector2 = p - point3;
				vector /= vector.Length;
				double num = Vector.CrossProduct(vector, vector2);
				p += new Vector(vector.Y, 0.0 - vector.X) * num;
			}
			else if (flag4 && !base.Parent.IsEqualP(point2, point4))
			{
				Vector vector3 = point2 - point4;
				Vector vector4 = p - point4;
				vector3 /= vector3.Length;
				double num2 = Vector.CrossProduct(vector3, vector4);
				p += new Vector(vector3.Y, 0.0 - vector3.X) * num2;
			}
			if (!flag3 && index > 0)
			{
				Point point5 = ((index <= 2) ? From : points[index - 3]);
				Point cross = MathHelper.GetCross(point5, point3, p, point);
				Vector vector5 = point3 - point5;
				Vector vector6 = cross - point5;
				double num3 = Vector.Multiply(vector5, vector6);
				if (num3 < 1E-08)
				{
					vector6 = point - point3;
					p = MathHelper.GetCross(point, point + vector6, point2, p);
				}
				points[index - 2] = cross;
			}
			if (!flag4 && index + 2 < points.Count())
			{
				Point point6 = points[index + 3];
				Point cross2 = MathHelper.GetCross(point6, point4, p, point2);
				Vector vector7 = point4 - point6;
				Vector vector8 = cross2 - point6;
				double num4 = Vector.Multiply(vector7, vector8);
				if (num4 < 1E-08)
				{
					vector8 = point2 - point4;
					p = MathHelper.GetCross(point2, point2 + vector8, point, p);
				}
				points[index + 2] = cross2;
			}
		}
		else
		{
			Point point7 = points[index - 1];
			Point point8 = ((index + 1 >= points.Count()) ? default(Point) : points[index + 1]);
			bool flag5 = locks[index - 1];
			bool flag6 = index + 1 < locks.Count() && locks[index + 1];
			Point point9 = ((index == 1) ? From : points[index - 2]);
			Point point10 = ((index + 2 >= points.Count()) ? default(Point) : points[index + 2]);
			bool flag7 = index == 1 || locks[index - 2];
			bool flag8 = index + 2 >= locks.Count() || locks[index + 2];
			if (flag5 && flag6)
			{
				Vector vector9 = point8 - point7;
				Vector vector10 = p - point7;
				vector9 /= vector9.Length;
				double num5 = Vector.CrossProduct(vector9, vector10);
				vector10 = new Vector(vector9.Y, 0.0 - vector9.X) * num5;
				p += vector10;
				vector9 = point7 - p;
				vector10 = point8 - p;
				double num6 = Vector.Multiply(vector9, vector10);
				if (num6 > -1E-08)
				{
					p = points[index];
				}
				else
				{
					points[index] = p;
				}
				return;
			}
			if (index - 1 >= 0 && index + 1 < points.Count())
			{
				if (!flag6)
				{
					Point cross3 = MathHelper.GetCross(point7, p, point10, point8);
					Vector vector11 = point8 - point10;
					Vector vector12 = cross3 - point10;
					double num7 = Vector.Multiply(vector11, vector12);
					if (num7 < 1E-08)
					{
						p = points[index];
						return;
					}
					points[index] = p;
					points[index + 1] = cross3;
					return;
				}
				if (!flag5)
				{
					Point cross4 = MathHelper.GetCross(point8, p, point9, point7);
					Vector vector13 = point7 - point9;
					Vector vector14 = cross4 - point9;
					double num8 = Vector.Multiply(vector13, vector14);
					if (num8 < 1E-08)
					{
						p = points[index];
						return;
					}
					points[index] = p;
					points[index - 1] = cross4;
					return;
				}
			}
		}
		points[index] = p;
	}
}
