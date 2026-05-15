using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public class ECAMItemHeader : BaseFileHeader
{
	[FieldOffset(0)]
	public ushort wHeaderSize;

	[FieldOffset(2)]
	public byte bType;

	[FieldOffset(3)]
	public byte bReserved;

	[FieldOffset(4)]
	public float fX;

	[FieldOffset(8)]
	public float fY;

	[FieldOffset(12)]
	public float fK;

	[FieldOffset(16)]
	public byte bIsDX;

	[FieldOffset(17)]
	public byte bIsDY;

	[FieldOffset(18)]
	public byte bIsDK;

	[FieldOffset(19)]
	public byte bIsDType;

	[FieldOffset(20)]
	public int spDX;

	[FieldOffset(24)]
	public int spDY;

	[FieldOffset(28)]
	public int spDK;

	[FieldOffset(32)]
	public int spDType;

	public override int HeaderSize
	{
		get
		{
			return wHeaderSize;
		}
		set
		{
			wHeaderSize = (ushort)value;
		}
	}

	public override FileHeaderTypes HeaderType => FileHeaderTypes.ECAMItem;

	public override IFileHeader Create()
	{
		return new ECAMItemHeader();
	}

	public override void SetStrPtr(int id, int sp)
	{
		switch (id)
		{
		case 0:
			spDX = sp;
			break;
		case 1:
			spDY = sp;
			break;
		case 2:
			spDK = sp;
			break;
		case 3:
			spDType = sp;
			break;
		default:
			base.SetStrPtr(id, sp);
			break;
		}
	}
}
