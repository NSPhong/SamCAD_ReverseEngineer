using System.Collections.Generic;

namespace SamSoarII.Utility.Files.XD;

public class XDDevice
{
	private XDDeviceSeries parent;

	private string name;

	private List<XDValueRange> vrs;

	public XDDeviceSeries Parent => parent;

	public string Name => name;

	public IList<XDValueRange> VRs => vrs;

	public XDDevice(XDDeviceSeries _parent, string _name)
	{
		parent = _parent;
		name = _name;
		vrs = new List<XDValueRange>();
	}
}
