using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXInputFormat : GXUnitFormat
{
	public override bool CanPulse => false;

	public GXInputFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
		isinput = true;
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXInputFormat(_inst, _args);
	}
}
