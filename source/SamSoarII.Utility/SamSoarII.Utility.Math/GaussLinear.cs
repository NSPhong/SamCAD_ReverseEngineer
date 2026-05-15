using System;

namespace SamSoarII.Utility.Math;

public class GaussLinear : IDisposable
{
	private int count;

	private double[][] a;

	public int Count => count;

	public GaussLinear(int _count)
	{
		count = _count;
		a = new double[count][];
		for (int i = 0; i < count; i++)
		{
			a[i] = new double[count + 1];
		}
	}

	public GaussLinear(double[][] _a)
	{
		count = _a.Length;
		a = new double[count][];
		for (int i = 0; i < count; i++)
		{
			a[i] = new double[count + 1];
			Array.Copy(_a[i], a[i], count + 1);
		}
	}

	public void Dispose()
	{
		a = null;
		count = 0;
	}

	public void SetEquation(int line, double[] _a)
	{
		Array.Copy(_a, a[line], count + 1);
	}

	protected bool IsZero(double d)
	{
		return System.Math.Abs(d) <= 1E-08;
	}

	public double[] Caculate()
	{
		double[] array = new double[count];
		double[] array2 = null;
		for (int i = 0; i < count; i++)
		{
			array[i] = double.NaN;
		}
		for (int j = 0; j < count; j++)
		{
			for (int k = j; k < count; k++)
			{
				if (!IsZero(a[k][j]))
				{
					array2 = a[k];
					a[k] = a[j];
					a[j] = array2;
					break;
				}
			}
			if (IsZero(a[j][j]))
			{
				return array;
			}
			for (int l = j + 1; l < count; l++)
			{
				if (!IsZero(a[l][j]))
				{
					double num = a[l][j] / a[j][j];
					for (int m = j; m < count + 1; m++)
					{
						a[l][m] -= a[j][m] * num;
					}
				}
			}
		}
		for (int num2 = count - 1; num2 >= 0; num2--)
		{
			array[num2] = (0.0 - a[num2][count]) / a[num2][num2];
			for (int n = 0; n < num2; n++)
			{
				if (!IsZero(a[n][num2]))
				{
					double num3 = a[n][num2] / a[num2][num2];
					for (int num4 = num2; num4 < count + 1; num4++)
					{
						a[n][num4] -= a[num2][num4] * num3;
					}
				}
			}
		}
		return array;
	}
}
