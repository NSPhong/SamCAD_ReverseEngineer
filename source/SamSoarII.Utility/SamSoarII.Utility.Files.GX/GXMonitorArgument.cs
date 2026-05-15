namespace SamSoarII.Utility.Files.GX;

public class GXMonitorArgument
{
	private string name;

	private string type;

	private string value;

	public string Name => name;

	public string Type => type;

	public string Value => value;

	public GXMonitorArgument(string _name, string _type, string _value)
	{
		name = _name;
		type = _type;
		value = _value;
	}
}
