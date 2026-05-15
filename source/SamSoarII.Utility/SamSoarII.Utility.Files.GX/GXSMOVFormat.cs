using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXSMOVFormat : GXUnitSpFormat
{
	public GXSMOVFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXSMOVFormat(_inst, _args);
	}
}
