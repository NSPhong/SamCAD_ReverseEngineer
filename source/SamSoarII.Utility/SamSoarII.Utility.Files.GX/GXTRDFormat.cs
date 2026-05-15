using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXTRDFormat : GXUnitSpFormat
{
	public GXTRDFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXTRDFormat(_inst, _args);
	}
}
