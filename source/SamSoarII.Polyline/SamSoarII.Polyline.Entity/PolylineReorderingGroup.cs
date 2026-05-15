using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using SamSoarII.Core.Files;
using SamSoarII.Shell;

namespace SamSoarII.Polyline.Entity;

public class PolylineReorderingGroup : PolylineEntity, IPolylineReorderingGroup, IPolylineGroup, IPolylineEntity, INotifyPropertyChanged, IDisposable, IGridPenningEntity, IEnumerable<IPolylineEntity>, IEnumerable
{
	private IPolylineGroup core;

	private int oldgid;

	private int newgid;

	private bool ismoved;

	private FrameworkElement view;

	public IPolylineGroup Core => core;

	public int OldGID => oldgid;

	public int NewGID
	{
		get
		{
			return newgid;
		}
		set
		{
			newgid = value;
			InvokePropertyChanged("NewGID");
			InvokePropertyChanged("GID");
		}
	}

	public bool IsMoved
	{
		get
		{
			return ismoved;
		}
		set
		{
			ismoved = value;
			InvokePropertyChanged("IsMoved");
		}
	}

	public FrameworkElement View
	{
		get
		{
			return view;
		}
		set
		{
			view = value;
			InvokePropertyChanged("View");
		}
	}

	public override PolylineType Type => PolylineType.ReorderingGroup;

	int IPolylineGroup.GID
	{
		get
		{
			return NewGID;
		}
		set
		{
			NewGID = value;
		}
	}

	int IPolylineGroup.Start
	{
		get
		{
			return core.Start;
		}
		set
		{
		}
	}

	int IPolylineGroup.Count
	{
		get
		{
			return core.Count;
		}
		set
		{
		}
	}

	bool IPolylineGroup.IsExpand
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	IPolylineEntity IPolylineGroup.this[int id] => core[id];

	public PolylineReorderingGroup(IPolylineGroup _core)
		: base(_core.Parent, _core.ID, default(Point))
	{
		core = _core;
		oldgid = core.GID;
		newgid = oldgid;
		ismoved = false;
	}

	protected override string _GetName()
	{
		return core.Name;
	}

	void IPolylineGroup.Save(PolylineGroupHeader header)
	{
	}

	void IPolylineGroup.Load(PolylineGroupHeader header)
	{
	}

	IEnumerator<IPolylineEntity> IEnumerable<IPolylineEntity>.GetEnumerator()
	{
		return core.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return core.GetEnumerator();
	}

	public bool IsLeftInside()
	{
		return core.IsLeftInside();
	}
}
