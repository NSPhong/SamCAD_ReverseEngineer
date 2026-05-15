#define DEBUG
using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Standard;

internal sealed class SafeDC : SafeHandleZeroOrMinusOneIsInvalid
{
	private static class NativeMethods
	{
		[DllImport("user32.dll")]
		public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

		[DllImport("user32.dll")]
		public static extern Standard.SafeDC GetDC(IntPtr hwnd);

		[DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
		public static extern Standard.SafeDC CreateDC([MarshalAs(UnmanagedType.LPWStr)] string lpszDriver, [MarshalAs(UnmanagedType.LPWStr)] string lpszDevice, IntPtr lpszOutput, IntPtr lpInitData);

		[DllImport("gdi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern Standard.SafeDC CreateCompatibleDC(IntPtr hdc);

		[DllImport("gdi32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool DeleteDC(IntPtr hdc);
	}

	private IntPtr? _hwnd;

	private bool _created;

	public IntPtr Hwnd
	{
		set
		{
			Standard.Assert.NullableIsNull(_hwnd);
			_hwnd = value;
		}
	}

	private SafeDC()
		: base(ownsHandle: true)
	{
	}

	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	protected override bool ReleaseHandle()
	{
		if (_created)
		{
			return NativeMethods.DeleteDC(handle);
		}
		if (!_hwnd.HasValue || _hwnd.Value == IntPtr.Zero)
		{
			return true;
		}
		return NativeMethods.ReleaseDC(_hwnd.Value, handle) == 1;
	}

	public static Standard.SafeDC CreateDC(string deviceName)
	{
		Standard.SafeDC safeDC = null;
		try
		{
			safeDC = NativeMethods.CreateDC(deviceName, null, IntPtr.Zero, IntPtr.Zero);
		}
		finally
		{
			if (safeDC != null)
			{
				safeDC._created = true;
			}
		}
		if (safeDC.IsInvalid)
		{
			safeDC.Dispose();
			throw new SystemException("Unable to create a device context from the specified device information.");
		}
		return safeDC;
	}

	public static Standard.SafeDC CreateCompatibleDC(Standard.SafeDC hdc)
	{
		Standard.SafeDC safeDC = null;
		try
		{
			IntPtr zero = IntPtr.Zero;
			if (hdc != null)
			{
				zero = hdc.handle;
			}
			safeDC = NativeMethods.CreateCompatibleDC(zero);
			if (safeDC == null)
			{
				Standard.HRESULT.ThrowLastError();
			}
		}
		finally
		{
			if (safeDC != null)
			{
				safeDC._created = true;
			}
		}
		if (safeDC.IsInvalid)
		{
			safeDC.Dispose();
			throw new SystemException("Unable to create a device context from the specified device information.");
		}
		return safeDC;
	}

	public static Standard.SafeDC GetDC(IntPtr hwnd)
	{
		Standard.SafeDC safeDC = null;
		try
		{
			safeDC = NativeMethods.GetDC(hwnd);
		}
		finally
		{
			if (safeDC != null)
			{
				safeDC.Hwnd = hwnd;
			}
		}
		if (safeDC.IsInvalid)
		{
			Standard.HRESULT.E_FAIL.ThrowIfFailed();
		}
		return safeDC;
	}

	public static Standard.SafeDC GetDesktop()
	{
		return GetDC(IntPtr.Zero);
	}

	public static Standard.SafeDC WrapDC(IntPtr hdc)
	{
		return new Standard.SafeDC
		{
			handle = hdc,
			_created = false,
			_hwnd = IntPtr.Zero
		};
	}
}
