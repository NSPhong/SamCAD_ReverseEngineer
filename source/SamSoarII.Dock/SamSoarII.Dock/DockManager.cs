using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using System.Xml.Linq;
using SamSoarII.Dock.AutoHide;
using SamSoarII.Dock.Dock;
using SamSoarII.Dock.Float;
using SamSoarII.Dock.Global;
using SamSoarII.Dock.Interface;
using SamSoarII.Dock.View;
using SamSoarII.Dock.View.Tab;
using SamSoarII.Dock.View.ToolBar;

namespace SamSoarII.Dock;

public class DockManager : System.Windows.Controls.UserControl, IDockView, IDisposable, IComponentConnector
{
	private bool isdisposed = false;

	private DockBaseViewInfo[] infos;

	private AutoHideSideBar[] sidebars;

	private List<FloatWindow> fwnds;

	private ISideStackPanel mainside;

	private AutoHidePopup autohidepopup;

	private DockContextMenu ctxmenu;

	private DockTabManager maintab;

	private MouseHelper mousehelper;

	private DockStackResizeLine resizeline;

	private bool ignoreattach = false;

	public static string RelativePath = "\\Resources\\LayoutConfig.xml";

	internal Grid GD_Main;

	internal RowDefinition RD_Top;

	internal RowDefinition RD_Center;

	internal RowDefinition RD_Bottom;

	internal ColumnDefinition CD_Left;

	internal ColumnDefinition CD_Center;

	internal ColumnDefinition CD_Right;

	internal System.Windows.Controls.UserControl UC_Main;

	internal DockPanel DP_AutoHide;

	internal Canvas CV_Resize;

	private bool _contentLoaded;

	public bool IsDisposed => isdisposed;

	internal DockTabManager MainTab => maintab;

	public IDockContent SelectedItem => maintab?.SelectedItem?.DockContent;

	internal MouseHelper MouseHelper => mousehelper;

	internal DockStackResizeLine ResizeLine => resizeline;

	IDockView IDockView.ViewParent => null;

	public Window WindowOwner => Window.GetWindow(this);

	public static string FilePath => Directory.GetParent(System.Windows.Forms.Application.ExecutablePath).FullName + RelativePath;

	public event RoutedEventHandler IsShowedChanged = delegate
	{
	};

	public event RoutedEventHandler MainTabEmptyed = delegate
	{
	};

	public DockManager()
	{
		InitializeComponent();
		ReinitalizeComponent();
		infos = new DockBaseViewInfo[65536];
		Load();
	}

	private void ReinitalizeComponent()
	{
		sidebars = new AutoHideSideBar[4];
		fwnds = new List<FloatWindow>();
		maintab = new DockTabManager(this);
		mainside = null;
		autohidepopup = new AutoHidePopup(this);
		ctxmenu = new DockContextMenu(this);
		mousehelper = new MouseHelper(this);
		resizeline = new DockStackResizeLine(this);
		for (int i = 0; i < 4; i++)
		{
			AutoHideCommon.Sides side = (AutoHideCommon.Sides)i;
			sidebars[i] = new AutoHideSideBar(this, side);
			GD_Main.Children.Add(sidebars[i]);
		}
		UC_Main.Content = maintab;
	}

	public void Dispose()
	{
		if (!isdisposed)
		{
			isdisposed = true;
			Save();
			mousehelper.Dispose();
		}
	}

	public bool ContainView(IDockContent dockcontent)
	{
		return infos[dockcontent.DockID]?.Content != null;
	}

	public void AddView(IDockContent dockcontent, ViewCommon.BaseViewTypes viewtype)
	{
		if (!ContainView(dockcontent))
		{
			int dockID = dockcontent.DockID;
			IDockBaseView baseview = null;
			switch (viewtype)
			{
			case ViewCommon.BaseViewTypes.Document:
			case ViewCommon.BaseViewTypes.Anchor:
				baseview = new DockBaseView(this, dockcontent, viewtype);
				break;
			case ViewCommon.BaseViewTypes.Toolbar:
				baseview = new DockToolBarView(this, dockcontent);
				break;
			}
			if (infos[dockID] == null)
			{
				infos[dockID] = new DockBaseViewInfo(this, dockID);
			}
			infos[dockID].Register(dockcontent, baseview);
			ApplySize(baseview);
		}
	}

	public void RemoveView(IDockContent dockcontent)
	{
		if (ContainView(dockcontent))
		{
			int dockID = dockcontent.DockID;
			IDockBaseView baseView = infos[dockID].BaseView;
			Hide(baseView);
			if (baseView is IDisposable)
			{
				((IDisposable)baseView).Dispose();
			}
			infos[dockID].Unregister();
		}
	}

	public void ResetView()
	{
		for (int i = 0; i < 65536; i++)
		{
			if (infos[i]?.Content != null)
			{
				RemoveView(infos[i].Content);
			}
		}
	}

	public IDockBaseViewInfo GetInfo(IDockContent dockcontent)
	{
		return infos[dockcontent.DockID];
	}

	public void AutoHideView(IDockContent dockcontent, AutoHideCommon.Sides side)
	{
		if (dockcontent != null && ContainView(dockcontent))
		{
			int dockID = dockcontent.DockID;
			IDockBaseView baseView = infos[dockID].BaseView;
			AddAutoHide(baseView, side);
		}
	}

	internal void AddAutoHide(IDockBaseView baseview, AutoHideCommon.Sides side)
	{
		if (baseview != null)
		{
			AutoHideSideBar autoHideSideBar = sidebars[(int)side];
			Hide(baseview);
			autoHideSideBar.AddChild(baseview);
			switch (side)
			{
			case AutoHideCommon.Sides.Left:
				WriteAttach(baseview, (IDockBaseView)null, ViewCommon.BaseViewRelations.LeftAutoHide);
				break;
			case AutoHideCommon.Sides.Right:
				WriteAttach(baseview, (IDockBaseView)null, ViewCommon.BaseViewRelations.RightAutoHide);
				break;
			case AutoHideCommon.Sides.Top:
				WriteAttach(baseview, (IDockBaseView)null, ViewCommon.BaseViewRelations.TopAutoHide);
				break;
			case AutoHideCommon.Sides.Bottom:
				WriteAttach(baseview, (IDockBaseView)null, ViewCommon.BaseViewRelations.BottomAutoHide);
				break;
			}
		}
	}

	internal void RemoveAutoHide(IDockBaseView baseview)
	{
		if (baseview != null && baseview.DockContainer is DockCollectionContainer)
		{
			DockCollectionContainer dockCollectionContainer = (DockCollectionContainer)baseview.DockContainer;
			if (dockCollectionContainer.ViewParent is AutoHideSideBar)
			{
				AutoHideSideBar autoHideSideBar = (AutoHideSideBar)dockCollectionContainer.ViewParent;
				autoHideSideBar.RemoveChild(baseview);
			}
		}
	}

	public void DockView(IDockContent moved, AutoHideCommon.Sides side)
	{
		if (moved != null && ContainView(moved))
		{
			int dockID = moved.DockID;
			IDockBaseView baseView = infos[dockID].BaseView;
			switch (side)
			{
			case AutoHideCommon.Sides.Left:
				ShowDockTo(baseView, baseView, ViewCommon.BaseViewRelations.LeftSide);
				break;
			case AutoHideCommon.Sides.Right:
				ShowDockTo(baseView, baseView, ViewCommon.BaseViewRelations.RightSide);
				break;
			case AutoHideCommon.Sides.Top:
				ShowDockTo(baseView, baseView, ViewCommon.BaseViewRelations.TopSide);
				break;
			case AutoHideCommon.Sides.Bottom:
				ShowDockTo(baseView, baseView, ViewCommon.BaseViewRelations.BottomSide);
				break;
			}
		}
	}

	public void DockViewTo(IDockContent moved, IDockContent target, ViewCommon.BaseViewRelations relation)
	{
		if (ContainView(moved) && ContainView(target))
		{
			int dockID = moved.DockID;
			int dockID2 = target.DockID;
			IDockBaseView baseView = infos[dockID].BaseView;
			IDockBaseView baseView2 = infos[dockID2].BaseView;
			ShowDockTo(baseView, baseView2, relation);
		}
	}

	public void Show(IDockContent dockcontent)
	{
		if (dockcontent != null && ContainView(dockcontent))
		{
			int dockID = dockcontent.DockID;
			IDockBaseView baseView = infos[dockID].BaseView;
			Show(baseView);
		}
	}

	public void Hide(IDockContent dockcontent)
	{
		if (dockcontent != null && ContainView(dockcontent))
		{
			int dockID = dockcontent.DockID;
			IDockBaseView baseView = infos[dockID].BaseView;
			Hide(baseView);
		}
	}

	public void ShowOrHide(IDockContent dockcontent)
	{
		if (dockcontent != null)
		{
			if (IsShowed(dockcontent))
			{
				Hide(dockcontent);
			}
			else
			{
				Show(dockcontent);
			}
		}
	}

	public void ShowFloat(IDockContent dockcontent, Rect rect)
	{
		if (dockcontent != null)
		{
			DockBaseViewInfo dockBaseViewInfo = infos[dockcontent.DockID];
			dockBaseViewInfo.SetRect(rect.Left, rect.Top, rect.Width, rect.Height);
			ShowFloatWindow(dockBaseViewInfo.BaseView, rect.Left, rect.Top);
		}
	}

	public void HideAll()
	{
		for (int i = 0; i < 65536; i++)
		{
			if (infos[i]?.BaseView != null)
			{
				CommandClose(infos[i].BaseView);
			}
		}
	}

	public void UpdateAll()
	{
		for (int i = 0; i < 65536; i++)
		{
			if (infos[i]?.BaseView == null)
			{
				continue;
			}
			if (infos[i].BaseView is DockBaseView)
			{
				DockBaseView dockBaseView = (DockBaseView)infos[i].BaseView;
				dockBaseView.Header.Update();
			}
			if (infos[i].BaseView.DockContainer is DockTabContainer)
			{
				DockTabContainer dockTabContainer = (DockTabContainer)infos[i].BaseView.DockContainer;
				DockTab dockTab = (DockTab)dockTabContainer.ViewParent;
				if (dockTab.SelectedItem == infos[i].BaseView)
				{
					dockTab.TabBar.Update();
				}
			}
		}
	}

