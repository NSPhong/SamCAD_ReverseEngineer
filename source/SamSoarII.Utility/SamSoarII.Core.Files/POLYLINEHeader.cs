using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public abstract class POLYLINEHeader : BaseFileHeader
{
	public int dwHeaderSize;

	public int bRefMode;

	public int spRefAddr;

	public override int HeaderSize
	{
		get
		{
			return dwHeaderSize;
		}
		set
		{
			dwHeaderSize = value;
		}
	}

	public override void SetStrPtr(int id, int sp)
	{
		if (id == 0)
		{
			spRefAddr = sp;
		}
	}
}
[StructLayout(LayoutKind.Explicit)]
public abstract class PolylineHeader : BaseFileHeader
{
	[FieldOffset(0)]
	[MarshalAs(UnmanagedType.I2)]
	public short wHeaderSize;

	[FieldOffset(2)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bType = 0;

	[FieldOffset(3)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bMode;

	[FieldOffset(4)]
	[MarshalAs(UnmanagedType.I4)]
	public int spX;

	[FieldOffset(8)]
	[MarshalAs(UnmanagedType.I4)]
	public int spY;

	[FieldOffset(12)]
	[MarshalAs(UnmanagedType.I4)]
	public int spAC;

	[FieldOffset(16)]
	[MarshalAs(UnmanagedType.I4)]
	public int spDC;

	[FieldOffset(20)]
	[MarshalAs(UnmanagedType.I4)]
	public int spV;

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

	public override int MaxHeaderSize => 24;

	public override void SetStrPtr(int id, int sp)
	{
		switch (id)
		{
		case 0:
			spX = sp;
			break;
		case 1:
			spY = sp;
			break;
		case 2:
			spAC = sp;
			break;
		case 3:
			spDC = sp;
			break;
		case 4:
			spV = sp;
			break;
		}
	}
}
