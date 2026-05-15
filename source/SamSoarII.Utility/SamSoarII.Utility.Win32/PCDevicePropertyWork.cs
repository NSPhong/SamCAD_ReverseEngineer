namespace SamSoarII.Utility.Win32;

public class PCDevicePropertyWork
{
	private string name;

	private int apiid;

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

	public PCDevicePropertyWork(string _name, int _apiid)
	{
		name = _name;
		apiid = _apiid;
	}
}
