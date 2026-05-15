using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SamSoarII.Shell;

public class GridPenningPanelGlobalDrawing : Panel
{
	public static readonly DashStyle Line_DashStyle = new DashStyle(new double[2] { 5.0, 5.0 }, 0.0);

	public static readonly double ArrowHeight = 16.0;

	public static readonly double ArrowWidth = 4.0;

	private GridPenningPanel parent;

	private DrawingVisual dwvisual;

	public GridPenningPanel ViewParent => parent;

	public GridPenning Penning => parent?.ViewParent;

	public IGridPenningSource DrawingSource => parent?.DrawingSource;

	protected override int VisualChildrenCount => (dwvisual != null) ? 1 : 0;

	public GridPenningPanelGlobalDrawing(GridPenningPanel _parent)
	{
		parent = _parent;
		dwvisual = new DrawingVisual();
		AddLogicalChild(dwvisual);
		AddVisualChild(dwvisual);
	}

	protected override Visual GetVisualChild(int index)
	{
		return dwvisual;
	}

	protected Point ToVisualPoint(Point op, Rect dwrect)
	{
		Vector vector = op - dwrect.TopLeft;
		return new Point(vector.X * parent.ActualWidth / dwrect.Width, vector.Y * parent.ActualHeight / dwrect.Height);
	}

	protected void DrawingArrow(Point pcenter, Vector vdirect, StreamGeometryContext stmctx)
	{
		if (vdirect.Length != 0.0)
		{
			vdirect *= ArrowHeight / vdirect.Length;
			Vector vector = new Vector(vdirect.Y, 0.0 - vdirect.X);
			Vector vector2 = new Vector(0.0 - vdirect.Y, vdirect.X);
			vector *= ArrowWidth * 0.5 / ArrowHeight;
			vector2 *= ArrowWidth * 0.5 / ArrowHeight;
			Point startPoint = pcenter + vdirect;
			Point point = pcenter + vector;
			Point point2 = pcenter + vector2;
			stmctx.BeginFigure(startPoint, isFilled: true, isClosed: true);
			stmctx.LineTo(point, isStroked: true, isSmoothJoin: true);
			stmctx.LineTo(point2, isStroked: true, isSmoothJoin: true);
		}
	}

