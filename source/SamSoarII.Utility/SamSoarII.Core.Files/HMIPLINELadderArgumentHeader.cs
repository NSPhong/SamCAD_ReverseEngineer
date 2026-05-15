using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class HMIPLINELadderArgumentHeader : BaseFileHeader
{
	public int dwHeaderSize;

	public int dwPlaneID;

	public int spMotionPlot;

	public int spAutoSwitch;

	public int spNextBit;

	public int spPrevBit;

	public int spDestAddr;

	public int spSaveToFile;

	public int spExportFile;

	public int spFileAddr;

	public int spFileFirst;

	public int spRefreshIndex;

	public int spClearIndex;

	public int spDeleteIndex;

	public int spEndIndex;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.HMIPLINELadderArgument;

	public override IFileHeader Create()
	{
		return new HMIPLINELadderArgumentHeader();
	}

	public override void SetStrPtr(int id, int sp)
	{
		switch (id)
		{
		case 0:
			spMotionPlot = sp;
			break;
		case 1:
			spAutoSwitch = sp;
			break;
		case 2:
			spNextBit = sp;
			break;
		case 3:
			spPrevBit = sp;
			break;
		case 4:
			spDestAddr = sp;
			break;
		case 5:
			spSaveToFile = sp;
			break;
		case 6:
			spExportFile = sp;
			break;
		case 7:
			spFileAddr = sp;
			break;
		case 8:
			spFileFirst = sp;
			break;
		case 9:
			spRefreshIndex = sp;
			break;
		case 10:
			spClearIndex = sp;
			break;
		case 11:
			spDeleteIndex = sp;
			break;
		case 12:
			spEndIndex = sp;
			break;
		default:
			base.SetStrPtr(id, sp);
			break;
		}
	}
}
