using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class ProjectHeader_Version_2_2_56 : ProjectHeader_Version_2_2_50
{
	public int lpValueBoard;

	public override IFileHeader Create()
	{
		return new ProjectHeader_Version_2_2_56();
	}

	public override FileVersion MinorSupportedVersion()
	{
		return new FileVersion
		{
			dwVersionMain = 2,
			dwVersionSub = 2,
			dwVersionModify = 56
		};
	}
}
