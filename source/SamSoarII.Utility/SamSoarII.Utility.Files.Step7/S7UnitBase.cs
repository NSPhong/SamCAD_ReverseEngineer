namespace SamSoarII.Utility.Files.Step7;

public class S7UnitBase : S7Object
{
	protected S7Network parent;

	public S7Network Parent => parent;

	public S7UnitBase(S7Network _parent)
		: base(_parent.Data)
	{
		parent = _parent;
	}
}
