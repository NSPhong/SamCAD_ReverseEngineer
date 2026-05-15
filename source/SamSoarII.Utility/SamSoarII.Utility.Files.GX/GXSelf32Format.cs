using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXSelf32Format : GXSelf16Format
{
	public GXSelf32Format(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXSelf32Format(_inst, _args);
	}
}
