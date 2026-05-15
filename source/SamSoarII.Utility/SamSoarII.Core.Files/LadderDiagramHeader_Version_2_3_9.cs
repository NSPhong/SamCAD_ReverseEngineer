using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class LadderDiagramHeader_Version_2_3_9 : LadderDiagramHeader_Version_2_2_40
{
	public int dwArgsCount;

	public int lpArgs;

	public int dwCALLTableCount;

	public int lpCALLTables;

	public int dwHSCSTableCount;

	public int lpHSCSTables;

	public override IFileHeader Create()
	{
		return new LadderDiagramHeader_Version_2_3_9();
	}

	public override FileVersion MinorSupportedVersion()
	{
		return new FileVersion
		{
			dwVersionMain = 2,
			dwVersionSub = 3,
			dwVersionModify = 9
		};
	}
}
