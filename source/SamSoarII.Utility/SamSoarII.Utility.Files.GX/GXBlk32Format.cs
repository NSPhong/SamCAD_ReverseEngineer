using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXBlk32Format : GXUnitFormat
{
	public GXBlk32Format(GXInstFormat inst, IEnumerable<GXArgFormat> args)
		: base(inst, args)
	{
		isoutput = true;
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXBlk32Format(_inst, _args);
	}
}
