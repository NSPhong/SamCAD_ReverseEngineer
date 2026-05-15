#define DEBUG
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using Standard;

namespace Microsoft.Windows.Shell;

public class SystemParameters2 : INotifyPropertyChanged
{
	private delegate void _SystemMetricUpdate(IntPtr wParam, IntPtr lParam);

	[ThreadStatic]
	private static SystemParameters2 _threadLocalSingleton;

	private Standard.MessageWindow _messageHwnd;

	private bool _isGlassEnabled;

	private Color _glassColor;

	private SolidColorBrush _glassColorBrush;

	private Thickness _windowResizeBorderThickness;

	private Thickness _windowNonClientFrameThickness;

	private double _captionHeight;

	private Size _smallIconSize;

	private string _uxThemeName;

	private string _uxThemeColor;

	private bool _isHighContrast;

	private CornerRadius _windowCornerRadius;

	private Rect _captionButtonLocation;

	private readonly Dictionary<Standard.WM, List<_SystemMetricUpdate>> _UpdateTable;

	public static SystemParameters2 Current
	{
		get
		{
			if (_threadLocalSingleton == null)
			{
				_threadLocalSingleton = new SystemParameters2();
			}
			return _threadLocalSingleton;
		}
	}

	public bool IsGlassEnabled
	{
		get
		{
			return Standard.NativeMethods.DwmIsCompositionEnabled();
		}
		private set
		{
			if (value != _isGlassEnabled)
			{
				_isGlassEnabled = value;
				_NotifyPropertyChanged("IsGlassEnabled");
			}
		}
	}

	public Color WindowGlassColor
	{
		get
		{
			return _glassColor;
		}
		private set
		{
			if (value != _glassColor)
			{
				_glassColor = value;
				_NotifyPropertyChanged("WindowGlassColor");
			}
		}
	}

	public SolidColorBrush WindowGlassBrush
	{
		get
		{
			return _glassColorBrush;
		}
		private set
		{
			Standard.Assert.IsNotNull(value);
			Standard.Assert.IsTrue(value.IsFrozen);
			if (_glassColorBrush == null || value.Color != _glassColorBrush.Color)
			{
				_glassColorBrush = value;
				_NotifyPropertyChanged("WindowGlassBrush");
			}
		}
	}

	public Thickness WindowResizeBorderThickness
	{
		get
		{
			return _windowResizeBorderThickness;
		}
		private set
		{
			if (value != _windowResizeBorderThickness)
			{
				_windowResizeBorderThickness = value;
				_NotifyPropertyChanged("WindowResizeBorderThickness");
			}
		}
	}

	public Thickness WindowNonClientFrameThickness
	{
		get
		{
			return _windowNonClientFrameThickness;
		}
		private set
		{
			if (value != _windowNonClientFrameThickness)
			{
				_windowNonClientFrameThickness = value;
				_NotifyPropertyChanged("WindowNonClientFrameThickness");
			}
		}
	}

	public double WindowCaptionHeight
	{
		get
		{
			return _captionHeight;
		}
		private set
		{
			if (value != _captionHeight)
			{
				_captionHeight = value;
				_NotifyPropertyChanged("WindowCaptionHeight");
			}
		}
	}

	public Size SmallIconSize
	{
		get
		{
			return new Size(_smallIconSize.Width, _smallIconSize.Height);
		}
		private set
		{
			if (value != _smallIconSize)
			{
				_smallIconSize = value;
				_NotifyPropertyChanged("SmallIconSize");
			}
		}
	}

	public string UxThemeName
	{
		get
		{
			return _uxThemeName;
		}
		private set
		{
			if (value != _uxThemeName)
			{
				_uxThemeName = value;
				_NotifyPropertyChanged("UxThemeName");
			}
		}
	}

	public string UxThemeColor
	{
		get
		{
			return _uxThemeColor;
		}
		private set
		{
			if (value != _uxThemeColor)
			{
				_uxThemeColor = value;
				_NotifyPropertyChanged("UxThemeColor");
			}
		}
	}

