using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class SFCUnitHeader_Version_2_3_2 : SFCUnitHeader_Version_2_2_59
{
	public override IFileHeader Create()
	{
		return new SFCUnitHeader_Version_2_3_2();
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
}
