using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;
using SamSoarII.Dock.Interface;

namespace SamSoarII.Dock.View.Tab;

internal class DockTab : Grid, IDisposable, IDockCollection, IDockView
{
	public enum ItemBarAlignments
	{
		Top,
		Bottom,
		Left,
		Right
	}

	public enum Themes
	{
		Classic,
		SamSoarII
	}

	private bool isdisposed = false;

	private DockManager parent;

	private DockTabManager manager;

	private ItemBarAlignments itembaralignment;

	private Themes theme;

	private List<DockTabContainer> children;

	private int selectedindex;

	private DockTabBarInfo tabbarinfo;

	private List<DockTabBarInfo> hiddeninfos;

	private RowDefinition rdefbar;

	private RowDefinition rdefin;

	private DockTabBarContainer tabbar;

	private DockTabExtendMenu exmenu;

	private bool ischildchanging = false;

	private bool ischanged = false;

	private DispatcherTimer updatetimer;

	private int hidetoselect;

	public bool IsDisposed => isdisposed;

	public DockManager ViewParent => parent;

	IDockView IDockView.ViewParent
	{
		get
		{
			object result;
			if (ViewParent == null)
			{
				result = null;
			}
			else
			{
				IDockView viewParent = ViewParent;
				result = viewParent;
			}
			return (IDockView)result;
		}
	}

	public DockTabManager Manager => manager;

	public ItemBarAlignments ItemBarAlignment => itembaralignment;

	public Themes Theme => theme;

	public int Count => children.Count();

	public IEnumerable<IDockBaseView> ViewChildren => children.Select((DockTabContainer c) => c.ViewContent);

	public IDockBaseView this[int id] => children[id].ViewContent;

	public int SelectedIndex
	{
		get
		{
			return selectedindex;
		}
		set
		{
			FrameworkElement frameworkElement = null;
			if (SelectedItem is FrameworkElement)
			{
				frameworkElement = (FrameworkElement)SelectedItem;
				base.Children.Remove(frameworkElement);
			}
			selectedindex = value;
			if (SelectedItem is FrameworkElement)
			{
				frameworkElement = (FrameworkElement)SelectedItem;
				if (itembaralignment == ItemBarAlignments.Top)
				{
					Grid.SetColumn(frameworkElement, 0);
					Grid.SetRow(frameworkElement, 1);
				}
				if (itembaralignment == ItemBarAlignments.Bottom)
				{
					Grid.SetColumn(frameworkElement, 0);
					Grid.SetRow(frameworkElement, 0);
				}
				base.Children.Add(frameworkElement);
			}
			frameworkElement = ViewCommon.GetFrameworkElement(SelectedItem);
			ischanged = true;
			this.SelectionChanged(this, new RoutedEventArgs());
		}
	}

	public IDockBaseView SelectedItem
	{
		get
		{
			return (selectedindex >= 0 && selectedindex < Count) ? this[selectedindex] : null;
		}
		set
		{
			int selectedIndex = ((value?.DockContainer is DockTabContainer) ? children.IndexOf((DockTabContainer)value.DockContainer) : (-1));
			SelectedIndex = selectedIndex;
		}
	}

	public bool IsUserFocused => SelectedItem?.IsUserFocused ?? false;

	public DockTabBarInfo TabbarInfo => tabbarinfo;

	public IList<DockTabBarInfo> HiddenInfos => hiddeninfos;

	public DockTabBarContainer TabBar => tabbar;

	public DockTabExtendMenu ExtendMenu => exmenu;

	public bool IsChildChanging => ischildchanging;

	public bool IsChanged
	{
		get
		{
			return ischanged;
		}
		set
		{
			ischanged = value;
		}
	}

	public event RoutedEventHandler SelectionChanged = delegate
	{
	};

