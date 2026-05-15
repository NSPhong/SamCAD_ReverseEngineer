using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public class OtherParamsHeader : BaseFileHeader
{
	[FieldOffset(0)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bHeaderSize;

	[FieldOffset(1)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bIsUsedWDT;

	[FieldOffset(2)]
	[MarshalAs(UnmanagedType.I2)]
	public ushort wWDTTime;

	[FieldOffset(4)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bIsRTCErrorOpened;

	[FieldOffset(5)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bRTCFormat;

	[FieldOffset(6)]
	[MarshalAs(UnmanagedType.I2)]
	public ushort wPowerDownDetectTime;

	[FieldOffset(8)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bHardwareRestore;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.OtherParams;

	public override IFileHeader Create()
	{
		return new OtherParamsHeader();
	}
}
