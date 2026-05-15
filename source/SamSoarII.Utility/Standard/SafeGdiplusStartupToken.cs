using System;
using System.Runtime.ConstrainedExecution;
using Microsoft.Win32.SafeHandles;

namespace Standard;

internal sealed class SafeGdiplusStartupToken : SafeHandleZeroOrMinusOneIsInvalid
{
	private SafeGdiplusStartupToken()
		: base(ownsHandle: true)
	{
	}

	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	protected override bool ReleaseHandle()
	{
		Standard.Status status = Standard.NativeMethods.GdiplusShutdown(handle);
		return status == Standard.Status.Ok;
	}

	public static Standard.SafeGdiplusStartupToken Startup()
	{
		Standard.SafeGdiplusStartupToken safeGdiplusStartupToken = new Standard.SafeGdiplusStartupToken();
		if (Standard.NativeMethods.GdiplusStartup(out var token, new Standard.StartupInput(), out var _) == Standard.Status.Ok)
		{
			safeGdiplusStartupToken.handle = token;
			return safeGdiplusStartupToken;
		}
		safeGdiplusStartupToken.Dispose();
		throw new Exception("Unable to initialize GDI+");
	}
}
