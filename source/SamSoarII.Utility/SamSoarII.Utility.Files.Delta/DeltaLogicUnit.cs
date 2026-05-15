using System.Collections.Generic;

namespace SamSoarII.Utility.Files.Delta;

public class DeltaLogicUnit
{
	private DeltaLogicUnit parent;

	private int logicx;

	private int logicy;

	private int x;

	private int y;

	private int width;

	private int height;

	private DeltaUnit core;

	private List<DeltaLogicUnit> items;

	public DeltaLogicUnit Parent
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

	public int LogicX
	{
		get
		{
			return logicx;
		}
		set
		{
			logicx = value;
		}
	}

	public int LogicY
	{
		get
		{
			return logicy;
		}
		set
		{
			logicy = value;
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

	public DeltaUnit Core
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

	public List<DeltaLogicUnit> Items => items;

	public DeltaLogicUnit()
	{
		width = 1;
		height = 1;
		items = new List<DeltaLogicUnit>();
	}
}
