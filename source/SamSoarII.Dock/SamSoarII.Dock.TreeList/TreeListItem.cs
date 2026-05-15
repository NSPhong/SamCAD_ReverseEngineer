using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SamSoarII.Dock.TreeList;

internal class TreeListItem : ITreeListItem, IList<ITreeListItem>, ICollection<ITreeListItem>, IEnumerable<ITreeListItem>, IEnumerable, INotifyPropertyChanged
{
	private TreeList root;

	private ITreeListItemCtx ctx;

	private TreeListItem parent;

	private int id;

	private int height;

	private bool isexpand;

	private bool isselected;

	private List<TreeListItem> items;

	public TreeList Root
	{
		get
		{
			return root;
		}
		set
		{
			root = value;
		}
	}

	public ITreeListItemCtx Ctx
	{
		get
		{
			return ctx;
		}
		set
		{
			_SetCtx(value);
		}
	}

	public TreeListItem Parent
	{
		get
		{
			return parent;
		}
		set
		{
			_SetParent(value);
		}
	}

	ITreeListItem ITreeListItem.Parent => Parent;

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

	public int Level => _GetLevel();

	public int Height
	{
		get
		{
			return (!isexpand) ? 1 : height;
		}
		set
		{
			height = value;
			IvProp("Height");
		}
	}

	public bool IsExpand
	{
		get
		{
			return isexpand;
		}
		set
		{
			_SetIsExpand(value);
		}
	}

	public bool IsSelected
	{
		get
		{
			return isselected;
		}
		set
		{
			isselected = value;
			IvProp("IsSelected");
		}
	}

	public object Header => ctx?.Header ?? null;

	public bool IsDirectory => ctx?.IsDirectory ?? true;

	public bool IsSorted => ctx?.IsSorted ?? root?.IsRootSorted ?? false;

	public ITreeListItem this[int id]
	{
		get
		{
			return items[id];
		}
		set
		{
		}
	}

	int ICollection<ITreeListItem>.Count => items.Count();

	bool ICollection<ITreeListItem>.IsReadOnly => true;

	public event PropertyChangedEventHandler PropertyChanged;

	public TreeListItem()
	{
		root = null;
		ctx = null;
		parent = null;
		items = new List<TreeListItem>();
		id = -1;
		height = 1;
	}

	public int IndexOf(ITreeListItem item)
	{
		return items.IndexOf((TreeListItem)item);
	}

	void IList<ITreeListItem>.Insert(int index, ITreeListItem item)
	{
	}

	void IList<ITreeListItem>.RemoveAt(int index)
	{
	}

	void ICollection<ITreeListItem>.Add(ITreeListItem item)
	{
	}

	bool ICollection<ITreeListItem>.Contains(ITreeListItem item)
	{
		return items.Contains((TreeListItem)item);
	}

	void ICollection<ITreeListItem>.CopyTo(ITreeListItem[] array, int arrayIndex)
	{
		for (int i = 0; i < Math.Min(items.Count(), array.Length - arrayIndex); i++)
		{
			array[i + arrayIndex] = items[i];
		}
	}

	void ICollection<ITreeListItem>.Clear()
	{
	}

	bool ICollection<ITreeListItem>.Remove(ITreeListItem item)
	{
		return false;
	}

	IEnumerator<ITreeListItem> IEnumerable<ITreeListItem>.GetEnumerator()
	{
		return items.Cast<ITreeListItem>().GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return items.GetEnumerator();
	}

	protected int _GetLevel()
	{
		int num = -1;
		for (TreeListItem treeListItem = parent; treeListItem != null; treeListItem = treeListItem.Parent)
		{
			num++;
		}
		return num;
	}

	protected void _RaiseInfo(int dtheight)
	{
		for (TreeListItem treeListItem = parent; treeListItem != null; treeListItem = treeListItem.parent)
		{
			treeListItem.Height += dtheight;
		}
	}

	protected virtual void _SetCtx(ITreeListItemCtx value)
	{
		if (ctx != null)
		{
			ctx.PropertyChanged -= OnCtxPropertyChanged;
		}
		ctx = value;
		if (ctx != null)
		{
			ctx.PropertyChanged += OnCtxPropertyChanged;
		}
	}

	protected virtual void _SetParent(TreeListItem value)
	{
		_RaiseInfo(-Height);
		parent = value;
		_RaiseInfo(Height);
	}

	protected virtual void _SetIsExpand(bool value)
	{
		int num = Height;
		isexpand = value;
		IvProp("IsExpand");
		int num2 = Height;
		int num3 = num2 - num;
		if (num3 != 0)
		{
			_RaiseInfo(num3);
		}
	}

	protected void _UpdateIDs()
	{
		for (int i = 0; i < items.Count(); i++)
		{
			items[i].ID = i;
		}
	}

	public void Add(TreeListItem item)
	{
		item.Parent?.Remove(item);
		if (IsSorted)
		{
			for (int i = 0; i <= items.Count(); i++)
			{
				if (i >= items.Count() || item.Ctx.CompareTo(items[i].Ctx) <= 0)
				{
					items.Insert(i, item);
					item.Parent = this;
					_UpdateIDs();
					break;
				}
			}
		}
		else
		{
			items.Add(item);
			item.Parent = this;
			_UpdateIDs();
		}
	}

	public void Insert(TreeListItem item, int id)
	{
		item.Parent?.Remove(item);
		if (IsSorted)
		{
			Add(item);
			return;
		}
		item.ID = id;
		items.Insert(id, item);
		item.Parent = this;
		_UpdateIDs();
	}

	public void Remove(TreeListItem item)
	{
		item.Parent = null;
		items.RemoveAt(item.ID);
		item.ID = -1;
		_UpdateIDs();
	}

	public void Clear()
	{
		for (int i = 0; i < items.Count(); i++)
		{
			items[i].Parent = null;
			items[i].ID = -1;
		}
		items.Clear();
	}

	public void IvProp(string name)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
	}

	private void OnCtxPropertyChanged(object sender, PropertyChangedEventArgs e)
	{
		string propertyName = e.PropertyName;
		string text = propertyName;
		if (!(text == "Header"))
		{
			if (text == "IsDirectory")
			{
				IvProp("IsDirectory");
			}
		}
		else
		{
			IvProp("Header");
		}
	}
}
