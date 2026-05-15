namespace SamSoarII.Utility.Files.FP;

public class FPUnit : FPFormat
{
	private FPNetwork network;

	private int x;

	private int y;

	private FPFormat format;

	private FPUnit parent;

	private FPUnitConvert convert;

	public FPNetwork Network => network;

	public int X => x;

	public int Y => y;

	public FPFormat Format
	{
		get
		{
			return format;
		}
		set
		{
			format = value;
		}
	}

	public FPUnit Parent
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

	public FPUnitConvert Convert
	{
		get
		{
			return convert;
		}
		set
		{
			convert = value;
		}
	}

	public FPUnit(FPNetwork _network, int _x, int _y, long _offset, uint _maincode, uint _posicode, uint _idencode)
		: base(_offset, _maincode, _posicode, _idencode)
	{
		network = _network;
		name = "FPUnit";
		x = _x;
		y = _y;
		parent = null;
		convert = null;
	}

	public override string ToString()
	{
		if ((maincode & 0xFF000000u) == 67108864)
		{
			return "HLINE";
		}
		FPFormat fPFormat = format;
		if (fPFormat != null && fPFormat.IsUseMainRegi)
		{
			return $"{format.Name}{ToRegiString()}";
		}
		FPFormat fPFormat2 = format;
		if (fPFormat2 != null && fPFormat2.IsUseOffset)
		{
			return $"{format.Name}{FPValueFormat.ToOffsetString(maincode, offset)}";
		}
		if (format != null)
		{
			return format.Name;
		}
		if (parent != null)
		{
			return ToRegiString();
		}
		return string.Empty;
	}

	protected string ToRegiString()
	{
		return (maincode & 0xFF00) switch
		{
			0u => $"X{FPValueFormat.ToOffsetString(maincode, offset)}", 
			256u => $"Y{FPValueFormat.ToOffsetString(maincode, offset)}", 
			512u => $"R{FPValueFormat.ToOffsetString(maincode, offset)}", 
			768u => $"L{FPValueFormat.ToOffsetString(maincode, offset)}", 
			1024u => $"T{FPValueFormat.ToOffsetString(maincode, offset)}", 
			1280u => $"C{FPValueFormat.ToOffsetString(maincode, offset)}", 
			1536u => $"P{FPValueFormat.ToOffsetString(maincode, offset)}", 
			1792u => $"E{FPValueFormat.ToOffsetString(maincode, offset)}", 
			2048u => $"SR{FPValueFormat.ToOffsetString(maincode, offset)}", 
			4096u => $"WX{FPValueFormat.ToOffsetString(maincode, offset)}", 
			4352u => $"WY{FPValueFormat.ToOffsetString(maincode, offset)}", 
			4608u => $"WR{FPValueFormat.ToOffsetString(maincode, offset)}", 
			4864u => $"WL{FPValueFormat.ToOffsetString(maincode, offset)}", 
			5376u => $"DT{FPValueFormat.ToOffsetString(maincode, offset)}", 
			5632u => $"LD{FPValueFormat.ToOffsetString(maincode, offset)}", 
			5888u => $"SD{FPValueFormat.ToOffsetString(maincode, offset)}", 
			6912u => $"SV{FPValueFormat.ToOffsetString(maincode, offset)}", 
			7168u => $"EV{FPValueFormat.ToOffsetString(maincode, offset)}", 
			10240u => $"K{FPValueFormat.ToOffsetString(maincode, offset)}", 
			10496u => $"U{FPValueFormat.ToOffsetString(maincode, offset)}", 
			11008u => $"SF{FPValueFormat.ToOffsetString(maincode, offset)}", 
			11264u => $"DF{FPValueFormat.ToOffsetString(maincode, offset)}", 
			_ => string.Empty, 
		};
	}
}
