namespace SamSoarII.Utility.Files.KVS;

public class KVSModInfo
{
	public const ushort CODE_HOLD_MODULE = 0;

	public const ushort CODE_INIT_MODULE = 1;

	public const ushort CODE_MODULE = 2;

	public const ushort CODE_PERIOD_MODULE = 3;

	public const ushort CODE_MACRO = 4;

	public const ushort CODE_HOLD_MACRO = 5;

	protected ushort code;

	protected string name;

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
}
