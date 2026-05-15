using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using SamSoarII.Dock.Dock;
using SamSoarII.Dock.Float;
using SamSoarII.Dock.Interface;
using SamSoarII.Dock.View;
using SamSoarII.Dock.View.Tab;

namespace SamSoarII.Dock.Global;

internal class MouseHelper : Window, IDisposable
{
	private DockManager parent;

	private IDockFloat dockfloat;

	private FrameworkElement target;

	private FrameworkElement colle;

	private DockDropSuit dropsuit;

	private DockTabSuit tabsuit;

	private FloatWindowResizer floatresizer;

	private Canvas maincanvas;

	private FrameworkElement placeout;

	private Point lastp = default(Point);

	private Point nextp = default(Point);

	private bool isending = false;

	private bool toresize = false;

	public FloatWindow resizetarget = null;

	public DockManager ViewParent => parent;

	public IDockFloat DockFloat => dockfloat;

	public FrameworkElement Drag => (dockfloat is FrameworkElement) ? ((FrameworkElement)dockfloat) : null;

	public FrameworkElement Target => target;

	public FrameworkElement TargetCollection => colle;

	public DockDropSuit DropSuit => dropsuit;

	public DockTabSuit TabSuit => tabsuit;

	public Thickness ResizeOffset
	{
		get
		{
			return floatresizer.Offset;
		}
		set
		{
			floatresizer.Offset = value;
		}
	}

	public double ResizeWidth => floatresizer.ContentWidth;

	public double ResizeHeight => floatresizer.ContentHeight;

	public MouseHelper(DockManager _parent)
	{
		parent = _parent;
		maincanvas = new Canvas();
		dropsuit = new DockDropSuit();
		tabsuit = new DockTabSuit();
		floatresizer = new FloatWindowResizer();
		placeout = dropsuit.PlaceOut;
		base.SourceInitialized += delegate
		{
			IntPtr handle = new WindowInteropHelper(this).Handle;
			HwndSource.FromHwnd(handle)?.AddHook(WinProc);
		};
		base.Owner = parent.WindowOwner;
		base.WindowStyle = WindowStyle.None;
		base.ResizeMode = ResizeMode.NoResize;
		base.ShowInTaskbar = false;
		base.AllowsTransparency = true;
		base.Background = Brushes.Transparent;
		base.WindowState = WindowState.Maximized;
		base.Content = maincanvas;
		maincanvas.Width = base.ActualWidth;
		maincanvas.Height = base.ActualHeight;
		maincanvas.MouseMove += OnMainCanvasMouseMove;
		maincanvas.MouseLeftButtonUp += OnMainCanvasMouseUp;
		maincanvas.Children.Add(dropsuit);
		maincanvas.Children.Add(tabsuit);
		maincanvas.Children.Add(placeout);
		maincanvas.Children.Add(floatresizer);
		dropsuit.Visibility = Visibility.Hidden;
		tabsuit.Visibility = Visibility.Hidden;
		placeout.Visibility = Visibility.Hidden;
		floatresizer.Visibility = Visibility.Hidden;
		base.Loaded += OnWindowFirstLoaded;
		base.IsVisibleChanged += OnIsVisibleChanged;
		Show();
	}

	public void Dispose()
	{
		Close();
		parent = null;
	}

	public void Start(IDockFloat _dockfloat)
	{
		if (dockfloat == null)
		{
			dockfloat = _dockfloat;
			if (dockfloat is UIElement)
			{
				UIElement uIElement = (UIElement)dockfloat;
				uIElement.IsHitTestVisible = false;
			}
			lastp = Mouse.GetPosition(this);
			Show();
			maincanvas.CaptureMouse();
		}
	}

	public void End()
	{
		if (!isending && dockfloat != null)
		{
			isending = true;
			parent.InvokeDrop();
			if (target != null)
			{
				EndDrop();
			}
			maincanvas.ReleaseMouseCapture();
			Hide();
			if (dockfloat is UIElement)
			{
				UIElement uIElement = (UIElement)dockfloat;
				uIElement.IsHitTestVisible = true;
			}
			dockfloat = null;
			isending = false;
		}
	}

