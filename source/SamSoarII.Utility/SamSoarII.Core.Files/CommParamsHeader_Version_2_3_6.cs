using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public class CommParamsHeader_Version_2_3_6 : CommunicationParamsHeader
{
	[FieldOffset(0)]
	public ushort wFrameInterval;

	public override IFileHeader Create()
	{
		return new CommParamsHeader_Version_2_3_6();
	}

	public override FileVersion MinorSupportedVersion()
	{
		return new FileVersion
		{
			dwVersionMain = 2,
			dwVersionSub = 3,
			dwVersionModify = 6
		};
	}
}
