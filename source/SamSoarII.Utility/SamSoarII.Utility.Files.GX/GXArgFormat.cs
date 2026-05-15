namespace SamSoarII.Utility.Files.GX;

public class GXArgFormat
{
	private string name;

	private GXValueFormat[] allows;

	protected GXValueFormat lastfit;

	public string Name => name;

	public GXValueFormat[] Allows => allows;

	public GXValueFormat LastFit => lastfit;

	public GXArgFormat(string _name, GXValueFormat[] _allows)
	{
		name = _name;
		allows = _allows;
	}

	public virtual bool IsOverflow(long hex, int len)
	{
		return lastfit?.IsOverflow(hex, len) ?? false;
	}

	public virtual string ToString(long hex, int len)
	{
		return lastfit?.ToString(hex, len) ?? "???";
	}

	public virtual string ToStringV(long hex, int len, long hex2, int len2)
	{
		return lastfit?.ToStringV(hex, len, hex2, len2) ?? "???";
	}

	public virtual string ToStringZ(long hex, int len, long hex2, int len2)
	{
		return lastfit?.ToStringZ(hex, len, hex2, len2) ?? "???";
	}

	public virtual string ToStringDot(long hex, int len, long hex2, int len2)
	{
		return lastfit?.ToStringDot(hex, len, hex2, len2) ?? "???";
	}

	public virtual string ToStringCom(long hex, int len, long hex2, int len2)
	{
		return lastfit?.ToStringCom(hex, len, hex2, len2) ?? "???";
	}

	public virtual string ToString_OverflowClear(long hex, int len)
	{
		if (IsOverflow(hex, len))
		{
			return "???";
		}
		return ToString(hex, len);
	}

	public virtual string ToStringV_OverflowClear(long hex, int len, long hex2, int len2)
	{
		if (IsOverflow(hex, len))
		{
			return "???";
		}
		return ToStringV(hex, len, hex2, len2);
	}

	public virtual string ToStringZ_OverflowClear(long hex, int len, long hex2, int len2)
	{
		if (IsOverflow(hex, len))
		{
			return "???";
		}
		return ToStringZ(hex, len, hex2, len2);
	}

	public virtual string ToStringDot_OverflowClear(long hex, int len, long hex2, int len2)
	{
		if (IsOverflow(hex, len))
		{
			return "???";
		}
		return ToStringDot(hex, len, hex2, len2);
	}

	public virtual string ToStringCom_OverflowClear(long hex, int len, long hex2, int len2)
	{
		if (IsOverflow(hex, len))
		{
			return "???";
		}
		return ToStringCom(hex, len, hex2, len2);
	}

	public bool Check(long hex, int len)
	{
		GXValueFormat[] array = allows;
		foreach (GXValueFormat gXValueFormat in array)
		{
			if (gXValueFormat.Check(hex, len))
			{
				lastfit = gXValueFormat;
				return true;
			}
		}
		lastfit = null;
		return false;
	}

	public bool CheckV(long hex, int len, long hex2, int len2)
	{
		GXValueFormat[] array = allows;
		foreach (GXValueFormat gXValueFormat in array)
		{
			if (gXValueFormat.CheckV(hex, len, hex2, len2))
			{
				lastfit = gXValueFormat;
				return true;
			}
		}
		lastfit = null;
		return false;
	}

	public bool CheckZ(long hex, int len, long hex2, int len2)
	{
		GXValueFormat[] array = allows;
		foreach (GXValueFormat gXValueFormat in array)
		{
			if (gXValueFormat.CheckZ(hex, len, hex2, len2))
			{
				lastfit = gXValueFormat;
				return true;
			}
		}
		lastfit = null;
		return false;
	}

	public bool CheckDot(long hex, int len, long hex2, int len2)
	{
		GXValueFormat[] array = allows;
		foreach (GXValueFormat gXValueFormat in array)
		{
			if (gXValueFormat.CheckDot(hex, len, hex2, len2))
			{
				lastfit = gXValueFormat;
				return true;
			}
		}
		lastfit = null;
		return false;
	}

	public bool CheckCom(long hex, int len, long hex2, int len2)
	{
		GXValueFormat[] array = allows;
		foreach (GXValueFormat gXValueFormat in array)
		{
			if (gXValueFormat.CheckCom(hex, len, hex2, len2))
			{
				lastfit = gXValueFormat;
				return true;
			}
		}
		lastfit = null;
		return false;
	}

	public virtual GXArgFormat Clone()
	{
		return new GXArgFormat(name, allows)
		{
			lastfit = lastfit
		};
	}
}
