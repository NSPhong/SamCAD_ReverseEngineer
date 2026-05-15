namespace SamSoarII.Utility.Files.Step7;

public class S7FBDUnitFormat
{
	private S7FBDUnit core;

	private string name;

	private int net;

	private int x;

	private int y;

	public S7FBDUnit Core
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

	public S7FBDUnitFormat()
	{
	}

	public S7FBDUnitFormat(S7UnitFormat _source)
	{
		core = new S7FBDUnit(_source.Core.Parent, _source.Core);
		name = _source.Name;
		net = _source.Net;
		x = _source.X;
		y = _source.Y;
	}
}
