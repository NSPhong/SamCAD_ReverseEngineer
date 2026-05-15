using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXZCPFFormat : GXUnitSpFormat
{
	public GXZCPFFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXZCPFFormat(_inst, _args);
	}
}
