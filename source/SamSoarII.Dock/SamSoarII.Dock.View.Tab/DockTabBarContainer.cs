using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SamSoarII.Dock.View.Tab;

public class DockTabBarContainer : Panel
{
	private DockTab parent;

	private DockTabBarDrawer drawer;

	private DockTabBarInfo info;

	private int mouseoverindex;

	private bool closeover;

	private bool extendover;

	private bool leftdown = false;

	private Point leftstart = default(Point);

	private int leftindex = -1;

	internal DockTab ViewParent => parent;

	internal DockManager DockManager => (ViewParent?.ViewParent != null) ? ViewParent.ViewParent : ((info?.Parent?.ViewParent == null) ? null : info?.Parent?.ViewParent);

	internal DockTabBarDrawer Drawer => drawer;

	internal DockTabBarInfo Info
	{
		get
		{
			return info;
		}
		set
		{
			info = value;
			if (parent == null)
			{
				Update();
			}
		}
	}

	internal int MouseOverIndex => mouseoverindex;

	internal bool CloseOver => closeover;

	internal bool ExtendOver => extendover;

	internal bool IsExtendOpen
	{
		get
		{
			DockTab dockTab = parent;
			return dockTab != null && dockTab.ExtendMenu?.IsOpen == true;
		}
	}

	internal int SelectedIndex
	{
		get
		{
			return (parent != null && info != null) ? (parent.SelectedIndex - info.Start) : (-1);
		}
		set
		{
			if (parent != null && info != null)
			{
				parent.SelectedIndex = value + info.Start;
			}
		}
	}

	protected override int VisualChildrenCount => (drawer != null) ? 1 : 0;

	public DockTabBarContainer()
	{
		parent = null;
		drawer = new DockTabBarDrawer(this);
		mouseoverindex = -1;
		closeover = false;
		extendover = false;
		AddVisualChild(drawer);
		AddLogicalChild(drawer);
		base.ToolTip = "";
	}

	internal DockTabBarContainer(DockTab _parent)
	{
		parent = _parent;
		drawer = new DockTabBarDrawer(this);
		mouseoverindex = -1;
		closeover = false;
		extendover = false;
		AddVisualChild(drawer);
		AddLogicalChild(drawer);
		base.ToolTip = "";
	}

	protected override Visual GetVisualChild(int index)
	{
		return drawer;
	}

	internal void Update()
	{
		if (info != null && info.Parent != null)
		{
			drawer.Update();
		}
	}

	protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
	{
		base.OnRenderSizeChanged(sizeInfo);
		if (info != null && info.Parent != null && parent == null)
		{
			drawer.Update();
		}
	}

	protected override void OnMouseMove(MouseEventArgs e)
	{
		base.OnMouseMove(e);
		if (info == null || info.Parent == null)
		{
			return;
		}
		Point position = e.GetPosition(this);
		int num = -1;
		bool flag = false;
		bool flag2 = false;
		try
		{
			for (int i = 0; i < info.Infos.Count; i++)
			{
				DockTabItemInfo dockTabItemInfo = info.Infos[i];
				if (dockTabItemInfo.IsOver(position.X))
				{
					num = i;
					if (dockTabItemInfo.IsCloseOver(position.X))
					{
						flag = true;
					}
					break;
				}
			}
			if (position.X >= base.ActualWidth - DockTabBarDrawer.ExtendWidth && position.X <= base.ActualWidth)
			{
				flag2 = true;
			}
			if (!leftdown)
			{
				if (num != mouseoverindex || flag != closeover || flag2 != extendover)
				{
					mouseoverindex = num;
					closeover = flag;
					extendover = flag2;
					if (mouseoverindex >= 0 && MouseOverIndex < info.Infos.Count())
					{
						base.ToolTip = info.Infos[mouseoverindex].Container.HeaderText.Text;
					}
					else
					{
						base.ToolTip = "";
					}
					drawer.Update();
				}
				return;
			}
			mouseoverindex = -1;
			closeover = false;
			extendover = false;
			Vector vector = position - leftstart;
			if (vector.Y < -32.0 || vector.Y > 32.0)
			{
				ReleaseMouseCapture();
				DockManager.StartDrag(info.Infos[leftindex].Container.ViewContent);
				leftdown = false;
				leftindex = -1;
			}
			else if (num >= 0 && num != leftindex)
			{
				DockTabItemInfo dockTabItemInfo2 = info.Infos[num];
				DockTabItemInfo dockTabItemInfo3 = info.Infos[leftindex];
				bool flag3 = false;
				if ((leftindex - 1 != num) ? (dockTabItemInfo2.Right - position.X < dockTabItemInfo3.Width) : (position.X - dockTabItemInfo2.Left < dockTabItemInfo3.Width))
				{
					info.Parent.ExchangeChild(info.Start + leftindex, info.Start + num);
					leftindex = num;
				}
			}
		}
		catch (Exception)
		{
		}
	}

