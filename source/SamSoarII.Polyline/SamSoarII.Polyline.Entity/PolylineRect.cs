using System;
using System.ComponentModel;
using System.Windows;
using SamSoarII.Shell;

namespace SamSoarII.Polyline.Entity;

public class PolylineRect : PolylinePolygon, IPolylineRect, IPolylinePolygon, IPolylineEntity, INotifyPropertyChanged, IDisposable, IGridPenningEntity
{
	private Rect rect;

	public override PolylineType Type => PolylineType.Rect;

	public override bool IsSpecial => true;

	public Rect Rect
	{
		get
		{
			return rect;
		}
		set
		{
			rect = value;
			InvokePropertyChanged("Rect");
		}
	}

	public PolylineRect()
	{
		rect = default(Rect);
	}

	public PolylineRect(IPolylineImage _parent, int _id, Rect _rect)
		: base(_parent, _id)
	{
		rect = _rect;
	}

	protected override string _GetName()
	{
		return $"{base.ID}. Rectangle";
	}
}
