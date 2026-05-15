using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SamSoarII.Dock.Dock;
using SamSoarII.Dock.Interface;

namespace SamSoarII.Dock.View.Tab;

internal class DockTabManager : UserControl, IDockContainer, IDockView
{
	protected static readonly DependencyProperty GroupStyleProperty = DependencyProperty.Register("GroupStyle", typeof(ViewCommon.TabGroupStyles), typeof(DockTabManager), new PropertyMetadata(ViewCommon.TabGroupStyles.Single, OnPropertyChanged_GroupStyle));

	private DockManager parent;

	private DockTabContextMenu ctxmenu;

	public ViewCommon.TabGroupStyles GroupStyle
	{
		get
		{
			return (ViewCommon.TabGroupStyles)GetValue(GroupStyleProperty);
		}
		set
		{
			SetValue(GroupStyleProperty, value);
		}
	}

	public DockManager ViewParent => parent;

	public DockTabContextMenu CtxMenu => ctxmenu;

	public DockTab Tab => (base.Content is DockTab) ? ((DockTab)base.Content) : null;

	public DockStackPanel Group => (base.Content is DockStackPanel) ? ((DockStackPanel)base.Content) : null;

	public IDockBaseView SelectedItem
	{
		get
		{
			if (GroupStyle == ViewCommon.TabGroupStyles.Single)
			{
				return Tab?.SelectedItem;
			}
			return Group.ViewChildren.Where((FrameworkElement v) => v is DockTab)?.Cast<DockTab>()?.FirstOrDefault((DockTab t) => t?.SelectedItem != null)?.SelectedItem;
		}
	}

	public int Count
	{
		get
		{
			if (GroupStyle == ViewCommon.TabGroupStyles.Single)
			{
				return Tab?.Count ?? 0;
			}
			return Group.ViewChildren.Select((FrameworkElement v) => (v is DockTab) ? ((DockTab)v).Count : 0).Sum();
		}
	}

	public IEnumerable<IDockBaseView> ViewChildren
	{
		get
		{
			if (Tab != null)
			{
				foreach (IDockBaseView viewChild in Tab.ViewChildren)
				{
					yield return viewChild;
				}
				yield break;
			}
			foreach (FrameworkElement fele in Group?.ViewChildren ?? new FrameworkElement[0])
			{
				if (!(fele is DockTab))
				{
					continue;
				}
				DockTab docktab = (DockTab)fele;
				foreach (IDockBaseView viewChild2 in docktab.ViewChildren)
				{
					yield return viewChild2;
				}
			}
		}
	}

	IDockBaseView IDockContainer.ViewContent
	{
		get
		{
			return ((IDockBaseView)Tab) ?? ((IDockBaseView)Group) ?? null;
		}
		set
		{
		}
	}

	IDockView IDockView.ViewParent => parent;

	public DockTabManager(DockManager _parent)
	{
		parent = _parent;
		ctxmenu = new DockTabContextMenu(this);
		base.Content = new DockTab(parent, this, DockTab.ItemBarAlignments.Top);
		Tab.Width = base.Width;
		Tab.Height = base.Height;
	}

