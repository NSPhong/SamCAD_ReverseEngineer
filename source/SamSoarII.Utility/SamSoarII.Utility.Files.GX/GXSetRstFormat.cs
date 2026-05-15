using System.Collections.Generic;
using System.Text;

namespace SamSoarII.Utility.Files.GX;

public class GXSetRstFormat : GXUnitFormat
{
	public override bool CanPulse => false;

	public GXSetRstFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
		isoutput = true;
	}

	public override void Append(StringBuilder sb, IList<long> hexs, IList<int> lens, IList<byte[]> datas)
	{
		if (base.Args[0].LastFit is GXValueFormatD)
		{
			int i = 0;
			int j = 0;
			int k = 0;
			if (base.Inst.Name.Equals("SET"))
			{
				sb.Append("MOV HFFFF ");
				AppendOne(sb, hexs, lens, datas, ref i, ref j, ref k);
			}
			else
			{
				sb.Append("MOV H0000 ");
				AppendOne(sb, hexs, lens, datas, ref i, ref j, ref k);
			}
		}
		else
		{
			base.Append(sb, hexs, lens, datas);
			sb.Append(" K1");
		}
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXSetRstFormat(_inst, _args);
	}
}
