using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace SamSoarII.Dock.Global;

public abstract class Win32Helper
{
	[Flags]
	public enum SetWindowPosFlags : uint
	{
		SynchronousWindowPosition = 0x4000u,
		DeferErase = 0x2000u,
		DrawFrame = 0x20u,
		FrameChanged = 0x20u,
		HideWindow = 0x80u,
		DoNotActivate = 0x10u,
		DoNotCopyBits = 0x100u,
		IgnoreMove = 2u,
		DoNotChangeOwnerZOrder = 0x200u,
		DoNotRedraw = 8u,
		DoNotReposition = 0x200u,
		DoNotSendChangingEvent = 0x400u,
		IgnoreResize = 1u,
		IgnoreZOrder = 4u,
		ShowWindow = 0x40u
	}

	[StructLayout(LayoutKind.Sequential)]
	public class WINDOWPOS
	{
		public IntPtr hwnd;

		public IntPtr hwndInsertAfter;

		public int x;

		public int y;

		public int cx;

		public int cy;

		public int flags;
	}

	public enum HookType
	{
		WH_JOURNALRECORD,
		WH_JOURNALPLAYBACK,
		WH_KEYBOARD,
		WH_GETMESSAGE,
		WH_CALLWNDPROC,
		WH_CBT,
		WH_SYSMSGFILTER,
		WH_MOUSE,
		WH_HARDWARE,
		WH_DEBUG,
		WH_SHELL,
		WH_FOREGROUNDIDLE,
		WH_CALLWNDPROCRET,
		WH_KEYBOARD_LL,
		WH_MOUSE_LL
	}

	public delegate int HookProc(int code, IntPtr wParam, IntPtr lParam);

	[Serializable]
	public struct RECT(int left_, int top_, int right_, int bottom_)
	{
		public int Left = left_;

		public int Top = top_;

		public int Right = right_;

		public int Bottom = bottom_;

		public int Height => Bottom - Top;

		public int Width => Right - Left;

		public Size Size => new Size(Width, Height);

		public Point Location => new Point(Left, Top);

		public Rect ToRectangle()
		{
			return new Rect(Left, Top, Right, Bottom);
		}

		public static RECT FromRectangle(Rect rectangle)
		{
			return new Rect(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);
		}

		public override int GetHashCode()
		{
			return Left ^ ((Top << 13) | (Top >> 19)) ^ ((Width << 26) | (Width >> 6)) ^ ((Height << 7) | (Height >> 25));
		}

		public static implicit operator Rect(RECT rect)
		{
			return rect.ToRectangle();
		}

		public static implicit operator RECT(Rect rect)
		{
			return FromRectangle(rect);
		}
	}

	internal struct MINMAXINFO
	{
		public Win32Point ptReserved;

		public Win32Point ptMaxSize;

		public Win32Point ptMaxPosition;

		public Win32Point ptMinTrackSize;

		public Win32Point ptMaxTrackSize;
	}

	public enum GetWindow_Cmd : uint
	{
		GW_HWNDFIRST,
		GW_HWNDLAST,
		GW_HWNDNEXT,
		GW_HWNDPREV,
		GW_OWNER,
		GW_CHILD,
		GW_ENABLEDPOPUP
	}

	public struct Win32Point
	{
		public int X;

		public int Y;
	}

	[StructLayout(LayoutKind.Sequential)]
	public class MonitorInfo
	{
		public int Size = Marshal.SizeOf(typeof(MonitorInfo));

		public RECT Monitor;

		public RECT Work;

		public uint Flags;
	}

	public const int WS_CHILD = 1073741824;

	public const int WS_VISIBLE = 268435456;

	public const int WS_VSCROLL = 2097152;

	public const int WS_BORDER = 8388608;

	public const int WS_CLIPSIBLINGS = 67108864;

	public const int WS_CLIPCHILDREN = 33554432;

	public const int WS_TABSTOP = 65536;

	public const int WS_GROUP = 131072;

	public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

