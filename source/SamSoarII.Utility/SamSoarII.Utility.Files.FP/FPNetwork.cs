namespace SamSoarII.Utility.Files.FP;

public class FPNetwork
{
	private int id;

	private ushort code;

	private int step;

	private int flag;

	private GridDictionary<FPUnit> children;

	private GridDictionary<FPVLine> vlines;

	private int width;

	private int height;

	public int ID => id;

	public ushort Code => code;

	public int Step => step;

	public int Flag => flag;

	public GridDictionary<FPUnit> Children => children;

	public GridDictionary<FPVLine> VLines => vlines;

	public int Width => width;

	public int Height => height;

	public FPNetwork(int _id, ushort _code, int _step, int _flag, int _height)
	{
		id = _id;
		code = _code;
		step = _step;
		flag = _flag;
		children = new GridDictionary<FPUnit>(10);
		vlines = new GridDictionary<FPVLine>(10);
		width = 10;
		height = _height;
	}
}
