using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public class ExpansionUnitAIHeader : BaseFileHeader
{
	[FieldOffset(0)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bHeaderSize;

	[FieldOffset(1)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bIsEnabled;

	[FieldOffset(2)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bModeIndex;

	[FieldOffset(3)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bSampleNumberIndex;

	[FieldOffset(4)]
	[MarshalAs(UnmanagedType.I2)]
	public ushort wDigitRangeLow;

	[FieldOffset(6)]
	[MarshalAs(UnmanagedType.I2)]
	public ushort wDigitRangeHigh;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.ExpansionUnitAIParams;

	public override IFileHeader Create()
	{
		return new ExpansionUnitAIHeader();
	}
}
