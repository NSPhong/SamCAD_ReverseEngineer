using System.Collections.Generic;
using System.Linq;
using SamSoarII.Utility.Files.GX.Init;

namespace SamSoarII.Utility.Files.GX;

public class GXPLCSeries
{
	public static readonly GXPLCSeries[] Items;

	private Enum_GXPLCSeries type;

	private Dictionary<string, GXSystemRemap> remap;

	public static GXPLCSeries Current { get; set; }

	public Enum_GXPLCSeries Type => type;

	public Dictionary<string, GXSystemRemap> Remap => remap;

	static GXPLCSeries()
	{
		Items = new GXPLCSeries[1]
		{
			new GXPLCSeries(Enum_GXPLCSeries.FX)
		};
		GXPLCSeriesInitor.Init();
		Current = Items.FirstOrDefault();
	}

	public GXPLCSeries(Enum_GXPLCSeries _type)
	{
		type = _type;
		remap = new Dictionary<string, GXSystemRemap>();
	}

	public override string ToString()
	{
		if (type == Enum_GXPLCSeries.FX)
		{
			return "FXCPU";
		}
		return "<None>";
	}
}
