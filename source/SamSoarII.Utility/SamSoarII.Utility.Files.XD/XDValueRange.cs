using System;

namespace SamSoarII.Utility.Files.XD;

public class XDValueRange
{
	private Enum_XDValue e;

	private string name;

	private int start;

	private int end;

	private int nbase;

	public Enum_XDValue E => e;

	public string Name => name;

	public int Start => start;

	public int End => end;

	public int NBase => nbase;

	public XDValueRange(string _name, int _start, int _end, int _nbase)
	{
		name = _name;
		start = _start;
		end = _end;
		nbase = _nbase;
		Enum.TryParse<Enum_XDValue>(name, out e);
	}
}