	public bool IsShowed(IDockContent dockcontent)
	{
		if (dockcontent == null)
		{
			return false;
		}
		if (!ContainView(dockcontent))
		{
			return false;
		}
		int dockID = dockcontent.DockID;
		IDockBaseView baseView = infos[dockID].BaseView;
		return IsShowed(baseView);
	}

	public bool IsDocked(IDockContent dockcontent)
	{
		if (dockcontent == null)
		{
			return false;
		}
		if (!ContainView(dockcontent))
		{
			return false;
		}
		int dockID = dockcontent.DockID;
		IDockBaseView baseView = infos[dockID].BaseView;
		return IsDocked(baseView);
	}

	public bool IsFloated(IDockContent dockcontent)
	{
		if (dockcontent == null)
		{
			return false;
		}
		if (!ContainView(dockcontent))
		{
			return false;
		}
		int dockID = dockcontent.DockID;
		IDockBaseView baseView = infos[dockID].BaseView;
		return IsFloated(baseView);
	}

	internal void Show(IDockBaseView baseview)
	{
		if (baseview == null)
		{
			return;
		}
		if (IsShowed(baseview))
		{
			if (baseview.DockContainer is DockTabContainer)
			{
				DockTab dockTab = (DockTab)baseview.DockContainer.ViewParent;
				dockTab.SelectedItem = baseview;
			}
			else if (baseview is DockBaseView)
			{
				DockBaseView dockBaseView = (DockBaseView)baseview;
				dockBaseView.MainGrid.Focus();
			}
			return;
		}
		if (baseview.Type == ViewCommon.BaseViewTypes.Document)
		{
			if (baseview.DockContainer is DockTabContainer)
			{
				DockTabContainer dockTabContainer = (DockTabContainer)baseview.DockContainer;
				DockTab dockTab2 = (DockTab)dockTabContainer.ViewParent;
				dockTab2.SelectedItem = baseview;
			}
			else
			{
				maintab.AddChild(baseview);
			}
			return;
		}
		ignoreattach = true;
		IDockBaseView target = null;
		ViewCommon.BaseViewRelations relation = ViewCommon.BaseViewRelations.Null;
		DockBaseViewInfo dockBaseViewInfo = null;
		int num = 0;
		int num2 = 0;
		int lastchildid = -1;
		if (FindChildrenAttach(baseview.DockContent.DockID, -1, ref target, ref relation))
		{
			ShowDockTo(baseview, target, relation);
			ignoreattach = false;
			return;
		}
		dockBaseViewInfo = infos[baseview.DockContent.DockID];
		num = dockBaseViewInfo.TargetID;
		num2 = 0;
		while (num2++ < 256 && dockBaseViewInfo != null && num >= 0 && num < 256)
		{
			if (IsShowed(infos[num].BaseView))
			{
				target = infos[num].BaseView;
				relation = dockBaseViewInfo.Relation;
				break;
			}
			if (FindChildrenAttach(num, lastchildid, ref target, ref relation))
			{
				break;
			}
			dockBaseViewInfo = infos[num];
			lastchildid = num;
			num = dockBaseViewInfo.TargetID;
		}
		if (target != null || relation != ViewCommon.BaseViewRelations.Null)
		{
			ShowDockTo(baseview, target, relation);
			ignoreattach = false;
			return;
		}
		target = ((num < 0 || num >= 256) ? null : infos[num]?.BaseView);
		relation = ((dockBaseViewInfo == null) ? ViewCommon.BaseViewRelations.LeftSide : ((dockBaseViewInfo.Relation == ViewCommon.BaseViewRelations.DockAsDocument || dockBaseViewInfo.Relation == ViewCommon.BaseViewRelations.Float) ? dockBaseViewInfo.Relation : dockBaseViewInfo.LastSide));
		ShowDockTo(baseview, target, relation);
		ignoreattach = false;
	}

	internal void Hide(IDockBaseView baseview)
	{
		if (baseview == null)
		{
			return;
		}
		if (baseview is FloatStackPanel)
		{
			HideIn(((FloatStackPanel)baseview).ViewContent);
			return;
		}
		if (baseview is FloatTab)
		{
			HideIn(((FloatTab)baseview).ViewContent);
			return;
		}
		switch (baseview.HeaderType)
		{
		case ViewCommon.HeaderTypes.AutoHide:
			HideAutoHide();
			return;
		case ViewCommon.HeaderTypes.Float:
		case ViewCommon.HeaderTypes.FloatMaximized:
		{
			if (!(baseview.DockContainer is FloatWindow))
			{
				break;
			}
			FloatWindow fwnd = (FloatWindow)baseview.DockContainer;
			HideFloatWindow(fwnd);
			return;
		}
		}
		if (baseview.DockContainer is DockCollectionContainer)
		{
			DockCollectionContainer dockCollectionContainer = (DockCollectionContainer)baseview.DockContainer;
			IDockCollection dockCollection = dockCollectionContainer.ViewParent;
			if (dockCollection == null)
			{
				return;
			}
			dockCollection.RemoveChild(baseview);
			FrameworkElement fele = ((dockCollection is FrameworkElement) ? ((FrameworkElement)dockCollection) : null);
			FrameworkElement frameworkElement = null;
			IDockCollection dockCollection2 = ViewCommon.GetDockCollection(ref fele);
			List<IDockCollection> list = new List<IDockCollection>();
			fele = ((dockCollection is FrameworkElement) ? ((FrameworkElement)dockCollection) : null);
			IDockView dockView = ViewCommon.GetDockView(ref fele);
			while (dockCollection.Count <= 1 && dockCollection != maintab.Tab)
			{
				if (dockCollection is DockTab && ((DockTab)dockCollection).Manager != null)
				{
					DockTab dockTab = (DockTab)dockCollection;
					DockTabManager manager = dockTab.Manager;
					if (dockTab.Count != 0)
					{
						break;
					}
					if (manager.GroupStyle != ViewCommon.TabGroupStyles.Single)
					{
						manager.Group.RemoveChild(dockTab);
						if (manager.Group.Count == 1)
						{
							manager.GroupStyle = ViewCommon.TabGroupStyles.Single;
						}
					}
					else if (manager == maintab)
					{
					}
					break;
				}
				if (baseview is DockBaseView)
				{
					DockBaseView dockBaseView = (DockBaseView)baseview;
					if (dockBaseView.Type == ViewCommon.BaseViewTypes.Document && (dockCollection2 is SideStackHorizontalPanel || dockCollection2 is SideStackVerticalPanel))
					{
						break;
					}
				}
				if (dockCollection.Count > 0)
				{
					frameworkElement = null;
					if (dockCollection is DockStackPanel)
					{
						DockStackPanel dockStackPanel = (DockStackPanel)dockCollection;
						frameworkElement = dockStackPanel.ViewChildren[0];
						dockStackPanel.ResetChild();
					}
					else if (dockCollection is DockTab)
					{
						DockTab dockTab2 = (DockTab)dockCollection;
						IDockBaseView dockBaseView2 = dockTab2.ViewChildren.FirstOrDefault();
						frameworkElement = ((dockBaseView2 is FrameworkElement) ? ((FrameworkElement)dockBaseView2) : null);
						dockTab2.ResetChild();
					}
				}
				list.Add(dockCollection);
				if (dockCollection2 is DockStackPanel)
				{
					DockStackPanel dockStackPanel2 = (DockStackPanel)dockCollection2;
					if (frameworkElement == null)
					{
						dockStackPanel2.RemoveChild(fele);
					}
					else
					{
						dockStackPanel2.ReplaceChild(fele, frameworkElement);
					}
					dockCollection = dockStackPanel2;
					continue;
				}
				if (dockCollection2 is DockTab)
				{
					DockTab dockTab3 = (DockTab)dockCollection2;
					dockTab3.RemoveChild(baseview);
					dockCollection = dockTab3;
					continue;
				}
				if (dockView is FloatStackPanel)
				{
					FloatStackPanel floatStackPanel = (FloatStackPanel)dockView;
					FloatWindow viewParent = floatStackPanel.ViewParent;
					DockStackPanel viewContent = floatStackPanel.ViewContent;
					floatStackPanel.Dispose();
					if (viewParent.IsDisposed)
					{
						return;
					}
					if (frameworkElement is DockBaseView)
					{
						viewParent.ViewContent = (DockBaseView)frameworkElement;
					}
					else if (frameworkElement is DockStackPanel)
					{
						viewParent.ViewContent = new FloatStackPanel(viewParent, (DockStackPanel)frameworkElement)
						{
							Width = frameworkElement.Width,
							Height = frameworkElement.Height
						};
					}
					else if (frameworkElement is DockTab)
					{
						viewParent.ViewContent = new FloatTab(viewParent, (DockTab)frameworkElement)
						{
							Width = frameworkElement.Width,
							Height = frameworkElement.Height
						};
					}
					else
					{
						viewParent.Close();
					}
					break;
				}
				if (dockView is FloatTab)
				{
					FloatTab floatTab = (FloatTab)dockView;
					FloatWindow viewParent2 = floatTab.ViewParent;
					DockTab viewContent2 = floatTab.ViewContent;
					floatTab.Dispose();
					if (viewParent2.IsDisposed)
					{
						return;
					}
					if (frameworkElement is DockBaseView)
					{
						viewParent2.ViewContent = (DockBaseView)frameworkElement;
					}
					else if (frameworkElement is DockStackPanel)
					{
						viewParent2.ViewContent = new FloatStackPanel(viewParent2, (DockStackPanel)frameworkElement)
						{
							Width = frameworkElement.Width,
							Height = frameworkElement.Height
						};
					}
					else if (frameworkElement is DockTab)
					{
						viewParent2.ViewContent = new FloatTab(viewParent2, (DockTab)frameworkElement)
						{
							Width = frameworkElement.Width,
							Height = frameworkElement.Height
						};
					}
					else
					{
						viewParent2.Close();
					}
					break;
				}
				if (dockCollection != mainside)
				{
					break;
				}
				UC_Main.Content = null;
				if (frameworkElement != null)
				{
					UC_Main.Content = frameworkElement;
					if (frameworkElement is ISideStackPanel)
					{
						mainside = (ISideStackPanel)frameworkElement;
					}
					else
					{
						mainside = null;
					}
				}
				break;
			}
			foreach (IDockCollection item in list)
			{
				if (item is IDisposable)
				{
					((IDisposable)item).Dispose();
				}
			}
		}
		if (maintab == null || maintab.Count == 0)
		{
			this.MainTabEmptyed(this, new RoutedEventArgs());
		}
	}

