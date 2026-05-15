using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using SamSoarII.Dock.AutoHide;
using SamSoarII.Dock.Dock;
using SamSoarII.Dock.Float;
using SamSoarII.Dock.Global;
using SamSoarII.Dock.Interface;
using SamSoarII.Dock.View.Tab;

namespace SamSoarII.Dock.View;

internal class DockBaseView : Border, IUserFocus, IDockBaseView, IDockView, IDisposable
{
	private bool isdisposed = false;

	private ViewCommon.BaseViewTypes type;

	private DockManager parent;

	private IDockContainer dockcontainer;

	private IDockContent content;

	protected Grid maingrid;

	protected RowDefinition rdefheader;

	protected RowDefinition rdefinner;

	protected HeaderContainer header;

	protected DockBaseInner inner;

	private FrameworkElement lastfocus = null;

	public bool IsDisposed => isdisposed;

	public ViewCommon.BaseViewTypes Type => type;

	public DockManager ViewParent => parent;

	IDockView IDockView.ViewParent => ViewParent;

	DockManager IDockBaseView.DockManager => ViewParent;

	public IDockContainer DockContainer
	{
		get
		{
			return dockcontainer;
		}
		set
		{
			if (dockcontainer == value)
			{
				return;
			}
			IDockContainer dockContainer = dockcontainer;
			if (dockContainer is DockCollectionContainer)
			{
				DockCollectionContainer dockCollectionContainer = (DockCollectionContainer)dockContainer;
				if (dockCollectionContainer.ViewParent != null && !dockCollectionContainer.ViewParent.IsChildChanging)
				{
					dockCollectionContainer.ViewParent.RemoveChild(this);
				}
			}
			dockcontainer = null;
			if (dockContainer != null && dockContainer.ViewContent != null)
			{
				dockContainer.ViewContent = null;
			}
			dockcontainer = value;
			if (dockcontainer != null && dockcontainer.ViewContent != this)
			{
				dockcontainer.ViewContent = this;
			}
			if (type == ViewCommon.BaseViewTypes.Document)
			{
				if (dockcontainer is DockTabContainer)
				{
					RowDefinition rowDefinition = rdefheader;
					double minHeight = (rdefheader.MaxHeight = 0.0);
					rowDefinition.MinHeight = minHeight;
				}
				else
				{
					RowDefinition rowDefinition2 = rdefheader;
					double minHeight = (rdefheader.MaxHeight = HeaderDrawer.HeaderHeight);
					rowDefinition2.MinHeight = minHeight;
				}
			}
			else if (type == ViewCommon.BaseViewTypes.Anchor)
			{
				if (dockcontainer is DockTabContainer)
				{
					DockTabContainer dockTabContainer = (DockTabContainer)dockcontainer;
					FrameworkElement frameworkElement = (FrameworkElement)dockTabContainer.ViewParent;
					FrameworkElement fele = frameworkElement;
					IDockView dockView = ViewCommon.GetDockView(ref fele);
					if (dockView is FloatTab)
					{
						RowDefinition rowDefinition3 = rdefheader;
						double minHeight = (rdefheader.MaxHeight = HeaderDrawer.HeaderHeight);
						rowDefinition3.MinHeight = minHeight;
					}
					else if (frameworkElement is DockTab)
					{
						DockTab dockTab = (DockTab)frameworkElement;
						if (dockTab.ItemBarAlignment == DockTab.ItemBarAlignments.Top)
						{
							RowDefinition rowDefinition4 = rdefheader;
							double minHeight = (rdefheader.MaxHeight = 0.0);
							rowDefinition4.MinHeight = minHeight;
						}
						else
						{
							RowDefinition rowDefinition5 = rdefheader;
							double minHeight = (rdefheader.MaxHeight = HeaderDrawer.HeaderHeight);
							rowDefinition5.MinHeight = minHeight;
						}
					}
				}
				else
				{
					RowDefinition rowDefinition6 = rdefheader;
					double minHeight = (rdefheader.MaxHeight = HeaderDrawer.HeaderHeight);
					rowDefinition6.MinHeight = minHeight;
				}
			}
			parent.InvokeInShowedChanged(this);
		}
	}

	public IDockContent DockContent => content;

	public bool IsUserFocused => UserFocusManager.IsUserFocused(this);

