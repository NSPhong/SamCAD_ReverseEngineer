using System.Collections.Generic;

namespace SamSoarII.Utility.Files.Step7;

public class S7Device
{
	private S7DeviceSeries series;

	private string name;

	private List<S7DeviceValueRange> ranges;

	public S7DeviceSeries Series
	{
		get
		{
			return series;
		}
		set
		{
			series = value;
		}
	}

	public string Name => name;

	public IList<S7DeviceValueRange> Ranges => ranges;

	public S7Device(string _name)
	{
		series = null;
		name = _name;
		ranges = new List<S7DeviceValueRange>();
	}
}
