using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class ProjectHeader_Version_2_3_9 : ProjectHeader_Version_2_2_56
{
	public int lpStrMoni;

	public override IFileHeader Create()
	{
		return new ProjectHeader_Version_2_3_9();
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
