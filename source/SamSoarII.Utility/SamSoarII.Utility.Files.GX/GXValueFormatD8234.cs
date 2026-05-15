namespace SamSoarII.Utility.Files.GX;

public class GXValueFormatD8234 : GXValueFormatD
{
	public GXValueFormatD8234(string _name, byte[] _data)
		: base(_name, _data)
	{
	}

	public override bool IsOverflow(long hex, int len)
	{
		int num = 0;
		for (int num2 = len - 1; num2 >= data.Length; num2--)
		{
			num <<= 8;
			num += (int)((hex >> 8 * (len - num2 - 1)) & 0xFF);
		}
		return num < 0 || num >= 8234;
	}
}
