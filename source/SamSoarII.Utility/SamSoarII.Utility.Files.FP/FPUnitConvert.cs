using System.Collections.Generic;

namespace SamSoarII.Utility.Files.FP;

public abstract class FPUnitConvert
{
	public static readonly Dictionary<string, FPUnitConvert> ItemOfNames = new Dictionary<string, FPUnitConvert>();

	public abstract string OldName { get; }

	public abstract void Add(string item);
}