	public static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);

	public static readonly IntPtr HWND_TOP = new IntPtr(0);

	public static readonly IntPtr HWND_BOTTOM = new IntPtr(1);

	public const int WM_WINDOWPOSCHANGED = 71;

	public const int WM_WINDOWPOSCHANGING = 70;

	public const int WM_GETMINMAXINFO = 36;

	public const int WM_NCMOUSEMOVE = 160;

	public const int WM_NCLBUTTONDOWN = 161;

	public const int WM_NCLBUTTONUP = 162;

	public const int WM_NCLBUTTONDBLCLK = 163;

	public const int WM_NCRBUTTONDOWN = 164;

	public const int WM_NCRBUTTONUP = 165;

	public const int WM_CAPTURECHANGED = 533;

	public const int WM_EXITSIZEMOVE = 562;

	public const int WM_ENTERSIZEMOVE = 561;

	public const int WM_MOVE = 3;

	public const int WM_MOVING = 534;

	public const int WM_KILLFOCUS = 8;

	public const int WM_SETFOCUS = 7;

	public const int WM_ACTIVATE = 6;

	public const int WM_NCHITTEST = 132;

	public const int WM_INITMENUPOPUP = 279;

	public const int WM_KEYDOWN = 256;

	public const int WM_KEYUP = 257;

	public const int WA_INACTIVE = 0;

	public const int WM_SYSCOMMAND = 274;

	public const int SC_MAXIMIZE = 61488;

	public const int SC_RESTORE = 61728;

	public const int WM_CREATE = 1;

	public const int HT_CAPTION = 2;

	public const int HCBT_SETFOCUS = 9;

	public const int HCBT_ACTIVATE = 5;

	public const uint GW_HWNDNEXT = 2u;

	public const uint GW_HWNDPREV = 3u;

	public const int WM_MOUSEMOVE = 512;

	public const int WM_LBUTTONDOWN = 513;

	public const int WM_LBUTTONUP = 514;

	public const int WM_LBUTTONDBLCLK = 515;

	public const int WM_RBUTTONDOWN = 516;

	public const int WM_RBUTTONUP = 517;

	public const int WM_RBUTTONDBLCLK = 518;

	public const int WM_MBUTTONDOWN = 519;

	public const int WM_MBUTTONUP = 520;

	public const int WM_MBUTTONDBLCLK = 521;

	public const int WM_MOUSEWHEEL = 522;

	public const int WM_MOUSEHWHEEL = 526;

	public static readonly uint MONITOR_DEFAULTTONEAREST = 2u;

	[DllImport("user32.dll", CharSet = CharSet.Unicode)]
	public static extern IntPtr CreateWindow(string lpszClassName, string lpszWindowName, int style, int x, int y, int width, int height, IntPtr hwndParent, IntPtr hMenu, IntPtr hInst, [MarshalAs(UnmanagedType.AsAny)] object pvParam);

	[DllImport("user32.dll", CharSet = CharSet.Unicode)]
	public static extern IntPtr CreateWindowEx(int dwExStyle, string lpszClassName, string lpszWindowName, int style, int x, int y, int width, int height, IntPtr hwndParent, IntPtr hMenu, IntPtr hInst, [MarshalAs(UnmanagedType.AsAny)] object pvParam);

	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags);

	[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
	public static extern bool IsChild(IntPtr hWndParent, IntPtr hwnd);

	[DllImport("user32.dll")]
	public static extern IntPtr SetFocus(IntPtr hWnd);

	[DllImport("user32.dll", SetLastError = true)]
	public static extern IntPtr SetActiveWindow(IntPtr hWnd);

	[DllImport("user32.dll", CharSet = CharSet.Unicode)]
	public static extern bool DestroyWindow(IntPtr hwnd);

	[DllImport("user32.dll")]
	public static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

	[DllImport("user32.dll")]
	public static extern int PostMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

	[DllImport("user32.dll")]
	private static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

	[DllImport("user32.dll")]
	public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

	[DllImport("kernel32.dll")]
	public static extern uint GetCurrentThreadId();

	[DllImport("user32.dll")]
	public static extern IntPtr SetWindowsHookEx(HookType code, HookProc func, IntPtr hInstance, int threadID);

	[DllImport("user32.dll")]
	public static extern int UnhookWindowsHookEx(IntPtr hhook);

	[DllImport("user32.dll")]
	public static extern int CallNextHookEx(IntPtr hhook, int code, IntPtr wParam, IntPtr lParam);

	public static RECT GetClientRect(IntPtr hWnd)
	{
		RECT lpRect = default(RECT);
		GetClientRect(hWnd, out lpRect);
		return lpRect;
	}

	public static RECT GetWindowRect(IntPtr hWnd)
	{
		RECT lpRect = default(RECT);
		GetWindowRect(hWnd, out lpRect);
		return lpRect;
	}

	[DllImport("user32.dll")]
	public static extern IntPtr GetTopWindow(IntPtr hWnd);

	[DllImport("user32.dll", SetLastError = true)]
	public static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

	public static int MakeLParam(int LoWord, int HiWord)
	{
		return (HiWord << 16) | (LoWord & 0xFFFF);
	}

	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool GetCursorPos(ref Win32Point pt);

	public static Point GetMousePosition()
	{
		Win32Point pt = default(Win32Point);
		GetCursorPos(ref pt);
		return new Point(pt.X, pt.Y);
	}

	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool IsWindowVisible(IntPtr hWnd);

	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool IsWindowEnabled(IntPtr hWnd);

	[DllImport("user32.dll")]
	public static extern IntPtr GetFocus();

	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool BringWindowToTop(IntPtr hWnd);

	[DllImport("user32.dll", SetLastError = true)]
	public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

	[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
	public static extern IntPtr GetParent(IntPtr hWnd);

	[DllImport("user32.dll")]
	private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

	[DllImport("user32.dll", SetLastError = true)]
	private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

	public static void SetOwner(IntPtr childHandle, IntPtr ownerHandle)
	{
		SetWindowLong(childHandle, -8, ownerHandle.ToInt32());
	}

	public static IntPtr GetOwner(IntPtr childHandle)
	{
		return new IntPtr(GetWindowLong(childHandle, -8));
	}

	[DllImport("user32.dll")]
	public static extern IntPtr MonitorFromRect([In] ref RECT lprc, uint dwFlags);

	[DllImport("user32.dll")]
	public static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);

	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool GetMonitorInfo(IntPtr hMonitor, [In][Out] MonitorInfo lpmi);
}
