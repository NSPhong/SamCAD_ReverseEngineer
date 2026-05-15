using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXSFRDFormat : GXSFFormat
{
	public GXSFRDFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXSFRDFormat(_inst, _args);
	}
}