	internal void HideIn(FrameworkElement fele)
	{
		if (fele == null)
		{
			return;
		}
		ViewCommon.HandleIn(fele, delegate(FrameworkElement _fele)
		{
			if (!(_fele is IDockBaseView))
			{
				return false;
			}
			Hide((IDockBaseView)_fele);
			return true;
		});
	}

	internal bool IsShowed(IDockBaseView baseview)
	{
		if (baseview == null)
		{
			return false;
		}
		if (baseview.DockContainer == null)
		{
			return false;
		}
		if (baseview.DockContainer.ViewParent is AutoHideSideBar)
		{
			return autohidepopup.Visibility == Visibility.Visible && autohidepopup.ViewContent == baseview;
		}
		return true;
	}

	internal bool IsDocked(IDockBaseView baseview)
	{
		if (baseview == null)
		{
			return false;
		}
		if (baseview?.DockContainer?.ViewParent is AutoHideSideBar)
		{
			return false;
		}
		if (!IsShowed(baseview))
		{
			return false;
		}
		if (!(baseview is UIElement))
		{
			return false;
		}
		UIElement dependencyObject = (UIElement)baseview;
		return Window.GetWindow(dependencyObject) == WindowOwner;
	}

	internal bool IsFloated(IDockBaseView baseview)
	{
		if (baseview == null)
		{
			return false;
		}
		if (baseview?.DockContainer?.ViewParent is AutoHideSideBar)
		{
			return false;
		}
		if (!IsShowed(baseview))
		{
			return false;
		}
		if (!(baseview is UIElement))
		{
			return false;
		}
		UIElement uIElement = null;
		if (baseview.DockContainer is FloatWindow)
		{
			return true;
		}
		uIElement = ((!(baseview.DockContainer.ViewParent is UIElement)) ? ((UIElement)baseview) : ((UIElement)baseview.DockContainer.ViewParent));
		return Window.GetWindow(uIElement) != WindowOwner;
	}

	internal bool FindChildrenAttach(int rootdockid, int lastchildid, ref IDockBaseView target, ref ViewCommon.BaseViewRelations relation)
	{
		Queue<int> queue = new Queue<int>();
		DockBaseViewInfo dockBaseViewInfo = null;
		int num = 0;
		int num2 = 0;
		target = null;
		relation = ViewCommon.BaseViewRelations.Null;
		queue.Enqueue(rootdockid);
		while (num2++ < 256 && queue.Count() > 0 && queue.Count() < 256)
		{
			num = queue.Dequeue();
			if (num < 0 || num >= 256)
			{
				continue;
			}
			dockBaseViewInfo = infos[num];
			if (dockBaseViewInfo == null || dockBaseViewInfo.BaseView == null)
			{
				continue;
			}
			if (IsShowed(dockBaseViewInfo.BaseView))
			{
				switch (dockBaseViewInfo.Relation)
				{
				case ViewCommon.BaseViewRelations.Top:
					relation = ViewCommon.BaseViewRelations.Bottom;
					break;
				case ViewCommon.BaseViewRelations.Bottom:
					relation = ViewCommon.BaseViewRelations.Top;
					break;
				case ViewCommon.BaseViewRelations.Left:
					relation = ViewCommon.BaseViewRelations.Right;
					break;
				case ViewCommon.BaseViewRelations.Right:
					relation = ViewCommon.BaseViewRelations.Left;
					break;
				case ViewCommon.BaseViewRelations.Inside:
					relation = ViewCommon.BaseViewRelations.Inside;
					break;
				case ViewCommon.BaseViewRelations.BottomFromStackPanel:
					relation = ViewCommon.BaseViewRelations.TopFromStackPanel;
					break;
				case ViewCommon.BaseViewRelations.TopFromStackPanel:
					relation = ViewCommon.BaseViewRelations.BottomFromStackPanel;
					break;
				case ViewCommon.BaseViewRelations.LeftFromStackPanel:
					relation = ViewCommon.BaseViewRelations.RightFromStackPanel;
					break;
				case ViewCommon.BaseViewRelations.RightFromStackPanel:
					relation = ViewCommon.BaseViewRelations.LeftFromStackPanel;
					break;
				}
				if (relation != ViewCommon.BaseViewRelations.Null)
				{
					target = dockBaseViewInfo.BaseView;
					return true;
				}
			}
			foreach (int child in dockBaseViewInfo.Children)
			{
				if (num != rootdockid || child != lastchildid)
				{
					queue.Enqueue(child);
				}
			}
		}
		return false;
	}

	internal void ApplySize(IDockBaseView baseview)
	{
		if (baseview != null)
		{
			int dockID = baseview.DockContent.DockID;
			DockBaseViewInfo dockBaseViewInfo = infos[dockID];
			if (baseview is FrameworkElement)
			{
				FrameworkElement frameworkElement = (FrameworkElement)baseview;
				frameworkElement.Width = dockBaseViewInfo.Width;
				frameworkElement.Height = dockBaseViewInfo.Height;
			}
		}
	}

	internal void WriteSize(IDockBaseView baseview)
	{
		if (baseview == null || !(baseview is DockBaseView))
		{
			return;
		}
		int dockID = baseview.DockContent.DockID;
		DockBaseViewInfo dockBaseViewInfo = infos[dockID];
		if (!(baseview is FrameworkElement))
		{
			return;
		}
		FrameworkElement fele = (FrameworkElement)baseview;
		double width = fele.Width;
		double height = fele.Height;
		width = Math.Max(width, fele.MinWidth);
		height = Math.Max(height, fele.MinHeight);
		width = Math.Min(width, SystemParameters.PrimaryScreenWidth - 32.0);
		height = Math.Min(height, SystemParameters.PrimaryScreenHeight - 32.0);
		if (dockBaseViewInfo.Relation == ViewCommon.BaseViewRelations.Float)
		{
			IDockView dockView = ViewCommon.GetDockView(ref fele);
			double num = 0.0;
			double num2 = 0.0;
			while (!(dockView is FloatWindow) && dockView is FrameworkElement)
			{
				fele = (FrameworkElement)dockView;
				dockView = ViewCommon.GetDockView(ref fele);
			}
			if (dockView is FloatWindow)
			{
				FloatWindow floatWindow = (FloatWindow)dockView;
				num = floatWindow.Top;
				num2 = floatWindow.Left;
			}
			else
			{
				num = dockBaseViewInfo.Top;
				num2 = dockBaseViewInfo.Left;
			}
			num = Math.Max(num, 0.0);
			num2 = Math.Max(num2, 0.0);
			num = Math.Min(num, SystemParameters.PrimaryScreenHeight - 32.0);
			num2 = Math.Min(num2, SystemParameters.PrimaryScreenWidth - 32.0);
			dockBaseViewInfo.SetRect(num2, num, width, height);
		}
		else
		{
			dockBaseViewInfo.SetSize(width, height);
		}
	}

	internal void WriteAttach(IDockBaseView baseview, IDockBaseView target, ViewCommon.BaseViewRelations relation)
	{
		if (baseview == null)
		{
			return;
		}
		int dockID = baseview.DockContent.DockID;
		DockBaseViewInfo dockBaseViewInfo = infos[dockID];
		int num = ((target?.DockContent != null) ? target.DockContent.DockID : (-1));
		int targetID = dockBaseViewInfo.TargetID;
		DockBaseViewInfo dockBaseViewInfo2 = ((num >= 0 && num < 256) ? infos[num] : null);
		DockBaseViewInfo dockBaseViewInfo3 = ((targetID >= 0 && targetID < 256) ? infos[targetID] : null);
		Queue<int> queue = new Queue<int>();
		int num2 = 0;
		bool flag = false;
		if (ignoreattach)
		{
			ignoreattach = false;
			return;
		}
		if (dockBaseViewInfo2 == null)
		{
			switch (relation)
			{
			case ViewCommon.BaseViewRelations.Inside:
				relation = ViewCommon.BaseViewRelations.DockAsDocument;
				num = -1;
				break;
			case ViewCommon.BaseViewRelations.Top:
			case ViewCommon.BaseViewRelations.TopFromStackPanel:
				relation = ViewCommon.BaseViewRelations.TopSide;
				num = -1;
				break;
			case ViewCommon.BaseViewRelations.Bottom:
			case ViewCommon.BaseViewRelations.BottomFromStackPanel:
				relation = ViewCommon.BaseViewRelations.BottomSide;
				num = -1;
				break;
			case ViewCommon.BaseViewRelations.Left:
			case ViewCommon.BaseViewRelations.LeftFromStackPanel:
				relation = ViewCommon.BaseViewRelations.LeftSide;
				num = -1;
				break;
			case ViewCommon.BaseViewRelations.Right:
			case ViewCommon.BaseViewRelations.RightFromStackPanel:
				relation = ViewCommon.BaseViewRelations.RightSide;
				num = -1;
				break;
			}
		}
		else
		{
			queue.Enqueue(dockID);
			while (num2++ < 256 && queue.Count() > 0)
			{
				int num3 = queue.Dequeue();
				if (num == num3)
				{
					flag = true;
					break;
				}
				if (num3 < 0 || num3 >= 256)
				{
					continue;
				}
				DockBaseViewInfo dockBaseViewInfo4 = infos[num3];
				if (dockBaseViewInfo4 == null)
				{
					continue;
				}
				foreach (int child in dockBaseViewInfo4.Children)
				{
					queue.Enqueue(child);
				}
			}
		}
		if (flag)
		{
			if (dockBaseViewInfo.Children.Count() > 0)
			{
				int num4 = dockBaseViewInfo.Children.Last();
				DockBaseViewInfo dockBaseViewInfo5 = infos[num4];
				dockBaseViewInfo5.SetAttach(targetID, dockBaseViewInfo.Relation);
				if (dockBaseViewInfo3 != null)
				{
					dockBaseViewInfo3.Children.Remove(dockID);
					dockBaseViewInfo3.Children.Add(num4);
				}
				for (int i = 0; i < dockBaseViewInfo.Children.Count - 1; i++)
				{
					int num5 = dockBaseViewInfo.Children[i];
					DockBaseViewInfo dockBaseViewInfo6 = infos[num5];
					dockBaseViewInfo6.SetAttach(num4, dockBaseViewInfo6.Relation);
					dockBaseViewInfo5.Children.Add(num5);
				}
			}
			else
			{
				dockBaseViewInfo3?.Children.Remove(dockID);
			}
			dockBaseViewInfo.SetAttach(num, relation);
			if (num >= 0 && num < infos.Length)
			{
				infos[num].Children.Add(dockID);
			}
			dockBaseViewInfo.Children.Clear();
		}
		else
		{
			dockBaseViewInfo3?.Children.Remove(dockID);
			dockBaseViewInfo.SetAttach(num, relation);
			dockBaseViewInfo2?.Children.Add(dockID);
		}
	}

