using System.Collections.Generic;
using System.Text;

namespace SamSoarII.Utility.Files.GX;

public class GXTOUTFormat : GXUnitSpFormat
{
	public override bool CanPulse => false;

	public GXTOUTFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
	}

	public override void Append(StringBuilder sb, IList<long> hexs, IList<int> lens, IList<byte[]> datas)
	{
		StringBuilder stringBuilder = new StringBuilder();
		base.Append(stringBuilder, hexs, lens, datas);
		string[] array = stringBuilder.ToString().Split();
		switch (array[1][0])
		{
		case 'C':
			sb.Append($"CTU CV{array[1].Substring(1)} {array[2]}");
			break;
		case 'T':
			sb.Append($"TON TV{array[1].Substring(1)} {array[2]}");
			break;
		default:
			sb.Append(stringBuilder.ToString());
			break;
		}
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXTOUTFormat(_inst, _args);
	}
}
