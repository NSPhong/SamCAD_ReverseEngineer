using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SamSoarII.Utility;

public class ValueConverter
{
	private static UTF8Encoding encoding = new UTF8Encoding();

	private static readonly char[] NumChars = new char[16]
	{
		'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
		'A', 'B', 'C', 'D', 'E', 'F'
	};

	public static ushort ToUINT16(ushort BCDValue)
	{
		ushort num = 0;
		num += (ushort)(BCDValue & 0xF);
		num += (ushort)(((BCDValue & 0xF0) >> 4) * 10);
		num += (ushort)(((BCDValue & 0xF00) >> 8) * 100);
		return (ushort)(num + (ushort)(((BCDValue & 0xF000) >> 12) * 1000));
	}

	public static uint ToUINT32(uint BCDValue)
	{
		ushort num = 0;
		num += (ushort)(BCDValue & 0xF);
		num += (ushort)(((BCDValue & 0xF0) >> 4) * 10);
		num += (ushort)(((BCDValue & 0xF00) >> 8) * 100);
		num += (ushort)(((BCDValue & 0xF000) >> 12) * 1000);
		num += (ushort)(((BCDValue & 0xF0000) >> 16) * 10000);
		num += (ushort)(((BCDValue & 0xF00000) >> 20) * 100000);
		num += (ushort)(((BCDValue & 0xF000000) >> 24) * 1000000);
		return (ushort)(num + (ushort)(((BCDValue & 0xF0000000u) >> 28) * 10000000));
	}

	public static ushort ToBCD(ushort value)
	{
		ushort num = 0;
		num += (ushort)(value / 1000 << 12);
		value %= 1000;
		num += (ushort)(value / 100 << 8);
		value %= 100;
		num += (ushort)(value / 10 << 4);
		value %= 10;
		return (ushort)(num + value);
	}

	public static uint ToDBCD(uint value)
	{
		uint num = 0u;
		num += value / 10000000 << 28;
		value %= 10000000;
		num += value / 1000000 << 24;
		value %= 1000000;
		num += value / 100000 << 20;
		value %= 100000;
		num += value / 10000 << 16;
		value %= 10000;
		num += value / 1000 << 12;
		value %= 1000;
		num += value / 100 << 8;
		value %= 100;
		num += value / 10 << 4;
		value %= 10;
		return num + value;
	}

	public static byte[] GetBytes(ushort value, bool isLowAhead = false)
	{
		byte b = (byte)(value & 0xFF);
		byte b2 = (byte)((value & 0xFF00) >> 8);
		if (!isLowAhead)
		{
			return new byte[2] { b2, b };
		}
		return new byte[2] { b, b2 };
	}

	public static int GetValueByBytes(params byte[] value)
	{
		int num = 0;
		for (int i = 0; i < value.Length; i++)
		{
			num += value[i] << 8 * i;
		}
		return num;
	}

	public static byte[] GetBytes(uint value, bool isLowHead = false)
	{
		byte b = (byte)(value & 0xFF);
		byte b2 = (byte)((value & 0xFF00) >> 8);
		byte b3 = (byte)((value & 0xFF0000) >> 16);
		byte b4 = (byte)((value & 0xFF000000u) >> 24);
		if (!isLowHead)
		{
			return new byte[4] { b2, b, b4, b3 };
		}
		return new byte[4] { b, b2, b3, b4 };
	}

	public static byte[] GetBytes(string value, bool isAscii = false)
	{
		if (!isAscii)
		{
			return encoding.GetBytes(value);
		}
		byte[] array = new byte[value.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = (byte)value[i];
		}
		return array;
	}

	public static int GetLength(string value)
	{
		return encoding.GetByteCount(value);
	}

	public static string ParseToString(byte[] data, bool isAscii = false)
	{
		if (!isAscii)
		{
			return new string(encoding.GetChars(data));
		}
		char[] array = new char[data.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = (char)data[i];
		}
		return new string(array);
	}

	public static uint GetValue(params byte[] data)
	{
		if (data.Length == 2)
		{
			return (uint)((data[0] << 8) + data[1]);
		}
		return (uint)((data[0] << 8) + data[1] + (data[2] << 24) + (data[3] << 16));
	}

