using System.Collections.Generic;

namespace SamSoarII.Utility.Files.Step7;

public class S7DeviceSeries
{
	private string name;

	private List<S7Device> devices;

	private List<S7DeviceValueRange> ranges;

	public string Name => name;

	public IList<S7Device> Devices => devices;

	public IList<S7DeviceValueRange> Ranges => ranges;

	public S7DeviceSeries(string _name)
	{
		name = _name;
		devices = new List<S7Device>();
		ranges = new List<S7DeviceValueRange>();
	}
}
