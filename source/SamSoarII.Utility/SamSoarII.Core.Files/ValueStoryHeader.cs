using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public class ValueStoryHeader : BaseFileHeader
{
	[FieldOffset(0)]
	[MarshalAs(UnmanagedType.I2)]
	public short wHeaderSize;

	[FieldOffset(2)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bDataType;

	[FieldOffset(3)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bIntraType;

	[FieldOffset(4)]
	[MarshalAs(UnmanagedType.I2)]
	public short wIntraOffset;

	[FieldOffset(6)]
	[MarshalAs(UnmanagedType.I2)]
	public short wFlag;

	[FieldOffset(8)]
	[MarshalAs(UnmanagedType.I4)]
	public int spName;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.ValueStory;

	public override IFileHeader Create()
	{
		return new ValueStoryHeader();
	}

	public override void SetStrPtr(int id, int sp)
	{
		if (id == 0)
		{
			spName = sp;
		}
		else
		{
			base.SetStrPtr(id, sp);
		}
	}
}
