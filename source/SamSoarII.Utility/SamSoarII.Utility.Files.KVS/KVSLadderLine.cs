namespace SamSoarII.Utility.Files.KVS;

public class KVSLadderLine
{
	public const uint STARTCODE_NONE = 0u;

	public const uint STARTCODE_LINECOMMENT = 1u;

	public const uint STARTCODE_ZONESCRIPT = 2u;

	public const uint STARTCODE_SCRIPTLADDER = 6657u;

	public const uint STARTCODE_SCRIPTLINECOMMENT = 2560u;

	public const uint ENDCODE_LINECOMMENT = 165u;

	public const uint ENDCODE_LADDER = 1005u;

	protected KVSLadder parent;

	protected int y;

	protected byte[] data;

	protected uint startcode;

	protected uint endcode;

	protected int eaid;

	protected int rsid;

	protected string comment;

	protected string rectscript;

	protected string zonescript;

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

	public byte[] Data
	{
		get
		{
			return data;
		}
		set
		{
			data = value;
		}
	}

	public uint StartCode
	{
		get
		{
			return startcode;
		}
		set
		{
			startcode = value;
		}
	}

	public uint EndCode
	{
		get
		{
			return endcode;
		}
		set
		{
			endcode = value;
		}
	}

	public int EAID
	{
		get
		{
			return eaid;
		}
		set
		{
			eaid = value;
		}
	}

	public int RSID
	{
		get
		{
			return rsid;
		}
		set
		{
			rsid = value;
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

	public string RectScript
	{
		get
		{
			return rectscript;
		}
		set
		{
			rectscript = value;
		}
	}

	public string ZoneScript
	{
		get
		{
			return zonescript;
		}
		set
		{
			zonescript = value;
		}
	}

	public bool IsLineComment => startcode == 1 || startcode == 2560;

	public bool IsZoneScript => startcode == 2;

	public bool IsRectScript => rsid > 0;

	public bool IsScriptGenerated => startcode == 6657 || startcode == 2560;

	public KVSLadderLine(KVSLadder _parent)
	{
		parent = _parent;
	}
}