	public static uint ParseShowValue(string showValue, WordType type)
	{
		return type switch
		{
			WordType.WORD => (uint)short.Parse(showValue), 
			WordType.UWORD => ushort.Parse(showValue), 
			WordType.DWORD => (uint)int.Parse(showValue), 
			WordType.UDWORD => uint.Parse(showValue), 
			WordType.BCD => ToUINT16(ushort.Parse(showValue)), 
			WordType.FLOAT => FloatToUInt(float.Parse(showValue)), 
			WordType.HEX => ushort.Parse(showValue, NumberStyles.HexNumber), 
			WordType.DHEX => uint.Parse(showValue, NumberStyles.HexNumber), 
			_ => throw new FormatException(), 
		};
	}

	public unsafe static long DoubleToInt64(double value)
	{
		return *(long*)(&value);
	}

	public unsafe static double Int64ToDouble(long value)
	{
		return *(double*)(&value);
	}

	public unsafe static uint FloatToUInt(float value)
	{
		return *(uint*)(&value);
	}

	public unsafe static ulong DoubleToULong(double value)
	{
		return *(ulong*)(&value);
	}

	public unsafe static float UIntToFloat(uint value)
	{
		return *(float*)(&value);
	}

	public static int IntToDex(int value)
	{
		int num = 0;
		int num2 = 1;
		while (value > 0)
		{
			num += (value & 7) * num2;
			num2 *= 10;
			value >>= 3;
		}
		return num;
	}

	public static int DexToInt(int value)
	{
		int num = 0;
		int num2 = 1;
		while (value > 0)
		{
			num = ((value % 10 < 8) ? (num + value % 10 * num2) : (num + 7 * num2));
			num2 <<= 3;
			value /= 10;
		}
		return num;
	}

	public unsafe static string ChangeShowValue(WordType sourceType, WordType desType, uint value)
	{
		switch (desType)
		{
		case WordType.WORD:
			return ((short)value).ToString();
		case WordType.UWORD:
			return ((ushort)value).ToString();
		case WordType.DWORD:
		{
			int num = (int)value;
			return num.ToString();
		}
		case WordType.UDWORD:
			return value.ToString();
		case WordType.BCD:
			if (!AssertValue(sourceType, value))
			{
				throw new Exception();
			}
			return ToBCD((ushort)value).ToString();
		case WordType.FLOAT:
			return ((float*)(&value))->ToString();
		case WordType.HEX:
			return $"0x{value:x4}";
		case WordType.DHEX:
			return $"0x{value:x8}";
		default:
			throw new Exception();
		}
	}

	private static bool AssertValue(WordType type, uint value)
	{
		switch (type)
		{
		case WordType.WORD:
		{
			short num2 = (short)value;
			if (num2 < 0 || num2 > 9999)
			{
				return false;
			}
			return true;
		}
		case WordType.UWORD:
		{
			ushort num = (ushort)value;
			if (num < 0 || num > 9999)
			{
				return false;
			}
			return true;
		}
		case WordType.DWORD:
			if ((int)value < 0 || (int)value > 9999)
			{
				return false;
			}
			return true;
		case WordType.UDWORD:
			if (value < 0 || value > 9999)
			{
				return false;
			}
			return true;
		case WordType.BCD:
			return true;
		case WordType.FLOAT:
			return false;
		default:
			return false;
		}
	}

	public static bool AssertByte(string text)
	{
		return text.Length > 0 && text.Length <= 3 && text.All((char c) => char.IsDigit(c)) && (text.Length != 3 || text[0] <= '2') && (text.Length != 3 || text[0] != '2' || text[1] <= '5') && (text.Length != 3 || text[0] != '2' || text[1] != '5' || text[2] <= '5');
	}

	public static bool AssertShort(string text)
	{
		if (text.Length <= 0 || text.Length > 6)
		{
			return false;
		}
		if (text[0] != '-' && !char.IsDigit(text[0]))
		{
			return false;
		}
		for (int i = 1; i < text.Length; i++)
		{
			if (!char.IsDigit(text[i]))
			{
				return false;
			}
		}
		int num = int.Parse(text);
		return num >= -32768 && num <= 32767;
	}

	public static bool AssertUShort(string text)
	{
		if (text.Length <= 0 || text.Length > 5)
		{
			return false;
		}
		if (!text.All((char c) => char.IsDigit(c)))
		{
			return false;
		}
		int num = int.Parse(text);
		return num >= 0 && num < 65536;
	}

