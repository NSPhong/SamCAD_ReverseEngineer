using System.Collections.Generic;
using System.Linq;

namespace SamSoarII.Utility.Files.KVS;

public class KVSActionSet
{
	private KVSLadder parent;

	private List<KVSAction> items;

	private KVSUnit from;

	private KVSUnit to;

	public KVSLadder Parent => parent;

	public IList<KVSAction> Items => items;

	public KVSUnit From
	{
		get
		{
			return from;
		}
		set
		{
			from = value;
		}
	}

	public KVSUnit To
	{
		get
		{
			return to;
		}
		set
		{
			to = value;
		}
	}

	public KVSActionSet(KVSLadder _parent)
	{
		parent = _parent;
		items = new List<KVSAction>();
	}

	public static KVSActionSet Create(KVSUnit unit)
	{
		if (unit == null || !unit.IsLD)
		{
			return null;
		}
		List<KVSActionStackNode> list = new List<KVSActionStackNode>();
		KVSActionStackNode kVSActionStackNode = new KVSActionStackNode
		{
			From = unit
		};
		KVSLadder kVSLadder = unit.Parent;
		KVSActionSet kVSActionSet = new KVSActionSet(kVSLadder);
		IEnumerable<KVSUnit> nextsNotLine = kVSLadder.GetNextsNotLine(unit);
		while (nextsNotLine != null && nextsNotLine.Count() != 0)
		{
			if (nextsNotLine.Count() > 1)
			{
				return null;
			}
			KVSUnit kVSUnit = nextsNotLine.FirstOrDefault();
			if (kVSUnit.IsLD)
			{
				list.Add(kVSActionStackNode);
				kVSActionStackNode = new KVSActionStackNode
				{
					From = kVSUnit
				};
			}
			else if (kVSUnit.IsST)
			{
				kVSActionStackNode.To = kVSUnit;
				kVSActionSet.Items.Add(kVSActionStackNode.Create());
				if (list.Count() == 0)
				{
					break;
				}
				kVSActionStackNode = list.LastOrDefault();
				list.RemoveAt(list.Count() - 1);
			}
			else
			{
				if (!kVSUnit.IsIn)
				{
					return null;
				}
				kVSActionStackNode.Ins.Add(kVSUnit);
			}
		}
		kVSActionSet.From = kVSActionSet.Items.FirstOrDefault()?.From;
		kVSActionSet.To = kVSActionSet.Items.LastOrDefault()?.To;
		return kVSActionSet;
	}
}
