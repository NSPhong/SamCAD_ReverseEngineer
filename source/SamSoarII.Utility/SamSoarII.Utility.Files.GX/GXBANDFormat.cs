using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXBANDFormat : GXUnitSpFormat
{
	public GXBANDFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXBANDFormat(_inst, _args);
	}
}
