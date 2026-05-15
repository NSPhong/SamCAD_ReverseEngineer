using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class ProjectHeader_Version_2_2_46 : ProjectHeader
{
	public int dwFolderCount;

	public int lpFolder;

	public FileVersion stMinorAppVersion;

	public override IFileHeader Create()
	{
		return new ProjectHeader_Version_2_2_46();
	}

	public override FileVersion MinorSupportedVersion()
	{
		return new FileVersion
		{
			dwVersionMain = 2,
			dwVersionSub = 2,
			dwVersionModify = 46
		};
	}
}
