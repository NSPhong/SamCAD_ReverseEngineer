using SamSoarII.Shell;

namespace SamSoarII.Polyline.Entity;

public class PolylineBSplineSegment : IGridPenningEntity
{
	private PolylineBSpline parent;

	private int start;

	private int count;

	private int monox;

	private int monoy;

	int IGridPenningEntity.ColorID => 0;

	public PolylineBSpline Parent => parent;

	public int Start => start;

	public int Count => count;

	public int MonoX => monox;

	public int MonoY => monoy;

	public PolylineBSplineSegment(PolylineBSpline _parent, int _start, int _count, int _monox, int _monoy)
	{
		parent = _parent;
		start = _start;
		count = _count;
		monox = _monox;
		monoy = _monoy;
	}
}
