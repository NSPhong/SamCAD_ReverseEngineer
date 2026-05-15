namespace SamSoarII.Utility.Files.XD;

public class XDFuncBlock
{
	private XDProject parent;

	private string name;

	private string code;

	public XDProject Parent
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

	public string Code
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

	public XDFuncBlock(XDProject _parent)
	{
		parent = _parent;
	}
}
