using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXBlkBFormat : GXUnitFormat
{
	public GXBlkBFormat(GXInstFormat inst, IEnumerable<GXArgFormat> args)
		: base(inst, args)
	{
		isoutput = true;
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXBlkBFormat(_inst, _args);
	}
}
