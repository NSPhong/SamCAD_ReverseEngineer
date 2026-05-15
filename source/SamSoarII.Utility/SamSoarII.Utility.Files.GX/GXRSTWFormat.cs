using System.Collections.Generic;
using System.Text;

namespace SamSoarII.Utility.Files.GX;

public class GXRSTWFormat : GXUnitSpFormat
{
	public GXRSTWFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXRSTWFormat(_inst, _args);
	}

	public override void Append(StringBuilder sb, IList<long> hexs, IList<int> lens, IList<byte[]> datas)
	{
		int i = 0;
		int j = 0;
		int k = 0;
		sb.Append(base.Inst.Name);
		sb.Append(" K0 ");
		AppendOne(sb, hexs, lens, datas, ref i, ref j, ref k);
	}
}
