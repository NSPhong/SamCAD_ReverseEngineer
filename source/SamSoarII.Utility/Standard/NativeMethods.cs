#define DEBUG
using System;
using System.ComponentModel;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace Standard;

internal static class NativeMethods
{
	[DllImport("user32.dll", EntryPoint = "AdjustWindowRectEx", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool _AdjustWindowRectEx(ref Standard.RECT lpRect, Standard.WS dwStyle, [MarshalAs(UnmanagedType.Bool)] bool bMenu, Standard.WS_EX dwExStyle);

	public static Standard.RECT AdjustWindowRectEx(Standard.RECT lpRect, Standard.WS dwStyle, bool bMenu, Standard.WS_EX dwExStyle)
	{
		if (!_AdjustWindowRectEx(ref lpRect, dwStyle, bMenu, dwExStyle))
		{
			Standard.HRESULT.ThrowLastError();
		}
		return lpRect;
	}

	[DllImport("user32.dll", EntryPoint = "ChangeWindowMessageFilter", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool _ChangeWindowMessageFilter(Standard.WM message, Standard.MSGFLT dwFlag);

	[DllImport("user32.dll", EntryPoint = "ChangeWindowMessageFilterEx", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool _ChangeWindowMessageFilterEx(IntPtr hwnd, Standard.WM message, Standard.MSGFLT action, [Optional][In][Out] ref Standard.CHANGEFILTERSTRUCT pChangeFilterStruct);

	public static Standard.HRESULT ChangeWindowMessageFilterEx(IntPtr hwnd, Standard.WM message, Standard.MSGFLT action, out Standard.MSGFLTINFO filterInfo)
	{
		filterInfo = Standard.MSGFLTINFO.NONE;
		if (!Standard.Utility.IsOSVistaOrNewer)
		{
			return Standard.HRESULT.S_FALSE;
		}
		if (!Standard.Utility.IsOSWindows7OrNewer)
		{
			if (!_ChangeWindowMessageFilter(message, action))
			{
				return (Standard.HRESULT)Standard.Win32Error.GetLastError();
			}
			return Standard.HRESULT.S_OK;
		}
		Standard.CHANGEFILTERSTRUCT pChangeFilterStruct = new Standard.CHANGEFILTERSTRUCT
		{
			cbSize = (uint)Marshal.SizeOf(typeof(Standard.CHANGEFILTERSTRUCT))
		};
		if (!_ChangeWindowMessageFilterEx(hwnd, message, action, ref pChangeFilterStruct))
		{
			return (Standard.HRESULT)Standard.Win32Error.GetLastError();
		}
		filterInfo = pChangeFilterStruct.ExtStatus;
		return Standard.HRESULT.S_OK;
	}

	[DllImport("gdi32.dll")]
	public static extern Standard.CombineRgnResult CombineRgn(IntPtr hrgnDest, IntPtr hrgnSrc1, IntPtr hrgnSrc2, Standard.RGN fnCombineMode);

	[DllImport("shell32.dll", CharSet = CharSet.Unicode, EntryPoint = "CommandLineToArgvW")]
	private static extern IntPtr _CommandLineToArgvW([MarshalAs(UnmanagedType.LPWStr)] string cmdLine, out int numArgs);

	public static string[] CommandLineToArgvW(string cmdLine)
	{
		IntPtr intPtr = IntPtr.Zero;
		try
		{
			int numArgs = 0;
			intPtr = _CommandLineToArgvW(cmdLine, out numArgs);
			if (intPtr == IntPtr.Zero)
			{
				throw new Win32Exception();
			}
			string[] array = new string[numArgs];
			for (int i = 0; i < numArgs; i++)
			{
				IntPtr ptr = Marshal.ReadIntPtr(intPtr, i * Marshal.SizeOf(typeof(IntPtr)));
				array[i] = Marshal.PtrToStringUni(ptr);
			}
			return array;
		}
		finally
		{
			IntPtr actual = _LocalFree(intPtr);
			Standard.Assert.AreEqual(IntPtr.Zero, actual);
		}
	}

	[DllImport("gdi32.dll", EntryPoint = "CreateDIBSection", SetLastError = true)]
	private static extern Standard.SafeHBITMAP _CreateDIBSection(Standard.SafeDC hdc, [In] ref Standard.BITMAPINFO bitmapInfo, int iUsage, out IntPtr ppvBits, IntPtr hSection, int dwOffset);

	[DllImport("gdi32.dll", EntryPoint = "CreateDIBSection", SetLastError = true)]
	private static extern Standard.SafeHBITMAP _CreateDIBSectionIntPtr(IntPtr hdc, [In] ref Standard.BITMAPINFO bitmapInfo, int iUsage, out IntPtr ppvBits, IntPtr hSection, int dwOffset);

	public static Standard.SafeHBITMAP CreateDIBSection(Standard.SafeDC hdc, ref Standard.BITMAPINFO bitmapInfo, out IntPtr ppvBits, IntPtr hSection, int dwOffset)
	{
		Standard.SafeHBITMAP safeHBITMAP = null;
		safeHBITMAP = ((hdc != null) ? _CreateDIBSection(hdc, ref bitmapInfo, 0, out ppvBits, hSection, dwOffset) : _CreateDIBSectionIntPtr(IntPtr.Zero, ref bitmapInfo, 0, out ppvBits, hSection, dwOffset));
		if (safeHBITMAP.IsInvalid)
		{
			Standard.HRESULT.ThrowLastError();
		}
		return safeHBITMAP;
	}

	[DllImport("gdi32.dll", EntryPoint = "CreateRoundRectRgn", SetLastError = true)]
	private static extern IntPtr _CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

	public static IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse)
	{
		IntPtr intPtr = _CreateRoundRectRgn(nLeftRect, nTopRect, nRightRect, nBottomRect, nWidthEllipse, nHeightEllipse);
		if (IntPtr.Zero == intPtr)
		{
			throw new Win32Exception();
		}
		return intPtr;
	}

	[DllImport("gdi32.dll", EntryPoint = "CreateRectRgn", SetLastError = true)]
	private static extern IntPtr _CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

	public static IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect)
	{
		IntPtr intPtr = _CreateRectRgn(nLeftRect, nTopRect, nRightRect, nBottomRect);
		if (IntPtr.Zero == intPtr)
		{
			throw new Win32Exception();
		}
		return intPtr;
	}

	[DllImport("gdi32.dll", EntryPoint = "CreateRectRgnIndirect", SetLastError = true)]
	private static extern IntPtr _CreateRectRgnIndirect([In] ref Standard.RECT lprc);

	public static IntPtr CreateRectRgnIndirect(Standard.RECT lprc)
	{
		IntPtr intPtr = _CreateRectRgnIndirect(ref lprc);
		if (IntPtr.Zero == intPtr)
		{
			throw new Win32Exception();
		}
		return intPtr;
	}

	[DllImport("gdi32.dll")]
	public static extern IntPtr CreateSolidBrush(int crColor);

	[DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "CreateWindowExW", SetLastError = true)]
	private static extern IntPtr _CreateWindowEx(Standard.WS_EX dwExStyle, [MarshalAs(UnmanagedType.LPWStr)] string lpClassName, [MarshalAs(UnmanagedType.LPWStr)] string lpWindowName, Standard.WS dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam);

	public static IntPtr CreateWindowEx(Standard.WS_EX dwExStyle, string lpClassName, string lpWindowName, Standard.WS dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam)
	{
		IntPtr intPtr = _CreateWindowEx(dwExStyle, lpClassName, lpWindowName, dwStyle, x, y, nWidth, nHeight, hWndParent, hMenu, hInstance, lpParam);
		if (IntPtr.Zero == intPtr)
		{
			Standard.HRESULT.ThrowLastError();
		}
		return intPtr;
	}

	[DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "DefWindowProcW")]
	public static extern IntPtr DefWindowProc(IntPtr hWnd, Standard.WM Msg, IntPtr wParam, IntPtr lParam);

	[DllImport("gdi32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool DeleteObject(IntPtr hObject);

	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool DestroyIcon(IntPtr handle);

	[DllImport("user32.dll", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool DestroyWindow(IntPtr hwnd);

	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool IsWindow(IntPtr hwnd);

	[DllImport("dwmapi.dll", PreserveSig = false)]
	public static extern void DwmExtendFrameIntoClientArea(IntPtr hwnd, ref Standard.MARGINS pMarInset);

	[DllImport("dwmapi.dll", EntryPoint = "DwmIsCompositionEnabled", PreserveSig = false)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool _DwmIsCompositionEnabled();

	[DllImport("dwmapi.dll", EntryPoint = "DwmGetColorizationColor")]
	private static extern Standard.HRESULT _DwmGetColorizationColor(out uint pcrColorization, [MarshalAs(UnmanagedType.Bool)] out bool pfOpaqueBlend);

	public static bool DwmGetColorizationColor(out uint pcrColorization, out bool pfOpaqueBlend)
	{
		if (Standard.Utility.IsOSVistaOrNewer && IsThemeActive() && _DwmGetColorizationColor(out pcrColorization, out pfOpaqueBlend).Succeeded)
		{
			return true;
		}
		pcrColorization = 4278190080u;
		pfOpaqueBlend = true;
		return false;
	}

	public static bool DwmIsCompositionEnabled()
	{
		if (!Standard.Utility.IsOSVistaOrNewer)
		{
			return false;
		}
		return _DwmIsCompositionEnabled();
	}

	[DllImport("dwmapi.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool DwmDefWindowProc(IntPtr hwnd, Standard.WM msg, IntPtr wParam, IntPtr lParam, out IntPtr plResult);

	[DllImport("dwmapi.dll", EntryPoint = "DwmSetWindowAttribute")]
	private static extern void _DwmSetWindowAttribute(IntPtr hwnd, Standard.DWMWA dwAttribute, ref int pvAttribute, int cbAttribute);

	public static void DwmSetWindowAttributeFlip3DPolicy(IntPtr hwnd, Standard.DWMFLIP3D flip3dPolicy)
	{
		Standard.Assert.IsTrue(Standard.Utility.IsOSVistaOrNewer);
		int pvAttribute = (int)flip3dPolicy;
		_DwmSetWindowAttribute(hwnd, Standard.DWMWA.FLIP3D_POLICY, ref pvAttribute, 4);
	}

	public static void DwmSetWindowAttributeDisallowPeek(IntPtr hwnd, bool disallowPeek)
	{
		Standard.Assert.IsTrue(Standard.Utility.IsOSWindows7OrNewer);
		int pvAttribute = (disallowPeek ? 1 : 0);
		_DwmSetWindowAttribute(hwnd, Standard.DWMWA.DISALLOW_PEEK, ref pvAttribute, 4);
	}

	[DllImport("user32.dll", EntryPoint = "EnableMenuItem")]
	private static extern int _EnableMenuItem(IntPtr hMenu, Standard.SC uIDEnableItem, Standard.MF uEnable);

	public static Standard.MF EnableMenuItem(IntPtr hMenu, Standard.SC uIDEnableItem, Standard.MF uEnable)
	{
		return (Standard.MF)_EnableMenuItem(hMenu, uIDEnableItem, uEnable);
	}

	[DllImport("user32.dll", EntryPoint = "RemoveMenu", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool _RemoveMenu(IntPtr hMenu, uint uPosition, uint uFlags);

	public static void RemoveMenu(IntPtr hMenu, Standard.SC uPosition, Standard.MF uFlags)
	{
		if (!_RemoveMenu(hMenu, (uint)uPosition, (uint)uFlags))
		{
			throw new Win32Exception();
		}
	}

	[DllImport("user32.dll", EntryPoint = "DrawMenuBar", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool _DrawMenuBar(IntPtr hWnd);

	public static void DrawMenuBar(IntPtr hWnd)
	{
		if (!_DrawMenuBar(hWnd))
		{
			throw new Win32Exception();
		}
	}

	[DllImport("kernel32.dll")]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool FindClose(IntPtr handle);

	[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
	public static extern Standard.SafeFindHandle FindFirstFileW(string lpFileName, [In][Out][MarshalAs(UnmanagedType.LPStruct)] Standard.WIN32_FIND_DATAW lpFindFileData);

	[DllImport("kernel32.dll", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool FindNextFileW(Standard.SafeFindHandle hndFindFile, [In][Out][MarshalAs(UnmanagedType.LPStruct)] Standard.WIN32_FIND_DATAW lpFindFileData);

	[DllImport("user32.dll", EntryPoint = "GetClientRect", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool _GetClientRect(IntPtr hwnd, out Standard.RECT lpRect);

	public static Standard.RECT GetClientRect(IntPtr hwnd)
	{
		if (!_GetClientRect(hwnd, out var lpRect))
		{
			Standard.HRESULT.ThrowLastError();
		}
		return lpRect;
	}

	[DllImport("uxtheme.dll", CharSet = CharSet.Unicode, EntryPoint = "GetCurrentThemeName")]
	private static extern Standard.HRESULT _GetCurrentThemeName(StringBuilder pszThemeFileName, int dwMaxNameChars, StringBuilder pszColorBuff, int cchMaxColorChars, StringBuilder pszSizeBuff, int cchMaxSizeChars);

	public static void GetCurrentThemeName(out string themeFileName, out string color, out string size)
	{
		StringBuilder stringBuilder = new StringBuilder(260);
		StringBuilder stringBuilder2 = new StringBuilder(260);
		StringBuilder stringBuilder3 = new StringBuilder(260);
		_GetCurrentThemeName(stringBuilder, stringBuilder.Capacity, stringBuilder2, stringBuilder2.Capacity, stringBuilder3, stringBuilder3.Capacity).ThrowIfFailed();
		themeFileName = stringBuilder.ToString();
		color = stringBuilder2.ToString();
		size = stringBuilder3.ToString();
	}

	[DllImport("uxtheme.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool IsThemeActive();

	[Obsolete("Use SafeDC.GetDC instead.", true)]
	public static void GetDC()
	{
	}

	[DllImport("gdi32.dll")]
	public static extern int GetDeviceCaps(Standard.SafeDC hdc, Standard.DeviceCap nIndex);

	[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetModuleFileName", SetLastError = true)]
	private static extern int _GetModuleFileName(IntPtr hModule, StringBuilder lpFilename, int nSize);

	public static string GetModuleFileName(IntPtr hModule)
	{
		StringBuilder stringBuilder = new StringBuilder(260);
		while (true)
		{
			int num = _GetModuleFileName(hModule, stringBuilder, stringBuilder.Capacity);
			if (num == 0)
			{
				Standard.HRESULT.ThrowLastError();
			}
			if (num != stringBuilder.Capacity)
			{
				break;
			}
			stringBuilder.EnsureCapacity(stringBuilder.Capacity * 2);
		}
		return stringBuilder.ToString();
	}

	[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetModuleHandleW", SetLastError = true)]
	private static extern IntPtr _GetModuleHandle([MarshalAs(UnmanagedType.LPWStr)] string lpModuleName);

	public static IntPtr GetModuleHandle(string lpModuleName)
	{
		IntPtr intPtr = _GetModuleHandle(lpModuleName);
		if (intPtr == IntPtr.Zero)
		{
			Standard.HRESULT.ThrowLastError();
		}
		return intPtr;
	}

	[DllImport("user32.dll", EntryPoint = "GetMonitorInfo", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool _GetMonitorInfo(IntPtr hMonitor, [In][Out] Standard.MONITORINFO lpmi);

	public static Standard.MONITORINFO GetMonitorInfo(IntPtr hMonitor)
	{
		Standard.MONITORINFO mONITORINFO = new Standard.MONITORINFO();
		if (!_GetMonitorInfo(hMonitor, mONITORINFO))
		{
			throw new Win32Exception();
		}
		return mONITORINFO;
	}

	[DllImport("gdi32.dll", EntryPoint = "GetStockObject", SetLastError = true)]
	private static extern IntPtr _GetStockObject(Standard.StockObject fnObject);

	public static IntPtr GetStockObject(Standard.StockObject fnObject)
	{
		IntPtr result = _GetStockObject(fnObject);
		bool flag = false;
		return result;
	}

	[DllImport("user32.dll")]
	public static extern IntPtr GetSystemMenu(IntPtr hWnd, [MarshalAs(UnmanagedType.Bool)] bool bRevert);

	[DllImport("user32.dll")]
	public static extern int GetSystemMetrics(Standard.SM nIndex);

	public static IntPtr GetWindowLongPtr(IntPtr hwnd, Standard.GWL nIndex)
	{
		IntPtr zero = IntPtr.Zero;
		zero = ((8 != IntPtr.Size) ? new IntPtr(GetWindowLongPtr32(hwnd, nIndex)) : GetWindowLongPtr64(hwnd, nIndex));
		if (IntPtr.Zero == zero)
		{
			throw new Win32Exception();
		}
		return zero;
	}

	[DllImport("uxtheme.dll", PreserveSig = false)]
	public static extern void SetWindowThemeAttribute([In] IntPtr hwnd, [In] Standard.WINDOWTHEMEATTRIBUTETYPE eAttribute, [In] ref Standard.WTA_OPTIONS pvAttribute, [In] uint cbAttribute);

	[DllImport("user32.dll", EntryPoint = "GetWindowLong", SetLastError = true)]
	private static extern int GetWindowLongPtr32(IntPtr hWnd, Standard.GWL nIndex);

	[DllImport("user32.dll", EntryPoint = "GetWindowLongPtr", SetLastError = true)]
	private static extern IntPtr GetWindowLongPtr64(IntPtr hWnd, Standard.GWL nIndex);

	[DllImport("user32.dll", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool GetWindowPlacement(IntPtr hwnd, Standard.WINDOWPLACEMENT lpwndpl);

	public static Standard.WINDOWPLACEMENT GetWindowPlacement(IntPtr hwnd)
	{
		Standard.WINDOWPLACEMENT wINDOWPLACEMENT = new Standard.WINDOWPLACEMENT();
		if (GetWindowPlacement(hwnd, wINDOWPLACEMENT))
		{
			return wINDOWPLACEMENT;
		}
		throw new Win32Exception();
	}

	[DllImport("user32.dll", EntryPoint = "GetWindowRect", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool _GetWindowRect(IntPtr hWnd, out Standard.RECT lpRect);

	public static Standard.RECT GetWindowRect(IntPtr hwnd)
	{
		if (!_GetWindowRect(hwnd, out var lpRect))
		{
			Standard.HRESULT.ThrowLastError();
		}
		return lpRect;
	}

	[DllImport("gdiplus.dll")]
	public static extern Standard.Status GdipCreateBitmapFromStream(IStream stream, out IntPtr bitmap);

	[DllImport("gdiplus.dll")]
	public static extern Standard.Status GdipCreateHBITMAPFromBitmap(IntPtr bitmap, out IntPtr hbmReturn, int background);

	[DllImport("gdiplus.dll")]
	public static extern Standard.Status GdipCreateHICONFromBitmap(IntPtr bitmap, out IntPtr hbmReturn);

	[DllImport("gdiplus.dll")]
	public static extern Standard.Status GdipDisposeImage(IntPtr image);

	[DllImport("gdiplus.dll")]
	public static extern Standard.Status GdipImageForceValidation(IntPtr image);

	[DllImport("gdiplus.dll")]
	public static extern Standard.Status GdiplusStartup(out IntPtr token, Standard.StartupInput input, out Standard.StartupOutput output);

	[DllImport("gdiplus.dll")]
	public static extern Standard.Status GdiplusShutdown(IntPtr token);

	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool IsWindowVisible(IntPtr hwnd);

	[DllImport("kernel32.dll", EntryPoint = "LocalFree", SetLastError = true)]
	private static extern IntPtr _LocalFree(IntPtr hMem);

	[DllImport("user32.dll")]
	public static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);

	[DllImport("user32.dll", EntryPoint = "PostMessage", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool _PostMessage(IntPtr hWnd, Standard.WM Msg, IntPtr wParam, IntPtr lParam);

	public static void PostMessage(IntPtr hWnd, Standard.WM Msg, IntPtr wParam, IntPtr lParam)
	{
		if (!_PostMessage(hWnd, Msg, wParam, lParam))
		{
			throw new Win32Exception();
		}
	}

	[DllImport("user32.dll", EntryPoint = "RegisterClassExW", SetLastError = true)]
	private static extern short _RegisterClassEx([In] ref Standard.WNDCLASSEX lpwcx);

	public static short RegisterClassEx(ref Standard.WNDCLASSEX lpwcx)
	{
		short num = _RegisterClassEx(ref lpwcx);
		if (num == 0)
		{
			Standard.HRESULT.ThrowLastError();
		}
		return num;
	}

	[DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "RegisterWindowMessage", SetLastError = true)]
	private static extern uint _RegisterWindowMessage([MarshalAs(UnmanagedType.LPWStr)] string lpString);

	public static Standard.WM RegisterWindowMessage(string lpString)
	{
		uint num = _RegisterWindowMessage(lpString);
		if (num == 0)
		{
			Standard.HRESULT.ThrowLastError();
		}
		return (Standard.WM)num;
	}

	[DllImport("user32.dll", EntryPoint = "SetActiveWindow", SetLastError = true)]
	private static extern IntPtr _SetActiveWindow(IntPtr hWnd);

	public static IntPtr SetActiveWindow(IntPtr hwnd)
	{
		Standard.Verify.IsNotDefault(hwnd, "hwnd");
		IntPtr intPtr = _SetActiveWindow(hwnd);
		if (intPtr == IntPtr.Zero)
		{
			Standard.HRESULT.ThrowLastError();
		}
		return intPtr;
	}

	public static IntPtr SetClassLongPtr(IntPtr hwnd, Standard.GCLP nIndex, IntPtr dwNewLong)
	{
		if (8 == IntPtr.Size)
		{
			return SetClassLongPtr64(hwnd, nIndex, dwNewLong);
		}
		return new IntPtr(SetClassLongPtr32(hwnd, nIndex, dwNewLong.ToInt32()));
	}

	[DllImport("user32.dll", EntryPoint = "SetClassLong", SetLastError = true)]
	private static extern int SetClassLongPtr32(IntPtr hWnd, Standard.GCLP nIndex, int dwNewLong);

	[DllImport("user32.dll", EntryPoint = "SetClassLongPtr", SetLastError = true)]
	private static extern IntPtr SetClassLongPtr64(IntPtr hWnd, Standard.GCLP nIndex, IntPtr dwNewLong);

	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern Standard.ErrorModes SetErrorMode(Standard.ErrorModes newMode);

	[DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool _SetProcessWorkingSetSize(IntPtr hProcess, IntPtr dwMinimiumWorkingSetSize, IntPtr dwMaximumWorkingSetSize);

	public static void SetProcessWorkingSetSize(IntPtr hProcess, int dwMinimumWorkingSetSize, int dwMaximumWorkingSetSize)
	{
		if (!_SetProcessWorkingSetSize(hProcess, new IntPtr(dwMinimumWorkingSetSize), new IntPtr(dwMaximumWorkingSetSize)))
		{
			throw new Win32Exception();
		}
	}

	public static IntPtr SetWindowLongPtr(IntPtr hwnd, Standard.GWL nIndex, IntPtr dwNewLong)
	{
		if (8 == IntPtr.Size)
		{
			return SetWindowLongPtr64(hwnd, nIndex, dwNewLong);
		}
		return new IntPtr(SetWindowLongPtr32(hwnd, nIndex, dwNewLong.ToInt32()));
	}

	[DllImport("user32.dll", EntryPoint = "SetWindowLong", SetLastError = true)]
	private static extern int SetWindowLongPtr32(IntPtr hWnd, Standard.GWL nIndex, int dwNewLong);

	[DllImport("user32.dll", EntryPoint = "SetWindowLongPtr", SetLastError = true)]
	private static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, Standard.GWL nIndex, IntPtr dwNewLong);

	[DllImport("user32.dll", EntryPoint = "SetWindowRgn", SetLastError = true)]
	private static extern int _SetWindowRgn(IntPtr hWnd, IntPtr hRgn, [MarshalAs(UnmanagedType.Bool)] bool bRedraw);

	public static void SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool bRedraw)
	{
		if (_SetWindowRgn(hWnd, hRgn, bRedraw) == 0)
		{
			throw new Win32Exception();
		}
	}

	[DllImport("user32.dll", EntryPoint = "SetWindowPos", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool _SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, Standard.SWP uFlags);

	public static bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, Standard.SWP uFlags)
	{
		if (!_SetWindowPos(hWnd, hWndInsertAfter, x, y, cx, cy, uFlags))
		{
			return false;
		}
		return true;
	}

	[DllImport("shell32.dll")]
	public static extern Standard.Win32Error SHFileOperation(ref Standard.SHFILEOPSTRUCT lpFileOp);

	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool ShowWindow(IntPtr hwnd, Standard.SW nCmdShow);

	[DllImport("user32.dll", EntryPoint = "SystemParametersInfoW", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool _SystemParametersInfo_String(Standard.SPI uiAction, int uiParam, [MarshalAs(UnmanagedType.LPWStr)] string pvParam, Standard.SPIF fWinIni);

	[DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "SystemParametersInfoW", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool _SystemParametersInfo_NONCLIENTMETRICS(Standard.SPI uiAction, int uiParam, [In][Out] ref Standard.NONCLIENTMETRICS pvParam, Standard.SPIF fWinIni);

	[DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "SystemParametersInfoW", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool _SystemParametersInfo_HIGHCONTRAST(Standard.SPI uiAction, int uiParam, [In][Out] ref Standard.HIGHCONTRAST pvParam, Standard.SPIF fWinIni);

	public static void SystemParametersInfo(Standard.SPI uiAction, int uiParam, string pvParam, Standard.SPIF fWinIni)
	{
		if (!_SystemParametersInfo_String(uiAction, uiParam, pvParam, fWinIni))
		{
			Standard.HRESULT.ThrowLastError();
		}
	}

	public static Standard.NONCLIENTMETRICS SystemParameterInfo_GetNONCLIENTMETRICS()
	{
		Standard.NONCLIENTMETRICS pvParam = (Standard.Utility.IsOSVistaOrNewer ? Standard.NONCLIENTMETRICS.VistaMetricsStruct : Standard.NONCLIENTMETRICS.XPMetricsStruct);
		if (!_SystemParametersInfo_NONCLIENTMETRICS(Standard.SPI.GETNONCLIENTMETRICS, pvParam.cbSize, ref pvParam, Standard.SPIF.None))
		{
			Standard.HRESULT.ThrowLastError();
		}
		return pvParam;
	}

	public static Standard.HIGHCONTRAST SystemParameterInfo_GetHIGHCONTRAST()
	{
		Standard.HIGHCONTRAST pvParam = new Standard.HIGHCONTRAST
		{
			cbSize = Marshal.SizeOf(typeof(Standard.HIGHCONTRAST))
		};
		if (!_SystemParametersInfo_HIGHCONTRAST(Standard.SPI.GETHIGHCONTRAST, pvParam.cbSize, ref pvParam, Standard.SPIF.None))
		{
			Standard.HRESULT.ThrowLastError();
		}
		return pvParam;
	}

	[DllImport("user32.dll")]
	public static extern uint TrackPopupMenuEx(IntPtr hmenu, uint fuFlags, int x, int y, IntPtr hwnd, IntPtr lptpm);

	[DllImport("gdi32.dll", EntryPoint = "SelectObject", SetLastError = true)]
	private static extern IntPtr _SelectObject(Standard.SafeDC hdc, IntPtr hgdiobj);

	public static IntPtr SelectObject(Standard.SafeDC hdc, IntPtr hgdiobj)
	{
		IntPtr intPtr = _SelectObject(hdc, hgdiobj);
		if (intPtr == IntPtr.Zero)
		{
			Standard.HRESULT.ThrowLastError();
		}
		return intPtr;
	}

	[DllImport("gdi32.dll", EntryPoint = "SelectObject", SetLastError = true)]
	private static extern IntPtr _SelectObjectSafeHBITMAP(Standard.SafeDC hdc, Standard.SafeHBITMAP hgdiobj);

	public static IntPtr SelectObject(Standard.SafeDC hdc, Standard.SafeHBITMAP hgdiobj)
	{
		IntPtr intPtr = _SelectObjectSafeHBITMAP(hdc, hgdiobj);
		if (intPtr == IntPtr.Zero)
		{
			Standard.HRESULT.ThrowLastError();
		}
		return intPtr;
	}

	[DllImport("user32.dll", SetLastError = true)]
	public static extern int SendInput(int nInputs, ref Standard.INPUT pInputs, int cbSize);

	[DllImport("user32.dll", SetLastError = true)]
	public static extern IntPtr SendMessage(IntPtr hWnd, Standard.WM Msg, IntPtr wParam, IntPtr lParam);

	[DllImport("user32.dll", EntryPoint = "UnregisterClass", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool _UnregisterClassAtom(IntPtr lpClassName, IntPtr hInstance);

	[DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "UnregisterClass", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool _UnregisterClassName(string lpClassName, IntPtr hInstance);

	public static void UnregisterClass(short atom, IntPtr hinstance)
	{
		if (!_UnregisterClassAtom(new IntPtr(atom), hinstance))
		{
			Standard.HRESULT.ThrowLastError();
		}
	}

	public static void UnregisterClass(string lpClassName, IntPtr hInstance)
	{
		if (!_UnregisterClassName(lpClassName, hInstance))
		{
			Standard.HRESULT.ThrowLastError();
		}
	}

	[DllImport("user32.dll", EntryPoint = "UpdateLayeredWindow", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool _UpdateLayeredWindow(IntPtr hwnd, Standard.SafeDC hdcDst, [In] ref Standard.POINT pptDst, [In] ref Standard.SIZE psize, Standard.SafeDC hdcSrc, [In] ref Standard.POINT pptSrc, int crKey, ref Standard.BLENDFUNCTION pblend, Standard.ULW dwFlags);

	[DllImport("user32.dll", EntryPoint = "UpdateLayeredWindow", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool _UpdateLayeredWindowIntPtr(IntPtr hwnd, IntPtr hdcDst, IntPtr pptDst, IntPtr psize, IntPtr hdcSrc, IntPtr pptSrc, int crKey, ref Standard.BLENDFUNCTION pblend, Standard.ULW dwFlags);

	public static void UpdateLayeredWindow(IntPtr hwnd, Standard.SafeDC hdcDst, ref Standard.POINT pptDst, ref Standard.SIZE psize, Standard.SafeDC hdcSrc, ref Standard.POINT pptSrc, int crKey, ref Standard.BLENDFUNCTION pblend, Standard.ULW dwFlags)
	{
		if (!_UpdateLayeredWindow(hwnd, hdcDst, ref pptDst, ref psize, hdcSrc, ref pptSrc, crKey, ref pblend, dwFlags))
		{
			Standard.HRESULT.ThrowLastError();
		}
	}

	public static void UpdateLayeredWindow(IntPtr hwnd, int crKey, ref Standard.BLENDFUNCTION pblend, Standard.ULW dwFlags)
	{
		if (!_UpdateLayeredWindowIntPtr(hwnd, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, crKey, ref pblend, dwFlags))
		{
			Standard.HRESULT.ThrowLastError();
		}
	}

	[DllImport("shell32.dll", EntryPoint = "SHAddToRecentDocs")]
	private static extern void _SHAddToRecentDocs_String(Standard.SHARD uFlags, [MarshalAs(UnmanagedType.LPWStr)] string pv);

	[DllImport("shell32.dll", EntryPoint = "SHAddToRecentDocs")]
	private static extern void _SHAddToRecentDocs_ShellLink(Standard.SHARD uFlags, Standard.IShellLinkW pv);

	public static void SHAddToRecentDocs(string path)
	{
		_SHAddToRecentDocs_String(Standard.SHARD.PATHW, path);
	}

	public static void SHAddToRecentDocs(Standard.IShellLinkW shellLink)
	{
		_SHAddToRecentDocs_ShellLink(Standard.SHARD.LINK, shellLink);
	}

	[DllImport("dwmapi.dll", EntryPoint = "DwmGetCompositionTimingInfo")]
	private static extern Standard.HRESULT _DwmGetCompositionTimingInfo(IntPtr hwnd, ref Standard.DWM_TIMING_INFO pTimingInfo);

	public static Standard.DWM_TIMING_INFO? DwmGetCompositionTimingInfo(IntPtr hwnd)
	{
		if (!Standard.Utility.IsOSVistaOrNewer)
		{
			return null;
		}
		Standard.DWM_TIMING_INFO pTimingInfo = new Standard.DWM_TIMING_INFO
		{
			cbSize = Marshal.SizeOf(typeof(Standard.DWM_TIMING_INFO))
		};
		Standard.HRESULT hRESULT = _DwmGetCompositionTimingInfo(hwnd, ref pTimingInfo);
		if (hRESULT == Standard.HRESULT.E_PENDING)
		{
			return null;
		}
		hRESULT.ThrowIfFailed();
		return pTimingInfo;
	}

	[DllImport("dwmapi.dll", PreserveSig = false)]
	public static extern void DwmInvalidateIconicBitmaps(IntPtr hwnd);

	[DllImport("dwmapi.dll", PreserveSig = false)]
	public static extern void DwmSetIconicThumbnail(IntPtr hwnd, IntPtr hbmp, Standard.DWM_SIT dwSITFlags);

	[DllImport("dwmapi.dll", PreserveSig = false)]
	public static extern void DwmSetIconicLivePreviewBitmap(IntPtr hwnd, IntPtr hbmp, Standard.RefPOINT pptClient, Standard.DWM_SIT dwSITFlags);

	[DllImport("shell32.dll", PreserveSig = false)]
	public static extern void SHGetItemFromDataObject(IDataObject pdtobj, Standard.DOGIF dwFlags, [In] ref Guid riid, [MarshalAs(UnmanagedType.Interface)] out object ppv);

	[DllImport("shell32.dll", PreserveSig = false)]
	public static extern Standard.HRESULT SHCreateItemFromParsingName([MarshalAs(UnmanagedType.LPWStr)] string pszPath, IBindCtx pbc, [In] ref Guid riid, [MarshalAs(UnmanagedType.Interface)] out object ppv);

	[DllImport("shell32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool Shell_NotifyIcon(Standard.NIM dwMessage, [In] Standard.NOTIFYICONDATA lpdata);

	[DllImport("shell32.dll", PreserveSig = false)]
	public static extern void SetCurrentProcessExplicitAppUserModelID([MarshalAs(UnmanagedType.LPWStr)] string AppID);

	[DllImport("shell32.dll")]
	public static extern Standard.HRESULT GetCurrentProcessExplicitAppUserModelID([MarshalAs(UnmanagedType.LPWStr)] out string AppID);
}
