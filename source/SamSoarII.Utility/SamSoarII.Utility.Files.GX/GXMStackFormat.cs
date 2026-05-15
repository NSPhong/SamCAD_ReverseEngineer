using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXMStackFormat : GXUnitFormat
{
	public override bool CanPulse => false;

	public GXMStackFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXMStackFormat(_inst, _args);
	}
}
