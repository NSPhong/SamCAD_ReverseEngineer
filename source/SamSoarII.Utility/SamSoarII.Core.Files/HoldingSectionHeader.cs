using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class HoldingSectionHeader : BaseFileHeader
{
	public short wHeaderSize;

	public short wMStart;

	public short wMLength;

	public short wDStart;

	public short wDLength;

	public short wSStart;

	public short wSLength;

	public short wCVStart;

	public short wCVLength;

	public short wCV32LowStart;

	public short wCV32LowLength;

	public short wCV32HighStart;

	public short wCV32HighLength;

	public byte bNotClear;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.HoldingParams;

	public override IFileHeader Create()
	{
		return new HoldingSectionHeader();
	}
}
