namespace SamSoarII.Utility.Files.GX;

public class GXValueFormatDot : GXValueFormat
{
	public GXValueFormatDot(string _name, byte[] _data)
		: base(_name, _data)
	{
	}

	public override bool Check(long hex, int len)
	{
		return false;
	}

	public override bool CheckDot(long hex, int len, long hex2, int len2)
	{
		if (((hex >> (len - 1) * 8) & 0xFF) != 242)
		{
			return false;
		}
		return base.Check(hex2, len2);
	}
}
