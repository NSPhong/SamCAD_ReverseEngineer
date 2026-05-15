using System.Collections.Generic;
using System.Linq;

namespace SamSoarII.Utility.Files.KVS;

public class KVSModule
{
	protected KVSBase baseinfo;

	protected KVSLadder ladder;

	protected List<KVSVar> vars = new List<KVSVar>();

	protected Dictionary<string, KVSLocalLabel> labels = new Dictionary<string, KVSLocalLabel>();

	public KVSBase BaseInfo
	{
		get
		{
			return baseinfo;
		}
		set
		{
			baseinfo = value;
		}
	}

	public KVSLadder Ladder
	{
		get
		{
			return ladder;
		}
		set
		{
			ladder = value;
		}
	}

	public IList<KVSVar> Vars => vars;

	public IDictionary<string, KVSLocalLabel> Labels => labels;

	public string Name => baseinfo?.Names?.FirstOrDefault() ?? "Unknown module";

	public string Comment => baseinfo?.Comments?.FirstOrDefault() ?? string.Empty;

	public void Setup(KVSReport rep)
	{
		if (rep.BaseInfo != null)
		{
			BaseInfo = rep.BaseInfo;
		}
		if (rep.Ladder != null)
		{
			Ladder = rep.Ladder;
			Ladder.Parent = this;
		}
		if (rep.EAList != null && ladder != null)
		{
			Setup(rep.EAList);
		}
		if (rep.SList != null && ladder != null)
		{
			Setup(rep.SList);
		}
		if (rep.LCList != null && ladder != null)
		{
			Setup(rep.LCList);
		}
		if (rep.LLList != null)
		{
			foreach (KVSLocalLabel lL in rep.LLList)
			{
				if (!labels.ContainsKey(lL.Name))
				{
					labels.Add(lL.Name, lL);
				}
			}
		}
		if (rep.LLCList != null)
		{
			foreach (KVSLocalLabel value in labels.Values)
			{
				if (value.CMID >= 0 && value.CMID < rep.LLCList.Count())
				{
					value.Comment = rep.LLCList[value.CMID].Comment;
				}
			}
		}
		if (rep.VList != null)
		{
			vars.AddRange(rep.VList);
		}
	}

	protected void Setup(KVSExArgsList ealist)
	{
		for (int i = 0; i < ladder.RowCount; i++)
		{
			KVSLadderLine kVSLadderLine = ladder.Lines[i];
			int num = kVSLadderLine.EAID;
			if (num <= 0 || num - 1 >= ealist.Count())
			{
				continue;
			}
			for (int num2 = ladder.ColCount - 1; num2 >= 0; num2--)
			{
				KVSUnit kVSUnit = ladder.Children[num2, i];
				if (kVSUnit?.Format != null && kVSUnit.Args.Count() < kVSUnit.Format.Args.Count())
				{
					KVSExArgs kVSExArgs = ealist[--num];
					foreach (KVSArg arg in kVSExArgs.Args)
					{
						KVSArg kVSArg = arg.Clone();
						kVSArg.ID = kVSUnit.Args.Count();
						kVSUnit.Args.Add(kVSArg);
					}
					if (num <= 0)
					{
						break;
					}
				}
			}
		}
	}

	protected void Setup(KVSStringList slist)
	{
		foreach (KVSUnit child in ladder.Children)
		{
			foreach (KVSArg arg in child.Args)
			{
			}
		}
	}

	protected void Setup(KVSLineCommentList lclist)
	{
	}
}