	public bool HighContrast
	{
		get
		{
			return _isHighContrast;
		}
		private set
		{
			if (value != _isHighContrast)
			{
				_isHighContrast = value;
				_NotifyPropertyChanged("HighContrast");
			}
		}
	}

	public CornerRadius WindowCornerRadius
	{
		get
		{
			return _windowCornerRadius;
		}
		private set
		{
			if (value != _windowCornerRadius)
			{
				_windowCornerRadius = value;
				_NotifyPropertyChanged("WindowCornerRadius");
			}
		}
	}

	public Rect WindowCaptionButtonsLocation
	{
		get
		{
			return _captionButtonLocation;
		}
		private set
		{
			if (value != _captionButtonLocation)
			{
				_captionButtonLocation = value;
				_NotifyPropertyChanged("WindowCaptionButtonsLocation");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	private void _InitializeIsGlassEnabled()
	{
		IsGlassEnabled = Standard.NativeMethods.DwmIsCompositionEnabled();
	}

	private void _UpdateIsGlassEnabled(IntPtr wParam, IntPtr lParam)
	{
		_InitializeIsGlassEnabled();
	}

	private void _InitializeGlassColor()
	{
		Standard.NativeMethods.DwmGetColorizationColor(out var pcrColorization, out var pfOpaqueBlend);
		pcrColorization |= (uint)(pfOpaqueBlend ? (-16777216) : 0);
		WindowGlassColor = Standard.Utility.ColorFromArgbDword(pcrColorization);
		SolidColorBrush solidColorBrush = new SolidColorBrush(WindowGlassColor);
		solidColorBrush.Freeze();
		WindowGlassBrush = solidColorBrush;
	}

	private void _UpdateGlassColor(IntPtr wParam, IntPtr lParam)
	{
		bool flag = lParam != IntPtr.Zero;
		uint num = (uint)wParam.ToInt64();
		num |= (uint)(flag ? (-16777216) : 0);
		WindowGlassColor = Standard.Utility.ColorFromArgbDword(num);
		SolidColorBrush solidColorBrush = new SolidColorBrush(WindowGlassColor);
		solidColorBrush.Freeze();
		WindowGlassBrush = solidColorBrush;
	}

	private void _InitializeCaptionHeight()
	{
		Point devicePoint = new Point(0.0, Standard.NativeMethods.GetSystemMetrics(Standard.SM.CYCAPTION));
		WindowCaptionHeight = Standard.DpiHelper.DevicePixelsToLogical(devicePoint).Y;
	}

	private void _UpdateCaptionHeight(IntPtr wParam, IntPtr lParam)
	{
		_InitializeCaptionHeight();
	}

	private void _InitializeWindowResizeBorderThickness()
	{
		Size deviceSize = new Size(Standard.NativeMethods.GetSystemMetrics(Standard.SM.CXFRAME), Standard.NativeMethods.GetSystemMetrics(Standard.SM.CYFRAME));
		Size size = Standard.DpiHelper.DeviceSizeToLogical(deviceSize);
		WindowResizeBorderThickness = new Thickness(size.Width, size.Height, size.Width, size.Height);
	}

	private void _UpdateWindowResizeBorderThickness(IntPtr wParam, IntPtr lParam)
	{
		_InitializeWindowResizeBorderThickness();
	}

	private void _InitializeWindowNonClientFrameThickness()
	{
		Size deviceSize = new Size(Standard.NativeMethods.GetSystemMetrics(Standard.SM.CXFRAME), Standard.NativeMethods.GetSystemMetrics(Standard.SM.CYFRAME));
		Size size = Standard.DpiHelper.DeviceSizeToLogical(deviceSize);
		int systemMetrics = Standard.NativeMethods.GetSystemMetrics(Standard.SM.CYCAPTION);
		double y = Standard.DpiHelper.DevicePixelsToLogical(new Point(0.0, systemMetrics)).Y;
		WindowNonClientFrameThickness = new Thickness(size.Width, size.Height + y, size.Width, size.Height);
	}

	private void _UpdateWindowNonClientFrameThickness(IntPtr wParam, IntPtr lParam)
	{
		_InitializeWindowNonClientFrameThickness();
	}

	private void _InitializeSmallIconSize()
	{
		SmallIconSize = new Size(Standard.NativeMethods.GetSystemMetrics(Standard.SM.CXSMICON), Standard.NativeMethods.GetSystemMetrics(Standard.SM.CYSMICON));
	}

	private void _UpdateSmallIconSize(IntPtr wParam, IntPtr lParam)
	{
		_InitializeSmallIconSize();
	}

	private void _LegacyInitializeCaptionButtonLocation()
	{
		int systemMetrics = Standard.NativeMethods.GetSystemMetrics(Standard.SM.CXSIZE);
		int systemMetrics2 = Standard.NativeMethods.GetSystemMetrics(Standard.SM.CYSIZE);
		int num = Standard.NativeMethods.GetSystemMetrics(Standard.SM.CXFRAME) + Standard.NativeMethods.GetSystemMetrics(Standard.SM.CXEDGE);
		int num2 = Standard.NativeMethods.GetSystemMetrics(Standard.SM.CYFRAME) + Standard.NativeMethods.GetSystemMetrics(Standard.SM.CYEDGE);
		Rect windowCaptionButtonsLocation = new Rect(0.0, 0.0, systemMetrics * 3, systemMetrics2);
		windowCaptionButtonsLocation.Offset((double)(-num) - windowCaptionButtonsLocation.Width, num2);
		WindowCaptionButtonsLocation = windowCaptionButtonsLocation;
	}

	private void _InitializeCaptionButtonLocation()
	{
		if (!Standard.Utility.IsOSVistaOrNewer || !Standard.NativeMethods.IsThemeActive())
		{
			_LegacyInitializeCaptionButtonLocation();
			return;
		}
		Standard.TITLEBARINFOEX tITLEBARINFOEX = new Standard.TITLEBARINFOEX
		{
			cbSize = Marshal.SizeOf(typeof(Standard.TITLEBARINFOEX))
		};
		IntPtr hglobal = Marshal.AllocHGlobal(tITLEBARINFOEX.cbSize);
		try
		{
			Marshal.StructureToPtr((object)tITLEBARINFOEX, hglobal, false);
			Standard.NativeMethods.ShowWindow(_messageHwnd.Handle, Standard.SW.SHOW);
			Standard.NativeMethods.SendMessage(_messageHwnd.Handle, Standard.WM.GETTITLEBARINFOEX, IntPtr.Zero, hglobal);
			tITLEBARINFOEX = (Standard.TITLEBARINFOEX)Marshal.PtrToStructure(hglobal, typeof(Standard.TITLEBARINFOEX));
		}
		finally
		{
			Standard.NativeMethods.ShowWindow(_messageHwnd.Handle, Standard.SW.HIDE);
			Standard.Utility.SafeFreeHGlobal(ref hglobal);
		}
		Standard.RECT rECT = Standard.RECT.Union(tITLEBARINFOEX.rgrect_CloseButton, tITLEBARINFOEX.rgrect_MinimizeButton);
		Standard.Assert.AreEqual(rECT, Standard.RECT.Union(rECT, tITLEBARINFOEX.rgrect_MaximizeButton));
		Standard.RECT windowRect = Standard.NativeMethods.GetWindowRect(_messageHwnd.Handle);
		Rect deviceRectangle = new Rect(rECT.Left - windowRect.Width - windowRect.Left, rECT.Top - windowRect.Top, rECT.Width, rECT.Height);
		Rect windowCaptionButtonsLocation = Standard.DpiHelper.DeviceRectToLogical(deviceRectangle);
		WindowCaptionButtonsLocation = windowCaptionButtonsLocation;
	}

	private void _UpdateCaptionButtonLocation(IntPtr wParam, IntPtr lParam)
	{
		_InitializeCaptionButtonLocation();
	}

	private void _InitializeHighContrast()
	{
		HighContrast = (Standard.NativeMethods.SystemParameterInfo_GetHIGHCONTRAST().dwFlags & Standard.HCF.HIGHCONTRASTON) != 0;
	}

	private void _UpdateHighContrast(IntPtr wParam, IntPtr lParam)
	{
		_InitializeHighContrast();
	}

	private void _InitializeThemeInfo()
	{
		if (!Standard.NativeMethods.IsThemeActive())
		{
			UxThemeName = "Classic";
			UxThemeColor = "";
		}
		else
		{
			Standard.NativeMethods.GetCurrentThemeName(out var themeFileName, out var color, out var _);
			UxThemeName = Path.GetFileNameWithoutExtension(themeFileName);
			UxThemeColor = color;
		}
	}

	private void _UpdateThemeInfo(IntPtr wParam, IntPtr lParam)
	{
		_InitializeThemeInfo();
	}

	private void _InitializeWindowCornerRadius()
	{
		Standard.Assert.IsNeitherNullNorEmpty(UxThemeName);
		CornerRadius cornerRadius = default(CornerRadius);
		WindowCornerRadius = UxThemeName.ToUpperInvariant() switch
		{
			"LUNA" => new CornerRadius(6.0, 6.0, 0.0, 0.0), 
			"AERO" => (!Standard.NativeMethods.DwmIsCompositionEnabled()) ? new CornerRadius(6.0, 6.0, 0.0, 0.0) : new CornerRadius(8.0), 
			_ => new CornerRadius(0.0), 
		};
	}

	private void _UpdateWindowCornerRadius(IntPtr wParam, IntPtr lParam)
	{
		_InitializeWindowCornerRadius();
	}

	private SystemParameters2()
	{
		_messageHwnd = new Standard.MessageWindow((Standard.CS)0u, Standard.WS.TILEDWINDOW | Standard.WS.DISABLED, Standard.WS_EX.None, new Rect(-16000.0, -16000.0, 100.0, 100.0), "", _WndProc);
		_messageHwnd.Dispatcher.ShutdownStarted += delegate
		{
			Standard.Utility.SafeDispose(ref _messageHwnd);
		};
		_InitializeIsGlassEnabled();
		_InitializeGlassColor();
		_InitializeCaptionHeight();
		_InitializeWindowNonClientFrameThickness();
		_InitializeWindowResizeBorderThickness();
		_InitializeCaptionButtonLocation();
		_InitializeSmallIconSize();
		_InitializeHighContrast();
		_InitializeThemeInfo();
		_InitializeWindowCornerRadius();
		_UpdateTable = new Dictionary<Standard.WM, List<_SystemMetricUpdate>>
		{
			{
				Standard.WM.THEMECHANGED,
				new List<_SystemMetricUpdate> { _UpdateThemeInfo, _UpdateHighContrast, _UpdateWindowCornerRadius, _UpdateCaptionButtonLocation }
			},
			{
				Standard.WM.WININICHANGE,
				new List<_SystemMetricUpdate> { _UpdateCaptionHeight, _UpdateWindowResizeBorderThickness, _UpdateSmallIconSize, _UpdateHighContrast, _UpdateWindowNonClientFrameThickness, _UpdateCaptionButtonLocation }
			},
			{
				Standard.WM.DWMNCRENDERINGCHANGED,
				new List<_SystemMetricUpdate> { _UpdateIsGlassEnabled }
			},
			{
				Standard.WM.DWMCOMPOSITIONCHANGED,
				new List<_SystemMetricUpdate> { _UpdateIsGlassEnabled }
			},
			{
				Standard.WM.DWMCOLORIZATIONCOLORCHANGED,
				new List<_SystemMetricUpdate> { _UpdateGlassColor }
			}
		};
	}

	private IntPtr _WndProc(IntPtr hwnd, Standard.WM msg, IntPtr wParam, IntPtr lParam)
	{
		if (_UpdateTable != null && _UpdateTable.TryGetValue(msg, out var value))
		{
			Standard.Assert.IsNotNull(value);
			foreach (_SystemMetricUpdate item in value)
			{
				item(wParam, lParam);
			}
		}
		return Standard.NativeMethods.DefWindowProc(hwnd, msg, wParam, lParam);
	}

	private void _NotifyPropertyChanged(string propertyName)
	{
		Standard.Assert.IsNeitherNullNorEmpty(propertyName);
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