	public DockTab(DockManager _parent, DockTabManager _manager, ItemBarAlignments _itembaralignment, Themes _theme = Themes.SamSoarII)
	{
		parent = _parent;
		manager = _manager;
		itembaralignment = _itembaralignment;
		theme = _theme;
		children = new List<DockTabContainer>();
		tabbar = new DockTabBarContainer(this);
		hiddeninfos = new List<DockTabBarInfo>();
		rdefbar = new RowDefinition
		{
			MinHeight = DockTabBarDrawer.TabBarHeight,
			MaxHeight = DockTabBarDrawer.TabBarHeight
		};
		rdefin = new RowDefinition
		{
			Height = new GridLength(1.0, GridUnitType.Star)
		};
		exmenu = new DockTabExtendMenu();
		selectedindex = -1;
		updatetimer = new DispatcherTimer(TimeSpan.FromMilliseconds(100.0), DispatcherPriority.Normal, TimerUpdate, base.Dispatcher);
		exmenu.Closed += OnExtendMenuClosed;
		tabbar.HorizontalAlignment = HorizontalAlignment.Left;
		base.Children.Add(tabbar);
		base.Width = 500.0;
		base.Height = 500.0;
		base.MinWidth = 80.0;
		base.MinHeight = 40.0;
		switch (itembaralignment)
		{
		case ItemBarAlignments.Top:
			base.RowDefinitions.Add(rdefbar);
			base.RowDefinitions.Add(rdefin);
			Grid.SetRow(tabbar, 0);
			break;
		case ItemBarAlignments.Bottom:
			base.RowDefinitions.Add(rdefin);
			base.RowDefinitions.Add(rdefbar);
			Grid.SetRow(tabbar, 1);
			break;
		}
	}

	public void Dispose()
	{
		if (!isdisposed)
		{
			isdisposed = true;
			updatetimer.Stop();
			ResetChild();
			parent = null;
			tabbar = null;
			updatetimer = null;
			manager = null;
		}
	}

	public void AddChild(IDockBaseView baseview)
	{
		ischildchanging = true;
		SelectedIndex = -1;
		DockTabContainer dockTabContainer = new DockTabContainer(this, baseview);
		dockTabContainer.ViewContent.DockContent.PropertyChanged += OnDockContentPropertyChanged;
		baseview.DockContainer = dockTabContainer;
		children.Add(dockTabContainer);
		SelectedIndex = children.Count() - 1;
		ischildchanging = false;
		ischanged = true;
		SizeUpdate();
	}

	public void InsertChild(IDockBaseView baseview, int index)
	{
		ischildchanging = true;
		SelectedIndex = -1;
		DockTabContainer dockTabContainer = new DockTabContainer(this, baseview);
		dockTabContainer.ViewContent.DockContent.PropertyChanged += OnDockContentPropertyChanged;
		baseview.DockContainer = dockTabContainer;
		children.Insert(index, dockTabContainer);
		SelectedIndex = index;
		ischildchanging = false;
		ischanged = true;
		SizeUpdate();
	}

	public void RemoveChild(IDockBaseView baseview)
	{
		ischildchanging = true;
		if (!(baseview.DockContainer is DockTabContainer))
		{
			ischildchanging = false;
			return;
		}
		DockTabContainer dockTabContainer = (DockTabContainer)baseview.DockContainer;
		dockTabContainer.ViewContent.DockContent.PropertyChanged -= OnDockContentPropertyChanged;
		if (dockTabContainer.ViewParent != this)
		{
			ischildchanging = false;
			return;
		}
		int num = children.IndexOf(dockTabContainer);
		int selectedIndex = SelectedIndex;
		SelectedIndex = -1;
		children.Remove(dockTabContainer);
		if (!dockTabContainer.IsViewContentEditing)
		{
			dockTabContainer.ViewContent = null;
		}
		dockTabContainer.Dispose();
		SelectedIndex = Math.Min(children.Count() - 1, Math.Max(0, selectedIndex - ((num <= selectedIndex) ? 1 : 0)));
		ischildchanging = false;
		ischanged = true;
		SizeUpdate();
	}

	public void ExchangeChild(int index1, int index2)
	{
		ischildchanging = true;
		int selectedIndex = SelectedIndex;
		if (selectedIndex == index1 || selectedIndex == index2)
		{
			SelectedIndex = -1;
		}
		DockTabContainer value = children[index1];
		children[index1] = children[index2];
		children[index2] = value;
		if (selectedIndex == index1)
		{
			SelectedIndex = index2;
		}
		if (selectedIndex == index2)
		{
			SelectedIndex = index1;
		}
		ischildchanging = false;
		ischanged = true;
		SizeUpdate();
	}

	public void MoveChild(int index1, int index2)
	{
		ischildchanging = true;
		int selectedIndex = SelectedIndex;
		SelectedIndex = -1;
		DockTabContainer item = children[index1];
		children.Remove(item);
		children.Insert(index2, item);
		SelectedIndex = index2;
		ischildchanging = false;
		ischanged = true;
		SizeUpdate();
	}

	public void ResetChild()
	{
		ischildchanging = true;
		DockTabContainer[] array = children.ToArray();
		foreach (DockTabContainer dockTabContainer in array)
		{
			RemoveChild(dockTabContainer.ViewContent);
		}
		ischildchanging = false;
		ischanged = true;
	}

