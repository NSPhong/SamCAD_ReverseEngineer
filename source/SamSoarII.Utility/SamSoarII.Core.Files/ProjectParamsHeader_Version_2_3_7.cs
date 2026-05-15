using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class ProjectParamsHeader_Version_2_3_7 : ProjectParamsHeader_Version_2_3_6
{
	public int lpCom2;

	public override IFileHeader Create()
	{
		return new ProjectParamsHeader_Version_2_3_7();
	}

	public override FileVersion MinorSupportedVersion()
	{
		return new FileVersion
		{
			dwVersionMain = 2,
			dwVersionSub = 3,
			dwVersionModify = 7
		};
	}
}
