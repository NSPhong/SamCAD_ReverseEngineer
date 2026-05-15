using System;
using System.Windows;
using System.Windows.Media;

namespace SamSoarII.Utility.DXF;

public class DXFCircle : DXFEntity
{
	public Point CenterP = default(Point);

	public double radius;

	public DXFCircle(string name, DXFModel parent)
		: base(parent)
	{
		base.Name = name;
		base.Type = EntityType.Circle;
		ReadProperties();
		parent.Graph.AddEdge(new DXFEdge(this));
	}

	protected DXFCircle(DXFModel parent)
		: base(parent)
	{
	}

	public override void ReadProperties()
	{
		while (true)
		{
			base.Parent.Reader.MoveNext();
			if (base.Parent.Reader.CurrentCode == 0)
			{
				break;
			}
			switch (base.Parent.Reader.CurrentCode)
			{
			case 10:
				CenterP.X = Convert.ToDouble(base.Parent.Reader.CurrentValue);
				break;
			case 20:
				CenterP.Y = Convert.ToDouble(base.Parent.Reader.CurrentValue);
				break;
			case 40:
				radius = Convert.ToDouble(base.Parent.Reader.CurrentValue);
				break;
			}
		}
	}

	public override void Render(DrawingContext context, StreamGeometryContext ctx)
	{
		ctx.LineTo(new Point(CenterP.X + radius, CenterP.Y), isStroked: true, isSmoothJoin: true);
		ctx.ArcTo(new Point(CenterP.X + radius, CenterP.Y + 0.01), new Size(radius, radius), 0.0, isLargeArc: true, SweepDirection.Counterclockwise, isStroked: true, isSmoothJoin: true);
	}
}
