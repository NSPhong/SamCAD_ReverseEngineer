using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class LadderDiagramHeader_Version_2_3_23 : LadderDiagramHeader_Version_2_3_14
{
	public int dwDVITTableCount;

	public int lpDVITTables;

	public override IFileHeader Create()
	{
		return new LadderDiagramHeader_Version_2_3_23();
	}

	public override FileVersion MinorSupportedVersion()
	{
		return new FileVersion
		{
			dwVersionMain = 2,
			dwVersionSub = 3,
			dwVersionModify = 23
		};
	}
}
