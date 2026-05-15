using System.Windows;
using SamSoarII.Utility.Collection;

namespace SamSoarII.Polyline.Entity.Module;

internal class PolylineMeaning : IQuickSetSupport
{
	private bool isactive;

	private IPolylineEntity entity;

	private Point point;

	private Meaning meaning;

	private QuickSetItem qsitem;

	private int aid1;

	private int aid2;

	public bool IsActive
	{
		get
		{
			return isactive;
		}
		set
		{
			isactive = value;
		}
	}

	public IPolylineEntity Entity => entity;

	public Point Point => point;

	public Meaning Meaning => meaning;

	public int GID => entity?.Group?.GID ?? (-1);

	QuickSetItem IQuickSetSupport.Item
	{
		get
		{
			return qsitem;
		}
		set
		{
			qsitem = value;
		}
	}

	public int Aid1
	{
		get
		{
			return aid1;
		}
		set
		{
			aid1 = value;
		}
	}

	public int Aid2
	{
		get
		{
			return aid2;
		}
		set
		{
			aid2 = value;
		}
	}

	public PolylineMeaning(IPolylineEntity _entity, Point _point, Meaning _meaning)
	{
		entity = _entity;
		point = _point;
		meaning = _meaning;
		isactive = true;
		qsitem = null;
		aid1 = -1;
		aid2 = -1;
	}
}