	internal void WriteAttach(IDockBaseView baseview, FrameworkElement felet, ViewCommon.BaseViewRelations relation)
	{
		if (baseview == null)
		{
			return;
		}
		if (felet is IDockBaseView)
		{
			WriteAttach(baseview, (IDockBaseView)felet, relation);
			return;
		}
		if (felet is DockStackPanel)
		{
			DockStackPanel dockStackPanel = (DockStackPanel)felet;
			{
				foreach (FrameworkElement viewChild in dockStackPanel.ViewChildren)
				{
					if (!(viewChild is IDockBaseView))
					{
						continue;
					}
					switch (relation)
					{
					case ViewCommon.BaseViewRelations.Left:
						WriteAttach(baseview, (IDockBaseView)viewChild, ViewCommon.BaseViewRelations.LeftFromStackPanel);
						break;
					case ViewCommon.BaseViewRelations.Right:
						WriteAttach(baseview, (IDockBaseView)viewChild, ViewCommon.BaseViewRelations.RightFromStackPanel);
						break;
					case ViewCommon.BaseViewRelations.Top:
						WriteAttach(baseview, (IDockBaseView)viewChild, ViewCommon.BaseViewRelations.TopFromStackPanel);
						break;
					case ViewCommon.BaseViewRelations.Bottom:
						WriteAttach(baseview, (IDockBaseView)viewChild, ViewCommon.BaseViewRelations.BottomFromStackPanel);
						break;
					}
					break;
				}
				return;
			}
		}
		if (felet != maintab.Tab)
		{
			DockStackPanel dockStackPanel2 = maintab.Group;
			if (dockStackPanel2 == null || dockStackPanel2.ViewChildren?.Contains(felet) != true)
			{
				if ((uint)(relation - 6) <= 3u || (uint)(relation - 11) <= 4u)
				{
					WriteAttach(baseview, (IDockBaseView)null, relation);
				}
				return;
			}
		}
		WriteAttach(baseview, (IDockBaseView)null, ViewCommon.BaseViewRelations.DockAsDocument);
	}

	internal void ShowContextMenu(IDockBaseView baseview)
	{
		if (baseview != null && baseview is DockBaseView)
		{
			DockBaseView dockBaseView = (DockBaseView)baseview;
			ShowContextMenu(baseview, HeaderDrawer.HeaderHeight, dockBaseView.ButtonStart);
		}
	}

	internal void ShowContextMenu(IDockBaseView baseview, double top, double left)
	{
		if (baseview != null)
		{
			ctxmenu.Core = baseview;
			if (baseview is DockBaseView)
			{
				DockBaseView placementTarget = (DockBaseView)baseview;
				ctxmenu.PlacementTarget = placementTarget;
				ctxmenu.Placement = PlacementMode.Relative;
				ctxmenu.VerticalOffset = top;
				ctxmenu.HorizontalOffset = left;
				ctxmenu.IsOpen = true;
			}
		}
	}

	internal void ShowAutoHide(AutoHideSideBar sidebar, IDockBaseView baseview)
	{
		if (baseview == null || sidebar == null)
		{
			return;
		}
		Hide(baseview);
		ApplySize(baseview);
		if (!sidebar.ViewChildren.Contains(baseview))
		{
			sidebar.AddChild(baseview);
			switch (sidebar.Side)
			{
			case AutoHideCommon.Sides.Top:
				WriteAttach(baseview, (IDockBaseView)null, ViewCommon.BaseViewRelations.TopAutoHide);
				break;
			case AutoHideCommon.Sides.Bottom:
				WriteAttach(baseview, (IDockBaseView)null, ViewCommon.BaseViewRelations.BottomAutoHide);
				break;
			case AutoHideCommon.Sides.Left:
				WriteAttach(baseview, (IDockBaseView)null, ViewCommon.BaseViewRelations.LeftAutoHide);
				break;
			case AutoHideCommon.Sides.Right:
				WriteAttach(baseview, (IDockBaseView)null, ViewCommon.BaseViewRelations.RightAutoHide);
				break;
			}
		}
		if (baseview is FrameworkElement)
		{
			FrameworkElement frameworkElement = (FrameworkElement)baseview;
			if (sidebar.IsHorizontalOrientation)
			{
				frameworkElement.Width = CD_Center.ActualWidth;
			}
			if (sidebar.IsVerticalOrientation)
			{
				frameworkElement.Height = RD_Center.ActualHeight;
			}
		}
		autohidepopup.Show(sidebar, baseview);
		if (baseview is DockBaseView)
		{
			this.IsShowedChanged(baseview.DockContent, new RoutedEventArgs());
		}
	}

	internal void HideAutoHide()
	{
		IDockBaseView viewContent = autohidepopup.ViewContent;
		autohidepopup.Hide();
		if (viewContent is DockBaseView)
		{
			this.IsShowedChanged(viewContent.DockContent, new RoutedEventArgs());
		}
	}

	internal FloatWindow ShowFloatWindow(IDockBaseView baseview, double left, double top)
	{
		if (baseview == null)
		{
			return null;
		}
		Hide(baseview);
		WriteAttach(baseview, (IDockBaseView)null, ViewCommon.BaseViewRelations.Float);
		ApplySize(baseview);
		FloatWindow floatWindow = new FloatWindow(this);
		floatWindow.Owner = WindowOwner;
		floatWindow.Left = left;
		floatWindow.Top = top;
		floatWindow.ViewContent = baseview;
		floatWindow.Show();
		fwnds.Add(floatWindow);
		return floatWindow;
	}

	internal void HideFloatWindow(FloatWindow fwnd)
	{
		if (fwnd != null)
		{
			fwnd.Owner = null;
			fwnd.ViewContent = null;
			fwnd.Hide();
			if (!fwnd.ToClose)
			{
				fwnd.Close();
			}
			fwnd.Dispose();
			fwnds.Remove(fwnd);
			if (maintab.SelectedItem is DockBaseView)
			{
				DockBaseView dockBaseView = (DockBaseView)maintab.SelectedItem;
				dockBaseView.MainGrid.Focus();
			}
		}
	}

	internal void ShowDock(IDockBaseView baseview)
	{
	}

	internal void ShowDockTo(IDockBaseView baseview, IDockBaseView target, ViewCommon.BaseViewRelations relation)
	{
		if (baseview is FrameworkElement)
		{
			FrameworkElement feleb = (FrameworkElement)baseview;
			FrameworkElement felet = ((target is FrameworkElement) ? ((FrameworkElement)target) : null);
			ShowDockTo(feleb, felet, relation);
		}
	}

