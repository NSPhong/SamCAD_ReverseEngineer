using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class ProjectHeader_Version_2_2_30 : ProjectHeader
{
	public int spFilename;

	public override IFileHeader Create()
	{
		return new ProjectHeader_Version_2_2_30();
	}

	public override FileVersion MinorSupportedVersion()
	{
		return new FileVersion
		{
			dwVersionMain = 2,
			dwVersionSub = 2,
			dwVersionModify = 30
		};
	}

	public override void SetStrPtr(int id, int sp)
	{
		if (id == 2)
		{
			spFilename = sp;
		}
		else
		{
			base.SetStrPtr(id, sp);
		}
	}
}
