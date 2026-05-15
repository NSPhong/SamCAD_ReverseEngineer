using System.Collections.Generic;
using System.Text;

namespace SamSoarII.Utility.Files.GX;

public class GXDBONFormat : GXUnitSpFormat
{
	public GXDBONFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXDBONFormat(_inst, _args);
	}

	public override void Append(StringBuilder sb, IList<long> hexs, IList<int> lens, IList<byte[]> datas)
	{
		StringBuilder stringBuilder = new StringBuilder();
		base.Append(stringBuilder, hexs, lens, datas);
		string[] array = stringBuilder.ToString().Split();
		sb.Append($"SHRD {array[1]} {array[3]} LD_BOND,");
		sb.Append($"MOV K1M8166 K1{array[2]}");
	}
}
