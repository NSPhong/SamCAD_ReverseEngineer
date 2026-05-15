namespace SamSoarII.Utility.Files.FP;

public class FPRedirectItem
{
	private int code;

	private int offset;

	public int Code => code;

	public int Offset => offset;

	public FPRedirectItem(int _code, int _offset)
	{
		code = _code;
		offset = _offset;
	}
}
