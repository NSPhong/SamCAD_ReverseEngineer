using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXOutputComment : GXUnitModel
{
	private string comment;

	public string Comment
	{
		get
		{
			return comment;
		}
		set
		{
			comment = value;
		}
	}

	public GXOutputComment(GXUnitFormat _format, IEnumerable<long> _hexs, IEnumerable<int> _lens, IEnumerable<byte[]> _datas)
		: base(_format, _hexs, _lens, _datas)
	{
	}
}
