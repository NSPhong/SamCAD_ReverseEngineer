using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public class PolylineSystemHeader : BaseFileHeader
{
	[FieldOffset(0)]
	[MarshalAs(UnmanagedType.I2)]
	public short wHeaderSize;

	[FieldOffset(2)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bID;

	[FieldOffset(3)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bIsEnable;

	[FieldOffset(4)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bUnit;

	[FieldOffset(5)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bOverflow;

	[FieldOffset(6)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bIsHMIEnabled;

	[FieldOffset(7)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bReserved;

	[FieldOffset(8)]
	[MarshalAs(UnmanagedType.I4)]
	public int spHMIAddress;

	[FieldOffset(12)]
	[MarshalAs(UnmanagedType.I4)]
	public int lpAxisX;

	[FieldOffset(16)]
	[MarshalAs(UnmanagedType.I4)]
	public int lpAxisY;

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

	public override int MaxHeaderSize => 20;

	public override FileHeaderTypes HeaderType => FileHeaderTypes.PolylineSystem;

	public override IFileHeader Create()
	{
		return new PolylineSystemHeader();
	}

	public override void SetStrPtr(int id, int sp)
	{
		if (id == 0)
		{
			spHMIAddress = sp;
		}
	}
}
