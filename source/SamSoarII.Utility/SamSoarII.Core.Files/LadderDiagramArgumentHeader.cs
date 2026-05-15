using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public class LadderDiagramArgumentHeader : BaseFileHeader
{
	[FieldOffset(0)]
	public byte bHeaderSize;

	[FieldOffset(1)]
	public byte bValueType;

	[FieldOffset(2)]
	public byte bIOAccess;

	[FieldOffset(3)]
	public byte bReserved;

	[FieldOffset(4)]
	public int spName;

	[FieldOffset(8)]
	public int spComment;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.LadderDiagramArgument;

	public override IFileHeader Create()
	{
		return new LadderDiagramArgumentHeader();
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
		default:
			base.SetStrPtr(id, sp);
			break;
		}
	}
}
