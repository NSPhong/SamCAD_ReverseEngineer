using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXZCPDFormat : GXUnitSpFormat
{
	public GXZCPDFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXZCPDFormat(_inst, _args);
	}
}
