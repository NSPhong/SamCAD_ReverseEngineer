using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace SamSoarII.Utility.Files.FP;

public class FPDevice
{
	private enum ListStatus
	{
		Root,
		Dev_Begin,
		Dev_End,
		Dev_In
	}

	public static readonly Dictionary<uint, FPDevice> ItemOfBins;

	private FPDeviceSeries parent;

	private string name;

	private uint binaryfeature;

	public FPDeviceSeries Parent => parent;

	public string Name => name;

	public uint BinaryFeature => binaryfeature;

	private static void AddRange(IList<FPDeviceSeries> devs, IList<string> args)
	{
		if (args.Count() < 3)
		{
			return;
		}
		FPFormat value = null;
		if (!FPFormat.ItemOfNames.TryGetValue(args[0], out value) || !(value is FPValueFormat))
		{
			return;
		}
		FPValueFormat format = (FPValueFormat)value;
		if (!int.TryParse(args[1], out var result) || !int.TryParse(args[2], out var result2))
		{
			return;
		}
		FPDeviceRange fPDeviceRange = new FPDeviceRange(format, result, result2);
		foreach (FPDeviceSeries dev in devs)
		{
			dev.Ranges.Add(fPDeviceRange.Format.MainCode & 0xFF00, fPDeviceRange);
		}
	}

	static FPDevice()
	{
		ItemOfBins = new Dictionary<uint, FPDevice>();
		string path = $"{FileHelper.AppRootPath}\\Converter\\fpwin\\device_list.txt";
		string text = null;
		string text2 = null;
		int num = 0;
		int num2 = 0;
		bool flag = false;
		string text3 = null;
		string text4 = null;
		string text5 = null;
		string text6 = null;
		string text7 = null;
		using (StreamReader streamReader = new StreamReader(path))
		{
			text = streamReader.ReadToEnd();
		}
		for (int i = 0; i < text.Length; i++)
		{
			switch (text[i])
			{
			case '\n':
			case '\r':
				num2 = i + 1;
				break;
			case '\t':
			case ' ':
				if (num == 0)
				{
					num2 = i + 1;
				}
				break;
			case '"':
				if (!flag)
				{
					flag = true;
					num2 = i + 1;
					break;
				}
				flag = false;
				if (i > num2)
				{
					text3 = text.Substring(num2, i - num2);
				}
				break;
			case ',':
			case ';':
			{
				if (flag)
				{
					break;
				}
				text2 = text3 ?? ((i > num2) ? text.Substring(num2, i - num2) : string.Empty);
				num2 = i + 1;
				text3 = null;
				switch (num++)
				{
				case 0:
					text4 = text2;
					break;
				case 1:
					text5 = text2;
					break;
				case 2:
					text7 = text2;
					break;
				}
				if (text[i] != ';')
				{
					break;
				}
				uint result = 0u;
				if (text6 != null)
				{
					text6 = $"{FileHelper.AppRootPath}\\Converter\\fpwin\\{text6}";
				}
				if (text7 != null)
				{
					uint.TryParse(text7, NumberStyles.HexNumber, null, out result);
				}
				if (text4 != null && text5 != null && text6 != null && File.Exists(text6))
				{
					using Stream stream = File.OpenRead(text6);
					byte[] array = new byte[4];
					stream.Position = 8L;
					stream.Read(array, 0, 4);
					result = (uint)((array[0] << 24) | (array[1] << 16) | (array[2] << 8) | array[3]);
				}
				if (text4 != null && text5 != null && result != 0)
				{
					FPDeviceSeries value = null;
					if (!FPDeviceSeries.ItemOfNames.TryGetValue(text4, out value))
					{
						value = new FPDeviceSeries(text4);
						FPDeviceSeries.ItemOfNames.Add(text4, value);
					}
					FPDevice value2 = new FPDevice(value, text5, result);
					if (!ItemOfBins.ContainsKey(result))
					{
						ItemOfBins.Add(result, value2);
					}
					string text8 = (text4.Contains(',') ? $"\"{text4}\"" : text4);
					string text9 = (text5.Contains(',') ? $"\"{text5}\"" : text5);
				}
				num = 0;
				break;
			}
			}
		}
		string path2 = $"{FileHelper.AppRootPath}\\Converter\\fpwin\\device_register.txt";
		string text10 = null;
		int num3 = 0;
		ListStatus listStatus = ListStatus.Root;
		List<string> list = new List<string>();
		List<FPDeviceSeries> list2 = new List<FPDeviceSeries>();
		using (StreamReader streamReader2 = new StreamReader(path2))
		{
			text10 = streamReader2.ReadToEnd();
		}
		for (int j = 0; j < text10.Length; j++)
		{
			switch (text10[j])
			{
			case '\n':
			case '\r':
				num3 = j + 1;
				break;
			case '\t':
			case ' ':
			{
				ListStatus listStatus10 = listStatus;
				ListStatus listStatus11 = listStatus10;
				if (listStatus11 == ListStatus.Dev_In && list.Count() == 0)
				{
					num3 = j + 1;
				}
				break;
			}
			case '(':
				if (listStatus == ListStatus.Root)
				{
					num3 = j + 1;
					listStatus = ListStatus.Dev_Begin;
					list2.Clear();
				}
				break;
			case ')':
			{
				ListStatus listStatus6 = listStatus;
				ListStatus listStatus7 = listStatus6;
				if (listStatus7 != ListStatus.Dev_Begin)
				{
					break;
				}
				if (j > num3)
				{
					list.Add(text10.Substring(num3, j - num3));
				}
				num3 = j + 1;
				listStatus = ListStatus.Dev_End;
				list2.Clear();
				foreach (string item in list)
				{
					FPDeviceSeries value3 = null;
					if (FPDeviceSeries.ItemOfNames.TryGetValue(item, out value3))
					{
						list2.Add(value3);
					}
				}
				break;
			}
			case '{':
			{
				ListStatus listStatus12 = listStatus;
				ListStatus listStatus13 = listStatus12;
				if (listStatus13 == ListStatus.Dev_End)
				{
					num3 = j + 1;
					listStatus = ListStatus.Dev_In;
					list.Clear();
				}
				break;
			}
			case '}':
			{
				ListStatus listStatus4 = listStatus;
				ListStatus listStatus5 = listStatus4;
				if (listStatus5 == ListStatus.Dev_In)
				{
					if (j > num3)
					{
						list.Add(text10.Substring(num3, j - num3));
					}
					num3 = j + 1;
					listStatus = ListStatus.Root;
					AddRange(list2, list);
					list.Clear();
					list2.Clear();
				}
				break;
			}
			case ',':
			{
				ListStatus listStatus8 = listStatus;
				ListStatus listStatus9 = listStatus8;
				if (listStatus9 == ListStatus.Dev_Begin || listStatus9 == ListStatus.Dev_In)
				{
					if (j > num3)
					{
						list.Add(text10.Substring(num3, j - num3));
					}
					num3 = j + 1;
					AddRange(list2, list);
					list.Clear();
				}
				break;
			}
			case ';':
			{
				ListStatus listStatus2 = listStatus;
				ListStatus listStatus3 = listStatus2;
				if (listStatus3 == ListStatus.Dev_In && j > num3)
				{
					list.Add(text10.Substring(num3, j - num3));
				}
				break;
			}
			}
		}
	}

	public FPDevice(FPDeviceSeries _parent, string _name, uint _bin)
	{
		parent = _parent;
		name = _name;
		binaryfeature = _bin;
	}
}
