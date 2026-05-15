using System;
using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public sealed class LadderUnitHeader : BaseFileHeader
{
	[FieldOffset(0)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bHeaderSize;

	[FieldOffset(2)]
	[MarshalAs(UnmanagedType.I2)]
	public short wType;

	[FieldOffset(1)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bX;

	[FieldOffset(4)]
	[MarshalAs(UnmanagedType.I2)]
	public ushort wY;

	[FieldOffset(8)]
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
	public int[] lpArgs = new int[16];

	public override int HeaderSize
	{
		get
		{
			return bHeaderSize;
		}
		set
		{
			bHeaderSize = (byte)value;
		}
	}

	public override int MaxHeaderSize => 70;

	public override FileHeaderTypes HeaderType => FileHeaderTypes.LadderUnit;

	public override void SetStrPtr(int id, int sp)
	{
		lpArgs[id] = sp;
	}

	public override IFileHeader Create()
	{
		return new LadderUnitHeader();
	}

	public override void Save(IntPtr intptr)
	{
		base.Save(intptr);
		Marshal.Copy(lpArgs, 0, intptr + 6, lpArgs.Length);
	}

	public override void Load(IntPtr intptr)
	{
		base.Load(intptr);
		Marshal.Copy(intptr + 6, lpArgs, 0, lpArgs.Length);
	}
}
