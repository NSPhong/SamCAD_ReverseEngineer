namespace SamSoarII.Utility;

public class QuadraticEquation
{
	private int varnum;

	private double[] factor;

	public int VarNum => varnum;

	public double[] Factor => factor;

	public QuadraticEquation(int _varnum)
	{
		int num = 1;
		varnum = _varnum;
		for (int i = 0; i < varnum; i++)
		{
			num *= 3;
		}
		factor = new double[num];
		for (int j = 0; j < num; j++)
		{
			factor[j] = 0.0;
		}
	}

	public void SetFactor1(int t1, double value)
	{
		factor[t1] = value;
	}

	public void SetFactor2(int t1, int t2, double value)
	{
		int num = t1 + t2 * 3;
		factor[num] = value;
	}

	public void SetFactor3(int t1, int t2, int t3, double value)
	{
		int num = t1 + t2 * 3 + t3 * 9;
		factor[num] = value;
	}

	public void SetFactor4(int t1, int t2, int t3, int t4, double value)
	{
		int num = t1 + t2 * 3 + t3 * 9 + t4 * 27;
		factor[num] = value;
	}

	public double GetValue(double[] vars)
	{
		double num = 0.0;
		for (int i = 0; i < factor.Length; i++)
		{
			if (factor[i] == 0.0)
			{
				continue;
			}
			int num2 = i;
			double num3 = factor[i];
			for (int j = 0; j < varnum; j++)
			{
				switch (j % 3)
				{
				case 1:
					num3 *= vars[j];
					break;
				case 2:
					num3 *= vars[j] * vars[j];
					break;
				}
			}
			num += num3;
		}
		return num;
	}

	public void GetPartialDerivative(double[] vars, int vid, ref double pd, ref double zp)
	{
		double num = 0.0;
		double num2 = 0.0;
		int num3 = 1;
		for (int i = 0; i < vid; i++)
		{
			num3 *= 3;
		}
		for (int j = 0; j < factor.Length; j++)
		{
			if (factor[j] == 0.0 || j / num3 % 3 == 0)
			{
				continue;
			}
			double num4 = factor[j];
			int num5 = j;
			int num6 = 0;
			while (num6 < varnum)
			{
				if (num6 != vid)
				{
					switch (num5 % 3)
					{
					case 1:
						num4 *= vars[num6];
						break;
					case 2:
						num4 *= vars[num6] * vars[num6];
						break;
					}
				}
				num6++;
				num5 /= 3;
			}
			switch (j / num3 % 3)
			{
			case 1:
				num += num4;
				break;
			case 2:
				num2 += num4;
				break;
			}
		}
		if (num2 == 0.0)
		{
			pd = num;
			zp = double.NaN;
		}
		else
		{
			pd = num2 * vars[vid] + num;
			zp = (0.0 - num) / num2;
		}
	}
}
