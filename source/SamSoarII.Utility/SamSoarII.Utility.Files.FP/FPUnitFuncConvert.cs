using System.Collections.Generic;
using System.Linq;

namespace SamSoarII.Utility.Files.FP;

public class FPUnitFuncConvert : FPUnitConvert
{
	private string oldname;

	private List<string> temps;

	private List<string> vartypes;

	private List<FPUnitFuncOutput> outputs;

	public override string OldName => oldname;

	public IList<string> Temps => temps;

	public IList<string> VarTypes => vartypes;

	public IList<FPUnitFuncOutput> Outputs => outputs;

	public FPUnitFuncConvert(string _oldname)
	{
		oldname = _oldname;
		temps = new List<string>();
		vartypes = new List<string>();
		outputs = new List<FPUnitFuncOutput>();
	}

	public override void Add(string item)
	{
		item = item.Trim();
		if (item.FirstOrDefault() != '(')
		{
			return;
		}
		int num = item.IndexOf(',');
		int num2 = item.IndexOf(')');
		int result = 0;
		int result2 = 0;
		int num3 = 0;
		List<string> list = new List<string>();
		int.TryParse(item.Substring(1, num - 1), out result);
		int.TryParse(item.Substring(num + 1, num2 - num - 1), out result2);
		int num4 = num2 + 1;
		for (int i = num2 + 1; i < item.Length; i++)
		{
			switch (item[i])
			{
			case '(':
				num3++;
				break;
			case ')':
				num3--;
				break;
			case ',':
				if (num3 <= 0)
				{
					if (num4 < i)
					{
						list.Add(item.Substring(num4, i - num4));
					}
					num4 = i + 1;
				}
				break;
			}
		}
		if (num4 < item.Length)
		{
			list.Add(item.Substring(num4));
		}
		FPUnitFuncOutput fPUnitFuncOutput = new FPUnitFuncOutput(result, result2);
		for (int j = 0; j < result2; j++)
		{
			string text = ((j + 1 >= result2) ? null : list[(j + 1) * (result + 1) - 1]);
			for (int k = 0; k < result; k++)
			{
				FPUnitFuncItem fPUnitFuncItem = fPUnitFuncOutput.Get(k, j);
				string text2 = list[j * (result + 1) + k];
				if (text != null && text[k] == 'V')
				{
					fPUnitFuncItem.IsV = true;
				}
				if (text2.Equals("H"))
				{
					fPUnitFuncItem.IsH = true;
				}
				else
				{
					fPUnitFuncItem.Unit = FPUnitBaseConvert.Parse(text2);
				}
			}
		}
		outputs.Add(fPUnitFuncOutput);
	}
}
