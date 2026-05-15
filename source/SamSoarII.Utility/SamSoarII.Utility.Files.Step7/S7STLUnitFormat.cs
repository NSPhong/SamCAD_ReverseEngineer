namespace SamSoarII.Utility.Files.Step7;

public class S7STLUnitFormat
{
	private S7STLStmt core;

	private string name;

	private int net;

	private int y;

	public S7STLStmt Core
	{
		get
		{
			return core;
		}
		set
		{
			core = value;
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

	public int Net
	{
		get
		{
			return net;
		}
		set
		{
			net = value;
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

	public int Code => core?.Code ?? (-1);

	public S7STLUnitFormat()
	{
	}

	public S7STLUnitFormat(S7UnitFormat _source)
	{
		core = new S7STLStmt(_source.Core.Parent, _source.Core);
		name = _source.Name;
		net = _source.Net;
		y = 0;
	}
}
