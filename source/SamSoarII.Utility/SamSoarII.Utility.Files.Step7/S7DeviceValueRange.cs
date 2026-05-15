namespace SamSoarII.Utility.Files.Step7;

public class S7DeviceValueRange
{
	private Enum_S7BaseType e;

	private int start;

	private int count;

	public Enum_S7BaseType E => e;

	public int Start => start;

	public int Count => count;

	public int End => start + count - 1;

	public S7DeviceValueRange(Enum_S7BaseType _e, int _start, int _count)
	{
		e = _e;
		start = _start;
		count = _count;
	}
}
