using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class ExpansionUnitAIHeader_2_3_16 : ExpansionUnitAIHeader_2_3_14
{
	public int dwIsUsedModeReg;

	public int dwModeReg;

	public override IFileHeader Create()
	{
		return new ExpansionUnitAIHeader_2_3_16();
	}

	public override FileVersion MinorSupportedVersion()
	{
		return new FileVersion
		{
			dwVersionMain = 2,
			dwVersionSub = 3,
			dwVersionModify = 16
		};
	}
}
