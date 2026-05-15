using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamSoarII.Utility.Files.GX;

public class GXUnitModel
{
	private GXUnitFormat format;

	private long[] hexs;

	private int[] lens;

	private List<byte[]> datas;

	public GXUnitFormat Format
	{
		get
		{
			return format;
		}
		set
		{
			format = value;
		}
	}

	public long[] Hexs => hexs;

	public int[] Lens => lens;

	public IList<byte[]> Datas => datas;

	public GXUnitModel(GXUnitFormat _format, IEnumerable<long> _hexs, IEnumerable<int> _lens, IEnumerable<byte[]> _datas)
	{
		format = _format.Clone();
		hexs = _hexs.ToArray();
		lens = _lens.ToArray();
		datas = _datas.ToList();
	}

	public void Append(StringBuilder sb)
	{
		if (format.Inst.Name.Equals("LDI"))
		{
		}
		if (format.IsOverflow(hexs, lens, datas))
		{
			if (format.Inst.Name.Equals("LDI"))
			{
			}
			if (format.IsInput)
			{
				format.Append_OverflowClear(sb, hexs, lens, datas);
			}
			else
			{
				format.AppendA(sb, hexs, lens, datas);
			}
		}
		else
		{
			format.Append(sb, hexs, lens, datas);
		}
	}
}
