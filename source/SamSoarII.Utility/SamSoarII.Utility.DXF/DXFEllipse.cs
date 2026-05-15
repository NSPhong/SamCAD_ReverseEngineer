using System;
using System.Windows;
using System.Windows.Media;

namespace SamSoarII.Utility.DXF;

public class DXFEllipse : DXFEntity
{
	public Point CenterP = default(Point);

	public Point LongP = default(Point);

	private double SRadian;

	private double ERadian;

	private double ratio;

	public Point ShortP
	{
		get
		{
			Vector vector = LongP - CenterP;
			Vector vector2 = new Vector(vector.Y, 0.0 - vector.X);
			return CenterP + vector2 * ratio;
		}
	}

	public DXFEllipse(string name, DXFModel parent)
		: base(parent)
	{
		base.Name = name;
		base.Type = EntityType.Ellipse;
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
			case 11:
				LongP.X = Convert.ToDouble(base.Parent.Reader.CurrentValue);
				break;
			case 21:
				LongP.Y = Convert.ToDouble(base.Parent.Reader.CurrentValue);
				break;
			case 40:
				ratio = Convert.ToDouble(base.Parent.Reader.CurrentValue);
				break;
			case 41:
				SRadian = Convert.ToDouble(base.Parent.Reader.CurrentValue);
				break;
			case 42:
				ERadian = Convert.ToDouble(base.Parent.Reader.CurrentValue);
				break;
			}
		}
	}

	public override void Render(DrawingContext context, StreamGeometryContext ctx)
	{
		double num = (ERadian - SRadian) * 180.0 / System.Math.PI;
		double num2 = DXFHelper.ComputeLength(CenterP, LongP);
		ctx.ArcTo(LongP, new Size(num2, System.Math.Abs(num2 * ratio)), DXFHelper.ComputeRotateAngle(CenterP, LongP), num > 180.0, SweepDirection.Counterclockwise, isStroked: true, isSmoothJoin: true);
	}
}
