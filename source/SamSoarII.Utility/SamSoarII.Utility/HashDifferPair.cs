using System;

namespace SamSoarII.Utility;

internal class HashDifferPair : IComparable<HashDifferPair>
{
	private uint id1;

	private uint id2;

	private uint pos1;

	private uint pos2;

	private uint matchcount;

	public uint ID1 => id1;

	public uint ID2 => id2;

	public uint Pos1 => pos1;

	public uint Pos2 => pos2;

	public uint PosInSrc => System.Math.Min(pos1, pos2);

	public uint PosInDst => System.Math.Max(pos1, pos2);

	public uint MatchCount => matchcount;

	public HashDifferPair(uint _srcid, uint _dstid, uint _srcpos, uint _dstpos, uint _matchcount)
	{
		id1 = _srcid;
		id2 = _dstid;
		pos1 = _srcpos;
		pos2 = _dstpos;
		matchcount = _matchcount;
	}

	public int CompareTo(HashDifferPair other)
	{
		int num = 0;
		num = matchcount.CompareTo(other.matchcount);
		if (num != 0)
		{
			return num;
		}
		num = id1.CompareTo(other.id1);
		if (num != 0)
		{
			return num;
		}
		num = id2.CompareTo(other.id2);
		if (num != 0)
		{
			return num;
		}
		num = pos1.CompareTo(other.pos1);
		if (num != 0)
		{
			return num;
		}
		num = pos2.CompareTo(other.pos2);
		if (num != 0)
		{
			return num;
		}
		return 0;
	}

	public void Move(uint _dstid, uint _dstpos, uint _matchcount)
	{
		id2 = _dstid;
		pos2 = _dstpos;
		matchcount = _matchcount;
	}
}
