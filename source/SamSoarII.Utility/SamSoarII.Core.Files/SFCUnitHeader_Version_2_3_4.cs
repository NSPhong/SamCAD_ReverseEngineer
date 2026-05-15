using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class SFCUnitHeader_Version_2_3_4 : SFCUnitHeader_Version_2_3_2
{
	public override IFileHeader Create()
	{
		return new SFCUnitHeader_Version_2_3_4();
	}

	public override FileVersion MinorSupportedVersion()
	{
		return new FileVersion
		{
			dwVersionMain = 2,
			dwVersionSub = 3,
			dwVersionModify = 4
		};
	}
}
