using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public class ModbusItemHeader : BaseFileHeader
{
	[FieldOffset(0)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bHeaderSize;

	[FieldOffset(1)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bHandleCode;

	[FieldOffset(2)]
	[MarshalAs(UnmanagedType.I2)]
	public short wSlaveID;

	[FieldOffset(4)]
	[MarshalAs(UnmanagedType.I4)]
	public int spSlaveRegister;

	[FieldOffset(8)]
	[MarshalAs(UnmanagedType.I4)]
	public int dwSlaveCount;

	[FieldOffset(12)]
	[MarshalAs(UnmanagedType.I4)]
	public int spMasteRegister;

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

	public override int MaxHeaderSize => 16;

	public override FileHeaderTypes HeaderType => FileHeaderTypes.ModbusItem;

	public override IFileHeader Create()
	{
		return new ModbusItemHeader();
	}

	public override void SetStrPtr(int id, int sp)
	{
		switch (id)
		{
		case 0:
			spSlaveRegister = sp;
			break;
		case 1:
			spMasteRegister = sp;
			break;
		}
	}
}
