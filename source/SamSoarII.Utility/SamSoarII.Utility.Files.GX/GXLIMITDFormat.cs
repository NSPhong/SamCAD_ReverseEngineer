using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXLIMITDFormat : GXUnitSpFormat
{
	public GXLIMITDFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXLIMITDFormat(_inst, _args);
	}
}
