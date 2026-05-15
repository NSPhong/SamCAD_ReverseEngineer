using System.Collections.Generic;
using System.Linq;

namespace SamSoarII.Utility.Files.KVS;

public class KVSActionStackNode
{
	private KVSUnit from;

	private KVSUnit to;

	private List<KVSUnit> ins;

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

	public List<KVSUnit> Ins => ins;

	public KVSActionStackNode()
	{
		ins = new List<KVSUnit>();
	}

	public KVSAction Create()
	{
		if (ins.Count() == 0)
		{
			return new KVSMoveAction
			{
				From = from,
				To = to
			};
		}
		if (ins.Count() == 1)
		{
			return new KVSSingleAction
			{
				From = from,
				To = to
			};
		}
		KVSMultipleAction kVSMultipleAction = new KVSMultipleAction
		{
			From = from,
			To = to
		};
		foreach (KVSUnit @in in ins)
		{
			kVSMultipleAction.Ins.Add(@in);
		}
		return kVSMultipleAction;
	}
}
