using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class FuncBlockHeader_Version_2_2_47 : FuncBlockHeader_Version_2_2_41
{
	public int dwKeyID;

	public override IFileHeader Create()
	{
		return new FuncBlockHeader_Version_2_2_47();
	}

	public override FileVersion MinorSupportedVersion()
	{
		return new FileVersion
		{
			dwVersionMain = 2,
			dwVersionSub = 2,
			dwVersionModify = 47
		};
	}
}
