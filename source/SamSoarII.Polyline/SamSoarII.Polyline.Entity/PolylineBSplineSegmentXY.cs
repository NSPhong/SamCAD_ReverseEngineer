using SamSoarII.Shell;

namespace SamSoarII.Polyline.Entity;

public class PolylineBSplineSegmentXY : PolylineBSplineSegment, IGridPenningFunctionXY, IGridPenningEntity
{
	private int cache;

	private int[] chcache;

	public string Name => base.Parent.Name;

	public double XMin => (base.Parent == null) ? 0.0 : ((base.MonoX > 0) ? base.Parent.Nodes[base.Start].X : base.Parent.Nodes[base.Start + base.Count - 1].X);

	public double XMax => (base.Parent == null) ? 0.0 : ((base.MonoX > 0) ? base.Parent.Nodes[base.Start + base.Count - 1].X : base.Parent.Nodes[base.Start].X);

	public PolylineBSplineSegmentXY(PolylineBSpline _parent, int _start, int _count, int _monox, int _monoy)
		: base(_parent, _start, _count, _monox, _monoy)
	{
		cache = base.Start;
		chcache = new int[4] { base.Start, base.Start, base.Start, base.Start };
	}

	protected bool _GetY(double x, ref double y, ref int i)
	{
		if (base.Parent == null)
		{
			return false;
		}
		if (base.MonoX > 0)
		{
			while (i > base.Start && base.Parent.Nodes[i].X > x)
			{
				i--;
			}
			while (i + 1 < base.Start + base.Count && base.Parent.Nodes[i + 1].X < x)
			{
				i++;
			}
			if (x < base.Parent.Nodes[i].X)
			{
				return false;
			}
			if (i + 1 >= base.Start + base.Count || x > base.Parent.Nodes[i + 1].X)
			{
				return false;
			}
		}
		else
		{
			while (i > base.Start && base.Parent.Nodes[i].X < x)
			{
				i--;
			}
			while (i + 1 < base.Start + base.Count && base.Parent.Nodes[i + 1].X > x)
			{
				i++;
			}
			if (x > base.Parent.Nodes[i].X)
			{
				return false;
			}
			if (i + 1 >= base.Start + base.Count || x < base.Parent.Nodes[i + 1].X)
			{
				return false;
			}
		}
		if (base.Parent.Nodes[i].X == base.Parent.Nodes[i + 1].X)
		{
			y = base.Parent.Nodes[i].Y;
		}
		else
		{
			y = base.Parent.Nodes[i].Y + (base.Parent.Nodes[i + 1].Y - base.Parent.Nodes[i].Y) * (x - base.Parent.Nodes[i].X) / (base.Parent.Nodes[i + 1].X - base.Parent.Nodes[i].X);
		}
		return true;
	}

	public bool GetY(double x, ref double y)
	{
		return _GetY(x, ref y, ref cache);
	}

	public bool GetY(double x, ref double y, int channel)
	{
		return _GetY(x, ref y, ref chcache[channel]);
	}

	public bool GetYString(double x, ref string y)
	{
		double y2 = 0.0;
		if (!GetY(x, ref y2))
		{
			return false;
		}
		y = $"{y2:f2}";
		return true;
	}

	public bool GetYString(double x, ref string y, int channel)
	{
		double y2 = 0.0;
		if (!GetY(x, ref y2, channel))
		{
			return false;
		}
		y = $"{y2:f2}";
		return true;
	}
}
