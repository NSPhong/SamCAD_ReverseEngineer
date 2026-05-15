using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Threading;
using SamSoarII.Polyline.Control.TreeView;
using SamSoarII.Polyline.Entity;

namespace SamSoarII.Polyline.Control;

public class PolylineSelectTreeBox : UserControl, IComponentConnector, IStyleConnector
{
	protected static readonly DependencyProperty SelectedImageProperty = DependencyProperty.Register("SelectedImage", typeof(IPolylineImage), typeof(PolylineSelectTreeBox), new PropertyMetadata(null, OnPropertyChanged_SelectedImage));

	protected static readonly DependencyProperty SelectedEntityProperty = DependencyProperty.Register("SelectedEntity", typeof(IPolylineEntity), typeof(PolylineSelectTreeBox), new PropertyMetadata(null, OnPropertyChanged_SelectedEntity));

	protected static readonly DependencyProperty ItemsProperty = DependencyProperty.Register("Items", typeof(IList<IPolylineEntity>), typeof(PolylineSelectTreeBox), new PropertyMetadata(new IPolylineEntity[0], OnPropertyChanged_Items));

	protected static readonly DependencyProperty IsReorderModeProperty = DependencyProperty.Register("IsReorderMode", typeof(bool), typeof(PolylineSelectTreeBox), new PropertyMetadata(false, OnPropertyChanged_IsReorderMode));

	private DispatcherTimer timer;

	private List<PolylineSelectTreeView_Group> reorderings;

	private PolylineSelectReorderContextMenu cm_reorder;

	private IPolylineReorderingGroup reordertarget;

	private ObservableCollection<IPolylineEntity> reorders;

	private ObservableCollection<IPolylineReorderingGroup> moveds;

	private PolylineSelectItemPanel itemspanel;

	private bool _invoke_selectionchanged = true;

	private RoutedEvent _sc_last_routedevent = null;

	private List<IPolylineEntity> _sc_addeditems = null;

	private List<IPolylineEntity> _sc_removeditems = null;

	internal PolylineSelectTreeBox This;

	internal ListBox LB_Main;

	private bool _contentLoaded;

	public IPolylineImage SelectedImage
	{
		get
		{
			return (IPolylineImage)GetValue(SelectedImageProperty);
		}
		set
		{
			SetValue(SelectedImageProperty, value);
		}
	}

	public IPolylineEntity SelectedEntity
	{
		get
		{
			return (IPolylineEntity)GetValue(SelectedEntityProperty);
		}
		set
		{
			SetValue(SelectedEntityProperty, value);
		}
	}

	public IList<IPolylineEntity> Items
	{
		get
		{
			return (IList<IPolylineEntity>)GetValue(ItemsProperty);
		}
		set
		{
			SetValue(ItemsProperty, value);
		}
	}

	public bool IsReorderMode
	{
		get
		{
			return (bool)GetValue(IsReorderModeProperty);
		}
		set
		{
			SetValue(IsReorderModeProperty, value);
		}
	}

	public IEnumerable<IPolylineEntity> SelectedItems => LB_Main.SelectedItems?.Cast<IPolylineEntity>();

	public ObservableCollection<IPolylineEntity> Reorders => reorders;

	public ObservableCollection<IPolylineReorderingGroup> Moveds => moveds;

	public PolylineSelectItemPanel ItemsPanel
	{
		get
		{
			return itemspanel;
		}
		set
		{
			itemspanel = value;
		}
	}

	public event SelectionChangedEventHandler SelectionChanged;

	public PolylineSelectTreeBox()
	{
		InitializeComponent();
		reorders = new ObservableCollection<IPolylineEntity>();
		moveds = new ObservableCollection<IPolylineReorderingGroup>();
		timer = new DispatcherTimer(new TimeSpan(0, 0, 0, 0, 100), DispatcherPriority.Normal, OnTimer, base.Dispatcher);
		reorderings = new List<PolylineSelectTreeView_Group>();
		cm_reorder = new PolylineSelectReorderContextMenu();
		reordertarget = null;
		cm_reorder.Reorder += OnContextMenuReorder;
	}

