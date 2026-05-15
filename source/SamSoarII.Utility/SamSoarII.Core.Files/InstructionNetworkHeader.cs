using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public class InstructionNetworkHeader : BaseFileHeader
{
	[FieldOffset(0)]
	[MarshalAs(UnmanagedType.I2)]
	public short wHeaderSize;

	[FieldOffset(2)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bIsBlockForceCreated;

	[FieldOffset(3)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bReserved;

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

	public override int MaxHeaderSize => 4;

	public override FileHeaderTypes HeaderType => FileHeaderTypes.InstructionNetwork;

	public override IFileHeader Create()
	{
		return new InstructionNetworkHeader();
	}
}
