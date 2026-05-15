using System.Collections.Generic;

namespace SamSoarII.Utility.Files.FP;

public class FPProject
{
	private string name;

	private FPDevice device;

	private List<FPLadder> ladders;

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

	public FPDevice Device
	{
		get
		{
			return device;
		}
		set
		{
			device = value;
		}
	}

	public IList<FPLadder> Ladders => ladders;

	public FPProject()
	{
		name = "FPProject";
		device = null;
		ladders = new List<FPLadder>();
	}
}
