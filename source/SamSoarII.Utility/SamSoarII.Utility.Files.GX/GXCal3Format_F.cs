using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXCal3Format_F : GXUnitFormat
{
	public GXCal3Format_F(GXInstFormat inst, IEnumerable<GXArgFormat> args)
		: base(inst, args)
	{
		isoutput = true;
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXCal3Format_F(_inst, _args);
	}
}
