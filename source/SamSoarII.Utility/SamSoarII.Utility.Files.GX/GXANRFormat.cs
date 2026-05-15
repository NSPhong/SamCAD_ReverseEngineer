using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXANRFormat : GXUnitSpFormat
{
	public GXANRFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXANRFormat(_inst, _args);
	}
}