	private static void OnPropertyChanged_SelectedImage(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PolylineSelectTreeBox)
		{
			((PolylineSelectTreeBox)d).OnSelectedImageChanged(e);
		}
	}

	protected virtual void OnSelectedImageChanged(DependencyPropertyChangedEventArgs e)
	{
		InvalidateItems();
	}

	private static void OnPropertyChanged_SelectedEntity(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PolylineSelectTreeBox)
		{
			((PolylineSelectTreeBox)d).OnSelectedEntityChanged(e);
		}
	}

	protected virtual void OnSelectedEntityChanged(DependencyPropertyChangedEventArgs e)
	{
		if (SelectedEntity != null)
		{
			LB_Main.ScrollIntoView(SelectedEntity);
		}
	}

	private static void OnPropertyChanged_Items(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PolylineSelectTreeBox)
		{
			((PolylineSelectTreeBox)d).OnItemsChanged(e);
		}
	}

	protected virtual void OnItemsChanged(DependencyPropertyChangedEventArgs e)
	{
	}

	private static void OnPropertyChanged_IsReorderMode(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PolylineSelectTreeBox)
		{
			((PolylineSelectTreeBox)d).OnIsReorderModeChanged(e);
		}
	}

	protected virtual void OnIsReorderModeChanged(DependencyPropertyChangedEventArgs e)
	{
		base.ContextMenu = (IsReorderMode ? cm_reorder : null);
	}

	public void InvalidateItems()
	{
		if (SelectedImage == null)
		{
			Items = new IPolylineEntity[0];
			return;
		}
		List<IPolylineEntity> list = new List<IPolylineEntity>();
		List<IPolylineEntity> list2 = new List<IPolylineEntity>();
		int num = 0;
		list2.AddRange(SelectedImage.Groups.Where((IPolylineGroup g) => g.IsSelected));
		list2.AddRange(SelectedImage.Items.Where((IPolylineEntity i) => i.IsSelected));
		if (SelectedImage != null)
		{
			foreach (IPolylineGroup group in SelectedImage.Groups)
			{
				if (num < group.Start)
				{
					for (int num2 = num; num2 < group.Start; num2++)
					{
						list.Add(SelectedImage.Items[num2]);
					}
				}
				list.Add(group);
				bool flag = false;
				for (int num3 = group.Start; num3 < group.Start + group.Count; num3++)
				{
					if (SelectedImage.Items[num3].IsSelected)
					{
						flag = true;
						break;
					}
				}
				group.IsExpand |= flag;
				if (group.IsExpand)
				{
					for (int num4 = group.Start; num4 < group.Start + group.Count; num4++)
					{
						list.Add(SelectedImage.Items[num4]);
					}
				}
				num = group.Start + group.Count;
			}
			if (num < SelectedImage.Items.Count)
			{
				for (int num5 = num; num5 < SelectedImage.Items.Count; num5++)
				{
					list.Add(SelectedImage.Items[num5]);
				}
			}
		}
		Items = list;
		LB_Main_SelectionChanged_Begin();
		LB_Main.SelectedItems.Clear();
		foreach (IPolylineEntity item in list2)
		{
			LB_Main.SelectedItems.Add(item);
		}
		LB_Main_SelectionChanged_End();
	}

	public void Select(int start, int count)
	{
		if (SelectedImage == null)
		{
			return;
		}
		bool flag = false;
		SelectedImage.Select(start, count);
		foreach (IPolylineGroup group in SelectedImage.Groups)
		{
			group.IsSelected = false;
			if (group.Start + group.Count > start && group.Start < start + count)
			{
				if (!group.IsExpand)
				{
					flag = true;
				}
				group.IsExpand = true;
			}
		}
		if (flag)
		{
			InvalidateItems();
			return;
		}
		List<IPolylineEntity> list = new List<IPolylineEntity>();
		list.AddRange(SelectedImage.Items.Where((IPolylineEntity i) => i.IsSelected));
		list.AddRange(SelectedImage.Groups.Where((IPolylineGroup g) => g.IsSelected));
		LB_Main_SelectionChanged_Begin();
		LB_Main.SelectedItems.Clear();
		foreach (IPolylineEntity item in list)
		{
			LB_Main.SelectedItems.Add(item);
		}
		LB_Main_SelectionChanged_End();
	}

	public void SelectSingle(IPolylineEntity target)
	{
		if (SelectedEntity == target)
		{
			if (LB_Main.SelectedItems != null && LB_Main.SelectedItems.Count > 1)
			{
				SelectedEntity = null;
				SelectedEntity = target;
			}
		}
		else
		{
			SelectedEntity = target;
		}
	}

	public void SelectShift(int index)
	{
		if (SelectedImage != null && SelectedEntity != null)
		{
			SelectShift(SelectedImage.Items[index]);
		}
	}

	public void SelectCtrl(int index)
	{
		if (SelectedImage != null && SelectedEntity != null)
		{
			SelectCtrl(SelectedImage.Items[index]);
		}
	}

	public void SelectShift(IPolylineEntity target)
	{
		if (SelectedImage == null || SelectedEntity == null)
		{
			return;
		}
		IPolylineEntity selectedEntity = SelectedEntity;
		if (target.Group != null && !target.Group.IsExpand)
		{
			target.Group.IsExpand = true;
			InvalidateItems();
		}
		LB_Main_SelectionChanged_Begin();
		SelectedEntity = null;
		SelectedEntity = selectedEntity;
		int num = Items.IndexOf(selectedEntity);
		int val = Items.IndexOf(target);
		for (int i = Math.Min(num, val); i <= Math.Max(num, val); i++)
		{
			if (i != num)
			{
				IPolylineEntity polylineEntity = Items[i];
				if (polylineEntity != target.Group && polylineEntity != selectedEntity.Group)
				{
					LB_Main.SelectedItems.Add(polylineEntity);
				}
			}
		}
		LB_Main_SelectionChanged_End();
	}

	public void SelectCtrl(IPolylineEntity target)
	{
		if (SelectedImage != null && SelectedEntity != null)
		{
			if (target.Group != null && !target.Group.IsExpand)
			{
				target.Group.IsExpand = true;
				InvalidateItems();
			}
			LB_Main_SelectionChanged_Begin();
			if (LB_Main.SelectedItems.Contains(target))
			{
				LB_Main.SelectedItems.Remove(target);
			}
			else
			{
				LB_Main.SelectedItems.Add(target);
			}
			LB_Main_SelectionChanged_End();
		}
	}

	public void ReorderBegin()
	{
		reorders.Clear();
		moveds.Clear();
		foreach (IPolylineGroup group in SelectedImage.Groups)
		{
			reorders.Add(new PolylineReorderingGroup(group));
		}
		IsReorderMode = true;
		Items = reorders;
	}

	public void ReorderEnd()
	{
		IsReorderMode = false;
		InvalidateItems();
	}

	public void ReorderEscape()
	{
		IsReorderMode = false;
		InvalidateItems();
	}

	private void LB_Main_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (e.AddedItems != null)
		{
			foreach (IPolylineEntity addedItem in e.AddedItems)
			{
				addedItem.IsSelected = true;
			}
		}
		if (e.RemovedItems != null)
		{
			foreach (IPolylineEntity removedItem in e.RemovedItems)
			{
				removedItem.IsSelected = false;
			}
		}
		if (_invoke_selectionchanged)
		{
			this.SelectionChanged?.Invoke(this, e);
			return;
		}
		_sc_last_routedevent = e.RoutedEvent;
		if (_sc_addeditems == null)
		{
			_sc_addeditems = new List<IPolylineEntity>();
		}
		if (_sc_removeditems == null)
		{
			_sc_removeditems = new List<IPolylineEntity>();
		}
		if (e.AddedItems != null)
		{
			_sc_addeditems.AddRange(e.AddedItems.Cast<IPolylineEntity>());
		}
		if (e.RemovedItems != null)
		{
			_sc_removeditems.AddRange(e.RemovedItems.Cast<IPolylineEntity>());
		}
	}

	protected void LB_Main_SelectionChanged_Begin()
	{
		_sc_addeditems = null;
		_sc_removeditems = null;
		_sc_last_routedevent = null;
		_invoke_selectionchanged = false;
	}

	protected void LB_Main_SelectionChanged_End()
	{
		if (_sc_last_routedevent != null)
		{
			SelectionChangedEventArgs e = new SelectionChangedEventArgs(_sc_last_routedevent, _sc_removeditems, _sc_addeditems);
			this.SelectionChanged?.Invoke(this, e);
		}
		_sc_addeditems = null;
		_sc_removeditems = null;
		_sc_last_routedevent = null;
		_invoke_selectionchanged = true;
	}

	private void ContentSite_MouseDown(object sender, MouseButtonEventArgs e)
	{
		if (!(sender is FrameworkElement))
		{
			return;
		}
		FrameworkElement frameworkElement = (FrameworkElement)sender;
		if (!(frameworkElement.DataContext is IPolylineEntity))
		{
			return;
		}
		IPolylineEntity polylineEntity = (IPolylineEntity)frameworkElement.DataContext;
		frameworkElement.Focus();
		Keyboard.Focus(frameworkElement);
		e.Handled = true;
		if (e.ChangedButton == MouseButton.Left)
		{
			switch (Keyboard.PrimaryDevice.Modifiers)
			{
			case ModifierKeys.None:
				SelectSingle(polylineEntity);
				CaptureMouse();
				break;
			case ModifierKeys.Shift:
				SelectShift(polylineEntity);
				break;
			case ModifierKeys.Control:
				SelectCtrl(polylineEntity);
				break;
			case ModifierKeys.Alt:
			case ModifierKeys.Alt | ModifierKeys.Control:
				break;
			}
		}
		else
		{
			if (e.ChangedButton != MouseButton.Right)
			{
				return;
			}
			if (IsReorderMode && ItemsPanel != null)
			{
				int num = (int)ItemsPanel.VerticalOffset;
				int num2 = (int)ItemsPanel.ViewportHeight;
				reordertarget = null;
				for (int i = num; i < num + num2; i++)
				{
					if (i < 0 || i >= reorders.Count() || !(reorders[i] is IPolylineReorderingGroup))
					{
						continue;
					}
					IPolylineReorderingGroup polylineReorderingGroup = (IPolylineReorderingGroup)reorders[i];
					if (polylineReorderingGroup.View != null)
					{
						Point position = e.GetPosition(polylineReorderingGroup.View);
						if (position.X >= 0.0 && position.Y >= 0.0 && position.X <= polylineReorderingGroup.View.ActualWidth && position.Y <= polylineReorderingGroup.View.ActualHeight)
						{
							reordertarget = polylineReorderingGroup;
							break;
						}
					}
				}
			}
			switch (Keyboard.PrimaryDevice.Modifiers)
			{
			case ModifierKeys.None:
				if (!polylineEntity.IsSelected && !IsReorderMode)
				{
					SelectSingle(polylineEntity);
				}
				break;
			case ModifierKeys.Control:
			case ModifierKeys.Shift:
				break;
			case ModifierKeys.Alt:
			case ModifierKeys.Alt | ModifierKeys.Control:
				break;
			}
		}
	}

	protected override void OnMouseUp(MouseButtonEventArgs e)
	{
		base.OnMouseUp(e);
		ReleaseMouseCapture();
	}

	protected override void OnMouseMove(MouseEventArgs e)
	{
		base.OnMouseMove(e);
		if (e.LeftButton != MouseButtonState.Pressed || !(SelectedEntity is IPolylineReorderingGroup))
		{
			return;
		}
		IPolylineReorderingGroup polylineReorderingGroup = (IPolylineReorderingGroup)SelectedEntity;
		if (polylineReorderingGroup.View == null)
		{
			return;
		}
		FrameworkElement view = polylineReorderingGroup.View;
		Point position = e.GetPosition(view);
		if (position.Y <= 0.0)
		{
			if (polylineReorderingGroup.NewGID > 0)
			{
				IPolylineReorderingGroup polylineReorderingGroup2 = (IPolylineReorderingGroup)reorders[polylineReorderingGroup.NewGID - 1];
				reorders.Move(polylineReorderingGroup.NewGID, polylineReorderingGroup.NewGID - 1);
				polylineReorderingGroup2.NewGID++;
				polylineReorderingGroup.NewGID--;
				if (!polylineReorderingGroup.IsMoved)
				{
					polylineReorderingGroup.IsMoved = true;
					moveds.Add(polylineReorderingGroup);
				}
				LB_Main.ScrollIntoView(polylineReorderingGroup);
			}
		}
		else if (position.Y >= view.ActualHeight && polylineReorderingGroup.NewGID + 1 < reorders.Count())
		{
			IPolylineReorderingGroup polylineReorderingGroup3 = (IPolylineReorderingGroup)reorders[polylineReorderingGroup.NewGID + 1];
			reorders.Move(polylineReorderingGroup.NewGID, polylineReorderingGroup.NewGID + 1);
			polylineReorderingGroup3.NewGID--;
			polylineReorderingGroup.NewGID++;
			if (!polylineReorderingGroup.IsMoved)
			{
				polylineReorderingGroup.IsMoved = true;
				moveds.Add(polylineReorderingGroup);
			}
			LB_Main.ScrollIntoView(polylineReorderingGroup);
		}
	}

	private void PolylineSelectTreeView_Group_ArrowClick(object sender, RoutedEventArgs e)
	{
		if (!IsReorderMode && sender is PolylineSelectTreeView_Group)
		{
			PolylineSelectTreeView_Group polylineSelectTreeView_Group = (PolylineSelectTreeView_Group)sender;
			if (polylineSelectTreeView_Group.Group != null)
			{
				polylineSelectTreeView_Group.Group.IsExpand = !polylineSelectTreeView_Group.Group.IsExpand;
				InvalidateItems();
			}
		}
	}

	private void PolylineSelectTreeView_Group_ReorderVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
	{
		if (!(sender is PolylineSelectTreeView_Group))
		{
			return;
		}
		PolylineSelectTreeView_Group item = (PolylineSelectTreeView_Group)sender;
		if (e.OldValue is bool && e.NewValue is bool)
		{
			if (!(bool)e.OldValue && (bool)e.NewValue)
			{
				reorderings.Add(item);
			}
			if ((bool)e.OldValue && !(bool)e.NewValue)
			{
				reorderings.Remove(item);
			}
		}
	}

	private void OnTimer(object sender, EventArgs e)
	{
		foreach (PolylineSelectTreeView_Group reordering in reorderings)
		{
			reordering.UpdateReorder();
		}
	}

	private void OnContextMenuReorder(object sender, ReorderEventArgs e)
	{
		if (SelectedEntity == null)
		{
			return;
		}
		List<IPolylineReorderingGroup> list = LB_Main.SelectedItems.Cast<IPolylineReorderingGroup>().ToList();
		List<IPolylineEntity> list2 = new List<IPolylineEntity>();
		list.Sort((IPolylineReorderingGroup g1, IPolylineReorderingGroup g2) => g1.NewGID.CompareTo(g2.NewGID));
		int newGID = list.FirstOrDefault().NewGID;
		int num = list.Count();
		int num2 = -1;
		switch (e.Command)
		{
		case ReorderCommands.MoveBefore:
			if (reordertarget != null)
			{
				num2 = reordertarget.NewGID;
			}
			break;
		case ReorderCommands.MoveAfter:
			if (reordertarget != null)
			{
				num2 = reordertarget.NewGID + 1;
			}
			break;
		case ReorderCommands.MoveHome:
			num2 = 0;
			break;
		case ReorderCommands.MoveEnd:
			num2 = reorders.Count();
			break;
		}
		if (num2 == newGID)
		{
			return;
		}
		foreach (IPolylineReorderingGroup item in list)
		{
			if (!item.IsMoved)
			{
				item.IsMoved = true;
				moveds.Add(item);
			}
		}
		if (num2 < newGID)
		{
			for (int num3 = 0; num3 < num2; num3++)
			{
				list2.Add(reorders[num3]);
			}
			list2.AddRange(list);
			for (int num4 = num2; num4 < newGID; num4++)
			{
				list2.Add(reorders[num4]);
			}
			for (int num5 = newGID + num; num5 < reorders.Count(); num5++)
			{
				list2.Add(reorders[num5]);
			}
		}
		else
		{
			for (int num6 = 0; num6 < newGID; num6++)
			{
				list2.Add(reorders[num6]);
			}
			for (int num7 = newGID + num; num7 < num2; num7++)
			{
				list2.Add(reorders[num7]);
			}
			list2.AddRange(list);
			for (int num8 = num2 + num; num8 < reorders.Count(); num8++)
			{
				list2.Add(reorders[num8]);
			}
		}
		reorders.Clear();
		foreach (IPolylineEntity item2 in list2)
		{
			IPolylineReorderingGroup polylineReorderingGroup = (IPolylineReorderingGroup)item2;
			polylineReorderingGroup.NewGID = reorders.Count();
			reorders.Add(item2);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/SamSoarII.Polyline;component/control/polylineselecttreebox.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	void IComponentConnector.Connect(int connectionId, object target)
	{
		switch (connectionId)
		{
		case 1:
			This = (PolylineSelectTreeBox)target;
			break;
		case 2:
			LB_Main = (ListBox)target;
			LB_Main.SelectionChanged += LB_Main_SelectionChanged;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	void IStyleConnector.Connect(int connectionId, object target)
	{
		if (connectionId == 3)
		{
			((ContentPresenter)target).MouseDown += ContentSite_MouseDown;
		}
	}
}
