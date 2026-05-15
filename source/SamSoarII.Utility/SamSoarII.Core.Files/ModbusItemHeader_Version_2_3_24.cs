using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class ModbusItemHeader_Version_2_3_24 : ModbusItemHeader
{
	public int spComment;

	public override int MaxHeaderSize => 20;

	public override IFileHeader Create()
	{
		return new ModbusItemHeader_Version_2_3_24();
	}

	public override FileVersion MinorSupportedVersion()
	{
		return new FileVersion
		{
			dwVersionMain = 2,
			dwVersionSub = 3,
			dwVersionModify = 24
		};
	}

	public override void SetStrPtr(int id, int sp)
	{
		if (id == 2)
		{
			spComment = sp;
		}
		else
		{
			base.SetStrPtr(id, sp);
		}
	}
}
