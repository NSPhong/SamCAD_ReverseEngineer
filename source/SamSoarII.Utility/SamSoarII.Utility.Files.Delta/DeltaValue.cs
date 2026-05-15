namespace SamSoarII.Utility.Files.Delta;

public class DeltaValue
{
	private string name;

	private string comment;

	private DeltaSystemElement sysinfo;

	public string Name
	{
		get
		{
			return name;
		}
		set
		{
			name = value;
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

	public DeltaSystemElement SysInfo
	{
		get
		{
			return sysinfo;
		}
		set
		{
			sysinfo = value;
		}
	}

	public DeltaValue(string _name)
	{
		name = _name;
		comment = string.Empty;
	}
}
