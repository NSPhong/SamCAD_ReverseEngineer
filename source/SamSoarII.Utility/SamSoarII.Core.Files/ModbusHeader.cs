using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class ModbusHeader : BaseFileHeader
{
	public int dwHeaderSize;

	public int spName;

	public int spBrief;

	public int dwItemCount;

	public int lpItem;

	public override int HeaderSize
	{
		get
		{
			return dwHeaderSize;
		}
		set
		{
			dwHeaderSize = value;
		}
	}

	public override FileHeaderTypes HeaderType => FileHeaderTypes.Modbus;

	public override IFileHeader Create()
	{
		return new ModbusHeader();
	}

	public override void SetStrPtr(int id, int sp)
	{
		switch (id)
		{
		case 0:
			spName = sp;
			break;
		case 1:
			spBrief = sp;
			break;
		}
	}
}
