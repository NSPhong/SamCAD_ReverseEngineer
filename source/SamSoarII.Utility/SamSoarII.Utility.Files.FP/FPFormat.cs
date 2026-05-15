using System.Collections.Generic;

namespace SamSoarII.Utility.Files.FP;

public class FPFormat
{
	public const uint MAINCODE_MASK_LINK = 255u;

	public const uint MAINCODE_H_LEFT = 1u;

	public const uint MAINCODE_V_TOP = 2u;

	public const uint MAINCODE_H_RIGHT = 4u;

	public const uint MAINCODE_V_BOTTOM = 8u;

	public const uint MAINCODE_MASK_BASE = 4278190080u;

	public const uint MAINCODE_INPUT = 16777216u;

	public const uint MAINCODE_HLINE = 67108864u;

	public const uint MAINCODE_MASK_REGI = 65280u;

	public const uint MAINCODE_X = 0u;

	public const uint MAINCODE_Y = 256u;

	public const uint MAINCODE_R = 512u;

	public const uint MAINCODE_L = 768u;

	public const uint MAINCODE_T = 1024u;

	public const uint MAINCODE_C = 1280u;

	public const uint MAINCODE_P = 1536u;

	public const uint MAINCODE_E = 1792u;

	public const uint MAINCODE_SR = 2048u;

	public const uint MAINCODE_CS = 3072u;

	public const uint MAINCODE_CE = 3328u;

	public const uint MAINCODE_WX = 4096u;

	public const uint MAINCODE_WY = 4352u;

	public const uint MAINCODE_WR = 4608u;

	public const uint MAINCODE_WL = 4864u;

	public const uint MAINCODE_DT = 5376u;

	public const uint MAINCODE_LD = 5632u;

	public const uint MAINCODE_SD = 5888u;

	public const uint MAINCODE_SV = 6912u;

	public const uint MAINCODE_EV = 7168u;

	public const uint MAINCODE_I = 7424u;

	public const uint MAINCODE_K = 10240u;

	public const uint MAINCODE_U = 10496u;

	public const uint MAINCODE_SF = 11008u;

	public const uint MAINCODE_DF = 11264u;

	public const uint MAINCODE_FL = 12544u;

	public const uint POSICODE_MASK_SUFF = 983040u;

	public const uint POSICODE_BIT = 0u;

	public const uint POSICODE_US = 131072u;

	public const uint POSICODE_UL = 196608u;

	public const uint POSICODE_SF = 327680u;

	public const uint POSICODE_SS = 655360u;

	public const uint POSICODE_SL = 720896u;

	public const uint POSICODE_DF = 851968u;

	public const uint POSICODE_MASK_I_USED = 251658240u;

	public const uint POSICODE_MASK_I_OFFSET = 15728640u;

	public const uint IDENCODE_MASK_1 = 4294901760u;

	public const uint IDENCODE_MASK_2 = 65535u;

	public static readonly FPFormatGroup Units;

	public static readonly FPFormatGroup Values;

	public static readonly Dictionary<string, FPFormat> ItemOfNames;

	protected string name;

	protected long offset;

	protected uint maincode;

	protected uint posicode;

	protected uint idencode;

	protected bool isusesoffset;

	protected bool isusemainregi;

	public string Name
	{
		get
		{
			return name;
		}
		set
		{
			name = value;
		}
	}

	public long Offset => offset;

	public uint MainCode => maincode;

	public uint PosiCode => posicode;

	public uint IdenCode => idencode;

	public bool IsUseOffset
	{
		get
		{
			return isusesoffset;
		}
		set
		{
			isusesoffset = value;
		}
	}

	public bool IsUseMainRegi
	{
		get
		{
			return isusemainregi;
		}
		set
		{
			isusemainregi = value;
		}
	}

	public int Width => (int)((posicode >> 8) & 0xF);

	public int Height => (int)(posicode & 0xF);

	public int XOfWidth => (int)((posicode >> 12) & 0xF);

	public int YOfHeight => (int)((posicode >> 4) & 0xF);

	static FPFormat()
	{
		Units = new FPFormatGroup_MainCode();
		Values = new FPFormatGroup_MainRegi();
		ItemOfNames = new Dictionary<string, FPFormat>();
		Values.Set(new FPValueFormat(0u, "X"));
		Values.Set(new FPValueFormat(256u, "Y"));
		Values.Set(new FPValueFormat(512u, "R"));
		Values.Set(new FPValueFormat(768u, "L"));
		Values.Set(new FPValueFormat(1024u, "T"));
		Values.Set(new FPValueFormat(1280u, "C"));
		Values.Set(new FPValueFormat(1536u, "P"));
		Values.Set(new FPValueFormat(1792u, "E"));
		Values.Set(new FPValueFormat(3072u, "CS"));
		Values.Set(new FPValueFormat(3328u, "CE"));
		Values.Set(new FPValueFormat(2048u, "SR"));
		Values.Set(new FPValueFormat(4096u, "WX"));
		Values.Set(new FPValueFormat(4352u, "WY"));
		Values.Set(new FPValueFormat(4608u, "WR"));
		Values.Set(new FPValueFormat(4864u, "WL"));
		Values.Set(new FPValueFormat(5376u, "DT"));
		Values.Set(new FPValueFormat(5632u, "LD"));
		Values.Set(new FPValueFormat(5888u, "SD"));
		Values.Set(new FPValueFormat(6912u, "SV"));
		Values.Set(new FPValueFormat(7168u, "EV"));
		Values.Set(new FPValueFormat(7424u, "I"));
		Values.Set(new FPValueFormat(10240u, "K"));
		Values.Set(new FPValueFormat(10496u, "U"));
		Values.Set(new FPValueFormat(11008u, "SF"));
		Values.Set(new FPValueFormat(11264u, "DF"));
		Values.Set(new FPValueFormat(12544u, "FL"));
		foreach (FPValueFormat item in Values.Items)
		{
			ItemOfNames.Add(item.Name, item);
		}
	}

	public FPFormat(long _offset, uint _maincode, uint _posicode, uint _idencode)
	{
		name = "FPFormat";
		offset = _offset;
		maincode = _maincode;
		posicode = _posicode;
		idencode = _idencode;
		isusesoffset = false;
		isusemainregi = false;
	}
}
