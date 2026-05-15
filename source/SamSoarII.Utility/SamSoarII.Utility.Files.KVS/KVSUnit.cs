using System.Collections.Generic;

namespace SamSoarII.Utility.Files.KVS;

public class KVSUnit
{
	public const ushort CODE_LD = 0;

	public const ushort CODE_LDB = 1;

	public const ushort CODE_LDP = 6;

	public const ushort CODE_LDF = 7;

	public const ushort CODE_RFSX = 57345;

	public const ushort CODE_RFSY = 57346;

	public const ushort CODE_LDPB = 57350;

	public const ushort CODE_LDFB = 57351;

	public const ushort CODE_BLD = 12;

	public const ushort CODE_BLDB = 13;

	public const ushort CODE_EQ = 18;

	public const ushort CODE_NE = 23;

	public const ushort CODE_G = 20;

	public const ushort CODE_L = 19;

	public const ushort CODE_GE = 22;

	public const ushort CODE_LE = 21;

	public const ushort CODE_OUT = 36;

	public const ushort CODE_OUB = 37;

	public const ushort CODE_SET = 38;

	public const ushort CODE_RST = 39;

	public const ushort CODE_KEEP = 40;

	public const ushort CODE_DIFU = 41;

	public const ushort CODE_DIFD = 42;

	public const ushort CODE_ONDL = 43;

	public const ushort CODE_OFDL = 44;

	public const ushort CODE_SHOT = 45;

	public const ushort CODE_FLIK = 46;

	public const ushort CODE_ALT = 47;

	public const ushort CODE_BOUT = 48;

	public const ushort CODE_BOUB = 49;

	public const ushort CODE_BSET = 50;

	public const ushort CODE_BRST = 51;

	public const ushort CODE_TMR = 52;

	public const ushort CODE_TMH = 53;

	public const ushort CODE_TMS = 54;

	public const ushort CODE_C = 55;

	public const ushort CODE_ITVL = 56;

	public const ushort CODE_UDC = 57;

	public const ushort CODE_UDT = 58;

	public const ushort CODE_OUTC = 57344;

	public const ushort CODE_TMU = 57356;

	public const ushort CODE_END = 60;

	public const ushort CODE_ENDH = 61;

	public const ushort CODE_INV = 57347;

	public const ushort CODE_MEP = 57348;

	public const ushort CODE_MEF = 57349;

	public const ushort CODE_SFT = 68;

	public const ushort CODE_MEMSW = 69;

	public const ushort CODE_STP = 70;

	public const ushort CODE_STE = 71;

	public const ushort CODE_STG = 72;

	public const ushort CODE_JMP = 73;

	public const ushort CODE_ENDS = 74;

	public const ushort CODE_W_ON = 75;

	public const ushort CODE_W_OFF = 76;

	public const ushort CODE_W_UE = 77;

	public const ushort CODE_W_DE = 78;

	public const ushort CODE_MC = 79;

	public const ushort CODE_MCR = 80;

	public const ushort CODE_CALL = 81;

	public const ushort CODE_SBR = 82;

	public const ushort CODE_RET = 83;

	public const ushort CODE_FOR = 84;

	public const ushort CODE_NEXT = 85;

	public const ushort CODE_BREAK = 86;

	public const ushort CODE_ECALL = 57624;

	public const ushort CODE_DW = 87;

	public const ushort CODE_LDA = 88;

	public const ushort CODE_STA = 89;

	public const ushort CODE_TMIN = 90;

	public const ushort CODE_MOV = 91;

	public const ushort CODE_BMOV = 92;

	public const ushort CODE_FMOV = 93;

	public const ushort CODE_ADD = 94;

	public const ushort CODE_SUB = 95;

	public const ushort CODE_MUL = 96;

	public const ushort CODE_DIV = 97;

	public const ushort CODE_INC = 98;

	public const ushort CODE_DEC = 99;

	public const ushort CODE_ROOT = 100;

	public const ushort CODE_CMP = 101;

	public const ushort CODE_ZCMP = 102;

	public const ushort CODE_ANDA = 103;

	public const ushort CODE_ORA = 104;

	public const ushort CODE_EORA = 105;

	public const ushort CODE_COM = 106;

	public const ushort CODE_NEG = 107;

	public const ushort CODE_SRA = 108;

	public const ushort CODE_SLA = 109;

	public const ushort CODE_RRA = 110;

	public const ushort CODE_RLA = 111;

	public const ushort CODE_ENRA = 57857;

	public const ushort CODE_POW = 57897;

	public const ushort CODE_TBCD = 114;

