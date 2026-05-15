using System;
using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public sealed class PolylineUserDataHeader : BaseFileHeader
{
	[MarshalAs(UnmanagedType.I4)]
	public int dwHeaderSize;

	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
	public byte[] dwData = new byte[1];

	public override int HeaderSize
	{
		get
		{
			return dwHeaderSize;
		}
		set
		{
			dwHeaderSize = value;
			dwData = new byte[Math.Max(1, value - 4)];
		}
	}

	public override FileHeaderTypes HeaderType => FileHeaderTypes.PolylineUserData;

	public override IFileHeader Create()
	{
		return new PolylineUserDataHeader();
	}

	public unsafe override void SetStrPtr(int id, int sp)
	{
		fixed (byte* ptr = &dwData[id])
		{
			*(int*)ptr = sp;
		}
	}

	public override void Save(IntPtr intptr)
	{
		base.Save(intptr);
		Marshal.Copy(dwData, 0, intptr + 4, dwData.Length);
	}

	public override void Load(IntPtr intptr)
	{
		base.Load(intptr);
		dwData = new byte[Math.Max(1, dwHeaderSize - 4)];
		Marshal.Copy(intptr + 4, dwData, 0, dwData.Length);
	}
}
