using System;
using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public class FBDUnitHeader : BaseFileHeader
{
	[FieldOffset(0)]
	public ushort wHeaderSize;

	[FieldOffset(2)]
	public ushort wUnitType;

	[FieldOffset(4)]
	public ushort wX;

	[FieldOffset(6)]
	public ushort wY;

	[FieldOffset(8)]
	public byte bWidth;

	[FieldOffset(9)]
	public byte bHeight;

	[FieldOffset(10)]
	public byte bChildrenCount;

	[FieldOffset(11)]
	public byte bReserved;

	[FieldOffset(12)]
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
	public int[] lpArgs = new int[64];

	public override int HeaderSize
	{
		get
		{
			return wHeaderSize;
		}
		set
		{
			wHeaderSize = (ushort)value;
		}
	}

	public override FileHeaderTypes HeaderType => FileHeaderTypes.FBDUnit;

	public override IFileHeader Create()
	{
		return new FBDUnitHeader();
	}

	public override void SetStrPtr(int id, int sp)
	{
		if (id == 65535)
		{
			lpArgs[(HeaderSize - 12) / 4 - 1] = sp;
		}
		else
		{
			lpArgs[id] = sp;
		}
	}

	public override void Save(IntPtr intptr)
	{
		base.Save(intptr);
		if (HeaderSize > 12)
		{
			Marshal.Copy(lpArgs, 0, intptr + 12, (HeaderSize - 12) / 4);
		}
	}

	public override void Load(IntPtr intptr)
	{
		base.Load(intptr);
		if (HeaderSize > 12)
		{
			lpArgs = new int[64];
			Marshal.Copy(intptr + 12, lpArgs, 0, (HeaderSize - 12) / 4);
		}
	}
}
