using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXSTLFormat : GXUnitSpFormat
{
	public override bool CanPulse => false;

	public GXSTLFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
		isoutput = false;
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXSTLFormat(_inst, _args);
	}
}
