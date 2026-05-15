using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class AIOAnalogOutputParamsHeader_2_3_17 : AIOAnalogOutputParamsHeader
{
	public int dwIsUsedModeReg;

	public int dwModeReg;

	public override IFileHeader Create()
	{
		return new AIOAnalogOutputParamsHeader_2_3_17();
	}

	public override FileVersion MinorSupportedVersion()
	{
		return new FileVersion
		{
			dwVersionMain = 2,
			dwVersionSub = 3,
			dwVersionModify = 17
		};
	}
}
