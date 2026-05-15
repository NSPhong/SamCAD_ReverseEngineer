using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXZONEDFormat : GXUnitSpFormat
{
	public GXZONEDFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXZONEDFormat(_inst, _args);
	}
}
