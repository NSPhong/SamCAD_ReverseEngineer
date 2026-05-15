using System;
using System.ComponentModel;
using System.Windows;
using SamSoarII.Shell;

namespace SamSoarII.Polyline.Entity;

public class PolylineDemoCorner : PolylineEntity, IPolylineCorner, IPolylineEntity, INotifyPropertyChanged, IDisposable, IGridPenningEntity
{
	private Point from;

	private Point corner;

	public override PolylineType Type => PolylineType.Corner;

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

	public Point Corner
	{
		get
		{
			return corner;
		}
		set
		{
			corner = value;
		}
	}

	public PolylineDemoCorner()
	{
	}

	public PolylineDemoCorner(IPolylineImage _image, int _id, Point _from, Point _corner, Point _to)
		: base(_image, _id, _to)
	{
		from = _from;
		corner = _corner;
	}

	protected override string _GetName()
	{
		return $"{base.ID}. Corner";
	}
}
