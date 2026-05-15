using System.Collections.Generic;

namespace SamSoarII.Utility.Files.FP;

public class FPFuncBlock
{
	public static string CommonDefines = string.Empty;

	public static readonly Dictionary<string, FPFuncBlock> ItemOfNames = new Dictionary<string, FPFuncBlock>();

	private string name;

	private string text;

	public string Name => name;

	public string Text => text;

	public FPFuncBlock(string _name, string _text)
	{
		name = _name;
		text = _text;
	}
}
