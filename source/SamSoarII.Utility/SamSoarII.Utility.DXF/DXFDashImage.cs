using System.Windows;
using System.Windows.Media;

namespace SamSoarII.Utility.DXF;

public class DXFDashImage : DXFImage
{
	public DXFDashImage(DXFImagePanel _parent, DXFModel _model)
		: base(_parent, _model)
	{
	}

	public override void DrawImage()
	{
		using DrawingContext drawingContext = RenderOpen();
		DXFImage.DashPen.Thickness = 1.0 / parent.Scale;
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
						item.Entity.Render(drawingContext, streamGeometryContext);
					}
				}
			}
		}
		drawingContext.DrawGeometry(null, DXFImage.BlackPen, streamGeometry);
	}
}
