using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public class PreNetListHeader : BaseFileHeader
{
	[FieldOffset(0)]
	public byte bHeaderSize;

	[FieldOffset(1)]
	public byte bX;

	[FieldOffset(2)]
	public ushort wY;

	[FieldOffset(4)]
	public short wNetworkID;

	[FieldOffset(6)]
	public ushort wFlag;

	[FieldOffset(8)]
	public int spDiagramName;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.PreNetList;

	public override IFileHeader Create()
	{
		return new PreNetListHeader();
	}

	public override void SetStrPtr(int id, int sp)
	{
		if (id == 0)
		{
			spDiagramName = sp;
		}
		else
		{
			base.SetStrPtr(id, sp);
		}
	}
}
