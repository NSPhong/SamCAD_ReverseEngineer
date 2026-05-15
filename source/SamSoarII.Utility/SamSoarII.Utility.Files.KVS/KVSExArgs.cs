using System.Collections.Generic;

namespace SamSoarII.Utility.Files.KVS;

public class KVSExArgs
{
	protected int id;

	protected List<KVSArg> args = new List<KVSArg>();

	public int ID
	{
		get
		{
			return id;
		}
		set
		{
			id = value;
		}
	}

	public IList<KVSArg> Args => args;
}
