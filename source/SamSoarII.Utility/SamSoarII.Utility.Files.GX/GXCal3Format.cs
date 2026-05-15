using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXCal3Format : GXUnitFormat
{
	public GXCal3Format(GXInstFormat inst, IEnumerable<GXArgFormat> args)
		: base(inst, args)
	{
		isoutput = true;
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXCal3Format(_inst, _args);
	}
}
