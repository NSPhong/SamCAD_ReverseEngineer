using System.Collections.Generic;

namespace SamSoarII.Utility.Files.Step7;

public class S7Project : S7Object
{
	private string name;

	private string version;

	private S7Program main;

	private List<S7Program> progs;

	private Dictionary<long, S7ValueInfo> vis;

	public string Name => name;

	public string Version => version;

	public S7Program Main => main;

	public IList<S7Program> Progs => progs;

	public IDictionary<long, S7ValueInfo> VIs => vis;

	public S7Project(S7DataStream _data)
		: base(_data)
	{
		dataindex = 0;
		datacount = data.Size;
		Initialize();
	}

	private void Initialize()
	{
		data.Start(0);
		data.Start(19);
		int _size = 0;
		version = data.GetString(0, out _size);
		data.Move(_size + 2);
		name = data.GetString(1, out _size);
		data.Move(_size + 3);
		data.Move(192);
		data.Move(256);
		data.Move(768);
		data.Move(541);
		_size = data.GetB(0);
		data.Move(_size + 1);
		data.Move(287);
		int w = data.GetW(0);
		data.Move(2);
		progs = new List<S7Program>();
		vis = new Dictionary<long, S7ValueInfo>();
		while (w-- > 0)
		{
			S7Program s7Program = new S7Program(data, data.Index);
			progs.Add(s7Program);
			data.Start(s7Program.DataIndex + s7Program.DataCount);
			if (s7Program.E == Enum_Program.MAIN)
			{
				main = s7Program;
			}
			foreach (S7Network net in s7Program.Nets)
			{
				foreach (S7UnitBase unit in net.Units)
				{
					if (unit is S7Unit)
					{
						S7Unit s7Unit = (S7Unit)unit;
						foreach (S7ValueBase value in s7Unit.Values)
						{
							VIAdd(value);
						}
					}
					if (!(unit is S7STLUnit))
					{
						continue;
					}
					S7STLUnit s7STLUnit = (S7STLUnit)unit;
					foreach (S7ValueBase value2 in s7STLUnit.Values)
					{
						VIAdd(value2);
					}
				}
				foreach (S7STLStmt sTL in net.STLs)
				{
					foreach (S7ValueBase value3 in sTL.Values)
					{
						VIAdd(value3);
					}
				}
			}
		}
	}

	public void VIAdd(S7ValueBase value)
	{
		if (value is S7Value)
		{
			S7Value s7Value = (S7Value)value;
			Enum_S7BaseType bas = (Enum_S7BaseType)0;
			int ofs = 0;
			if (s7Value.VarFormat == Enum_S7VarFormat.Address)
			{
				bas = s7Value.BaseType;
				ofs = s7Value.Offset;
			}
			long key = VIToKey(bas, ofs);
			S7ValueInfo value2 = null;
			if (!vis.TryGetValue(key, out value2))
			{
				value2 = new S7ValueInfo(bas, ofs);
				vis.Add(key, value2);
			}
			value2.Values.Add(value);
		}
	}

	public long VIToKey(Enum_S7BaseType bas, int ofs)
	{
		return ((long)bas << 32) + ofs;
	}
}
