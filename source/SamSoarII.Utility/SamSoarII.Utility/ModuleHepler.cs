using SamSoarII.Core.Models;

namespace SamSoarII.Utility;

public class ModuleHepler
{
	public static ExpansionModuleType GetTypeByCode(byte code)
	{
		switch (code)
		{
		case 182:
			return ExpansionModuleType.FGs_E4AI;
		case 178:
			return ExpansionModuleType.FGs_E8R;
		case 186:
			return ExpansionModuleType.FGs_E8T;
		case 177:
			return ExpansionModuleType.FGs_E8X;
		case 179:
			return ExpansionModuleType.FGs_E8X8T;
		case 180:
			return ExpansionModuleType.FGs_E16R;
		case 181:
			return ExpansionModuleType.FGs_E8AI;
		case 188:
			return ExpansionModuleType.FGs_E16T;
		case 183:
			return ExpansionModuleType.FGs_E2AO;
		case 184:
			return ExpansionModuleType.FGs_E4AI2AO;
		case 185:
			return ExpansionModuleType.FGs_E4TC;
		case 187:
			return ExpansionModuleType.FGs_E8X8R;
		case 189:
			return ExpansionModuleType.FGs_E16X;
		case 190:
			return ExpansionModuleType.FGs_E16X16T;
		case 191:
			return ExpansionModuleType.FGs_E16X16R;
		case 0:
			return ExpansionModuleType.None;
		default:
			if (code < 128)
			{
				return (ExpansionModuleType)(code + 19);
			}
			return (ExpansionModuleType)(code - 128 + 59);
		}
	}
}
