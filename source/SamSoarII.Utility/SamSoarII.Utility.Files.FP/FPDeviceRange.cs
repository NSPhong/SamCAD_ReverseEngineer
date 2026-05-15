namespace SamSoarII.Utility.Files.FP;

public class FPDeviceRange
{
	private FPValueFormat format;

	private int start;

	private int end;

	private bool issp;

	public FPValueFormat Format => format;

	public int Start => start;

	public int End => end;

	public bool IsSp
	{
		get
		{
			return issp;
		}
		set
		{
			issp = value;
		}
	}

	public FPDeviceRange(FPValueFormat _format, int _start, int _end)
	{
		format = _format;
		start = _start;
		end = _end;
		issp = false;
	}
}
