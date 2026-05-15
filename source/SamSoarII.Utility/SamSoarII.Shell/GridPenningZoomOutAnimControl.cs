using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SamSoarII.Shell;

public class GridPenningZoomOutAnimControl : UserControl
{
	public static readonly int TickMax = 16;

	private static readonly Brush Brush_Border = new SolidColorBrush(new Color
	{
		A = byte.MaxValue,
		R = 45,
		G = 58,
		B = 129
	});

	private static readonly Brush Brush_Fill = new SolidColorBrush(new Color
	{
		A = 32,
		R = 196,
		G = 199,
		B = 250
	});

	private static readonly Brush Brush_Lookbar = new SolidColorBrush(new Color
	{
		A = byte.MaxValue,
		R = 208,
		G = 208,
		B = byte.MaxValue
	});

	private static readonly Brush Brush_LookFill = new SolidColorBrush(new Color
	{
		A = byte.MaxValue,
		R = 32,
		G = 32,
		B = 32
	});

	private static readonly Pen Pen_Border = new Pen(Brush_Border, 1.5);

	private static readonly Pen Pen_Lookbar = new Pen(Brush_Lookbar, 1.5);

	private static readonly double Radius_Border = 4.0;

	private static readonly double Radius_Lookin = 16.0;

	private static readonly double Radius_Lookout = 20.0;

	private static readonly double Length_Look = 20.0;

	private static readonly double Length_Lookbar = 48.0;

	private static readonly double Thickness_Lookbar = 4.0;

	protected static readonly DependencyProperty TickProperty = DependencyProperty.Register("Tick", typeof(int), typeof(GridPenningZoomOutAnimControl), new PropertyMetadata(0, OnPropertyChanged_Tick));

	protected static readonly DependencyProperty RectProperty = DependencyProperty.Register("Rect", typeof(Rect), typeof(GridPenningZoomOutAnimControl), new PropertyMetadata(default(Rect), OnPropertyChanged_Rect));

	public int Tick
	{
		get
		{
			return (int)GetValue(TickProperty);
		}
		set
		{
			SetValue(TickProperty, value);
		}
	}

	public Rect Rect
	{
		get
		{
			return (Rect)GetValue(RectProperty);
		}
		set
		{
			SetValue(RectProperty, value);
		}
	}

	private static void OnPropertyChanged_Tick(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is GridPenningZoomOutAnimControl)
		{
			((GridPenningZoomOutAnimControl)d).OnTickChanged(e);
		}
	}

	protected virtual void OnTickChanged(DependencyPropertyChangedEventArgs e)
	{
		InvalidateVisual();
	}

	private static void OnPropertyChanged_Rect(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
	}

	protected override void OnRender(DrawingContext ctx)
	{
		base.OnRender(ctx);
		Rect rect = new Rect(0.0, 0.0, base.ActualWidth, base.ActualHeight);
		Rect rect2 = new Rect(rect.Width * Rect.X, rect.Height * Rect.Y, rect.Width * Rect.Width, rect.Height * Rect.Height);
		double num = rect.Top + (rect2.Top - rect.Top) * (double)(TickMax - Tick) / (double)TickMax;
		double num2 = rect.Bottom + (rect2.Bottom - rect.Bottom) * (double)(TickMax - Tick) / (double)TickMax;
		double num3 = rect.Left + (rect2.Left - rect.Left) * (double)(TickMax - Tick) / (double)TickMax;
		double num4 = rect.Right + (rect2.Right - rect.Right) * (double)(TickMax - Tick) / (double)TickMax;
		Rect rectangle = new Rect(num3, num, num4 - num3, num2 - num);
		Point point = new Point(rectangle.X + rectangle.Width / 2.0, rectangle.Y + rectangle.Height / 2.0);
		StreamGeometry streamGeometry = new StreamGeometry();
		RotateTransform transform = new RotateTransform(135.0, point.X, point.Y);
		using (StreamGeometryContext streamGeometryContext = streamGeometry.Open())
		{
			double num5 = 0.0;
			double num6 = Math.PI * 2.0 * (double)Tick / (double)TickMax;
			streamGeometryContext.BeginFigure(new Point(point.X, point.Y - Radius_Lookout), isFilled: true, isClosed: true);
			streamGeometryContext.LineTo(point, isStroked: true, isSmoothJoin: false);
			streamGeometryContext.LineTo(new Point(point.X + Radius_Lookout * Math.Sin(num6), point.Y + Radius_Lookout * Math.Cos(num6)), isStroked: true, isSmoothJoin: false);
			if (num6 > 5.497787143782138)
			{
				streamGeometryContext.LineTo(new Point(point.X - Radius_Lookout, point.Y - Radius_Lookout), isStroked: true, isSmoothJoin: false);
			}
			if (num6 > 3.9269908169872414)
			{
				streamGeometryContext.LineTo(new Point(point.X - Radius_Lookout, point.Y + Radius_Lookout), isStroked: true, isSmoothJoin: false);
			}
			if (num6 > Math.PI * 3.0 / 4.0)
			{
				streamGeometryContext.LineTo(new Point(point.X + Radius_Lookout, point.Y + Radius_Lookout), isStroked: true, isSmoothJoin: false);
			}
			if (num6 > Math.PI / 4.0)
			{
				streamGeometryContext.LineTo(new Point(point.X + Radius_Lookout, point.Y - Radius_Lookout), isStroked: true, isSmoothJoin: false);
			}
		}
		ctx.DrawRoundedRectangle(Brush_Fill, Pen_Border, rectangle, Radius_Border, Radius_Border);
		ctx.PushTransform(transform);
		ctx.DrawRectangle(Brush_Lookbar, null, new Rect(point.X - Thickness_Lookbar / 2.0, point.Y - Length_Lookbar, Thickness_Lookbar, Length_Lookbar));
		ctx.Pop();
		ctx.DrawEllipse(Brush_LookFill, Pen_Lookbar, point, Radius_Lookout, Radius_Lookout);
		ctx.PushClip(streamGeometry);
		ctx.DrawEllipse(Brush_Lookbar, null, point, Radius_Lookout, Radius_Lookout);
		ctx.Pop();
		ctx.DrawEllipse(Brush_LookFill, Pen_Lookbar, point, Radius_Lookin, Radius_Lookin);
		ctx.DrawRectangle(Brush_Lookbar, null, new Rect(point.X - Length_Look / 2.0, point.Y - Thickness_Lookbar / 2.0, Length_Look, Thickness_Lookbar));
	}
}
