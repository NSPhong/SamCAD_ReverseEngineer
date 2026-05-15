using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SamSoarII.Utility;

public class StringHelper
{
	public static Regex PasswordRegex = new Regex("^[a-zA-Z0-9][\\w@#$%^&*+=-]{3,11}", RegexOptions.Compiled);

	public static Regex FuncName = new Regex("^[_a-zA-Z][_a-zA-Z0-9]*", RegexOptions.Compiled);

	public static readonly int MaxCapacity = 32;

	public static int Compare(string str, int index)
	{
		int num = 0;
		string text = str.Substring(0, index + 1);
		foreach (char c in text)
		{
			num += c;
		}
		return num;
	}

	public static int Compare(string str1, string str2)
	{
		char[] array = str1.ToCharArray();
		char[] array2 = str2.ToCharArray();
		for (int i = 0; i < System.Math.Min(array.Length, array2.Length); i++)
		{
			if (array[i] != array2[i])
			{
				return array[i] - array2[i];
			}
		}
		return array.Length - array2.Length;
	}

	public static string Trunc(string value)
	{
		int num = value.Count();
		num += ChineseCharCount(value);
		if (num > MaxCapacity)
		{
			bool flag = IsUNCPath(value);
			int num2 = (flag ? (num - MaxCapacity + 5) : (num - MaxCapacity + 3));
			List<string> list = value.Split(new char[1] { Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries).ToList();
			string text = (flag ? ("\\\\" + list.First()) : list.First());
			string text2 = list.Last();
			if (list.Count == 2)
			{
				if (text2.Length > num2)
				{
					text2 = "..." + text2.Substring(num2);
				}
				else
				{
					text = text.Substring(0, 3) + "..." + text.Substring(3 + num2);
				}
				value = $"{text}{Path.DirectorySeparatorChar}{text2}";
			}
			else
			{
				if (text.Length + text2.Length + 5 > MaxCapacity)
				{
					num2 = text.Length + text2.Length + 5 - MaxCapacity + 3;
					if (text2.Length > num2)
					{
						text2 = "..." + text2.Substring(num2);
					}
					else
					{
						text = text.Substring(0, 3) + "..." + text.Substring(3 + num2);
					}
				}
				else
				{
					int num3 = list.Count - 2;
					while (text.Length + text2.Length + 5 + list.ElementAt(num3).Length + 1 <= MaxCapacity && num3 != 0)
					{
						text2 = list.ElementAt(num3) + Path.DirectorySeparatorChar + text2;
						num3--;
					}
				}
				value = string.Format("{0}{1}{3}{1}{2}", text, Path.DirectorySeparatorChar, text2, "...");
			}
		}
		return value;
	}

	private static bool IsUNCPath(string path)
	{
		return path.StartsWith("\\\\");
	}

	public static int ChineseCharCount(string value)
	{
		int num = 0;
		foreach (char c in value)
		{
			if (c >= '一' && c <= '龻')
			{
				num++;
			}
		}
		return num;
	}

	public static string Encrypt(string password)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(password.ToCharArray());
		for (int i = 0; i < bytes.Length; i++)
		{
			bytes[i] ^= 0xFF;
		}
		int num = 0;
		int num2 = bytes.Length - 1;
		while (num < num2)
		{
			byte b = bytes[num];
			bytes[num] = bytes[num2];
			bytes[num2] = b;
			num++;
			num2--;
		}
		char[] array = new char[bytes.Length];
		for (int j = 0; j < bytes.Length; j++)
		{
			array[j] = (char)bytes[j];
		}
		return new string(array);
	}

	public static string Decrypt(string ciphertext)
	{
		byte[] array = new byte[ciphertext.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = (byte)ciphertext[i];
		}
		int num = 0;
		int num2 = array.Length - 1;
		while (num < num2)
		{
			byte b = array[num];
			array[num] = array[num2];
			array[num2] = b;
			num++;
			num2--;
		}
		for (int j = 0; j < array.Length; j++)
		{
			array[j] ^= 0xFF;
		}
		char[] array2 = new char[array.Length];
		for (int k = 0; k < array.Length; k++)
		{
			array2[k] = (char)array[k];
		}
		return new string(array2);
	}

	public static string RemoveSystemSeparator(string path)
	{
		if (path.EndsWith(Path.DirectorySeparatorChar.ToString()))
		{
			return path.Substring(0, path.Length - 1);
		}
		return path;
	}

	public static int GetByteCount(char c)
	{
		return (c <= 'ÿ') ? 1 : 2;
	}

	public static int GetByteCount(string text)
	{
		return (text.Length != 0) ? text.Sum((char c) => GetByteCount(c)) : 0;
	}

	public static string Compress(string text, int maxlen)
	{
		int num = 0;
		for (int i = 0; i < text.Length; i++)
		{
			num += GetByteCount(text[i]);
			if (num > maxlen)
			{
				while (num > maxlen - 2)
				{
					num -= GetByteCount(text[i--]);
				}
				return text.Substring(0, i + 1) + new string('.', maxlen - num);
			}
		}
		return text;
	}

	public static void GetPosition(string text, int offset, ref int line, ref int column)
	{
		line = 0;
		column = 0;
		for (int i = 0; i < offset; i++)
		{
			if (text[i] == '\n')
			{
				line++;
				column = 0;
			}
			else
			{
				column++;
			}
		}
	}
}
