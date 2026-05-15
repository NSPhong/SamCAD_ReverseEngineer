#define DEBUG
using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Win32.SafeHandles;

namespace Standard;

internal sealed class SafeConnectionPointCookie : SafeHandleZeroOrMinusOneIsInvalid
{
	private IConnectionPoint _cp;

	public SafeConnectionPointCookie(IConnectionPointContainer target, object sink, Guid eventId)
		: base(ownsHandle: true)
	{
		Standard.Verify.IsNotNull(target, "target");
		Standard.Verify.IsNotNull(sink, "sink");
		Standard.Verify.IsNotDefault(eventId, "eventId");
		handle = IntPtr.Zero;
		IConnectionPoint ppCP = null;
		try
		{
			target.FindConnectionPoint(ref eventId, out ppCP);
			ppCP.Advise(sink, out var pdwCookie);
			if (pdwCookie == 0)
			{
				throw new InvalidOperationException("IConnectionPoint::Advise returned an invalid cookie.");
			}
			handle = new IntPtr(pdwCookie);
			_cp = ppCP;
			ppCP = null;
		}
		finally
		{
			Standard.Utility.SafeRelease(ref ppCP);
		}
	}

	public void Disconnect()
	{
		ReleaseHandle();
	}

	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	protected override bool ReleaseHandle()
	{
		try
		{
			if (!IsInvalid)
			{
				int dwCookie = handle.ToInt32();
				handle = IntPtr.Zero;
				Standard.Assert.IsNotNull(_cp);
				try
				{
					_cp.Unadvise(dwCookie);
				}
				finally
				{
					Standard.Utility.SafeRelease(ref _cp);
				}
			}
			return true;
		}
		catch
		{
			return false;
		}
	}
}
