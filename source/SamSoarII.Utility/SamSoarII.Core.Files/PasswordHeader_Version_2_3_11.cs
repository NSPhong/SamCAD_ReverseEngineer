using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class PasswordHeader_Version_2_3_11 : PasswordHeader
{
	public int dwCannotUpload;

	public override IFileHeader Create()
	{
		return new PasswordHeader_Version_2_3_11();
	}

	public override FileVersion MinorSupportedVersion()
	{
		return new FileVersion
		{
			dwVersionMain = 2,
			dwVersionSub = 3,
			dwVersionModify = 11
		};
	}
}
