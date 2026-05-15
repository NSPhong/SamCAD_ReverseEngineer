using System.Collections.Generic;

namespace SamSoarII.Utility.Files.Step7;

public class S7PreList : S7Object
{
	private S7Network parent;

	private List<S7PreNode> nodes;

	public S7Network Parent => parent;

	public IList<S7PreNode> Nodes => nodes;

	public S7PreList(S7Network _parent, int _dataindex)
		: base(_parent.Data)
	{
		parent = _parent;
		dataindex = _dataindex;
		nodes = new List<S7PreNode>();
		data.Start(dataindex);
		int w = data.GetW(1);
		data.Move(3);
		while (w-- > 0)
		{
			S7PreNode s7PreNode = new S7PreNode(this, data.Index);
			nodes.Add(s7PreNode);
			data.Start(s7PreNode.DataIndex + s7PreNode.DataCount);
		}
		datacount = data.Index - dataindex;
	}
}
