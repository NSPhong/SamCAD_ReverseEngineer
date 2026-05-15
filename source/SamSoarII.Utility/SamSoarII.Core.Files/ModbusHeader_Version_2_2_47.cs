using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class ModbusHeader_Version_2_2_47 : ModbusHeader
{
	public int dwKeyID;

	public override IFileHeader Create()
	{
		return new ModbusHeader_Version_2_2_47();
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
