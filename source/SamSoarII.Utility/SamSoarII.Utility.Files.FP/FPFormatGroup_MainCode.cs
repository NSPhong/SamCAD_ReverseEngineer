namespace SamSoarII.Utility.Files.FP;

public class FPFormatGroup_MainCode : FPFormatGroup
{
	public FPFormatGroup_MainCode()
		: base(0u)
	{
	}

	protected override int Compare(FPFormat f0, FPFormat f1)
	{
		return (f0.MainCode & 0xFFFFFF00u).CompareTo(f1.MainCode & 0xFFFFFF00u);
	}

	protected override FPFormat Create(FPFormat fmt)
	{
		if (fmt.IsUseOffset)
		{
			return fmt;
		}
		FPFormatGroup_Offset fPFormatGroup_Offset = new FPFormatGroup_Offset(fmt.MainCode);
		fPFormatGroup_Offset.Set(fmt);
		return fPFormatGroup_Offset;
	}
}
