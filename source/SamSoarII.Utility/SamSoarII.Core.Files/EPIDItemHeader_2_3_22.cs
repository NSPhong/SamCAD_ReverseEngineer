using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class EPIDItemHeader_2_3_22 : EPIDItemHeader_Version_2_3_15
{
	public int dwInputFilter;

	public int dwUncompDiffFactor;

	public int dwSelfMaintainAlgorithm;

	public int dwWarning;

	public override IFileHeader Create()
	{
		return new EPIDItemHeader_2_3_22();
	}

	public override FileVersion MinorSupportedVersion()
	{
		return new FileVersion
		{
			dwVersionMain = 2,
			dwVersionSub = 3,
			dwVersionModify = 22
		};
	}
}
