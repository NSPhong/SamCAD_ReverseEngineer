namespace SamSoarII.Core.Files;

public class LocatedFileHeaderString
{
	private IFileHeader header;

	private int id;

	private string text;

	private LocatedStringByteData data;

	public IFileHeader Header => header;

	public int ID => id;

	public string Text => (text != null) ? text : string.Empty;

	public LocatedStringByteData Data
	{
		get
		{
			return data;
		}
		set
		{
			data = value;
		}
	}

	public LocatedFileHeaderString(IFileHeader _header, int _id, string _text)
	{
		header = _header;
		id = _id;
		text = _text;
	}

	public override string ToString()
	{
		return $"{{txt={Text},hd={header},id={id}}}";
	}
}
