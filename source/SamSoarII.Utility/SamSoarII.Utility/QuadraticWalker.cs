using System;

namespace SamSoarII.Utility;

public class QuadraticWalker
{
	private int varnum;

	private QuadraticEquation[] equations;

	public int VarNum => varnum;

	public QuadraticEquation[] Equations => equations;

	public QuadraticWalker(int _varnum)
	{
		varnum = _varnum;
		equations = new QuadraticEquation[varnum];
		for (int i = 0; i < varnum; i++)
		{
			equations[i] = new QuadraticEquation(varnum);
		}
	}

	public bool GetResult(double[] mins, double[] maxs, double[] rets)
	{
		Random random = new Random();
		for (int i = 0; i < varnum; i++)
		{
			rets[i] = mins[i] + random.NextDouble() * (maxs[i] - mins[i]);
		}
		bool flag = true;
		double[] array = new double[varnum];
		double[] array2 = new double[varnum];
		int num = 0;
		while (flag && num++ < varnum * 2)
		{
			flag = false;
			for (int j = 0; j < varnum; j++)
			{
				for (int k = 0; k < varnum; k++)
				{
					equations[k].GetPartialDerivative(rets, j, ref array[k], ref array2[k]);
				}
				int l = 0;
				int m = 0;
				for (; l < varnum && (array[l] == 0.0 || array2[l] == rets[j]); l++)
				{
				}
				if (l >= varnum)
				{
					continue;
				}
				for (; m < varnum && !double.IsNaN(array2[m]) && (array[m] == 0.0 || !(array[m] * array[l] < 0.0)); m++)
				{
				}
				if (m < varnum)
				{
					continue;
				}
				flag = true;
				if (array2[l] < rets[j])
				{
					double num2 = array2[l];
					for (int n = 0; n < varnum; n++)
					{
						num2 = System.Math.Max(num2, array2[n]);
					}
					rets[j] = num2;
				}
				else
				{
					double num3 = array2[l];
					for (int num4 = 0; num4 < varnum; num4++)
					{
						num3 = System.Math.Min(num3, array2[num4]);
					}
					rets[j] = num3;
				}
			}
		}
		QuadraticEquation[] array3 = equations;
		foreach (QuadraticEquation quadraticEquation in array3)
		{
			if (System.Math.Abs(quadraticEquation.GetValue(rets)) > 1E-08)
			{
				return false;
			}
		}
		return true;
	}
}
