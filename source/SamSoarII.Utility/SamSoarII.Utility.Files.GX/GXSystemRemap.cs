namespace SamSoarII.Utility.Files.GX;

public class GXSystemRemap
{
	private string from;

	private string to;

	public string From => from;

	public string To => to;

	public GXSystemRemap(string _from, string _to)
	{
		from = _from;
		to = _to;
	}
}
