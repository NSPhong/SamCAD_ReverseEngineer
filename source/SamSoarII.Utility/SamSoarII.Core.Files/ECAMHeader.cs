using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public class ECAMHeader : BaseFileHeader
{
	[FieldOffset(0)]
	public ushort wHeaderSize;

	[FieldOffset(2)]
	public byte bCAMType;

	[FieldOffset(3)]
	public byte bCAMUnit;

	[FieldOffset(4)]
	public int dwItemCount;

	[FieldOffset(8)]
	public int lpItems;

	[FieldOffset(12)]
	public float fYMin;

	[FieldOffset(16)]
	public float fYMax;

	[FieldOffset(20)]
	public int spXPlsMm;

	[FieldOffset(24)]
	public int spYPlsMm;

	[FieldOffset(28)]
	public int spXRoundPls;

	[FieldOffset(32)]
	public int spXRoundSize;

	[FieldOffset(36)]
	public int spYRoundPls;

	[FieldOffset(40)]
	public int spYRoundSize;

	[FieldOffset(44)]
	public int spKnifeDiameter;

	[FieldOffset(48)]
	public int spKnifeNumber;

	[FieldOffset(52)]
	public int spXFixVelocity;

	[FieldOffset(56)]
	public int spXFixMove;

	[FieldOffset(60)]
	public int spXFixOffset;

	[FieldOffset(64)]
	public int spXMeasureOffset;

	[FieldOffset(68)]
	public int spXNowOffset;

	[FieldOffset(72)]
	public int spXTargetOffset;

	[FieldOffset(76)]
	public int spYScaleUp;

	[FieldOffset(80)]
	public int spYScaleDown;

	[FieldOffset(84)]
	public int spFollowPerformance;

	[FieldOffset(88)]
	public int spFixProfix;

	[FieldOffset(92)]
	public int spCutLength;

	[FieldOffset(96)]
	public int spAngleAc;

	[FieldOffset(100)]
	public int spAngleSyn;

	[FieldOffset(104)]
	public int spXAc;

	[FieldOffset(108)]
	public int spXSyn;

	[FieldOffset(112)]
	public int spXDc;

	[FieldOffset(116)]
	public int spYAc;

	[FieldOffset(120)]
	public int spYTotal;

	[FieldOffset(124)]
	public int spTarget;

	[FieldOffset(128)]
	public int spClutch;

	[FieldOffset(132)]
	public int spPhaseID;

	[FieldOffset(136)]
	public int spDControl;

	[FieldOffset(140)]
	public int spSystemReserved;

	[FieldOffset(144)]
	public int spSynLab;

	[FieldOffset(148)]
	public byte bIsXRemember;

	[FieldOffset(149)]
	public byte bIsDControl;

	[FieldOffset(150)]
	public byte bDirection;

	[FieldOffset(151)]
	public byte bIsConstLabel;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.ECAM;

	public override IFileHeader Create()
	{
		return new ECAMHeader();
	}

	public override void SetStrPtr(int id, int sp)
	{
		switch (id)
		{
		case 0:
			spXPlsMm = sp;
			break;
		case 1:
			spYPlsMm = sp;
			break;
		case 2:
			spXRoundPls = sp;
			break;
		case 3:
			spXRoundSize = sp;
			break;
		case 4:
			spYRoundPls = sp;
			break;
		case 5:
			spYRoundSize = sp;
			break;
		case 6:
			spKnifeDiameter = sp;
			break;
		case 7:
			spKnifeNumber = sp;
			break;
		case 8:
			spXFixVelocity = sp;
			break;
		case 9:
			spXFixMove = sp;
			break;
		case 10:
			spXFixOffset = sp;
			break;
		case 11:
			spXMeasureOffset = sp;
			break;
		case 12:
			spXNowOffset = sp;
			break;
		case 13:
			spXTargetOffset = sp;
			break;
		case 14:
			spYScaleUp = sp;
			break;
		case 15:
			spYScaleDown = sp;
			break;
		case 16:
			spFollowPerformance = sp;
			break;
		case 17:
			spFixProfix = sp;
			break;
		case 18:
			spCutLength = sp;
			break;
		case 19:
			spAngleAc = sp;
			break;
		case 20:
			spAngleSyn = sp;
			break;
		case 21:
			spXAc = sp;
			break;
		case 22:
			spXSyn = sp;
			break;
		case 23:
			spXDc = sp;
			break;
		case 24:
			spYAc = sp;
			break;
		case 25:
			spYTotal = sp;
			break;
		case 26:
			spTarget = sp;
			break;
		case 27:
			spClutch = sp;
			break;
		case 28:
			spPhaseID = sp;
			break;
		case 29:
			spDControl = sp;
			break;
		case 30:
			spSynLab = sp;
			break;
		case 31:
			spSystemReserved = sp;
			break;
		}
		base.SetStrPtr(id, sp);
	}
}
