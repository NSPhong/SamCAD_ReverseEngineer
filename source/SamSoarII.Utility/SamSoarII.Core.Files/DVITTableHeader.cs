using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public sealed class DVITTableHeader : BaseFileHeader
{
	[FieldOffset(0)]
	public ushort wHeaderSize;

	[FieldOffset(2)]
	public ushort wNetworkID;

	[FieldOffset(4)]
	public ushort wX;

	[FieldOffset(6)]
	public ushort wY;

	[FieldOffset(8)]
	public ushort wITType;

	[FieldOffset(10)]
	public ushort wENType;

	[FieldOffset(12)]
	public uint dwITAddr;

	[FieldOffset(16)]
	public uint dwENAddr;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.DVITTable;

	public override IFileHeader Create()
	{
		return new DVITTableHeader();
	}
}
