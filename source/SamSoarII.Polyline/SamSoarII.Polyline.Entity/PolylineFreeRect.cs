using System;
using System.ComponentModel;
using System.Windows;
using SamSoarII.Shell;

namespace SamSoarII.Polyline.Entity;

public class PolylineFreeRect : PolylinePolygon, IPolylineFreeRect, IPolylinePolygon, IPolylineEntity, INotifyPropertyChanged, IDisposable, IGridPenningEntity
{
	private Point from;

	private Point p1;

	private Point p2;

	public override PolylineType Type => PolylineType.FreeRect;

	public override bool IsSpecial => true;

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

	public Point P1
	{
		get
		{
			return p1;
		}
		set
		{
			p1 = value;
		}
	}

	public Point P2
	{
		get
		{
			return p2;
		}
		set
		{
			p2 = value;
		}
	}

	public PolylineFreeRect()
	{
	}

	public PolylineFreeRect(IPolylineImage _parent, int _id)
		: base(_parent, _id)
	{
	}

	protected override string _GetName()
	{
		return $"{base.ID}. Free rectangle";
	}
}
