using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class LadderNetworkHeader : BaseFileHeader
{
	public int dwHeaderSize;

	public int dwID;

	public int dwNetworkFlag;

	public int spBrief;

	public int spDescription;

	public int dwRowCount;

	public int dwUnitCount;

	public int lpUnit;

	public int lpHLine;

	public int lpVLine;

	public int lpInst;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.LadderNetwork;

	public override IFileHeader Create()
	{
		return new LadderNetworkHeader();
	}

	public override void SetStrPtr(int id, int sp)
	{
		switch (id)
		{
		case 0:
			spBrief = sp;
			break;
		case 1:
			spDescription = sp;
			break;
		}
	}
}
