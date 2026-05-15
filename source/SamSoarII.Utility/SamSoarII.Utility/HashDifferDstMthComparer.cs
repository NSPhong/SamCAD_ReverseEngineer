using System.Collections.Generic;

namespace SamSoarII.Utility;

internal class HashDifferDstMthComparer : IComparer<HashDifferPair>
{
	public int Compare(HashDifferPair p1, HashDifferPair p2)
	{
		int num = (int)(p1.PosInDst + p1.MatchCount);
		int value = (int)(p2.PosInDst + p2.MatchCount);
		return num.CompareTo(value);
	}
}
