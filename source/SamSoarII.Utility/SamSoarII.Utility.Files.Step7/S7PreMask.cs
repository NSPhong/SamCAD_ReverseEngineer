using System.Collections.Generic;

namespace SamSoarII.Utility.Files.Step7;

public class S7PreMask : S7Object
{
	private S7Network parent;

	private List<ushort> masks;

	public S7Network Parent => parent;

	public IList<ushort> Masks => masks;

	public S7PreMask(S7Network _parent, int _dataindex)
		: base(_parent.Data)
	{
		parent = _parent;
		masks = new List<ushort>();
		dataindex = _dataindex;
		data.Start(dataindex);
		int w = data.GetW(1);
		data.Move(3);
		while (w-- > 0)
		{
			masks.Add(data.GetW(0));
			data.Move(2);
		}
		datacount = data.Index - dataindex;
	}
}
