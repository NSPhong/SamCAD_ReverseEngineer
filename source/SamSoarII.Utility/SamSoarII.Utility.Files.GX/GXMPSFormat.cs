using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXMPSFormat : GXMStackFormat
{
	public override bool CanPulse => false;

	public GXMPSFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXMPSFormat(_inst, _args);
	}
}
