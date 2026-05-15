using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using SamSoarII.Dock.Float;
using SamSoarII.Dock.Global;
using SamSoarII.Dock.Interface;

namespace SamSoarII.Dock.View;

internal class HeaderContainer : Panel
{
	private IDockBaseView parent;

	private HeaderDrawer drawer;

	private int mouseoverindex;

	private Point leftstart = default(Point);

	private bool leftdown = false;

	public IDockBaseView ViewParent => parent;

	public DockManager DockManager => ViewParent?.DockManager;

	public MouseHelper MouseHelper => DockManager?.MouseHelper;

	internal HeaderDrawer Drawer => drawer;

	protected override int VisualChildrenCount => (drawer != null) ? 1 : 0;

	public int MouseOverIndex
	{
		get
		{
			return mouseoverindex;
		}
		protected set
		{
			if (mouseoverindex != value)
			{
				mouseoverindex = value;
				drawer.Update();
			}
		}
	}

	public HeaderContainer(IDockBaseView _parent)
	{
		base.Focusable = false;
		mouseoverindex = -1;
		parent = _parent;
		drawer = new HeaderDrawer(this);
		Panel.SetZIndex(this, 1);
		AddLogicalChild(drawer);
		AddVisualChild(drawer);
	}

	protected override Visual GetVisualChild(int index)
	{
		return drawer;
	}

	public void Update()
	{
		drawer?.Update();
	}

	protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
	{
		base.OnRenderSizeChanged(sizeInfo);
		drawer?.Update();
	}

	protected override void OnMouseMove(MouseEventArgs e)
	{
		base.OnMouseMove(e);
		Point position = e.GetPosition(this);
		if (leftdown)
		{
			if ((position - leftstart).Length > 8.0)
			{
				leftdown = false;
				ReleaseMouseCapture();
				DockManager.StartDrag(parent);
			}
		}
		else if (position.Y < 0.0 || position.Y > HeaderDrawer.HeaderHeight || position.X < drawer.ButtonStart || position.X > drawer.ButtonStart + HeaderDrawer.ButtAllWidth * 3.0)
		{
			MouseOverIndex = -1;
		}
		else
		{
			MouseOverIndex = (int)((position.X - drawer.ButtonStart) / HeaderDrawer.ButtAllWidth);
		}
	}

	protected override void OnMouseLeave(MouseEventArgs e)
	{
		base.OnMouseLeave(e);
		MouseOverIndex = -1;
	}

	protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
	{
		base.OnMouseLeftButtonDown(e);
		if (parent is DockBaseView)
		{
			DockBaseView dockBaseView = (DockBaseView)parent;
			dockBaseView.MainGrid?.Focus();
			Keyboard.Focus(dockBaseView.MainGrid);
		}
		if (MouseOverIndex >= 0 && MouseOverIndex < 3)
		{
			switch (ViewCommon.GetHeaderButtonType(parent.HeaderType, MouseOverIndex))
			{
			case ViewCommon.HeaderButtonTypes.Menu:
				DockManager.ShowContextMenu(parent);
				break;
			case ViewCommon.HeaderButtonTypes.Close:
				DockManager.CommandClose(parent);
				break;
			case ViewCommon.HeaderButtonTypes.Dock:
				DockManager.CommandDock(parent);
				break;
			case ViewCommon.HeaderButtonTypes.AutoHide:
				DockManager.CommandAutoHide(parent);
				break;
			case ViewCommon.HeaderButtonTypes.Maximize:
				if (ViewParent?.DockContainer is FloatWindow)
				{
					FloatWindow floatWindow2 = (FloatWindow)ViewParent.DockContainer;
					floatWindow2.WindowState = WindowState.Maximized;
				}
				break;
			case ViewCommon.HeaderButtonTypes.Restore:
				if (ViewParent?.DockContainer is FloatWindow)
				{
					FloatWindow floatWindow = (FloatWindow)ViewParent.DockContainer;
					floatWindow.WindowState = WindowState.Normal;
				}
				break;
			case ViewCommon.HeaderButtonTypes.Minimize:
				break;
			}
		}
		else if (ViewParent?.DockContainer is FloatWindow)
		{
			FloatWindow floatWindow3 = (FloatWindow)ViewParent.DockContainer;
			if (floatWindow3.WindowState == WindowState.Normal)
			{
				MouseHelper.Start(floatWindow3);
			}
		}
		else
		{
			leftdown = true;
			leftstart = e.GetPosition(this);
			CaptureMouse();
		}
	}

	protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
	{
		base.OnMouseLeftButtonUp(e);
		if (base.IsMouseCaptured)
		{
			leftdown = false;
			ReleaseMouseCapture();
		}
	}

	protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
	{
		base.OnMouseRightButtonDown(e);
		Point position = e.GetPosition(this);
		if (parent is DockBaseView)
		{
			DockManager?.ShowContextMenu(parent, position.Y, position.X);
		}
	}
}
