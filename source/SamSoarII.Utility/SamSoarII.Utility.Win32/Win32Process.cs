using System;
using System.Runtime.InteropServices;

namespace SamSoarII.Utility.Win32;

public class Win32Process
{
	[StructLayout(LayoutKind.Explicit)]
	public struct SECURITY_ATTRIBUTES
	{
		[FieldOffset(0)]
		private int length;

		[FieldOffset(4)]
		private IntPtr descriptor;

		[FieldOffset(8)]
		private int inherited;
	}

	[StructLayout(LayoutKind.Explicit)]
	public struct PROCESS_INFOMATION
	{
		[FieldOffset(0)]
		private int hdprocess;

		[FieldOffset(4)]
		private int hdthread;

		[FieldOffset(8)]
		private int idprocess;

		[FieldOffset(12)]
		private int idthread;
	}

	[StructLayout(LayoutKind.Explicit)]
	public struct STARTUPINFO
	{
		[FieldOffset(0)]
		private int cb;

		[FieldOffset(4)]
		private IntPtr reserved;

		[FieldOffset(8)]
		private IntPtr desktop;

		[FieldOffset(12)]
		private IntPtr title;

		[FieldOffset(16)]
		private int x;

		[FieldOffset(20)]
		private int y;

		[FieldOffset(24)]
		private int xsize;

		[FieldOffset(28)]
		private int ysize;

		[FieldOffset(32)]
		private int xcountchars;

		[FieldOffset(36)]
		private int ycountchars;

		[FieldOffset(40)]
		private int fillattributes;

		[FieldOffset(44)]
		private int flags;

		[FieldOffset(48)]
		private short showwindow;

		[FieldOffset(50)]
		private short reserved2;

		[FieldOffset(52)]
		private IntPtr reserved3;

		[FieldOffset(56)]
		private int hdinput;

		[FieldOffset(60)]
		private int hdoutput;

		[FieldOffset(64)]
		private int hderror;
	}

	private string filename;

	private string arguments;

	public string FileName
	{
		get
		{
			return filename;
		}
		set
		{
			filename = value;
		}
	}

	public string Arguments
	{
		get
		{
			return arguments;
		}
		set
		{
			arguments = value;
		}
	}

	[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "CreateProcessW")]
	public static extern int CreateProcess(string app, string cmd, IntPtr atprocess, IntPtr atthread, int inherited, int cflag, IntPtr env, string directory, ref STARTUPINFO startup, ref PROCESS_INFOMATION procinfo);

	[DllImport("kernel32.dll")]
	public static extern int CreatePipe(out int hdread, out int hdwrite, IntPtr atpipe, int nsize);

	[DllImport("kernel32.dll")]
	public static extern int CloseHandle(int hd);

	[DllImport("kernel32.dll", EntryPoint = "GetStartupInfoW")]
	public static extern int GetStartupInfo(out STARTUPINFO si);

	public Win32Process()
	{
		filename = string.Empty;
		arguments = string.Empty;
	}
}
