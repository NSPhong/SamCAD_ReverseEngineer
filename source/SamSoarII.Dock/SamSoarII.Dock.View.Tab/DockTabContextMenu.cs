using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SamSoarII.Dock.Interface;
using SamSoarII.Dock.Properties;

namespace SamSoarII.Dock.View.Tab;

internal class DockTabContextMenu : ContextMenu
{
	private static readonly ImageSource Image_Float = new BitmapImage(new Uri("pack://application:,,,/SamSoarII.Dock;component/Resources/Images/Icon_Float.png"));

	private static readonly ImageSource Image_CreateH = new BitmapImage(new Uri("pack://application:,,,/SamSoarII.Dock;component/Resources/Images/Icon_HorizonGroup.png"));

	private static readonly ImageSource Image_CreateV = new BitmapImage(new Uri("pack://application:,,,/SamSoarII.Dock;component/Resources/Images/Icon_VerticalGroup.png"));

	protected static readonly DependencyProperty CoreProperty = DependencyProperty.Register("Core", typeof(IDockBaseView), typeof(DockTabContextMenu), new PropertyMetadata(null, OnPropertyChanged_Core));

	private DockTabManager manager;

	private MenuItem miFloat;

	private MenuItem miFloatAll;

	private MenuItem miClose;

	private MenuItem miCreateH;

	private MenuItem miCreateV;

	private MenuItem miSplitH;

	private MenuItem miSplitV;

	private MenuItem miNext;

	private MenuItem miPrev;

	private MenuItem miMerge;

	public IDockBaseView Core
	{
		get
		{
			return (IDockBaseView)GetValue(CoreProperty);
		}
		set
		{
			SetValue(CoreProperty, value);
		}
	}

	public DockTabManager Manager => manager;

	public DockManager DockManager => manager.ViewParent;

	public DockTabContextMenu(DockTabManager _manager)
	{
		manager = _manager;
		miFloat = new MenuItem();
		miFloatAll = new MenuItem();
		miClose = new MenuItem();
		miCreateH = new MenuItem();
		miCreateV = new MenuItem();
		miSplitH = new MenuItem();
		miSplitV = new MenuItem();
		miNext = new MenuItem();
		miPrev = new MenuItem();
		miMerge = new MenuItem();
		miFloat.Icon = new Image
		{
			Source = Image_Float,
			Width = 16.0,
			Height = 16.0
		};
		miFloat.Click += OnMenuItemClick;
		miFloatAll.Click += OnMenuItemClick;
		miClose.Click += OnMenuItemClick;
		miCreateH.Icon = new Image
		{
			Source = Image_CreateH,
			Width = 16.0,
			Height = 16.0
		};
		miCreateH.Click += OnMenuItemClick;
		miCreateV.Icon = new Image
		{
			Source = Image_CreateV,
			Width = 16.0,
			Height = 16.0
		};
		miCreateV.Click += OnMenuItemClick;
		miSplitH.Click += OnMenuItemClick;
		miSplitV.Click += OnMenuItemClick;
		miNext.Click += OnMenuItemClick;
		miPrev.Click += OnMenuItemClick;
		miMerge.Click += OnMenuItemClick;
		base.Items.Add(miFloat);
		base.Items.Add(miFloatAll);
		base.Items.Add(miClose);
		base.Items.Add(new Separator());
		base.Items.Add(miCreateH);
		base.Items.Add(miCreateV);
		base.Items.Add(miSplitH);
		base.Items.Add(miSplitV);
		base.Items.Add(miNext);
		base.Items.Add(miPrev);
		base.Items.Add(miMerge);
	}

