using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class SFCOrMergeHeader_Version_2_3_2 : SFCOrMergeHeader
{
	public int spExpr;

	public override IFileHeader Create()
	{
		return new SFCOrMergeHeader_Version_2_3_2();
	}

	public override FileVersion MinorSupportedVersion()
	{
		return new FileVersion
		{
			dwVersionMain = 2,
			dwVersionSub = 3,
			dwVersionModify = 2
		};
	}

	public override void SetStrPtr(int id, int sp)
	{
		if (id == 0)
		{
			spExpr = sp;
		}
		else
		{
			base.SetStrPtr(id, sp);
		}
	}
}
