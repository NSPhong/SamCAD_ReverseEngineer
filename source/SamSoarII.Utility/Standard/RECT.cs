using System;

namespace Standard;

internal struct RECT
{
	private int _left;

	private int _top;

	private int _right;

	private int _bottom;

	public int Left
	{
		get
		{
			return _left;
		}
		set
		{
			_left = value;
		}
	}

	public int Right
	{
		get
		{
			return _right;
		}
		set
		{
			_right = value;
		}
	}

	public int Top
	{
		get
		{
			return _top;
		}
		set
		{
			_top = value;
		}
	}

	public int Bottom
	{
		get
		{
			return _bottom;
		}
		set
		{
			_bottom = value;
		}
	}

	public int Width => _right - _left;

	public int Height => _bottom - _top;

	public Standard.POINT Position => new Standard.POINT
	{
		x = _left,
		y = _top
	};

	public Standard.SIZE Size => new Standard.SIZE
	{
		cx = Width,
		cy = Height
	};

	public void Offset(int dx, int dy)
	{
		_left += dx;
		_top += dy;
		_right += dx;
		_bottom += dy;
	}

	public static Standard.RECT Union(Standard.RECT rect1, Standard.RECT rect2)
	{
		return new Standard.RECT
		{
			Left = Math.Min(rect1.Left, rect2.Left),
			Top = Math.Min(rect1.Top, rect2.Top),
			Right = Math.Max(rect1.Right, rect2.Right),
			Bottom = Math.Max(rect1.Bottom, rect2.Bottom)
		};
	}

	public override bool Equals(object obj)
	{
		try
		{
			Standard.RECT rECT = (Standard.RECT)obj;
			return rECT._bottom == _bottom && rECT._left == _left && rECT._right == _right && rECT._top == _top;
		}
		catch (InvalidCastException)
		{
			return false;
		}
	}

	public override int GetHashCode()
	{
		return ((_left << 16) | Standard.Utility.LOWORD(_right)) ^ ((_top << 16) | Standard.Utility.LOWORD(_bottom));
	}
}
