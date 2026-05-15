using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace SamSoarII.Utility.DXF;

public class DXFSpline : DXFEntity
{
	private static double[] bsfunc = new double[4096];

	private Point startP;

	private Point endP;

	private int rank;

	private List<double> nodes = new List<double>();

	private List<double> weights = new List<double>();

	private List<Point> controlP = new List<Point>();

	public Point StartP => startP;

	public Point EndP => endP;

	public int Rank => rank;

	public int NodeNumber => nodes.Count;

	public int ControlPNumber => controlP.Count;

	public List<double> Nodes => nodes;

	public List<double> Weights => weights;

	public List<Point> ControlP => controlP;

	public DXFSpline(string name, DXFModel parent)
		: base(parent)
	{
		base.Name = name;
		base.Type = EntityType.Spline;
		ReadProperties();
		parent.Graph.AddEdge(new DXFEdge(this));
	}

	public override void ReadProperties()
	{
		Point item = default(Point);
		while (true)
		{
			base.Parent.Reader.MoveNext();
			if (base.Parent.Reader.CurrentCode == 0)
			{
				break;
			}
			switch (base.Parent.Reader.CurrentCode)
			{
			case 71:
				rank = Convert.ToInt32(base.Parent.Reader.CurrentValue);
				break;
			case 40:
				nodes.Add(Convert.ToDouble(base.Parent.Reader.CurrentValue));
				break;
			case 41:
				weights.Add(Convert.ToDouble(base.Parent.Reader.CurrentValue));
				break;
			case 10:
				item = new Point
				{
					X = Convert.ToDouble(base.Parent.Reader.CurrentValue)
				};
				break;
			case 20:
				item.Y = Convert.ToDouble(base.Parent.Reader.CurrentValue);
				controlP.Add(item);
				break;
			}
		}
		RegularNode();
		startP = ComputeNode(nodes[rank]);
		endP = ComputeNode(nodes[controlP.Count]);
	}

	private Point ComputeNode(double u)
	{
		int i;
		for (i = rank; nodes[i + 1] < u; i++)
		{
		}
		int num = i - rank;
		Point result = default(Point);
		double num2 = 0.0;
		bsfunc[i] = 1.0;
		bsfunc[i + 1] = 0.0;
		for (int j = 1; j <= rank; j++)
		{
			bsfunc[i - j] = 0.0;
			for (int k = i - j; k <= i; k++)
			{
				bsfunc[k] = bsfunc[k] * GetRatioLeft(u, k, j) + bsfunc[k + 1] * GetRatioRight(u, k, j);
			}
		}
		if (weights.Count > 0)
		{
			for (int l = num; l <= i; l++)
			{
				double num3 = bsfunc[l];
				double num4 = weights[l] * num3;
				num2 += num4;
				result.X += num4 * controlP[l].X;
				result.Y += num4 * controlP[l].Y;
			}
			result.X /= num2;
			result.Y /= num2;
			return result;
		}
		for (int m = num; m <= i; m++)
		{
			double num5 = bsfunc[m];
			num2 += num5;
			result.X += num5 * controlP[m].X;
			result.Y += num5 * controlP[m].Y;
		}
		result.X /= num2;
		result.Y /= num2;
		result.X = System.Math.Round(result.X, 2);
		result.Y = System.Math.Round(result.Y, 2);
		return result;
	}

	private double GetBaseFuncValue(double u, int pbase, int rank)
	{
		if (rank > 0)
		{
			return GetRatioLeft(u, pbase, rank) * GetBaseFuncValue(u, pbase, rank - 1) + GetRatioRight(u, pbase, rank) * GetBaseFuncValue(u, pbase + 1, rank - 1);
		}
		if (u >= nodes[pbase] && u <= nodes[pbase + 1])
		{
			return 1.0;
		}
		return 0.0;
	}

	private double GetRatioLeft(double u, int pbase, int rank)
	{
		double num = u - nodes[pbase];
		double num2 = nodes[pbase + rank] - nodes[pbase];
		if (num2 < 0.001)
		{
			return 0.0;
		}
		return num / num2;
	}

	private double GetRatioRight(double u, int pbase, int rank)
	{
		double num = nodes[pbase + rank + 1] - u;
		double num2 = nodes[pbase + rank + 1] - nodes[pbase + 1];
		if (num2 < 0.001)
		{
			return 0.0;
		}
		return num / num2;
	}

	private void RegularNode()
	{
		double num = nodes.Last();
		for (int i = 0; i < nodes.Count; i++)
		{
			nodes[i] = System.Math.Round(nodes[i] * 1000.0 / num, 3);
		}
	}

	public float[] GetReverseNodes()
	{
		float[] array = new float[nodes.Count];
		float num = (float)nodes.Last();
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = num - (float)nodes[nodes.Count - i - 1];
		}
		return array;
	}

	public Point[] GetSamplePoints()
	{
		Point[] array = new Point[100];
		int num = 10;
		int num2 = 0;
		while (num <= 1000)
		{
			array[num2] = ComputeNode(num);
			num += 10;
			num2++;
		}
		return array;
	}

	public override void Render(DrawingContext context, StreamGeometryContext ctx)
	{
		Point[] samplePoints = GetSamplePoints();
		if (!base.IsReverse)
		{
			ctx.PolyLineTo(samplePoints, isStroked: true, isSmoothJoin: true);
		}
		else
		{
			ctx.PolyLineTo(samplePoints.Reverse().ToList(), isStroked: true, isSmoothJoin: true);
		}
	}
}
