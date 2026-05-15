namespace SamSoarII.Utility.Win32;

public class PCDeviceProperty
{
	private string name;

	private int apiid;

	private string value;

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

	public int APIID
	{
		get
		{
			return apiid;
		}
		set
		{
			apiid = value;
		}
	}

	public string Value
	{
		get
		{
			return value;
		}
		set
		{
			this.value = value;
		}
	}

	public PCDeviceProperty(string _name, int _apiid, string _value)
	{
		name = _name;
		apiid = _apiid;
		value = _value;
	}
}
