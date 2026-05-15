using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class AIOAnalogInputParamsHeader_2_3_17 : AIOAnalogInputParamsHeader
{
	public int dwIsUsedModeReg;

	public int dwModeReg;

	public override IFileHeader Create()
	{
		return new AIOAnalogInputParamsHeader_2_3_17();
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
