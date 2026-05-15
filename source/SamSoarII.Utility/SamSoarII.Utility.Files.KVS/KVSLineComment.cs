namespace SamSoarII.Utility.Files.KVS;

public class KVSLineComment
{
	protected int y;

	protected string comment;

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
}