	public ViewCommon.HeaderTypes HeaderType
	{
		get
		{
			if (dockcontainer == null)
			{
				return ViewCommon.HeaderTypes.Null;
			}
			if (dockcontainer is DockCollectionContainer)
			{
				DockCollectionContainer dockCollectionContainer = (DockCollectionContainer)dockcontainer;
				if (dockCollectionContainer.ViewParent is AutoHideSideBar)
				{
					return ViewCommon.HeaderTypes.AutoHide;
				}
				if (dockCollectionContainer.ViewParent is DockStackPanel)
				{
					DockStackPanel dockStackPanel = (DockStackPanel)dockCollectionContainer.ViewParent;
					return (Window.GetWindow(dockStackPanel) == dockStackPanel.ViewParent.WindowOwner) ? ViewCommon.HeaderTypes.Dock : ViewCommon.HeaderTypes.FloatInner;
				}
				if (dockCollectionContainer.ViewParent is DockTab)
				{
					DockTabContainer dockTabContainer = (DockTabContainer)dockcontainer;
					DockTab dockTab = (DockTab)dockTabContainer.ViewParent;
					if (type == ViewCommon.BaseViewTypes.Document)
					{
						return ViewCommon.HeaderTypes.Null;
					}
					if (type == ViewCommon.BaseViewTypes.Anchor)
					{
						if (dockTab.ItemBarAlignment == DockTab.ItemBarAlignments.Top)
						{
							return ViewCommon.HeaderTypes.Null;
						}
						return (Window.GetWindow(dockTab) == dockTab.ViewParent.WindowOwner) ? ViewCommon.HeaderTypes.Dock : ViewCommon.HeaderTypes.FloatInner;
					}
				}
				return ViewCommon.HeaderTypes.Null;
			}
			if (dockcontainer is FloatWindow)
			{
				FloatWindow floatWindow = (FloatWindow)dockcontainer;
				WindowState windowState = floatWindow.WindowState;
				WindowState windowState2 = windowState;
				if (windowState2 == WindowState.Maximized)
				{
					return ViewCommon.HeaderTypes.FloatMaximized;
				}
				return ViewCommon.HeaderTypes.Float;
			}
			return ViewCommon.HeaderTypes.Null;
		}
	}

	public Grid MainGrid => maingrid;

	public HeaderContainer Header => header;

	public DockBaseInner Inner => inner;

	public double ButtonStart => header.Drawer.ButtonStart;

	void IUserFocus.InvokeIsUserFocusedChanged()
	{
		header?.Update();
		base.BorderBrush = (IsUserFocused ? ViewCommon.Blue : ViewCommon.Gray);
		if (dockcontainer is DockTabContainer)
		{
			DockTabContainer dockTabContainer = (DockTabContainer)dockcontainer;
			((DockTab)dockTabContainer.ViewParent).IsChanged = true;
		}
		else if (dockcontainer is FloatWindow)
		{
			FloatWindow floatWindow = (FloatWindow)dockcontainer;
			floatWindow.InvokeIsUserFocusedChanged();
		}
	}

	public DockBaseView(DockManager _parent, IDockContent _content, ViewCommon.BaseViewTypes _type)
	{
		base.BorderThickness = new Thickness(0.75);
		base.BorderBrush = ViewCommon.Gray;
		base.Focusable = false;
		base.MinWidth = 80.0;
		base.MinHeight = 40.0;
		base.Background = Brushes.White;
		type = _type;
		parent = _parent;
		content = _content;
		maingrid = new Grid();
		header = new HeaderContainer(this);
		inner = new DockBaseInner(this);
		rdefheader = new RowDefinition
		{
			MinHeight = HeaderDrawer.HeaderHeight,
			MaxHeight = HeaderDrawer.HeaderHeight
		};
		rdefinner = new RowDefinition
		{
			Height = new GridLength(1.0, GridUnitType.Star)
		};
		Child = maingrid;
		maingrid.GotFocus += OnMainGridGotFocus;
		maingrid.GotKeyboardFocus += OnMainGridGotKeyboardFocus;
		maingrid.Focusable = true;
		maingrid.RowDefinitions.Add(rdefheader);
		maingrid.RowDefinitions.Add(rdefinner);
		Grid.SetRow(header, 0);
		Grid.SetRow(inner, 1);
		maingrid.Children.Add(header);
		maingrid.Children.Add(inner);
		inner.Content = content;
		base.Loaded += OnLoaded;
	}

	public void Dispose()
	{
		if (!isdisposed)
		{
			isdisposed = true;
			maingrid.GotFocus -= OnMainGridGotFocus;
			parent = null;
			content = null;
		}
	}

	public FloatWindow GetFloatWindow()
	{
		Window window = Window.GetWindow(this);
		return (window is FloatWindow) ? ((FloatWindow)window) : null;
	}

	private void OnLoaded(object sender, RoutedEventArgs e)
	{
		maingrid.Focus();
		Keyboard.Focus(maingrid);
	}

	private void OnMainGridGotFocus(object sender, RoutedEventArgs e)
	{
		UserFocusManager.Focus(this);
		Window window = Window.GetWindow(this);
		if (window == null)
		{
			return;
		}
		IInputElement focusedElement = FocusManager.GetFocusedElement(window);
		if (focusedElement == maingrid)
		{
			if (lastfocus != null && IsAncestorOf(lastfocus))
			{
				lastfocus.Focus();
				Keyboard.Focus(lastfocus);
			}
			else if (DockContent is FrameworkElement)
			{
				FrameworkElement frameworkElement = (FrameworkElement)DockContent;
				frameworkElement.Focus();
				Keyboard.Focus(frameworkElement);
			}
		}
		else if (focusedElement is FrameworkElement)
		{
			lastfocus = (FrameworkElement)focusedElement;
		}
	}

	private void OnMainGridGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
	{
		UserFocusManager.Focus(this);
	}

	protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
	{
		base.OnPropertyChanged(e);
		if (e.Property == UIElement.IsVisibleProperty && (bool)e.NewValue)
		{
			header.Update();
		}
		if (e.Property == FrameworkElement.WidthProperty || e.Property == FrameworkElement.HeightProperty)
		{
			parent.WriteSize(this);
		}
	}
}
