using System;
using System.Collections.Generic;
using System.Linq;

namespace SamSoarII.Utility.Files.Delta;

public class DeltaNetwork
{
	private DeltaRoutine parent;

	private int id;

	private bool isactive;

	private int width;

	private int height;

	private List<DeltaUnit> units;

	private int strike;

	private int rootwidth;

	public DeltaRoutine Parent
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

	public int ID
	{
		get
		{
			return id;
		}
		set
		{
			id = value;
		}
	}

	public bool IsActive
	{
		get
		{
			return isactive;
		}
		set
		{
			isactive = value;
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
			_SetWidth(value);
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
			_SetHeight(value);
		}
	}

	public DeltaNetwork(DeltaRoutine _parent)
	{
		parent = _parent;
		units = new List<DeltaUnit>();
		width = 4;
		strike = 8;
		height = 1;
		id = -1;
		isactive = true;
		while (units.Count() < strike * height)
		{
			units.Add(null);
		}
	}

	protected void _SetWidth(int _width)
	{
		width = _width;
		int num = strike;
		while (strike < width * 2)
		{
			strike *= 2;
		}
		if (strike == num)
		{
			return;
		}
		while (units.Count() < strike * height)
		{
			units.Add(null);
		}
		for (int num2 = height - 1; num2 >= 0; num2--)
		{
			for (int num3 = strike - 1; num3 >= 0; num3--)
			{
				units[num2 * strike + num3] = ((num3 < num) ? units[num2 * num + num3] : null);
			}
		}
	}

	protected void _SetHeight(int _height)
	{
		height = _height;
		while (units.Count() < strike * height)
		{
			units.Add(null);
		}
	}

	public DeltaUnit GetUnit(int x, int y)
	{
		int num = y * strike + x * 2;
		return (num >= 0 && num < units.Count()) ? units[num] : null;
	}

	public void AddUnit(DeltaUnit unit)
	{
		for (int i = unit.X; i < unit.X + unit.Width; i++)
		{
			for (int j = unit.Y; j < unit.Y + unit.Height; j++)
			{
				int num = j * strike + i * 2;
				DeltaUnit unit2 = GetUnit(i, j);
				if (unit2 != null)
				{
					RemoveUnit(unit);
				}
				if (num >= 0 && num < units.Count())
				{
					units[num] = unit;
				}
			}
		}
	}

	public void RemoveUnit(DeltaUnit unit)
	{
		for (int i = unit.X; i < unit.X + unit.Width; i++)
		{
			for (int j = unit.Y; j < unit.Y + unit.Height; j++)
			{
				int num = j * strike + i * 2;
				if (num >= 0 && num < units.Count())
				{
					units[num] = null;
				}
			}
		}
	}

	public void RemoveUnit(int x, int y)
	{
		int num = y * strike + x * 2;
		if (num >= 0 && num < units.Count() && units[num] != null)
		{
			RemoveUnit(units[num]);
		}
	}

	public DeltaVLine GetVLine(int x, int y)
	{
		int num = y * strike + x * 2 + 1;
		return (num >= 0 && num < units.Count() && units[num] is DeltaVLine) ? ((DeltaVLine)units[num]) : null;
	}

	public void AddVLine(DeltaVLine vline)
	{
		int num = vline.Y * strike + vline.X * 2 + 1;
		if (num >= 0 && num < units.Count())
		{
			units[num] = vline;
		}
	}

	public void RemoveVLine(DeltaVLine vline)
	{
		int num = vline.Y * strike + vline.X * 2 + 1;
		if (num >= 0 && num < units.Count())
		{
			units[num] = null;
		}
	}

	public void RemoveVLine(int x, int y)
	{
		int num = y * strike + x * 2 + 1;
		if (num >= 0 && num < units.Count())
		{
			units[num] = null;
		}
	}

	public void AddLogic(DeltaLogicUnit logic, int sx, int sy)
	{
		if (Width < sx + logic.Width)
		{
			Width = sx + logic.Width;
		}
		if (Height < sy + logic.Height)
		{
			Height = sy + logic.Height;
		}
		if (logic.Core != null)
		{
			DeltaUnit core = logic.Core;
			core.X = sx;
			core.Y = sy;
			AddUnit(core);
			return;
		}
		int num = 0;
		foreach (DeltaLogicUnit item in logic.Items)
		{
			AddLogic(item, sx + item.X, sy + item.Y);
			num = System.Math.Max(num, item.Y);
		}
		if (sx > 0)
		{
			for (int i = 0; i < num; i++)
			{
				AddVLine(new DeltaVLine
				{
					X = sx - 1,
					Y = sy + i
				});
			}
		}
		if (!(logic is DeltaLogicOutputUnit) && !(logic is DeltaLogicBranchUnit))
		{
			for (int j = 0; j < num; j++)
			{
				AddVLine(new DeltaVLine
				{
					X = sx + logic.Width - 1,
					Y = sy + j
				});
			}
		}
	}