	public void DrawingAll()
	{
		using DrawingContext drawingContext = dwvisual.RenderOpen();
		Rect drawingRectAll = parent.GetDrawingRectAll();
		uint rGBA_Foreground = parent.RGBA_Foreground;
		Brush brush = new SolidColorBrush(new Color
		{
			A = (byte)(rGBA_Foreground >> 24),
			R = (byte)(rGBA_Foreground >> 16),
			G = (byte)(rGBA_Foreground >> 8),
			B = (byte)rGBA_Foreground
		});
		Pen pen = new Pen(brush, GridPenningPanel.PenThickness);
		StreamGeometry streamGeometry = new StreamGeometry();
		StreamGeometryContext streamGeometryContext = streamGeometry.Open();
		Pen pen2 = new Pen(new SolidColorBrush(new Color
		{
			A = (byte)(rGBA_Foreground >> 24),
			R = (byte)(rGBA_Foreground >> 16),
			G = (byte)(rGBA_Foreground >> 8),
			B = (byte)rGBA_Foreground
		}), 1.0)
		{
			DashStyle = Line_DashStyle
		};
		StreamGeometry streamGeometry2 = new StreamGeometry();
		StreamGeometryContext streamGeometryContext2 = streamGeometry2.Open();
		IEnumerable<IGridPenningEntity> entities = DrawingSource.GetEntities(drawingRectAll);
		foreach (IGridPenningEntity item in entities)
		{
			if (item is IGridPenningLine && Penning.DrawingMode == GridPenningDrawingMode.WPFDevice)
			{
				IGridPenningLine gridPenningLine = (IGridPenningLine)item;
				Point point = ToVisualPoint(gridPenningLine.Start, drawingRectAll);
				Point point2 = ToVisualPoint(gridPenningLine.End, drawingRectAll);
				if (gridPenningLine.IsReal)
				{
					if (!point.Equals(point2))
					{
						streamGeometryContext.BeginFigure(point, isFilled: false, isClosed: false);
						streamGeometryContext.LineTo(point2, isStroked: true, isSmoothJoin: false);
					}
				}
				else if (!point.Equals(point2))
				{
					streamGeometryContext2.BeginFigure(point, isFilled: false, isClosed: false);
					streamGeometryContext2.LineTo(point2, isStroked: true, isSmoothJoin: false);
				}
				if (Penning.IsArrowVisible)
				{
					Vector vector = (point2 - point) * 0.5;
					Point pcenter = point + vector;
					DrawingArrow(pcenter, vector, streamGeometryContext);
				}
			}
			else if (item is IGridPenningArch && Penning.DrawingMode == GridPenningDrawingMode.WPFDevice)
			{
				IGridPenningArch gridPenningArch = (IGridPenningArch)item;
				Point point3 = ToVisualPoint(gridPenningArch.Start, drawingRectAll);
				Point point4 = ToVisualPoint(gridPenningArch.End, drawingRectAll);
				Vector vector2 = gridPenningArch.End - gridPenningArch.Start;
				Vector vector3 = gridPenningArch.Start - gridPenningArch.Center;
				double num = Vector.CrossProduct(vector2, vector3);
				if (point3.Equals(point4))
				{
					continue;
				}
				double num2 = gridPenningArch.EndAngle - gridPenningArch.StartAngle;
				double num3 = gridPenningArch.Radius * parent.ActualWidth / drawingRectAll.Width;
				double num4 = gridPenningArch.Radius * parent.ActualHeight / drawingRectAll.Height;
				for (; num2 < 0.0; num2 += Math.PI * 2.0)
				{
				}
				streamGeometryContext.BeginFigure(point4, isFilled: false, isClosed: false);
				streamGeometryContext.ArcTo(point3, new Size(num3, num4), 0.0, gridPenningArch.IsLarge, gridPenningArch.Clockwise ? SweepDirection.Clockwise : SweepDirection.Counterclockwise, isStroked: true, isSmoothJoin: false);
				if (!Penning.IsArrowVisible)
				{
					continue;
				}
				double num5 = gridPenningArch.StartAngle;
				double num6 = gridPenningArch.EndAngle;
				if (!gridPenningArch.Clockwise)
				{
					for (; num6 < num5; num6 += Math.PI * 2.0)
					{
					}
				}
				else
				{
					for (; num5 < num6; num5 += Math.PI * 2.0)
					{
					}
				}
				double num7 = (num5 + num6) * 0.5;
				Point point5 = ToVisualPoint(gridPenningArch.Center, drawingRectAll);
				Point point6 = new Point(point5.X + Math.Cos(num7) * num3, point5.Y + Math.Sin(num7) * num4);
				Vector vector4 = point6 - point5;
				vector4 = ((!gridPenningArch.Clockwise) ? new Vector(0.0 - vector4.Y, vector4.X) : new Vector(vector4.Y, 0.0 - vector4.X));
				vector4 = new Vector(vector4.X * num3, vector4.Y * num4);
				point6 -= vector4 * ArrowHeight * 0.5 / vector4.Length;
				DrawingArrow(point6, vector4, streamGeometryContext);
			}
			else if (item is IGridPenningCircle && Penning.DrawingMode == GridPenningDrawingMode.WPFDevice)
			{
				IGridPenningCircle gridPenningCircle = (IGridPenningCircle)item;
				Point center = ToVisualPoint(gridPenningCircle.Center, drawingRectAll);
				double num8 = gridPenningCircle.Radius * parent.ActualWidth / drawingRectAll.Width;
				double num9 = gridPenningCircle.Radius * parent.ActualHeight / drawingRectAll.Height;
				drawingContext.DrawEllipse(null, pen, center, num8, num9);
				if (Penning.IsArrowVisible)
				{
					Point pcenter2 = new Point(center.X + num8, center.Y);
					Point pcenter3 = new Point(center.X, center.Y + num9);
					Point pcenter4 = new Point(center.X - num8, center.Y);
					Point pcenter5 = new Point(center.X, center.Y - num9);
					DrawingArrow(pcenter2, new Vector(0.0, (!gridPenningCircle.Clockwise) ? 1 : (-1)), streamGeometryContext);
					DrawingArrow(pcenter3, new Vector(gridPenningCircle.Clockwise ? 1 : (-1), 0.0), streamGeometryContext);
					DrawingArrow(pcenter4, new Vector(0.0, gridPenningCircle.Clockwise ? 1 : (-1)), streamGeometryContext);
					DrawingArrow(pcenter5, new Vector((!gridPenningCircle.Clockwise) ? 1 : (-1), 0.0), streamGeometryContext);
				}
			}
			else if (item is IGridPenningEllipseArch)
			{
				IGridPenningEllipseArch gridPenningEllipseArch = (IGridPenningEllipseArch)item;
				Point point7 = ToVisualPoint(gridPenningEllipseArch.Center, drawingRectAll);
				Point point8 = ToVisualPoint(gridPenningEllipseArch.Start, drawingRectAll);
				Point point9 = ToVisualPoint(gridPenningEllipseArch.End, drawingRectAll);
				double num10 = Vector.AngleBetween(new Vector(1.0, 0.0), gridPenningEllipseArch.Direction);
				num10 *= Math.PI / 180.0;
				double longRadius = gridPenningEllipseArch.LongRadius;
				double shortRadius = gridPenningEllipseArch.ShortRadius;
				longRadius = Math.Pow(longRadius * Math.Cos(num10) * parent.ActualWidth / drawingRectAll.Width, 2.0) + Math.Pow(longRadius * Math.Sin(num10) * parent.ActualHeight / drawingRectAll.Height, 2.0);
				longRadius = Math.Sqrt(longRadius);
				shortRadius = Math.Pow(shortRadius * Math.Sin(num10) * parent.ActualWidth / drawingRectAll.Width, 2.0) + Math.Pow(shortRadius * Math.Cos(num10) * parent.ActualHeight / drawingRectAll.Height, 2.0);
				shortRadius = Math.Sqrt(shortRadius);
				if (point8.Equals(point9))
				{
					continue;
				}
				streamGeometryContext.BeginFigure(point9, isFilled: false, isClosed: false);
				streamGeometryContext.ArcTo(point8, new Size(longRadius, shortRadius), num10, gridPenningEllipseArch.IsLarge, gridPenningEllipseArch.Clockwise ? SweepDirection.Clockwise : SweepDirection.Counterclockwise, isStroked: true, isSmoothJoin: false);
				if (!Penning.IsArrowVisible)
				{
					continue;
				}
				double num11 = gridPenningEllipseArch.StartAngle;
				double num12 = gridPenningEllipseArch.EndAngle;
				if (!gridPenningEllipseArch.Clockwise)
				{
					for (; num12 < num11; num12 += Math.PI * 2.0)
					{
					}
				}
				else
				{
					for (; num11 < num12; num11 += Math.PI * 2.0)
					{
					}
				}
				double num13 = (num11 + num12) * 0.5;
				Vector vector5 = new Vector(gridPenningEllipseArch.LongRadius * Math.Cos(num13 - num10), gridPenningEllipseArch.ShortRadius * Math.Sin(num13 - num10));
				vector5 = new Vector(vector5.X * Math.Sin(num10) + vector5.Y * Math.Cos(num10), vector5.X * Math.Cos(num10) - vector5.Y * Math.Sin(num10));
				Point pcenter6 = ToVisualPoint(gridPenningEllipseArch.Center + vector5, drawingRectAll);
				vector5 = ((!gridPenningEllipseArch.Clockwise) ? new Vector(0.0 - vector5.Y, vector5.X) : new Vector(vector5.Y, 0.0 - vector5.X));
				DrawingArrow(pcenter6, vector5, streamGeometryContext);
			}
			else if (item is IGridPenningEllipse)
			{
				IGridPenningEllipse gridPenningEllipse = (IGridPenningEllipse)item;
				Point center2 = ToVisualPoint(gridPenningEllipse.Center, drawingRectAll);
				double num14 = Vector.AngleBetween(new Vector(1.0, 0.0), gridPenningEllipse.Direction);
				num14 *= Math.PI / 180.0;
				double longRadius2 = gridPenningEllipse.LongRadius;
				double shortRadius2 = gridPenningEllipse.ShortRadius;
				longRadius2 = Math.Pow(longRadius2 * Math.Cos(num14) * parent.ActualWidth / drawingRectAll.Width, 2.0) + Math.Pow(longRadius2 * Math.Sin(num14) * parent.ActualHeight / drawingRectAll.Height, 2.0);
				longRadius2 = Math.Sqrt(longRadius2);
				shortRadius2 = Math.Pow(shortRadius2 * Math.Sin(num14) * parent.ActualWidth / drawingRectAll.Width, 2.0) + Math.Pow(shortRadius2 * Math.Cos(num14) * parent.ActualHeight / drawingRectAll.Height, 2.0);
				shortRadius2 = Math.Sqrt(shortRadius2);
				drawingContext.PushTransform(new RotateTransform(num14 * 180.0 / Math.PI, center2.X, center2.Y));
				drawingContext.DrawEllipse(null, pen, center2, longRadius2, shortRadius2);
				drawingContext.Pop();
				if (Penning.IsArrowVisible)
				{
					Vector direction = gridPenningEllipse.Direction;
					Vector vector6 = new Vector(0.0 - direction.Y, direction.X);
					direction *= gridPenningEllipse.LongRadius / direction.Length;
					vector6 *= gridPenningEllipse.ShortRadius / vector6.Length;
					Point pcenter7 = ToVisualPoint(gridPenningEllipse.Center + direction, drawingRectAll);
					Point pcenter8 = ToVisualPoint(gridPenningEllipse.Center + vector6, drawingRectAll);
					Point pcenter9 = ToVisualPoint(gridPenningEllipse.Center - direction, drawingRectAll);
					Point pcenter10 = ToVisualPoint(gridPenningEllipse.Center - vector6, drawingRectAll);
					DrawingArrow(pcenter7, new Vector(0.0 - direction.Y, direction.X) * ((!gridPenningEllipse.Clockwise) ? 1 : (-1)), streamGeometryContext);
					DrawingArrow(pcenter8, new Vector(0.0 - vector6.Y, vector6.X) * ((!gridPenningEllipse.Clockwise) ? 1 : (-1)), streamGeometryContext);
					DrawingArrow(pcenter9, new Vector(direction.Y, 0.0 - direction.X) * ((!gridPenningEllipse.Clockwise) ? 1 : (-1)), streamGeometryContext);
					DrawingArrow(pcenter10, new Vector(vector6.Y, 0.0 - vector6.X) * ((!gridPenningEllipse.Clockwise) ? 1 : (-1)), streamGeometryContext);
				}
			}
		}
		streamGeometryContext.Close();
		streamGeometryContext2.Close();
		drawingContext.DrawGeometry(brush, pen, streamGeometry);
		drawingContext.DrawGeometry(null, pen2, streamGeometry2);
	}
}
