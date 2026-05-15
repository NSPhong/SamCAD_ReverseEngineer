using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamSoarII.Utility.Files.GX;

public class GXSelf16Format : GXUnitFormat
{
	public GXSelf16Format(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
		isoutput = true;
	}

	public override void Append(StringBuilder sb, IList<long> hexs, IList<int> lens, IList<byte[]> datas)
	{
		base.Append(sb, hexs, lens, datas);
		int i = 0;
		int j = 0;
		int k = 0;
		while (i < hexs.Count())
		{
			sb.Append(" ");
			AppendOne(sb, hexs, lens, datas, ref i, ref j, ref k);
			i++;
			j++;
		}
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXSelf16Format(_inst, _args);
	}
}
