using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXCmp32Format : GXInputFormat
{
	public override bool CanPulse => false;

	public GXCmp32Format(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
		isinput = true;
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXCmp32Format(_inst, _args);
	}
}
