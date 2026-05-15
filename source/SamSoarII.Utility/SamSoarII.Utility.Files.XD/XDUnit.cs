using System.Collections.Generic;
using System.Text;

namespace SamSoarII.Utility.Files.XD;

public class XDUnit
{
	private XDLadder parent;

	private int x;

	private int y;

	private int instcode;

	private string instname;

	private List<string> args = new List<string>();

	public XDLadder Parent
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

	public int InstCode
	{
		get
		{
			return instcode;
		}
		set
		{
			instcode = value;
		}
	}

	public string InstName
	{
		get
		{
			return instname;
		}
		set
		{
			instname = value;
		}
	}

	public IList<string> Args => args;

	public XDUnit(XDLadder _parent)
	{
		parent = _parent;
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append($"{instname}({instcode})");
		foreach (string arg in args)
		{
			stringBuilder.Append($" {arg}");
		}
		return stringBuilder.ToString();
	}

	public bool IsSBRHeader(out int sbrid)
	{
		sbrid = -1;
		if (instname != null && instname.Length > 1 && instname[0] == 'P' && int.TryParse(instname.Substring(1), out sbrid))
		{
			return true;
		}
		return false;
	}

	public bool IsINTHeader(out int intid)
	{
		intid = -1;
		if (instname != null && instname.Length > 1 && instname[0] == 'I' && int.TryParse(instname.Substring(1), out intid))
		{
			return true;
		}
		return false;
	}
}
