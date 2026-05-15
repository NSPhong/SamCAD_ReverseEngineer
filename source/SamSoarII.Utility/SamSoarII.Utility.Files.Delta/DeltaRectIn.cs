using System.Collections.Generic;

namespace SamSoarII.Utility.Files.Delta;

public class DeltaRectIn : DeltaUnit
{
	private List<string> ins;

	public IList<string> Ins => ins;

	public DeltaRectIn()
	{
		ins = new List<string>();
	}
}
