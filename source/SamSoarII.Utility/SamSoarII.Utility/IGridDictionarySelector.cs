using System;
using System.Collections;
using System.Collections.Generic;

namespace SamSoarII.Utility;

public interface IGridDictionarySelector<T> : IEnumerable<T>, IEnumerable, IEnumerator<T>, IDisposable, IEnumerator
{
	int X1 { get; }

	int X2 { get; }

	int Y1 { get; }

	int Y2 { get; }

	int Width { get; }

	int Height { get; }

	T this[int x, int y] { get; set; }

	void Clear();

	IGridDictionarySelector<T> Clone();
}
