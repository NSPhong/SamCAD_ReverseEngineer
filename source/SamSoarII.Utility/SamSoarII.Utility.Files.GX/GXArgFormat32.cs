namespace SamSoarII.Utility.Files.GX;

public class GXArgFormat32 : GXArgFormat
{
	public GXArgFormat32(string _name, GXValueFormat[] _allows)
		: base(_name, _allows)
	{
	}

	public override string ToString(long hex, int len)
	{
		if (base.LastFit is GXValueFormatB)
		{
			return "K32" + base.LastFit.ToString(hex, len);
		}
		return base.ToString(hex, len);
	}

	public override GXArgFormat Clone()
	{
		return new GXArgFormat32(base.Name, base.Allows)
		{
			lastfit = lastfit
		};
	}
}