	public const ushort CODE_TBIN = 115;

	public const ushort CODE_MPX = 116;

	public const ushort CODE_DMX = 117;

	public const ushort CODE_GRY = 118;

	public const ushort CODE_RGRY = 119;

	public const ushort CODE_SWAP = 124;

	public const ushort CODE_XCH = 125;

	public const ushort CODE_FLOAT = 126;

	public const ushort CODE_INTG = 127;

	public const ushort CODE_BSWAP = 58197;

	public const ushort CODE_CJ = 57600;

	public const ushort CODE_NCJ = 57614;

	public const ushort CODE_GOTO = 57601;

	public const ushort CODE_LABEL = 57602;

	public const ushort CODE_SCJ = 57623;

	public const ushort CODE_DFLOAT = 57903;

	public const ushort CODE_DINTG = 57904;

	public const ushort CODE_FTODF = 57905;

	public const ushort CODE_DFTOF = 57906;

	public const ushort CODE_DISF = 129;

	public const ushort CODE_UNIF = 130;

	public const ushort CODE_EXP = 131;

	public const ushort CODE_LOG = 132;

	public const ushort CODE_LOG10 = 57898;

	public const ushort CODE_RAD = 133;

	public const ushort CODE_DEG = 134;

	public const ushort CODE_SIN = 135;

	public const ushort CODE_COS = 136;

	public const ushort CODE_TAN = 137;

	public const ushort CODE_ASIN = 138;

	public const ushort CODE_ACOS = 139;

	public const ushort CODE_ATAN = 140;

	public const ushort CODE_ASC = 141;

	public const ushort CODE_RASC = 142;

	public const ushort CODE_DASC = 143;

	public const ushort CODE_RDASC = 144;

	public const ushort CODE_HASC = 57864;

	public const ushort CODE_RHASC = 57865;

	public const ushort CODE_FASC = 57866;

	public const ushort CODE_RFASC = 57867;

	public const ushort CODE_SMOV = 145;

	public const ushort CODE_SADD = 146;

	public const ushort CODE_SCMP = 147;

	public const ushort CODE_DISS = 148;

	public const ushort CODE_UNIS = 149;

	public const ushort CODE_RCOM = 151;

	public const ushort CODE_SRGHT = 57868;

	public const ushort CODE_SLEFT = 57869;

	public const ushort CODE_SMID = 57870;

	public const ushort CODE_SRPLC = 57871;

	public const ushort CODE_SINS = 57873;

	public const ushort CODE_SDEL = 57872;

	public const ushort CODE_STRIM = 57900;

	public const ushort CODE_SFIND = 57874;

	public const ushort CODE_SFINDN = 57901;

	public const ushort CODE_CPSASC = 58198;

	public const ushort CODE_RCPSASC = 58199;

	public const ushort CODE_LEN = 150;

	public const ushort CODE_HKEY = 152;

	public const ushort CODE_BCNT = 153;

	public const ushort CODE_DCNT = 154;

	public const ushort CODE_SER = 155;

	public const ushort CODE_MAX = 156;

	public const ushort CODE_MIN = 157;

	public const ushort CODE_AVG = 158;

	public const ushort CODE_ZRES = 159;

	public const ushort CODE_ESUM = 58113;

	public const ushort CODE_CRC = 58114;

	public const ushort CODE_WSUM = 58155;

	public const ushort CODE_DSER = 58163;

	public const ushort CODE_EXT = 162;

	public const ushort CODE_HSP = 170;

	public const ushort CODE_DI = 171;

	public const ushort CODE_EI = 172;

	public const ushort CODE_INT = 173;

	public const ushort CODE_RETI = 174;

	public const ushort CODE_HLINE = 255;

	public const ushort CODE_NEXTLINE_BEGIN = 189;

	public const ushort CODE_NEXTLINE_END = 190;

	public const ushort CODE_MCALL = 57604;

	public const ushort CODE_MSTRT = 57605;

	public const ushort CODE_MEND = 57606;

	public const ushort CODE_MDSTRT = 57607;

	public const ushort CODE_MDSTOP = 57608;

	public const ushort CODE_ZPUSH = 57615;

	public const ushort CODE_ZPOP = 57616;

	public const ushort CODE_ADRSET = 57609;

	public const ushort CODE_ADRINC = 57610;

	public const ushort CODE_ADRDEC = 57611;

	public const ushort CODE_ADRADD = 57612;

	public const ushort CODE_ADRSUB = 57613;

	public const ushort CODE_UREAD = 57618;

	public const ushort CODE_UWRIT = 57619;

