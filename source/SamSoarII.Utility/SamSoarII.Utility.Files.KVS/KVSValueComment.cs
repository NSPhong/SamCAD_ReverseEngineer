namespace SamSoarII.Utility.Files.KVS;

public class KVSValueComment
{
	protected KVSValueCommentList parent;

	protected int offset;

	protected string comment;

	public KVSValueCommentList Parent
	{
		get
		{
			return parent;
		}
		set
		{
			parent = value;
		}
	}

	public ushort Code => parent?.ElementCode ?? 0;

	public int Offset
	{
		get
		{
			return offset;
		}
		set
		{
			offset = value;
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
