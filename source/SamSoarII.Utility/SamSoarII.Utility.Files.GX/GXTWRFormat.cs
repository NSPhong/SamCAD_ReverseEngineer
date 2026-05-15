using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXTWRFormat : GXTRDFormat
{
	public GXTWRFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXTWRFormat(_inst, _args);
	}
}
