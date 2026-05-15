using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXZONEFormat : GXUnitSpFormat
{
	public GXZONEFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXZONEFormat(_inst, _args);
	}
}
