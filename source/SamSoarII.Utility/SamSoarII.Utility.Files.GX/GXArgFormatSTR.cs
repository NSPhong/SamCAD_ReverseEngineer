namespace SamSoarII.Utility.Files.GX;

public class GXArgFormatSTR : GXArgFormat
{
	public GXArgFormatSTR(string _name, GXValueFormat[] _allows)
		: base(_name, _allows)
	{
	}

	public override string ToString(long hex, int len)
	{
		return base.ToString(hex, len);
	}

	public virtual string ToString(byte[] data)
	{
		if (base.LastFit is GXValueFormatSTR)
		{
			return ((GXValueFormatSTR)base.LastFit).ToString(data);
		}
		if (data.Length < 8)
		{
			long num = 0L;
			for (int num2 = data.Length - 1; num2 >= 0; num2--)
			{
				num <<= 8;
				num += data[num2];
			}
			return ToString(num, data.Length);
		}
		return "???";
	}

	public virtual bool Check(byte[] data)
	{
		if (data == null || data.Length == 0 || data[0] != 238)
		{
			return false;
		}
		GXValueFormat[] array = base.Allows;
		foreach (GXValueFormat gXValueFormat in array)
		{
			if (gXValueFormat is GXValueFormatSTR)
			{
				lastfit = gXValueFormat;
				return true;
			}
		}
		return false;
	}

	public override GXArgFormat Clone()
	{
		return new GXArgFormatSTR(base.Name, base.Allows)
		{
			lastfit = lastfit
		};
	}
}
