using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXCmp16Format : GXInputFormat
{
	public override bool CanPulse => false;

	public GXCmp16Format(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
		isinput = true;
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXCmp16Format(_inst, _args);
	}
}
