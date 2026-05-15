using System.Collections.Generic;

namespace SamSoarII.Utility.Files.Delta;

public class DeltaRectUnit : DeltaUnit
{
	private string symbol;

	private List<string> ins;

	private List<string> outs;

	public string Symbol
	{
		get
		{
			return symbol;
		}
		set
		{
			symbol = value;
		}
	}

	public IList<string> Ins => ins;

	public IList<string> Outs => outs;

	public DeltaRectUnit()
	{
		ins = new List<string>();
		outs = new List<string>();
	}
}
