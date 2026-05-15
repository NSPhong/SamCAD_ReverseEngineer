using System.Collections.Generic;

namespace SamSoarII.Utility.Files.FP;

public class FPValueConvert
{
	public static readonly Dictionary<string, FPValueConvert> ItemOfNames = new Dictionary<string, FPValueConvert>();

	private string oldname;

	private string newname;

	public string OldName => oldname;

	public string NewName => newname;

	public FPValueConvert(string _oldname, string _newname)
	{
		oldname = _oldname;
		newname = _newname;
	}
}
