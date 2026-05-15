namespace SamSoarII.Utility.Files.Delta;

public class DeltaOutputUnit : DeltaUnit
{
	private int type;

	private string devname;

	public int Type
	{
		get
		{
			return type;
		}
		set
		{
			type = value;
		}
	}

	public string DevName
	{
		get
		{
			return devname;
		}
		set
		{
			devname = value;
		}
	}

	public override string ToString()
	{
		string arg = string.Empty;
		switch (type)
		{
		case 13:
			arg = "OUT";
			break;
		case 15:
			arg = "SET";
			break;
		case 16:
			arg = "RST";
			break;
		}
		return $"DeltaOutputUnit : {arg} {devname}";
	}
}
