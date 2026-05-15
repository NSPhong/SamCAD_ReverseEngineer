using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXLIMITFormat : GXUnitSpFormat
{
	public GXLIMITFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXLIMITFormat(_inst, _args);
	}
}
