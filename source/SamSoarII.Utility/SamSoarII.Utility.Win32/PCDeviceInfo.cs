using System.Collections.Generic;

namespace SamSoarII.Utility.Win32;

public class PCDeviceInfo
{
	private string name;

	private List<PCDeviceProperty> props;

	public string Name
	{
		get
		{
			return name;
		}
		set
		{
			name = value;
		}
	}

	public IList<PCDeviceProperty> Props => props;

	public PCDeviceInfo(string _name)
	{
		name = _name;
		props = new List<PCDeviceProperty>();
	}
}
