using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class ExpansionUnitAIHeader_2_3_14 : ExpansionUnitAIHeader_2_2_66
{
	public override IFileHeader Create()
	{
		return new ExpansionUnitAIHeader_2_3_14();
	}

	public override FileVersion MinorSupportedVersion()
	{
		return new FileVersion
		{
			dwVersionMain = 2,
			dwVersionSub = 3,
			dwVersionModify = 14
		};
	}
}
