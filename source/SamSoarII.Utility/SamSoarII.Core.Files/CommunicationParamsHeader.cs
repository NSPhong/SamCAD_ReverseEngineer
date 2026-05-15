using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public class CommunicationParamsHeader : BaseFileHeader
{
	[FieldOffset(0)]
	[MarshalAs(UnmanagedType.I2)]
	public short wHeaderSize;

	[FieldOffset(2)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bBaudRate;

	[FieldOffset(3)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bDataBit;

	[FieldOffset(4)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bStopBit;

	[FieldOffset(5)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bCheckCode;

	[FieldOffset(6)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bBufferBit;

	[FieldOffset(7)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bStationNumber;

	[FieldOffset(8)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bTimeout;

	[FieldOffset(9)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bRestrans;

	[FieldOffset(10)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bComType;

	[FieldOffset(11)]
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

	public override int MaxHeaderSize => 16;

	public override FileHeaderTypes HeaderType => FileHeaderTypes.ProjectParams;

	public override IFileHeader Create()
	{
		return new CommunicationParamsHeader();
	}
}
