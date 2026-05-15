using System;
using System.Text.RegularExpressions;

namespace SamSoarII;

public class LocalizedDateTime : IComparable<LocalizedDateTime>
{
	private int year;

	private int month;

	private int day;

	private int hour;

	private int minute;

	private int second;

	private static Regex FormatRegex = new Regex("(\\d+)/(\\d+)/(\\d+),(\\d+):(\\d+):(\\d+)", RegexOptions.Compiled);

	public int Year => year;

	public int Month => month;

	public int Day => day;

	public int Hour => hour;

	public int Minute => minute;

	public int Second => second;

	public LocalizedDateTime(DateTime time)
	{
		year = time.Year;
		month = time.Month;
		day = time.Day;
		hour = time.Hour;
		minute = time.Minute;
		second = time.Second;
	}

	public LocalizedDateTime(int _year, int _month, int _day, int _hour, int _minute, int _second)
	{
		year = _year;
		month = _month;
		day = _day;
		hour = _hour;
		minute = _minute;
		second = _second;
	}

	public override string ToString()
	{
		return $"{year:d}/{month:d}/{day:d},{hour:d}:{minute:d2}:{second:d2}";
	}

	public string ToSimple()
	{
		return $"{year:d4}{month:d2}{day:d2}{hour:d2}{minute:d2}{second:d2}";
	}

	public int CompareTo(LocalizedDateTime other)
	{
		return -ToSimple().CompareTo(other.ToSimple());
	}

	public static LocalizedDateTime Parse(string txt)
	{
		Match match = FormatRegex.Match(txt);
		if (!match.Success)
		{
			return null;
		}
		try
		{
			int num = int.Parse(match.Groups[1].Value);
			int num2 = int.Parse(match.Groups[2].Value);
			int num3 = int.Parse(match.Groups[3].Value);
			int num4 = int.Parse(match.Groups[4].Value);
			int num5 = int.Parse(match.Groups[5].Value);
			int num6 = int.Parse(match.Groups[6].Value);
			return new LocalizedDateTime(num, num2, num3, num4, num5, num6);
		}
		catch (Exception)
		{
			return null;
		}
	}
}
