using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXDINTFormat : GXUnitSpFormat
{
	public GXDINTFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXDINTFormat(_inst, _args);
	}
}
