namespace SamSoarII.Utility.Files.Step7;

public class S7Const : S7ValueBase
{
	private int id;

	private Enum_S7ConstFormat constformat;

	private Enum_S7DataType datatype;

	private object constdata;

	private bool isundefined;

	public int ID => id;

	public Enum_S7ConstFormat ConstFormat => constformat;

	public Enum_S7DataType DataType => datatype;

	public object ConstData => constdata;

	public bool IsUndefined => isundefined;

	public S7Const(S7UnitBase _parent, int _dataindex)
		: base(_parent, _dataindex)
	{
		dataindex = _dataindex;
		data.Start(dataindex);
		id = data.GetW(0);
		isundefined = data.GetB(5) == 2;
		data.Move(9);
		constformat = (Enum_S7ConstFormat)data.GetB(6);
		datatype = (Enum_S7DataType)data.GetB(7);
		switch (constformat)
		{
		case Enum_S7ConstFormat.Int:
			constdata = data.GetI(9);
			break;
		case Enum_S7ConstFormat.Float:
			constdata = data.GetF(9);
			break;
		}
		data.Move(14);
		datacount = data.Index - dataindex;
	}

	protected S7Const(S7UnitBase _parent, S7Const _source)
		: base(_parent, _source.DataIndex)
	{
		datacount = _source.datacount;
		id = _source.ID;
		isundefined = _source.isundefined;
		constformat = _source.constformat;
		datatype = _source.datatype;
		constdata = _source.constdata;
	}

	public override S7ValueBase Clone(S7UnitBase _parent)
	{
		return new S7Const(_parent, this);
	}
}
