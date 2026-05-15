using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXDIFormat : GXUnitSpFormat
{
	public GXDIFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXDIFormat(_inst, _args);
	}
}
