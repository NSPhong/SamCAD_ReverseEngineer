using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXBKCal3Format : GXUnitFormat
{
	public GXBKCal3Format(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
		isoutput = true;
	}
}
