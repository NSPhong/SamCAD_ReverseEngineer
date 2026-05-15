namespace SamSoarII.Utility.Files.XD;

public class XDLineComment
{
	private XDLadder parent;

	private int y;

	private string comment;

	public XDLadder Parent => parent;

	public int Y
	{
		get
		{
			return y;
		}
		set
		{
			y = value;
		}
	}

	public string Comment
	{
		get
		{
			return comment;
		}
		set
		{
			comment = value;
		}
	}

	public XDLineComment(XDLadder _parent)
	{
		parent = _parent;
	}
}