	private static void OnPropertyChanged_Core(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is DockTabContextMenu)
		{
			((DockTabContextMenu)d).OnCoreChanged(e);
		}
	}

	protected virtual void OnCoreChanged(DependencyPropertyChangedEventArgs e)
	{
		miFloat.Header = SamSoarII.Dock.Properties.Resources.Document_Float;
		miFloatAll.Header = SamSoarII.Dock.Properties.Resources.Document_FloatAll;
		miClose.Header = SamSoarII.Dock.Properties.Resources.Document_Close;
		miCreateH.Header = SamSoarII.Dock.Properties.Resources.Document_NewHorizontalTabGroup;
		miCreateV.Header = SamSoarII.Dock.Properties.Resources.Document_NewVerticalTabGroup;
		miSplitH.Header = SamSoarII.Dock.Properties.Resources.Document_SplitHorizontalTabGroup;
		miSplitV.Header = SamSoarII.Dock.Properties.Resources.Document_SplitVerticalTabGroup;
		miNext.Header = SamSoarII.Dock.Properties.Resources.Document_MoveToNextTabGroup;
		miPrev.Header = SamSoarII.Dock.Properties.Resources.Document_MoveToPreviousTabGroup;
		miMerge.Header = SamSoarII.Dock.Properties.Resources.Document_Merge;
		miFloat.IsEnabled = Core != null;
		miFloatAll.IsEnabled = true;
		miClose.IsEnabled = Core != null;
		miCreateH.IsEnabled = Core != null && manager.GroupStyle != ViewCommon.TabGroupStyles.Vertical;
		miCreateV.IsEnabled = Core != null && manager.GroupStyle != ViewCommon.TabGroupStyles.Horizontal;
		miSplitH.IsEnabled = Core != null && manager.GroupStyle != ViewCommon.TabGroupStyles.Vertical;
		miSplitV.IsEnabled = Core != null && manager.GroupStyle != ViewCommon.TabGroupStyles.Horizontal;
		miNext.IsEnabled = Core != null && manager.GroupStyle != ViewCommon.TabGroupStyles.Single;
		miPrev.IsEnabled = Core != null && manager.GroupStyle != ViewCommon.TabGroupStyles.Single;
		miMerge.IsEnabled = manager.GroupStyle != ViewCommon.TabGroupStyles.Single;
		if (Core != null)
		{
			DockTab tab = manager.GetTab(Core);
			int tabIndexOf = manager.GetTabIndexOf(Core);
			int selectedIndex = tab.SelectedIndex;
			miCreateH.IsEnabled = tab.Count > 1;
			miCreateV.IsEnabled = tab.Count > 1;
			miSplitH.IsEnabled = tab.Count > 1 && selectedIndex > 0;
			miSplitV.IsEnabled = tab.Count > 1 && selectedIndex > 0;
			miPrev.IsEnabled &= manager.Group != null && tabIndexOf > 0;
			miNext.IsEnabled &= manager.Group != null && tabIndexOf < manager.Group.Count - 1;
		}
		((Image)miFloat.Icon).Opacity = (miFloat.IsEnabled ? 1.0 : 0.5);
		((Image)miCreateH.Icon).Opacity = (miCreateH.IsEnabled ? 1.0 : 0.5);
		((Image)miCreateV.Icon).Opacity = (miCreateV.IsEnabled ? 1.0 : 0.5);
	}

	protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
	{
		base.OnPropertyChanged(e);
		if (e.Property == UIElement.IsVisibleProperty && e.OldValue is bool && e.NewValue is bool && (bool)e.OldValue && !(bool)e.NewValue)
		{
			Core = null;
		}
	}

	private void OnMenuItemClick(object sender, RoutedEventArgs e)
	{
		if (Core == null)
		{
			return;
		}
		Point point = new Point(0.0, 0.0);
		if (Core is FrameworkElement)
		{
			point = ((FrameworkElement)Core).PointToScreen(new Point(0.0, 0.0));
		}
		if (sender == miFloat)
		{
			DockManager?.ShowFloatWindow(Core, point.X * 0.75, point.Y * 0.75);
		}
		if (sender == miFloatAll)
		{
			IDockBaseView[] array = manager.ViewChildren.ToArray();
			foreach (IDockBaseView baseview in array)
			{
				DockManager?.ShowFloatWindow(baseview, point.X * 0.75, point.Y * 0.75);
				point.X += 32.0;
				point.Y += 20.0;
			}
		}
		if (sender == miClose)
		{
			DockManager?.CommandClose(Core);
		}
		if (sender == miCreateH || sender == miCreateV)
		{
			int tabIndexOf = manager.GetTabIndexOf(Core);
			manager.RemoveChild(Core);
			manager.GroupStyle = ((sender == miCreateH) ? ViewCommon.TabGroupStyles.Horizontal : ViewCommon.TabGroupStyles.Vertical);
			manager.InsertChild(Core, tabIndexOf + 1);
		}
		if (sender == miSplitH || sender == miSplitV)
		{
			DockTab tab = manager.GetTab(Core);
			int tabIndexOf2 = manager.GetTabIndexOf(Core);
			if (tab == null)
			{
				return;
			}
			int selectedIndex = tab.SelectedIndex;
			int count = tab.Count;
			IDockBaseView[] array2 = new IDockBaseView[count - selectedIndex];
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j] = tab.GetChild(j + selectedIndex);
			}
			IDockBaseView[] array3 = array2;
			foreach (IDockBaseView baseview2 in array3)
			{
				tab.RemoveChild(baseview2);
			}
			manager.GroupStyle = ((sender == miSplitH) ? ViewCommon.TabGroupStyles.Horizontal : ViewCommon.TabGroupStyles.Vertical);
			manager.InsertChild(array2[0], tabIndexOf2 + 1);
			for (int l = 1; l < array2.Length; l++)
			{
				manager.AddChild(array2[l], tabIndexOf2 + 1);
			}
		}
		if (sender == miPrev)
		{
			int tabIndexOf3 = manager.GetTabIndexOf(Core);
			manager.RemoveChild(Core);
			manager.AddChild(Core, tabIndexOf3 - 1);
		}
		if (sender == miNext)
		{
			DockTab tab2 = manager.GetTab(Core);
			int tabIndexOf4 = manager.GetTabIndexOf(Core);
			manager.RemoveChild(Core);
			int count2 = tab2.Count;
			manager.AddChild(Core, tabIndexOf4 + ((count2 > 0) ? 1 : 0));
		}
		if (sender == miMerge)
		{
			manager.GroupStyle = ViewCommon.TabGroupStyles.Single;
		}
	}
}
