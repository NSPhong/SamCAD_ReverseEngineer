using System.Collections.Generic;
using System.Linq;

namespace SamSoarII.Utility.DXF;

public class OneLinearGraph
{
	private bool isFirst;

	private DXFGraph parent;

	private DXFVertex startP;

	private DXFVertex endP;

	private List<DXFEdge> path;

	public bool IsFirst => isFirst;

	public DXFGraph Parent => parent;

	public DXFVertex StartP => startP;

	public DXFVertex EndP => endP;

	public List<DXFEdge> Path => path;

	public OneLinearGraph(DXFGraph parent, List<PointRegion> subGraph)
	{
		this.parent = parent;
		IEnumerable<PointRegion> source = subGraph.Where((PointRegion vertex) => vertex.CompareTo(parent.StartP.Region) == 0);
		isFirst = source.Count() > 0;
		if (isFirst)
		{
			startP = parent.StartP;
		}
		path = new List<DXFEdge>();
		Convert(subGraph);
	}

	private void Convert(List<PointRegion> subGraph)
	{
		if (subGraph.Count == 1)
		{
			startP = subGraph.First().GetVertex();
			endP = startP;
		}
		else if (subGraph.Count == 2)
		{
			if (isFirst)
			{
				DXFVertex dXFVertex = ((subGraph.Last().CompareTo(startP.Region) != 0) ? subGraph.Last().GetVertex() : subGraph.First().GetVertex());
				if (parent.Degree(startP.Region) % 2 == 0)
				{
					endP = startP;
				}
				else
				{
					endP = dXFVertex;
				}
			}
			else if (parent.Degree(subGraph.First()) % 2 == 0)
			{
				startP = subGraph.First().GetVertex();
				endP = startP;
			}
			else
			{
				startP = subGraph.First().GetVertex();
				endP = subGraph.Last().GetVertex();
			}
		}
		else
		{
			List<PointRegion> list = subGraph.Where((PointRegion region) => parent.Degree(region) % 2 == 1).ToList();
			if (isFirst)
			{
				if (parent.Degree(startP.Region) % 2 == 1)
				{
					list.Remove(startP.Region);
					endP = GetRelativeVertex(startP, list);
					list.Remove(endP.Region);
				}
				else
				{
					endP = startP;
				}
				while (list.Count > 0)
				{
					DXFVertex vertex = list.First().GetVertex();
					list.RemoveAt(0);
					DXFVertex relativeVertex = GetRelativeVertex(vertex, list);
					list.Remove(relativeVertex.Region);
					parent.AddEdge(new DXFEdge(vertex, relativeVertex, parent.Model));
				}
			}
			else if (list.Count > 0)
			{
				while (list.Count > 2)
				{
					DXFVertex vertex2 = list.First().GetVertex();
					list.RemoveAt(0);
					DXFVertex relativeVertex2 = GetRelativeVertex(vertex2, list);
					list.Remove(relativeVertex2.Region);
					parent.AddEdge(new DXFEdge(vertex2, relativeVertex2, parent.Model));
				}
				startP = list.First().GetVertex();
				endP = list.Last().GetVertex();
			}
			else
			{
				startP = subGraph.First().GetVertex();
				endP = startP;
			}
		}
		GenPath(subGraph);
	}

