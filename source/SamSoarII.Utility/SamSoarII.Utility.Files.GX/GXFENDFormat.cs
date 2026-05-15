using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXFENDFormat : GXUnitSpFormat
{
	public GXFENDFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
		isoutput = false;
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXFENDFormat(_inst, _args);
	}
}
