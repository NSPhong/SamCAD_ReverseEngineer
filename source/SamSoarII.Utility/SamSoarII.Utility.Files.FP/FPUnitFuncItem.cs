namespace SamSoarII.Utility.Files.FP;

public class FPUnitFuncItem
{
	private bool isv;

	private bool ish;

	private FPUnitBaseConvert unit;

	public bool IsV
	{
		get
		{
			return isv;
		}
		set
		{
			isv = value;
		}
	}

	public bool IsH
	{
		get
		{
			return ish;
		}
		set
		{
			ish = value;
		}
	}

	public FPUnitBaseConvert Unit
	{
		get
		{
			return unit;
		}
		set
		{
			unit = value;
		}
	}
}