	public void StartDrop(FrameworkElement _target)
	{
		if (target != null || Drag == null)
		{
			return;
		}
		target = _target;
		IDockCollection dockCollection = ViewCommon.GetDockCollection(ref _target);
		Point point = default(Point);
		dropsuit.IM_Top.Visibility = Visibility.Hidden;
		dropsuit.IM_Bottom.Visibility = Visibility.Hidden;
		dropsuit.IM_Left.Visibility = Visibility.Hidden;
		dropsuit.IM_Right.Visibility = Visibility.Hidden;
		dropsuit.IM_Center.Visibility = Visibility.Visible;
		dropsuit.IM_CTop.Visibility = Visibility.Visible;
		dropsuit.IM_CBottom.Visibility = Visibility.Visible;
		dropsuit.IM_CLeft.Visibility = Visibility.Visible;
		dropsuit.IM_CRight.Visibility = Visibility.Visible;
		placeout.Visibility = Visibility.Hidden;
		if (dockCollection is FrameworkElement)
		{
			if (dockCollection is DockStackPanel)
			{
				if (dockCollection is DockStackVerticalPanel)
				{
					dropsuit.IM_Top.Visibility = Visibility.Hidden;
					dropsuit.IM_Bottom.Visibility = Visibility.Hidden;
					dropsuit.IM_Left.Visibility = Visibility.Visible;
					dropsuit.IM_Right.Visibility = Visibility.Visible;
				}
				if (dockCollection is DockStackHorizontalPanel)
				{
					dropsuit.IM_Top.Visibility = Visibility.Visible;
					dropsuit.IM_Bottom.Visibility = Visibility.Visible;
					dropsuit.IM_Left.Visibility = Visibility.Hidden;
					dropsuit.IM_Right.Visibility = Visibility.Hidden;
				}
			}
			colle = (FrameworkElement)dockCollection;
		}
		if (target is DockTab)
		{
			DockTab dockTab = (DockTab)target;
			if (ViewCommon.FirstOrDefault(Drag, (IDockBaseView bv) => bv.Type == ViewCommon.BaseViewTypes.Document) != null)
			{
				tabsuit.Tab = dockTab;
			}
			if (dockTab.Manager == null)
			{
				dropsuit.IM_CLeft.Visibility = Visibility.Hidden;
				dropsuit.IM_CRight.Visibility = Visibility.Hidden;
				dropsuit.IM_CTop.Visibility = Visibility.Hidden;
				dropsuit.IM_CBottom.Visibility = Visibility.Hidden;
			}
			else if (dockTab.Manager.GroupStyle == ViewCommon.TabGroupStyles.Single)
			{
				dropsuit.IM_CLeft.Visibility = Visibility.Visible;
				dropsuit.IM_CRight.Visibility = Visibility.Visible;
				dropsuit.IM_CTop.Visibility = Visibility.Visible;
				dropsuit.IM_CBottom.Visibility = Visibility.Visible;
			}
			else if (dockTab.Manager.GroupStyle == ViewCommon.TabGroupStyles.Horizontal)
			{
				dropsuit.IM_CLeft.Visibility = Visibility.Visible;
				dropsuit.IM_CRight.Visibility = Visibility.Visible;
				dropsuit.IM_CTop.Visibility = Visibility.Hidden;
				dropsuit.IM_CBottom.Visibility = Visibility.Hidden;
			}
			else if (dockTab.Manager.GroupStyle == ViewCommon.TabGroupStyles.Vertical)
			{
				dropsuit.IM_CLeft.Visibility = Visibility.Hidden;
				dropsuit.IM_CRight.Visibility = Visibility.Hidden;
				dropsuit.IM_CTop.Visibility = Visibility.Visible;
				dropsuit.IM_CBottom.Visibility = Visibility.Visible;
			}
			if (Drag is FloatWindow)
			{
				FloatWindow floatWindow = (FloatWindow)Drag;
				if (floatWindow.ViewContent != null)
				{
					IDockBaseView viewContent = floatWindow.ViewContent;
					if (viewContent.Type != ViewCommon.BaseViewTypes.Document)
					{
						dropsuit.IM_Top.Visibility = Visibility.Visible;
						dropsuit.IM_Bottom.Visibility = Visibility.Visible;
						dropsuit.IM_Left.Visibility = Visibility.Visible;
						dropsuit.IM_Right.Visibility = Visibility.Visible;
					}
					else
					{
						dropsuit.IM_Top.Visibility = Visibility.Hidden;
						dropsuit.IM_Bottom.Visibility = Visibility.Hidden;
						dropsuit.IM_Left.Visibility = Visibility.Hidden;
						dropsuit.IM_Right.Visibility = Visibility.Hidden;
					}
				}
			}
			if (target == parent.MainTab.Tab)
			{
				colle = parent.MainTab.Tab;
			}
		}
		if (colle != null)
		{
			point = colle.TranslatePoint(new Point(0.0, 0.0), maincanvas);
			Canvas.SetLeft(placeout, point.X);
			Canvas.SetTop(placeout, point.Y);
			placeout.Width = colle.ActualWidth;
			placeout.Height = colle.ActualHeight;
			placeout.Visibility = Visibility.Visible;
		}
		if (target != null)
		{
			point = target.TranslatePoint(new Point(0.0, 0.0), maincanvas);
			Canvas.SetLeft(dropsuit, point.X);
			Canvas.SetTop(dropsuit, point.Y);
			dropsuit.Width = target.ActualWidth;
			dropsuit.Height = target.ActualHeight;
			dropsuit.Visibility = Visibility.Visible;
		}
	}

