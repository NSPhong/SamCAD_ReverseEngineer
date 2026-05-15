namespace SamSoarII.Utility.Files.FP;

public class FPUnitBaseVar : FPItemConvert
{
	private string text;

	private FPValueConvert conv;

	public string Text => text;

	public FPValueConvert Conv => conv;

	public FPUnitBaseVar(string _text, FPValueConvert _conv)
	{
		text = _text;
		conv = _conv;
	}
}
