using System;
using System.Collections.Generic;

namespace SamSoarII.Utility;

public class EnumFilter
{
	public static string[] GetNames(Type enumType, bool[] filterCondition)
	{
		if (filterCondition != null && filterCondition.Length != 0)
		{
			string[] names = Enum.GetNames(enumType);
			List<string> list = new List<string>();
			for (int i = 0; i < System.Math.Min(filterCondition.Length, names.Length); i++)
			{
				if (filterCondition[i])
				{
					list.Add(names[i]);
				}
			}
			return list.ToArray();
		}
		return Enum.GetNames(enumType);
	}
}