	internal void ShowDockTo(FrameworkElement feleb, FrameworkElement felet, ViewCommon.BaseViewRelations relation)
	{
		FrameworkElement fele = felet;
		IDockView dockView = ViewCommon.GetDockView(ref fele);
		ISideStackPanel next = mainside;
		DockStackPanel dockStackPanel = null;
		DockTab dockTab = null;
		DockTabManager dockTabManager = null;
		FloatWindow floatWindow = null;
		FloatStackPanel floatStackPanel = null;
		FloatTab floatTab = null;
		bool flag = ignoreattach;
		if (feleb is FloatStackPanel)
		{
			floatStackPanel = (FloatStackPanel)feleb;
			ViewCommon.BaseViewRelations baseViewRelations = relation;
			ViewCommon.BaseViewRelations baseViewRelations2 = baseViewRelations;
			if (baseViewRelations2 == ViewCommon.BaseViewRelations.Inside || baseViewRelations2 == ViewCommon.BaseViewRelations.DockAsDocument)
			{
				ShowDockIn(floatStackPanel.ViewContent, felet, relation);
				return;
			}
			dockStackPanel = floatStackPanel.ViewContent;
			ViewCommon.HandleIn(dockStackPanel, delegate(FrameworkElement frameworkElement5)
			{
				if (!(frameworkElement5 is DockBaseView))
				{
					return false;
				}
				DockBaseView dockBaseView4 = (DockBaseView)frameworkElement5;
				int dockID = dockBaseView4.DockContent.DockID;
				if (dockID < 0 || dockID >= 256)
				{
					return false;
				}
				if (infos[dockID].Relation != ViewCommon.BaseViewRelations.Float)
				{
					return false;
				}
				WriteAttach(dockBaseView4, felet, relation);
				return true;
			});
			floatStackPanel.Dispose();
			floatWindow?.Close();
			ShowDockTo(dockStackPanel, felet, relation);
			return;
		}
		if (feleb is FloatTab)
		{
			floatTab = (FloatTab)feleb;
			ViewCommon.BaseViewRelations baseViewRelations3 = relation;
			ViewCommon.BaseViewRelations baseViewRelations4 = baseViewRelations3;
			if (baseViewRelations4 == ViewCommon.BaseViewRelations.Inside || baseViewRelations4 == ViewCommon.BaseViewRelations.DockAsDocument)
			{
				ShowDockIn(floatTab.ViewContent, felet, relation);
				return;
			}
			dockTab = floatTab.ViewContent;
			ViewCommon.HandleIn(dockTab, delegate(FrameworkElement frameworkElement5)
			{
				if (!(frameworkElement5 is DockBaseView))
				{
					return false;
				}
				DockBaseView dockBaseView4 = (DockBaseView)frameworkElement5;
				int dockID = dockBaseView4.DockContent.DockID;
				if (dockID < 0 || dockID >= 256)
				{
					return false;
				}
				if (infos[dockID].Relation != ViewCommon.BaseViewRelations.Float)
				{
					return false;
				}
				WriteAttach(dockBaseView4, felet, relation);
				return true;
			});
			floatTab.Dispose();
			floatWindow?.Close();
			ShowDockTo(dockTab, felet, relation);
			return;
		}
		if (feleb is IDockBaseView)
		{
			IDockBaseView baseview = (IDockBaseView)feleb;
			Hide(baseview);
			WriteAttach(baseview, felet, relation);
		}
		if (dockView is DockStackPanel)
		{
			dockStackPanel = (DockStackPanel)dockView;
		}
		if (dockView is DockTab)
		{
			dockTab = (DockTab)dockView;
		}
		if (dockView is DockTabManager)
		{
			dockTabManager = (DockTabManager)dockView;
		}
		if (dockView is FloatWindow)
		{
			floatWindow = (FloatWindow)dockView;
		}
		if (dockView is FloatStackPanel)
		{
			floatStackPanel = (FloatStackPanel)dockView;
		}
		if (dockView is FloatTab)
		{
			floatTab = (FloatTab)dockView;
		}
		switch (relation)
		{
		case ViewCommon.BaseViewRelations.Left:
		case ViewCommon.BaseViewRelations.Right:
			if (dockTab != null)
			{
				felet = dockTab;
				FrameworkElement fele3 = felet;
				dockView = ViewCommon.GetDockView(ref fele3);
				if (dockView is DockStackPanel)
				{
					dockStackPanel = (DockStackPanel)dockView;
				}
				if (dockView is FloatWindow)
				{
					floatWindow = (FloatWindow)dockView;
				}
				if (dockView is DockTab)
				{
					dockTab = (DockTab)dockView;
				}
				if (dockView is DockTabManager)
				{
					dockTabManager = (DockTabManager)dockView;
				}
				if (dockView is FloatTab)
				{
					floatTab = (FloatTab)dockView;
				}
			}
			if (dockStackPanel != null && dockStackPanel.Parent is DockTabManager)
			{
				dockTabManager = (DockTabManager)dockStackPanel.Parent;
				dockStackPanel = null;
			}
			if (dockTabManager != null)
			{
				dockTabManager.GroupStyle = ViewCommon.TabGroupStyles.Horizontal;
				int num3 = dockTabManager.Group.ViewChildren.IndexOf(felet);
				DockTab addtab2 = null;
				if (feleb is IDockBaseView)
				{
					IDockBaseView view2 = (IDockBaseView)feleb;
					if (num3 >= 0 && relation == ViewCommon.BaseViewRelations.Left)
					{
						dockTabManager.InsertChild(view2, num3);
					}
					else if (num3 >= 0 && relation == ViewCommon.BaseViewRelations.Right)
					{
						dockTabManager.InsertChild(view2, num3 + 1);
					}
					else
					{
						dockTabManager.InsertChild(view2, dockTabManager.Group.Count);
					}
				}
				else if (feleb is DockTab)
				{
					addtab2 = (DockTab)feleb;
				}
				else
				{
					addtab2 = new DockTab(this, dockTabManager, DockTab.ItemBarAlignments.Top);
					ViewCommon.HandleIn(feleb, delegate(FrameworkElement leaf)
					{
						if (!(leaf is IDockBaseView))
						{
							return false;
						}
						IDockBaseView baseview7 = (IDockBaseView)leaf;
						addtab2.AddChild(baseview7);
						return true;
					});
				}
				if (addtab2 != null)
				{
					if (num3 >= 0 && relation == ViewCommon.BaseViewRelations.Top)
					{
						dockTabManager.Group.InsertChild(num3, addtab2);
					}
					else if (num3 >= 0 && relation == ViewCommon.BaseViewRelations.Bottom)
					{
						dockTabManager.Group.InsertChild(num3 + 1, addtab2);
					}
					else
					{
						dockTabManager.Group.AddChild(addtab2);
					}
				}
			}
			else if (dockStackPanel != null)
			{
				if (dockStackPanel is DockStackVerticalPanel)
				{
					DockStackHorizontalPanel dockStackHorizontalPanel = new DockStackHorizontalPanel(this);
					dockStackHorizontalPanel.Height = felet.Height;
					dockStackPanel.ReplaceChild(felet, dockStackHorizontalPanel);
					if (relation == ViewCommon.BaseViewRelations.Left)
					{
						dockStackHorizontalPanel.AddChild(feleb);
						dockStackHorizontalPanel.AddChild(felet);
					}
					else
					{
						dockStackHorizontalPanel.AddChild(felet);
						dockStackHorizontalPanel.AddChild(feleb);
					}
				}
				else
				{
					if (!(dockStackPanel is DockStackHorizontalPanel))
					{
						break;
					}
					int num4 = dockStackPanel.ViewChildren.IndexOf(felet);
					if (num4 >= 0 && num4 < dockStackPanel.ViewChildren.Count)
					{
						if (relation == ViewCommon.BaseViewRelations.Left)
						{
							dockStackPanel.InsertChild(num4, feleb);
						}
						else
						{
							dockStackPanel.InsertChild(num4 + 1, feleb);
						}
					}
				}
			}
			else if (floatWindow != null)
			{
				DockStackHorizontalPanel dockStackHorizontalPanel2 = new DockStackHorizontalPanel(this);
				floatStackPanel = new FloatStackPanel(floatWindow, dockStackHorizontalPanel2)
				{
					Width = felet.Width,
					Height = felet.Height
				};
				floatWindow.ViewContent = null;
				if (relation == ViewCommon.BaseViewRelations.Left)
				{
					dockStackHorizontalPanel2.AddChild(feleb);
					dockStackHorizontalPanel2.AddChild(felet);
				}
				else
				{
					dockStackHorizontalPanel2.AddChild(felet);
					dockStackHorizontalPanel2.AddChild(feleb);
				}
				floatWindow.ViewContent = floatStackPanel;
			}
			else if (floatTab != null)
			{
				floatWindow = floatTab.ViewParent;
				floatWindow.ViewContent = null;
				floatTab.Dispose();
				DockStackHorizontalPanel dockStackHorizontalPanel3 = new DockStackHorizontalPanel(this);
				floatStackPanel = new FloatStackPanel(floatWindow, dockStackHorizontalPanel3)
				{
					Width = felet.Width,
					Height = felet.Height
				};
				if (relation == ViewCommon.BaseViewRelations.Left)
				{
					dockStackHorizontalPanel3.AddChild(feleb);
					dockStackHorizontalPanel3.AddChild(felet);
				}
				else
				{
					dockStackHorizontalPanel3.AddChild(felet);
					dockStackHorizontalPanel3.AddChild(feleb);
				}
				floatWindow.ViewContent = floatStackPanel;
			}
			else if (floatStackPanel != null)
			{
				floatWindow = floatStackPanel.ViewParent;
				floatWindow.ViewContent = null;
				floatStackPanel.Dispose();
				DockStackHorizontalPanel dockStackHorizontalPanel4 = new DockStackHorizontalPanel(this);
				floatStackPanel = new FloatStackPanel(floatWindow, dockStackHorizontalPanel4)
				{
					Width = felet.Width,
					Height = felet.Height
				};
				if (relation == ViewCommon.BaseViewRelations.Left)
				{
					dockStackHorizontalPanel4.AddChild(feleb);
					dockStackHorizontalPanel4.AddChild(felet);
				}
				else
				{
					dockStackHorizontalPanel4.AddChild(felet);
					dockStackHorizontalPanel4.AddChild(feleb);
				}
				floatWindow.ViewContent = floatStackPanel;
			}
			break;
		case ViewCommon.BaseViewRelations.Top:
		case ViewCommon.BaseViewRelations.Bottom:
			if (dockTab != null)
			{
				felet = dockTab;
				FrameworkElement fele2 = felet;
				dockView = ViewCommon.GetDockView(ref fele2);
				if (dockView is DockStackPanel)
				{
					dockStackPanel = (DockStackPanel)dockView;
				}
				if (dockView is FloatWindow)
				{
					floatWindow = (FloatWindow)dockView;
				}
				if (dockView is DockTab)
				{
					dockTab = (DockTab)dockView;
				}
				if (dockView is FloatTab)
				{
					floatTab = (FloatTab)dockView;
				}
			}
			if (dockStackPanel != null && dockStackPanel.Parent is DockTabManager)
			{
				dockTabManager = (DockTabManager)dockStackPanel.Parent;
				dockStackPanel = null;
			}
			if (dockTabManager != null)
			{
				dockTabManager.GroupStyle = ViewCommon.TabGroupStyles.Vertical;
				int num = dockTabManager.Group.ViewChildren.IndexOf(felet);
				DockTab addtab = null;
				if (feleb is IDockBaseView)
				{
					IDockBaseView view = (IDockBaseView)feleb;
					if (num >= 0 && relation == ViewCommon.BaseViewRelations.Top)
					{
						dockTabManager.InsertChild(view, num);
					}
					else if (num >= 0 && relation == ViewCommon.BaseViewRelations.Bottom)
					{
						dockTabManager.InsertChild(view, num + 1);
					}
					else
					{
						dockTabManager.InsertChild(view, dockTabManager.Group.Count);
					}
				}
				else if (feleb is DockTab)
				{
					addtab = (DockTab)feleb;
				}
				else
				{
					addtab = new DockTab(this, dockTabManager, DockTab.ItemBarAlignments.Top);
					ViewCommon.HandleIn(feleb, delegate(FrameworkElement leaf)
					{
						if (!(leaf is IDockBaseView))
						{
							return false;
						}
						IDockBaseView baseview7 = (IDockBaseView)leaf;
						addtab.AddChild(baseview7);
						return true;
					});
				}
				if (addtab != null)
				{
					if (num >= 0 && relation == ViewCommon.BaseViewRelations.Top)
					{
						dockTabManager.Group.InsertChild(num, addtab);
					}
					else if (num >= 0 && relation == ViewCommon.BaseViewRelations.Bottom)
					{
						dockTabManager.Group.InsertChild(num + 1, addtab);
					}
					else
					{
						dockTabManager.Group.AddChild(addtab);
					}
				}
			}
			else if (dockStackPanel != null)
			{
				if (dockStackPanel is DockStackHorizontalPanel)
				{
					DockStackVerticalPanel dockStackVerticalPanel = new DockStackVerticalPanel(this);
					dockStackVerticalPanel.Width = felet.Width;
					dockStackPanel.ReplaceChild(felet, dockStackVerticalPanel);
					if (relation == ViewCommon.BaseViewRelations.Top)
					{
						dockStackVerticalPanel.AddChild(feleb);
						dockStackVerticalPanel.AddChild(felet);
					}
					else
					{
						dockStackVerticalPanel.AddChild(felet);
						dockStackVerticalPanel.AddChild(feleb);
					}
				}
				else
				{
					if (!(dockStackPanel is DockStackVerticalPanel))
					{
						break;
					}
					int num2 = dockStackPanel.ViewChildren.IndexOf(felet);
					if (num2 >= 0 && num2 < dockStackPanel.ViewChildren.Count)
					{
						if (relation == ViewCommon.BaseViewRelations.Top)
						{
							dockStackPanel.InsertChild(num2, feleb);
						}
						else
						{
							dockStackPanel.InsertChild(num2 + 1, feleb);
						}
					}
				}
			}
			else if (floatWindow != null)
			{
				DockStackVerticalPanel dockStackVerticalPanel2 = new DockStackVerticalPanel(this);
				floatStackPanel = new FloatStackPanel(floatWindow, dockStackVerticalPanel2)
				{
					Width = felet.Width,
					Height = felet.Height
				};
				floatWindow.ViewContent = null;
				if (relation == ViewCommon.BaseViewRelations.Top)
				{
					dockStackVerticalPanel2.AddChild(feleb);
					dockStackVerticalPanel2.AddChild(felet);
				}
				else
				{
					dockStackVerticalPanel2.AddChild(felet);
					dockStackVerticalPanel2.AddChild(feleb);
				}
				floatWindow.ViewContent = floatStackPanel;
			}
			else if (floatTab != null)
			{
				floatWindow = floatTab.ViewParent;
				floatWindow.ViewContent = null;
				floatTab.Dispose();
				DockStackVerticalPanel dockStackVerticalPanel3 = new DockStackVerticalPanel(this);
				floatStackPanel = new FloatStackPanel(floatWindow, dockStackVerticalPanel3)
				{
					Width = felet.Width,
					Height = felet.Height
				};
				if (relation == ViewCommon.BaseViewRelations.Top)
				{
					dockStackVerticalPanel3.AddChild(feleb);
					dockStackVerticalPanel3.AddChild(felet);
				}
				else
				{
					dockStackVerticalPanel3.AddChild(felet);
					dockStackVerticalPanel3.AddChild(feleb);
				}
				floatWindow.ViewContent = floatStackPanel;
			}
			else if (floatStackPanel != null)
			{
				floatWindow = floatStackPanel.ViewParent;
				floatWindow.ViewContent = null;
				floatStackPanel.Dispose();
				DockStackVerticalPanel dockStackVerticalPanel4 = new DockStackVerticalPanel(this);
				floatStackPanel = new FloatStackPanel(floatWindow, dockStackVerticalPanel4)
				{
					Width = felet.Width,
					Height = felet.Height
				};
				if (relation == ViewCommon.BaseViewRelations.Top)
				{
					dockStackVerticalPanel4.AddChild(feleb);
					dockStackVerticalPanel4.AddChild(felet);
				}
				else
				{
					dockStackVerticalPanel4.AddChild(felet);
					dockStackVerticalPanel4.AddChild(feleb);
				}
				floatWindow.ViewContent = floatStackPanel;
			}
			break;
		case ViewCommon.BaseViewRelations.LeftSide:
		{
			FrameworkElement frameworkElement = null;
			SideStackHorizontalPanel sideStackHorizontalPanel = null;
			while (next != null && next.Side != AutoHideCommon.Sides.Left)
			{
				next = next.Next;
			}
			if (next == null)
			{
				frameworkElement = ((!(mainside is FrameworkElement)) ? maintab : ((FrameworkElement)mainside));
				UC_Main.Content = null;
				sideStackHorizontalPanel = new SideStackHorizontalPanel(this, AutoHideCommon.Sides.Left)
				{
					Width = UC_Main.ActualWidth,
					Height = UC_Main.ActualHeight
				};
				sideStackHorizontalPanel.AddChild(frameworkElement);
				mainside = sideStackHorizontalPanel;
				UC_Main.Content = sideStackHorizontalPanel;
			}
			else if (next is SideStackHorizontalPanel)
			{
				sideStackHorizontalPanel = (SideStackHorizontalPanel)next;
			}
			sideStackHorizontalPanel?.InsertChild(0, feleb);
			break;
		}
		case ViewCommon.BaseViewRelations.RightSide:
		{
			FrameworkElement frameworkElement2 = null;
			SideStackHorizontalPanel sideStackHorizontalPanel2 = null;
			while (next != null && next.Side != AutoHideCommon.Sides.Right)
			{
				next = next.Next;
			}
			if (next == null)
			{
				frameworkElement2 = ((!(mainside is FrameworkElement)) ? maintab : ((FrameworkElement)mainside));
				UC_Main.Content = null;
				sideStackHorizontalPanel2 = new SideStackHorizontalPanel(this, AutoHideCommon.Sides.Right)
				{
					Width = UC_Main.ActualWidth,
					Height = UC_Main.ActualHeight
				};
				sideStackHorizontalPanel2.AddChild(frameworkElement2);
				mainside = sideStackHorizontalPanel2;
				UC_Main.Content = sideStackHorizontalPanel2;
			}
			else if (next is SideStackHorizontalPanel)
			{
				sideStackHorizontalPanel2 = (SideStackHorizontalPanel)next;
			}
			sideStackHorizontalPanel2?.AddChild(feleb);
			break;
		}
		case ViewCommon.BaseViewRelations.TopSide:
		{
			FrameworkElement frameworkElement4 = null;
			SideStackVerticalPanel sideStackVerticalPanel2 = null;
			while (next != null && next.Side != AutoHideCommon.Sides.Top)
			{
				next = next.Next;
			}
			if (next == null)
			{
				frameworkElement4 = ((!(mainside is FrameworkElement)) ? maintab : ((FrameworkElement)mainside));
				UC_Main.Content = null;
				sideStackVerticalPanel2 = new SideStackVerticalPanel(this, AutoHideCommon.Sides.Top)
				{
					Width = UC_Main.ActualWidth,
					Height = UC_Main.ActualHeight
				};
				sideStackVerticalPanel2.AddChild(frameworkElement4);
				mainside = sideStackVerticalPanel2;
				UC_Main.Content = sideStackVerticalPanel2;
			}
			else if (next is SideStackVerticalPanel)
			{
				sideStackVerticalPanel2 = (SideStackVerticalPanel)next;
			}
			sideStackVerticalPanel2?.InsertChild(0, feleb);
			break;
		}
		case ViewCommon.BaseViewRelations.BottomSide:
		{
			FrameworkElement frameworkElement3 = null;
			SideStackVerticalPanel sideStackVerticalPanel = null;
			while (next != null && next.Side != AutoHideCommon.Sides.Bottom)
			{
				next = next.Next;
			}
			if (next == null)
			{
				frameworkElement3 = ((!(mainside is FrameworkElement)) ? maintab : ((FrameworkElement)mainside));
				UC_Main.Content = null;
				sideStackVerticalPanel = new SideStackVerticalPanel(this, AutoHideCommon.Sides.Bottom)
				{
					Width = UC_Main.ActualWidth,
					Height = UC_Main.ActualHeight
				};
				sideStackVerticalPanel.AddChild(frameworkElement3);
				mainside = sideStackVerticalPanel;
				UC_Main.Content = sideStackVerticalPanel;
			}
			else if (next is SideStackVerticalPanel)
			{
				sideStackVerticalPanel = (SideStackVerticalPanel)next;
			}
			sideStackVerticalPanel?.AddChild(feleb);
			break;
		}
		case ViewCommon.BaseViewRelations.Inside:
			if (felet is DockTab)
			{
				dockTab = (DockTab)felet;
				if (feleb is IDockBaseView)
				{
					IDockBaseView baseview3 = (IDockBaseView)feleb;
					dockTab.InsertChild(baseview3, 0);
				}
			}
			else
			{
				if (!(feleb is IDockBaseView) || !(felet is IDockBaseView))
				{
					break;
				}
				IDockBaseView dockBaseView2 = (IDockBaseView)feleb;
				IDockBaseView dockBaseView3 = (IDockBaseView)felet;
				if (dockStackPanel != null || floatWindow != null)
				{
					DockTab dockTab2 = new DockTab(this, null, DockTab.ItemBarAlignments.Bottom, DockTab.Themes.Classic);
					dockTab2.Width = felet.Width;
					dockTab2.Height = felet.Height + DockTabBarDrawer.TabBarHeight;
					int index = dockStackPanel?.ViewChildren.IndexOf(felet) ?? (-1);
					dockStackPanel?.RemoveChild(felet);
					if (floatWindow != null)
					{
						floatWindow.ViewContent = null;
					}
					dockTab2.AddChild(dockBaseView3);
					dockTab2.AddChild(dockBaseView2);
					dockTab2.SelectedIndex = 1;
					dockStackPanel?.InsertChild(index, dockTab2);
					if (floatWindow != null)
					{
						floatWindow.ViewContent = new FloatTab(floatWindow, dockTab2)
						{
							Width = dockTab2.Width,
							Height = dockTab2.Height
						};
					}
				}
				else if (dockTab != null)
				{
					int num5 = dockTab.ViewChildren.ToList().IndexOf(dockBaseView3);
					dockTab.InsertChild(dockBaseView2, num5 + 1);
					dockTab.SelectedItem = dockBaseView2;
				}
			}
			break;
		case ViewCommon.BaseViewRelations.Float:
		{
			if (!(feleb is IDockBaseView))
			{
				break;
			}
			IDockBaseView dockBaseView = (IDockBaseView)feleb;
			double left = 0.0;
			double top = 0.0;
			if (feleb is DockBaseView)
			{
				DockBaseViewInfo dockBaseViewInfo = infos[dockBaseView.DockContent.DockID];
				if (dockBaseViewInfo != null)
				{
					left = dockBaseViewInfo.Left;
					top = dockBaseViewInfo.Top;
				}
			}
			ShowFloatWindow(dockBaseView, left, top);
			break;
		}
		case ViewCommon.BaseViewRelations.LeftAutoHide:
			if (feleb is IDockBaseView)
			{
				IDockBaseView baseview5 = (IDockBaseView)feleb;
				ShowAutoHide(sidebars[2], baseview5);
			}
			break;
		case ViewCommon.BaseViewRelations.RightAutoHide:
			if (feleb is IDockBaseView)
			{
				IDockBaseView baseview4 = (IDockBaseView)feleb;
				ShowAutoHide(sidebars[3], baseview4);
			}
			break;
		case ViewCommon.BaseViewRelations.TopAutoHide:
			if (feleb is IDockBaseView)
			{
				IDockBaseView baseview6 = (IDockBaseView)feleb;
				ShowAutoHide(sidebars[0], baseview6);
			}
			break;
		case ViewCommon.BaseViewRelations.BottomAutoHide:
			if (feleb is IDockBaseView)
			{
				IDockBaseView baseview2 = (IDockBaseView)feleb;
				ShowAutoHide(sidebars[1], baseview2);
			}
			break;
		case ViewCommon.BaseViewRelations.LeftFromStackPanel:
			if (dockStackPanel != null && !(dockStackPanel is SideStackHorizontalPanel) && !(dockStackPanel is SideStackVerticalPanel))
			{
				ShowDockTo(feleb, dockStackPanel, ViewCommon.BaseViewRelations.Left);
				break;
			}
			ignoreattach = flag && !ignoreattach;
			ShowDockTo(feleb, felet, ViewCommon.BaseViewRelations.Left);
			break;
		case ViewCommon.BaseViewRelations.RightFromStackPanel:
			if (dockStackPanel != null && !(dockStackPanel is SideStackHorizontalPanel) && !(dockStackPanel is SideStackVerticalPanel))
			{
				ShowDockTo(feleb, dockStackPanel, ViewCommon.BaseViewRelations.Right);
				break;
			}
			ignoreattach = flag && !ignoreattach;
			ShowDockTo(feleb, felet, ViewCommon.BaseViewRelations.Right);
			break;
		case ViewCommon.BaseViewRelations.TopFromStackPanel:
			if (dockStackPanel != null && !(dockStackPanel is SideStackHorizontalPanel) && !(dockStackPanel is SideStackVerticalPanel))
			{
				ShowDockTo(feleb, dockStackPanel, ViewCommon.BaseViewRelations.Top);
				break;
			}
			ignoreattach = flag && !ignoreattach;
			ShowDockTo(feleb, felet, ViewCommon.BaseViewRelations.Top);
			break;
		case ViewCommon.BaseViewRelations.BottomFromStackPanel:
			if (dockStackPanel != null && !(dockStackPanel is SideStackHorizontalPanel) && !(dockStackPanel is SideStackVerticalPanel))
			{
				ShowDockTo(feleb, dockStackPanel, ViewCommon.BaseViewRelations.Bottom);
				break;
			}
			ignoreattach = flag && !ignoreattach;
			ShowDockTo(feleb, felet, ViewCommon.BaseViewRelations.Bottom);
			break;
		case ViewCommon.BaseViewRelations.DockAsDocument:
			if (maintab != null)
			{
				if (feleb is IDockBaseView)
				{
					maintab.AddChild((IDockBaseView)feleb);
				}
				else
				{
					ShowDockIn(feleb, maintab, ViewCommon.BaseViewRelations.Inside);
				}
			}
			break;
		}
	}

