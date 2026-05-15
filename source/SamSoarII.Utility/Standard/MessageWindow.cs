#define DEBUG
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Threading;

namespace Standard;

internal sealed class MessageWindow : DispatcherObject, IDisposable
{
	private static readonly Standard.WndProc s_WndProc = _WndProc;

	private static readonly Dictionary<IntPtr, Standard.MessageWindow> s_windowLookup = new Dictionary<IntPtr, Standard.MessageWindow>();

	private Standard.WndProc _wndProcCallback;

	private string _className;

	private bool _isDisposed;

	public IntPtr Handle { get; private set; }

	public MessageWindow(Standard.CS classStyle, Standard.WS style, Standard.WS_EX exStyle, Rect location, string name, Standard.WndProc callback)
	{
		_wndProcCallback = callback;
		_className = "MessageWindowClass+" + Guid.NewGuid().ToString();
		Standard.WNDCLASSEX lpwcx = new Standard.WNDCLASSEX
		{
			cbSize = Marshal.SizeOf(typeof(Standard.WNDCLASSEX)),
			style = classStyle,
			lpfnWndProc = s_WndProc,
			hInstance = Standard.NativeMethods.GetModuleHandle(null),
			hbrBackground = Standard.NativeMethods.GetStockObject(Standard.StockObject.NULL_BRUSH),
			lpszMenuName = "",
			lpszClassName = _className
		};
		Standard.NativeMethods.RegisterClassEx(ref lpwcx);
		GCHandle gCHandle = default(GCHandle);
		try
		{
			gCHandle = GCHandle.Alloc(this);
			IntPtr lpParam = (IntPtr)gCHandle;
			Handle = Standard.NativeMethods.CreateWindowEx(exStyle, _className, name, style, (int)location.X, (int)location.Y, (int)location.Width, (int)location.Height, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, lpParam);
		}
		finally
		{
			gCHandle.Free();
		}
	}

	~MessageWindow()
	{
		_Dispose(disposing: false, isHwndBeingDestroyed: false);
	}

	public void Dispose()
	{
		_Dispose(disposing: true, isHwndBeingDestroyed: false);
		GC.SuppressFinalize(this);
	}

	private void _Dispose(bool disposing, bool isHwndBeingDestroyed)
	{
		if (_isDisposed)
		{
			return;
		}
		_isDisposed = true;
		IntPtr hwnd = Handle;
		string className = _className;
		if (isHwndBeingDestroyed)
		{
			base.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (DispatcherOperationCallback)((object arg) => _DestroyWindow(IntPtr.Zero, className)));
		}
		else if (Handle != IntPtr.Zero)
		{
			if (CheckAccess())
			{
				_DestroyWindow(hwnd, className);
			}
			else
			{
				base.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (DispatcherOperationCallback)((object arg) => _DestroyWindow(hwnd, className)));
			}
		}
		s_windowLookup.Remove(hwnd);
		_className = null;
		Handle = IntPtr.Zero;
	}

	private static IntPtr _WndProc(IntPtr hwnd, Standard.WM msg, IntPtr wParam, IntPtr lParam)
	{
		IntPtr zero = IntPtr.Zero;
		Standard.MessageWindow value = null;
		if (msg == Standard.WM.CREATE)
		{
			value = (Standard.MessageWindow)GCHandle.FromIntPtr(((Standard.CREATESTRUCT)Marshal.PtrToStructure(lParam, typeof(Standard.CREATESTRUCT))).lpCreateParams).Target;
			s_windowLookup.Add(hwnd, value);
		}
		else if (!s_windowLookup.TryGetValue(hwnd, out value))
		{
			return Standard.NativeMethods.DefWindowProc(hwnd, msg, wParam, lParam);
		}
		Standard.Assert.IsNotNull(value);
		zero = value._wndProcCallback?.Invoke(hwnd, msg, wParam, lParam) ?? Standard.NativeMethods.DefWindowProc(hwnd, msg, wParam, lParam);
		if (msg == Standard.WM.NCDESTROY)
		{
			value._Dispose(disposing: true, isHwndBeingDestroyed: true);
			GC.SuppressFinalize(value);
		}
		return zero;
	}

	private static object _DestroyWindow(IntPtr hwnd, string className)
	{
		Standard.Utility.SafeDestroyWindow(ref hwnd);
		Standard.NativeMethods.UnregisterClass(className, Standard.NativeMethods.GetModuleHandle(null));
		return null;
	}
}
