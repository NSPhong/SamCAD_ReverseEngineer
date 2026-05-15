using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace SamSoarII.Utility.DXF;

public class DXFImage : DrawingVisual
{
	public static readonly Pen BlackPen = new Pen(Brushes.Black, 1.0);

	public static readonly Pen DashPen = new Pen(Brushes.Black, 1.0)
	{
		DashStyle = new DashStyle(new List<double> { 1.0, 4.0 }, 0.0)
	};

	protected DXFImagePanel parent;

	protected DXFModel model;

	public DXFImage(DXFImagePanel _parent, DXFModel _model)
	{
		parent = _parent;
		model = _model;
	}

	public virtual void DrawImage()
	{
		using DrawingContext drawingContext = RenderOpen();
		BlackPen.Thickness = 1.0 / parent.Scale;
		DashPen.Thickness = 1.0 / parent.Scale;
		drawingContext.DrawLine(BlackPen, new Point(0.0, 0.0), new Point(10.0 / parent.Scale, 0.0));
		drawingContext.DrawLine(BlackPen, new Point(0.0, 0.0), new Point(0.0, 10.0 / parent.Scale));
		StreamGeometry streamGeometry = new StreamGeometry();
		using (StreamGeometryContext streamGeometryContext = streamGeometry.Open())
		{
			streamGeometryContext.BeginFigure(new Point(0.0, 0.0), isFilled: false, isClosed: false);
			foreach (DXFEdge item in model.Graph.Path)
			{
				if (item.Entity.Type == EntityType.Line)
				{
					DXFLine dXFLine = (DXFLine)item.Entity;
					if (!dXFLine.IsReal)
					{
						streamGeometryContext.BeginFigure(item.End.P, isFilled: false, isClosed: false);
					}
					else
					{
						item.Entity.Render(drawingContext, streamGeometryContext);
					}
				}
				else
				{
					item.Entity.Render(drawingContext, streamGeometryContext);
				}
			}
		}
		drawingContext.DrawGeometry(null, BlackPen, streamGeometry);
	}
}
