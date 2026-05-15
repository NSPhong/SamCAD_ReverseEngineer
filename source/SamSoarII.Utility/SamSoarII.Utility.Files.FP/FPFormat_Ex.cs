using System.Collections.Generic;

namespace SamSoarII.Utility.Files.FP;

public class FPFormat_Ex
{
	public static readonly FPFormatGroup Units = new FPFormatGroup_MainCode();

	public static readonly FPFormatGroup Values = new FPFormatGroup_MainRegi();

	public static readonly Dictionary<string, FPFormat> ItemOfNames = new Dictionary<string, FPFormat>();
}
