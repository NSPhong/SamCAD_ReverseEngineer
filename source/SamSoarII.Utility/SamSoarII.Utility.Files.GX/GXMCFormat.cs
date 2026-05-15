using System.Collections.Generic;
using System.Text;

namespace SamSoarII.Utility.Files.GX;

public class GXMCFormat : GXUnitFormat
{
	public GXMCFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
		isinput = false;
		isoutput = true;
		ispulse = false;
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXMCFormat(_inst, _args);
	}

	public override void Append(StringBuilder sb, IList<long> hexs, IList<int> lens, IList<byte[]> datas)
	{
		int i = 1;
		int j = 1;
		int k = 0;
		sb.Append(base.Inst.Name);
		sb.Append(" ");
		AppendOne(sb, hexs, lens, datas, ref i, ref j, ref k);
	}
}