	public IDockBaseView GetChild(int index)
	{
		return (index < 0 || index >= Count) ? null : children[index]?.ViewContent;
	}

	private void TimerUpdate(object sender, EventArgs e)
	{
		if (!ischanged)
		{
			return;
		}
		ischanged = false;
		try
		{
			if (tabbarinfo != null)
			{
				tabbarinfo?.Dispose();
				tabbarinfo = null;
			}
			if (hiddeninfos != null)
			{
				foreach (DockTabBarInfo hiddeninfo in hiddeninfos)
				{
					hiddeninfo?.Dispose();
				}
				hiddeninfos = new List<DockTabBarInfo>();
			}
			tabbarinfo = new DockTabBarInfo(this, 0, base.ActualWidth - DockTabBarDrawer.ExtendWidth - 4.0);
			if (selectedindex > tabbarinfo.End)
			{
				try
				{
					MoveChild(selectedindex, 0);
					return;
				}
				catch (Exception)
				{
					ischanged = true;
					return;
				}
			}
			if (tabbarinfo.End + 1 < Count)
			{
				DockTabBarInfo dockTabBarInfo = new DockTabBarInfo(this, tabbarinfo.End + 1, 800.0);
				hiddeninfos.Add(dockTabBarInfo);
				while (dockTabBarInfo.End + 1 < Count)
				{
					dockTabBarInfo = new DockTabBarInfo(this, dockTabBarInfo.End + 1, 800.0);
					hiddeninfos.Add(dockTabBarInfo);
				}
			}
			try
			{
				exmenu.ItemsSource = hiddeninfos;
				tabbar.Info = tabbarinfo;
				tabbar.Update();
			}
			catch (Exception)
			{
				ischanged = true;
			}
		}
		catch (Exception)
		{
			ischanged = true;
		}
	}

	protected virtual void SizeUpdate()
	{
		ItemBarAlignments itemBarAlignments = itembaralignment;
		ItemBarAlignments itemBarAlignments2 = itemBarAlignments;
		if ((uint)itemBarAlignments2 <= 1u)
		{
			tabbar.Width = base.ActualWidth;
		}
		foreach (IDockBaseView viewChild in ViewChildren)
		{
			if (viewChild is FrameworkElement)
			{
				FrameworkElement frameworkElement = (FrameworkElement)viewChild;
				frameworkElement.Height = rdefin.ActualHeight;
				frameworkElement.Width = base.ActualWidth;
			}
		}
	}

	public void ShowExtendMenu()
	{
		if (hiddeninfos.Count() > 0)
		{
			exmenu.Placement = PlacementMode.Relative;
			exmenu.PlacementTarget = this;
			exmenu.VerticalOffset = rdefbar.ActualHeight;
			exmenu.HorizontalOffset = base.ActualWidth - hiddeninfos.Select((DockTabBarInfo hi) => hi.Infos.Last().Right).Max();
			exmenu.IsOpen = true;
		}
	}

	public void HideExtendMenu(int _hidetoselect = -1)
	{
		hidetoselect = _hidetoselect;
		exmenu.IsOpen = false;
	}

	private void OnExtendMenuClosed(object sender, RoutedEventArgs e)
	{
		if (hidetoselect >= 0)
		{
			MoveChild(hidetoselect, 0);
			hidetoselect = -1;
		}
		tabbar.Update();
	}

	protected void DockTabManager_InvokeRemove()
	{
		if (Count <= 0 && manager != null && manager.Tab != this)
		{
			manager.Group?.RemoveChild(this);
			if (manager.Group != null && manager.Group.Count <= 1)
			{
				manager.GroupStyle = ViewCommon.TabGroupStyles.Single;
			}
			Dispose();
		}
	}

	protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
	{
		base.OnRenderSizeChanged(sizeInfo);
		ischanged = true;
		SizeUpdate();
	}

	private void OnSelectedItemLoaded(object sender, RoutedEventArgs e)
	{
		FrameworkElement frameworkElement = (FrameworkElement)sender;
		frameworkElement.Loaded -= OnSelectedItemLoaded;
		frameworkElement.Focus();
		Keyboard.Focus(frameworkElement);
	}

	private void OnDockContentPropertyChanged(object sender, PropertyChangedEventArgs e)
	{
		string propertyName = e.PropertyName;
		string text = propertyName;
		if (text == "Header")
		{
			ischanged = true;
		}
	}
}
