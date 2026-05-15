using System;
using System.Runtime.InteropServices;

namespace SamSoarII.Utility.Files.Delta;

public struct DelZip179CallbackInfo
{
	public IntPtr caller;

	public int version;

	public int iszip;

	public int actioncode;

	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
	public int[] args;

	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
	public int[] refs;
}
