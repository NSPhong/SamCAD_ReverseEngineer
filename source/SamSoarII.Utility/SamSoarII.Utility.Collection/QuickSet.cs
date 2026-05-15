using System;
using System.Collections.Generic;

namespace SamSoarII.Utility.Collection;

public class QuickSet : IDisposable
{
	private bool isdisposed;

	private List<QuickSetItem> items;

	public bool IsDisposed => isdisposed;

	public QuickSet()
	{
		items = new List<QuickSetItem>();
	}

	public void Dispose()
	{
		if (isdisposed)
		{
			return;
		}
		isdisposed = true;
		foreach (QuickSetItem item in items)
		{
			if (item.Item != null)
			{
				item.Item.Item = null;
			}
			if (item != null)
			{
				item.Item = null;
			}
		}
		items.Clear();
		items = null;
	}

	public IQuickSetSupport GetRoot(IQuickSetSupport item)
	{
		IQuickSetSupport quickSetSupport = item;
		while (item?.Item?.Parent != null)
		{
			item = item.Item.Parent.Item;
		}
		IQuickSetSupport quickSetSupport2 = item;
		IQuickSetSupport quickSetSupport3 = null;
		if (quickSetSupport2 == null)
		{
			return quickSetSupport2;
		}
		item = quickSetSupport;
		while (item != quickSetSupport2 && item?.Item?.Parent != null)
		{
			quickSetSupport3 = item.Item.Parent.Item;
			item.Item.Parent = quickSetSupport2.Item;
			item = quickSetSupport3;
		}
		return quickSetSupport2;
	}

	public void Add(IQuickSetSupport item, IQuickSetSupport parent)
	{
		if (item != null && parent != null)
		{
			if (item.Item == null)
			{
				QuickSetItem quickSetItem = new QuickSetItem();
				quickSetItem.Item = item;
				item.Item = quickSetItem;
				items.Add(quickSetItem);
			}
			if (parent.Item == null)
			{
				QuickSetItem quickSetItem2 = new QuickSetItem();
				quickSetItem2.Item = parent;
				parent.Item = quickSetItem2;
				items.Add(quickSetItem2);
			}
			item = GetRoot(item);
			parent = GetRoot(parent);
			item.Item.Parent = parent?.Item;
		}
	}

	public bool IsSameRoot(IQuickSetSupport item1, IQuickSetSupport item2)
	{
		IQuickSetSupport root = GetRoot(item1);
		IQuickSetSupport root2 = GetRoot(item2);
		return root != null && root2 != null && root == root2;
	}
}
