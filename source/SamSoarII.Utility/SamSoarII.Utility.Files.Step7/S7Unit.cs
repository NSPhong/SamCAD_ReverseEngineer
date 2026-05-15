using System.Collections.Generic;
using System.Linq;

namespace SamSoarII.Utility.Files.Step7;

public class S7Unit : S7UnitBase
{
	private S7UnitFormat format;

	private int x;

	private int y;

	private int code;

	private Enum_S7VerticalLineStatus vertical;

	private List<S7UnitLine> lines;

	private List<S7ValueBase> values;

	public S7UnitFormat Format => format;

	public int X => x;

	public int Y => y;

	public int Code => code;

	public Enum_S7VerticalLineStatus Vertical => vertical;

	public IList<S7UnitLine> Lines => lines;

	public IList<S7ValueBase> Values => values;

	public S7Unit(S7Network _parent, int _dataindex)
		: base(_parent)
	{
		dataindex = _dataindex;
		format = null;
		data.Start(dataindex);
		x = data.GetB(2);
		y = data.GetB(1);
		code = data.GetI(5);
		vertical = (Enum_S7VerticalLineStatus)data.GetB(9);
		data.Move(12);
		lines = new List<S7UnitLine>();
		int b = data.GetB(0);
		data.Move(1);
		while (b-- > 0)
		{
			S7UnitLine s7UnitLine = new S7UnitLine(this, data.Index);
			lines.Add(s7UnitLine);
			data.Start(s7UnitLine.DataIndex + s7UnitLine.DataCount);
		}
		int w = data.GetW(2);
		data.Move(4);
		values = new List<S7ValueBase>();
		while (w-- > 0)
		{
			S7ValueBase s7ValueBase = S7ValueBase.Create(this, data);
			values.Add(s7ValueBase);
			data.Start(s7ValueBase.DataIndex + s7ValueBase.DataCount);
		}
		datacount = data.Index - dataindex;
		S7Translator.UnitFormats.TryGetValue(code, out format);
	}

	public S7Unit(S7Network _parent, S7Unit _source)
		: base(_parent)
	{
		dataindex = _source.dataindex;
		datacount = _source.datacount;
		x = _source.x;
		y = _source.y;
		code = _source.code;
		vertical = _source.vertical;
		lines = _source.Lines.Select((S7UnitLine sl) => sl.Clone(this)).ToList();
		values = _source.Values.Select((S7ValueBase sv) => sv.Clone(this)).ToList();
	}
}
