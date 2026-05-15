using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using SamSoarII.Dock.Interface;
using SamSoarII.Dock.View;

namespace SamSoarII.Dock.AutoHide;

internal class AutoHideSideBar : Panel, IDockCollection, IDockView
{
	private AutoHideCommon.Sides side;

	private DockManager parent;

	private List<IDockBaseView> viewchildren;

	private AutoHideDrawer drawer;

	private bool ischildchanging = false;

	private int mouseoverindex;

	public AutoHideCommon.Sides Side => side;

	internal bool IsButtonReverseOrder => side == AutoHideCommon.Sides.Left || side == AutoHideCommon.Sides.Bottom;

	internal bool IsHorizontalOrientation => side == AutoHideCommon.Sides.Top || side == AutoHideCommon.Sides.Bottom;

	internal bool IsVerticalOrientation => side == AutoHideCommon.Sides.Left || side == AutoHideCommon.Sides.Right;

	internal double ViewWidth => (IsVerticalOrientation && ViewChildren.Count > 0) ? AutoHideDrawer.AllThickness : 0.0;

	internal double ViewHeight => (IsHorizontalOrientation && ViewChildren.Count > 0) ? AutoHideDrawer.AllThickness : 0.0;

	public DockManager ViewParent => parent;

	IDockView IDockView.ViewParent => ViewParent;

	internal IList<IDockBaseView> ViewChildren => viewchildren;

	public int Count => viewchildren.Count();

	internal AutoHideDrawer Drawer => drawer;

	protected override int VisualChildrenCount => (drawer != null) ? 1 : 0;

	public bool IsChildChanging => ischildchanging;

	public int MouseOverIndex => mouseoverindex;

	public IDockBaseView MouseOverItem => (mouseoverindex >= 0 && mouseoverindex < viewchildren.Count()) ? viewchildren[mouseoverindex] : null;

	public AutoHideSideBar(DockManager _parent, AutoHideCommon.Sides _side)
	{
		base.Background = Brushes.Transparent;
		parent = _parent;
		side = _side;
		drawer = new AutoHideDrawer(this);
		viewchildren = new List<IDockBaseView>();
		AddLogicalChild(drawer);
		AddVisualChild(drawer);
		switch (side)
		{
		case AutoHideCommon.Sides.Top:
			Grid.SetRow(this, 0);
			Grid.SetColumn(this, 1);
			break;
		case AutoHideCommon.Sides.Bottom:
			Grid.SetRow(this, 2);
			Grid.SetColumn(this, 1);
			break;
		case AutoHideCommon.Sides.Left:
			Grid.SetRow(this, 1);
			Grid.SetColumn(this, 0);
			break;
		case AutoHideCommon.Sides.Right:
			Grid.SetRow(this, 1);
			Grid.SetColumn(this, 2);
			break;
		}
	}

	protected override Visual GetVisualChild(int index)
	{
		return drawer;
	}

	public void AddChild(IDockBaseView baseview)
	{
		ischildchanging = true;
		if (baseview.DockContainer is DockCollectionContainer && ((DockCollectionContainer)baseview.DockContainer).ViewParent == this)
		{
			ischildchanging = false;
			return;
		}
		baseview.DockContent.PropertyChanged += OnDockContentPropertyChanged;
		baseview.DockContainer = new DockCollectionContainer(this, baseview);
		viewchildren.Add(baseview);
		parent.InvokeChildrenChanged(this);
		mouseoverindex = -1;
		drawer?.Update();
		ischildchanging = false;
	}

	public void RemoveChild(IDockBaseView baseview)
	{
		ischildchanging = true;
		if (!(baseview.DockContainer is DockCollectionContainer))
		{
			ischildchanging = false;
			return;
		}
		DockCollectionContainer dockCollectionContainer = (DockCollectionContainer)baseview.DockContainer;
		if (dockCollectionContainer.ViewParent != this)
		{
			ischildchanging = false;
			return;
		}
		baseview.DockContent.PropertyChanged -= OnDockContentPropertyChanged;
		baseview.DockContainer = null;
		viewchildren.Remove(baseview);
		parent.InvokeChildrenChanged(this);
		mouseoverindex = -1;
		drawer?.Update();
		if (!dockCollectionContainer.IsViewContentEditing)
		{
			dockCollectionContainer.ViewContent = null;
		}
		dockCollectionContainer.Dispose();
		ischildchanging = false;
	}

	public void ResetChild()
	{
		ischildchanging = true;
		foreach (IDockBaseView viewchild in viewchildren)
		{
			viewchild.DockContent.PropertyChanged -= OnDockContentPropertyChanged;
		}
		viewchildren.Clear();
		parent.InvokeChildrenChanged(this);
		mouseoverindex = -1;
		drawer?.Update();
		ischildchanging = false;
	}

	protected override void OnMouseMove(MouseEventArgs e)
	{
		base.OnMouseMove(e);
		Point position = e.GetPosition(this);
		double value = (IsHorizontalOrientation ? position.X : position.Y);
		mouseoverindex = -1;
		for (int i = 0; i < drawer.Infos.Count; i++)
		{
			if (drawer.Infos[i].Inside(value))
			{
				mouseoverindex = i;
				break;
			}
		}
		drawer?.Update();
	}

	protected override void OnMouseLeave(MouseEventArgs e)
	{
		base.OnMouseLeave(e);
		mouseoverindex = -1;
		drawer?.Update();
	}

	protected override void OnMouseDown(MouseButtonEventArgs e)
	{
		base.OnMouseDown(e);
	}

	protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
	{
		base.OnMouseLeftButtonDown(e);
		parent.InvokeLeftButtonDown(this);
	}

	private void OnDockContentPropertyChanged(object sender, PropertyChangedEventArgs e)
	{
		drawer?.Update();
	}
}
