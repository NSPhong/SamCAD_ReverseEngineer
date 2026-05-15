using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public class HMIPLINEArgumentHeader : BaseFileHeader
{
	[FieldOffset(0)]
	public byte bHeaderSize;

	[FieldOffset(1)]
	public byte bCondition;

	[FieldOffset(2)]
	public short wReserved1;

	[FieldOffset(4)]
	public int spValue;

	[FieldOffset(8)]
	public int spRunning;

	[FieldOffset(12)]
	public int spFinally;

	[FieldOffset(16)]
	public int spXOffset;

	[FieldOffset(20)]
	public int spYOffset;

	[FieldOffset(24)]
	public int spVelocity;

	[FieldOffset(28)]
	public int spAccelerate;

	[FieldOffset(32)]
	public int spDecelerate;

	[FieldOffset(36)]
	public int spEndSource;

	[FieldOffset(40)]
	public int spEndDestination;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.HMIPLINEArgument;

	public override IFileHeader Create()
	{
		return new HMIPLINEArgumentHeader();
	}

	public override void SetStrPtr(int id, int sp)
	{
		switch (id)
		{
		case 0:
			spValue = sp;
			break;
		case 1:
			spRunning = sp;
			break;
		case 2:
			spFinally = sp;
			break;
		case 3:
			spXOffset = sp;
			break;
		case 4:
			spYOffset = sp;
			break;
		case 5:
			spVelocity = sp;
			break;
		case 6:
			spAccelerate = sp;
			break;
		case 7:
			spDecelerate = sp;
			break;
		case 8:
			spEndSource = sp;
			break;
		case 9:
			spEndDestination = sp;
			break;
		default:
			base.SetStrPtr(id, sp);
			break;
		}
	}
}
