using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXSRETFormat : GXUnitSpFormat
{
	public override bool CanPulse => false;

	public GXSRETFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
		isoutput = false;
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXSRETFormat(_inst, _args);
	}
}
