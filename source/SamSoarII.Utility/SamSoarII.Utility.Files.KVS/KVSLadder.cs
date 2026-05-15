using System.Collections.Generic;

namespace SamSoarII.Utility.Files.KVS;

public class KVSLadder
{
	public static readonly byte[] LineStart = new byte[12]
	{
		0, 0, 0, 0, 146, 1, 0, 0, 0, 0,
		0, 0
	};

	private KVSModule parent;

	private bool issuccess;

	private string name;

	private int rowcount;

	private int colcount;

	private int sbrid;

	private int intid;

	private GridDictionary<KVSUnit> children;

	private GridDictionary<KVSUnit> vlines;

	private List<KVSLadderLine> lines;

	public KVSModule Parent
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

	public bool IsSuccess => issuccess;

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

	public int RowCount
	{
		get
		{
			return rowcount;
		}
		set
		{
			rowcount = value;
		}
	}

	public int ColCount
	{
		get
		{
			return colcount;
		}
		set
		{
			colcount = value;
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

	public GridDictionary<KVSUnit> Children => children;

	public GridDictionary<KVSUnit> VLines => vlines;

	public IList<KVSLadderLine> Lines => lines;

	public KVSLadder()
	{
		issuccess = false;
		children = new GridDictionary<KVSUnit>(10);
		vlines = new GridDictionary<KVSUnit>(10);
		rowcount = 0;
		colcount = 10;
		name = "KVS ladder diagram";
		sbrid = -1;
		intid = -1;
	}

	public KVSLadder(KVSFileStream s)
	{
		issuccess = true;
		children = new GridDictionary<KVSUnit>(10);
		vlines = new GridDictionary<KVSUnit>(10);
		lines = new List<KVSLadderLine>();
		colcount = 10;
		name = "KVS ladder diagram";
		sbrid = -1;
		intid = -1;
		uint num = s.ReadUInt();
		rowcount = (int)s.ReadUInt();
		s.Skip(32);
		for (int i = 0; i < rowcount; i++)
		{
			KVSLadderLine kVSLadderLine = new KVSLadderLine(this)
			{
				Y = i
			};
			uint num2 = s.ReadUInt();
			int index = (int)(s.Index + num2 - 4);
			kVSLadderLine.StartCode = s.ReadUInt();
			ushort num3 = s.ReadUShort();
			for (int j = 0; j < colcount; j++)
			{
				if (((num3 >> j) & 1) != 0)
				{
					vlines[j, i] = new KVSVLine(this)
					{
						X = j,
						Y = i
					};
				}
			}
			for (int k = 0; k < colcount; k++)
			{
				KVSUnit kVSUnit = new KVSUnit(this)
				{
					X = k,
					Y = i
				};
				kVSUnit.Step = s.ReadInt();
				kVSUnit.Code = s.ReadUShort();
				kVSUnit.Row = s.ReadByte();
				kVSUnit.Flag = s.ReadByte();
				children[k, i] = kVSUnit;
				for (int l = 0; l < 3; l++)
				{
					KVSArg kVSArg = new KVSArg(kVSUnit)
					{
						ID = l,
						Code = s.ReadUShort(),
						Offset = s.ReadULong()
					};
					kVSUnit.OArgs[l] = kVSArg;
					if (kVSArg.Code != 63)
					{
						kVSUnit.Args.Add(kVSArg);
					}
				}
			}
			kVSLadderLine.EAID = (int)s.ReadUInt();
			kVSLadderLine.EndCode = s.ReadUInt();
			kVSLadderLine.RSID = (int)s.ReadUInt();
			lines.Add(kVSLadderLine);
			s.Index = index;
		}
	}

	public IEnumerable<KVSUnit> GetNexts(KVSUnit unit)
	{
		if (unit == null || unit.Code == 65534)
		{
			yield break;
		}
		int x = unit.X;
		int y = unit.Y;
		while (x + 1 < colcount && y < rowcount)
		{
			KVSUnit _next = children[x + 1, y];
			if (_next != null)
			{
				yield return _next;
			}
			if (vlines[x, y] == null)
			{
				break;
			}
			y++;
		}
		x = unit.X;
		y = unit.Y;
		while (x + 1 < colcount && y > 0 && vlines[x, y - 1] != null)
		{
			GridDictionary<KVSUnit> gridDictionary = children;
			int x2 = x + 1;
			int num = y - 1;
			y = num;
			KVSUnit _next2 = gridDictionary[x2, num];
			if (_next2 != null)
			{
				yield return _next2;
			}
		}
	}

	public IEnumerable<KVSUnit> GetPrevious(KVSUnit unit)
	{
		if (unit == null || unit.Code == 65534)
		{
			yield break;
		}
		int x = unit.X;
		int y = unit.Y;
		while (x - 1 >= 0 && y < rowcount)
		{
			KVSUnit _prev = children[x - 1, y];
			if (_prev != null)
			{
				yield return _prev;
			}
			if (vlines[x - 1, y] == null)
			{
				break;
			}
			y++;
		}
		x = unit.X;
		y = unit.Y;
		while (x - 1 >= 0 && y > 0 && vlines[x - 1, y - 1] != null)
		{
			GridDictionary<KVSUnit> gridDictionary = children;
			int x2 = x - 1;
			int num = y - 1;
			y = num;
			KVSUnit _prev2 = gridDictionary[x2, num];
			if (_prev2 != null)
			{
				yield return _prev2;
			}
		}
	}

	public IEnumerable<KVSUnit> GetNextsNotLine(KVSUnit unit)
	{
		List<KVSUnit> list = new List<KVSUnit>();
		if (unit == null || unit.Code == 65534)
		{
			return list;
		}
		foreach (KVSUnit next in GetNexts(unit))
		{
			if (next.Code == 255)
			{
				list.AddRange(GetNextsNotLine(next));
			}
			else
			{
				list.Add(next);
			}
		}
		return list;
	}

	public IEnumerable<KVSUnit> GetPreviousNotLine(KVSUnit unit)
	{
		List<KVSUnit> list = new List<KVSUnit>();
		if (unit == null || unit.Code == 65534)
		{
			return list;
		}
		foreach (KVSUnit previou in GetPrevious(unit))
		{
			if (previou.Code == 255)
			{
				list.AddRange(GetPreviousNotLine(previou));
			}
			else
			{
				list.Add(previou);
			}
		}
		return list;
	}

	public KVSLadder GetRange(int y1, int y2)
	{
		KVSLadder kVSLadder = new KVSLadder();
		kVSLadder.issuccess = issuccess;
		kVSLadder.RowCount = y2 - y1 + 1;
		kVSLadder.ColCount = ColCount;
		kVSLadder.Name = Name;
		kVSLadder.SBRID = SBRID;
		kVSLadder.INTID = INTID;
		for (int i = y1; i <= y2; i++)
		{
			for (int j = 0; j < colcount; j++)
			{
				KVSUnit kVSUnit = children[j, i]?.Clone();
				KVSUnit kVSUnit2 = vlines[j, i]?.Clone();
				if (kVSUnit != null)
				{
					kVSUnit.Parent = kVSLadder;
					kVSUnit.Y -= y1;
					kVSLadder.Children[j, i - y1] = kVSUnit;
				}
				if (kVSUnit2 != null)
				{
					kVSUnit2.Parent = kVSLadder;
					kVSUnit2.Y -= y1;
					kVSLadder.VLines[j, i - y1] = kVSUnit2;
				}
			}
		}
		return kVSLadder;
	}

	public void GenerateActionSets()
	{
		int num = 0;
		int num2 = 0;
		while (num < colcount && num2 < rowcount)
		{
			KVSUnit kVSUnit = children[num, num2];
			if (kVSUnit != null && kVSUnit.IsLD)
			{
				KVSActionSet kVSActionSet = KVSActionSet.Create(kVSUnit);
				if (kVSActionSet != null)
				{
					kVSUnit.ActionSet = kVSActionSet;
					kVSUnit = kVSActionSet.To;
				}
			}
			if (kVSUnit != null)
			{
				num = kVSUnit.X;
				num2 = kVSUnit.Y;
			}
			if (++num >= ColCount)
			{
				num = 0;
				num2++;
			}
		}
	}
}
