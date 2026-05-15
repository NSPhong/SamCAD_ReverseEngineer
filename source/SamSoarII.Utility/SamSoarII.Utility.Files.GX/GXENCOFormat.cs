using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXENCOFormat : GXCOFormat
{
	public GXENCOFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXENCOFormat(_inst, _args);
	}
}