	public static bool AssertInt(string text)
	{
		if (text.Length <= 0 || text.Length > 11)
		{
			return false;
		}
		if (text[0] != '-' && !char.IsDigit(text[0]))
		{
			return false;
		}
		for (int i = 1; i < text.Length; i++)
		{
			if (!char.IsDigit(text[i]))
			{
				return false;
			}
		}
		long num = long.Parse(text);
		return num >= int.MinValue && num <= int.MaxValue;
	}

	public static bool AssertUInt(string text)
	{
		if (text.Length <= 0 || text.Length > 10)
		{
			return false;
		}
		if (!text.All((char c) => char.IsDigit(c)))
		{
			return false;
		}
		long num = long.Parse(text);
		return num >= 0 && num <= uint.MaxValue;
	}

	public static bool AssertHex(string text)
	{
		if (text.Length <= 0 || text.Length > 4)
		{
			return false;
		}
		return text.All((char c) => char.IsNumber(c) || (char.ToUpper(c) >= 'A' && char.ToUpper(c) <= 'F'));
	}

	public static bool AssertDHex(string text)
	{
		if (text.Length <= 0 || text.Length > 8)
		{
			return false;
		}
		return text.All((char c) => char.IsNumber(c) || (char.ToUpper(c) >= 'A' && char.ToUpper(c) <= 'F'));
	}

	public static bool AssertFloat(string text)
	{
		if (text.Length <= 0 || text.Length > 20)
		{
			return false;
		}
		int i = ((text[0] == '-') ? 1 : 48);
		if (i >= text.Length)
		{
			return false;
		}
		for (; i < text.Length && char.IsDigit(text[i]); i++)
		{
		}
		if (i >= text.Length)
		{
			return true;
		}
		if (text[i] != '.')
		{
			return false;
		}
		for (i++; i < text.Length && char.IsDigit(text[i]); i++)
		{
		}
		if (i - 1 < text.Length && !char.IsDigit(text[i - 1]))
		{
			return false;
		}
		if (i >= text.Length)
		{
			return true;
		}
		if (text[i] != 'e' && text[i] != 'E')
		{
			return false;
		}
		if (i + 1 >= text.Length)
		{
			return false;
		}
		for (i = ((text[i + 1] == '-') ? (i + 2) : (i + 1)); i < text.Length && char.IsDigit(text[i]); i++)
		{
		}
		return i >= text.Length;
	}

	public static string SubString(string str, int start, int end)
	{
		if (str == null)
		{
			return "";
		}
		if (start < 0)
		{
			start = 0;
		}
		if (end >= str.Length)
		{
			end = str.Length - 1;
		}
		return (start > end) ? "" : str.Substring(start, end - start + 1);
	}

	public static string ToString(float f)
	{
		if (f == 0f)
		{
			return "0";
		}
		if ((double)System.Math.Abs(f) > 10000000.0 || (double)System.Math.Abs(f) < 1E-07)
		{
			return $"{f:f7}";
		}
		return $"{f:f7}".TrimEnd('0').TrimEnd('.');
	}

	public unsafe static string ToString(sbyte* p, int ml, Encoding e)
	{
		string text = new string(p, 0, ml, e);
		int num = text.IndexOf('\0');
		return (num >= 0) ? text.Substring(0, num) : text;
	}

	public static string NBase_ToString(int value, int nbase)
	{
		if (value == 0)
		{
			return "0";
		}
		bool flag = value < 0;
		StringBuilder stringBuilder = new StringBuilder();
		for (value = System.Math.Abs(value); value > 0; value /= nbase)
		{
			stringBuilder.Append(NumChars[value % nbase]);
		}
		if (flag)
		{
			stringBuilder.Append('-');
		}
		return string.Join(string.Empty, stringBuilder.ToString().Reverse());
	}

	public static int NBase_Parse(string text, int nbase)
	{
		int num = 0;
		bool flag = false;
		foreach (char c in text)
		{
			if (c == '-')
			{
				flag = true;
				continue;
			}
			num *= nbase;
			num = ((!char.IsDigit(c)) ? (num + (char.ToUpper(c) - 65 + 10)) : (num + (c - 48)));
		}
		if (flag)
		{
			num = -num;
		}
		return num;
	}

