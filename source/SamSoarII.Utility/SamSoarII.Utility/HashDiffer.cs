using System;
using System.Collections.Generic;
using System.Linq;

namespace SamSoarII.Utility;

public class HashDiffer : IDisposable
{
	private static SortedList<ulong, uint>[] hashcells;

	private static int cidbit;

	private static SortedSet<HashDifferPair> cmppairs;

	private static SortedSet<HashDifferResult> resultfights;

	private static uint[,] dpvalue;

	private static byte[,] dpaction;

	private static uint ComparePairMininum;

	private static uint ComparePairCapacity;

	private IList<uint> srcs;

	private IList<uint> dsts;

	private uint[] odrs;

	private uint[] raks;

	private uint[] mths;

	static HashDiffer()
	{
		hashcells = new SortedList<ulong, uint>[1024];
		cidbit = 22;
		cmppairs = new SortedSet<HashDifferPair>();
		resultfights = new SortedSet<HashDifferResult>();
		dpvalue = new uint[1024, 1024];
		dpaction = new byte[1024, 1024];
		ComparePairMininum = 64u;
		ComparePairCapacity = 1024u;
		for (int i = 0; i < hashcells.Length; i++)
		{
			hashcells[i] = new SortedList<ulong, uint>();
		}
	}

	protected static void CellClear()
	{
		for (int i = 0; i < hashcells.Length; i++)
		{
			hashcells[i].Clear();
		}
	}

	protected static void CellSetMaxKey(uint maxkey)
	{
		uint num = maxkey;
		cidbit = 0;
		while (num >= 1024)
		{
			num >>= 1;
			cidbit++;
		}
	}

	protected static void CellSetMaxKeyL(ulong maxlkey)
	{
		ulong num = maxlkey;
		cidbit = 0;
		while (num >= 1024)
		{
			num >>= 1;
			cidbit++;
		}
	}

	protected static void CellAdd(uint key, uint value)
	{
		ulong num = (ulong)key << 32;
		uint num2 = (key >> cidbit) & 0x3FF;
		Random random = new Random();
		num &= 0xFFFFFFFF00000000uL;
		num |= (uint)random.Next();
		while (hashcells[num2].ContainsKey(num))
		{
			num &= 0xFFFFFFFF00000000uL;
			num |= (uint)random.Next();
		}
		hashcells[num2].Add(num, value);
	}

	protected static void CellAddL(ulong lkey, uint value)
	{
		uint num = (uint)((lkey >> cidbit) & 0x3FF);
		Random random = new Random();
		while (hashcells[num].ContainsKey(lkey))
		{
			lkey++;
		}
		hashcells[num].Add(lkey, value);
	}

	protected static void CellGather(uint[] odrs)
	{
		uint num = 0u;
		for (int i = 0; i < hashcells.Length; i++)
		{
			foreach (uint item in hashcells[i].Select((KeyValuePair<ulong, uint> kv) => kv.Value))
			{
				odrs[num++] = item;
			}
		}
	}

	protected static void FightClear()
	{
		resultfights.Clear();
	}

	protected static void FightAdd(HashDifferResult result)
	{
		if (!resultfights.Contains(result))
		{
			resultfights.Add(result);
			while (resultfights.Count() > 32)
			{
				resultfights.Remove(resultfights.First());
			}
		}
	}

	protected static void PairClear()
	{
		cmppairs.Clear();
	}

	public HashDiffer(IList<uint> _srcs, IList<uint> _dsts)
	{
		srcs = _srcs;
		dsts = _dsts;
	}

	public void Dispose()
	{
		srcs = null;
		dsts = null;
		odrs = null;
		raks = null;
		mths = null;
	}

