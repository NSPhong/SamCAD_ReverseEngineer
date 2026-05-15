using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public class PolylineUserFormatHeader : BaseFileHeader
{
	[FieldOffset(0)]
	public ushort wHeaderSize;

	[FieldOffset(2)]
	public byte bDataType;

	[FieldOffset(3)]
	public byte bReserved1;

	[FieldOffset(4)]
	public int spName;

	public override int HeaderSize
	{
		get
		{
			return wHeaderSize;
		}
		set
		{
			wHeaderSize = (ushort)value;
		}
	}

	public override FileHeaderTypes HeaderType => FileHeaderTypes.PolylineUserFormat;

	public override IFileHeader Create()
	{
		return new PolylineUserFormatHeader();
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
