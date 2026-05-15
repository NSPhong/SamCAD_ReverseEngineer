using System.Collections.Generic;

namespace SamSoarII.Utility.Files.KVS;

public class KVSMultipleAction : KVSAction
{
	private List<KVSUnit> ins = new List<KVSUnit>();

	public IList<KVSUnit> Ins => ins;
}
