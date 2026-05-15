using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class ProjectParamsHeader_Version_2_3_11 : ProjectParamsHeader_Version_2_3_7
{
	public int lpaioanalog;

	public override IFileHeader Create()
	{
		return new ProjectParamsHeader_Version_2_3_11();
	}

	public override FileVersion MinorSupportedVersion()
	{
		return new FileVersion
		{
			dwVersionMain = 2,
			dwVersionSub = 3,
			dwVersionModify = 11
		};
	}
}