	public static bool NBase_Assert(string text, int nbase)
	{
		foreach (char c in text)
		{
			if (char.IsDigit(c))
			{
				if (c - 48 >= nbase)
				{
					return false;
				}
			}
			else if (char.ToUpper(c) - 65 + 10 >= nbase)
			{
				return false;
			}
		}
		return true;
	}

	public static short ToInt16(object obj)
	{
		if (obj == null)
		{
			return 0;
		}
		if (!(obj is short result))
		{
			if (obj is char)
			{
				return (short)(char)obj;
			}
			if (!(obj is short result2))
			{
				if (obj is ushort)
				{
					return (short)(ushort)obj;
				}
				if (obj is int)
				{
					return (short)(int)obj;
				}
				if (obj is uint)
				{
					return (short)(uint)obj;
				}
				if (obj is long)
				{
					return (short)(long)obj;
				}
				if (obj is ulong)
				{
					return (short)(ulong)obj;
				}
				if (obj is bool)
				{
					return (short)(((bool)obj) ? 1 : 0);
				}
				if (obj is float)
				{
					return (short)(float)obj;
				}
				if (obj is double)
				{
					return (short)(double)obj;
				}
				short result3 = 0;
				return (short)(short.TryParse(obj.ToString(), out result3) ? result3 : 0);
			}
			return result2;
		}
		return result;
	}

	public static ushort ToUInt16(object obj)
	{
		if (obj == null)
		{
			return 0;
		}
		if (!(obj is ushort result))
		{
			if (!(obj is ushort result2))
			{
				if (obj is short)
				{
					return (ushort)(short)obj;
				}
				if (!(obj is ushort result3))
				{
					if (obj is int)
					{
						return (ushort)(int)obj;
					}
					if (obj is uint)
					{
						return (ushort)(uint)obj;
					}
					if (obj is long)
					{
						return (ushort)(long)obj;
					}
					if (obj is ulong)
					{
						return (ushort)(ulong)obj;
					}
					if (obj is bool)
					{
						return (ushort)(((bool)obj) ? 1u : 0u);
					}
					if (obj is float)
					{
						return (ushort)(float)obj;
					}
					if (obj is double)
					{
						return (ushort)(double)obj;
					}
					ushort result4 = 0;
					return (ushort)(ushort.TryParse(obj.ToString(), out result4) ? result4 : 0);
				}
				return result3;
			}
			return result2;
		}
		return result;
	}

	public static int ToInt32(object obj)
	{
		if (obj == null)
		{
			return 0;
		}
		if (!(obj is int result))
		{
			if (!(obj is int result2))
			{
				if (!(obj is int result3))
				{
					if (!(obj is int result4))
					{
						if (!(obj is int result5))
						{
							if (!(obj is int result6))
							{
								if (obj is long)
								{
									return (int)(long)obj;
								}
								if (obj is ulong)
								{
									return (int)(ulong)obj;
								}
								if (obj is bool)
								{
									return ((bool)obj) ? 1 : 0;
								}
								if (obj is float)
								{
									return (int)(float)obj;
								}
								if (obj is double)
								{
									return (int)(double)obj;
								}
								int result7 = 0;
								return int.TryParse(obj.ToString(), out result7) ? result7 : 0;
							}
							return result6;
						}
						return result5;
					}
					return result4;
				}
				return result3;
			}
			return result2;
		}
		return result;
	}

	public static uint ToUInt32(object obj)
	{
		if (obj == null)
		{
			return 0u;
		}
		if (!(obj is uint result))
		{
			if (!(obj is uint result2))
			{
				if (!(obj is uint result3))
				{
					if (!(obj is uint result4))
					{
						if (!(obj is uint result5))
						{
							if (!(obj is uint result6))
							{
								if (obj is long)
								{
									return (uint)(long)obj;
								}
								if (obj is ulong)
								{
									return (uint)(ulong)obj;
								}
								if (obj is bool)
								{
									return ((bool)obj) ? 1u : 0u;
								}
								if (obj is float)
								{
									return (uint)(float)obj;
								}
								if (obj is double)
								{
									return (uint)(double)obj;
								}
								uint result7 = 0u;
								return uint.TryParse(obj.ToString(), out result7) ? result7 : 0u;
							}
							return result6;
						}
						return result5;
					}
					return result4;
				}
				return result3;
			}
			return result2;
		}
		return result;
	}

