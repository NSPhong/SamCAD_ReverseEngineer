using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public class HMIPLINEImageArgumentHeader : BaseFileHeader
{
	[FieldOffset(0)]
	public byte bHeaderSize;

	[FieldOffset(1)]
	public byte bStartupMode;

	[FieldOffset(2)]
	public short wPlaneID;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.HMIPLINEImageArgument;

	public override IFileHeader Create()
	{
		return new HMIPLINEImageArgumentHeader();
	}
}
