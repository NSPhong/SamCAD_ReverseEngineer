using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SamSoarII.Dock.Interface;
using SamSoarII.Dock.View;

namespace SamSoarII.Dock.Dock;

internal abstract class DockStackPanel : Canvas, IDisposable, IDockCollection, IDockView
{
	public static readonly double SeperatorInterval = 8.0;

	protected DockManager parent;

	protected List<FrameworkElement> viewchildren;

	private bool ischildchanging = false;

	public DockManager ViewParent => parent;

	IDockView IDockView.ViewParent => ViewParent;

	public IList<FrameworkElement> ViewChildren => viewchildren;

	public int Count => viewchildren.Count();

	public abstract Orientation Orientation { get; }

	public bool IsChildChanging => ischildchanging;

	public DockStackPanel(DockManager _parent)
	{
		parent = _parent;
		viewchildren = new List<FrameworkElement>();
		base.Width = (base.MinWidth = 40.0);
		base.Height = (base.MinHeight = 40.0);
	}

	public void Dispose()
	{
		parent = null;
	}

	public void AddChild(FrameworkElement view)
	{
		InsertChild(ViewChildren.Count, view);
	}

	public void InsertChild(int index, FrameworkElement view)
	{
		ischildchanging = false;
		if (view is IDockBaseView)
		{
			IDockBaseView dockBaseView = (IDockBaseView)view;
			if (dockBaseView.DockContainer is DockCollectionContainer && ((DockCollectionContainer)dockBaseView.DockContainer).ViewParent == this)
			{
				ischildchanging = false;
				return;
			}
			dockBaseView.DockContainer = new DockCollectionContainer(this, dockBaseView);
		}
		else if (viewchildren.Contains(view))
		{
			ischildchanging = false;
			return;
		}
		viewchildren.Insert(index, view);
		BaseUpdate(index);
		ischildchanging = true;
	}

	public void RemoveChild(FrameworkElement view)
	{
		ischildchanging = true;
		if (view is IDockBaseView)
		{
			IDockBaseView dockBaseView = (IDockBaseView)view;
			if (!(dockBaseView.DockContainer is DockCollectionContainer))
			{
				ischildchanging = false;
				return;
			}
			DockCollectionContainer dockCollectionContainer = (DockCollectionContainer)dockBaseView.DockContainer;
			if (dockCollectionContainer.ViewParent != this)
			{
				ischildchanging = false;
				return;
			}
			if (!dockCollectionContainer.IsViewContentEditing)
			{
				dockCollectionContainer.ViewContent = null;
			}
			dockCollectionContainer.Dispose();
		}
		else if (!viewchildren.Contains(view))
		{
			ischildchanging = false;
			return;
		}
		viewchildren.Remove(view);
		BaseUpdate(-1);
		ischildchanging = false;
	}

	public void ReplaceChild(FrameworkElement oldview, FrameworkElement newview)
	{
		if ((!(oldview is IDockBaseView) && !viewchildren.Contains(oldview)) || (!(newview is IDockBaseView) && viewchildren.Contains(newview)))
		{
			return;
		}
		ischildchanging = true;
		if (oldview is IDockBaseView)
		{
			IDockBaseView dockBaseView = (IDockBaseView)oldview;
			if (!(dockBaseView.DockContainer is DockCollectionContainer))
			{
				ischildchanging = false;
				return;
			}
			DockCollectionContainer dockCollectionContainer = (DockCollectionContainer)dockBaseView.DockContainer;
			if (dockCollectionContainer.ViewParent != this)
			{
				ischildchanging = false;
				return;
			}
			if (dockCollectionContainer.IsViewContentEditing)
			{
				dockCollectionContainer.ViewContent = null;
			}
			dockCollectionContainer.Dispose();
		}
		if (newview is IDockBaseView)
		{
			IDockBaseView dockBaseView2 = (IDockBaseView)newview;
			if (dockBaseView2.DockContainer is DockCollectionContainer && ((DockCollectionContainer)dockBaseView2.DockContainer).ViewParent == this)
			{
				ischildchanging = false;
				return;
			}
			dockBaseView2.DockContainer = new DockCollectionContainer(this, dockBaseView2);
		}
		int num = viewchildren.IndexOf(oldview);
		viewchildren[num] = newview;
		BaseUpdate(num);
		ischildchanging = false;
	}

	public void ResetChild()
	{
		ischildchanging = true;
		viewchildren.Clear();
		BaseUpdate(-1);
		ischildchanging = false;
	}

	void IDockCollection.AddChild(IDockBaseView baseview)
	{
		if (baseview is FrameworkElement)
		{
			AddChild((FrameworkElement)baseview);
		}
	}

	void IDockCollection.RemoveChild(IDockBaseView baseview)
	{
		if (baseview is FrameworkElement)
		{
			RemoveChild((FrameworkElement)baseview);
		}
	}

	void IDockCollection.ResetChild()
	{
		ResetChild();
	}

	public virtual void BaseUpdate(int lockid)
	{
		foreach (FrameworkElement child in base.Children)
		{
			if (child is DockStackSeperator)
			{
				((DockStackSeperator)child).Dispose();
			}
		}
		base.Children.Clear();
	}

	public virtual void SizeUpdate()
	{
	}

	protected virtual void WidthUpdate(double _width, int lockid)
	{
	}

	protected virtual void HeightUpdate(double _height, int lockid)
	{
	}

	protected virtual void InfoUpdate()
	{
	}

	protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
	{
		base.OnPropertyChanged(e);
		if (e.Property == FrameworkElement.WidthProperty)
		{
			WidthUpdate((double)e.NewValue, -1);
		}
		if (e.Property == FrameworkElement.HeightProperty)
		{
			HeightUpdate((double)e.NewValue, -1);
		}
	}
}
