using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public class ZRNRHeader : BaseFileHeader
{
	[FieldOffset(0)]
	[MarshalAs(UnmanagedType.I4)]
	public int dwHeaderSize;

	[FieldOffset(4)]
	[MarshalAs(UnmanagedType.I4)]
	public int dwFlag;

	[FieldOffset(8)]
	[MarshalAs(UnmanagedType.I4)]
	public int spOrigin;

	[FieldOffset(12)]
	[MarshalAs(UnmanagedType.I4)]
	public int spPositive;

	[FieldOffset(16)]
	[MarshalAs(UnmanagedType.I4)]
	public int spNegative;

	[FieldOffset(20)]
	[MarshalAs(UnmanagedType.I4)]
	public int spZero;

	[FieldOffset(24)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bPositivePole;

	[FieldOffset(25)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bNegativePole;

	[FieldOffset(26)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bZeroPole;

	[FieldOffset(27)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bReturnZero;

	[FieldOffset(28)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bReturnZeroDirection;

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

	public override int MaxHeaderSize => 29;

	public override FileHeaderTypes HeaderType => FileHeaderTypes.ZRNR;

	public override IFileHeader Create()
	{
		return new ZRNRHeader();
	}

	public override void SetStrPtr(int id, int sp)
	{
		switch (id)
		{
		case 0:
			spOrigin = sp;
			break;
		case 1:
			spPositive = sp;
			break;
		case 2:
			spNegative = sp;
			break;
		case 3:
			spZero = sp;
			break;
		}
	}
}
