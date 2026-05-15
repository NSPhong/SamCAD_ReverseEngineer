using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public class FuncBlockHeader_Version_2_2_41 : FuncBlockHeader
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
	public ushort wCaretLine;

	[FieldOffset(12)]
	[MarshalAs(UnmanagedType.I2)]
	public ushort wCaretColumn;

	[FieldOffset(14)]
	[MarshalAs(UnmanagedType.I2)]
	public ushort wReserved1;

	public override IFileHeader Create()
	{
		return new FuncBlockHeader_Version_2_2_41();
	}

	public override FileVersion MinorSupportedVersion()
	{
		return new FileVersion
		{
			dwVersionMain = 2,
			dwVersionSub = 2,
			dwVersionModify = 41
		};
	}
}
