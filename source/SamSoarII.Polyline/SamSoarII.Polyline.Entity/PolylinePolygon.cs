using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using SamSoarII.Shell;

namespace SamSoarII.Polyline.Entity;

public class PolylinePolygon : PolylineEntity, IPolylinePolygon, IPolylineEntity, INotifyPropertyChanged, IDisposable, IGridPenningEntity
{
	private List<Point> points;

	private List<double> roundradius;

	public override PolylineType Type => PolylineType.Polygon;

	public IList<Point> Points => points;

	public IList<double> RoundRadius => roundradius;

	public override Point To
	{
		get
		{
			return base.From;
		}
		set
		{
		}
	}

	public PolylinePolygon()
	{
		points = new List<Point>();
		roundradius = new List<double>();
	}

	public PolylinePolygon(IPolylineImage _parent, int _id)
		: base(_parent, _id, default(Point))
	{
		points = new List<Point>();
		roundradius = new List<double>();
	}

	protected override string _GetName()
	{
		return $"{base.ID}. Polygon";
	}
}
