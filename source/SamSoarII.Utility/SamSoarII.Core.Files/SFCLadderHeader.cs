using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public class SFCLadderHeader : BaseFileHeader
{
	[FieldOffset(0)]
	[MarshalAs(UnmanagedType.I4)]
	public int dwHeaderSize;

	[FieldOffset(4)]
	[MarshalAs(UnmanagedType.I4)]
	public int dwSFCLadderFlag;

	[FieldOffset(8)]
	[MarshalAs(UnmanagedType.I2)]
	public short wWidth;

	[FieldOffset(10)]
	[MarshalAs(UnmanagedType.I2)]
	public short wHeight;

	[FieldOffset(12)]
	[MarshalAs(UnmanagedType.I4)]
	public int dwUnitCount;

	[FieldOffset(16)]
	[MarshalAs(UnmanagedType.I4)]
	public int lpUnit;

	public override int HeaderSize
	{
		get
		{
			return dwHeaderSize;
		}
		set
		{
			dwHeaderSize = value;
		}
	}

	public override FileHeaderTypes HeaderType => FileHeaderTypes.SFCLadder;

	public override IFileHeader Create()
	{
		return new SFCLadderHeader();
	}
}
