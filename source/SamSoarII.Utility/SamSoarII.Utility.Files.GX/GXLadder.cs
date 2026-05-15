using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXLadder
{
	private string name;

	private List<string> code;

	private List<string> brief;

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

	public List<string> Code => code;

	public List<string> Brief => brief;

	public GXLadder(string _name)
	{
		name = _name;
		code = new List<string>();
		brief = new List<string>();
	}
}
