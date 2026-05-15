using System.Collections.Generic;

namespace SamSoarII.Utility;

internal class HashDifferSrcMthComparer : IComparer<HashDifferPair>
{
	public int Compare(HashDifferPair p1, HashDifferPair p2)
	{
		int num = (int)(p1.PosInSrc + p1.MatchCount);
		int value = (int)(p2.PosInSrc + p2.MatchCount);
		return num.CompareTo(value);
	}
}
