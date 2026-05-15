using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class LadderDiagramHeader : BaseFileHeader
{
	public int dwHeaderSize;

	public int dwKeyID;

	public int dwDiagramFlag;

	public int spName;

	public int spBrief;

	public int spDescription;

	public int spPath;

	public int dwPasswordFlag;

	public int spPassword;

	public int dwNetworkCount;

	public int lpNetwork;

	public int lpSFCLadder;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.LadderDiagram;

	public override IFileHeader Create()
	{
		return new LadderDiagramHeader();
	}

	public override void SetStrPtr(int id, int sp)
	{
		switch (id)
		{
		case 0:
			spName = sp;
			break;
		case 1:
			spBrief = sp;
			break;
		case 2:
			spDescription = sp;
			break;
		case 3:
			spPath = sp;
			break;
		case 4:
			spPassword = sp;
			break;
		}
	}
}
