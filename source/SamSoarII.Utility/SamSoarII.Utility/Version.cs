using System;

namespace SamSoarII.Utility;

public struct Version(int ver_Main, int ver_Sub, int ver_Modify) : IComparable<Version>
{
	private int ver_Main = ver_Main;

	private int ver_Sub = ver_Sub;

	private int ver_Modify = ver_Modify;

	private int ver_temporary = 0;

	public int Ver_Main
	{
		get
		{
			return ver_Main;
		}
		set
		{
			ver_Main = value;
		}
	}

	public int Ver_Sub
	{
		get
		{
			return ver_Sub;
		}
		set
		{
			ver_Sub = value;
		}
	}

	public int Ver_Modify
	{
		get
		{
			return ver_Modify;
		}
		set
		{
			ver_Modify = value;
		}
	}

	public int Ver_Temporary
	{
		get
		{
			return ver_temporary;
		}
		set
		{
			ver_temporary = value;
		}
	}

	public int CompareTo(Version other)
	{
		if (Ver_Main > other.Ver_Main)
		{
			return 1;
		}
		if (Ver_Main < other.Ver_Main)
		{
			return -1;
		}
		if (Ver_Sub > other.Ver_Sub)
		{
			return 1;
		}
		if (Ver_Sub < other.Ver_Sub)
		{
			return -1;
		}
		if (Ver_Modify > other.Ver_Modify)
		{
			return 1;
		}
		if (Ver_Modify < other.Ver_Modify)
		{
			return -1;
		}
		return 0;
	}

	public int CompareTo(int main, int sub, int modify)
	{
		if (Ver_Main > main)
		{
			return 1;
		}
		if (Ver_Main < main)
		{
			return -1;
		}
		if (Ver_Sub > sub)
		{
			return 1;
		}
		if (Ver_Sub < sub)
		{
			return -1;
		}
		if (Ver_Modify > modify)
		{
			return 1;
		}
		if (Ver_Modify < modify)
		{
			return -1;
		}
		return 0;
	}

	public void Parse(string version)
	{
		string[] array = version.Split(new string[1] { "." }, StringSplitOptions.RemoveEmptyEntries);
		try
		{
			ver_Main = int.Parse(array[0]);
		}
		catch (Exception)
		{
			ver_Main = 0;
		}
		try
		{
			ver_Sub = int.Parse(array[1]);
		}
		catch (Exception)
		{
			ver_Sub = 0;
		}
		try
		{
			int num = array[2].IndexOf('-');
			if (num >= 0)
			{
				ver_Modify = int.Parse(array[2].Substring(0, num));
				ver_temporary = int.Parse(array[2].Substring(num + 1));
			}
			else
			{
				ver_Modify = int.Parse(array[2]);
				ver_temporary = 0;
			}
		}
		catch (Exception)
		{
			ver_Modify = 0;
			ver_temporary = 0;
		}
	}

	public static bool operator ==(Version left, Version right)
	{
		return left.CompareTo(right) == 0;
	}

	public static bool operator !=(Version left, Version right)
	{
		return !(left == right);
	}

	public static bool operator >(Version left, Version right)
	{
		return left.CompareTo(right) > 0;
	}

	public static bool operator <(Version left, Version right)
	{
		return left.CompareTo(right) < 0;
	}

	public static bool operator >=(Version left, Version right)
	{
		return !(left < right);
	}

	public static bool operator <=(Version left, Version right)
	{
		return !(left > right);
	}

	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return false;
		}
		if (obj is Version version)
		{
			return version == this;
		}
		return false;
	}

	public override int GetHashCode()
	{
		return ToString().GetHashCode();
	}

	public override string ToString()
	{
		if (ver_temporary > 0)
		{
			return $"{Ver_Main}.{Ver_Sub}.{Ver_Modify}-{ver_temporary}";
		}
		return $"{Ver_Main}.{Ver_Sub}.{Ver_Modify}";
	}

	public string ToString_OnlyMain()
	{
		return $"{Ver_Main:x}";
	}
}
