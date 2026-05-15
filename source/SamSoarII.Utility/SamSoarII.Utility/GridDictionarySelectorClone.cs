using System;
using System.Collections;
using System.Collections.Generic;

namespace SamSoarII.Utility;

public class GridDictionarySelectorClone<T> : IEnumerable<T>, IEnumerable, IGridDictionarySelector<T>, IEnumerator<T>, IDisposable, IEnumerator
{
	private T[,] data;

	private int top;

	private int left;

	private int width;

	private int height;

	private int x;

	private int y;

	public int X1 => top;

	public int X2 => top + width - 1;

	public int Y1 => left;

	public int Y2 => left + height - 1;

	public int Width => width;

	public int Height => height;

	public T Current => (x >= 0 && x < width && y >= 0 && y < height) ? data[x, y] : default(T);

	object IEnumerator.Current => Current;

	public T this[int x, int y]
	{
		get
		{
			return (x >= top && x < top + width && y >= left && y < left + height) ? data[x - top, y - left] : default(T);
		}
		set
		{
			if (x >= top && x < top + width && y >= left && y < left + height)
			{
				data[x - top, y - left] = value;
			}
		}
	}

	public GridDictionarySelectorClone(IGridDictionarySelector<T> origin)
	{
		top = origin.Y1;
		left = origin.X1;
		width = origin.Width;
		height = origin.Height;
		data = new T[width, height];
		origin.Reset();
		for (x = 0; x < width; x++)
		{
			for (y = 0; y < height; y++)
			{
				origin.MoveNext();
				data[x, y] = origin.Current;
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

	public void Dispose()
	{
	}

	public bool MoveNext()
	{
		while (y < height)
		{
			if (++x >= width)
			{
				x = 0;
				y++;
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
		x = -1;
		y = 0;
	}

	public void Clear()
	{
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				data[i, j] = default(T);
			}
		}
	}

	public IGridDictionarySelector<T> Clone()
	{
		return new GridDictionarySelectorClone<T>(this);
	}
}
