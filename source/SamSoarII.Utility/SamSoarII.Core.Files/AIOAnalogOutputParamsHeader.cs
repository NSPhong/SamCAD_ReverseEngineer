using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public class AIOAnalogOutputParamsHeader : BaseFileHeader
{
	[FieldOffset(0)]
	public byte bHeaderSize;

	[FieldOffset(1)]
	public byte bIsEnabled;

	[FieldOffset(2)]
	public byte bMode;

	[FieldOffset(3)]
	public byte bTick;

	[FieldOffset(4)]
	public ushort wDigit0;

	[FieldOffset(6)]
	public ushort wDigit1;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.AIOAnalogOutputParams;

	public override IFileHeader Create()
	{
		return new AIOAnalogOutputParamsHeader();
	}
}
