using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public class ExpansionHeader_Version_2_2_38 : ExpansionHeader
{
	[FieldOffset(0)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bExistCount;

	[FieldOffset(1)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bReserved1;

	[FieldOffset(2)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bReserved2;

	[FieldOffset(3)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bReserved3;

	[FieldOffset(4)]
	[MarshalAs(UnmanagedType.I4)]
	public int dwUnit8sCount;

	[FieldOffset(8)]
	[MarshalAs(UnmanagedType.I4)]
	public int lpUnit8s;

	[FieldOffset(12)]
	[MarshalAs(UnmanagedType.I4)]
	public int dwReserved3;

	public override IFileHeader Create()
	{
		return new ExpansionHeader_Version_2_2_38();
	}

	public override FileVersion MinorSupportedVersion()
	{
		return new FileVersion
		{
			dwVersionMain = 2,
			dwVersionSub = 2,
			dwVersionModify = 38
		};
	}
}
