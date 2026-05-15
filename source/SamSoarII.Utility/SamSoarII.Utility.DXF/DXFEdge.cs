using System.Windows;

namespace SamSoarII.Utility.DXF;

public class DXFEdge
{
	private bool isSreached;

	private DXFEntity entity;

	public DXFVertex Start;

	public DXFVertex End;

	public DXFVertex Relative;

	public bool IsSreached
	{
		get
		{
			return isSreached;
		}
		set
		{
			isSreached = value;
		}
	}

	public DXFEntity Entity => entity;

	public DXFEdge(DXFEntity entity)
	{
		this.entity = entity;
		isSreached = false;
		ComputeVertex();
	}

	public DXFEdge(DXFVertex start, DXFVertex end, DXFModel model)
	{
		Start = start;
		End = end;
		isSreached = false;
		entity = new DXFLine(model, start.P, end.P);
	}

	private void ComputeVertex()
	{
		switch (Entity.Type)
		{
		case EntityType.Line:
		{
			DXFLine dXFLine = (DXFLine)entity;
			Start = new DXFVertex(dXFLine.StartP);
			End = new DXFVertex(dXFLine.EndP);
			break;
		}
		case EntityType.Arc:
		{
			DXFArc dXFArc = (DXFArc)entity;
			Start = new DXFVertex(dXFArc.StartP);
			End = new DXFVertex(dXFArc.EndP);
			break;
		}
		case EntityType.Ellipse:
		{
			DXFEllipse dXFEllipse = (DXFEllipse)entity;
			Start = new DXFVertex(dXFEllipse.LongP);
			End = Start;
			break;
		}
		case EntityType.Circle:
		{
			DXFCircle dXFCircle = (DXFCircle)entity;
			Point p = new Point(dXFCircle.CenterP.X + dXFCircle.radius, dXFCircle.CenterP.Y);
			Start = new DXFVertex(p);
			End = Start;
			break;
		}
		case EntityType.Spline:
		{
			DXFSpline dXFSpline = (DXFSpline)entity;
			Start = new DXFVertex(dXFSpline.StartP);
			End = new DXFVertex(dXFSpline.EndP);
			break;
		}
		}
	}

	public void Flip()
	{
		DXFVertex start = Start;
		Start = End;
		End = start;
		entity.IsReverse = !entity.IsReverse;
	}
}
