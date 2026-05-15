using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXRETFormat : GXUnitSpFormat
{
	public override bool CanPulse => false;

	public GXRETFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
		isoutput = false;
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXRETFormat(_inst, _args);
	}
}
