namespace SamSoarII.Dock.AutoHide;

internal class AutoHideDrawerInfo
{
	private double start;

	private double end;

	public double Start => start;

	public double End => end;

	public AutoHideDrawerInfo(double _start, double _end)
	{
		start = _start;
		end = _end;
	}

	public bool Inside(double value)
	{
		return value >= start && value <= end;
	}
}
