using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class ECAMHeader_Version_2_3_6 : ECAMHeader
{
	public int spLabelType;

	public int spProtectLength;

	public override IFileHeader Create()
	{
		return new ECAMHeader_Version_2_3_6();
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

	public override void SetStrPtr(int id, int sp)
	{
		switch (id)
		{
		case 32:
			spLabelType = sp;
			break;
		case 33:
			spProtectLength = sp;
			break;
		default:
			base.SetStrPtr(id, sp);
			break;
		}
	}
}
