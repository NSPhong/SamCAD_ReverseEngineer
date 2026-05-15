using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXCMPFormat : GXUnitSpFormat
{
	public GXCMPFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXCMPFormat(_inst, _args);
	}
}
