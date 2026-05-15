using System.Collections.Generic;

namespace SamSoarII.Utility.Files.FP;

public class FPLadder
{
	private int id;

	private string name;

	private List<FPNetwork> children;

	public int ID => id;

	public string Name => name;

	public IList<FPNetwork> Children => children;

	public FPLadder(int _id, string _name)
	{
		id = _id;
		name = _name;
		children = new List<FPNetwork>();
	}
}
