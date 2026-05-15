using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXBKCmpFormat : GXUnitFormat
{
	public GXBKCmpFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
		isoutput = true;
	}
}
