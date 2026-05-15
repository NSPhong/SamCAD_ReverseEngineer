using System;
using System.Windows;
using System.Windows.Media;

namespace SamSoarII.Utility.DXF;

public class DXFLine : DXFEntity
{
	public Point StartP;

	public Point EndP;

	private bool isReal;

	public bool IsReal => isReal;

	public DXFLine(string name, DXFModel parent)
		: base(parent)
	{
		base.Name = name;
		base.Type = EntityType.Line;
		isReal = true;
		ReadProperties();
		parent.Graph.AddEdge(new DXFEdge(this));
	}

	public DXFLine(DXFModel parent, Point startP, Point endP)
		: base(parent)
	{
		base.Name = "LINE";
		base.Type = EntityType.Line;
		isReal = false;
		StartP = startP;
		EndP = endP;
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
				StartP.X = Convert.ToDouble(base.Parent.Reader.CurrentValue);
				break;
			case 20:
				StartP.Y = Convert.ToDouble(base.Parent.Reader.CurrentValue);
				break;
			case 11:
				EndP.X = Convert.ToDouble(base.Parent.Reader.CurrentValue);
				break;
			case 21:
				EndP.Y = Convert.ToDouble(base.Parent.Reader.CurrentValue);
				break;
			}
		}
	}

	public override void Render(DrawingContext context, StreamGeometryContext ctx)
	{
		if (isReal)
		{
			if (!base.IsReverse)
			{
				ctx.LineTo(EndP, isStroked: true, isSmoothJoin: true);
			}
			else
			{
				ctx.LineTo(StartP, isStroked: true, isSmoothJoin: true);
			}
			return;
		}
		context.DrawLine(DXFImage.DashPen, StartP, EndP);
		if (!base.IsReverse)
		{
			ctx.BeginFigure(EndP, isFilled: false, isClosed: false);
		}
		else
		{
			ctx.BeginFigure(StartP, isFilled: false, isClosed: false);
		}
	}
}
