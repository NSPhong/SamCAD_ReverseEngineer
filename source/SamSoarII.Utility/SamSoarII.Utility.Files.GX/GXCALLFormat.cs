using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXCALLFormat : GXUnitSpFormat
{
	public GXCALLFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXCALLFormat(_inst, _args);
	}
}
