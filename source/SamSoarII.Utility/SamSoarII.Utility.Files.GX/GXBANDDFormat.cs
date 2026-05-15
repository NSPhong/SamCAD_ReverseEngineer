using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXBANDDFormat : GXUnitSpFormat
{
	public GXBANDDFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXBANDDFormat(_inst, _args);
	}
}
