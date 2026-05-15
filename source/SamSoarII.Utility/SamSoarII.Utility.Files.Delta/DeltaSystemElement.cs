namespace SamSoarII.Utility.Files.Delta;

public class DeltaSystemElement
{
	private string source;

	private string target;

	private string comment;

	public string Source
	{
		get
		{
			return source;
		}
		set
		{
			source = value;
		}
	}

	public string Target
	{
		get
		{
			return target;
		}
		set
		{
			target = value;
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

	public DeltaSystemElement(string _source, string _target)
	{
		source = _source;
		target = _target;
	}
}