	public const ushort CODE_UFILL = 57620;

	public const ushort CODE_PMOV = 57856;

	public const ushort CODE_PLDA = 57893;

	public const ushort CODE_PSTA = 57894;

	public const ushort CODE_BYLMOV = 58195;

	public const ushort CODE_BYBMOV = 58196;

	public const ushort CODE_ASRA = 57907;

	public const ushort CODE_ASLA = 57908;

	public const ushort CODE_RRNCA = 57860;

	public const ushort CODE_RLNCA = 57861;

	public const ushort CODE_BCMP = 58115;

	public const ushort CODE_BCMPI = 58116;

	public const ushort CODE_RND = 57895;

	public const ushort CODE_SORT = 58188;

	public const ushort CODE_SORTN = 58189;

	public const ushort CODE_FIFOW = 160;

	public const ushort CODE_FIFOR = 161;

	public const ushort CODE_LIFOW = 58147;

	public const ushort CODE_LIFOR = 58117;

	public const ushort CODE_FWRIT = 58118;

	public const ushort CODE_FINS = 58119;

	public const ushort CODE_FDEL = 58120;

	public const ushort CODE_WTIME = 164;

	public const ushort CODE_SEC = 165;

	public const ushort CODE_RSEC = 166;

	public const ushort CODE_AJST = 167;

	public const ushort CODE_LDWK = 58121;

	public const ushort CODE_LDWKB = 58122;

	public const ushort CODE_LDCAL = 58127;

	public const ushort CODE_LDCALB = 58128;

	public const ushort CODE_ARES = 168;

	public const ushort CODE_DIC = 58133;

	public const ushort CODE_CTH = 175;

	public const ushort CODE_CTC = 176;

	public const ushort CODE_PSTRT = 58207;

	public const ushort CODE_JOG = 58208;

	public const ushort CODE_ORG = 58209;

	public const ushort CODE_TCH = 58210;

	public const ushort CODE_HOME = 58211;

	public const ushort CODE_CHGSP = 58212;

	public const ushort CODE_CHGTGT = 58213;

	public const ushort CODE_RFSPS = 58214;

	public const ushort CODE_MCMP = 58148;

	public const ushort CODE_ABSENC = 58136;

	public const ushort CODE_INCENC = 58137;

	public const ushort CODE_FCNT = 58152;

	public const ushort CODE_RCNT = 58153;

	public const ushort CODE_PLSOUT = 58154;

	public const ushort CODE_PID = 58138;

	public const ushort CODE_PIDAT = 58194;

	public const ushort CODE_LOGE = 58140;

	public const ushort CODE_LOGD = 58141;

	public const ushort CODE_TRGD = 58162;

	public const ushort CODE_MWRIT = 58142;

	public const ushort CODE_MREAD = 58143;

	public const ushort CODE_MFREE = 58144;

	public const ushort CODE_MMKDIR = 58149;

	public const ushort CODE_MRMDIR = 58160;

	public const ushort CODE_MDEL = 58161;

	public const ushort CODE_MPRINT = 58181;

	public const ushort CODE_MREADL = 58182;

	public const ushort CODE_MCOPY = 58183;

	public const ushort CODE_MMOV = 58184;

	public const ushort CODE_MREN = 58185;

	public const ushort CODE_MFREEK = 58186;

	public const ushort CODE_MSTAT = 58187;

	public const ushort CODE_AWNUM = 58150;

	public const ushort CODE_AWMSG = 58151;

	public const ushort CODE_RFSCTH = 58156;

	public const ushort CODE_RFSFRC = 58159;

	public const ushort CODE_SPRD = 58202;

	public const ushort CODE_SPWR = 58203;

	public const ushort CODE_SSVC = 58204;

	public const ushort CODE_RFSCI = 58205;

	public const ushort CODE_RFSCO = 58206;

	public const ushort CODE_WSR = 112;

	public const ushort CODE_WSL = 113;

	public const ushort CODE_BSR = 57858;

	public const ushort CODE_BSL = 57859;

	public const ushort CODE_LIMIT = 57875;

	public const ushort CODE_BANDC = 57876;

	public const ushort CODE_ZONE = 57877;

	public const ushort CODE_APR = 57878;

	public const ushort CODE_RAMP = 58191;

	public const ushort CODE_TPOUT = 58192;

	public const ushort CODE_LLFLT = 58193;

	public const ushort CODE_DSWAP = 58197;

	public const ushort CODE_CALADD = 57879;

	public const ushort CODE_CALSUB = 57880;

	public const ushort CODE_CALMUL = 57881;