	public void EndDrop()
	{
		if (target != null)
		{
			dropsuit.Reset();
			dropsuit.Visibility = Visibility.Hidden;
			placeout.Visibility = Visibility.Hidden;
			tabsuit.Tab = null;
			target = null;
			colle = null;
		}
	}

	public void StartResize(FloatWindow _target)
	{
		toresize = true;
		resizetarget = _target;
		Show();
	}

	public void EndResize()
	{
		floatresizer.End();
		Hide();
	}

	private void OnWindowFirstLoaded(object sender, RoutedEventArgs e)
	{
		base.Loaded -= OnWindowFirstLoaded;
		lastp = Mouse.GetPosition(this);
		Hide();
	}

	protected override void OnDeactivated(EventArgs e)
	{
		base.OnDeactivated(e);
		End();
	}

	private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
	{
		if (!(bool)e.NewValue)
		{
			End();
		}
		else if (toresize)
		{
			Point start = resizetarget.TranslatePoint(new Point(0.0, 0.0), maincanvas);
			floatresizer.Start(resizetarget, start);
			toresize = false;
			resizetarget = null;
		}
	}

	protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
	{
		base.OnRenderSizeChanged(sizeInfo);
		maincanvas.Width = base.ActualWidth;
		maincanvas.Height = base.ActualHeight;
	}

	private void OnMainCanvasMouseMove(object sender, MouseEventArgs e)
	{
		if (dockfloat != null)
		{
			nextp = e.GetPosition(this);
			nextp.X = Math.Max(nextp.X, lastp.X - dockfloat.Left);
			nextp.X = Math.Min(nextp.X, base.ActualWidth - 32.0 + lastp.X - dockfloat.Left);
			nextp.Y = Math.Max(nextp.Y, lastp.Y - dockfloat.Top);
			nextp.Y = Math.Min(nextp.Y, base.ActualHeight - 20.0 + lastp.Y - dockfloat.Top);
			dockfloat.Left += nextp.X - lastp.X;
			dockfloat.Top += nextp.Y - lastp.Y;
			lastp = nextp;
		}
		if (target != null)
		{
			Point position = e.GetPosition(target);
			if (position.X < 0.0 || position.X > target.ActualWidth || position.Y < 0.0 || position.Y > target.ActualHeight)
			{
				EndDrop();
			}
			else
			{
				dropsuit.InvokeMouse(e);
			}
			if (tabsuit.Tab != null)
			{
				if (dropsuit.State == DockDropSuit.Status.None)
				{
					tabsuit.InvokeMouse(e);
				}
				else
				{
					tabsuit.SelectedIndex = -1;
				}
			}
		}
		parent.InvokeDragMove(e);
	}

	private void OnMainCanvasMouseUp(object sender, MouseButtonEventArgs e)
	{
		End();
	}

	private static IntPtr WinProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
	{
		if (msg == 36)
		{
			WmGetMinMaxInfo(hwnd, lParam);
			handled = true;
		}
		return (IntPtr)0;
	}

	private static void WmGetMinMaxInfo(IntPtr hwnd, IntPtr lParam)
	{
		Win32Helper.MINMAXINFO mINMAXINFO = (Win32Helper.MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(Win32Helper.MINMAXINFO));
		IntPtr intPtr = Win32Helper.MonitorFromWindow(hwnd, Win32Helper.MONITOR_DEFAULTTONEAREST);
		if (intPtr != IntPtr.Zero)
		{
			Win32Helper.MonitorInfo monitorInfo = new Win32Helper.MonitorInfo();
			Win32Helper.GetMonitorInfo(intPtr, monitorInfo);
			Win32Helper.RECT work = monitorInfo.Work;
			Win32Helper.RECT monitor = monitorInfo.Monitor;
			mINMAXINFO.ptMaxPosition.X = Math.Abs(work.Left - monitor.Left);
			mINMAXINFO.ptMaxPosition.Y = Math.Abs(work.Top - monitor.Top);
			mINMAXINFO.ptMaxSize.X = Math.Abs(work.Right - work.Left);
			mINMAXINFO.ptMaxSize.Y = Math.Abs(work.Bottom - work.Top);
		}
		Marshal.StructureToPtr((object)mINMAXINFO, lParam, true);
	}
}
