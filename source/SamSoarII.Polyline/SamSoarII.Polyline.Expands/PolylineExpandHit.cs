using System.Windows;

namespace SamSoarII.Polyline.Expands;

public class PolylineExpandHit : IPolylineExpandHit
{
	private PolylineExpand parent;

	private int id;

	private Point p;

	private HitDirection dir;

	public PolylineExpand Parent
	{
		get
		{
			return parent;
		}
		set
		{
			parent = value;
		}
	}

	IPolylineExpand IPolylineExpandHit.Parent
	{
		get
		{
			return Parent;
		}
		set
		{
			Parent = (PolylineExpand)value;
		}
	}

	public int ID
	{
		get
		{
			return id;
		}
		set
		{
			id = value;
		}
	}

	public Point P
	{
		get
		{
			return p;
		}
		set
		{
			p = value;
		}
	}

	public HitDirection Dir
	{
		get
		{
			return dir;
		}
		set
		{
			dir = value;
		}
	}

	public PolylineExpandHit(PolylineExpand _parent, double _x, double _y)
		: this(_parent, new Point(_x, _y), 0)
	{
	}

	public PolylineExpandHit(PolylineExpand _parent, double _x, double _y, int _id)
		: this(_parent, new Point(_x, _y), _id)
	{
	}

	public PolylineExpandHit(PolylineExpand _parent, Point _p, int _id)
	{
		parent = _parent;
		p = _p;
		id = _id;
		dir = HitDirection.None;
	}
}
