using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SamSoarII.Utility.DXF;

public class DXFImagePanel : Panel
{
	private DXFModel core;

	private DXFImage image;

	private DXFDashImage dashimage;

	private double xmin;

	private double xmax;

	private double ymin;

	private double ymax;

	private double scale;

	private bool isshowdash;

	public DXFModel Core => core;

	public DXFImage MainImage => image;

	public DXFDashImage DashImage => dashimage;

	public double XMin => xmin;

	public double XMax => xmax;

	public double YMin => ymin;

	public double YMax => ymax;

	public double Scale
	{
		get
		{
			return scale;
		}
		set
		{
			scale = value;
			DrawImage();
			base.RenderTransform = new MatrixTransform(scale, 0.0, 0.0, 0.0 - scale, (0.0 - xmin) * scale, ymax * scale);
			base.Width = System.Math.Max(System.Math.Min(scale * (xmax - xmin), 800.0), 0.0);
			base.Height = System.Math.Max(System.Math.Min(scale * (ymax - ymin), 600.0), 0.0);
		}
	}

	public bool IsShowDash
	{
		get
		{
			return isshowdash;
		}
		set
		{
			if (isshowdash && !value)
			{
				RemoveLogicalChild(dashimage);
				RemoveVisualChild(dashimage);
			}
			if (!isshowdash && value)
			{
				AddLogicalChild(dashimage);
				AddVisualChild(dashimage);
				dashimage?.DrawImage();
			}
			isshowdash = value;
		}
	}

	protected override int VisualChildrenCount => (!isshowdash) ? 1 : 2;

	public DXFImagePanel(DXFModel _core)
	{
		core = _core;
		image = new DXFImage(this, core);
		dashimage = new DXFDashImage(this, core);
		isshowdash = false;
		scale = 1.0;
		xmin = double.MaxValue;
		xmax = double.MinValue;
		ymin = double.MaxValue;
		ymax = double.MinValue;
		foreach (DXFEdge item in core.Graph.Path)
		{
			_MeasureDimension(item.Entity);
		}
		AddLogicalChild(image);
		AddLogicalChild(dashimage);
		Scale = 1.0;
	}

	protected override Visual GetVisualChild(int index)
	{
		return index switch
		{
			0 => image, 
			1 => isshowdash ? dashimage : null, 
			_ => null, 
		};
	}

	private void _MeasureDimension(DXFEntity entity)
	{
		switch (entity.Type)
		{
		case EntityType.Line:
		{
			DXFLine dXFLine = (DXFLine)entity;
			_MeasureDimension(dXFLine.StartP);
			_MeasureDimension(dXFLine.EndP);
			break;
		}
		case EntityType.Arc:
		{
			DXFArc dXFArc = (DXFArc)entity;
			_MeasureDimension(new Point(dXFArc.CenterP.X - dXFArc.radius, dXFArc.CenterP.Y));
			_MeasureDimension(new Point(dXFArc.CenterP.X + dXFArc.radius, dXFArc.CenterP.Y));
			_MeasureDimension(new Point(dXFArc.CenterP.X, dXFArc.CenterP.Y - dXFArc.radius));
			_MeasureDimension(new Point(dXFArc.CenterP.X, dXFArc.CenterP.Y + dXFArc.radius));
			break;
		}
		case EntityType.Circle:
		{
			DXFCircle dXFCircle = (DXFCircle)entity;
			_MeasureDimension(new Point(dXFCircle.CenterP.X - dXFCircle.radius, dXFCircle.CenterP.Y));
			_MeasureDimension(new Point(dXFCircle.CenterP.X + dXFCircle.radius, dXFCircle.CenterP.Y));
			_MeasureDimension(new Point(dXFCircle.CenterP.X, dXFCircle.CenterP.Y - dXFCircle.radius));
			_MeasureDimension(new Point(dXFCircle.CenterP.X, dXFCircle.CenterP.Y + dXFCircle.radius));
			break;
		}
		case EntityType.Ellipse:
		{
			DXFEllipse dXFEllipse = (DXFEllipse)entity;
			_MeasureDimension(dXFEllipse.LongP);
			_MeasureDimension(dXFEllipse.CenterP + (dXFEllipse.CenterP - dXFEllipse.LongP));
			_MeasureDimension(dXFEllipse.ShortP);
			_MeasureDimension(dXFEllipse.CenterP + (dXFEllipse.CenterP - dXFEllipse.ShortP));
			break;
		}
		case EntityType.Spline:
		{
			DXFSpline dXFSpline = (DXFSpline)entity;
			Point[] samplePoints = dXFSpline.GetSamplePoints();
			foreach (Point p in samplePoints)
			{
				_MeasureDimension(p);
			}
			break;
		}
		case EntityType.Section:
		{
			DXFSection dXFSection = (DXFSection)entity;
			break;
		}
		}
	}

	private void _MeasureDimension(Point p)
	{
		xmin = System.Math.Min(xmin, p.X - 2.0);
		xmax = System.Math.Max(xmax, p.X + 2.0);
		ymin = System.Math.Min(ymin, p.Y - 2.0);
		ymax = System.Math.Max(ymax, p.Y + 2.0);
	}

	public void DrawImage()
	{
		image?.DrawImage();
		if (isshowdash)
		{
			dashimage?.DrawImage();
		}
	}
}
