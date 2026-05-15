using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXOutputFormat : GXUnitFormat
{
	public override bool CanPulse => false;

	public GXOutputFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
		isoutput = true;
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXOutputFormat(_inst, _args);
	}
}
