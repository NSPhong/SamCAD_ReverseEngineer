namespace SamSoarII.Utility.Files.GX;

public class GXRegister
{
	private string name;

	private string comment;

	public string Name => name;

	public string Comment => comment;

	public GXRegister(string _name, string _comment)
	{
		name = _name;
		comment = _comment;
	}
}
