namespace SamSoarII.Utility.Files.KVS;

public class KVSProInfo
{
	public const ushort CODE_KV_P16 = 5;

	public const ushort CODE_KV_10 = 41;

	public const ushort CODE_KV_24 = 42;

	public const ushort CODE_KV_700 = 48;

	public const ushort CODE_KV_700_M = 49;

	public const ushort CODE_KV_N14 = 135;

	public const ushort CODE_KV_N24 = 134;

	public const ushort CODE_KV_N40 = 133;

	public const ushort CODE_KV_N60 = 132;

	public const ushort CODE_KV_NC32 = 128;

	public const ushort CODE_KV_1000 = 50;

	public const ushort CODE_KV_3000 = 51;

	public const ushort CODE_KV_5000 = 52;

	public const ushort CODE_KV_5500 = 53;

	protected ushort device;

	protected string name;

	protected string comment;

	public ushort Device
	{
		get
		{
			return device;
		}
		set
		{
			device = value;
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
