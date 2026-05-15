using System.Windows;

namespace SamSoarII.Polyline.Entity;

public class PolylineDemoArch : PolylineArch
{
	private Point from;

	public override Point From
	{
		get
		{
			return from;
		}
		set
		{
			from = value;
		}
	}

	public PolylineDemoArch()
	{
		from = default(Point);
	}

	public PolylineDemoArch(IPolylineImage _parent, int _id, Point _from, Point _to, Point _center, bool _isclockwise = true)
		: base(_parent, _id, _to, _center, _isclockwise)
	{
		from = _from;
	}
}
