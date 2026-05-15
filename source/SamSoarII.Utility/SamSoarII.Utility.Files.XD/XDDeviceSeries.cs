using System.Collections.Generic;

namespace SamSoarII.Utility.Files.XD;

public class XDDeviceSeries
{
	private Enum_XDDeviceSeries e;

	private string name;

	private List<XDValueRange> vrs;

	public Enum_XDDeviceSeries E => e;

	public string Name => name;

	public IList<XDValueRange> VRs => vrs;

	public XDDeviceSeries(Enum_XDDeviceSeries _e, string _name)
	{
		e = _e;
		name = _name;
		vrs = new List<XDValueRange>();
	}
}
