using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public class StringMonitorElementHeader : BaseFileHeader
{
	[FieldOffset(0)]
	public byte bHeaderSize;

	[FieldOffset(1)]
	public byte bMaxSize;

	[FieldOffset(2)]
	public short wReserved;

	[FieldOffset(4)]
	public int spAddress;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.StringMonitorElement;

	public override IFileHeader Create()
	{
		return new StringMonitorElementHeader();
	}

	public override void SetStrPtr(int id, int sp)
	{
		if (id == 0)
		{
			spAddress = sp;
		}
		else
		{
			base.SetStrPtr(id, sp);
		}
	}
}
