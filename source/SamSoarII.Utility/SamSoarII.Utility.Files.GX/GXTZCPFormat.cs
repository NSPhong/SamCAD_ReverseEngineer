using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXTZCPFormat : GXUnitSpFormat
{
	public GXTZCPFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXTZCPFormat(_inst, _args);
	}
}
