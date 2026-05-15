using System.Collections.Generic;
using System.Windows;

namespace SamSoarII.Polyline.Entity;

internal class PolylineAction : IPolylineAction
{
	private int index;

	private IList<IPolylineEntity> removeditems;

	private IList<IPolylineEntity> addeditems;

	private ChangedTarget target;

	private ChangedFlags flag;

	private object oldvalue;

	private object newvalue;

	private string message;

	public int Index => index;

	public IList<IPolylineEntity> RemovedItems => removeditems;

	public IList<IPolylineEntity> AddedItems => addeditems;

	public ChangedTarget Target => target;

	public ChangedFlags Flag => flag;

	public object OldValue => oldvalue;

	public object NewValue => newvalue;

	public string Message
	{
		get
		{
			return message;
		}
		set
		{
			message = value;
		}
	}

	public bool OldBool => oldvalue is bool && (bool)oldvalue;

	public bool NewBool => newvalue is bool && (bool)newvalue;

	public double OldDouble => (oldvalue is double) ? ((double)oldvalue) : double.NaN;

	public double NewDouble => (newvalue is double) ? ((double)newvalue) : double.NaN;

	public int OldInt => (oldvalue is int) ? ((int)oldvalue) : 0;

	public int NewInt => (newvalue is int) ? ((int)newvalue) : 0;

	public Point OldPoint => (oldvalue is Point) ? ((Point)oldvalue) : default(Point);

	public Point NewPoint => (newvalue is Point) ? ((Point)newvalue) : default(Point);

	public PolylineAction(int _index, IList<IPolylineEntity> _removeditems, IList<IPolylineEntity> _addeditems)
	{
		index = _index;
		removeditems = _removeditems;
		addeditems = _addeditems;
		message = string.Empty;
	}

	public PolylineAction(int _index, ChangedTarget _target, object _oldvalue, object _newvalue)
	{
		index = _index;
		target = _target;
		flag = ChangedFlags.None;
		oldvalue = _oldvalue;
		newvalue = _newvalue;
		message = string.Empty;
	}

	public PolylineAction(int _index, ChangedTarget _target, ChangedFlags _flag, object _oldvalue, object _newvalue)
	{
		index = _index;
		target = _target;
		flag = _flag;
		oldvalue = _oldvalue;
		newvalue = _newvalue;
		message = string.Empty;
	}
}
