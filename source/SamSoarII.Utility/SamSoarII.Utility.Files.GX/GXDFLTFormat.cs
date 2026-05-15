using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXDFLTFormat : GXUnitSpFormat
{
	public GXDFLTFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXDFLTFormat(_inst, _args);
	}
}
