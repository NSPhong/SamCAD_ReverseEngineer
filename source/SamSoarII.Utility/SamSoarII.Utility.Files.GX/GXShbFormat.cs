using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXShbFormat : GXUnitFormat
{
	public GXShbFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
		isoutput = true;
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXShbFormat(_inst, _args);
	}
}
