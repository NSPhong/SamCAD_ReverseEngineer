using System;
using System.Runtime.InteropServices;

namespace SamSoarII.Utility.Files.Delta;

public struct DelZip179UnzipInfo
{
	public IntPtr hwnd;

	public IntPtr caller;

	public int vermain;

	public DelZip179Callback callback;

	public int zcallbackfunc;

	public int zstreamfunc;

	public int verbosity;

	public IntPtr inbuffer;

	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
	public int[] iboptions;

	public IntPtr outbuffer;

	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
	public int[] oboptions;

	public IntPtr errbuffer;

	public IntPtr errbuffer1;

	public IntPtr errbuffer2;

	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
	public int[] eboptions;

	public IntPtr poutput;

	public int szoutput;

	public int outputoption;

	public IntPtr pinput;

	public int szinput;

	public int inputoption;

	public IntPtr filepath;

	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
	public int[] fileoptions;

	public int endcode;
}
