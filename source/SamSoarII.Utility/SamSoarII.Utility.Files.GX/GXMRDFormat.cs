using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXMRDFormat : GXMStackFormat
{
	public override bool CanPulse => false;

	public GXMRDFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXMRDFormat(_inst, _args);
	}
}
