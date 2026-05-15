using System.Windows;

namespace SamSoarII.Polyline.Entity;

public class PolylineControlPoint : IPolylineControlPoint
{
	private IPolylineEntity parent;

	private int id;

	private IPolylineControlPointView view;

	public IPolylineEntity Parent => parent;

	public int ID => id;

	public virtual Point Point
	{
		get
		{
			if (id == 0)
			{
				return parent.From;
			}
			if (id == 1)
			{
				return parent.To;
			}
			if (id == 2 && parent is IPolylineCircle)
			{
				return ((IPolylineCircle)parent).Center;
			}
			if (parent is IPolylinePolygon)
			{
				return ((IPolylinePolygon)parent).Points[id - 2];
			}
			if (parent is IPolylineBSpline)
			{
				return ((IPolylineBSpline)parent).Points[id - 2];
			}
			if (parent is IPolylineB2Spline)
			{
				return ((IPolylineB2Spline)parent).Points[id - 2];
			}
			return default(Point);
		}
		set
		{
			if (id == 0)
			{
				parent.From = value;
			}
			else if (id == 1)
			{
				parent.To = value;
			}
			else if (id == 2 && parent is IPolylineCircle)
			{
				((IPolylineCircle)parent).Center = value;
			}
			else if (parent is IPolylinePolygon)
			{
				((IPolylinePolygon)parent).Points[id - 2] = value;
			}
			else if (parent is IPolylineBSpline)
			{
				((IPolylineBSpline)parent).Points[id - 2] = value;
			}
			else if (parent is IPolylineB2Spline)
			{
				((IPolylineB2Spline)parent).TryMove(id - 2, ref value);
			}
		}
	}

	public IPolylineControlPointView View
	{
		get
		{
			return view;
		}
		set
		{
			_setView(value);
		}
	}

	public PolylineControlPoint(IPolylineEntity _parent, int _id)
	{
		parent = _parent;
		id = _id;
	}

	protected void _setView(IPolylineControlPointView value)
	{
		IPolylineControlPointView polylineControlPointView = view;
		view = null;
		if (polylineControlPointView != null && polylineControlPointView.Core != null)
		{
			polylineControlPointView.Core = null;
		}
		view = value;
		if (view != null && view.Core != this)
		{
			view.Core = this;
		}
	}
}
