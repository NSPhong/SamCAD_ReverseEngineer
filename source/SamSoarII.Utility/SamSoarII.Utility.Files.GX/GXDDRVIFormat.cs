using System.Collections.Generic;
using System.Text;

namespace SamSoarII.Utility.Files.GX;

public class GXDDRVIFormat : GXUnitSpFormat
{
	public GXDDRVIFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXDDRVAFormat(_inst, _args);
	}

	public override void Append(StringBuilder sb, IList<long> hexs, IList<int> lens, IList<byte[]> datas)
	{
		StringBuilder stringBuilder = new StringBuilder();
		base.Append(stringBuilder, hexs, lens, datas);
		string[] array = stringBuilder.ToString().Split();
		sb.Append($"DRVI {array[1]} {array[2]} K100 K100 {array[3]} {array[4]}");
	}
}
