namespace SamSoarII.Utility.Files.GX;

public class GXPLCType
{
	public static readonly GXPLCType[] Items = new GXPLCType[1]
	{
		new GXPLCType(Enum_GXPLCTypes.FX2N, Enum_GXPLCSeries.FX)
	};

	private Enum_GXPLCTypes type;

	private Enum_GXPLCSeries series;

	public Enum_GXPLCTypes Type => type;

	public Enum_GXPLCSeries Series => series;

	public GXPLCType(Enum_GXPLCTypes _type, Enum_GXPLCSeries _series)
	{
		type = _type;
		series = _series;
	}

	public override string ToString()
	{
		if (type == Enum_GXPLCTypes.FX2N)
		{
			return "FX2N/FX2NC";
		}
		return "<None>";
	}
}
