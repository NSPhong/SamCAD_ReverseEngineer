namespace SamSoarII.Utility.Files.Step7;

public class S7UnitLine : S7Object
{
	private S7Unit parent;

	private bool islinkleft;

	private bool islinkright;

	public S7Unit Parent => parent;

	public bool IsLinkLeft => islinkleft;

	public bool IsLinkRight => islinkright;

	public S7UnitLine(S7Unit _parent, int _dataindex)
		: base(_parent.Data)
	{
		parent = _parent;
		dataindex = _dataindex;
		datacount = 53;
		data.Start(dataindex);
		islinkleft = data.GetB(26) > 0;
		islinkright = data.GetB(52) > 0;
	}

	protected S7UnitLine(S7Unit _parent, S7UnitLine _source)
		: base(_parent.Data)
	{
		dataindex = _source.dataindex;
		datacount = _source.datacount;
		islinkleft = _source.islinkleft;
		islinkright = _source.islinkright;
	}

	public S7UnitLine Clone(S7Unit _parent)
	{
		return new S7UnitLine(_parent, this);
	}
}
