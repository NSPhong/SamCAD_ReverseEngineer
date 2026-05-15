using System.Collections.Generic;

namespace SamSoarII.Utility.Files.KVS;

public class KVSReport
{
	protected KVSProInfoList proinfo;

	protected KVSModInfoList modinfo;

	protected KVSBase baseinfo;

	protected KVSLadder ladder;

	protected KVSExArgsList ealist;

	protected KVSStringList slist;

	protected KVSVarList vlist;

	protected List<KVSValueCommentList> vclists = new List<KVSValueCommentList>();

	protected KVSLineCommentList lclist;

	protected KVSScriptList scripts;

	protected KVSGlobalLabelList gllist;

	protected KVSGlobalLabelCommentList glclist;

	protected KVSLocalLabelList lllist;

	protected KVSLocalLabelCommentList llclist;

	protected List<KVSUnknownList> uklists = new List<KVSUnknownList>();

	public KVSProInfoList ProInfo
	{
		get
		{
			return proinfo;
		}
		set
		{
			proinfo = value;
		}
	}

	public KVSModInfoList ModInfo
	{
		get
		{
			return modinfo;
		}
		set
		{
			modinfo = value;
		}
	}

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

	public KVSExArgsList EAList
	{
		get
		{
			return ealist;
		}
		set
		{
			ealist = value;
		}
	}

	public KVSStringList SList
	{
		get
		{
			return slist;
		}
		set
		{
			slist = value;
		}
	}

	public KVSVarList VList
	{
		get
		{
			return vlist;
		}
		set
		{
			vlist = value;
		}
	}

	public IList<KVSValueCommentList> VCLists => vclists;

	public KVSLineCommentList LCList
	{
		get
		{
			return lclist;
		}
		set
		{
			lclist = value;
		}
	}

	public KVSScriptList Scripts
	{
		get
		{
			return scripts;
		}
		set
		{
			scripts = value;
		}
	}

	public KVSGlobalLabelList GLList
	{
		get
		{
			return gllist;
		}
		set
		{
			gllist = value;
		}
	}

	public KVSGlobalLabelCommentList GLCList
	{
		get
		{
			return glclist;
		}
		set
		{
			glclist = value;
		}
	}

	public KVSLocalLabelList LLList
	{
		get
		{
			return lllist;
		}
		set
		{
			lllist = value;
		}
	}

	public KVSLocalLabelCommentList LLCList
	{
		get
		{
			return llclist;
		}
		set
		{
			llclist = value;
		}
	}

	public IList<KVSUnknownList> UKLists => uklists;
}
