namespace SamSoarII.Utility.Files.Step7;

public class S7Value : S7ValueBase
{
	private int id;

	private Enum_S7DataType datatype;

	private Enum_S7BaseType basetype;

	private Enum_S7VarFormat varfmt;

	private string signal;

	private int offset;

	private bool isundefined;

	public int ID => id;

	public Enum_S7DataType DataType => datatype;

	public Enum_S7BaseType BaseType => basetype;

	public Enum_S7VarFormat VarFormat => varfmt;

	public string Signal => signal;

	public int Offset => offset;

	public bool IsUndefined => isundefined;

	public S7Value(S7UnitBase _parent, int _dataindex)
		: base(_parent, _dataindex)
	{
		dataindex = _dataindex;
		data.Start(dataindex);
		id = data.GetW(0);
		isundefined = data.GetB(5) == 2;
		data.Move(9);
		varfmt = (Enum_S7VarFormat)data.GetB(5);
		datatype = (Enum_S7DataType)data.GetB(7);
		switch (varfmt)
		{
		case Enum_S7VarFormat.Address:
			basetype = (Enum_S7BaseType)data.GetI(8);
			offset = data.GetI(12);
			data.Move(16);
			break;
		case Enum_S7VarFormat.Signal:
		{
			int b = data.GetB(9);
			signal = data.GetString(10, b);
			data.Move(10 + b);
			break;
		}
		}
		datacount = data.Index - dataindex;
	}

	protected S7Value(S7UnitBase _parent, S7Value _source)
		: base(_parent, _source.DataIndex)
	{
		datacount = _source.datacount;
		id = _source.id;
		isundefined = _source.isundefined;
		varfmt = _source.varfmt;
		datatype = _source.datatype;
		basetype = _source.basetype;
		offset = _source.offset;
		signal = _source.signal;
	}

	public override S7ValueBase Clone(S7UnitBase _parent)
	{
		return new S7Value(_parent, this);
	}
}
