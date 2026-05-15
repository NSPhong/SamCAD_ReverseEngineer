using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXCal2Format_32 : GXUnitFormat
{
	public GXCal2Format_32(GXInstFormat inst, IEnumerable<GXArgFormat> args)
		: base(inst, args)
	{
		isoutput = true;
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXCal2Format_32(_inst, _args);
	}
}
