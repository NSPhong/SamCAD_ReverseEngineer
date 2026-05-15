using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXTCal2Format : GXUnitSpFormat
{
	public GXTCal2Format(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXTCal2Format(_inst, _args);
	}
}
