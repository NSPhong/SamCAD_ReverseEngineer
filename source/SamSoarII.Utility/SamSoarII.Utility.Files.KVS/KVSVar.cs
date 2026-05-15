namespace SamSoarII.Utility.Files.KVS;

public class KVSVar
{
	public const ushort CODE_P = 60;

	public const ushort CODE_V = 59;

	public const ushort CODE_UR = 57;

	public const ushort CODE_UV = 56;

	public const ushort VT_BOOL = 4;

	public const ushort VT_UINT16 = 0;

	public const ushort VT_INT16 = 1;

	public const ushort VT_UINT32 = 2;

	public const ushort VT_INT32 = 3;

	public const ushort VT_FLOAT = 5;

	public const ushort VT_DOUBLE = 8;

	public const ushort VT_STRING = 6;

	protected ushort code;

	protected ushort offset;

	protected ushort valuetype;

	protected string title;

	protected string description;

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

	public ushort Offset
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

	public string Title
	{
		get
		{
			return title;
		}
		set
		{
			title = value;
		}
	}

	public string Description
	{
		get
		{
			return description;
		}
		set
		{
			description = value;
		}
	}
}
