using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamSoarII.Utility;

public class StringSorter<T> : IDisposable
{
	private class StringSorterNode : IDisposable
	{
		private T origin;

		private string text;

		private byte[] data;

		private StringSorterNode next;

		public T Origin => origin;

		public string Text => text;

		public byte[] Data => data;

		public StringSorterNode Next
		{
			get
			{
				return next;
			}
			set
			{
				next = value;
			}
		}

		public StringSorterNode(T _origin, string _text)
		{
			origin = _origin;
			text = _text;
			next = null;
			data = Encoding.Default.GetBytes(text);
		}

		public void Dispose()
		{
			origin = default(T);
			text = null;
			next = null;
			data = null;
		}
	}

	private StringSorterNode[] nodes;

	private StringSorterNode[] heads;

	public StringSorter(IEnumerable<T> _origins, Func<T, string> _func)
	{
		nodes = new StringSorterNode[_origins.Count()];
		heads = new StringSorterNode[256];
		int num = 0;
		foreach (T _origin in _origins)
		{
			nodes[num++] = new StringSorterNode(_origin, _func(_origin));
		}
	}

	public void Dispose()
	{
		StringSorterNode[] array = nodes;
		foreach (StringSorterNode stringSorterNode in array)
		{
			stringSorterNode.Dispose();
		}
		nodes = null;
		heads = null;
	}

	public List<T> Sort()
	{
		int num = nodes.Select((StringSorterNode n) => n.Data.Length).Max();
		for (int num2 = num - 1; num2 >= 0; num2--)
		{
			for (int num3 = 0; num3 < 256; num3++)
			{
				heads[num3] = null;
			}
			for (int num4 = nodes.Length - 1; num4 >= 0; num4--)
			{
				int num5 = ((num2 < nodes[num4].Data.Length) ? nodes[num4].Data[num2] : 0);
				nodes[num4].Next = heads[num5];
				heads[num5] = nodes[num4];
			}
			int num6 = 0;
			for (int num7 = 0; num7 < 256; num7++)
			{
				for (StringSorterNode stringSorterNode = heads[num7]; stringSorterNode != null; stringSorterNode = stringSorterNode.Next)
				{
					nodes[num6++] = stringSorterNode;
				}
			}
		}
		return nodes.Select((StringSorterNode n) => n.Origin).ToList();
	}
}