	public void Load(DeltaDMLElement de)
	{
		foreach (DeltaDMLElement item in de.Items)
		{
			switch (item.Name)
			{
			case "PROPERTIES_START":
				LoadProp(item);
				break;
			case "ROOTLINK_START":
				LoadRoot(item);
				break;
			case "OUTLINK_START":
				LoadOut(item);
				break;
			}
		}
	}

	protected void LoadProp(DeltaDMLElement de)
	{
		foreach (DeltaDMLElement item in de.Items)
		{
			string name = item.Name;
			string text = name;
			if (!(text == "NET_ID"))
			{
				if (text == "NET_ACTIVE")
				{
					bool.TryParse(item.Value, out isactive);
				}
			}
			else
			{
				int.TryParse(item.Value, out id);
			}
		}
	}

	protected void LoadRoot(DeltaDMLElement de)
	{
		LoadLogic(de, isroot: true);
	}

	protected void LoadOut(DeltaDMLElement de)
	{
		LoadLogic(de, isroot: false);
	}

	protected void LoadLogic(DeltaDMLElement de, bool isroot)
	{
		List<DeltaLogicUnit> list = new List<DeltaLogicUnit>();
		List<string> list2 = new List<string>();
		foreach (DeltaDMLElement item in de.Items)
		{
			DeltaUnit deltaUnit = null;
			DeltaLogicUnit deltaLogicUnit = null;
			int result = -1;
			int result2 = -1;
			int result3 = -1;
			string devName = null;
			string symbol = null;
			list2.Clear();
			foreach (DeltaDMLElement item2 in item.Items)
			{
				switch (item2.Name)
				{
				case "TYPE":
					int.TryParse(item2.Value, out result);
					break;
				case "LNK_C":
					int.TryParse(item2.Value, out result2);
					break;
				case "LNK_L":
					int.TryParse(item2.Value, out result3);
					break;
				case "DEV_NAME":
					devName = item2.Value;
					break;
				case "SYMB":
					symbol = item2.Value;
					break;
				}
				if (!item2.Name.Equals("VAR_NODE_S"))
				{
					continue;
				}
				foreach (DeltaDMLElement item3 in item2.Items)
				{
					list2.Add(item3.Value);
				}
			}
			switch (result)
			{
			case 20:
				deltaUnit = new DeltaEmpty();
				break;
			case 1:
			case 2:
			case 3:
			case 4:
				deltaUnit = new DeltaInputUnit
				{
					Type = result,
					DevName = devName
				};
				break;
			case 13:
			case 15:
			case 16:
				deltaUnit = new DeltaOutputUnit
				{
					Type = result,
					DevName = devName
				};
				break;
			case 5:
				deltaUnit = new DeltaHLine();
				break;
			case 11:
			{
				DeltaRectIn deltaRectIn = new DeltaRectIn();
				foreach (string item4 in list2)
				{
					deltaRectIn.Ins.Add(item4);
				}
				deltaUnit = deltaRectIn;
				break;
			}
			case 12:
			{
				DeltaRectOut deltaRectOut2 = new DeltaRectOut();
				foreach (string item5 in list2)
				{
					deltaRectOut2.Outs.Add(item5);
				}
				deltaUnit = deltaRectOut2;
				break;
			}
			case 9:
				deltaUnit = new DeltaSymb
				{
					Symbol = symbol
				};
				break;
			case 10:
				deltaUnit = new DeltaSymbIn
				{
					Symbol = symbol
				};
				break;
			case 7:
			{
				DeltaRectIn deltaRectIn2 = null;
				DeltaSymb deltaSymb = null;
				DeltaSymbIn deltaSymbIn = null;
				DeltaRectOut deltaRectOut = null;
				for (int j = System.Math.Max(0, list.Count() - 3); j < list.Count(); j++)
				{
					DeltaLogicUnit deltaLogicUnit4 = list[j];
					DeltaUnit core = deltaLogicUnit4.Core;
					if (core is DeltaRectIn)
					{
						deltaRectIn2 = (DeltaRectIn)core;
					}
					if (core is DeltaSymb)
					{
						deltaSymb = (DeltaSymb)core;
					}
					if (core is DeltaSymbIn)
					{
						deltaSymbIn = (DeltaSymbIn)core;
					}
					if (core is DeltaRectOut)
					{
						deltaRectOut = (DeltaRectOut)core;
					}
				}
				DeltaRectUnit deltaRectUnit = null;
				if (deltaSymb != null)
				{
					deltaRectUnit = new DeltaOutputRectUnit
					{
						Width = 0,
						Height = 1
					};
				}
				if (deltaSymbIn != null)
				{
					deltaRectUnit = new DeltaInputRectUnit
					{
						Width = 0,
						Height = 1
					};
				}
				if (deltaRectUnit == null)
				{
					break;
				}
				if (deltaRectIn2 != null)
				{
					deltaRectUnit.Width++;
					foreach (string @in in deltaRectIn2.Ins)
					{
						deltaRectUnit.Ins.Add(@in);
					}
				}
				if (deltaRectOut != null)
				{
					deltaRectUnit.Width++;
					foreach (string @out in deltaRectOut.Outs)
					{
						deltaRectUnit.Outs.Add(@out);
					}
				}
				if (deltaSymb != null)
				{
					deltaRectUnit.Width++;
					deltaRectUnit.Symbol = deltaSymb.Symbol;
				}
				if (deltaSymbIn != null)
				{
					deltaRectUnit.Width++;
					deltaRectUnit.Symbol = deltaSymbIn.Symbol;
				}
				list.RemoveRange(list.Count() - deltaRectUnit.Width, deltaRectUnit.Width);
				deltaUnit = deltaRectUnit;
				break;
			}
			case 6:
			{
				if (result3 <= 0 || result2 <= 0)
				{
					break;
				}
				int num8 = result3 * result2;
				deltaLogicUnit = new DeltaLogicUnit
				{
					Width = result3,
					Height = 0
				};
				while (num8 > 0)
				{
					DeltaLogicUnit deltaLogicUnit5 = list.LastOrDefault();
					if (deltaLogicUnit5 == null)
					{
						break;
					}
					num8 -= deltaLogicUnit5.Width;
					deltaLogicUnit5.Parent = deltaLogicUnit;
					deltaLogicUnit5.LogicX = num8 % result3;
					deltaLogicUnit5.LogicY = num8 / result3;
					deltaLogicUnit.Items.Add(deltaLogicUnit5);
					list.RemoveAt(list.Count() - 1);
				}
				deltaLogicUnit.Items.Reverse();
				int num9 = 0;
				int num10 = 0;
				foreach (DeltaLogicUnit item6 in deltaLogicUnit.Items)
				{
					if (item6.LogicY > num10)
					{
						deltaLogicUnit.Height += num9;
						num9 = 0;
						num10 = item6.LogicY;
					}
					item6.X = item6.LogicX;
					item6.Y = deltaLogicUnit.Height;
					num9 = System.Math.Max(num9, item6.Height);
				}
				deltaLogicUnit.Height += num9;
				list.Add(deltaLogicUnit);
				break;
			}
			case 29:
			{
				if (result3 <= 0 || result2 <= 0 || list.Count() < result2)
				{
					break;
				}
				deltaLogicUnit = list[list.Count() - result2];
				DeltaLogicOutputUnit deltaLogicOutputUnit = new DeltaLogicOutputUnit
				{
					Width = 0,
					Height = 0
				};
				DeltaLogicBranchUnit deltaLogicBranchUnit = new DeltaLogicBranchUnit
				{
					Width = 0,
					Height = 0
				};
				while (result3 > deltaLogicOutputUnit.Width)
				{
					DeltaLogicUnit deltaLogicUnit2 = deltaLogicUnit.Items.LastOrDefault();
					if (deltaLogicUnit2 == null)
					{
						break;
					}
					deltaLogicUnit2.Parent = deltaLogicOutputUnit;
					deltaLogicUnit.Width -= deltaLogicUnit2.Width;
					deltaLogicUnit.Items.RemoveAt(deltaLogicUnit.Items.Count() - 1);
					deltaLogicOutputUnit.Width += deltaLogicUnit2.Width;
					deltaLogicOutputUnit.Items.Add(deltaLogicUnit2);
				}
				deltaLogicOutputUnit.Items.Reverse();
				deltaLogicOutputUnit.Width = 0;
				int x;
				foreach (DeltaLogicUnit item7 in deltaLogicOutputUnit.Items)
				{
					x = (item7.LogicX = deltaLogicOutputUnit.Width);
					item7.X = x;
					x = (item7.LogicY = 0);
					item7.Y = x;
					deltaLogicOutputUnit.Width += item7.Width;
					deltaLogicOutputUnit.Height = System.Math.Max(deltaLogicOutputUnit.Height, item7.Height);
				}
				x = (deltaLogicOutputUnit.LogicX = 0);
				deltaLogicOutputUnit.X = x;
				x = (deltaLogicOutputUnit.LogicY = 0);
				deltaLogicOutputUnit.Y = x;
				deltaLogicOutputUnit.Parent = deltaLogicBranchUnit;
				deltaLogicBranchUnit.Items.Add(deltaLogicOutputUnit);
				deltaLogicBranchUnit.Width = deltaLogicOutputUnit.Width;
				deltaLogicBranchUnit.Height = deltaLogicOutputUnit.Height;
				for (int i = list.Count() - result2 + 1; i < list.Count(); i++)
				{
					DeltaLogicUnit deltaLogicUnit3 = list[i];
					deltaLogicUnit3.Parent = deltaLogicBranchUnit;
					x = (deltaLogicUnit3.LogicX = 0);
					deltaLogicUnit3.X = x;
					deltaLogicUnit3.LogicY = deltaLogicBranchUnit.Items.Count();
					deltaLogicUnit3.Y = deltaLogicBranchUnit.Height;
					deltaLogicBranchUnit.Items.Add(deltaLogicUnit3);
					deltaLogicBranchUnit.Width = System.Math.Max(deltaLogicBranchUnit.Width, deltaLogicUnit3.Width);
					deltaLogicBranchUnit.Height += deltaLogicUnit3.Height;
				}
				list.RemoveRange(list.Count() - result2 + 1, result2 - 1);
				deltaLogicBranchUnit.Parent = deltaLogicUnit;
				x = (deltaLogicBranchUnit.LogicX = deltaLogicUnit.Width);
				deltaLogicBranchUnit.X = x;
				x = (deltaLogicBranchUnit.LogicY = 0);
				deltaLogicBranchUnit.Y = x;
				deltaLogicUnit.Items.Add(deltaLogicBranchUnit);
				deltaLogicUnit.Width += deltaLogicBranchUnit.Width;
				deltaLogicUnit.Height = System.Math.Max(deltaLogicUnit.Height, deltaLogicBranchUnit.Height);
				break;
			}
			}
			if (deltaUnit is DeltaEmpty && list.LastOrDefault() is DeltaLogicOutputUnit)
			{
				DeltaLogicOutputUnit deltaLogicOutputUnit2 = (DeltaLogicOutputUnit)list.LastOrDefault();
				deltaLogicUnit = new DeltaLogicUnit
				{
					Core = deltaUnit,
					Width = deltaUnit.Width,
					Height = deltaUnit.Height
				};
				deltaLogicUnit.Parent = deltaLogicOutputUnit2;
				DeltaLogicUnit deltaLogicUnit6 = deltaLogicUnit;
				int x = (deltaLogicUnit.X = deltaLogicOutputUnit2.Width);
				deltaLogicUnit6.LogicX = x;
				DeltaLogicUnit deltaLogicUnit7 = deltaLogicUnit;
				x = (deltaLogicUnit.Y = 0);
				deltaLogicUnit7.LogicY = x;
				deltaLogicOutputUnit2.Width += deltaLogicUnit.Width;
				deltaLogicOutputUnit2.Height = System.Math.Max(deltaLogicOutputUnit2.Height, deltaLogicUnit.Height);
				deltaLogicOutputUnit2.Items.Add(deltaLogicUnit);
			}
			else if (deltaUnit != null)
			{
				deltaLogicUnit = new DeltaLogicUnit
				{
					Core = deltaUnit,
					Width = deltaUnit.Width,
					Height = deltaUnit.Height
				};
				list.Add(deltaLogicUnit);
			}
			if (!(deltaUnit is DeltaOutputUnit) && !(deltaUnit is DeltaOutputRectUnit))
			{
				continue;
			}
			DeltaLogicOutputUnit deltaLogicOutputUnit3 = new DeltaLogicOutputUnit();
			while (list.Count() > 0)
			{
				deltaLogicUnit = list.LastOrDefault();
				if (deltaLogicUnit.Core == null)
				{
					break;
				}
				list.RemoveAt(list.Count() - 1);
				deltaLogicUnit.Parent = deltaLogicOutputUnit3;
				deltaLogicOutputUnit3.Items.Add(deltaLogicUnit);
			}
			deltaLogicOutputUnit3.Items.Reverse();
			deltaLogicOutputUnit3.Width = 0;
			deltaLogicOutputUnit3.Height = 0;
			foreach (DeltaLogicUnit item8 in deltaLogicOutputUnit3.Items)
			{
				int x = (item8.LogicX = deltaLogicOutputUnit3.Width);
				item8.X = x;
				x = (item8.LogicY = 0);
				item8.Y = x;
				deltaLogicOutputUnit3.Width += item8.Width;
				deltaLogicOutputUnit3.Height = System.Math.Max(deltaLogicOutputUnit3.Height, item8.Height);
			}
			list.Add(deltaLogicOutputUnit3);
		}
		if (isroot)
		{
			int num15 = 0;
			foreach (DeltaLogicUnit item9 in list)
			{
				AddLogic(item9, num15, 0);
				num15 += item9.Width;
			}
			rootwidth = num15;
			return;
		}
		int num16 = 0;
		foreach (DeltaLogicUnit item10 in list)
		{
			AddLogic(item10, rootwidth, num16);
			num16 += item10.Height;
		}
	}
}
