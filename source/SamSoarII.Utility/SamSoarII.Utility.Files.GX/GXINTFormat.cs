using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXINTFormat : GXUnitSpFormat
{
	public GXINTFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXINTFormat(_inst, _args);
	}
}
