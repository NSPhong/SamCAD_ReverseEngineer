namespace SamSoarII.Utility.Files.GX;

public class GXValueFormatCom : GXValueFormat
{
	public GXValueFormatCom(string _name, byte[] _data)
		: base(_name, _data)
	{
	}

	public override bool Check(long hex, int len)
	{
		return false;
	}

	public override bool CheckCom(long hex, int len, long hex2, int len2)
	{
		if (((hex >> (len - 1) * 8) & 0xFF) != 241)
		{
			return false;
		}
		return base.Check(hex2, len2);
	}
}
