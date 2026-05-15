namespace SamSoarII.Utility.Files.GX;

public class GXArgFormatB : GXArgFormat
{
	public GXArgFormatB(string _name, GXValueFormat[] _allows)
		: base(_name, _allows)
	{
	}

	public override string ToString(long hex, int len)
	{
		if (base.LastFit is GXValueFormatD)
		{
			return base.LastFit.ToString(hex, len) + ".0";
		}
		return base.ToString(hex, len);
	}

	public override GXArgFormat Clone()
	{
		return new GXArgFormatB(base.Name, base.Allows)
		{
			lastfit = lastfit
		};
	}
}
