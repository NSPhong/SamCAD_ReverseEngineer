using System.Collections.Generic;

namespace SamSoarII.Utility.Files.Step7;

public class S7STLUnit : S7UnitBase
{
	private int id;

	private int code;

	private List<S7ValueBase> values;

	private S7STLUnitFormat format;

	public int ID => id;

	public int Code => code;

	public IList<S7ValueBase> Values => values;

	public S7STLUnitFormat Format => format;

	public bool IsEmpty => id == 65535;

	public S7STLUnit(S7Network _parent, int _dataindex)
		: base(_parent)
	{
		dataindex = _dataindex;
		data.Start(dataindex);
		id = data.GetW(0);
		if (IsEmpty)
		{
			data.Move(2);
			datacount = data.Index - dataindex;
			return;
		}
		code = data.GetI(4);
		values = new List<S7ValueBase>();
		int w = data.GetW(14);
		data.Move(16);
		while (w-- > 0)
		{
			S7ValueBase s7ValueBase = S7ValueBase.Create(this, data);
			values.Add(s7ValueBase);
			data.Start(s7ValueBase.DataIndex + s7ValueBase.DataCount);
		}
		datacount = data.Index - dataindex;
		S7Translator.STLFormats.TryGetValue(code, out format);
	}
}
