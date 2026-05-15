using SamSoarII.Shell;

namespace SamSoarII.Polyline.Entity;

public class PolylineBSplineSegmentYX : PolylineBSplineSegment, IGridPenningFunctionYX, IGridPenningEntity
{
	private int cache;

	private int[] chcache;

	public string Name => base.Parent.Name;

	public double YMin => (base.Parent == null) ? 0.0 : ((base.MonoY > 0) ? base.Parent.Nodes[base.Start].Y : base.Parent.Nodes[base.Start + base.Count - 1].Y);

	public double YMax => (base.Parent == null) ? 0.0 : ((base.MonoY > 0) ? base.Parent.Nodes[base.Start + base.Count - 1].Y : base.Parent.Nodes[base.Start].Y);

	public PolylineBSplineSegmentYX(PolylineBSpline _parent, int _start, int _count, int _monox, int _monoy)
		: base(_parent, _start, _count, _monox, _monoy)
	{
		cache = base.Start;
		chcache = new int[4] { base.Start, base.Start, base.Start, base.Start };
	}

	protected bool _GetX(double y, ref double x, ref int i)
	{
		if (base.Parent == null)
		{
			return false;
		}
		if (base.MonoY > 0)
		{
			while (i > base.Start && base.Parent.Nodes[i].Y > y)
			{
				i--;
			}
			while (i + 1 < base.Start + base.Count && base.Parent.Nodes[i + 1].Y < y)
			{
				i++;
			}
			if (y < base.Parent.Nodes[i].Y)
			{
				return false;
			}
			if (i + 1 >= base.Start + base.Count || y > base.Parent.Nodes[i + 1].Y)
			{
				return false;
			}
		}
		else
		{
			while (i > base.Start && base.Parent.Nodes[i].Y < y)
			{
				i--;
			}
			while (i + 1 < base.Start + base.Count && base.Parent.Nodes[i + 1].Y > y)
			{
				i++;
			}
			if (y > base.Parent.Nodes[i].Y)
			{
				return false;
			}
			if (i + 1 >= base.Start + base.Count || y < base.Parent.Nodes[i + 1].Y)
			{
				return false;
			}
		}
		if (base.Parent.Nodes[i].Y == base.Parent.Nodes[i + 1].Y)
		{
			x = base.Parent.Nodes[i].X;
		}
		else
		{
			x = base.Parent.Nodes[i].X + (base.Parent.Nodes[i + 1].X - base.Parent.Nodes[i].X) * (y - base.Parent.Nodes[i].Y) / (base.Parent.Nodes[i + 1].Y - base.Parent.Nodes[i].Y);
		}
		return true;
	}

	public bool GetX(double y, ref double x)
	{
		return _GetX(y, ref x, ref cache);
	}

	public bool GetX(double y, ref double x, int channel)
	{
		return _GetX(y, ref x, ref chcache[channel]);
	}

	public bool GetXString(double y, ref string x)
	{
		double x2 = 0.0;
		if (!GetX(y, ref x2))
		{
			return false;
		}
		x = $"{x2:f2}";
		return true;
	}

	public bool GetXString(double y, ref string x, int channel)
	{
		double x2 = 0.0;
		if (!GetX(y, ref x2, channel))
		{
			return false;
		}
		x = $"{x2:f2}";
		return true;
	}
}
