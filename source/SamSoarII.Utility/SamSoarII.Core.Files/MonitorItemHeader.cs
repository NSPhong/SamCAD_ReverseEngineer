using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public class MonitorItemHeader : BaseFileHeader
{
	[FieldOffset(0)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bHeaderSize;

	[FieldOffset(1)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bDataType;

	[FieldOffset(2)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bIntraType;

	[FieldOffset(3)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bIntraAddr;

	[FieldOffset(4)]
	[MarshalAs(UnmanagedType.I4)]
	public int spName;

	[FieldOffset(8)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bFlag;

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

	public override int MaxHeaderSize => 9;

	public override FileHeaderTypes HeaderType => FileHeaderTypes.MonitorItem;

	public override IFileHeader Create()
	{
		return new MonitorItemHeader();
	}

	public override void SetStrPtr(int id, int sp)
	{
		if (id == 0)
		{
			spName = sp;
		}
	}
}