	private static void OnPropertyChanged_GroupStyle(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is DockTabManager)
		{
			((DockTabManager)d).OnGroupStyleChanged(e);
		}
	}

	protected virtual void OnGroupStyleChanged(DependencyPropertyChangedEventArgs e)
	{
		List<DockTab> list = null;
		if (e.OldValue is ViewCommon.TabGroupStyles)
		{
			if ((ViewCommon.TabGroupStyles)e.OldValue == ViewCommon.TabGroupStyles.Single)
			{
				list = new List<DockTab> { Tab };
			}
			else if (Group != null)
			{
				list = Group.ViewChildren.Where((FrameworkElement v) => v is DockTab).Cast<DockTab>().ToList();
				Group.ResetChild();
				Group.Dispose();
			}
		}
		if (e.NewValue is ViewCommon.TabGroupStyles)
		{
			switch ((ViewCommon.TabGroupStyles)e.NewValue)
			{
			case ViewCommon.TabGroupStyles.Single:
				base.Content = list?.FirstOrDefault() ?? new DockTab(parent, this, DockTab.ItemBarAlignments.Top);
				break;
			case ViewCommon.TabGroupStyles.Vertical:
				base.Content = new DockStackVerticalPanel(parent);
				break;
			case ViewCommon.TabGroupStyles.Horizontal:
				base.Content = new DockStackHorizontalPanel(parent);
				break;
			default:
				base.Content = null;
				break;
			}
		}
		if (Tab != null)
		{
			Tab.Width = base.Width;
			Tab.Height = base.Height;
		}
		if (Group != null)
		{
			Group.Width = base.Width;
			Group.Height = base.Height;
		}
		if (Tab != null && list != null)
		{
			for (int num = 1; num < list.Count(); num++)
			{
				if (list[num] != null)
				{
					IDockBaseView[] array = list[num].ViewChildren.ToArray();
					list[num].ResetChild();
					list[num].Dispose();
					IDockBaseView[] array2 = array;
					foreach (IDockBaseView baseview in array2)
					{
						Tab.AddChild(baseview);
					}
				}
			}
		}
		if (Group == null || list == null)
		{
			return;
		}
		foreach (DockTab item in list)
		{
			Group.AddChild(item);
		}
	}

	public void InsertChild(IDockBaseView view, int index)
	{
		if (GroupStyle == ViewCommon.TabGroupStyles.Single)
		{
			Tab?.InsertChild(view, index);
			return;
		}
		DockTab dockTab = new DockTab(parent, this, DockTab.ItemBarAlignments.Top);
		dockTab.AddChild(view);
		Group?.InsertChild(index, dockTab);
	}

	public void AddChild(IDockBaseView view, int index1 = 0, int index2 = -1)
	{
		if (GroupStyle == ViewCommon.TabGroupStyles.Single)
		{
			Tab?.AddChild(view);
		}
		else if (Group != null && index1 >= 0 && index1 < Group.ViewChildren.Count() && Group.ViewChildren[index1] is DockTab)
		{
			DockTab dockTab = (DockTab)Group.ViewChildren[index1];
			if (index2 < 0 || index2 > dockTab.ViewChildren.Count())
			{
				dockTab.AddChild(view);
			}
			else
			{
				dockTab.InsertChild(view, index2);
			}
		}
	}

	public void RemoveChild(IDockBaseView view)
	{
		if (parent != null)
		{
			parent.Hide(view);
		}
		else if (view.DockContainer?.ViewParent is IDockCollection)
		{
			IDockCollection dockCollection = (IDockCollection)view.DockContainer.ViewParent;
			dockCollection.RemoveChild(view);
		}
	}

	public DockTab GetTab(IDockBaseView view)
	{
		if (GroupStyle == ViewCommon.TabGroupStyles.Single)
		{
			return Tab;
		}
		if (view.DockContainer?.ViewParent is DockTab)
		{
			return (DockTab)view.DockContainer.ViewParent;
		}
		return null;
	}

	public int GetTabIndexOf(IDockBaseView view)
	{
		if (GroupStyle == ViewCommon.TabGroupStyles.Single)
		{
			return 0;
		}
		if (view.DockContainer?.ViewParent is DockTab)
		{
			DockTab item = (DockTab)view.DockContainer.ViewParent;
			return Group.ViewChildren.IndexOf(item);
		}
		return -1;
	}

	protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
	{
		base.OnPropertyChanged(e);
		if (e.Property == FrameworkElement.WidthProperty)
		{
			if (Tab != null)
			{
				Tab.Width = base.Width;
			}
			if (Group != null)
			{
				Group.Width = base.Width;
			}
		}
		if (e.Property == FrameworkElement.HeightProperty)
		{
			if (Tab != null)
			{
				Tab.Height = base.Height;
			}
			if (Group != null)
			{
				Group.Height = base.Height;
			}
		}
	}
}
