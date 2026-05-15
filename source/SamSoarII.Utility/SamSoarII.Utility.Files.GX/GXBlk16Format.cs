using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXBlk16Format : GXUnitFormat
{
	public GXBlk16Format(GXInstFormat inst, IEnumerable<GXArgFormat> args)
		: base(inst, args)
	{
		isoutput = true;
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXBlk16Format(_inst, _args);
	}
}
