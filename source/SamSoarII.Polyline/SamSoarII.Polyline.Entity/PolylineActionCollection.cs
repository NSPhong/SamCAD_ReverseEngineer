using System.Collections.Generic;
using System.Windows;

namespace SamSoarII.Polyline.Entity;

public class PolylineActionCollection : IPolylineActionCollection, IPolylineAction
{
	private List<IPolylineAction> items;

	private string message;

	public IList<IPolylineAction> Items => items;

	public int Index => -1;

	public IList<IPolylineEntity> RemovedItems => null;

	public IList<IPolylineEntity> AddedItems => null;

	public ChangedTarget Target => ChangedTarget.None;

	public ChangedFlags Flag => ChangedFlags.None;

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

	public object OldValue => null;

	public object NewValue => null;

	public bool OldBool => false;

	public bool NewBool => false;

	public double OldDouble => 0.0;

	public double NewDouble => 0.0;

	public int OldInt => 0;

	public int NewInt => 0;

	public Point OldPoint => default(Point);

	public Point NewPoint => default(Point);

	public PolylineActionCollection()
	{
		items = new List<IPolylineAction>();
		message = string.Empty;
	}
}
