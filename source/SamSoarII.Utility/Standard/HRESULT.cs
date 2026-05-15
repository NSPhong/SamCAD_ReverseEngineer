#define DEBUG
using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Standard;

[StructLayout(LayoutKind.Explicit)]
internal struct HRESULT(uint i)
{
	[FieldOffset(0)]
	private readonly uint _value = i;

	public static readonly Standard.HRESULT S_OK = new Standard.HRESULT(0u);

	public static readonly Standard.HRESULT S_FALSE = new Standard.HRESULT(1u);

	public static readonly Standard.HRESULT E_PENDING = new Standard.HRESULT(2147483658u);

	public static readonly Standard.HRESULT E_NOTIMPL = new Standard.HRESULT(2147500033u);

	public static readonly Standard.HRESULT E_NOINTERFACE = new Standard.HRESULT(2147500034u);

	public static readonly Standard.HRESULT E_POINTER = new Standard.HRESULT(2147500035u);

	public static readonly Standard.HRESULT E_ABORT = new Standard.HRESULT(2147500036u);

	public static readonly Standard.HRESULT E_FAIL = new Standard.HRESULT(2147500037u);

	public static readonly Standard.HRESULT E_UNEXPECTED = new Standard.HRESULT(2147549183u);

	public static readonly Standard.HRESULT STG_E_INVALIDFUNCTION = new Standard.HRESULT(2147680257u);

	public static readonly Standard.HRESULT REGDB_E_CLASSNOTREG = new Standard.HRESULT(2147746132u);

	public static readonly Standard.HRESULT DESTS_E_NO_MATCHING_ASSOC_HANDLER = new Standard.HRESULT(2147749635u);

	public static readonly Standard.HRESULT DESTS_E_NORECDOCS = new Standard.HRESULT(2147749636u);

	public static readonly Standard.HRESULT DESTS_E_NOTALLCLEARED = new Standard.HRESULT(2147749637u);

	public static readonly Standard.HRESULT E_ACCESSDENIED = new Standard.HRESULT(2147942405u);

	public static readonly Standard.HRESULT E_OUTOFMEMORY = new Standard.HRESULT(2147942414u);

	public static readonly Standard.HRESULT E_INVALIDARG = new Standard.HRESULT(2147942487u);

	public static readonly Standard.HRESULT INTSAFE_E_ARITHMETIC_OVERFLOW = new Standard.HRESULT(2147942934u);

	public static readonly Standard.HRESULT COR_E_OBJECTDISPOSED = new Standard.HRESULT(2148734498u);

	public static readonly Standard.HRESULT WC_E_GREATERTHAN = new Standard.HRESULT(3222072867u);

	public static readonly Standard.HRESULT WC_E_SYNTAX = new Standard.HRESULT(3222072877u);

	public Standard.Facility Facility => GetFacility((int)_value);

	public int Code => GetCode((int)_value);

	public bool Succeeded => (int)_value >= 0;

	public bool Failed => (int)_value < 0;

	public static Standard.HRESULT Make(bool severe, Standard.Facility facility, int code)
	{
		Standard.Assert.Implies(facility != (facility & (Standard.Facility)511), facility == Standard.Facility.Ese || facility == Standard.Facility.WinCodec);
		Standard.Assert.AreEqual(code, code & 0xFFFF);
		return new Standard.HRESULT((uint)((severe ? int.MinValue : 0) | ((int)facility << 16) | code));
	}

	public static Standard.Facility GetFacility(int errorCode)
	{
		return (Standard.Facility)((errorCode >> 16) & 0x1FFF);
	}

	public static int GetCode(int error)
	{
		return error & 0xFFFF;
	}

	public override string ToString()
	{
		FieldInfo[] fields = typeof(Standard.HRESULT).GetFields(BindingFlags.Static | BindingFlags.Public);
		foreach (FieldInfo fieldInfo in fields)
		{
			if (fieldInfo.FieldType == typeof(Standard.HRESULT))
			{
				Standard.HRESULT hRESULT = (Standard.HRESULT)fieldInfo.GetValue(null);
				if (hRESULT == this)
				{
					return fieldInfo.Name;
				}
			}
		}
		if (Facility == Standard.Facility.Win32)
		{
			FieldInfo[] fields2 = typeof(Standard.Win32Error).GetFields(BindingFlags.Static | BindingFlags.Public);
			foreach (FieldInfo fieldInfo2 in fields2)
			{
				if (fieldInfo2.FieldType == typeof(Standard.Win32Error))
				{
					Standard.Win32Error win32Error = (Standard.Win32Error)fieldInfo2.GetValue(null);
					if ((Standard.HRESULT)win32Error == this)
					{
						return "HRESULT_FROM_WIN32(" + fieldInfo2.Name + ")";
					}
				}
			}
		}
		return string.Format(CultureInfo.InvariantCulture, "0x{0:X8}", new object[1] { _value });
	}

	public override bool Equals(object obj)
	{
		try
		{
			return ((Standard.HRESULT)obj)._value == _value;
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

	public static bool operator ==(Standard.HRESULT hrLeft, Standard.HRESULT hrRight)
	{
		return hrLeft._value == hrRight._value;
	}

	public static bool operator !=(Standard.HRESULT hrLeft, Standard.HRESULT hrRight)
	{
		return !(hrLeft == hrRight);
	}

	public void ThrowIfFailed()
	{
		ThrowIfFailed(null);
	}

	public void ThrowIfFailed(string message)
	{
		if (!Failed)
		{
			return;
		}
		message = ((!string.IsNullOrEmpty(message)) ? (message + " (" + ToString() + ")") : ToString());
		Exception ex = Marshal.GetExceptionForHR((int)_value, new IntPtr(-1));
		Standard.Assert.IsNotNull(ex);
		Standard.Assert.IsFalse(ex is ArgumentNullException);
		if (ex.GetType() == typeof(COMException))
		{
			Standard.Facility facility = Facility;
			Standard.Facility facility2 = facility;
			ex = ((facility2 != Standard.Facility.Win32) ? ((ExternalException)new COMException(message, (int)_value)) : ((ExternalException)new Win32Exception(Code, message)));
		}
		else
		{
			ConstructorInfo constructor = ex.GetType().GetConstructor(new Type[1] { typeof(string) });
			if (null != constructor)
			{
				ex = constructor.Invoke(new object[1] { message }) as Exception;
				Standard.Assert.IsNotNull(ex);
			}
		}
		throw ex;
	}

	public static void ThrowLastError()
	{
		((Standard.HRESULT)Standard.Win32Error.GetLastError()).ThrowIfFailed();
		Standard.Assert.Fail();
	}
}
