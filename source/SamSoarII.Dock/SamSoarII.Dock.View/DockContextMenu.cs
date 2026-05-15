using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SamSoarII.Dock.Interface;
using SamSoarII.Dock.Properties;

namespace SamSoarII.Dock.View;

internal class DockContextMenu : ContextMenu
{
	private static readonly ImageSource Image_Float = new BitmapImage(new Uri("pack://application:,,,/SamSoarII.Dock;component/Resources/Images/Icon_Float.png"));

	private static readonly ImageSource Image_Dock = new BitmapImage(new Uri("pack://application:,,,/SamSoarII.Dock;component/Resources/Images/Icon_Pin.png"));

	private DockManager parent;

	private IDockBaseView core;

	private MenuItem miFloat;

	private MenuItem miDock;

	private MenuItem miDockDoc;

	private MenuItem miAutoHide;

	private MenuItem miHide;

	public DockManager ViewParent => parent;

	public IDockBaseView Core
	{
		get
		{
			return core;
		}
		set
		{
			core = value;
			miFloat.Header = SamSoarII.Dock.Properties.Resources.Anchorable_Float;
			miDock.Header = SamSoarII.Dock.Properties.Resources.Anchorable_Dock;
			miDockDoc.Header = SamSoarII.Dock.Properties.Resources.Anchorable_DockAsDocument;
			miAutoHide.Header = SamSoarII.Dock.Properties.Resources.Anchorable_AutoHide;
			miHide.Header = SamSoarII.Dock.Properties.Resources.Anchorable_Hide;
			switch (core?.HeaderType)
			{
			case ViewCommon.HeaderTypes.AutoHide:
				miFloat.IsEnabled = true;
				miDock.IsEnabled = true;
				miDockDoc.IsEnabled = true;
				miAutoHide.IsEnabled = false;
				miHide.IsEnabled = true;
				break;
			case ViewCommon.HeaderTypes.Dock:
				miFloat.IsEnabled = true;
				miDock.IsEnabled = false;
				miDockDoc.IsEnabled = true;
				miAutoHide.IsEnabled = core.Type != ViewCommon.BaseViewTypes.Document;
				miHide.IsEnabled = true;
				break;
			case ViewCommon.HeaderTypes.Float:
			case ViewCommon.HeaderTypes.FloatMaximized:
				miFloat.IsEnabled = false;
				miDock.IsEnabled = true;
				miDockDoc.IsEnabled = true;
				miAutoHide.IsEnabled = core.Type != ViewCommon.BaseViewTypes.Document;
				miHide.IsEnabled = true;
				break;
			case ViewCommon.HeaderTypes.FloatInner:
				miFloat.IsEnabled = true;
				miDock.IsEnabled = true;
				miDockDoc.IsEnabled = true;
				miAutoHide.IsEnabled = false;
				miHide.IsEnabled = true;
				break;
			}
			((Image)miFloat.Icon).Opacity = (miFloat.IsEnabled ? 1.0 : 0.5);
			((Image)miDock.Icon).Opacity = (miDock.IsEnabled ? 1.0 : 0.5);
		}
	}

	public DockContextMenu(DockManager _parent)
	{
		parent = _parent;
		miFloat = new MenuItem();
		miDock = new MenuItem();
		miDockDoc = new MenuItem();
		miAutoHide = new MenuItem();
		miHide = new MenuItem();
		miFloat.Icon = new Image
		{
			Source = Image_Float,
			Width = 16.0,
			Height = 16.0
		};
		miFloat.Click += OnMenuItemClicked;
		miDock.Icon = new Image
		{
			Source = Image_Dock,
			Width = 16.0,
			Height = 16.0
		};
		miDock.Click += OnMenuItemClicked;
		miDockDoc.Click += OnMenuItemClicked;
		miAutoHide.Click += OnMenuItemClicked;
		miHide.Click += OnMenuItemClicked;
		base.Items.Add(miFloat);
		base.Items.Add(miDock);
		base.Items.Add(miDockDoc);
		base.Items.Add(miAutoHide);
		base.Items.Add(miHide);
	}

	private void OnMenuItemClicked(object sender, RoutedEventArgs e)
	{
		Point point = new Point(0.0, 0.0);
		if (core is FrameworkElement)
		{
			point = ((FrameworkElement)core).PointToScreen(new Point(0.0, 0.0));
		}
		if (sender == miFloat)
		{
			parent.ShowFloatWindow(core, point.X * 0.75, point.Y * 0.75);
		}
		if (sender == miDock)
		{
			parent.CommandDock(core);
		}
		if (sender == miDockDoc)
		{
			parent.CommandDockAsDocument(core);
		}
		if (sender == miAutoHide)
		{
			parent.CommandAutoHide(core);
		}
		if (sender == miHide)
		{
			parent.CommandClose(core);
		}
	}
}
