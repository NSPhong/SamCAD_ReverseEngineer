using System.Collections.Generic;
using System.Linq;

namespace SamSoarII.Utility.Files.FP;

public class FPUnitBaseConvert : FPUnitConvert
{
	private string oldname;

	private string newname;

	private string newnamed;

	private string newnamef;

	private bool ismatchall;

	private List<FPItemConvert> items;

	public override string OldName => oldname;

	public string NewName => newname;

	public string NewNameD => newnamed;

	public string NewNameF => newnamef;

	public bool IsMatchAll => ismatchall;

	public IList<FPItemConvert> Items => items;

	public FPUnitBaseConvert(string _oldname, string _newname, string _newnamed = null, string _newnamef = null, bool _ismatchall = false)
	{
		oldname = _oldname;
		newname = _newname;
		newnamed = _newnamed;
		newnamef = _newnamef;
		ismatchall = _ismatchall;
		items = new List<FPItemConvert>();
	}

	public static FPUnitBaseConvert Parse(string text)
	{
		string[] array = text.Split(' ');
		if (array.Length == 0)
		{
			return null;
		}
		FPUnitBaseConvert fPUnitBaseConvert = new FPUnitBaseConvert(array[0], array[0]);
		for (int i = 1; i < array.Length; i++)
		{
			fPUnitBaseConvert.Add(array[i]);
		}
		return fPUnitBaseConvert;
	}

	public override void Add(string item)
	{
		FPItemConvert fPItemConvert = null;
		if (item.FirstOrDefault() == '$' || item.FirstOrDefault() == '#')
		{
			int result = -1;
			int result2 = -1;
			int result3 = 0;
			FPUnitBaseMatch fPUnitBaseMatch = null;
			if (item.LastOrDefault() == ')' && item[1] == '(')
			{
				int num = item.IndexOf(',');
				int.TryParse(item.Substring(2, num - 2).Trim(), out result);
				int.TryParse(item.Substring(num + 1, item.Length - num - 2).Trim(), out result2);
				fPUnitBaseMatch = new FPUnitBaseMatch(result, result2);
			}
			else if (item.LastOrDefault() == ']')
			{
				int num2 = item.IndexOf('[');
				int.TryParse(item.Substring(1, num2 - 1).Trim(), out result);
				int.TryParse(item.Substring(num2 + 1, item.Length - num2 - 2).Trim(), out result3);
				fPUnitBaseMatch = new FPUnitBaseMatch(result, result2);
				fPUnitBaseMatch.OFS = result3;
			}
			else
			{
				int.TryParse(item.Substring(1).Trim(), out result);
				fPUnitBaseMatch = new FPUnitBaseMatch(result, result2);
			}
			if (item.FirstOrDefault() == '#')
			{
				fPUnitBaseMatch.IsTemp = true;
			}
			fPItemConvert = fPUnitBaseMatch;
		}
		else
		{
			fPItemConvert = new FPUnitBaseConst(item);
		}
		if (fPItemConvert != null)
		{
			items.Add(fPItemConvert);
		}
	}
}
