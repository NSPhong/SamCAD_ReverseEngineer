using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXWSTLFormat : GXUnitSpFormat
{
	public GXWSTLFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXWSTLFormat(_inst, _args);
	}
}