	public HashDifferResult Analyze()
	{
		if (srcs.Count == 0 || dsts.Count == 0)
		{
			return new HashDifferResult();
		}
		CellClear();
		FightClear();
		PairClear();
		uint num = 1u;
		odrs = new uint[srcs.Count + dsts.Count];
		raks = new uint[odrs.Length];
		mths = new uint[odrs.Length];
		uint[] array = new uint[odrs.Length];
		uint[] array2 = null;
		uint maxkey = System.Math.Max(srcs.Max(), dsts.Max());
		CellSetMaxKey(maxkey);
		for (uint num2 = 0u; num2 < srcs.Count; num2++)
		{
			CellAdd(srcs[(int)num2], num2);
		}
		for (uint num3 = (uint)(dsts.Count - 1); num3 < dsts.Count; num3--)
		{
			CellAdd(dsts[(int)num3], (uint)(num3 + srcs.Count));
		}
		CellGather(odrs);
		raks[odrs[0]] = 0u;
		for (uint num4 = 1u; num4 < odrs.Length; num4++)
		{
			uint value = 0u;
			uint value2 = 0u;
			IndexMove(odrs[num4 - 1], 0u, ref value);
			IndexMove(odrs[num4], 0u, ref value2);
			raks[odrs[num4]] = (uint)(raks[odrs[num4 - 1]] + ((value != value2) ? 1 : 0));
		}
		while (num < System.Math.Max(srcs.Count, dsts.Count))
		{
			CellClear();
			CellSetMaxKeyL((ulong)((long)raks.Count() << 40));
			for (uint num5 = 0u; num5 < srcs.Count; num5++)
			{
				ulong num6 = raks[num5];
				num6 <<= 20;
				num6 |= ((num5 + num >= srcs.Count) ? 1048574 : raks[num5 + num]);
				num6 <<= 20;
				CellAddL(num6, num5);
			}
			for (uint num7 = (uint)(dsts.Count - 1); num7 < dsts.Count; num7--)
			{
				ulong num8 = raks[num7 + srcs.Count];
				num8 <<= 20;
				num8 |= ((num7 + num >= dsts.Count) ? 1048575 : raks[num7 + num + srcs.Count]);
				num8 <<= 20;
				CellAddL(num8, (uint)(num7 + srcs.Count));
			}
			CellGather(odrs);
			array2 = raks;
			raks = array;
			array = array2;
			raks[odrs[0]] = 0u;
			for (uint num9 = 1u; num9 < odrs.Length; num9++)
			{
				uint num10 = odrs[num9 - 1];
				uint value3 = 0u;
				uint num11 = odrs[num9];
				uint value4 = 0u;
				ulong num12 = ((!IndexMove(num10, num, ref value3)) ? uint.MaxValue : array[num10 + num]);
				ulong num13 = ((!IndexMove(num11, num, ref value4)) ? uint.MaxValue : array[num11 + num]);
				num12 <<= 32;
				num13 <<= 32;
				num12 |= array[num10];
				num13 |= array[num11];
				raks[num11] = (uint)(raks[num10] + ((num12 != num13) ? 1 : 0));
			}
			num <<= 1;
		}
		for (uint num14 = 0u; num14 < odrs.Length; num14++)
		{
			raks[odrs[num14]] = num14;
		}
		for (uint num15 = 0u; num15 < raks.Length; num15++)
		{
			if (raks[num15] + 1 >= odrs.Length)
			{
				mths[raks[num15]] = 0u;
				continue;
			}
			uint num16 = ((num15 != 0 && num15 != srcs.Count && mths[raks[num15 - 1]] != 0) ? (mths[raks[num15 - 1]] - 1) : 0u);
			uint index = odrs[raks[num15] + 1];
			uint value5 = 0u;
			for (uint value6 = 0u; IndexMove(num15, num16, ref value5) && IndexMove(index, num16, ref value6); num16++)
			{
				if (value5 != value6)
				{
					break;
				}
			}
			mths[raks[num15]] = num16;
		}
		HashDifferPair hashDifferPair = null;
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		SortedSet<HashDifferPair> sortedSet = new SortedSet<HashDifferPair>(new HashDifferSrcMthComparer());
		for (uint num17 = 0u; num17 < srcs.Count; num17++)
		{
			while (sortedSet.Count() > 0)
			{
				hashDifferPair = sortedSet.FirstOrDefault();
				if (num17 < hashDifferPair.PosInSrc + hashDifferPair.MatchCount)
				{
					break;
				}
				int key = (int)(hashDifferPair.PosInSrc - hashDifferPair.PosInDst);
				if (--dictionary[key] == 0)
				{
					dictionary.Remove(key);
				}
				sortedSet.Remove(hashDifferPair);
			}
			if (raks[num17] + 1 >= odrs.Length)
			{
				continue;
			}
			uint num18 = raks[num17];
			uint num19 = raks[num17] + 1;
			uint num20 = mths[num18];
			while (num19 < odrs.Length && num20 >= ComparePairMininum && (odrs[num19] < srcs.Count || dictionary.ContainsKey((int)(num17 - odrs[num19]))))
			{
				num20 = System.Math.Min(num20, mths[num19++]);
			}
			if (num19 < odrs.Length && num20 >= ComparePairMininum)
			{
				hashDifferPair = new HashDifferPair(num18, num19, num17, odrs[num19], num20);
				cmppairs.Add(hashDifferPair);
				while (cmppairs.Count() > ComparePairCapacity)
				{
					cmppairs.Remove(cmppairs.FirstOrDefault());
				}
				int key2 = (int)(hashDifferPair.PosInSrc - hashDifferPair.PosInDst);
				if (dictionary.ContainsKey(key2))
				{
					dictionary[key2]++;
				}
				else
				{
					dictionary.Add(key2, 1);
				}
				sortedSet.Add(hashDifferPair);
			}
		}
		dictionary = new Dictionary<int, int>();
		sortedSet = new SortedSet<HashDifferPair>(new HashDifferDstMthComparer());
		for (uint num21 = (uint)srcs.Count; num21 < raks.Length; num21++)
		{
			while (sortedSet.Count() > 0)
			{
				hashDifferPair = sortedSet.FirstOrDefault();
				if (num21 < hashDifferPair.PosInDst + hashDifferPair.MatchCount)
				{
					break;
				}
				int key3 = (int)(hashDifferPair.PosInDst - hashDifferPair.PosInSrc);
				if (--dictionary[key3] == 0)
				{
					dictionary.Remove(key3);
				}
				sortedSet.Remove(hashDifferPair);
			}
			if (raks[num21] + 1 >= odrs.Length)
			{
				continue;
			}
			uint num22 = raks[num21];
			uint num23 = raks[num21] + 1;
			uint num24 = mths[num22];
			while (num23 < odrs.Length && num24 >= ComparePairMininum && (odrs[num23] >= srcs.Count || dictionary.ContainsKey((int)(num21 - odrs[num23]))))
			{
				num24 = System.Math.Min(num24, mths[num23++]);
			}
			if (num23 < odrs.Length && num24 >= ComparePairMininum)
			{
				hashDifferPair = new HashDifferPair(num22, num23, num21, odrs[num23], num24);
				cmppairs.Add(hashDifferPair);
				while (cmppairs.Count() > ComparePairCapacity)
				{
					cmppairs.Remove(cmppairs.FirstOrDefault());
				}
				int key4 = (int)(hashDifferPair.PosInDst - hashDifferPair.PosInSrc);
				if (dictionary.ContainsKey(key4))
				{
					dictionary[key4]++;
				}
				else
				{
					dictionary.Add(key4, 1);
				}
				sortedSet.Add(hashDifferPair);
			}
		}
		resultfights.Add(new HashDifferResult());
		hashDifferPair = cmppairs.LastOrDefault();
		while (hashDifferPair != null && hashDifferPair.MatchCount >= ComparePairMininum)
		{
			bool flag = hashDifferPair.Pos1 < srcs.Count;
			bool flag2 = hashDifferPair.Pos2 < srcs.Count;
			uint num25 = hashDifferPair.Pos1;
			uint num26 = hashDifferPair.Pos2;
			uint matchCount = hashDifferPair.MatchCount;
			if (flag ^ flag2)
			{
				if (!flag && flag2)
				{
					uint num27 = num25;
					num25 = num26;
					num26 = num27;
				}
				IntRange newsrc = new IntRange(num25, num25 + hashDifferPair.MatchCount);
				IntRange newdst = new IntRange((uint)(num26 - srcs.Count), (uint)(num26 + hashDifferPair.MatchCount - srcs.Count));
				HashDifferResult[] array3 = resultfights.ToArray();
				foreach (HashDifferResult hashDifferResult in array3)
				{
					HashDifferResult hashDifferResult2 = hashDifferResult.Insert(newsrc, newdst);
					if (hashDifferResult2 != null)
					{
						FightAdd(hashDifferResult2);
					}
				}
			}
			cmppairs.Remove(hashDifferPair);
			matchCount = System.Math.Min(matchCount, mths[hashDifferPair.ID2]);
			if (matchCount >= ComparePairMininum && hashDifferPair.ID2 + 1 < odrs.Length)
			{
				hashDifferPair.Move(hashDifferPair.ID2 + 1, odrs[hashDifferPair.ID2 + 1], matchCount);
				cmppairs.Add(hashDifferPair);
			}
			hashDifferPair = cmppairs.LastOrDefault();
		}
		HashDifferResult hashDifferResult3 = resultfights.LastOrDefault();
		List<IntRange> list = new List<IntRange>();
		List<IntRange> list2 = new List<IntRange>();
		uint num28 = 0u;
		uint num29 = 0u;
		for (int j = 0; j < hashDifferResult3.Srcs.Count(); j++)
		{
			IntRange intRange = hashDifferResult3.Srcs[j];
			IntRange intRange2 = hashDifferResult3.Dsts[j];
			if (intRange.Start > num28 && intRange2.Start > num29)
			{
				list.Add(new IntRange(num28, intRange.Start));
				list2.Add(new IntRange(num29, intRange2.Start));
			}
			num28 = intRange.End;
			num29 = intRange2.End;
		}
		if (num28 < srcs.Count && num29 < dsts.Count)
		{
			list.Add(new IntRange(num28, (uint)srcs.Count));
			list2.Add(new IntRange(num29, (uint)dsts.Count));
		}
		for (int k = 0; k < list.Count(); k++)
		{
			IntRange intRange3 = list[k];
			IntRange intRange4 = list2[k];
			if (intRange3.Count > 1024 || intRange4.Count > 1024)
			{
				continue;
			}
			for (int l = 0; l < intRange3.Count; l++)
			{
				for (int m = 0; m < intRange4.Count; m++)
				{
					uint num30 = ((l > 0) ? dpvalue[l - 1, m] : 0u);
					uint num31 = ((m > 0) ? dpvalue[l, m - 1] : 0u);
					uint num32 = ((l > 0 && m > 0) ? dpvalue[l - 1, m - 1] : 0u);
					byte b = 3;
					if (srcs[(int)(l + intRange3.Start)] == dsts[(int)(m + intRange4.Start)])
					{
						num32++;
						b = 4;
					}
					uint num33 = System.Math.Max(num32, System.Math.Max(num30, num31));
					dpvalue[l, m] = num33;
					dpaction[l, m] = (byte)((num33 == num30) ? 1u : ((num33 == num31) ? 2u : ((uint)((num33 == num32) ? b : 0))));
				}
			}
			uint num34 = (uint)(intRange3.Count - 1);
			uint num35 = (uint)(intRange4.Count - 1);
			uint num36 = num34;
			uint num37 = num35;
			while (num34 < intRange3.Count && num35 < intRange4.Count)
			{
				if (dpaction[num34, num35] == 4)
				{
					num34--;
					num35--;
					continue;
				}
				if (num34 < num36 && num35 < num37)
				{
					IntRange newsrc2 = new IntRange(intRange3.Start + num34 + 1, intRange3.Start + num36 + 1);
					IntRange newdst2 = new IntRange(intRange4.Start + num35 + 1, intRange4.Start + num37 + 1);
					hashDifferResult3 = hashDifferResult3.Insert(newsrc2, newdst2);
				}
				switch (dpaction[num34, num35])
				{
				case 1:
					num34--;
					break;
				case 2:
					num35--;
					break;
				case 3:
					num34--;
					num35--;
					break;
				default:
					num34 = (num35 = 0u);
					break;
				}
				num36 = num34;
				num37 = num35;
			}
			if ((int)num36 >= 0 && (int)num37 >= 0)
			{
				uint num38 = System.Math.Min(num36, num37);
				IntRange newsrc3 = new IntRange(intRange3.Start + num36 - num38, intRange3.Start + num36 + 1);
				IntRange newdst3 = new IntRange(intRange4.Start + num37 - num38, intRange4.Start + num37 + 1);
				hashDifferResult3 = hashDifferResult3.Insert(newsrc3, newdst3);
			}
		}
		return hashDifferResult3;
	}

	private bool IndexMove(uint index, uint offset, ref uint value)
	{
		if (index < srcs.Count)
		{
			if (index + offset < srcs.Count)
			{
				value = srcs[(int)(index + offset)];
				return true;
			}
		}
		else if (index < srcs.Count + dsts.Count)
		{
			index -= (uint)srcs.Count;
			if (index + offset < dsts.Count)
			{
				value = dsts[(int)(index + offset)];
				return true;
			}
		}
		return false;
	}
}
