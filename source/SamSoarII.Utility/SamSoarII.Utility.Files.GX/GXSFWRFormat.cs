using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXSFWRFormat : GXSFFormat
{
	public GXSFWRFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXSFWRFormat(_inst, _args);
	}
}
