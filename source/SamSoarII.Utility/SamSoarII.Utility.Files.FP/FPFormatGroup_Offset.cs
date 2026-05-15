namespace SamSoarII.Utility.Files.FP;

public class FPFormatGroup_Offset : FPFormatGroup
{
	public FPFormatGroup_Offset(uint _maincode)
		: base(_maincode)
	{
	}

	protected override int Compare(FPFormat f0, FPFormat f1)
	{
		return f0.Offset.CompareTo(f1.Offset);
	}

	protected override FPFormat Create(FPFormat fmt)
	{
		return fmt;
	}
}
