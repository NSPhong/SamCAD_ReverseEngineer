using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class SFCUnitHeader_Version_2_2_59 : SFCUnitHeader
{
	public int spComment;

	public override int MaxHeaderSize => 20;

	public override IFileHeader Create()
	{
		return new SFCUnitHeader_Version_2_2_59();
	}

	public override FileVersion MinorSupportedVersion()
	{
		return new FileVersion
		{
			dwVersionMain = 2,
			dwVersionSub = 2,
			dwVersionModify = 59
		};
	}

	public override void SetStrPtr(int id, int sp)
	{
		if (id == 0)
		{
			spComment = sp;
		}
		else
		{
			base.SetStrPtr(id, sp);
		}
	}
}
