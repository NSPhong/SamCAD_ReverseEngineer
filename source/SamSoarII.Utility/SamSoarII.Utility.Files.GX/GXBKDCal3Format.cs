using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXBKDCal3Format : GXUnitFormat
{
	public GXBKDCal3Format(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
		isoutput = true;
	}
}
