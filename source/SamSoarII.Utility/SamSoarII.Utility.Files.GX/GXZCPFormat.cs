using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXZCPFormat : GXUnitSpFormat
{
	public GXZCPFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXZCPFormat(_inst, _args);
	}
}
