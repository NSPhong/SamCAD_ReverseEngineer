namespace SamSoarII.Utility.Files.GX;

public class GXProjectType
{
	public static readonly GXProjectType[] Items = new GXProjectType[1]
	{
		new GXProjectType(Enum_GXProjectTypes.Custom)
	};

	private Enum_GXProjectTypes type;

	public Enum_GXProjectTypes Type => type;

	public GXProjectType(Enum_GXProjectTypes _type)
	{
		type = _type;
	}

	public override string ToString()
	{
		if (type == Enum_GXProjectTypes.Custom)
		{
			return "Simple Project";
		}
		return "<None>";
	}
}
