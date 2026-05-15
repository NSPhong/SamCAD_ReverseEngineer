namespace SamSoarII.Utility.Files.KVS;

public class KVSLocalLabel
{
	public const ushort BOOL = 1;

	public const ushort INT16 = 4;

	public const ushort UINT16 = 2;

	public const ushort INT32 = 5;

	public const ushort UINT32 = 3;

	public const ushort FLOAT = 6;

	public const ushort DOUBLE = 10;

	public const ushort TIMER = 8;

	public const ushort COUNTER = 9;

	public const ushort STRING = 7;

	protected ushort valuetype;

	protected object constvalue;

	protected ushort isarray;

	protected uint arraycount;

	protected int cmid;

	protected string name;

	protected string comment;

	public ushort ValueType
	{
		get
		{
			return valuetype;
		}
		set
		{
			valuetype = value;
		}
	}

	public object ConstValue
	{
		get
		{
			return constvalue;
		}
		set
		{
			constvalue = value;
		}
	}

	public ushort IsArray
	{
		get
		{
			return isarray;
		}
		set
		{
			isarray = value;
		}
	}

	public uint ArrayCount
	{
		get
		{
			return arraycount;
		}
		set
		{
			arraycount = value;
		}
	}

	public int CMID
	{
		get
		{
			return cmid;
		}
		set
		{
			cmid = value;
		}
	}

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
}
