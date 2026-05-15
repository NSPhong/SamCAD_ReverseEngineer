using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXSCL2DFormat : GXUnitSpFormat
{
	public GXSCL2DFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXSCL2DFormat(_inst, _args);
	}
}
