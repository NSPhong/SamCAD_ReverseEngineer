using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXSelfF32Format : GXSelf16Format
{
	public GXSelfF32Format(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXSelfF32Format(_inst, _args);
	}
}
