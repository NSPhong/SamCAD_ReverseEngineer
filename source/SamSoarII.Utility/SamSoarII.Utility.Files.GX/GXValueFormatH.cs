namespace SamSoarII.Utility.Files.GX;

public class GXValueFormatH : GXValueFormat
{
	public GXValueFormatH(string _name, byte[] _data)
		: base(_name, _data)
	{
	}

	public override string ToString(long hex, int len)
	{
		int num = 0;
		for (int num2 = len - 1; num2 >= base.Data.Length; num2--)
		{
			num <<= 8;
			num += (int)((hex >> 8 * (len - num2 - 1)) & 0xFF);
		}
		return $"{base.Name:s}{num:x}";
	}
}
