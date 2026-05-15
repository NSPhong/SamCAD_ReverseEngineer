namespace SamSoarII.Utility.Files.Delta;

public class DeltaRange
{
	private string bas;

	private bool issystem;

	private int start;

	private int count;

	public string Base
	{
		get
		{
			return bas;
		}
		set
		{
			bas = value;
		}
	}

	public bool IsSystem
	{
		get
		{
			return issystem;
		}
		set
		{
			issystem = value;
		}
	}

	public int Start
	{
		get
		{
			return start;
		}
		set
		{
			start = value;
		}
	}

	public int Count
	{
		get
		{
			return count;
		}
		set
		{
			count = value;
		}
	}

	public DeltaRange(string _bas, int _start, int _count)
	{
		bas = _bas;
		start = _start;
		count = _count;
	}
}
