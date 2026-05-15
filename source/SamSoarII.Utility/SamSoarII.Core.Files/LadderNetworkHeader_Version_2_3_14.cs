using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class LadderNetworkHeader_Version_2_3_14 : LadderNetworkHeader
{
	public int lpCmtList;

	public int lpExList;

	public override IFileHeader Create()
	{
		return new LadderNetworkHeader_Version_2_3_14();
	}

	public override FileVersion MinorSupportedVersion()
	{
		return new FileVersion
		{
			dwVersionMain = 2,
			dwVersionSub = 3,
			dwVersionModify = 14
		};
	}
}