	internal void ShowDockIn(FrameworkElement feleb, FrameworkElement felet, ViewCommon.BaseViewRelations relation)
	{
		ViewCommon.HandleIn(feleb, delegate(FrameworkElement fele)
		{
			ShowDockTo(fele, felet, relation);
			return true;
		});
	}

	internal void HideDock(IDockBaseView baseview, IDockCollection colle)
	{
	}

	internal void StartDrag(IDockBaseView _baseview)
	{
		if (_baseview != null && _baseview is DockBaseView)
		{
			DockBaseView baseview = (DockBaseView)_baseview;
			Point position = Mouse.GetPosition(WindowOwner);
			FloatWindow floatWindow = null;
			Hide(baseview);
			floatWindow = ((WindowOwner.WindowState != WindowState.Normal) ? ShowFloatWindow(baseview, position.X - 32.0, position.Y - 32.0) : ShowFloatWindow(baseview, position.X + WindowOwner.Left - 32.0, position.Y + WindowOwner.Top - 32.0));
			mousehelper.Start(floatWindow);
		}
	}

	public void Save()
	{
		XDocument xDocument = new XDocument();
		XElement xElement = new XElement("LayoutSetting");
		for (int i = 0; i < 256; i++)
		{
			if (infos[i] != null)
			{
				XElement xElement2 = new XElement("Window");
				infos[i].Save(xElement2);
				xElement.Add(xElement2);
			}
		}
		xDocument.Add(xElement);
		xDocument.Save(FilePath);
	}

