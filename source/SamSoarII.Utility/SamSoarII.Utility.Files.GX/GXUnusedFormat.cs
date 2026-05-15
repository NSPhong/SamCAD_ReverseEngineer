using System.Collections.Generic;
using System.Text;

namespace SamSoarII.Utility.Files.GX;

public class GXUnusedFormat : GXUnitFormat
{
	public GXUnusedFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
		isoutput = true;
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXUnusedFormat(_inst, _args);
	}

	public override GXUnitFormat Pulselize()
	{
		GXInstFormat gXInstFormat = base.Inst.Pulselize();
		GXUnusedFormat gXUnusedFormat = new GXUnusedFormat(gXInstFormat, base.Args);
		gXUnusedFormat.isinput = isinput;
		gXUnusedFormat.isoutput = isoutput;
		gXUnusedFormat.ispulse = true;
		return gXUnusedFormat;
	}

	public override void Append(StringBuilder sb, IList<long> hexs, IList<int> lens, IList<byte[]> datas)
	{
		base.AppendA(sb, hexs, lens, datas);
	}
}
