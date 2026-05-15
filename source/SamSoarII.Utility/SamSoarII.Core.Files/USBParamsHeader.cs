using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public class USBParamsHeader : BaseFileHeader
{
	[FieldOffset(0)]
	[MarshalAs(UnmanagedType.I2)]
	public short wHeaderSize;

	[FieldOffset(2)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bTimeout;

	[FieldOffset(3)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bIsAutoRelink;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.UsbParams;

	public override IFileHeader Create()
	{
		return new USBParamsHeader();
	}
}