	public void Load()
	{
		XDocument xDocument = XDocument.Load(FilePath);
		XElement xElement = xDocument.Element("LayoutSetting");
		for (int i = 0; i < 256; i++)
		{
			infos[i]?.Children?.Clear();
		}
		foreach (XElement item in xElement.Elements("Window"))
		{
			int num = int.Parse(item.Attribute("DockID").Value);
			if (num < 0 || num >= 256)
			{
				continue;
			}
			if (infos[num] == null)
			{
				infos[num] = new DockBaseViewInfo(this, num);
			}
			infos[num].Load(item);
			int targetID = infos[num].TargetID;
			if (targetID >= 0 && targetID < 256)
			{
				if (infos[targetID] == null)
				{
					infos[targetID] = new DockBaseViewInfo(this, targetID);
				}
				infos[targetID].Children.Add(num);
			}
		}
	}

	internal void InvokeInShowedChanged(IDockBaseView baseview)
	{
		this.IsShowedChanged(baseview.DockContent, new RoutedEventArgs());
	}

	internal void InvokeChildrenChanged(IDockCollection dockcolle)
	{
		if (dockcolle != null && dockcolle is AutoHideSideBar)
		{
			AutoHideSideBar autoHideSideBar = (AutoHideSideBar)dockcolle;
			switch (autoHideSideBar.Side)
			{
			case AutoHideCommon.Sides.Top:
			{
				RowDefinition rD_Top = RD_Top;
				double minWidth = (RD_Top.MaxHeight = autoHideSideBar.ViewHeight);
				rD_Top.MinHeight = minWidth;
				break;
			}
			case AutoHideCommon.Sides.Bottom:
			{
				RowDefinition rD_Bottom = RD_Bottom;
				double minWidth = (RD_Bottom.MaxHeight = autoHideSideBar.ViewHeight);
				rD_Bottom.MinHeight = minWidth;
				break;
			}
			case AutoHideCommon.Sides.Left:
			{
				ColumnDefinition cD_Left = CD_Left;
				double minWidth = (CD_Left.MaxWidth = autoHideSideBar.ViewWidth);
				cD_Left.MinWidth = minWidth;
				break;
			}
			case AutoHideCommon.Sides.Right:
			{
				ColumnDefinition cD_Right = CD_Right;
				double minWidth = (CD_Right.MaxWidth = autoHideSideBar.ViewWidth);
				cD_Right.MinWidth = minWidth;
				break;
			}
			}
		}
	}

	internal void InvokeLeftButtonDown(IDockCollection dockcolle)
	{
		if (dockcolle == null || !(dockcolle is AutoHideSideBar))
		{
			return;
		}
		AutoHideSideBar autoHideSideBar = (AutoHideSideBar)dockcolle;
		if (autoHideSideBar.MouseOverItem != null)
		{
			if (autohidepopup.ViewContent != autoHideSideBar.MouseOverItem)
			{
				ShowAutoHide(autoHideSideBar, autoHideSideBar.MouseOverItem);
			}
			else
			{
				HideAutoHide();
			}
		}
	}

	internal void InvokeDragMove(System.Windows.Input.MouseEventArgs e)
	{
		Point point = default(Point);
		FrameworkElement drag = mousehelper.Drag;
		for (int i = 0; i < 256; i++)
		{
			if (!(infos[i]?.BaseView is FrameworkElement))
			{
				continue;
			}
			IDockBaseView baseView = infos[i].BaseView;
			if (baseView.DockContainer == mousehelper.DockFloat)
			{
				continue;
			}
			FrameworkElement frameworkElement = (FrameworkElement)baseView;
			if (!frameworkElement.IsVisible)
			{
				continue;
			}
			bool flag = true;
			if (baseView.DockContainer is DockTabContainer)
			{
				DockTabContainer dockTabContainer = (DockTabContainer)baseView.DockContainer;
				DockTab dockTab = (DockTab)dockTabContainer.ViewParent;
				flag &= baseView == dockTab.SelectedItem;
				flag &= baseView.Type == ViewCommon.BaseViewTypes.Anchor;
			}
			if (baseView.Type == ViewCommon.BaseViewTypes.Anchor)
			{
				flag &= !ViewCommon.HasType(drag, ViewCommon.BaseViewTypes.Document);
				flag &= !ViewCommon.HasType(drag, ViewCommon.BaseViewTypes.Toolbar);
			}
			else
			{
				flag = false;
			}
			if (!flag)
			{
				continue;
			}
			point = e.GetPosition(frameworkElement);
			if (point.X >= 0.0 && point.X <= frameworkElement.ActualWidth && point.Y >= 0.0 && point.Y <= frameworkElement.ActualHeight)
			{
				if (mousehelper.Target != frameworkElement)
				{
					mousehelper.EndDrop();
					mousehelper.StartDrop(frameworkElement);
				}
				return;
			}
		}
		if (maintab == null)
		{
			return;
		}
		point = e.GetPosition(maintab);
		if (!(point.X >= 0.0) || !(point.X <= maintab.ActualWidth) || !(point.Y >= 0.0) || !(point.Y <= maintab.ActualHeight))
		{
			return;
		}
		if (maintab.Tab != null)
		{
			if (mousehelper.Target != maintab.Tab)
			{
				mousehelper.EndDrop();
				mousehelper.StartDrop(maintab.Tab);
			}
		}
		else
		{
			if (maintab.Group == null)
			{
				return;
			}
			foreach (FrameworkElement viewChild in maintab.Group.ViewChildren)
			{
				if (viewChild is DockTab)
				{
					DockTab dockTab2 = (DockTab)viewChild;
					point = e.GetPosition(dockTab2);
					if (point.X >= 0.0 && point.X <= dockTab2.ActualWidth && point.Y >= 0.0 && point.Y <= dockTab2.ActualHeight && mousehelper.Target != dockTab2)
					{
						mousehelper.EndDrop();
						mousehelper.StartDrop(dockTab2);
					}
				}
			}
		}
	}

