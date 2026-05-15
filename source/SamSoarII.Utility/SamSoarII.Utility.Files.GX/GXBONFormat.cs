using System.Collections.Generic;
using System.Text;

namespace SamSoarII.Utility.Files.GX;

public class GXBONFormat : GXUnitSpFormat
{
	public GXBONFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
		: base(_inst, _args)
	{
	}

	public override GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXBONFormat(_inst, _args);
	}

	public override void Append(StringBuilder sb, IList<long> hexs, IList<int> lens, IList<byte[]> datas)
	{
		StringBuilder stringBuilder = new StringBuilder();
		base.Append(stringBuilder, hexs, lens, datas);
		string[] array = stringBuilder.ToString().Split();
		sb.Append(string.Format("SHR {0} {2} K1{1}", array[1], array[2], array[3]));
	}
}
