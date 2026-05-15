using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace Standard;

internal sealed class SafeFindHandle : SafeHandleZeroOrMinusOneIsInvalid
{
	[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
	private SafeFindHandle()
		: base(ownsHandle: true)
	{
	}

	protected override bool ReleaseHandle()
	{
		return Standard.NativeMethods.FindClose(handle);
	}
}
