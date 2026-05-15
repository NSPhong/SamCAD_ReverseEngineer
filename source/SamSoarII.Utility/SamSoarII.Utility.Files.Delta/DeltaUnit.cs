namespace SamSoarII.Utility.Files.Delta;

public class DeltaUnit
{
	public const int TYPE_LD = 1;

	public const int TYPE_LDI = 2;

	public const int TYPE_LDP = 3;

	public const int TYPE_LDF = 4;

	public const int TYPE_HLINE = 5;

	public const int TYPE_COMBINE = 6;

	public const int TYPE_END = 7;

	public const int TYPE_SYMB = 9;

	public const int TYPE_SYMB_IN = 10;

	public const int TYPE_RECT_IN = 11;

	public const int TYPE_RECT_OUT = 12;

	public const int TYPE_OUT = 13;

	public const int TYPE_SET = 15;

	public const int TYPE_RST = 16;

	public const int TYPE_EMPTY = 20;

	public const int TYPE_COMBINE_OUT = 29;

	private int x;

	private int y;

	private int width;

	private int height;

	public int X
	{
		get
		{
			return x;
		}
		set
		{
			x = value;
		}
	}

	public int Y
	{
		get
		{
			return y;
		}
		set
		{
			y = value;
		}
	}

	public int Width
	{
		get
		{
			return width;
		}
		set
		{
			width = value;
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
			height = value;
		}
	}

	public DeltaUnit()
	{
		x = 0;
		y = 0;
		width = 1;
		height = 1;
	}
}
