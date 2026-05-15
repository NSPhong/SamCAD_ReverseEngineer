using System.Collections.Generic;

namespace SamSoarII.Utility.Files.XD;

public class XDValueInteract
{
	private XDValueRange parent;

	private SortedDictionary<int, XDValueInteractItem> items;

	public XDValueRange Parent => parent;

	public IDictionary<int, XDValueInteractItem> Items => items;

	public XDValueInteract(XDValueRange _parent)
	{
		parent = _parent;
		items = new SortedDictionary<int, XDValueInteractItem>();
	}

	public void Reset()
	{
		items.Clear();
	}

	public void Add(XDUnit unit, XDValue val)
	{
		if (val.E == parent.E && val.Ofs >= parent.Start && val.Ofs <= parent.End)
		{
			XDValueInteractItem value = null;
			if (!items.TryGetValue(val.Ofs, out value))
			{
				value = new XDValueInteractItem(val);
				items.Add(val.Ofs, value);
			}
			value.Units.Add(unit);
		}
	}
}
