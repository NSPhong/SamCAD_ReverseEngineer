using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class LadderDiagramHeader_Version_2_3_14 : LadderDiagramHeader_Version_2_3_12
{
	public int dwPreNetCount;

	public int lpPreNetList;

	public override IFileHeader Create()
	{
		return new LadderDiagramHeader_Version_2_3_14();
	}

	public override FileVersion MinorSupportedVersion()
	{
		return new FileVersion
		{
			dwVersionMain = 2,
			dwVersionSub = 3,
			dwVersionModify = 14
		};
	}
}
