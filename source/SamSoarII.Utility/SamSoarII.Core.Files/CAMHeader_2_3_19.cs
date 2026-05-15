using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class CAMHeader_2_3_19 : CAMHeader
{
	public int dwRefMode;

	public override IFileHeader Create()
	{
		return new CAMHeader_2_3_19();
	}

	public override FileVersion MinorSupportedVersion()
	{
		return new FileVersion
		{
			dwVersionMain = 2,
			dwVersionSub = 3,
			dwVersionModify = 19
		};
	}
}
