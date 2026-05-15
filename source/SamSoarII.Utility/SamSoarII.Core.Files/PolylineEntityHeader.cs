using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public class PolylineEntityHeader : BaseFileHeader
{
	[FieldOffset(0)]
	public byte bHeaderSize;

	[FieldOffset(1)]
	public byte bEntityType;

	[FieldOffset(2)]
	public byte bArgumentType;

	[FieldOffset(3)]
	public byte bIsReal;

	[FieldOffset(4)]
	public int lpArgument;

	[FieldOffset(8)]
	public int spName;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.PolylineImage;

	public override IFileHeader Create()
	{
		return new PolylineEntityHeader();
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
