using System.Runtime.ConstrainedExecution;
using Microsoft.Win32.SafeHandles;

namespace Standard;

internal sealed class SafeHBITMAP : SafeHandleZeroOrMinusOneIsInvalid
{
	private SafeHBITMAP()
		: base(ownsHandle: true)
	{
	}

	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	protected override bool ReleaseHandle()
	{
		return Standard.NativeMethods.DeleteObject(handle);
	}
}