	public const ushort CODE_CALDIV = 57882;

	public const ushort CODE_CALAND = 57883;

	public const ushort CODE_CALOR = 57884;

	public const ushort CODE_CALXOR = 57885;

	public const ushort CODE_CALNOT = 57886;

	public const ushort CODE_CALSHR = 57887;

	public const ushort CODE_CALSHL = 57888;

	public const ushort CODE_DISN = 57889;

	public const ushort CODE_UNIN = 57890;

	public const ushort CODE_DISB = 57891;

	public const ushort CODE_UNIB = 57892;

	public const ushort CODE_DECO = 57862;

	public const ushort CODE_ENCO = 57863;

	public const ushort CODE_ABS = 57896;

	public const ushort CODE_CPMSET = 58201;

	public const ushort CODE_CPMGET = 58200;

	public const ushort CODE_SEG = 58112;

	public const ushort CODE_FRSET = 57617;

	public const ushort CODE_FRSTM = 57621;

	public const ushort CODE_FRLDM = 57622;

	public const ushort CODE_BSUM = 58155;

	public const ushort CODE_PLSX = 177;

	public const ushort CODE_PLSY = 178;

	public const ushort CODE_JOGX = 179;

	public const ushort CODE_JOGY = 180;

	public const ushort CODE_ORGX = 181;

	public const ushort CODE_ORGY = 182;

	public const ushort CODE_TCHX = 183;

	public const ushort CODE_TCHY = 184;

	public const ushort CODE_HOMEX = 58134;

	public const ushort CODE_HOMEY = 58135;

	public const ushort CODE_CHGSPX = 58145;

	public const ushort CODE_CHGSPY = 58146;

	public const ushort CODE_RFSPSX = 58157;

	public const ushort CODE_RFSPSY = 58158;

	public const ushort CODE_NULL = 65534;

	public const byte SUFFIX_U = 0;

	public const byte SUFFIX_S = 1;

	public const byte SUFFIX_D = 2;

	public const byte SUFFIX_L = 3;

	public const byte SUFFIX_F = 4;

	public const byte SUFFXI_DF = 5;

	protected KVSLadder parent;

	protected int step;

	protected int x;

	protected int y;

	protected ushort code;

	protected byte row;

	protected byte flag;

	protected KVSActionSet actionset;

	protected KVSArg[] oargs;

	protected List<KVSArg> args;

	public KVSLadder Parent
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

	public int Step
	{
		get
		{
			return step;
		}
		set
		{
			step = value;
		}
	}

	public int X
	{
		get
		{
			return x;
		}
		set
		{
			x = value;
		}
	}

	public int Y
	{
		get
		{
			return y;
		}
		set
		{
			y = value;
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

	public byte Row
	{
		get
		{
			return row;
		}
		set
		{
			row = value;
		}
	}

	public byte Flag
	{
		get
		{
			return flag;
		}
		set
		{
			flag = value;
		}
	}

	public KVSActionSet ActionSet
	{
		get
		{
			return actionset;
		}
		set
		{
			actionset = value;
		}
	}

	public KVSArg[] OArgs => oargs;

	public IList<KVSArg> Args => args;

	public KVSUnitFormat Format => KVSTranslator.FormatOfCodes[code];

	public bool IsLD
	{
		get
		{
			KVSUnitFormat format = Format;
			return format != null && format.Shape == KVSUnitShape.InputIn;
		}
	}

	public bool IsST
	{
		get
		{
			KVSUnitFormat format = Format;
			return format != null && format.Shape == KVSUnitShape.InputOut;
		}
	}

	public bool IsIn
	{
		get
		{
			KVSUnitFormat format = Format;
			return format != null && format.Shape == KVSUnitShape.InputCalc;
		}
	}

	public KVSUnit(KVSLadder _parent)
	{
		parent = _parent;
		oargs = new KVSArg[3];
		args = new List<KVSArg>();
	}

	public virtual KVSUnit Create()
	{
		return new KVSUnit(parent);
	}

	public virtual KVSUnit Clone()
	{
		KVSUnit kVSUnit = Create();
		kVSUnit.X = X;
		kVSUnit.Y = Y;
		kVSUnit.Code = Code;
		kVSUnit.Row = Row;
		for (int i = 0; i < kVSUnit.OArgs.Length; i++)
		{
			kVSUnit.OArgs[i] = OArgs[i]?.Clone();
			if (kVSUnit.OArgs[i] != null)
			{
				kVSUnit.OArgs[i].Parent = kVSUnit;
			}
		}
		return kVSUnit;
	}
}
