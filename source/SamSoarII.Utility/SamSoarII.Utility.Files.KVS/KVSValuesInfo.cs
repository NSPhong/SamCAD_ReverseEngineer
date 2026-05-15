namespace SamSoarII.Utility.Files.KVS;

public class KVSValuesInfo
{
	private ushort code;

	private uint minoffset;

	private uint maxoffset;

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

	public uint MinOffset
	{
		get
		{
			return minoffset;
		}
		set
		{
			minoffset = value;
		}
	}

	public uint MaxOffset
	{
		get
		{
			return maxoffset;
		}
		set
		{
			maxoffset = value;
		}
	}

	public KVSValuesInfo Clone()
	{
		return new KVSValuesInfo
		{
			Code = code,
			MinOffset = minoffset,
			MaxOffset = maxoffset
		};
	}

	public bool Include(KVSValue v)
	{
		return v.Code == code && v.Offset >= minoffset && v.Offset <= maxoffset;
	}
}
