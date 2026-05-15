using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXWSTRFormat : GXUnitSpFormat
{
	public GXWSTRFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXWSTRFormat(_inst, _args);
	}
}
