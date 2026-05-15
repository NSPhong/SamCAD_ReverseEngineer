using System.Collections.Generic;
using System.Text;

namespace SamSoarII.Utility.Files.GX;

public class GXZRSTFormat : GXUnitSpFormat
{
	public GXZRSTFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXZRSTFormat(_inst, _args);
	}

	public override void Append(StringBuilder sb, IList<long> hexs, IList<int> lens, IList<byte[]> datas)
	{
		StringBuilder stringBuilder = new StringBuilder();
		int i = 0;
		int j = 0;
		int k = 0;
		int offset = GXValueFormat.GetOffset(hexs[i], lens[i]);
		AppendOne(stringBuilder, hexs, lens, datas, ref i, ref j, ref k);
		i++;
		j++;
		int offset2 = GXValueFormat.GetOffset(hexs[i], lens[i]);
		AppendOne(stringBuilder, hexs, lens, datas, ref i, ref j, ref k);
		if (base.Args[0].LastFit is GXValueFormatXY && base.Args[1].LastFit is GXValueFormatXY)
		{
			sb.Append($"RST {base.Args[0].LastFit.Name}{ValueConverter.IntToDex(offset)} K{offset2 - offset + 1}");
		}
		else if (base.Args[0].LastFit is GXValueFormatB && base.Args[1].LastFit is GXValueFormatB)
		{
			sb.Append($"RST {base.Args[0].LastFit.Name}{offset} K{offset2 - offset + 1}");
		}
		else if (base.Args[0].LastFit is GXValueFormatD && base.Args[1].LastFit is GXValueFormatD)
		{
			sb.Append($"FMOV K0 {base.Args[0].LastFit.Name}{offset} K{offset2 - offset + 1}");
		}
		else
		{
			sb.Append("A ZRST " + stringBuilder.ToString());
		}
	}
}
