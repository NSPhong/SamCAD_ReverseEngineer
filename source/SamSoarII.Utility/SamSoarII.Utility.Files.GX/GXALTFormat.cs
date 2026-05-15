using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXALTFormat : GXOutputFormat
{
	public override bool CanPulse => true;

	public GXALTFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXALTFormat(_inst, _args);
	}
}