	private void GenPath(List<PointRegion> subGraph)
	{
		Stack<DXFEdge> stack = new Stack<DXFEdge>(parent.Graph[startP.Region]);
		foreach (DXFEdge item in stack)
		{
			item.Relative = startP;
		}
		bool flag = true;
		DXFVertex other = null;
		List<DXFEdge> list = null;
		while (stack.Count > 0)
		{
			DXFEdge edge = stack.Pop();
			if (edge.IsSreached)
			{
				continue;
			}
			edge.IsSreached = true;
			if (edge.Start.CompareTo(edge.Relative) != 0)
			{
				edge.Flip();
			}
			if (flag)
			{
				path.Add(edge);
				IEnumerable<DXFEdge> enumerable = parent.Graph[edge.End.Region].Where((DXFEdge e) => !e.IsSreached);
				if (enumerable.Count() > 0)
				{
					foreach (DXFEdge item2 in enumerable)
					{
						item2.Relative = edge.End;
						stack.Push(item2);
					}
				}
				else
				{
					other = edge.End;
					flag = false;
				}
				continue;
			}
			if (list == null)
			{
				list = new List<DXFEdge>();
			}
			list.Add(edge);
			bool flag2 = false;
			if (edge.End.CompareTo(other) == 0)
			{
				flag2 = true;
				FlipPath(list);
			}
			else if (list.First().Start.CompareTo(other) == 0)
			{
				flag2 = true;
			}
			if (flag2)
			{
				path.AddRange(list);
				DXFEdge dXFEdge = list.Last();
				list.Clear();
				list = null;
				IEnumerable<DXFEdge> enumerable2 = parent.Graph[dXFEdge.End.Region].Where((DXFEdge e) => !e.IsSreached);
				if (enumerable2.Count() > 0)
				{
					flag = true;
					foreach (DXFEdge item3 in enumerable2)
					{
						item3.Relative = dXFEdge.End;
						stack.Push(item3);
					}
				}
				else
				{
					other = dXFEdge.End;
					flag = false;
				}
				continue;
			}
			if (list.First().Start.CompareTo(edge.End) == 0)
			{
				path.InsertRange(path.IndexOf(path.First((DXFEdge e) => e.Start.CompareTo(edge.End) == 0)), list);
				list.Clear();
				list = null;
			}
			IEnumerable<DXFEdge> enumerable3 = parent.Graph[edge.End.Region].Where((DXFEdge e) => !e.IsSreached);
			foreach (DXFEdge item4 in enumerable3)
			{
				item4.Relative = edge.End;
				stack.Push(item4);
			}
		}
	}

	private void FormatPath()
	{
		int num = 0;
		int count = path.Count;
		for (int i = 1; i < count; i++)
		{
			if (path[i - 1 + num].End.CompareTo(path[i + num].Start) != 0)
			{
				path.Insert(i + num, new DXFEdge(path[i - 1 + num].End, path[i + num].Start, parent.Model));
				num++;
			}
			if (i == count - 1 && endP.CompareTo(path[count - 1 + num].End) != 0)
			{
				endP = path[count - 1 + num].End;
			}
		}
	}

	private DXFVertex GetRelativeVertex(DXFVertex vertex, IEnumerable<PointRegion> vertices)
	{
		int num = 1;
		while (true)
		{
			foreach (PointRegion vertex3 in vertices)
			{
				DXFVertex vertex2 = vertex3.GetVertex();
				if ((vertex2 - vertex).Length < (double)num)
				{
					return vertex2;
				}
			}
			if (num > 50)
			{
				num++;
			}
			if (num > 200)
			{
				num += 3;
			}
			if (num > 500)
			{
				num += 5;
			}
			num++;
		}
	}

	public void Reverse()
	{
		path.Reverse();
		foreach (DXFEdge item in path)
		{
			item.Flip();
		}
		DXFVertex dXFVertex = startP;
		startP = endP;
		endP = dXFVertex;
	}

	public static void FlipPath(List<DXFEdge> path)
	{
		path.Reverse();
		foreach (DXFEdge item in path)
		{
			item.Flip();
		}
	}

	private bool Assert(List<DXFEdge> path)
	{
		bool flag = true;
		flag &= path.First().Start.CompareTo(startP) == 0;
		flag &= path.Last().End.CompareTo(endP) == 0;
		if (!flag)
		{
		}
		for (int i = 1; i < path.Count; i++)
		{
			if (path[i - 1].End.CompareTo(path[i].Start) != 0)
			{
				flag = false;
			}
		}
		return flag;
	}
}
