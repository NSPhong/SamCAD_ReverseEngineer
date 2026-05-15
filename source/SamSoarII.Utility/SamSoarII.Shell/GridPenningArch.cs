using System;
using System.Windows;
using System.Windows.Media;

namespace SamSoarII.Shell;

public class GridPenningArch : GridPenningEntity, IGridPenningArch, IGridPenningCircle, IGridPenningEntity
{
	public static readonly double Angle0 = 0.0;

	public static readonly double Angle90 = Math.PI / 2.0;

	public static readonly double Angle180 = Math.PI;

	public static readonly double Angle270 = 4.71238898038469;

	public static readonly double Angle360 = Math.PI * 2.0;

	private Point center;

	private Point start;

	private Point end;

	private double radios;

	private double startangle;

	private double endangle;

	private bool clockwise;

	private bool islarge;

	public Point Center => center;

	public Point Start => start;

	public Point End => end;

	public double Radius => radios;

	public double StartAngle => startangle;

	public double EndAngle => endangle;

	public bool Clockwise => clockwise;

	public bool IsLarge => islarge;

	public static Vector Rotate(Vector v, double angle)
	{
		Matrix matrix = new Matrix(Math.Sin(angle), Math.Cos(angle), Math.Cos(angle), 0.0 - Math.Sin(angle), 0.0, 0.0);
		return Vector.Multiply(v, matrix);
	}

	public GridPenningArch(Point _center, double _radios, double _startangle, double _endangle, bool _clockwise, bool _islarge)
	{
		base.ColorID = 0;
		center = _center;
		radios = _radios;
		startangle = _startangle;
		endangle = _endangle;
		clockwise = _clockwise;
		islarge = _islarge;
		Vector v = new Vector(0.0, radios);
		start = center + Rotate(v, startangle);
		end = center + Rotate(v, endangle);
	}

	public GridPenningArch(Point _center, Point _start, Point _end, bool _clockwise, bool _islarge)
	{
		center = _center;
		start = _start;
		end = _end;
		clockwise = _clockwise;
		islarge = _islarge;
		Vector vector = new Vector(0.0, radios);
		Vector vector2 = start - center;
		Vector vector3 = end - center;
		radios = vector2.Length;
		startangle = Vector.AngleBetween(vector, vector2);
		endangle = Vector.AngleBetween(vector, vector3);
	}
}
