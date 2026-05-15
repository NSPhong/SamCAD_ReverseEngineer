namespace SamSoarII.Utility.Files.KVS;

public class KVSArg
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

	public const ushort CODE_MASK_NULL = 0;

	public const ushort CODE_MASK_Z = 256;

	public const ushort CODE_MASK_CZ = 768;

	public const ushort CODE_MASK_WB = 2048;

	private KVSUnit parent;

	private KVSArgFormat format;

	private int id;

	private ushort code;

	private ulong offset;

	private string text;

	public KVSUnit Parent
	{
		get
		{
			return parent;
		}
		set
		{
			parent = value;
		}
	}

	public KVSArgFormat Format
	{
		get
		{
			return format ?? parent?.Format?.Args[id];
		}
		set
		{
			format = value;
		}
	}

	public int ID
	{
		get
		{
			return id;
		}
		set
		{
			id = value;
		}
	}

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

	public ulong Offset
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

	public string Text
	{
		get
		{
			return text;
		}
		set
		{
			text = value;
		}
	}

	public ushort CodeBase => (ushort)(code & 0xFF);

	public ushort CodeMask => (ushort)(code & 0xFF00);

	public string CodeName => GetCodeName(code);

	public uint OffsetBase => (uint)offset;

	public uint OffsetMask => (uint)(offset >> 32);

	public bool IsConst => CodeBase == 10 || CodeBase == 11 || CodeBase == 13 || CodeBase == 15 || CodeBase == 42 || CodeBase == 12 || CodeBase == 14;

	public bool IsOriginBit => CodeBase == 0 || CodeBase == 17 || CodeBase == 1 || CodeBase == 18 || CodeBase == 27;

	public static string GetCodeName(ushort code)
	{
		switch (code & 0xFF)
		{
		case 0:
			return "R";
		case 17:
			return "MR";
		case 18:
			return "LR";
		case 1:
			return "CR";
		case 2:
			return "T";
		case 3:
			return "C";
		case 27:
			return "B";
		case 28:
			return "W";
		case 4:
			return "CTH";
		case 5:
			return "CTC";
		case 6:
			return "DM";
		case 7:
			return "CM";
		case 8:
			return "TM";
		case 23:
			return "EM";
		case 25:
			return "FM";
		case 9:
			return "#TM";
		case 10:
		case 11:
		case 13:
			return "#";
		case 12:
		case 14:
			return "$";
		case 60:
			return "P";
		case 63:
			return $"NULL";
		default:
			return $"NULL";
		}
	}

	public KVSArg(KVSUnit _parent)
	{
		parent = _parent;
	}

	public override string ToString()
	{
		string empty = string.Empty;
		string text = string.Empty;
		switch (CodeBase)
		{
		case 0:
			empty = $"R{OffsetBase >> 4}{OffsetBase & 0xF:d2}";
			break;
		case 17:
			empty = $"MR{OffsetBase >> 4}{OffsetBase & 0xF:d2}";
			break;
		case 18:
			empty = $"LR{OffsetBase >> 4}{OffsetBase & 0xF:d2}";
			break;
		case 1:
			empty = $"CR{OffsetBase >> 4}{OffsetBase & 0xF:d2}";
			break;
		case 2:
			empty = $"T{OffsetBase}";
			break;
		case 3:
			empty = $"C{OffsetBase}";
			break;
		case 27:
			empty = $"B{OffsetBase:X}";
			break;
		case 28:
			empty = $"W{OffsetBase:X}";
			break;
		case 4:
			empty = $"CTH{OffsetBase}";
			break;
		case 5:
			empty = $"CTC{OffsetBase}";
			break;
		case 6:
			empty = $"DM{OffsetBase}";
			break;
		case 7:
			empty = $"CM{OffsetBase}";
			break;
		case 8:
			empty = $"TM{OffsetBase}";
			break;
		case 23:
			empty = $"EM{OffsetBase}";
			break;
		case 25:
			empty = $"FM{OffsetBase}";
			break;
		case 9:
			empty = $"#TM{OffsetBase}";
			break;
		case 10:
		case 11:
		case 13:
			empty = $"#{OffsetBase}";
			break;
		case 12:
		case 14:
			empty = $"${OffsetBase:X}";
			break;
		case 60:
			empty = $"P{OffsetBase}";
			break;
		case 63:
			return $"NULL";
		default:
			return $"(0x{code:X4})(0x{offset:X16})";
		}
		switch (CodeMask)
		{
		case 256:
			text = $":Z{OffsetMask}";
			break;
		case 768:
			text = $":{OffsetMask}";
			break;
		case 2048:
			text = $".{OffsetMask}";
			break;
		default:
			return $"(0x{code:X4})(0x{offset:X16})";
		case 0:
			break;
		}
		return empty + text;
	}

	public KVSArg Clone()
	{
		KVSArg kVSArg = new KVSArg(parent);
		kVSArg.ID = ID;
		kVSArg.Code = Code;
		kVSArg.Offset = Offset;
		return kVSArg;
	}
}
