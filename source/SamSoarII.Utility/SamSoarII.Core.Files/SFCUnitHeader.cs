using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public class SFCUnitHeader : BaseFileHeader
{
	[FieldOffset(0)]
	[MarshalAs(UnmanagedType.I2)]
	public short wHeaderSize;

	[FieldOffset(2)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bType;

	[FieldOffset(3)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bReserved;

	[FieldOffset(4)]
	[MarshalAs(UnmanagedType.I2)]
	public short wX;

	[FieldOffset(6)]
	[MarshalAs(UnmanagedType.I2)]
	public short wY;

	[FieldOffset(8)]
	[MarshalAs(UnmanagedType.I2)]
	public short wWidth;

	[FieldOffset(10)]
	[MarshalAs(UnmanagedType.I2)]
	public short wHeight;

	[FieldOffset(12)]
	[MarshalAs(UnmanagedType.I4)]
	public int dwReserved;

	public override int HeaderSize
	{
		get
		{
			return wHeaderSize;
		}
		set
		{
			wHeaderSize = (short)value;
		}
	}

	public override int MaxHeaderSize => 16;

	public override FileHeaderTypes HeaderType => FileHeaderTypes.SFCUnit;

	public override IFileHeader Create()
	{
		return new SFCUnitHeader();
	}
}
