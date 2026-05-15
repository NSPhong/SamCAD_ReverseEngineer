using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class ProjectHeader_Version_2_3_12 : ProjectHeader_Version_2_3_11
{
	public int spWhyVer;

	public override IFileHeader Create()
	{
		return new ProjectHeader_Version_2_3_12();
	}

	public override FileVersion MinorSupportedVersion()
	{
		return new FileVersion
		{
			dwVersionMain = 2,
			dwVersionSub = 3,
			dwVersionModify = 12
		};
	}

	public override void SetStrPtr(int id, int sp)
	{
		if (id == 10)
		{
			spWhyVer = sp;
		}
		else
		{
			base.SetStrPtr(id, sp);
		}
	}
}
