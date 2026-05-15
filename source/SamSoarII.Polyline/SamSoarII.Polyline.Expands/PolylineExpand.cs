using System.Collections.Generic;
using System.Windows;
using SamSoarII.Polyline.Entity;

namespace SamSoarII.Polyline.Expands;

public abstract class PolylineExpand : IPolylineExpand
{
	protected PolylineImage parent;

	protected int id;

	private double anglefrom;

	private double angleto;

	public PolylineImage Parent
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

	IPolylineImage IPolylineExpand.Parent
	{
		get
		{
			return Parent;
		}
		set
		{
			Parent = (PolylineImage)value;
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

	public double AngleFrom
	{
		get
		{
			return anglefrom;
		}
		set
		{
			anglefrom = value;
		}
	}

	public double AngleTo
	{
		get
		{
			return angleto;
		}
		set
		{
			angleto = value;
		}
	}

	protected PolylineExpand(PolylineImage _parent, int _id)
	{
		parent = _parent;
		id = _id;
	}

	public abstract IPolylineEntity ToEntity(Point _from, IPolylineExpand _that);

	public abstract Point? GetConnect(Point _from, IPolylineExpand _that);

	public abstract void GetYs(double _x, List<IPolylineExpandHit> _hits);

	public abstract void GetXs(double _y, List<IPolylineExpandHit> _hits);
}
