using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class EPIDItemHeader_Version_2_3_15 : EPIDItemHeader
{
	public int dwPIDMode;

	public int dwStabCheck;

	public int dwAdjustStatus;

	public override IFileHeader Create()
	{
		return new EPIDItemHeader_Version_2_3_15();
	}

	public override FileVersion MinorSupportedVersion()
	{
		return new FileVersion
		{
			dwVersionMain = 2,
			dwVersionSub = 3,
			dwVersionModify = 15
		};
	}
}
