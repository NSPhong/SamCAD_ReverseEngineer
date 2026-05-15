using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public class LadderDiagramHeader_Version_2_2_40 : LadderDiagramHeader
{
	[FieldOffset(0)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bViewShowedInTab;

	[FieldOffset(1)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bViewShowedFloat;

	[FieldOffset(2)]
	[MarshalAs(UnmanagedType.I2)]
	public ushort wViewFloatLeft;

	[FieldOffset(4)]
	[MarshalAs(UnmanagedType.I2)]
	public ushort wViewFloatTop;

	[FieldOffset(6)]
	[MarshalAs(UnmanagedType.I2)]
	public ushort wViewFloatWidth;

	[FieldOffset(8)]
	[MarshalAs(UnmanagedType.I2)]
	public ushort wViewFloatHeight;

	[FieldOffset(10)]
	[MarshalAs(UnmanagedType.I2)]
	public ushort wViewHorizontalOffset;

	[FieldOffset(12)]
	[MarshalAs(UnmanagedType.I2)]
	public ushort wViewVerticalOffset;

	[FieldOffset(14)]
	[MarshalAs(UnmanagedType.I2)]
	public ushort wReserved1;

	public override IFileHeader Create()
	{
		return new LadderDiagramHeader_Version_2_2_40();
	}

	public override FileVersion MinorSupportedVersion()
	{
		return new FileVersion
		{
			dwVersionMain = 2,
			dwVersionSub = 2,
			dwVersionModify = 40
		};
	}
}
