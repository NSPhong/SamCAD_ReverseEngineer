using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace SamSoarII.Utility.DXF;

public class DXFGraph
{
	private List<DXFEdge> path;

	private DXFVertex startP = new DXFVertex(new Point(0.0, 0.0));

	private DXFModel model;

	private bool isdrill;

	public RBDictionary<PointRegion, List<DXFEdge>> Graph { get; set; }

	public RBDictionary<PointRegion, bool> Flags { get; set; }

	public List<DXFEdge> Path => path;

	public DXFVertex StartP => startP;

	public DXFModel Model => model;

	public bool IsDrill
	{
		get
		{
			return isdrill;
		}
		set
		{
			isdrill = value;
		}
	}

	public DXFGraph(DXFModel model)
	{
		this.model = model;
		Graph = new RBDictionary<PointRegion, List<DXFEdge>>();
		Flags = new RBDictionary<PointRegion, bool>();
	}

	public void AddEdge(DXFEdge edge)
	{
		if (Graph.ContainsKey(edge.Start.Region))
		{
			Graph[edge.Start.Region].Add(edge);
		}
		else
		{
			Graph.Add(edge.Start.Region, new List<DXFEdge> { edge });
			Flags.Add(edge.Start.Region, value: false);
		}
		if (Graph.ContainsKey(edge.End.Region))
		{
			Graph[edge.End.Region].Add(edge);
			return;
		}
		Graph.Add(edge.End.Region, new List<DXFEdge> { edge });
		Flags.Add(edge.End.Region, value: false);
	}

	public void Convert()
	{
		path = new List<DXFEdge>();
		HashSet<PointRegion> hashSet = new HashSet<PointRegion>(Graph.Keys);
		List<OneLinearGraph> list = new List<OneLinearGraph>();
		StreamWriter streamWriter = new StreamWriter($"{FileHelper.AppRootPath}/debug.txt", append: false);
		foreach (PointRegion item in hashSet)
		{
			List<PointRegion> nextRegions = GetNextRegions(item);
			streamWriter.WriteLine("=========================================================================");
			streamWriter.WriteLine("P({0},{1}), D({2})", item.X, item.Y, nextRegions.Count());
			foreach (PointRegion item2 in nextRegions)
			{
				streamWriter.Write("({0},{1}) ", item2.X, item2.Y);
			}
			streamWriter.WriteLine();
		}
		streamWriter.Close();
		while (hashSet.Count > 0)
		{
			List<PointRegion> subGraph = new List<PointRegion>();
			PointRegion region = hashSet.First();
			DPFSearch(region, hashSet, subGraph);
			list.Add(new OneLinearGraph(this, subGraph));
		}
		CombinePath(list);
	}

	private void DPFSearch(PointRegion region, HashSet<PointRegion> regions, List<PointRegion> subGraph)
	{
		Flags[region] = true;
		regions.Remove(region);
		subGraph.Add(region);
		foreach (PointRegion nextRegion in GetNextRegions(region))
		{
			if (!Flags[nextRegion])
			{
				DPFSearch(nextRegion, regions, subGraph);
			}
		}
	}

	private List<PointRegion> GetNextRegions(PointRegion region)
	{
		List<PointRegion> list = new List<PointRegion>();
		List<DXFEdge> list2 = Graph[region];
		foreach (DXFEdge item in list2)
		{
			if (item.Start.CompareTo(item.End) != 0)
			{
				if (item.Start.Region.CompareTo(region) == 0)
				{
					list.Add(item.End.Region);
				}
				else
				{
					list.Add(item.Start.Region);
				}
			}
		}
		return list;
	}

	private void CombinePath(List<OneLinearGraph> graphs)
	{
		if (graphs.Count == 0)
		{
			return;
		}
		IEnumerable<OneLinearGraph> source = graphs.Where((OneLinearGraph graph) => graph.IsFirst);
		OneLinearGraph oneLinearGraph2;
		if (source.Count() > 0)
		{
			OneLinearGraph oneLinearGraph = source.First();
			path.AddRange(oneLinearGraph.Path);
			graphs.Remove(oneLinearGraph);
			if (graphs.Count <= 0)
			{
				return;
			}
			oneLinearGraph2 = GetRelativeGraph(oneLinearGraph.EndP, graphs);
			path.Add(new DXFEdge(oneLinearGraph.EndP, oneLinearGraph2.StartP, model));
		}
		else
		{
			oneLinearGraph2 = GetRelativeGraph(startP, graphs);
			path.Add(new DXFEdge(startP, oneLinearGraph2.StartP, model));
		}
		path.AddRange(oneLinearGraph2.Path);
		graphs.Remove(oneLinearGraph2);
		while (graphs.Count > 0)
		{
			OneLinearGraph relativeGraph = GetRelativeGraph(oneLinearGraph2.EndP, graphs);
			graphs.Remove(relativeGraph);
			path.Add(new DXFEdge(oneLinearGraph2.EndP, relativeGraph.StartP, model));
			path.AddRange(relativeGraph.Path);
			oneLinearGraph2 = relativeGraph;
		}
	}

	private OneLinearGraph GetRelativeGraph(DXFVertex point, IEnumerable<OneLinearGraph> graphs)
	{
		int num = 1;
		while (true)
		{
			foreach (OneLinearGraph graph in graphs)
			{
				if ((graph.StartP - point).Length < (double)num)
				{
					return graph;
				}
				if ((graph.EndP - point).Length < (double)num)
				{
					graph.Reverse();
					return graph;
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

	public int Degree(PointRegion region)
	{
		if (Graph.ContainsKey(region))
		{
			return Graph[region].Count;
		}
		return -1;
	}

	private bool Assert()
	{
		bool result = true;
		for (int i = 1; i < path.Count; i++)
		{
			if (path[i - 1].End.CompareTo(path[i].Start) != 0)
			{
				result = false;
			}
		}
		return result;
	}
}
