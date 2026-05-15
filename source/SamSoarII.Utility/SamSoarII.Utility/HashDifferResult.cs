using System;
using System.Collections.Generic;
using System.Linq;

namespace SamSoarII.Utility;

public class HashDifferResult : IComparable<HashDifferResult>
{
	private int matchcount;

	private List<IntRange> srcs;

	private List<IntRange> dsts;

	public int MatchCount => matchcount;

	public IList<IntRange> Srcs => srcs;

	public IList<IntRange> Dsts => dsts;

	public HashDifferResult()
	{
		matchcount = 0;
		srcs = new List<IntRange>();
		dsts = new List<IntRange>();
	}

	public HashDifferResult(HashDifferResult that)
	{
		matchcount = that.matchcount;
		srcs = that.srcs.ToList();
		dsts = that.dsts.ToList();
	}

	public int CompareTo(HashDifferResult other)
	{
		int num = 0;
		num = matchcount.CompareTo(other.matchcount);
		if (num != 0)
		{
			return num;
		}
		num = srcs.Count().CompareTo(other.srcs.Count());
		if (num != 0)
		{
			return -num;
		}
		for (int i = 0; i < srcs.Count(); i++)
		{
			num = srcs[i].Start.CompareTo(other.srcs[i].Start);
			if (num != 0)
			{
				return num;
			}
			num = srcs[i].End.CompareTo(other.srcs[i].End);
			if (num != 0)
			{
				return num;
			}
			num = dsts[i].Start.CompareTo(other.dsts[i].Start);
			if (num != 0)
			{
				return num;
			}
			num = dsts[i].End.CompareTo(other.dsts[i].End);
			if (num != 0)
			{
				return num;
			}
		}
		return 0;
	}

	protected bool GetInsertIndex(IList<IntRange> ranges, IntRange newrange, ref int index)
	{
		int num = 0;
		int num2 = ranges.Count() - 1;
		while (num2 - num > 1)
		{
			int num3 = num + num2 >> 1;
			if (newrange.End - 1 < ranges[num3].Start)
			{
				num2 = num3 - 1;
				continue;
			}
			if (newrange.Start > ranges[num3].End - 1)
			{
				num = num3;
				continue;
			}
			return false;
		}
		int num4 = System.Math.Min(num, num2);
		int num5 = System.Math.Max(num, num2);
		if (num5 >= 0 && num5 < ranges.Count())
		{
			if (newrange.AssertValue(ranges[num5].Start) || newrange.AssertValue(ranges[num5].End - 1))
			{
				return false;
			}
			if (ranges[num5].AssertValue(newrange.Start) || ranges[num5].AssertValue(newrange.End - 1))
			{
				return false;
			}
		}
		if (num4 >= 0 && num4 < ranges.Count())
		{
			if (newrange.AssertValue(ranges[num4].Start) || newrange.AssertValue(ranges[num4].End - 1))
			{
				return false;
			}
			if (ranges[num4].AssertValue(newrange.Start) || ranges[num4].AssertValue(newrange.End - 1))
			{
				return false;
			}
		}
		if (num5 >= 0 && num5 < ranges.Count() && newrange.Start >= ranges[num5].End)
		{
			index = num5 + 1;
		}
		else if (num4 >= 0 && num4 < ranges.Count() && newrange.Start >= ranges[num4].End)
		{
			index = num4 + 1;
		}
		else
		{
			index = 0;
		}
		return true;
	}

	public HashDifferResult Insert(IntRange newsrc, IntRange newdst)
	{
		int index = 0;
		int index2 = 0;
		if (!GetInsertIndex(srcs, newsrc, ref index))
		{
			return null;
		}
		if (!GetInsertIndex(dsts, newdst, ref index2))
		{
			return null;
		}
		if (index != index2)
		{
			return null;
		}
		HashDifferResult hashDifferResult = new HashDifferResult(this);
		hashDifferResult.matchcount += System.Math.Min(newsrc.Count, newdst.Count);
		hashDifferResult.srcs.Insert(index, newsrc);
		hashDifferResult.dsts.Insert(index2, newdst);
		return hashDifferResult;
	}
}
