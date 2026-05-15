using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SamSoarII.Polyline;

namespace SamCAD.Control;

public class GroupMatrixImage : UserControl
{
	private enum Directions
	{
		Up,
		Down,
		Left,
		Right
	}

	private static readonly Brush Brush_Start = new SolidColorBrush(new Color
	{
		A = byte.MaxValue,
		R = 128,
		G = 32,
		B = 32
	});

	private static readonly Brush Brush_Normal = new SolidColorBrush(new Color
	{
		A = byte.MaxValue,
		R = 32,
		G = 32,
		B = 32
	});

	private static readonly Pen Pen_Start = new Pen(Brush_Start, 1.5);

	private static readonly Pen Pen_Normal = new Pen(Brush_Normal, 0.75);

	private static readonly Pen Pen_Dash = new Pen(Brush_Normal, 0.75)
	{
		DashStyle = new DashStyle(new double[2] { 4.0, 4.0 }, 0.0)
	};

	private static readonly double Width_One = 32.0;

	private static readonly double Height_One = 24.0;

	private static readonly double Width_OneAll = 36.0;

	private static readonly double Height_OneAll = 28.0;

	private static readonly double Radius_One = 2.0;

	private GroupMatrixWindow parent;

	public GroupMatrixWindow ViewParent
	{
		get
		{
			return parent;
		}
		set
		{
			parent = value;
		}
	}

	protected void DrawDashH(DrawingContext ctx, int y, int x1, int x2)
	{
		double y2 = (double)y * Height_OneAll + Height_One * 0.5;
		double x3 = (double)x1 * Width_OneAll + Width_One * 0.5;
		double x4 = (double)x2 * Width_OneAll + Width_One * 0.5;
		ctx.DrawLine(Pen_Dash, new Point(x3, y2), new Point(x4, y2));
	}

	protected void DrawDashV(DrawingContext ctx, int x, int y1, int y2)
	{
		double x2 = (double)x * Width_OneAll + Width_One * 0.5;
		double y3 = (double)y1 * Height_OneAll + Height_One * 0.5;
		double y4 = (double)y2 * Height_OneAll + Height_One * 0.5;
		ctx.DrawLine(Pen_Dash, new Point(x2, y3), new Point(x2, y4));
	}

	protected void DrawDash(DrawingContext ctx, int x1, int y1, int x2, int y2)
	{
		double x3 = (double)x1 * Width_OneAll + Width_One * 0.5;
		double x4 = (double)x2 * Width_OneAll + Width_One * 0.5;
		double y3 = (double)y1 * Height_OneAll + Height_One * 0.5;
		double y4 = (double)y2 * Height_OneAll + Height_One * 0.5;
		ctx.DrawLine(Pen_Dash, new Point(x3, y3), new Point(x4, y4));
	}

