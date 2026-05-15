using System;
using System.Runtime.InteropServices;

namespace Standard;

[StructLayout(LayoutKind.Explicit)]
internal struct Win32Error(int i)
{
	[FieldOffset(0)]
	private readonly int _value = i;

	public static readonly Standard.Win32Error ERROR_SUCCESS = new Standard.Win32Error(0);

	public static readonly Standard.Win32Error ERROR_INVALID_FUNCTION = new Standard.Win32Error(1);

	public static readonly Standard.Win32Error ERROR_FILE_NOT_FOUND = new Standard.Win32Error(2);

	public static readonly Standard.Win32Error ERROR_PATH_NOT_FOUND = new Standard.Win32Error(3);

	public static readonly Standard.Win32Error ERROR_TOO_MANY_OPEN_FILES = new Standard.Win32Error(4);

	public static readonly Standard.Win32Error ERROR_ACCESS_DENIED = new Standard.Win32Error(5);

	public static readonly Standard.Win32Error ERROR_INVALID_HANDLE = new Standard.Win32Error(6);

	public static readonly Standard.Win32Error ERROR_OUTOFMEMORY = new Standard.Win32Error(14);

	public static readonly Standard.Win32Error ERROR_NO_MORE_FILES = new Standard.Win32Error(18);

	public static readonly Standard.Win32Error ERROR_SHARING_VIOLATION = new Standard.Win32Error(32);

	public static readonly Standard.Win32Error ERROR_INVALID_PARAMETER = new Standard.Win32Error(87);

	public static readonly Standard.Win32Error ERROR_INSUFFICIENT_BUFFER = new Standard.Win32Error(122);

	public static readonly Standard.Win32Error ERROR_NESTING_NOT_ALLOWED = new Standard.Win32Error(215);

	public static readonly Standard.Win32Error ERROR_KEY_DELETED = new Standard.Win32Error(1018);

	public static readonly Standard.Win32Error ERROR_NOT_FOUND = new Standard.Win32Error(1168);

	public static readonly Standard.Win32Error ERROR_NO_MATCH = new Standard.Win32Error(1169);

	public static readonly Standard.Win32Error ERROR_BAD_DEVICE = new Standard.Win32Error(1200);

	public static readonly Standard.Win32Error ERROR_CANCELLED = new Standard.Win32Error(1223);

	public static readonly Standard.Win32Error ERROR_CLASS_ALREADY_EXISTS = new Standard.Win32Error(1410);

	public static readonly Standard.Win32Error ERROR_INVALID_DATATYPE = new Standard.Win32Error(1804);

	public static explicit operator Standard.HRESULT(Standard.Win32Error error)
	{
		if (error._value <= 0)
		{
			return new Standard.HRESULT((uint)error._value);
		}
		return Standard.HRESULT.Make(severe: true, Standard.Facility.Win32, error._value & 0xFFFF);
	}

	public Standard.HRESULT ToHRESULT()
	{
		return (Standard.HRESULT)this;
	}

	public static Standard.Win32Error GetLastError()
	{
		return new Standard.Win32Error(Marshal.GetLastWin32Error());
	}

	public override bool Equals(object obj)
	{
		try
		{
			return ((Standard.Win32Error)obj)._value == _value;
		}
		catch (InvalidCastException)
		{
			return false;
		}
	}

	public override int GetHashCode()
	{
		return _value.GetHashCode();
	}

	public static bool operator ==(Standard.Win32Error errLeft, Standard.Win32Error errRight)
	{
		return errLeft._value == errRight._value;
	}

	public static bool operator !=(Standard.Win32Error errLeft, Standard.Win32Error errRight)
	{
		return !(errLeft == errRight);
	}
}
