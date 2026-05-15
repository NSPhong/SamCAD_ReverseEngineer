using System;
using System.Windows;
using System.Windows.Media;

namespace SamSoarII.Utility.DXF;

public class DXFArc : DXFCircle
{
	public double SAngle;

	public double EAngle;

	public Point StartP;

	public Point EndP;

	public DXFArc(string name, DXFModel parent)
		: base(parent)
	{
		base.Name = name;
		base.Type = EntityType.Arc;
		ReadProperties();
		parent.Graph.AddEdge(new DXFEdge(this));
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
			case 50:
				SAngle = Convert.ToDouble(base.Parent.Reader.CurrentValue);
				break;
			case 51:
				EAngle = Convert.ToDouble(base.Parent.Reader.CurrentValue);
				break;
			}
		}
		if (EAngle == 0.0)
		{
			EAngle = 360.0;
		}
		StartP = DXFHelper.ComputePoint(CenterP, radius, SAngle);
		EndP = DXFHelper.ComputePoint(CenterP, radius, EAngle);
	}

	public override void Render(DrawingContext context, StreamGeometryContext ctx)
	{
		if (!base.IsReverse)
		{
			ctx.ArcTo(EndP, new Size(radius, radius), 0.0, EAngle - SAngle > 180.0, SweepDirection.Clockwise, isStroked: true, isSmoothJoin: true);
		}
		else
		{
			ctx.ArcTo(StartP, new Size(radius, radius), 0.0, EAngle - SAngle > 180.0, SweepDirection.Counterclockwise, isStroked: true, isSmoothJoin: true);
		}
	}
}
