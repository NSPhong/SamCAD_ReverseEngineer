using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXUnitSpFormat : GXUnitFormat
{
	public GXUnitSpFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
		isoutput = true;
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXUnitSpFormat(_inst, _args);
	}
}