	public static float ToFloat(object obj)
	{
		if (obj == null)
		{
			return 0f;
		}
		if (!(obj is float result))
		{
			if (obj is double)
			{
				return (float)(double)obj;
			}
			if (obj is byte)
			{
				return (int)(byte)obj;
			}
			if (obj is char)
			{
				return (int)(char)obj;
			}
			if (obj is short)
			{
				return (short)obj;
			}
			if (obj is ushort)
			{
				return (int)(ushort)obj;
			}
			if (obj is int)
			{
				return (int)obj;
			}
			if (obj is uint)
			{
				return (uint)obj;
			}
			if (obj is long)
			{
				return (long)obj;
			}
			if (obj is ulong)
			{
				return (ulong)obj;
			}
			if (obj is bool)
			{
				return ((bool)obj) ? 1 : 0;
			}
			float result2 = 0f;
			return float.TryParse(obj.ToString(), out result2) ? result2 : 0f;
		}
		return result;
	}

	public static string ToSplitString(char sp, params object[] args)
	{
		bool flag = true;
		bool flag2 = false;
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < args.Length; i++)
		{
			string text = args[i]?.ToString() ?? string.Empty;
			flag2 = text.Contains(sp);
			if (!flag)
			{
				stringBuilder.Append(sp);
			}
			if (flag2)
			{
				stringBuilder.Append('"');
			}
			string text2 = text;
			foreach (char c in text2)
			{
				if (c == '"' || c == '\\')
				{
					stringBuilder.Append('\\');
				}
				stringBuilder.Append(c);
			}
			if (flag2)
			{
				stringBuilder.Append('"');
			}
			flag = false;
		}
		return stringBuilder.ToString();
	}

	public static string[] Split(string text, params char[] spcs)
	{
		if (text == null)
		{
			return new string[0];
		}
		int num = -1;
		bool flag = false;
		bool flag2 = false;
		List<string> list = new List<string>();
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < text.Length; i++)
		{
			if (text[i] == '\\')
			{
				flag = !flag;
				if (!flag)
				{
					stringBuilder.Append('\\');
				}
			}
			else if (text[i] == '"' && !flag)
			{
				stringBuilder.Append('"');
				if (!flag2)
				{
					flag2 = true;
					num = i;
					continue;
				}
				flag2 = false;
				if (num >= 0)
				{
					list.Add(stringBuilder.ToString());
				}
				stringBuilder.Clear();
				num = -1;
			}
			else if (flag2)
			{
				flag = false;
				stringBuilder.Append(text[i]);
			}
			else if (spcs.Contains(text[i]))
			{
				if (num >= 0)
				{
					list.Add(stringBuilder.ToString());
				}
				stringBuilder.Clear();
				num = -1;
				flag = false;
			}
			else if (num < 0)
			{
				num = i;
				flag = false;
				stringBuilder.Append(text[i]);
			}
			else
			{
				flag = false;
				stringBuilder.Append(text[i]);
			}
		}
		if (num >= 0)
		{
			list.Add(stringBuilder.ToString());
		}
		return list.ToArray();
	}

	public static int SplitIndex(string text, int index)
	{
		if (text == null)
		{
			return 0;
		}
		int num = -1;
		bool flag = false;
		bool flag2 = false;
		int num2 = 0;
		for (int i = 0; i < text.Length; i++)
		{
			if (i == index)
			{
				return num2;
			}
			if (text[i] == '\\')
			{
				flag = !flag;
			}
			else if (text[i] == '"' && !flag)
			{
				if (!flag2)
				{
					flag2 = true;
					num = i;
				}
				else
				{
					flag2 = false;
					num2++;
					num = -1;
				}
			}
			else if (flag2)
			{
				flag = false;
			}
			else if (text[i] == ' ' || text[i] == '\t' || text[i] == '\n')
			{
				if (num >= 0)
				{
					num2++;
				}
				num = -1;
				flag = false;
			}
			else if (num < 0)
			{
				num = i;
				flag = false;
			}
			else
			{
				flag = false;
			}
		}
		return num2;
	}

	public static string GetTrimming(string text, int maxlen)
	{
		StringBuilder arg = new StringBuilder();
		int num = 0;
		foreach (char c in text)
		{
			int num2 = num + ((c < 'Ā') ? 1 : 2);
			if (num2 > maxlen)
			{
				return $"{arg}~";
			}
		}
		return text;
	}
}
