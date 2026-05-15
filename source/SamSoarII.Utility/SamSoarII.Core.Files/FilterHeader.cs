using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public class FilterHeader : BaseFileHeader
{
	[FieldOffset(0)]
	[MarshalAs(UnmanagedType.I4)]
	public int dwHeaderSize;

	[FieldOffset(4)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bFilterModeIndex;

	[FieldOffset(5)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bFilterTimeIndex_Whole;

	[FieldOffset(6)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bHardwareEnabled;

	[FieldOffset(7)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bReserved1;

	[FieldOffset(8)]
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
	public ushort[] wFilterTimeIndics = new ushort[32];

	[FieldOffset(72)]
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
	public ushort[] wDivideFactorIndics = new ushort[16];

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.FilterParams;

	public override IFileHeader Create()
	{
		return new FilterHeader();
	}
}
