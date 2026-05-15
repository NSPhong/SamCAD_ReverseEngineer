using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public class AnalogHeader : BaseFileHeader
{
	[FieldOffset(0)]
	[MarshalAs(UnmanagedType.I4)]
	public int dwHeaderSize;

	[FieldOffset(4)]
	[MarshalAs(UnmanagedType.I2)]
	public short wIPChannelIndex;

	[FieldOffset(6)]
	[MarshalAs(UnmanagedType.I2)]
	public short wOPChannelIndex;

	[FieldOffset(8)]
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
	public byte[] bIPChannelCBEnabled = new byte[8];

	[FieldOffset(16)]
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
	public byte[] bOPChannelCBEnabled = new byte[4];

	[FieldOffset(20)]
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
	public byte[] bIPModeIndex = new byte[8];

	[FieldOffset(28)]
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
	public byte[] bOPModeIndex = new byte[4];

	[FieldOffset(32)]
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
	public byte[] bIPSampleTimeIndex = new byte[8];

	[FieldOffset(40)]
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
	public ushort[] wSampleValue = new ushort[8];

	[FieldOffset(56)]
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
	public ushort[] wIPStartRange = new ushort[4];

	[FieldOffset(64)]
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
	public ushort[] wIPEndRange = new ushort[4];

	[FieldOffset(72)]
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
	public ushort[] wOPStartRange = new ushort[4];

	[FieldOffset(80)]
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
	public ushort[] wOPEndRange = new ushort[4];

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.AnalogParams;

	public override IFileHeader Create()
	{
		return new AnalogHeader();
	}
}
