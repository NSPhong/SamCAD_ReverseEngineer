using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXLBLFormat : GXUnitSpFormat
{
	public override bool CanPulse => false;

	public GXLBLFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
		isoutput = false;
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXLBLFormat(_inst, _args);
	}
}
