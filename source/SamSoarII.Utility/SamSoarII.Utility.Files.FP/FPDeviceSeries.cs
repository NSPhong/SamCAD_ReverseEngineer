using System.Collections.Generic;

namespace SamSoarII.Utility.Files.FP;

public class FPDeviceSeries
{
	public static readonly Dictionary<string, FPDeviceSeries> ItemOfNames = new Dictionary<string, FPDeviceSeries>();

	private string name;

	private Dictionary<uint, FPDeviceRange> ranges;

	public string Name => name;

	public IDictionary<uint, FPDeviceRange> Ranges => ranges;

	public FPDeviceSeries(string _name)
	{
		name = _name;
		ranges = new Dictionary<uint, FPDeviceRange>();
	}

	public bool IsEX()
	{
		string text = name;
		string text2 = text;
		if (text2 == "FP7")
		{
			return true;
		}
		return false;
	}
}
