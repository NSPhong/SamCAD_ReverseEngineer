using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public class ValueHeader : BaseFileHeader
{
	[FieldOffset(0)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bHeaderSize;

	[FieldOffset(1)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bInitType;

	[FieldOffset(2)]
	[MarshalAs(UnmanagedType.I2)]
	public short wFlag;

	[FieldOffset(4)]
	[MarshalAs(UnmanagedType.I4)]
	public int spName;

	[FieldOffset(8)]
	[MarshalAs(UnmanagedType.I4)]
	public int spComment;

	[FieldOffset(12)]
	[MarshalAs(UnmanagedType.I4)]
	public int spAlias;

	[FieldOffset(16)]
	[MarshalAs(UnmanagedType.I4)]
	public int spInitValue;

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

	public override int MaxHeaderSize => 20;

	public override FileHeaderTypes HeaderType => FileHeaderTypes.Value;

	public override IFileHeader Create()
	{
		return new ValueHeader();
	}

	public override void SetStrPtr(int id, int sp)
	{
		switch (id)
		{
		case 0:
			spName = sp;
			break;
		case 1:
			spComment = sp;
			break;
		case 2:
			spAlias = sp;
			break;
		case 3:
			spInitValue = sp;
			break;
		}
	}
}
