using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public class ExpansionUnitHeader : BaseFileHeader
{
	[FieldOffset(0)]
	[MarshalAs(UnmanagedType.I2)]
	public short wHeaderSize;

	[FieldOffset(2)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bIsEnabled;

	[FieldOffset(3)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bType;

	[FieldOffset(4)]
	[MarshalAs(UnmanagedType.I4)]
	public int dwAICount;

	[FieldOffset(8)]
	[MarshalAs(UnmanagedType.I4)]
	public int lpAIs;

	[FieldOffset(12)]
	[MarshalAs(UnmanagedType.I4)]
	public int dwAOCount;

	[FieldOffset(16)]
	[MarshalAs(UnmanagedType.I4)]
	public int lpAOs;

	[FieldOffset(20)]
	[MarshalAs(UnmanagedType.I4)]
	public int dwFilterTimeIndex;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.ExpansionUnitParams;

	public override IFileHeader Create()
	{
		return new ExpansionUnitHeader();
	}
}
