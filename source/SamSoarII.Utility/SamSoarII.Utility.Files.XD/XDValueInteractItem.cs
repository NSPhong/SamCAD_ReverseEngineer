using System.Collections.Generic;

namespace SamSoarII.Utility.Files.XD;

public class XDValueInteractItem
{
	private XDValue value;

	private List<XDUnit> units;

	public XDValue Value => value;

	public IList<XDUnit> Units => units;

	public XDValueInteractItem(XDValue _value)
	{
		value = _value;
		units = new List<XDUnit>();
	}
}
