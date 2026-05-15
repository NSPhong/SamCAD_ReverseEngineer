using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXEIFormat : GXUnitSpFormat
{
	public GXEIFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXEIFormat(_inst, _args);
	}
}
