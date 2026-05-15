using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class FolderHeader_Version_2_3_22 : FolderHeader_Version_2_2_46
{
	public int dwItemsCount;

	public int lpItems;

	public override IFileHeader Create()
	{
		return new FolderHeader_Version_2_3_22();
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
