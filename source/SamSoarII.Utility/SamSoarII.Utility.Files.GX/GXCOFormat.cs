using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXCOFormat : GXUnitSpFormat
{
	public GXCOFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXCOFormat(_inst, _args);
	}
}
