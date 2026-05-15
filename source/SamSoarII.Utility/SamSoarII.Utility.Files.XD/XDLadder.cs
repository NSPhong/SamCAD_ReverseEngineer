using System.Collections.Generic;

namespace SamSoarII.Utility.Files.XD;

public class XDLadder
{
	private XDProject parent;

	private string name;

	private int sbrid;

	private int intid;

	private int width;

	private int height;

	private GridDictionary<XDUnit> children;

	private GridDictionary<XDUnit> vlines;

	private List<XDLineComment> lines;

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

	public int SBRID
	{
		get
		{
			return sbrid;
		}
		set
		{
			sbrid = value;
		}
	}

	public int INTID
	{
		get
		{
			return intid;
		}
		set
		{
			intid = value;
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

	public GridDictionary<XDUnit> Children => children;

	public GridDictionary<XDUnit> VLines => vlines;

	public IList<XDLineComment> Lines => lines;

	public XDLadder(XDProject _parent, string _name)
	{
		parent = _parent;
		name = _name;
		children = new GridDictionary<XDUnit>(12);
		vlines = new GridDictionary<XDUnit>(12);
		lines = new List<XDLineComment>();
		width = 12;
		height = 1;
		sbrid = -1;
		intid = -1;
	}

	public void Load(XDLadder src, int sy, int h)
	{
		height = h;
		for (int i = sy; i < sy + h; i++)
		{
			for (int j = 0; j < src.Width; j++)
			{
				XDUnit xDUnit = src.Children[j, i];
				XDUnit xDUnit2 = src.VLines[j, i];
				if (xDUnit != null)
				{
					XDUnit xDUnit3 = new XDUnit(this);
					xDUnit3.X = j;
					xDUnit3.Y = i - sy;
					xDUnit3.InstName = xDUnit.InstName;
					xDUnit3.InstCode = xDUnit.InstCode;
					foreach (string arg in xDUnit.Args)
					{
						xDUnit3.Args.Add(arg);
					}
					children[xDUnit3.X, xDUnit3.Y] = xDUnit3;
				}
				if (xDUnit2 != null)
				{
					XDUnit xDUnit4 = new XDUnit(this);
					xDUnit4.X = j;
					xDUnit4.Y = i - sy;
					xDUnit4.InstName = "VLINE";
					vlines[xDUnit4.X, xDUnit4.Y] = xDUnit4;
				}
			}
		}
	}
}
