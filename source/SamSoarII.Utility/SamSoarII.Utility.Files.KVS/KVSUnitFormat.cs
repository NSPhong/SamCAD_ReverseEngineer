using System.Collections.Generic;

namespace SamSoarII.Utility.Files.KVS;

public class KVSUnitFormat
{
	protected string name;

	protected ushort code;

	protected KVSUnitShape shape;

	protected List<KVSArgFormat> args = new List<KVSArgFormat>();

	protected int width = 1;

	protected int height = 1;

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

	public KVSUnitShape Shape
	{
		get
		{
			return shape;
		}
		set
		{
			shape = value;
		}
	}

	public IList<KVSArgFormat> Args => args;

	public int Width
	{
		get
		{
			return width;
		}
		set
		{
			width = value;
		}
	}

	public int Height
	{
		get
		{
			return height;
		}
		set
		{
			height = value;
		}
	}
}
