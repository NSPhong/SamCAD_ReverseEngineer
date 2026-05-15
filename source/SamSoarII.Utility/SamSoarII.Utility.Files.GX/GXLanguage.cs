namespace SamSoarII.Utility.Files.GX;

public class GXLanguage
{
	public static readonly GXLanguage[] Items = new GXLanguage[1]
	{
		new GXLanguage(Enum_GXLanguages.Ladder)
	};

	private Enum_GXLanguages type;

	public Enum_GXLanguages Type => type;

	public GXLanguage(Enum_GXLanguages _type)
	{
		type = _type;
	}

	public override string ToString()
	{
		if (type == Enum_GXLanguages.Ladder)
		{
			return "Ladder diagram";
		}
		return "<None>";
	}
}
