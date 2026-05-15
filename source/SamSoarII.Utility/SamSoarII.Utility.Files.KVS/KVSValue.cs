using System.Collections.Generic;

namespace SamSoarII.Utility.Files.KVS;

public class KVSValue
{
	public const ushort CODE_R = 0;

	public const ushort CODE_CR = 1;

	public const ushort CODE_T = 2;

	public const ushort CODE_C = 3;

	public const ushort CODE_CTH = 4;

	public const ushort CODE_CTC = 5;

	public const ushort CODE_DM = 6;

	public const ushort CODE_CM = 7;

	public const ushort CODE_TM = 8;

	public const ushort CODE_TM_T = 9;

	public const ushort CODE_K8 = 10;

	public const ushort CODE_K16 = 11;

	public const ushort CODE_H16 = 12;

	public const ushort CODE_K32 = 13;

	public const ushort CODE_H32 = 14;

	public const ushort CODE_KF = 15;

	public const ushort CODE_STRING = 16;

	public const ushort CODE_MR = 17;

	public const ushort CODE_LR = 18;

	public const ushort CODE_EM = 23;

	public const ushort CODE_FM = 25;

	public const ushort CODE_B = 27;

	public const ushort CODE_W = 28;

	public const ushort CODE_KDF = 42;

	public const ushort CODE_P = 60;

	public const ushort CODE_NULL = 63;

	public const ushort CODE_MASK_Z = 256;

	public const ushort CODE_MASK_WB = 2048;

	private ushort code;

	private uint offset;

	private string comment;

	private List<KVSArg> args = new List<KVSArg>();

	private List<KVSGlobalLabel> labels = new List<KVSGlobalLabel>();

	private KVSSystemValue systemvalue;

	public ushort Code
	{
		get
		{
			return code;
		}
		set
		{
			code = value;
		}
	}

	public uint Offset
	{
		get
		{
			return offset;
		}
		set
		{
			offset = value;
		}
	}

	public string Comment
	{
		get
		{
			return comment;
		}
		set
		{
			comment = value;
		}
	}

	public IList<KVSArg> Args => args;

	public IList<KVSGlobalLabel> Labels => labels;

	public KVSSystemValue SystemValue
	{
		get
		{
			return systemvalue;
		}
		set
		{
			systemvalue = value;
		}
	}

	public bool IsConst => Code == 10 || Code == 11 || Code == 13 || Code == 15 || Code == 42 || Code == 12 || Code == 14;

	public bool IsOriginBit => Code == 0 || Code == 17 || Code == 1 || Code == 18 || Code == 27;

	public override string ToString()
	{
		return new KVSArg(null)
		{
			Code = code,
			Offset = offset
		}.ToString();
	}
}
