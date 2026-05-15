using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXMEANFormat : GXUnitSpFormat
{
	public GXMEANFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXMEANFormat(_inst, _args);
	}
}
