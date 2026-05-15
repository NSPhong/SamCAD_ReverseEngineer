using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SamSoarII.Polyline;

namespace SamCAD.Control;

public class GroupFillImage : UserControl
{
	public static readonly double OuterSize = 120.0;

	public static readonly double InnerSize = 40.0;

	public static readonly double SpanSize = 10.0;

	private static readonly Brush Brush_Normal = new SolidColorBrush(new Color
	{
		A = byte.MaxValue,
		R = 32,
		G = 32,
		B = 32
	});

	private static readonly Pen Pen_Normal = new Pen(Brush_Normal, 0.75);

	private static readonly Pen Pen_Dash = new Pen(Brush_Normal, 0.75)
	{
		DashStyle = new DashStyle(new double[2] { 4.0, 4.0 }, 0.0)
	};

	protected static readonly DependencyProperty StrategyProperty = DependencyProperty.Register("Strategy", typeof(FillStrategy), typeof(GroupFillImage), new PropertyMetadata(FillStrategy.Horizontal, OnPropertyChanged_Strategy));

	protected static readonly DependencyProperty IsRemoveOldProperty = DependencyProperty.Register("IsRemoveOld", typeof(bool), typeof(GroupFillImage), new PropertyMetadata(true, OnPropertyChanged_IsRemoveOld));

	public FillStrategy Strategy
	{
		get
		{
			return (FillStrategy)GetValue(StrategyProperty);
		}
		set
		{
			SetValue(StrategyProperty, value);
		}
	}

	public bool IsRemoveOld
	{
		get
		{
			return (bool)GetValue(IsRemoveOldProperty);
		}
		set
		{
			SetValue(IsRemoveOldProperty, value);
		}
	}

	public GroupFillImage()
	{
		base.MinHeight = OuterSize;
		base.MinWidth = OuterSize;
	}

	private static void OnPropertyChanged_Strategy(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is GroupFillImage)
		{
			((GroupFillImage)d).OnStrategyChanged(e);
		}
	}

	protected virtual void OnStrategyChanged(DependencyPropertyChangedEventArgs e)
	{
		InvalidateVisual();
	}

	private static void OnPropertyChanged_IsRemoveOld(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is GroupFillImage)
		{
			((GroupFillImage)d).OnIsRemoveOldChanged(e);
		}
	}

	protected virtual void OnIsRemoveOldChanged(DependencyPropertyChangedEventArgs e)
	{
		InvalidateVisual();
	}

	protected void DrawImage(DrawingContext ctx, Rect outer, Rect inner)
	{
		StreamGeometry streamGeometry = new StreamGeometry();
		using (StreamGeometryContext streamGeometryContext = streamGeometry.Open())
		{
			streamGeometryContext.BeginFigure(outer.TopLeft, isFilled: true, isClosed: true);
			streamGeometryContext.LineTo(new Point(inner.X, outer.Y), isStroked: true, isSmoothJoin: true);
			streamGeometryContext.ArcTo(new Point(inner.Right, outer.Y), new Size(inner.Width / 2.0, inner.Width / 2.0), 180.0, isLargeArc: true, SweepDirection.Counterclockwise, isStroked: true, isSmoothJoin: true);
			streamGeometryContext.LineTo(outer.TopRight, isStroked: true, isSmoothJoin: true);
			streamGeometryContext.LineTo(outer.BottomRight, isStroked: true, isSmoothJoin: true);
			streamGeometryContext.LineTo(outer.BottomLeft, isStroked: true, isSmoothJoin: true);
		}
		ctx.DrawGeometry(null, Pen_Normal, streamGeometry);
	}

	protected override void OnRender(DrawingContext ctx)
	{
		base.OnRender(ctx);
		double outerSize = OuterSize;
		double outerSize2 = OuterSize;
		double spanSize = SpanSize;
		Rect outer = new Rect(0.0, 0.0, outerSize, outerSize2);
		Rect inner = new Rect((outerSize - InnerSize) / 2.0, 0.0, InnerSize, InnerSize / 2.0);
		if (!IsRemoveOld)
		{
			DrawImage(ctx, outer, inner);
		}
		if ((Strategy & FillStrategy.Horizontal) != FillStrategy.None)
		{
			for (double num = 0.0; num < outerSize; num += SpanSize)
			{
				double num2 = 0.0;
				double y = outerSize2;
				if (num >= inner.Left && num <= inner.Right)
				{
					double x = Math.Abs(num - inner.Left - inner.Width / 2.0);
					x = Math.Sqrt(Math.Pow(InnerSize / 2.0, 2.0) - Math.Pow(x, 2.0));
					num2 += x;
				}
				ctx.DrawLine(Pen_Normal, new Point(num, num2), new Point(num, y));
			}
		}
		else if ((Strategy & FillStrategy.Vertical) != FillStrategy.None)
		{
			for (double num3 = 0.0; num3 < outerSize2; num3 += SpanSize)
			{
				double x2 = 0.0;
				double x3 = outerSize;
				if (num3 >= inner.Top && num3 <= inner.Bottom)
				{
					double x4 = Math.Abs(num3 - inner.Top);
					x4 = Math.Sqrt(Math.Pow(InnerSize / 2.0, 2.0) - Math.Pow(x4, 2.0));
					ctx.DrawLine(Pen_Normal, new Point(x2, num3), new Point(outerSize / 2.0 - x4, num3));
					ctx.DrawLine(Pen_Normal, new Point(x3, num3), new Point(outerSize / 2.0 + x4, num3));
				}
				else
				{
					ctx.DrawLine(Pen_Normal, new Point(x2, num3), new Point(x3, num3));
				}
			}
		}
		else if ((Strategy & FillStrategy.Rounding) == 0)
		{
		}
	}
}
