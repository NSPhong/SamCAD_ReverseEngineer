#define DEBUG
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;
using Standard;

namespace Microsoft.Windows.Shell;

internal class WindowChromeWorker : DependencyObject
{
	private delegate void _Action();

	private const Standard.SWP _SwpFlags = Standard.SWP.DRAWFRAME | Standard.SWP.NOACTIVATE | Standard.SWP.NOMOVE | Standard.SWP.NOOWNERZORDER | Standard.SWP.NOSIZE | Standard.SWP.NOZORDER;

	private readonly List<KeyValuePair<Standard.WM, Standard.MessageHandler>> _messageTable;

	private Window _window;

	private IntPtr _hwnd;

	private HwndSource _hwndSource = null;

	private bool _isHooked = false;

	private bool _isFixedUp = false;

	private bool _isUserResizing = false;

	private bool _hasUserMovedWindow = false;

	private Point _windowPosAtStartOfUserMove = default(Point);

	private int _blackGlassFixupAttemptCount;

	private WindowChrome _chromeInfo;

	private WindowState _lastRoundingState;

	private WindowState _lastMenuState;

	private bool _isGlassEnabled;

	public static readonly DependencyProperty WindowChromeWorkerProperty = DependencyProperty.RegisterAttached("WindowChromeWorker", typeof(WindowChromeWorker), typeof(WindowChromeWorker), new PropertyMetadata(null, _OnChromeWorkerChanged));

	private static readonly Standard.HT[,] _HitTestBorders = new Standard.HT[3, 3]
	{
		{
			Standard.HT.TOPLEFT,
			Standard.HT.TOP,
			Standard.HT.TOPRIGHT
		},
		{
			Standard.HT.LEFT,
			Standard.HT.CLIENT,
			Standard.HT.RIGHT
		},
		{
			Standard.HT.BOTTOMLEFT,
			Standard.HT.BOTTOM,
			Standard.HT.BOTTOMRIGHT
		}
	};

	private bool _IsWindowDocked
	{
		get
		{
			Standard.Assert.IsTrue(Standard.Utility.IsPresentationFrameworkVersionLessThan4);
			if (_window.WindowState != WindowState.Normal)
			{
				return false;
			}
			Standard.RECT rECT = _GetAdjustedWindowRect(new Standard.RECT
			{
				Bottom = 100,
				Right = 100
			});
			Point point = new Point(_window.Left, _window.Top);
			point -= (Vector)Standard.DpiHelper.DevicePixelsToLogical(new Point(rECT.Left, rECT.Top));
			return _window.RestoreBounds.Location != point;
		}
	}

	public WindowChromeWorker()
	{
		_messageTable = new List<KeyValuePair<Standard.WM, Standard.MessageHandler>>
		{
			new KeyValuePair<Standard.WM, Standard.MessageHandler>(Standard.WM.SETTEXT, _HandleSetTextOrIcon),
			new KeyValuePair<Standard.WM, Standard.MessageHandler>(Standard.WM.SETICON, _HandleSetTextOrIcon),
			new KeyValuePair<Standard.WM, Standard.MessageHandler>(Standard.WM.NCACTIVATE, _HandleNCActivate),
			new KeyValuePair<Standard.WM, Standard.MessageHandler>(Standard.WM.NCCALCSIZE, _HandleNCCalcSize),
			new KeyValuePair<Standard.WM, Standard.MessageHandler>(Standard.WM.NCHITTEST, _HandleNCHitTest),
			new KeyValuePair<Standard.WM, Standard.MessageHandler>(Standard.WM.NCRBUTTONUP, _HandleNCRButtonUp),
			new KeyValuePair<Standard.WM, Standard.MessageHandler>(Standard.WM.SIZE, _HandleSize),
			new KeyValuePair<Standard.WM, Standard.MessageHandler>(Standard.WM.WINDOWPOSCHANGED, _HandleWindowPosChanged),
			new KeyValuePair<Standard.WM, Standard.MessageHandler>(Standard.WM.DWMCOMPOSITIONCHANGED, _HandleDwmCompositionChanged)
		};
		if (Standard.Utility.IsPresentationFrameworkVersionLessThan4)
		{
			_messageTable.AddRange(new KeyValuePair<Standard.WM, Standard.MessageHandler>[4]
			{
				new KeyValuePair<Standard.WM, Standard.MessageHandler>(Standard.WM.WININICHANGE, _HandleSettingChange),
				new KeyValuePair<Standard.WM, Standard.MessageHandler>(Standard.WM.ENTERSIZEMOVE, _HandleEnterSizeMove),
				new KeyValuePair<Standard.WM, Standard.MessageHandler>(Standard.WM.EXITSIZEMOVE, _HandleExitSizeMove),
				new KeyValuePair<Standard.WM, Standard.MessageHandler>(Standard.WM.MOVE, _HandleMove)
			});
		}
	}

