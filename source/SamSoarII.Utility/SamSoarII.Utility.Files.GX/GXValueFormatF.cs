namespace SamSoarII.Utility.Files.GX;

public class GXValueFormatF : GXValueFormat
{
	public GXValueFormatF(string _name, byte[] _data)
		: base(_name, _data)
	{
	}

	public override string ToString(long hex, int len)
	{
		uint num = 0u;
		for (int num2 = len - 1; num2 >= base.Data.Length; num2--)
		{
			num <<= 8;
			num += (uint)(int)((hex >> 8 * (len - num2 - 1)) & 0xFF);
		}
		return $"{base.Name}{ValueConverter.UIntToFloat(num)}";
	}
}
