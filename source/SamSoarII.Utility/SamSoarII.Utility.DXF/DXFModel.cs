using System;
using System.Collections.Generic;

namespace SamSoarII.Utility.DXF;

public class DXFModel
{
	private DXFGraph graph;

	private DXFReader converter;

	private List<DXFEntity> sections;

	private bool isdrill;

	public DXFGraph Graph => graph;

	public DXFReader Reader => converter;

	public List<DXFEntity> Sections => sections;

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

	public DXFModel()
	{
		sections = new List<DXFEntity>();
		graph = new DXFGraph(this);
	}

	public void Convert(string filename)
	{
		try
		{
			converter = new DXFReader(filename);
			while (!converter.Reader.EndOfStream)
			{
				converter.MoveNext();
				if (converter.CurrentValue == "EOF")
				{
					break;
				}
				string currentValue = converter.CurrentValue;
				string text = currentValue;
				if (!(text == "BLOCKS"))
				{
					if (text == "ENTITIES")
					{
						sections.Add(new DXFSection("ENTITIES", this));
					}
				}
				else
				{
					sections.Add(new DXFSection("BLOCKS", this));
				}
				bool flag = true;
			}
			converter.Close();
		}
		catch (Exception)
		{
			converter.Close();
		}
		Graph.IsDrill = IsDrill;
		Graph.Convert();
	}
}
