using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public class ValueBrpoElementHeader : BaseFileHeader
{
	[FieldOffset(0)]
	[MarshalAs(UnmanagedType.I2)]
	public short wHeaderSize;

	[FieldOffset(2)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bOperator;

	[FieldOffset(3)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bDataType;

	[FieldOffset(4)]
	[MarshalAs(UnmanagedType.I4)]
	public int spLeftValue;

	[FieldOffset(8)]
	[MarshalAs(UnmanagedType.I4)]
	public int spRightValue;

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

	public override int MaxHeaderSize => 12;

	public override FileHeaderTypes HeaderType => FileHeaderTypes.ValueBrpoElement;

	public override IFileHeader Create()
	{
		return new ValueBrpoElementHeader();
	}

	public override void SetStrPtr(int id, int sp)
	{
		switch (id)
		{
		case 0:
			spLeftValue = sp;
			break;
		case 1:
			spRightValue = sp;
			break;
		}
	}
}
