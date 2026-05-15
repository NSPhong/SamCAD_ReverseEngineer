using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class ExpansionUnitAOHeader_2_3_16 : ExpansionUnitAOHeader
{
	public int dwIsUsedModeReg;

	public int dwModeReg;

	public override IFileHeader Create()
	{
		return new ExpansionUnitAOHeader_2_3_16();
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
