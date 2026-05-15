using System;
using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class NetworkCommnetListHeader : BaseFileHeader
{
	public int dwHeaderSize;

	public int dwLineCount;

	public int dwUnitCount;

	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
	public int[] dwArgs = new int[1];

	public override int HeaderSize
	{
		get
		{
			return dwHeaderSize;
		}
		set
		{
			dwHeaderSize = value;
			dwArgs = new int[dwHeaderSize - 12];
		}
	}

	public override FileHeaderTypes HeaderType => FileHeaderTypes.NetworkCommentList;

	public override IFileHeader Create()
	{
		return new NetworkCommnetListHeader();
	}

	public override void SetStrPtr(int id, int sp)
	{
		dwArgs[id] = sp;
	}

	public override void Load(IntPtr intptr)
	{
		base.Load(intptr);
		dwArgs = new int[dwHeaderSize - 12];
		Marshal.Copy(intptr + 12, dwArgs, 0, dwArgs.Length);
	}

	public override void Save(IntPtr intptr)
	{
		base.Save(intptr);
		Marshal.Copy(dwArgs, 0, intptr + 12, dwArgs.Length);
	}
}
