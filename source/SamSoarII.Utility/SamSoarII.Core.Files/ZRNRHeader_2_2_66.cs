using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class ZRNRHeader_2_2_66 : ZRNRHeader
{
	public byte bOriginPole;

	public override int MaxHeaderSize => 33;

	public override IFileHeader Create()
	{
		return new ZRNRHeader_2_2_66();
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
