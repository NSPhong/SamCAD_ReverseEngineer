using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class ExpansionUnitAIHeader_2_2_66 : ExpansionUnitAIHeader
{
	public int dwSampleValue;

	public override IFileHeader Create()
	{
		return new ExpansionUnitAIHeader_2_2_66();
	}

	public override FileVersion MinorSupportedVersion()
	{
		return new FileVersion
		{
			dwVersionMain = 2,
			dwVersionSub = 2,
			dwVersionModify = 66
		};
	}
}
