namespace SamSoarII.Utility.Files.Step7;

public class S7UnitFormat
{
	private S7Unit core;

	private string name;

	private int net;

	private int x;

	private int y;

	public S7Unit Core
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

	public int Code => core?.Code ?? (-1);
}