	internal void InvokeDrop()
	{
		FrameworkElement frameworkElement = mousehelper.Drag;
		FrameworkElement target = mousehelper.Target;
		FrameworkElement targetCollection = mousehelper.TargetCollection;
		DockDropSuit.Status state = mousehelper.DropSuit.State;
		int num = ((mousehelper.TabSuit.Tab != null) ? mousehelper.TabSuit.SelectedIndex : (-1));
		if (frameworkElement is FloatWindow && ((state != DockDropSuit.Status.None && state != DockDropSuit.Status.Null) || num >= 0))
		{
			FloatWindow floatWindow = (FloatWindow)frameworkElement;
			if (floatWindow.ViewContent is DockBaseView)
			{
				frameworkElement = (DockBaseView)floatWindow.ViewContent;
			}
			else if (floatWindow.ViewContent is FloatStackPanel)
			{
				frameworkElement = (FloatStackPanel)floatWindow.ViewContent;
			}
			else if (floatWindow.ViewContent is FloatTab)
			{
				frameworkElement = (FloatTab)floatWindow.ViewContent;
			}
			floatWindow.Close();
		}
		if (frameworkElement == null)
		{
			return;
		}
		switch (state)
		{
		case DockDropSuit.Status.CenterTop:
			ShowDockTo(frameworkElement, target, ViewCommon.BaseViewRelations.Top);
			return;
		case DockDropSuit.Status.CenterBottom:
			ShowDockTo(frameworkElement, target, ViewCommon.BaseViewRelations.Bottom);
			return;
		case DockDropSuit.Status.CenterLeft:
			ShowDockTo(frameworkElement, target, ViewCommon.BaseViewRelations.Left);
			return;
		case DockDropSuit.Status.CenterRight:
			ShowDockTo(frameworkElement, target, ViewCommon.BaseViewRelations.Right);
			return;
		case DockDropSuit.Status.Top:
			if (target == maintab.Tab)
			{
				ShowDockTo(frameworkElement, null, ViewCommon.BaseViewRelations.TopSide);
			}
			else
			{
				ShowDockTo(frameworkElement, targetCollection, ViewCommon.BaseViewRelations.Top);
			}
			return;
		case DockDropSuit.Status.Bottom:
			if (target == maintab.Tab)
			{
				ShowDockTo(frameworkElement, null, ViewCommon.BaseViewRelations.BottomSide);
			}
			else
			{
				ShowDockTo(frameworkElement, targetCollection, ViewCommon.BaseViewRelations.Bottom);
			}
			return;
		case DockDropSuit.Status.Left:
			if (target == maintab.Tab)
			{
				ShowDockTo(frameworkElement, null, ViewCommon.BaseViewRelations.LeftSide);
			}
			else
			{
				ShowDockTo(frameworkElement, targetCollection, ViewCommon.BaseViewRelations.Left);
			}
			return;
		case DockDropSuit.Status.Right:
			if (target == maintab.Tab)
			{
				ShowDockTo(frameworkElement, null, ViewCommon.BaseViewRelations.RightSide);
			}
			else
			{
				ShowDockTo(frameworkElement, targetCollection, ViewCommon.BaseViewRelations.Right);
			}
			return;
		case DockDropSuit.Status.Center:
			ShowDockTo(frameworkElement, target, ViewCommon.BaseViewRelations.Inside);
			return;
		}
		if (num >= 0)
		{
			DockTab tab = mousehelper.TabSuit.Tab;
			if (num > 0)
			{
				ShowDockTo(frameworkElement, (FrameworkElement)tab.ViewChildren.ToArray()[num - 1], ViewCommon.BaseViewRelations.Inside);
			}
			else
			{
				ShowDockTo(frameworkElement, tab, ViewCommon.BaseViewRelations.Inside);
			}
		}
	}

	public void CommandClose(IDockContent dockcontent)
	{
		if (dockcontent != null && ContainView(dockcontent))
		{
			int dockID = dockcontent.DockID;
			IDockBaseView baseView = infos[dockID].BaseView;
			CommandClose(baseView);
		}
	}

	public void CommandDock(IDockContent dockcontent)
	{
		if (dockcontent != null && ContainView(dockcontent))
		{
			int dockID = dockcontent.DockID;
			IDockBaseView baseView = infos[dockID].BaseView;
			CommandDock(baseView);
		}
	}

	public void CommandDockAsDocument(IDockContent dockcontent)
	{
		if (dockcontent != null && ContainView(dockcontent))
		{
			int dockID = dockcontent.DockID;
			IDockBaseView baseView = infos[dockID].BaseView;
			CommandDockAsDocument(baseView);
		}
	}

	public void CommandAutoHide(IDockContent dockcontent)
	{
		if (dockcontent != null && ContainView(dockcontent))
		{
			int dockID = dockcontent.DockID;
			IDockBaseView baseView = infos[dockID].BaseView;
			CommandAutoHide(baseView);
		}
	}

	internal void CommandClose(IDockBaseView baseview)
	{
		if (baseview != null)
		{
			if (baseview.HeaderType == ViewCommon.HeaderTypes.AutoHide)
			{
				Hide(baseview);
				RemoveAutoHide(baseview);
			}
			else
			{
				Hide(baseview);
			}
		}
	}

	internal void CommandDock(IDockBaseView baseview)
	{
		if (baseview == null)
		{
			return;
		}
		if (baseview.Type == ViewCommon.BaseViewTypes.Document)
		{
			if (baseview is FrameworkElement && maintab != null)
			{
				ShowDockTo((FrameworkElement)baseview, maintab, ViewCommon.BaseViewRelations.Inside);
			}
		}
		else if (baseview.HeaderType == ViewCommon.HeaderTypes.AutoHide && baseview.DockContainer is DockCollectionContainer)
		{
			DockCollectionContainer dockCollectionContainer = (DockCollectionContainer)baseview.DockContainer;
			if (dockCollectionContainer.ViewParent is AutoHideSideBar)
			{
				AutoHideSideBar autoHideSideBar = (AutoHideSideBar)dockCollectionContainer.ViewParent;
				switch (autoHideSideBar.Side)
				{
				case AutoHideCommon.Sides.Top:
					ShowDockTo(baseview, null, ViewCommon.BaseViewRelations.TopSide);
					break;
				case AutoHideCommon.Sides.Bottom:
					ShowDockTo(baseview, null, ViewCommon.BaseViewRelations.BottomSide);
					break;
				case AutoHideCommon.Sides.Right:
					ShowDockTo(baseview, null, ViewCommon.BaseViewRelations.RightSide);
					break;
				default:
					ShowDockTo(baseview, null, ViewCommon.BaseViewRelations.LeftSide);
					break;
				}
			}
			else
			{
				ShowDockTo(baseview, null, ((DockBaseViewInfo)GetInfo(baseview.DockContent))?.LastSide ?? ViewCommon.BaseViewRelations.LeftSide);
			}
		}
		else
		{
			ShowDockTo(baseview, null, ((DockBaseViewInfo)GetInfo(baseview.DockContent))?.LastSide ?? ViewCommon.BaseViewRelations.LeftSide);
		}
	}

	internal void CommandDockAsDocument(IDockBaseView baseview)
	{
		if (baseview != null)
		{
			ShowDockTo(baseview, null, ViewCommon.BaseViewRelations.DockAsDocument);
		}
	}

	internal void CommandAutoHide(IDockBaseView baseview)
	{
		if (baseview == null)
		{
			return;
		}
		if (baseview.DockContainer is DockTabContainer)
		{
			DockTabContainer dockTabContainer = (DockTabContainer)baseview.DockContainer;
			DockTab dockTab = (DockTab)dockTabContainer.ViewParent;
			IDockBaseView[] array = dockTab.ViewChildren.ToArray();
			foreach (IDockBaseView baseview2 in array)
			{
				_CommandAutoHide(baseview2);
			}
		}
		else
		{
			_CommandAutoHide(baseview);
		}
	}

	private void _CommandAutoHide(IDockBaseView baseview)
	{
		if (baseview != null && baseview is FrameworkElement)
		{
			FrameworkElement fele = (FrameworkElement)baseview;
			switch (ViewCommon.GetSideStackPanel(ref fele)?.Side)
			{
			case AutoHideCommon.Sides.Top:
				AddAutoHide(baseview, AutoHideCommon.Sides.Top);
				break;
			case AutoHideCommon.Sides.Bottom:
				AddAutoHide(baseview, AutoHideCommon.Sides.Bottom);
				break;
			case AutoHideCommon.Sides.Left:
				AddAutoHide(baseview, AutoHideCommon.Sides.Left);
				break;
			case AutoHideCommon.Sides.Right:
				AddAutoHide(baseview, AutoHideCommon.Sides.Right);
				break;
			default:
				AddAutoHide(baseview, AutoHideCommon.Sides.Left);
				break;
			}
		}
	}

	private void UC_Main_SizeChanged(object sender, SizeChangedEventArgs e)
	{
		if (UC_Main.Content is FrameworkElement)
		{
			FrameworkElement frameworkElement = (FrameworkElement)UC_Main.Content;
			frameworkElement.Width = UC_Main.ActualWidth;
			frameworkElement.Height = UC_Main.ActualHeight;
		}
	}

	public IList<IDockContent> MainTabToList()
	{
		List<IDockContent> list = new List<IDockContent>();
		foreach (IDockBaseView viewChild in MainTab.ViewChildren)
		{
			list.Add(viewChild.DockContent);
		}
		return list;
	}

	public bool IsInsideMainTab(IDockContent dc)
	{
		if (dc == null)
		{
			return false;
		}
		if (dc.DockID < 0 || dc.DockID >= infos.Count())
		{
			return false;
		}
		DockBaseViewInfo dockBaseViewInfo = infos[dc.DockID];
		return dockBaseViewInfo.BaseView.DockContainer == MainTab;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/SamSoarII.Dock;component/dockmanager.xaml", UriKind.Relative);
			System.Windows.Application.LoadComponent(this, resourceLocator);
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
			GD_Main = (Grid)target;
			break;
		case 2:
			RD_Top = (RowDefinition)target;
			break;
		case 3:
			RD_Center = (RowDefinition)target;
			break;
		case 4:
			RD_Bottom = (RowDefinition)target;
			break;
		case 5:
			CD_Left = (ColumnDefinition)target;
			break;
		case 6:
			CD_Center = (ColumnDefinition)target;
			break;
		case 7:
			CD_Right = (ColumnDefinition)target;
			break;
		case 8:
			UC_Main = (System.Windows.Controls.UserControl)target;
			UC_Main.SizeChanged += UC_Main_SizeChanged;
			break;
		case 9:
			DP_AutoHide = (DockPanel)target;
			break;
		case 10:
			CV_Resize = (Canvas)target;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}
