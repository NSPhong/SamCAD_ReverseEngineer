using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public abstract class ArchHeader : PolylineHeader
{
	[FieldOffset(0)]
	[MarshalAs(UnmanagedType.I4)]
	public int spR;

	[FieldOffset(4)]
	[MarshalAs(UnmanagedType.I4)]
	public int spCX;

	[FieldOffset(8)]
	[MarshalAs(UnmanagedType.I4)]
	public int spCY;

	[FieldOffset(12)]
	[MarshalAs(UnmanagedType.I4)]
	public int spOX;

	[FieldOffset(16)]
	[MarshalAs(UnmanagedType.I4)]
	public int spOY;

	[FieldOffset(20)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bArchType;

	[FieldOffset(21)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bDirection;

	[FieldOffset(22)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bQuality;

	[FieldOffset(23)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bReserved;

	public override int MaxHeaderSize => base.MaxHeaderSize + 24;

	public override void SetStrPtr(int id, int sp)
	{
		switch (id)
		{
		case 5:
			spR = sp;
			break;
		case 6:
			spCX = sp;
			break;
		case 7:
			spCY = sp;
			break;
		case 8:
			spOX = sp;
			break;
		case 9:
			spOY = sp;
			break;
		default:
			base.SetStrPtr(id, sp);
			break;
		}
	}
}
