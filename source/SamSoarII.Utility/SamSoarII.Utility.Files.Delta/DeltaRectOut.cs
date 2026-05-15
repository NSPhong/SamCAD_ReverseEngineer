using System.Collections.Generic;

namespace SamSoarII.Utility.Files.Delta;

public class DeltaRectOut : DeltaUnit
{
	private List<string> outs;

	public IList<string> Outs => outs;

	public DeltaRectOut()
	{
		outs = new List<string>();
	}
}
