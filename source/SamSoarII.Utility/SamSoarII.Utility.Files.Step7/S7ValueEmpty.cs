namespace SamSoarII.Utility.Files.Step7;

public class S7ValueEmpty : S7ValueBase
{
	public S7ValueEmpty(S7UnitBase _parent, int _dataindex)
		: base(_parent, _dataindex)
	{
		dataindex = _dataindex;
		datacount = 2;
	}

	public override S7ValueBase Clone(S7UnitBase _parent)
	{
		return new S7ValueBase(_parent, dataindex);
	}
}
