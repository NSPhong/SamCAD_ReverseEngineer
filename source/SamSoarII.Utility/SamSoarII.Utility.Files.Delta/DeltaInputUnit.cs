namespace SamSoarII.Utility.Files.Delta;

public class DeltaInputUnit : DeltaUnit
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
		case 1:
			arg = "LD";
			break;
		case 2:
			arg = "LDI";
			break;
		case 3:
			arg = "LDP";
			break;
		case 4:
			arg = "LDF";
			break;
		}
		return $"DeltaInputUnit : {arg} {devname}";
	}
}