	public void SetWindowChrome(WindowChrome newChrome)
	{
		VerifyAccess();
		Standard.Assert.IsNotNull(_window);
		if (newChrome != _chromeInfo)
		{
			if (_chromeInfo != null)
			{
				_chromeInfo.PropertyChangedThatRequiresRepaint -= _OnChromePropertyChangedThatRequiresRepaint;
			}
			_chromeInfo = newChrome;
			if (_chromeInfo != null)
			{
				_chromeInfo.PropertyChangedThatRequiresRepaint += _OnChromePropertyChangedThatRequiresRepaint;
			}
			_ApplyNewCustomChrome();
		}
	}

	private void _OnChromePropertyChangedThatRequiresRepaint(object sender, EventArgs e)
	{
		_UpdateFrameState(force: true);
	}

	private static void _OnChromeWorkerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		Window window = (Window)d;
		WindowChromeWorker windowChromeWorker = (WindowChromeWorker)e.NewValue;
		Standard.Assert.IsNotNull(window);
		Standard.Assert.IsNotNull(windowChromeWorker);
		Standard.Assert.IsNull(windowChromeWorker._window);
		windowChromeWorker._SetWindow(window);
	}

	private void _SetWindow(Window window)
	{
		Standard.Assert.IsNull(_window);
		Standard.Assert.IsNotNull(window);
		_window = window;
		_hwnd = new WindowInteropHelper(_window).Handle;
		if (Standard.Utility.IsPresentationFrameworkVersionLessThan4)
		{
			Standard.Utility.AddDependencyPropertyChangeListener(_window, Control.TemplateProperty, _OnWindowPropertyChangedThatRequiresTemplateFixup);
			Standard.Utility.AddDependencyPropertyChangeListener(_window, FrameworkElement.FlowDirectionProperty, _OnWindowPropertyChangedThatRequiresTemplateFixup);
		}
		_window.Closed += _UnsetWindow;
		if (IntPtr.Zero != _hwnd)
		{
			_hwndSource = HwndSource.FromHwnd(_hwnd);
			Standard.Assert.IsNotNull(_hwndSource);
			_window.ApplyTemplate();
			if (_chromeInfo != null)
			{
				_ApplyNewCustomChrome();
			}
			return;
		}
		_window.SourceInitialized += delegate
		{
			_hwnd = new WindowInteropHelper(_window).Handle;
			Standard.Assert.IsNotDefault(_hwnd);
			_hwndSource = HwndSource.FromHwnd(_hwnd);
			Standard.Assert.IsNotNull(_hwndSource);
			if (_chromeInfo != null)
			{
				_ApplyNewCustomChrome();
			}
		};
	}

	private void _UnsetWindow(object sender, EventArgs e)
	{
		if (Standard.Utility.IsPresentationFrameworkVersionLessThan4)
		{
			Standard.Utility.RemoveDependencyPropertyChangeListener(_window, Control.TemplateProperty, _OnWindowPropertyChangedThatRequiresTemplateFixup);
			Standard.Utility.RemoveDependencyPropertyChangeListener(_window, FrameworkElement.FlowDirectionProperty, _OnWindowPropertyChangedThatRequiresTemplateFixup);
		}
		if (_chromeInfo != null)
		{
			_chromeInfo.PropertyChangedThatRequiresRepaint -= _OnChromePropertyChangedThatRequiresRepaint;
		}
		_RestoreStandardChromeState(isClosing: true);
	}

	public static WindowChromeWorker GetWindowChromeWorker(Window window)
	{
		Standard.Verify.IsNotNull(window, "window");
		return (WindowChromeWorker)window.GetValue(WindowChromeWorkerProperty);
	}

	public static void SetWindowChromeWorker(Window window, WindowChromeWorker chrome)
	{
		Standard.Verify.IsNotNull(window, "window");
		window.SetValue(WindowChromeWorkerProperty, chrome);
	}

	private void _OnWindowPropertyChangedThatRequiresTemplateFixup(object sender, EventArgs e)
	{
		Standard.Assert.IsTrue(Standard.Utility.IsPresentationFrameworkVersionLessThan4);
		if (_chromeInfo != null && _hwnd != IntPtr.Zero)
		{
			_window.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new _Action(_FixupFrameworkIssues));
		}
	}

	private void _ApplyNewCustomChrome()
	{
		if (_hwnd == IntPtr.Zero)
		{
			return;
		}
		if (_chromeInfo == null)
		{
			_RestoreStandardChromeState(isClosing: false);
			return;
		}
		if (!_isHooked)
		{
			_hwndSource.AddHook(_WndProc);
			_isHooked = true;
		}
		_FixupFrameworkIssues();
		_UpdateSystemMenu(_window.WindowState);
		_UpdateFrameState(force: true);
		Standard.NativeMethods.SetWindowPos(_hwnd, IntPtr.Zero, 0, 0, 0, 0, Standard.SWP.DRAWFRAME | Standard.SWP.NOACTIVATE | Standard.SWP.NOMOVE | Standard.SWP.NOOWNERZORDER | Standard.SWP.NOSIZE | Standard.SWP.NOZORDER);
	}

	private void _FixupFrameworkIssues()
	{
		Standard.Assert.IsNotNull(_chromeInfo);
		Standard.Assert.IsNotNull(_window);
		if (!Standard.Utility.IsPresentationFrameworkVersionLessThan4 || _window.Template == null)
		{
			return;
		}
		if (VisualTreeHelper.GetChildrenCount(_window) == 0)
		{
			_window.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new _Action(_FixupFrameworkIssues));
			return;
		}
		FrameworkElement frameworkElement = (FrameworkElement)VisualTreeHelper.GetChild(_window, 0);
		Standard.RECT windowRect = Standard.NativeMethods.GetWindowRect(_hwnd);
		Standard.RECT rECT = _GetAdjustedWindowRect(windowRect);
		Rect rect = Standard.DpiHelper.DeviceRectToLogical(new Rect(windowRect.Left, windowRect.Top, windowRect.Width, windowRect.Height));
		Rect rect2 = Standard.DpiHelper.DeviceRectToLogical(new Rect(rECT.Left, rECT.Top, rECT.Width, rECT.Height));
		Thickness thickness = new Thickness(rect.Left - rect2.Left, rect.Top - rect2.Top, rect2.Right - rect.Right, rect2.Bottom - rect.Bottom);
		frameworkElement.Margin = new Thickness(0.0, 0.0, 0.0 - (thickness.Left + thickness.Right), 0.0 - (thickness.Top + thickness.Bottom));
		if (_window.FlowDirection == FlowDirection.RightToLeft)
		{
			frameworkElement.RenderTransform = new MatrixTransform(1.0, 0.0, 0.0, 1.0, 0.0 - (thickness.Left + thickness.Right), 0.0);
		}
		else
		{
			frameworkElement.RenderTransform = null;
		}
		if (!_isFixedUp)
		{
			_hasUserMovedWindow = false;
			_window.StateChanged += _FixupRestoreBounds;
			_isFixedUp = true;
		}
	}

	private void _FixupWindows7Issues()
	{
		if (_blackGlassFixupAttemptCount <= 5 && Standard.Utility.IsOSWindows7OrNewer && Standard.NativeMethods.DwmIsCompositionEnabled())
		{
			_blackGlassFixupAttemptCount++;
			bool flag = false;
			try
			{
				flag = Standard.NativeMethods.DwmGetCompositionTimingInfo(_hwnd).HasValue;
			}
			catch (Exception)
			{
			}
			if (!flag)
			{
				base.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new _Action(_FixupWindows7Issues));
			}
			else
			{
				_blackGlassFixupAttemptCount = 0;
			}
		}
	}

	private void _FixupRestoreBounds(object sender, EventArgs e)
	{
		Standard.Assert.IsTrue(Standard.Utility.IsPresentationFrameworkVersionLessThan4);
		if ((_window.WindowState == WindowState.Maximized || _window.WindowState == WindowState.Minimized) && _hasUserMovedWindow)
		{
			_hasUserMovedWindow = false;
			Standard.WINDOWPLACEMENT windowPlacement = Standard.NativeMethods.GetWindowPlacement(_hwnd);
			Standard.RECT rECT = _GetAdjustedWindowRect(new Standard.RECT
			{
				Bottom = 100,
				Right = 100
			});
			Point point = Standard.DpiHelper.DevicePixelsToLogical(new Point(windowPlacement.rcNormalPosition.Left - rECT.Left, windowPlacement.rcNormalPosition.Top - rECT.Top));
			_window.Top = point.Y;
			_window.Left = point.X;
		}
	}

	private Standard.RECT _GetAdjustedWindowRect(Standard.RECT rcWindow)
	{
		Standard.Assert.IsTrue(Standard.Utility.IsPresentationFrameworkVersionLessThan4);
		Standard.WS dwStyle = (Standard.WS)(int)Standard.NativeMethods.GetWindowLongPtr(_hwnd, Standard.GWL.STYLE);
		Standard.WS_EX dwExStyle = (Standard.WS_EX)(int)Standard.NativeMethods.GetWindowLongPtr(_hwnd, Standard.GWL.EXSTYLE);
		return Standard.NativeMethods.AdjustWindowRectEx(rcWindow, dwStyle, bMenu: false, dwExStyle);
	}

	private IntPtr _WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
	{
		Standard.Assert.AreEqual(hwnd, _hwnd);
		foreach (KeyValuePair<Standard.WM, Standard.MessageHandler> item in _messageTable)
		{
			if (item.Key == (Standard.WM)msg)
			{
				return item.Value((Standard.WM)msg, wParam, lParam, out handled);
			}
		}
		return IntPtr.Zero;
	}

	private IntPtr _HandleSetTextOrIcon(Standard.WM uMsg, IntPtr wParam, IntPtr lParam, out bool handled)
	{
		bool flag = _ModifyStyle(Standard.WS.VISIBLE, Standard.WS.OVERLAPPED);
		IntPtr result = Standard.NativeMethods.DefWindowProc(_hwnd, uMsg, wParam, lParam);
		if (flag)
		{
			_ModifyStyle(Standard.WS.OVERLAPPED, Standard.WS.VISIBLE);
		}
		handled = true;
		return result;
	}

	private IntPtr _HandleNCActivate(Standard.WM uMsg, IntPtr wParam, IntPtr lParam, out bool handled)
	{
		IntPtr result = Standard.NativeMethods.DefWindowProc(_hwnd, Standard.WM.NCACTIVATE, wParam, new IntPtr(-1));
		handled = true;
		return result;
	}

	private IntPtr _HandleNCCalcSize(Standard.WM uMsg, IntPtr wParam, IntPtr lParam, out bool handled)
	{
		handled = true;
		return new IntPtr(768);
	}

	private IntPtr _HandleNCHitTest(Standard.WM uMsg, IntPtr wParam, IntPtr lParam, out bool handled)
	{
		IntPtr plResult = IntPtr.Zero;
		handled = false;
		if (Standard.Utility.IsOSVistaOrNewer && _chromeInfo.GlassFrameThickness != default(Thickness) && _isGlassEnabled)
		{
			handled = Standard.NativeMethods.DwmDefWindowProc(_hwnd, uMsg, wParam, lParam, out plResult);
		}
		if (IntPtr.Zero == plResult)
		{
			Point point = new Point(Standard.Utility.GET_X_LPARAM(lParam), Standard.Utility.GET_Y_LPARAM(lParam));
			Rect deviceRectangle = _GetWindowRect();
			Standard.HT hT = _HitTestNca(Standard.DpiHelper.DeviceRectToLogical(deviceRectangle), Standard.DpiHelper.DevicePixelsToLogical(point));
			if (hT != Standard.HT.CLIENT)
			{
				Point devicePoint = point;
				devicePoint.Offset(0.0 - deviceRectangle.X, 0.0 - deviceRectangle.Y);
				devicePoint = Standard.DpiHelper.DevicePixelsToLogical(devicePoint);
				IInputElement inputElement = _window.InputHitTest(devicePoint);
				if (inputElement != null && WindowChrome.GetIsHitTestVisibleInChrome(inputElement))
				{
					hT = Standard.HT.CLIENT;
				}
			}
			handled = true;
			plResult = new IntPtr((int)hT);
		}
		return plResult;
	}

	private IntPtr _HandleNCRButtonUp(Standard.WM uMsg, IntPtr wParam, IntPtr lParam, out bool handled)
	{
		if (2 == wParam.ToInt32())
		{
			if (_window.ContextMenu != null)
			{
				_window.ContextMenu.Placement = PlacementMode.MousePoint;
				_window.ContextMenu.IsOpen = true;
			}
			else if (WindowChrome.GetWindowChrome(_window).ShowSystemMenu)
			{
				SystemCommands.ShowSystemMenuPhysicalCoordinates(_window, new Point(Standard.Utility.GET_X_LPARAM(lParam), Standard.Utility.GET_Y_LPARAM(lParam)));
			}
		}
		handled = false;
		return IntPtr.Zero;
	}

	private IntPtr _HandleSize(Standard.WM uMsg, IntPtr wParam, IntPtr lParam, out bool handled)
	{
		WindowState? assumeState = null;
		if (wParam.ToInt32() == 2)
		{
			assumeState = WindowState.Maximized;
		}
		_UpdateSystemMenu(assumeState);
		handled = false;
		return IntPtr.Zero;
	}

	private IntPtr _HandleWindowPosChanged(Standard.WM uMsg, IntPtr wParam, IntPtr lParam, out bool handled)
	{
		_UpdateSystemMenu(null);
		if (!_isGlassEnabled)
		{
			Standard.Assert.IsNotDefault(lParam);
			Standard.WINDOWPOS value = (Standard.WINDOWPOS)Marshal.PtrToStructure(lParam, typeof(Standard.WINDOWPOS));
			_SetRoundingRegion(value);
		}
		handled = false;
		return IntPtr.Zero;
	}

	private IntPtr _HandleDwmCompositionChanged(Standard.WM uMsg, IntPtr wParam, IntPtr lParam, out bool handled)
	{
		_UpdateFrameState(force: false);
		handled = false;
		return IntPtr.Zero;
	}

	private IntPtr _HandleSettingChange(Standard.WM uMsg, IntPtr wParam, IntPtr lParam, out bool handled)
	{
		Standard.Assert.IsTrue(Standard.Utility.IsPresentationFrameworkVersionLessThan4);
		_FixupFrameworkIssues();
		handled = false;
		return IntPtr.Zero;
	}

	private IntPtr _HandleEnterSizeMove(Standard.WM uMsg, IntPtr wParam, IntPtr lParam, out bool handled)
	{
		Standard.Assert.IsTrue(Standard.Utility.IsPresentationFrameworkVersionLessThan4);
		_isUserResizing = true;
		Standard.Assert.Implies(_window.WindowState == WindowState.Maximized, Standard.Utility.IsOSWindows7OrNewer);
		if (_window.WindowState != WindowState.Maximized && !_IsWindowDocked)
		{
			_windowPosAtStartOfUserMove = new Point(_window.Left, _window.Top);
		}
		handled = false;
		return IntPtr.Zero;
	}

	private IntPtr _HandleExitSizeMove(Standard.WM uMsg, IntPtr wParam, IntPtr lParam, out bool handled)
	{
		Standard.Assert.IsTrue(Standard.Utility.IsPresentationFrameworkVersionLessThan4);
		_isUserResizing = false;
		if (_window.WindowState == WindowState.Maximized)
		{
			Standard.Assert.IsTrue(Standard.Utility.IsOSWindows7OrNewer);
			_window.Top = _windowPosAtStartOfUserMove.Y;
			_window.Left = _windowPosAtStartOfUserMove.X;
		}
		handled = false;
		return IntPtr.Zero;
	}

	private IntPtr _HandleMove(Standard.WM uMsg, IntPtr wParam, IntPtr lParam, out bool handled)
	{
		Standard.Assert.IsTrue(Standard.Utility.IsPresentationFrameworkVersionLessThan4);
		if (_isUserResizing)
		{
			_hasUserMovedWindow = true;
		}
		handled = false;
		return IntPtr.Zero;
	}

	private bool _ModifyStyle(Standard.WS removeStyle, Standard.WS addStyle)
	{
		Standard.Assert.IsNotDefault(_hwnd);
		Standard.WS wS = (Standard.WS)Standard.NativeMethods.GetWindowLongPtr(_hwnd, Standard.GWL.STYLE).ToInt32();
		Standard.WS wS2 = (wS & ~removeStyle) | addStyle;
		if (wS == wS2)
		{
			return false;
		}
		Standard.NativeMethods.SetWindowLongPtr(_hwnd, Standard.GWL.STYLE, new IntPtr((int)wS2));
		return true;
	}

	private WindowState _GetHwndState()
	{
		Standard.WINDOWPLACEMENT windowPlacement = Standard.NativeMethods.GetWindowPlacement(_hwnd);
		return windowPlacement.showCmd switch
		{
			Standard.SW.SHOWMINIMIZED => WindowState.Minimized, 
			Standard.SW.SHOWMAXIMIZED => WindowState.Maximized, 
			_ => WindowState.Normal, 
		};
	}

	private Rect _GetWindowRect()
	{
		Standard.RECT windowRect = Standard.NativeMethods.GetWindowRect(_hwnd);
		return new Rect(windowRect.Left, windowRect.Top, windowRect.Width, windowRect.Height);
	}

	private void _UpdateSystemMenu(WindowState? assumeState)
	{
		WindowState windowState = assumeState ?? _GetHwndState();
		if (!assumeState.HasValue && _lastMenuState == windowState)
		{
			return;
		}
		_lastMenuState = windowState;
		bool flag = _ModifyStyle(Standard.WS.VISIBLE, Standard.WS.OVERLAPPED);
		IntPtr systemMenu = Standard.NativeMethods.GetSystemMenu(_hwnd, bRevert: false);
		if (IntPtr.Zero != systemMenu)
		{
			Standard.WS value = (Standard.WS)Standard.NativeMethods.GetWindowLongPtr(_hwnd, Standard.GWL.STYLE).ToInt32();
			bool flag2 = Standard.Utility.IsFlagSet((int)value, 131072);
			bool flag3 = Standard.Utility.IsFlagSet((int)value, 65536);
			bool flag4 = Standard.Utility.IsFlagSet((int)value, 262144);
			switch (windowState)
			{
			case WindowState.Maximized:
				Standard.NativeMethods.EnableMenuItem(systemMenu, Standard.SC.RESTORE, Standard.MF.ENABLED);
				Standard.NativeMethods.EnableMenuItem(systemMenu, Standard.SC.MOVE, Standard.MF.GRAYED | Standard.MF.DISABLED);
				Standard.NativeMethods.EnableMenuItem(systemMenu, Standard.SC.SIZE, Standard.MF.GRAYED | Standard.MF.DISABLED);
				Standard.NativeMethods.EnableMenuItem(systemMenu, Standard.SC.MINIMIZE, (!flag2) ? (Standard.MF.GRAYED | Standard.MF.DISABLED) : Standard.MF.ENABLED);
				Standard.NativeMethods.EnableMenuItem(systemMenu, Standard.SC.MAXIMIZE, Standard.MF.GRAYED | Standard.MF.DISABLED);
				break;
			case WindowState.Minimized:
				Standard.NativeMethods.EnableMenuItem(systemMenu, Standard.SC.RESTORE, Standard.MF.ENABLED);
				Standard.NativeMethods.EnableMenuItem(systemMenu, Standard.SC.MOVE, Standard.MF.GRAYED | Standard.MF.DISABLED);
				Standard.NativeMethods.EnableMenuItem(systemMenu, Standard.SC.SIZE, Standard.MF.GRAYED | Standard.MF.DISABLED);
				Standard.NativeMethods.EnableMenuItem(systemMenu, Standard.SC.MINIMIZE, Standard.MF.GRAYED | Standard.MF.DISABLED);
				Standard.NativeMethods.EnableMenuItem(systemMenu, Standard.SC.MAXIMIZE, (!flag3) ? (Standard.MF.GRAYED | Standard.MF.DISABLED) : Standard.MF.ENABLED);
				break;
			default:
				Standard.NativeMethods.EnableMenuItem(systemMenu, Standard.SC.RESTORE, Standard.MF.GRAYED | Standard.MF.DISABLED);
				Standard.NativeMethods.EnableMenuItem(systemMenu, Standard.SC.MOVE, Standard.MF.ENABLED);
				Standard.NativeMethods.EnableMenuItem(systemMenu, Standard.SC.SIZE, (!flag4) ? (Standard.MF.GRAYED | Standard.MF.DISABLED) : Standard.MF.ENABLED);
				Standard.NativeMethods.EnableMenuItem(systemMenu, Standard.SC.MINIMIZE, (!flag2) ? (Standard.MF.GRAYED | Standard.MF.DISABLED) : Standard.MF.ENABLED);
				Standard.NativeMethods.EnableMenuItem(systemMenu, Standard.SC.MAXIMIZE, (!flag3) ? (Standard.MF.GRAYED | Standard.MF.DISABLED) : Standard.MF.ENABLED);
				break;
			}
		}
		if (flag)
		{
			_ModifyStyle(Standard.WS.OVERLAPPED, Standard.WS.VISIBLE);
		}
	}

	private void _UpdateFrameState(bool force)
	{
		if (IntPtr.Zero == _hwnd)
		{
			return;
		}
		bool flag = Standard.NativeMethods.DwmIsCompositionEnabled();
		if (force || flag != _isGlassEnabled)
		{
			_isGlassEnabled = flag && _chromeInfo.GlassFrameThickness != default(Thickness);
			if (!_isGlassEnabled)
			{
				_SetRoundingRegion(null);
			}
			else
			{
				_ClearRoundingRegion();
				_ExtendGlassFrame();
				_FixupWindows7Issues();
			}
			Standard.NativeMethods.SetWindowPos(_hwnd, IntPtr.Zero, 0, 0, 0, 0, Standard.SWP.DRAWFRAME | Standard.SWP.NOACTIVATE | Standard.SWP.NOMOVE | Standard.SWP.NOOWNERZORDER | Standard.SWP.NOSIZE | Standard.SWP.NOZORDER);
		}
	}

	private void _ClearRoundingRegion()
	{
		Standard.NativeMethods.SetWindowRgn(_hwnd, IntPtr.Zero, Standard.NativeMethods.IsWindowVisible(_hwnd));
	}

	private void _SetRoundingRegion(Standard.WINDOWPOS? wp)
	{
		Standard.WINDOWPLACEMENT windowPlacement = Standard.NativeMethods.GetWindowPlacement(_hwnd);
		if (windowPlacement.showCmd == Standard.SW.SHOWMAXIMIZED)
		{
			int num;
			int num2;
			if (wp.HasValue)
			{
				num = wp.Value.x;
				num2 = wp.Value.y;
			}
			else
			{
				Rect rect = _GetWindowRect();
				num = (int)rect.Left;
				num2 = (int)rect.Top;
			}
			IntPtr hMonitor = Standard.NativeMethods.MonitorFromWindow(_hwnd, 2u);
			Standard.MONITORINFO monitorInfo = Standard.NativeMethods.GetMonitorInfo(hMonitor);
			Standard.RECT rcWork = monitorInfo.rcWork;
			rcWork.Offset(-num, -num2);
			IntPtr gdiObject = IntPtr.Zero;
			try
			{
				gdiObject = Standard.NativeMethods.CreateRectRgnIndirect(rcWork);
				Standard.NativeMethods.SetWindowRgn(_hwnd, gdiObject, Standard.NativeMethods.IsWindowVisible(_hwnd));
				gdiObject = IntPtr.Zero;
				return;
			}
			finally
			{
				Standard.Utility.SafeDeleteObject(ref gdiObject);
			}
		}
		Size size;
		if (wp.HasValue && !Standard.Utility.IsFlagSet(wp.Value.flags, 1))
		{
			size = new Size(wp.Value.cx, wp.Value.cy);
		}
		else
		{
			if (wp.HasValue && _lastRoundingState == _window.WindowState)
			{
				return;
			}
			size = _GetWindowRect().Size;
		}
		_lastRoundingState = _window.WindowState;
		IntPtr gdiObject2 = IntPtr.Zero;
		try
		{
			double num3 = Math.Min(size.Width, size.Height);
			double x = Standard.DpiHelper.LogicalPixelsToDevice(new Point(_chromeInfo.CornerRadius.TopLeft, 0.0)).X;
			x = Math.Min(x, num3 / 2.0);
			if (_IsUniform(_chromeInfo.CornerRadius))
			{
				gdiObject2 = _CreateRoundRectRgn(new Rect(size), x);
			}
			else
			{
				gdiObject2 = _CreateRoundRectRgn(new Rect(0.0, 0.0, size.Width / 2.0 + x, size.Height / 2.0 + x), x);
				double x2 = Standard.DpiHelper.LogicalPixelsToDevice(new Point(_chromeInfo.CornerRadius.TopRight, 0.0)).X;
				x2 = Math.Min(x2, num3 / 2.0);
				Rect region = new Rect(0.0, 0.0, size.Width / 2.0 + x2, size.Height / 2.0 + x2);
				region.Offset(size.Width / 2.0 - x2, 0.0);
				Standard.Assert.AreEqual(region.Right, size.Width);
				_CreateAndCombineRoundRectRgn(gdiObject2, region, x2);
				double x3 = Standard.DpiHelper.LogicalPixelsToDevice(new Point(_chromeInfo.CornerRadius.BottomLeft, 0.0)).X;
				x3 = Math.Min(x3, num3 / 2.0);
				Rect region2 = new Rect(0.0, 0.0, size.Width / 2.0 + x3, size.Height / 2.0 + x3);
				region2.Offset(0.0, size.Height / 2.0 - x3);
				Standard.Assert.AreEqual(region2.Bottom, size.Height);
				_CreateAndCombineRoundRectRgn(gdiObject2, region2, x3);
				double x4 = Standard.DpiHelper.LogicalPixelsToDevice(new Point(_chromeInfo.CornerRadius.BottomRight, 0.0)).X;
				x4 = Math.Min(x4, num3 / 2.0);
				Rect region3 = new Rect(0.0, 0.0, size.Width / 2.0 + x4, size.Height / 2.0 + x4);
				region3.Offset(size.Width / 2.0 - x4, size.Height / 2.0 - x4);
				Standard.Assert.AreEqual(region3.Right, size.Width);
				Standard.Assert.AreEqual(region3.Bottom, size.Height);
				_CreateAndCombineRoundRectRgn(gdiObject2, region3, x4);
			}
			Standard.NativeMethods.SetWindowRgn(_hwnd, gdiObject2, Standard.NativeMethods.IsWindowVisible(_hwnd));
			gdiObject2 = IntPtr.Zero;
		}
		finally
		{
			Standard.Utility.SafeDeleteObject(ref gdiObject2);
		}
	}

	private static IntPtr _CreateRoundRectRgn(Rect region, double radius)
	{
		if (Standard.DoubleUtilities.AreClose(0.0, radius))
		{
			return Standard.NativeMethods.CreateRectRgn((int)Math.Floor(region.Left), (int)Math.Floor(region.Top), (int)Math.Ceiling(region.Right), (int)Math.Ceiling(region.Bottom));
		}
		return Standard.NativeMethods.CreateRoundRectRgn((int)Math.Floor(region.Left), (int)Math.Floor(region.Top), (int)Math.Ceiling(region.Right) + 1, (int)Math.Ceiling(region.Bottom) + 1, (int)Math.Ceiling(radius), (int)Math.Ceiling(radius));
	}

	private static void _CreateAndCombineRoundRectRgn(IntPtr hrgnSource, Rect region, double radius)
	{
		IntPtr gdiObject = IntPtr.Zero;
		try
		{
			gdiObject = _CreateRoundRectRgn(region, radius);
			if (Standard.NativeMethods.CombineRgn(hrgnSource, hrgnSource, gdiObject, Standard.RGN.OR) == Standard.CombineRgnResult.ERROR)
			{
				throw new InvalidOperationException("Unable to combine two HRGNs.");
			}
		}
		catch
		{
			Standard.Utility.SafeDeleteObject(ref gdiObject);
			throw;
		}
	}

	private static bool _IsUniform(CornerRadius cornerRadius)
	{
		if (!Standard.DoubleUtilities.AreClose(cornerRadius.BottomLeft, cornerRadius.BottomRight))
		{
			return false;
		}
		if (!Standard.DoubleUtilities.AreClose(cornerRadius.TopLeft, cornerRadius.TopRight))
		{
			return false;
		}
		if (!Standard.DoubleUtilities.AreClose(cornerRadius.BottomLeft, cornerRadius.TopRight))
		{
			return false;
		}
		return true;
	}

	private void _ExtendGlassFrame()
	{
		Standard.Assert.IsNotNull(_window);
		if (Standard.Utility.IsOSVistaOrNewer && !(IntPtr.Zero == _hwnd))
		{
			if (!Standard.NativeMethods.DwmIsCompositionEnabled())
			{
				_hwndSource.CompositionTarget.BackgroundColor = SystemColors.WindowColor;
				return;
			}
			_hwndSource.CompositionTarget.BackgroundColor = Colors.Transparent;
			Point point = Standard.DpiHelper.LogicalPixelsToDevice(new Point(_chromeInfo.GlassFrameThickness.Left, _chromeInfo.GlassFrameThickness.Top));
			Point point2 = Standard.DpiHelper.LogicalPixelsToDevice(new Point(_chromeInfo.GlassFrameThickness.Right, _chromeInfo.GlassFrameThickness.Bottom));
			Standard.MARGINS pMarInset = new Standard.MARGINS
			{
				cxLeftWidth = (int)Math.Ceiling(point.X),
				cxRightWidth = (int)Math.Ceiling(point2.X),
				cyTopHeight = (int)Math.Ceiling(point.Y),
				cyBottomHeight = (int)Math.Ceiling(point2.Y)
			};
			Standard.NativeMethods.DwmExtendFrameIntoClientArea(_hwnd, ref pMarInset);
		}
	}

	private Standard.HT _HitTestNca(Rect windowPosition, Point mousePosition)
	{
		int num = 1;
		int num2 = 1;
		bool flag = false;
		if (mousePosition.Y >= windowPosition.Top && mousePosition.Y < windowPosition.Top + _chromeInfo.ResizeBorderThickness.Top + _chromeInfo.CaptionHeight)
		{
			flag = mousePosition.Y < windowPosition.Top + _chromeInfo.ResizeBorderThickness.Top;
			num = 0;
		}
		else if (mousePosition.Y < windowPosition.Bottom && mousePosition.Y >= windowPosition.Bottom - (double)(int)_chromeInfo.ResizeBorderThickness.Bottom)
		{
			num = 2;
		}
		if (mousePosition.X >= windowPosition.Left && mousePosition.X < windowPosition.Left + (double)(int)_chromeInfo.ResizeBorderThickness.Left)
		{
			num2 = 0;
		}
		else if (mousePosition.X < windowPosition.Right && mousePosition.X >= windowPosition.Right - _chromeInfo.ResizeBorderThickness.Right)
		{
			num2 = 2;
		}
		if (num == 0 && num2 != 1 && !flag)
		{
			num = 1;
		}
		Standard.HT hT = _HitTestBorders[num, num2];
		if (hT == Standard.HT.TOP && !flag)
		{
			hT = Standard.HT.CAPTION;
		}
		return hT;
	}

	private void _RestoreStandardChromeState(bool isClosing)
	{
		VerifyAccess();
		_UnhookCustomChrome();
		if (!isClosing)
		{
			_RestoreFrameworkIssueFixups();
			_RestoreGlassFrame();
			_RestoreHrgn();
			_window.InvalidateMeasure();
		}
	}

	private void _UnhookCustomChrome()
	{
		Standard.Assert.IsNotDefault(_hwnd);
		Standard.Assert.IsNotNull(_window);
		if (_isHooked)
		{
			_hwndSource.RemoveHook(_WndProc);
			_isHooked = false;
		}
	}

	private void _RestoreFrameworkIssueFixups()
	{
		if (Standard.Utility.IsPresentationFrameworkVersionLessThan4)
		{
			Standard.Assert.IsTrue(_isFixedUp);
			FrameworkElement frameworkElement = (FrameworkElement)VisualTreeHelper.GetChild(_window, 0);
			frameworkElement.Margin = default(Thickness);
			_window.StateChanged -= _FixupRestoreBounds;
			_isFixedUp = false;
		}
	}

	private void _RestoreGlassFrame()
	{
		Standard.Assert.IsNull(_chromeInfo);
		Standard.Assert.IsNotNull(_window);
		if (Standard.Utility.IsOSVistaOrNewer && !(_hwnd == IntPtr.Zero))
		{
			_hwndSource.CompositionTarget.BackgroundColor = SystemColors.WindowColor;
			if (Standard.NativeMethods.DwmIsCompositionEnabled())
			{
				Standard.MARGINS pMarInset = default(Standard.MARGINS);
				Standard.NativeMethods.DwmExtendFrameIntoClientArea(_hwnd, ref pMarInset);
			}
		}
	}

	private void _RestoreHrgn()
	{
		_ClearRoundingRegion();
		Standard.NativeMethods.SetWindowPos(_hwnd, IntPtr.Zero, 0, 0, 0, 0, Standard.SWP.DRAWFRAME | Standard.SWP.NOACTIVATE | Standard.SWP.NOMOVE | Standard.SWP.NOOWNERZORDER | Standard.SWP.NOSIZE | Standard.SWP.NOZORDER);
	}
}
