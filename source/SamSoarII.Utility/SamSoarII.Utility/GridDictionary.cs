using System;
using System.Collections;
using System.Collections.Generic;

namespace SamSoarII.Utility;

public class GridDictionary<T> : IEnumerable<T>, IEnumerable
{
	private T[][] data;

	private int width;

	private int height;

	public int Width
	{
		get
		{
			return width;
		}
		set
		{
			int val = width;
			T[][] array = data;
			width = value;
			data = new T[height][];
			for (int i = 0; i < height; i++)
			{
				data[i] = new T[width];
				Array.Copy(array[i], data[i], System.Math.Min(val, width));
			}
		}
	}

	public int Height
	{
		get
		{
			return height;
		}
		set
		{
			int num = height;
			T[][] array = data;
			height = value;
			data = new T[height][];
			for (int i = 0; i < height; i++)
			{
				data[i] = ((array != null && i < num) ? array[i] : new T[width]);
			}
		}
	}

	public T this[int x, int y]
	{
		get
		{
			if (!_Assert(x, y))
			{
				return default(T);
			}
			return data[y][x];
		}
		set
		{
			if (x >= 0 && x < Width && y >= 0)
			{
				while (Height <= y)
				{
					Height *= 2;
				}
				data[y][x] = value;
			}
		}
	}

	public GridDictionary(int _width, int _height = 8)
	{
		width = _width;
		Height = _height;
	}

	public void ResizeToWidth(int delta)
	{
		T[][] array = new T[height][];
		for (int i = 0; i < height; i++)
		{
			if (data[i] != null)
			{
				array[i] = new T[width + delta];
				for (int j = 0; j < width + ((delta <= 0) ? delta : 0); j++)
				{
					array[i][j] = data[i][j];
				}
			}
		}
		width += delta;
		data = array;
	}

	private bool _Assert(int x, int y)
	{
		return x >= 0 && x < Width && y >= 0 && y < Height;
	}

	private bool _Assert(int x1, int x2, int y1, int y2)
	{
		return _Assert(x1, y1) && _Assert(x2, y2) && x1 <= x2 && y1 <= y2;
	}

	public IEnumerator<T> GetEnumerator()
	{
		return new GridDictionarySelector<T>(this, 0, Width - 1, 0, Height - 1);
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	public void Clear()
	{
		Clear(0, Width - 1, 0, Height - 1);
	}

	public void Clear(int x1, int x2, int y1, int y2)
	{
		if (!_Assert(x1, x2, y1, y2))
		{
			return;
		}
		for (int i = y1; i <= y2; i++)
		{
			for (int j = x1; j <= x2; j++)
			{
				data[i][j] = default(T);
			}
		}
	}

	public GridDictionarySelector<T> SelectRange(int x1, int x2, int y1, int y2)
	{
		x1 = System.Math.Max(x1, 0);
		y1 = System.Math.Max(y1, 0);
		x2 = System.Math.Min(x2, Width - 1);
		y2 = System.Math.Min(y2, Height - 1);
		return _Assert(x1, x2, y1, y2) ? new GridDictionarySelector<T>(this, x1, x2, y1, y2) : GridDictionarySelector<T>.Empty;
	}

	public void Set(int x, int y, IGridDictionarySelector<T> selector)
	{
		if (selector.Width <= 0 || selector.Height <= 0 || x < 0 || x + selector.Width >= Width || y < 0 || y + selector.Height >= Height)
		{
			return;
		}
		if (Height < y + selector.Height)
		{
			Height = (int)((double)(y + selector.Height) * 1.5);
		}
		for (int i = x; i < x + selector.Width; i++)
		{
			for (int j = y; j < y + selector.Height; j++)
			{
				data[j][i] = selector[i - x + selector.X1, j - y + selector.Y1];
			}
		}
	}
}
