using System;
using System.Collections;
using System.Collections.Generic;

namespace SamSoarII.Utility;

public class GridDictionarySelector<T> : IGridDictionarySelector<T>, IEnumerable<T>, IEnumerable, IEnumerator<T>, IDisposable, IEnumerator
{
	private GridDictionary<T> dict;

	private int x1;

	private int x2;

	private int y1;

	private int y2;

	private int cx;

	private int cy;

	public static GridDictionarySelector<T> Empty { get; private set; } = new GridDictionarySelector<T>(null, 0, 0, 0, 0);

	public int X1 => x1;

	public int X2 => x2;

	public int Y1 => y1;

	public int Y2 => y2;

	public int Width => x2 - x1 + 1;

	public int Height => y2 - y1 + 1;

	public T Current => (cx >= x1 && cx <= x2 && cy >= y1 && cy <= y2 && dict != null) ? dict[cx, cy] : default(T);

	object IEnumerator.Current => Current;

	public T this[int x, int y]
	{
		get
		{
			return (dict != null) ? dict[x, y] : default(T);
		}
		set
		{
			if (dict != null)
			{
				dict[x, y] = value;
			}
		}
	}

	public IEnumerator<T> GetEnumerator()
	{
		Reset();
		return this;
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		Reset();
		return this;
	}

	public GridDictionarySelector(GridDictionary<T> _dict, int _x1, int _x2, int _y1, int _y2)
	{
		dict = _dict;
		x1 = _x1;
		x2 = _x2;
		y1 = _y1;
		y2 = _y2;
		Reset();
	}

	public void Dispose()
	{
	}

	public bool MoveNext()
	{
		if (dict == null)
		{
			return false;
		}
		while (cy <= y2)
		{
			if (++cx > x2)
			{
				cx = x1;
				cy++;
			}
			if (Current != null)
			{
				return true;
			}
		}
		return false;
	}

	public void Reset()
	{
		cx = x1 - 1;
		cy = y1;
	}

	public void Clear()
	{
		if (dict != null)
		{
			dict.Clear(x1, x2, y1, y2);
		}
	}

	public IGridDictionarySelector<T> Clone()
	{
		return new GridDictionarySelectorClone<T>(this);
	}
}
