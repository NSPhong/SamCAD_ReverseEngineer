using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXSCL2Format : GXUnitSpFormat
{
	public GXSCL2Format(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXSCL2Format(_inst, _args);
	}
}
