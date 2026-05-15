namespace SamSoarII.Utility.Files.GX;

public class GXValueFormat
{
	protected string name;

	protected byte[] data;

	public string Name => name;

	public byte[] Data => data;

	public static int GetOffset(long hex, int len, int start = 1)
	{
		int num = 0;
		for (int num2 = len - 1; num2 >= start; num2--)
		{
			num <<= 8;
			num += (int)((hex >> 8 * (len - num2 - 1)) & 0xFF);
		}
		return num;
	}

	public GXValueFormat(string _name, byte[] _data)
	{
		name = _name;
		data = _data;
	}

	public virtual bool IsOverflow(long hex, int len)
	{
		return false;
	}

	public virtual string ToString(long hex, int len)
	{
		int num = 0;
		for (int num2 = len - 1; num2 >= data.Length; num2--)
		{
			num <<= 8;
			num += (int)((hex >> 8 * (len - num2 - 1)) & 0xFF);
		}
		return $"{name:s}{num:d}";
	}

	public virtual string ToStringV(long hex, int len, long hex2, int len2)
	{
		int num = 0;
		for (int num2 = len - 1; num2 >= data.Length; num2--)
		{
			num <<= 8;
			num += (int)((hex >> 8 * (len - num2 - 1)) & 0xFF);
		}
		return $"{ToString(hex2, len2):s}V{num:d}";
	}

	public virtual string ToStringZ(long hex, int len, long hex2, int len2)
	{
		int num = 0;
		for (int num2 = len - 1; num2 >= data.Length; num2--)
		{
			num <<= 8;
			num += (int)((hex >> 8 * (len - num2 - 1)) & 0xFF);
		}
		return $"{ToString(hex2, len2):s}Z{num:d}";
	}

	public virtual string ToStringCom(long hex, int len, long hex2, int len2)
	{
		int num = 0;
		for (int num2 = len - 1; num2 >= data.Length; num2--)
		{
			num <<= 8;
			num += (int)((hex >> 8 * (len - num2 - 1)) & 0xFF);
		}
		return $"K{num * 4}" + ToString(hex2, len2);
	}

	public virtual string ToStringDot(long hex, int len, long hex2, int len2)
	{
		int num = 0;
		for (int num2 = len - 1; num2 >= data.Length; num2--)
		{
			num <<= 8;
			num += (int)((hex >> 8 * (len - num2 - 1)) & 0xFF);
		}
		return ToString(hex2, len2) + $".{num:X1}";
	}

	public virtual bool Check(long hex, int len)
	{
		if (len < data.Length)
		{
			return false;
		}
		for (int i = 0; i < data.Length; i++)
		{
			if (data[i] != ((hex >> 8 * (len - i - 1)) & 0xFF))
			{
				return false;
			}
		}
		return true;
	}

	public virtual bool CheckDot(long hex, int len, long hex2, int len2)
	{
		return false;
	}

	public virtual bool CheckCom(long hex, int len, long hex2, int len2)
	{
		return false;
	}

	public virtual bool CheckV(long hex, int len, long hex2, int len2)
	{
		return ((hex >> 8 * (len - 1)) & 0xFF) == 244 && Check(hex2, len2);
	}

	public virtual bool CheckZ(long hex, int len, long hex2, int len2)
	{
		return ((hex >> 8 * (len - 1)) & 0xFF) == 240 && Check(hex2, len2);
	}
}
