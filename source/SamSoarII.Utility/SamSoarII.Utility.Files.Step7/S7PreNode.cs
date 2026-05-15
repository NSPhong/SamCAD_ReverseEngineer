namespace SamSoarII.Utility.Files.Step7;

public class S7PreNode : S7Object
{
	private S7PreList parent;

	private int id;

	private int code;

	public S7PreList Parent => parent;

	public int ID => id;

	public int Code => code;

	public S7PreNode(S7PreList _parent, int _dataindex)
		: base(_parent.Data)
	{
		parent = _parent;
		dataindex = _dataindex;
		id = data.GetW(0);
		code = data.GetI(2);
		data.Move(11);
		datacount = data.Index - dataindex;
	}
}