	protected override void OnMouseLeave(MouseEventArgs e)
	{
		base.OnMouseLeave(e);
		if (info != null && info.Parent != null && !leftdown)
		{
			mouseoverindex = -1;
			closeover = false;
			extendover = false;
			drawer.Update();
		}
	}

	protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
	{
		base.OnMouseLeftButtonDown(e);
		if (info == null || info.Parent == null)
		{
			return;
		}
		if (ViewParent != null)
		{
			if (mouseoverindex >= 0)
			{
				if (closeover)
				{
					if (mouseoverindex >= 0 && mouseoverindex < Info.Infos.Count())
					{
						DockManager.Hide(Info.Infos[mouseoverindex].Container.ViewContent);
					}
					return;
				}
				leftdown = true;
				leftstart = e.GetPosition(this);
				leftindex = mouseoverindex;
				if (ViewParent.SelectedIndex != mouseoverindex + info.Start)
				{
					ViewParent.SelectedIndex = mouseoverindex + info.Start;
				}
				else if (ViewParent.SelectedItem is DockBaseView)
				{
					((DockBaseView)ViewParent.SelectedItem).MainGrid.Focus();
				}
				CaptureMouse();
			}
			else if (extendover)
			{
				ViewParent.ShowExtendMenu();
			}
		}
		else if (mouseoverindex >= 0)
		{
			if (closeover)
			{
				DockManager.Hide(Info.Infos[mouseoverindex].Container.ViewContent);
			}
			else
			{
				info.Parent.HideExtendMenu(mouseoverindex + info.Start);
			}
		}
	}

	protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
	{
		base.OnMouseLeftButtonUp(e);
		leftdown = false;
		leftindex = -1;
		ReleaseMouseCapture();
	}

	protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
	{
		base.OnMouseRightButtonDown(e);
		DockTabContextMenu dockTabContextMenu = parent?.Manager?.CtxMenu;
		if (ViewParent != null && mouseoverindex >= 0)
		{
			if (ViewParent.SelectedIndex != mouseoverindex + info.Start)
			{
				ViewParent.SelectedIndex = mouseoverindex + info.Start;
			}
			else if (ViewParent.SelectedItem is DockBaseView)
			{
				((DockBaseView)ViewParent.SelectedItem).MainGrid.Focus();
			}
		}
		if (dockTabContextMenu != null)
		{
			dockTabContextMenu.Core = parent?.SelectedItem;
			dockTabContextMenu.IsOpen = true;
		}
	}

	protected override void OnMouseRightButtonUp(MouseButtonEventArgs e)
	{
		base.OnMouseRightButtonUp(e);
	}

	protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
	{
		base.OnPropertyChanged(e);
		if (e.Property == FrameworkElement.DataContextProperty)
		{
			if (e.OldValue is DockTabBarInfo)
			{
				Info = null;
			}
			if (e.NewValue is DockTabBarInfo)
			{
				Info = (DockTabBarInfo)e.NewValue;
			}
			if (Info != null && Info.Parent != null && Info.Infos.Count() > 0)
			{
				base.Height = DockTabBarDrawer.TabBarHeight;
				base.Width = Info.Infos.Last().Right;
				drawer.Update();
			}
		}
	}
}
