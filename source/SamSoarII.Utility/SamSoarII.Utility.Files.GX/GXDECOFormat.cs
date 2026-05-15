using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXDECOFormat : GXCOFormat
{
	public GXDECOFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXDECOFormat(_inst, _args);
	}
}
