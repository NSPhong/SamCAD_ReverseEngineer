using System.Collections.Generic;
using System.Linq;

namespace SamSoarII.Utility.Files.Step7;

public class S7STLStmt : S7UnitBase
{
	private int id;

	private int code;

	private List<S7ValueBase> values;

	private S7STLUnitFormat format;

	public int ID => id;

	public int Code => code;

	public IList<S7ValueBase> Values => values;

	public S7STLUnitFormat Format => format;

	public S7STLStmt(S7Network _parent, int _dataindex)
		: base(_parent)
	{
		dataindex = _dataindex;
		data.Start(dataindex);
		id = data.GetW(0);
		code = data.GetI(2);
		int w = data.GetW(8);
		values = new List<S7ValueBase>();
		data.Move(10);
		while (w-- > 0)
		{
			S7ValueBase s7ValueBase = S7ValueBase.Create(this, data);
			values.Add(s7ValueBase);
			data.Start(s7ValueBase.DataIndex + s7ValueBase.DataCount);
		}
		datacount = data.Index - dataindex;
		S7Translator.STLFormats.TryGetValue(code, out format);
	}

	public S7STLStmt(S7Network _parent, S7Unit _source)
		: base(_parent)
	{
		dataindex = _source.DataIndex;
		datacount = _source.DataCount;
		id = 0;
		code = _source.Code;
		values = _source.Values.Select((S7ValueBase sv) => sv.Clone(this)).ToList();
	}

	public S7STLStmt(S7Network _parent, S7STLUnit _source)
		: base(_parent)
	{
		dataindex = _source.DataIndex;
		datacount = _source.DataCount;
		id = _source.ID;
		code = _source.Code;
		values = _source.Values.Select((S7ValueBase sv) => sv.Clone(this)).ToList();
	}
}