	protected override void OnRender(DrawingContext ctx)
	{
		base.OnRender(ctx);
		if (parent == null)
		{
			return;
		}
		int num = Math.Min(parent.Row, 5);
		int num2 = Math.Min(parent.Column, 6);
		Point offset = parent.Offset;
		MatrixStrategy strategy = parent.Strategy;
		MatrixPriority priority = parent.Priority;
		if (offset.Y > 0.0)
		{
			ctx.PushTransform(new MatrixTransform(1.0, 0.0, 0.0, -1.0, 0.0, base.ActualHeight));
		}
		if (offset.X < 0.0)
		{
			ctx.PushTransform(new MatrixTransform(-1.0, 0.0, 0.0, 1.0, base.ActualWidth, 0.0));
		}
		for (int i = 0; i < num2; i++)
		{
			for (int j = 0; j < num; j++)
			{
				Pen pen = ((i == 0 && j == 0) ? Pen_Start : Pen_Normal);
				ctx.DrawRoundedRectangle(null, pen, new Rect
				{
					X = (double)i * Width_OneAll,
					Y = (double)j * Height_OneAll,
					Width = Width_One,
					Height = Height_One
				}, Radius_One, Radius_One);
			}
		}
		if (num > 1 || num2 > 1)
		{
			if (num <= 1)
			{
				DrawDashH(ctx, 0, 0, num2 - 1);
			}
			else if (num2 <= 1)
			{
				DrawDashV(ctx, 0, 0, num - 1);
			}
			else
			{
				switch (strategy)
				{
				case MatrixStrategy.Sprial:
					switch (priority)
					{
					case MatrixPriority.Horizontal:
					{
						Directions directions4 = Directions.Up;
						int num7 = 0;
						int num8 = num2 - 1;
						int num9 = 1;
						int num10 = num - 1;
						DrawDashH(ctx, 0, 0, num2 - 1);
						while (num7 <= num8 && num9 <= num10)
						{
							switch (directions4)
							{
							case Directions.Up:
								DrawDashV(ctx, num8--, num9 - 1, num10);
								directions4 = Directions.Left;
								break;
							case Directions.Left:
								DrawDashH(ctx, num10--, num7, num8 + 1);
								directions4 = Directions.Down;
								break;
							case Directions.Down:
								DrawDashV(ctx, num7++, num9, num10 + 1);
								directions4 = Directions.Right;
								break;
							case Directions.Right:
								DrawDashH(ctx, num9++, num7 - 1, num8);
								directions4 = Directions.Up;
								break;
							}
						}
						break;
					}
					case MatrixPriority.Vertical:
					{
						Directions directions3 = Directions.Right;
						int num3 = 1;
						int num4 = num2 - 1;
						int num5 = 0;
						int num6 = num - 1;
						DrawDashV(ctx, 0, 0, num - 1);
						while (num3 <= num4 && num5 <= num6)
						{
							switch (directions3)
							{
							case Directions.Right:
								DrawDashH(ctx, num6--, num3 - 1, num4);
								directions3 = Directions.Down;
								break;
							case Directions.Down:
								DrawDashV(ctx, num4--, num5, num6 + 1);
								directions3 = Directions.Left;
								break;
							case Directions.Left:
								DrawDashH(ctx, num5++, num3, num4 + 1);
								directions3 = Directions.Up;
								break;
							case Directions.Up:
								DrawDashV(ctx, num3++, num5 - 1, num6);
								directions3 = Directions.Right;
								break;
							}
						}
						break;
					}
					}
					break;
				case MatrixStrategy.Fold:
					switch (priority)
					{
					case MatrixPriority.Horizontal:
					{
						Directions directions2 = Directions.Left;
						DrawDashH(ctx, 0, 0, num2 - 1);
						for (int n = 1; n < num; n++)
						{
							DrawDashH(ctx, n, 0, num2 - 1);
							switch (directions2)
							{
							case Directions.Left:
								DrawDashV(ctx, num2 - 1, n - 1, n);
								directions2 = Directions.Right;
								break;
							case Directions.Right:
								DrawDashV(ctx, 0, n - 1, n);
								directions2 = Directions.Left;
								break;
							}
						}
						break;
					}
					case MatrixPriority.Vertical:
					{
						Directions directions = Directions.Down;
						DrawDashV(ctx, 0, 0, num - 1);
						for (int m = 1; m < num2; m++)
						{
							DrawDashV(ctx, m, 0, num - 1);
							switch (directions)
							{
							case Directions.Down:
								DrawDashH(ctx, num - 1, m - 1, m);
								directions = Directions.Up;
								break;
							case Directions.Up:
								DrawDashH(ctx, 0, m - 1, m);
								directions = Directions.Down;
								break;
							}
						}
						break;
					}
					}
					break;
				case MatrixStrategy.ZipZap:
					switch (priority)
					{
					case MatrixPriority.Horizontal:
					{
						for (int l = 0; l < num; l++)
						{
							DrawDashH(ctx, l, 0, num2 - 1);
							if (l > 0)
							{
								DrawDash(ctx, num2 - 1, l - 1, 0, l);
							}
						}
						break;
					}
					case MatrixPriority.Vertical:
					{
						for (int k = 0; k < num2; k++)
						{
							DrawDashV(ctx, k, 0, num - 1);
							if (k > 0)
							{
								DrawDash(ctx, k - 1, num - 1, k, 0);
							}
						}
						break;
					}
					}
					break;
				}
			}
		}
		if (offset.Y > 0.0)
		{
			ctx.Pop();
		}
		if (offset.X < 0.0)
		{
			ctx.Pop();
		}
	}
}
