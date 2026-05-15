using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace SamSoarII.Dock.TreeList;

public class TreeList : ItemsControl, IComponentConnector
{
	public static readonly DependencyProperty IsRootSortedProperty = DependencyProperty.Register("IsRootSorted", typeof(bool), typeof(TreeList), new PropertyMetadata(false, OnPropertyChanged_IsRootSorted));

	public static readonly DependencyProperty IsFreezedProperty = DependencyProperty.Register("IsFreezed", typeof(bool), typeof(TreeList), new PropertyMetadata(false, OnPropertyChanged_IsFreezed));

	public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(object), typeof(TreeList), new PropertyMetadata(false, OnPropertyChanged_SelectedItem));

	public static readonly DependencyProperty SelectedBackgroundProperty = DependencyProperty.Register("SelectedBackground", typeof(Brush), typeof(TreeList), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(byte.MaxValue, 16, 16, 138)), OnPropertyChanged_SelectedBackground));

	public static readonly DependencyProperty SelectedForegroundProperty = DependencyProperty.Register("SelectedForeground", typeof(Brush), typeof(TreeList), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue)), OnPropertyChanged_SelectedForeground));

	private Dictionary<ITreeListItemCtx, TreeListItem> items;

	private Dictionary<ITreeListItemCtx, TreeListItem> selecteds;

	private List<TreeListItem> list;

	private TreeListItem root;

	private bool _contentLoaded;

	public bool IsRootSorted
	{
		get
		{
			return (bool)GetValue(IsRootSortedProperty);
		}
		set
		{
			SetValue(IsRootSortedProperty, value);
		}
	}

	public bool IsFreezed
	{
		get
		{
			return (bool)GetValue(IsFreezedProperty);
		}
		set
		{
			SetValue(IsFreezedProperty, value);
		}
	}

	public object SelectedItem
	{
		get
		{
			return GetValue(SelectedItemProperty);
		}
		set
		{
			SetValue_SelectedItem(value);
		}
	}

	public Brush SelectedBackground
	{
		get
		{
			return (Brush)GetValue(SelectedBackgroundProperty);
		}
		set
		{
			SetValue(SelectedBackgroundProperty, value);
		}
	}

	public Brush SelectedForeground
	{
		get
		{
			return (Brush)GetValue(SelectedForegroundProperty);
		}
		set
		{
			SetValue(SelectedForegroundProperty, value);
		}
	}

	public IEnumerable<object> SelectedItems => selecteds.Keys;

	public event DependencyPropertyChangedEventHandler SelectedItemChanged;

	public TreeList()
	{
		InitializeComponent();
		items = new Dictionary<ITreeListItemCtx, TreeListItem>();
		selecteds = new Dictionary<ITreeListItemCtx, TreeListItem>();
		list = new List<TreeListItem>();
		root = new TreeListItem
		{
			Root = this,
			Parent = null,
			IsExpand = true,
			Height = 1,
			Ctx = null
		};
	}

	private static void OnPropertyChanged_IsRootSorted(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
	}

	private static void OnPropertyChanged_IsFreezed(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is TreeList)
		{
			((TreeList)d).OnIsFreezedChanged(e);
		}
	}

	private void OnIsFreezedChanged(DependencyPropertyChangedEventArgs e)
	{
		if (!IsFreezed)
		{
			_ListRefresh();
		}
	}

	internal void SetValue_SelectedItem(object value)
	{
		if (!(value is ITreeListItemCtx))
		{
			SelectClear();
		}
		else if (value != SelectedItem)
		{
			TreeListItem treeListItem = (TreeListItem)GetItem((ITreeListItemCtx)value);
			if (treeListItem == null)
			{
				SelectClear();
			}
			else
			{
				SelectSingle(treeListItem);
			}
		}
	}

	private static void OnPropertyChanged_SelectedItem(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is TreeList)
		{
			((TreeList)d).OnSelectedItemChanged(e);
		}
	}

	private void OnSelectedItemChanged(DependencyPropertyChangedEventArgs e)
	{
		this.SelectedItemChanged?.Invoke(this, e);
	}

	private static void OnPropertyChanged_SelectedBackground(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is TreeList)
		{
			((TreeList)d).OnSelectedBackgroundChanged(e);
		}
	}

	private void OnSelectedBackgroundChanged(DependencyPropertyChangedEventArgs e)
	{
	}

	private static void OnPropertyChanged_SelectedForeground(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is TreeList)
		{
			((TreeList)d).OnSelectedForegroundChanged(e);
		}
	}

	private void OnSelectedForegroundChanged(DependencyPropertyChangedEventArgs e)
	{
	}

	internal void _ListRefresh()
	{
		if (!IsFreezed && root != null)
		{
			list = new List<TreeListItem>();
			_ListGather(root);
			base.ItemsSource = list;
		}
	}

	internal void _ListGather(TreeListItem item)
	{
		foreach (TreeListItem item2 in (IEnumerable<ITreeListItem>)item)
		{
			list.Add(item2);
			if (item2.IsExpand)
			{
				_ListGather(item2);
			}
		}
	}

	public ITreeListItem GetItem(ITreeListItemCtx ctx)
	{
		TreeListItem value = null;
		items.TryGetValue(ctx, out value);
		return value;
	}

	internal TreeListItem CreateItem(ITreeListItemCtx ctx)
	{
		TreeListItem treeListItem = (TreeListItem)GetItem(ctx);
		if (treeListItem != null)
		{
			return treeListItem;
		}
		treeListItem = new TreeListItem
		{
			Root = this,
			Ctx = ctx,
			IsExpand = ctx.IsDefaultExpand
		};
		items.Add(ctx, treeListItem);
		return treeListItem;
	}

	public ITreeListItem Insert(ITreeListItemCtx ptx, ITreeListItemCtx ctx, int id)
	{
		TreeListItem treeListItem = ((ptx != null) ? ((TreeListItem)GetItem(ptx)) : root);
		if (treeListItem == null)
		{
			return null;
		}
		TreeListItem treeListItem2 = CreateItem(ctx);
		treeListItem.Insert(treeListItem2, id);
		_ListRefresh();
		return treeListItem2;
	}

	public ITreeListItem Add(ITreeListItemCtx ptx, ITreeListItemCtx ctx)
	{
		TreeListItem treeListItem = ((ptx != null) ? ((TreeListItem)GetItem(ptx)) : root);
		if (treeListItem == null)
		{
			return null;
		}
		TreeListItem treeListItem2 = CreateItem(ctx);
		treeListItem.Add(treeListItem2);
		_ListRefresh();
		return treeListItem2;
	}

	public bool Remove(ITreeListItemCtx ctx)
	{
		if (SelectedItem == ctx)
		{
			SelectClear();
		}
		TreeListItem treeListItem = (TreeListItem)GetItem(ctx);
		if (treeListItem == null)
		{
			return false;
		}
		items.Remove(ctx);
		treeListItem?.Parent?.Remove(treeListItem);
		_ListRefresh();
		return true;
	}

	public ITreeListItem Clear(ITreeListItemCtx ctx)
	{
		SelectClear();
		TreeListItem treeListItem = ((ctx != null) ? ((TreeListItem)GetItem(ctx)) : root);
		if (treeListItem == null)
		{
			return null;
		}
		treeListItem.Clear();
		_ListRefresh();
		return treeListItem;
	}

	public void ClearAll()
	{
		Clear(null);
		items.Clear();
		_ListRefresh();
	}

	private void _SelectClear()
	{
		foreach (TreeListItem value in selecteds.Values)
		{
			value.IsSelected = false;
		}
		selecteds.Clear();
	}

	internal void SelectClear()
	{
		_SelectClear();
		SetValue(SelectedItemProperty, null);
	}

	internal void SelectSingle(TreeListItem item)
	{
		if (item.Ctx.CanSingleSelected())
		{
			_SelectClear();
			item.IsSelected = true;
			selecteds.Add(item.Ctx, item);
			SetValue(SelectedItemProperty, item.Ctx);
		}
	}

	internal void SelectShift(TreeListItem item)
	{
		if (SelectedItem == null || !(SelectedItem is ITreeListItemCtx))
		{
			SelectSingle(item);
			return;
		}
		ITreeListItemCtx ctx = (ITreeListItemCtx)SelectedItem;
		TreeListItem treeListItem = (TreeListItem)GetItem(ctx);
		if (item.Parent == treeListItem.Parent && item.Ctx.CanMultiSelectedWith(treeListItem.Ctx) && treeListItem.Ctx.CanMultiSelectedWith(item.Ctx))
		{
			_SelectClear();
			int num = Math.Min(item.ID, treeListItem.ID);
			int num2 = Math.Max(item.ID, treeListItem.ID);
			for (int i = num; i <= num2; i++)
			{
				TreeListItem treeListItem2 = (TreeListItem)item.Parent[i];
				treeListItem2.IsSelected = true;
				selecteds.Add(treeListItem2.Ctx, treeListItem2);
			}
		}
	}

	internal void SelectCtrl(TreeListItem item)
	{
		if (SelectedItem == null || !(SelectedItem is ITreeListItemCtx))
		{
			SelectSingle(item);
			return;
		}
		ITreeListItemCtx ctx = (ITreeListItemCtx)SelectedItem;
		TreeListItem treeListItem = (TreeListItem)GetItem(ctx);
		if (item.Ctx.CanMultiSelectedWith(treeListItem.Ctx) && treeListItem.Ctx.CanMultiSelectedWith(item.Ctx))
		{
			item.IsSelected = true;
			selecteds.Add(item.Ctx, item);
		}
	}

	internal void ExpandChange(TreeListItem item)
	{
		SelectSingle(item);
		item.IsExpand = !item.IsExpand;
		_ListRefresh();
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/SamSoarII.Dock;component/treelist/treelist.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	void IComponentConnector.Connect(int connectionId, object target)
	{
		_contentLoaded = true;
	}
}
