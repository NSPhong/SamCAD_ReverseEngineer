namespace SamSoarII.Utility.Files.FP;

public class FPFormatGroup_MainRegi : FPFormatGroup
{
	public FPFormatGroup_MainRegi()
		: base(0u)
	{
	}

	protected override int Compare(FPFormat f0, FPFormat f1)
	{
		return (f0.MainCode & 0xFF00).CompareTo(f1.MainCode & 0xFF00);
	}

	protected override FPFormat Create(FPFormat fmt)
	{
		return fmt;
	}
}
