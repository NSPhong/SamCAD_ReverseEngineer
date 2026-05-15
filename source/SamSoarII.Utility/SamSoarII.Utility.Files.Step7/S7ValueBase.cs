namespace SamSoarII.Utility.Files.Step7;

public class S7ValueBase : S7Object
{
	protected S7UnitBase parent;

	public S7UnitBase Parent => parent;

	public static S7ValueBase Create(S7UnitBase _parent, S7DataStream _data)
	{
		ushort w = _data.GetW(0);
		Enum_S7ValueType b = (Enum_S7ValueType)_data.GetB(2);
		if (w == ushort.MaxValue)
		{
			return new S7ValueEmpty(_parent, _data.Index);
		}
		if (b == Enum_S7ValueType.Const)
		{
			return new S7Const(_parent, _data.Index);
		}
		return new S7Value(_parent, _data.Index);
	}

	public S7ValueBase(S7UnitBase _parent, int _dataindex)
		: base(_parent.Data)
	{
		parent = _parent;
	}

	public virtual S7ValueBase Clone(S7UnitBase _parent)
	{
		return null;
	}
}
