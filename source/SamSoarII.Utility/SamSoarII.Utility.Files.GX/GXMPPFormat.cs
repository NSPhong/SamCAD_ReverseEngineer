using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXMPPFormat : GXMStackFormat
{
	public override bool CanPulse => false;

	public GXMPPFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXMPPFormat(_inst, _args);
	}
}
