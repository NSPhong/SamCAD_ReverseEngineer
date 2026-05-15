namespace SamSoarII.Utility.Files.GX;

public class GXArgFormatF : GXArgFormat
{
	public GXArgFormatF(string _name, GXValueFormat[] _allows)
		: base(_name, _allows)
	{
	}

	public override GXArgFormat Clone()
	{
		return new GXArgFormatF(base.Name, base.Allows)
		{
			lastfit = lastfit
		};
	}
}
