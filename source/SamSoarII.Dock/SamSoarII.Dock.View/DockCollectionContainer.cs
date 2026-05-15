using System;
using SamSoarII.Dock.Interface;

namespace SamSoarII.Dock.View;

internal class DockCollectionContainer : IDockContainer, IDockView, IDisposable
{
	private bool isdisposed = false;

	private IDockCollection parent;

	private bool isviewcontentediting = false;

	private IDockBaseView viewcontent;

	public bool IsDisposed => isdisposed;

	public IDockCollection ViewParent => parent;

	IDockView IDockView.ViewParent => ViewParent;

	public bool IsViewContentEditing => isviewcontentediting;

	public virtual IDockBaseView ViewContent
	{
		get
		{
			return viewcontent;
		}
		set
		{
			if (!isdisposed && viewcontent != value)
			{
				isviewcontentediting = true;
				IDockBaseView dockBaseView = viewcontent;
				if (!parent.IsChildChanging)
				{
					parent.RemoveChild(dockBaseView);
				}
				viewcontent = null;
				if (dockBaseView != null && dockBaseView.DockContainer != null)
				{
					dockBaseView.DockContainer = null;
				}
				viewcontent = value;
				if (viewcontent != null && viewcontent.DockContainer != this)
				{
					viewcontent.DockContainer = this;
				}
				isviewcontentediting = false;
			}
		}
	}

	public DockCollectionContainer(IDockCollection _parent, IDockBaseView _viewcontent)
	{
		parent = _parent;
		ViewContent = _viewcontent;
	}

	public void Dispose()
	{
		if (!isdisposed)
		{
			isdisposed = true;
			parent = null;
		}
	}
}
