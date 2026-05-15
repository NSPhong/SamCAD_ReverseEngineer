using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Shapes;
using SamSoarII.Dock.View.Tab;

namespace SamSoarII.Dock.View;

public class DockTabSuit : UserControl, IComponentConnector
{
	private DockTab tab;

	private int index;

	private Point PT_1;

	private Point PT_2;

	private Point PT_3;

	private Point PT_4;

	private Point PT_5;

	private Point PT_6;

	private Point PT_7;

	private Point PT_8;

	internal DockTabSuit self;

	internal Polygon polygon;

	private bool _contentLoaded;

	internal DockTab Tab
	{
		get
		{
			return tab;
		}
		set
		{
			tab = value;
			base.Visibility = Visibility.Hidden;
			if (tab != null && base.Parent is Canvas)
			{
				base.Visibility = Visibility.Visible;
				Canvas relativeTo = (Canvas)base.Parent;
				Point point = tab.TranslatePoint(new Point(0.0, 0.0), relativeTo);
				Canvas.SetLeft(this, point.X);
				Canvas.SetTop(this, point.Y);
				base.Width = tab.ActualWidth;
				base.Height = tab.ActualHeight;
				switch (tab.ItemBarAlignment)
				{
				case DockTab.ItemBarAlignments.Top:
					PT_1.X = 0.0;
					PT_1.Y = base.Height;
					PT_2.X = base.Width;
					PT_2.Y = base.Height;
					PT_3.X = base.Width;
					PT_3.Y = DockTabBarDrawer.TabBarHeight;
					PT_4.X = base.Width;
					PT_4.Y = DockTabBarDrawer.TabBarHeight;
					PT_5.X = base.Width;
					PT_5.Y = 0.0;
					PT_6.X = 0.0;
					PT_6.Y = 0.0;
					PT_7.X = 0.0;
					PT_7.Y = DockTabBarDrawer.TabBarHeight;
					PT_8.X = 0.0;
					PT_8.Y = DockTabBarDrawer.TabBarHeight;
					break;
				case DockTab.ItemBarAlignments.Bottom:
					PT_1.X = 0.0;
					PT_1.Y = 0.0;
					PT_2.X = base.Width;
					PT_2.Y = 0.0;
					PT_3.X = base.Width;
					PT_3.Y = base.Height - DockTabBarDrawer.TabBarHeight;
					PT_4.X = base.Width;
					PT_4.Y = base.Height - DockTabBarDrawer.TabBarHeight;
					PT_5.X = base.Width;
					PT_5.Y = base.Height;
					PT_6.X = 0.0;
					PT_6.Y = base.Height;
					PT_7.X = 0.0;
					PT_7.Y = base.Height - DockTabBarDrawer.TabBarHeight;
					PT_8.X = 0.0;
					PT_8.Y = base.Height - DockTabBarDrawer.TabBarHeight;
					break;
				}
				UpdatePolygon();
				SelectedIndex = -1;
			}
		}
	}

	internal int SelectedIndex
	{
		get
		{
			return index;
		}
		set
		{
			index = value;
			base.Visibility = Visibility.Hidden;
			if (tab != null && index >= 0 && index <= tab.TabbarInfo.Infos.Count)
			{
				base.Visibility = Visibility.Visible;
				DockTabItemInfo dockTabItemInfo = null;
				if (index < tab.TabbarInfo.Infos.Count)
				{
					dockTabItemInfo = tab.TabbarInfo.Infos[index];
					ref Point pT_ = ref PT_6;
					double x = (PT_7.X = dockTabItemInfo.Left);
					pT_.X = x;
					ref Point pT_2 = ref PT_4;
					x = (PT_5.X = dockTabItemInfo.Right);
					pT_2.X = x;
				}
				else
				{
					dockTabItemInfo = tab.TabbarInfo.Infos.LastOrDefault();
					ref Point pT_3 = ref PT_6;
					double x = (PT_7.X = dockTabItemInfo?.Right ?? 0.0);
					pT_3.X = x;
					ref Point pT_4 = ref PT_4;
					x = (PT_5.X = base.Width);
					pT_4.X = x;
				}
				UpdatePolygon();
			}
		}
	}

	public DockTabSuit()
	{
		InitializeComponent();
	}

	private void UpdatePolygon()
	{
		polygon.Points.Clear();
		polygon.Points.Add(PT_1);
		polygon.Points.Add(PT_2);
		polygon.Points.Add(PT_3);
		polygon.Points.Add(PT_4);
		polygon.Points.Add(PT_5);
		polygon.Points.Add(PT_6);
		polygon.Points.Add(PT_7);
		polygon.Points.Add(PT_8);
	}

	public void InvokeMouse(MouseEventArgs e)
	{
		if (tab == null)
		{
			return;
		}
		Point position = e.GetPosition(this);
		int num = -1;
		switch (tab.ItemBarAlignment)
		{
		case DockTab.ItemBarAlignments.Top:
		{
			if (position.Y < 0.0 || position.Y > DockTabBarDrawer.TabBarHeight || position.X < 0.0 || position.X > base.Width)
			{
				break;
			}
			for (int j = 0; j < tab.TabbarInfo.Infos.Count; j++)
			{
				DockTabItemInfo dockTabItemInfo2 = tab.TabbarInfo.Infos[j];
				if (dockTabItemInfo2.IsOver(position.X))
				{
					num = j;
				}
			}
			if (num == -1)
			{
				num = tab.TabbarInfo.Infos.Count;
			}
			break;
		}
		case DockTab.ItemBarAlignments.Bottom:
		{
			if (position.Y < base.Height - DockTabBarDrawer.TabBarHeight || position.Y > base.Height || position.X < 0.0 || position.X > base.Width)
			{
				break;
			}
			for (int i = 0; i < tab.TabbarInfo.Infos.Count; i++)
			{
				DockTabItemInfo dockTabItemInfo = tab.TabbarInfo.Infos[i];
				if (dockTabItemInfo.IsOver(position.X))
				{
					num = i;
				}
			}
			if (num == -1)
			{
				num = tab.TabbarInfo.Infos.Count;
			}
			break;
		}
		}
		SelectedIndex = num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/SamSoarII.Dock;component/view/docktabsuit.xaml", UriKind.Relative);
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
			self = (DockTabSuit)target;
			break;
		case 2:
			polygon = (Polygon)target;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}
