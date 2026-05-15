using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public class LadderArgumentUsedTableHeader : BaseFileHeader
{
	[FieldOffset(0)]
	public byte bHeaderSize;

	[FieldOffset(1)]
	public byte bX;

	[FieldOffset(2)]
	public ushort wNetwork;

	[FieldOffset(4)]
	public ushort wY;

	[FieldOffset(6)]
	public byte bValueID;

	[FieldOffset(7)]
	public byte bArgument;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.LadderArgumentUsedTable;

	public override IFileHeader Create()
	{
		return new LadderArgumentUsedTableHeader();
	}
}
