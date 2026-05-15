namespace SamSoarII.Utility.Files.GX;

public class GXArgFormat16 : GXArgFormat
{
	public GXArgFormat16(string _name, GXValueFormat[] _allows)
		: base(_name, _allows)
	{
	}

	public override string ToString(long hex, int len)
	{
		if (base.LastFit is GXValueFormatB)
		{
			return "K16" + base.LastFit.ToString(hex, len);
		}
		return base.ToString(hex, len);
	}

	public override GXArgFormat Clone()
	{
		return new GXArgFormat16(base.Name, base.Allows)
		{
			lastfit = lastfit
		};
	}
}
