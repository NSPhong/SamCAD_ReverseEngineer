using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXSh32Format : GXSh16Format
{
	public GXSh32Format(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXSh32Format(_inst, _args);
	}
}
