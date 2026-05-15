using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SamSoarII.Core.Models;
using SamSoarII.Shell.Windows;
using SamSoarII.Utility;

namespace SamSoarII.Core.Helpers;

public class CSVFileHelper
{
	private static byte DATATYPE_FLOAT = 6;

	private static byte DATATYPE_UDWORD = 5;

	private static string DQuotation = $"\"\"";

	private static string Quotation = $"\"";

	private static string SBCDQuotation = ToSBC(DQuotation);

	private static string SBCQuotation = ToSBC(Quotation);

	private static string separator = ",";

	public static void ImportExcute(string filename, IValueManager valuemanager)
	{
		StreamReader streamReader = new StreamReader(File.OpenRead(filename), FileHelper.GetFileEncoding(filename), detectEncodingFromByteOrderMarks: true);
		while (!streamReader.EndOfStream)
		{
			string text = streamReader.ReadLine();
			if (text.Length > 0)
			{
				string name = null;
				string comment = null;
				string alias = null;
				InitValueInfo(text, out name, out comment, out alias);
				IValueInfo valueInfo = valuemanager[name];
				if (valueInfo != null)
				{
					valueInfo.Comment = comment;
					valueInfo.Alias = alias;
				}
			}
		}
		streamReader.Close();
	}

	public static void ImportLocal(string filename, IValueManager valuemanager, IProjectModel project)
	{
		StreamReader streamReader = new StreamReader(File.OpenRead(filename), FileHelper.GetFileEncoding(filename), detectEncodingFromByteOrderMarks: true);
		while (!streamReader.EndOfStream)
		{
			string line = streamReader.ReadLine();
			string name = null;
			int keyid = 0;
			string comment = null;
			InitLocalInfo(line, out name, out keyid, out comment);
			valuemanager.SetTempLocalEnvironment(project.Diagrams.FirstOrDefault((ILadderDiagramModel d) => d.KeyID == keyid), _iscreatelocalinfo: true);
			if (valuemanager.LocalDiagram != null)
			{
				IValueInfo valueInfo = valuemanager[name];
				if (valueInfo != null)
				{
					valueInfo.Comment = comment;
				}
			}
		}
		streamReader.Close();
	}

	public static void ImportInit(string filename, IElementInitWindow wndInit)
	{
		StreamReader streamReader = new StreamReader(File.OpenRead(filename), FileHelper.GetFileEncoding(filename), detectEncodingFromByteOrderMarks: true);
		string text = null;
		while (!streamReader.EndOfStream)
		{
			text = streamReader.ReadLine();
			string eletype = null;
			uint offset = 0u;
			int datatype = 0;
			string showvalue = null;
			InitElement(text, out eletype, out offset, out datatype, out showvalue);
			IElementInitializeModel element = wndInit.GetElement(eletype, offset);
			if (element != null)
			{
				element.DataType = datatype;
				element.ShowValue = showvalue;
			}
			else
			{
				element = wndInit.GenerateElementModel(datatype == 0, eletype, offset, datatype);
				element.ShowValue = showvalue;
				wndInit.AddElement(element);
			}
		}
		streamReader.Close();
	}

	public static IList<ICSVValueLine> ImportValueLine(string filename)
	{
		StreamReader streamReader = new StreamReader(File.OpenRead(filename), FileHelper.GetFileEncoding(filename), detectEncodingFromByteOrderMarks: true);
		List<ICSVValueLine> list = new List<ICSVValueLine>();
		List<uint> list2 = new List<uint>();
		List<string> list3 = new List<string>();
		string text = null;
		try
		{
			while (!streamReader.EndOfStream)
			{
				text = streamReader.ReadLine();
				string[] array = text.Split(',');
				if (array.Length < 2 || !ValueConverter.AssertUInt(array[0]))
				{
					continue;
				}
				uint num = uint.Parse(array[0]);
				if (num == uint.MaxValue)
				{
					int lparam = (int)list2.LastOrDefault();
					int wparam = (int)uint.Parse(list3.LastOrDefault());
					ICSVValueInfo valueInfo = GetValueInfo(lparam, wparam);
					list2.RemoveAt(list2.Count() - 1);
					list3.RemoveAt(list3.Count() - 1);
					if (valueInfo.DataType == DATATYPE_FLOAT)
					{
						ICSVValueLine item = new CSVValueLineF(valueInfo, list2.ToArray(), list3.Select((string vs) => float.Parse(vs)).ToArray());
						list.Add(item);
					}
					else if (valueInfo.DataType == DATATYPE_UDWORD)
					{
						ICSVValueLine item2 = new CSVValueLineI(valueInfo, list2.ToArray(), list3.Select((string vs) => uint.Parse(vs)).Cast<int>().ToArray());
						list.Add(item2);
					}
					else
					{
						ICSVValueLine item3 = new CSVValueLineI(valueInfo, list2.ToArray(), list3.Select((string vs) => int.Parse(vs)).ToArray());
						list.Add(item3);
					}
					list2.Clear();
					list3.Clear();
				}
				else
				{
					list2.Add(num);
					list3.Add(array[1]);
				}
			}
			return list;
		}
		catch (Exception ex)
		{
			throw ex;
		}
		finally
		{
			streamReader?.Close();
		}
	}

	public static ICSVValueInfo GetValueInfo(int lparam, int wparam)
	{
		byte baseaddress = (byte)((lparam >> 16) & 0xFF);
		ushort offset = (ushort)(lparam & 0xFFFF);
		byte datatype = (byte)((lparam >> 24) & 0xFF);
		byte intraaddress = (byte)((wparam >> 8) & 0xFF);
		byte intraoffset = (byte)(wparam & 0xFF);
		byte flag = (byte)((wparam >> 16) & 0xFF);
		return new CSVValueInfo(baseaddress, offset, intraaddress, intraoffset, flag, datatype);
	}

	public static void ExportExcute(string filename, IEnumerable<IValueInfo> elementCollection)
	{
		StreamWriter streamWriter = new StreamWriter(File.Open(filename, FileMode.Create, FileAccess.Write), Encoding.Default);
		foreach (IValueInfo item in elementCollection)
		{
			string text = item.Comment;
			string text2 = item.Alias;
			if (text.Contains(Quotation) || text.Contains(separator))
			{
				text = text.Replace(Quotation, DQuotation).Insert(0, Quotation);
				text = text.Insert(text.Length, Quotation);
			}
			if (text2.Contains(Quotation) || text2.Contains(separator))
			{
				text2 = text2.Replace(Quotation, DQuotation).Insert(0, Quotation);
				text2 = text2.Insert(text2.Length, Quotation);
			}
			streamWriter.WriteLine(string.Format("{0}{3}{1}{3}{2}", item.Name, text, text2, separator));
		}
		streamWriter.Close();
	}

	public static void ExportLocal(string filename, IEnumerable<ILocalInfo> lcolle)
	{
		StreamWriter streamWriter = new StreamWriter(File.Open(filename, FileMode.Create, FileAccess.Write), Encoding.Default);
		foreach (ILocalInfo item in lcolle)
		{
			int keyID = item.Diagram.KeyID;
			string text = item.Comment;
			if (text.Contains(Quotation) || text.Contains(separator))
			{
				text = text.Replace(Quotation, DQuotation).Insert(0, Quotation);
				text = text.Insert(text.Length, Quotation);
			}
			streamWriter.WriteLine(string.Format("{0}{3}{1}{3}{2}", item.Name, keyID, text, separator));
		}
		streamWriter.Close();
	}

	public static void ExportInit(string filename, IEnumerable<IElementInitializeModel> elements)
	{
		StreamWriter streamWriter = new StreamWriter(File.Open(filename, FileMode.Create, FileAccess.Write), Encoding.Default);
		foreach (IElementInitializeModel element in elements)
		{
			streamWriter.WriteLine(string.Format("{1}{0}{2}{0}{3}", separator, element.ShowName, element.DataType, element.ShowValue));
		}
		streamWriter.Close();
	}

	public static void ExportMonitorTable(string filename, IEnumerable<IMonitorTable> tables)
	{
		StreamWriter streamWriter = new StreamWriter(File.Open(filename, FileMode.Create, FileAccess.Write), Encoding.Default);
		foreach (IMonitorTable table in tables)
		{
			streamWriter.WriteLine($"##TABLE##,{table.Name}");
			foreach (IMonitorElement child in table.Children)
			{
				streamWriter.WriteLine(string.Format("{1}{0}{2}{0}{3}", separator, child.ShowName, child.DataType, child.CurrentValue));
			}
		}
		streamWriter.Close();
	}

	public static void ExportValueLine(string filename, IList<ICSVValueLine> valuelines)
	{
		StreamWriter streamWriter = new StreamWriter(File.Open(filename, FileMode.Create, FileAccess.Write), Encoding.Default);
		try
		{
			foreach (ICSVValueLine valueline in valuelines)
			{
				ICSVValueInfo valueInfo = valueline.ValueInfo;
				int num = (valueInfo.BaseAddress << 16) | valueInfo.Offset | (valueInfo.DataType << 24);
				int num2 = (valueInfo.IntraAddress << 8) | valueInfo.IntraOffset | (valueInfo.Flag << 16);
				if (valueline is ICSVValueLineI)
				{
					ICSVValueLineI iCSVValueLineI = (ICSVValueLineI)valueline;
					for (int i = 0; i < Math.Min(iCSVValueLineI.Times.Count, iCSVValueLineI.Values.Count); i++)
					{
						streamWriter.WriteLine($"{iCSVValueLineI.Times[i]},{((iCSVValueLineI.ValueInfo.DataType == DATATYPE_UDWORD) ? ((uint)iCSVValueLineI.Values[i]).ToString() : iCSVValueLineI.Values[i].ToString())}");
					}
				}
				else if (valueline is ICSVValueLineF)
				{
					ICSVValueLineF iCSVValueLineF = (ICSVValueLineF)valueline;
					for (int j = 0; j < Math.Min(iCSVValueLineF.Times.Count, iCSVValueLineF.Values.Count); j++)
					{
						streamWriter.WriteLine($"{iCSVValueLineF.Times[j]},{iCSVValueLineF.Values[j].ToString()}");
					}
				}
				streamWriter.WriteLine($"{num},{num2}");
				streamWriter.WriteLine(string.Format("{0},{0}", uint.MaxValue));
			}
		}
		catch (Exception ex)
		{
			throw ex;
		}
		finally
		{
			streamWriter?.Close();
			streamWriter = null;
		}
	}

	protected static void ExportValueInfo(StreamWriter stream, ICSVValueInfo value)
	{
		int num = (value.BaseAddress << 16) | value.Offset | (value.DataType << 24);
		int num2 = (value.IntraAddress << 8) | value.IntraOffset | (value.Flag << 16);
		stream.WriteLine($"{num}\t{num2}");
		stream.WriteLine(string.Format("{0}\t{1}", uint.MaxValue));
	}

	public static void ExportMonitorElements(string filename, IList<IMonitorElement> elements)
	{
		using StreamWriter streamWriter = new StreamWriter(File.OpenWrite(filename));
		foreach (IMonitorElement element in elements)
		{
			streamWriter.WriteLine(string.Format("{0}{3}{1}{3}{2}", element.ShowName, element.CurrentValue, string.Empty, separator));
		}
	}

	public static string ToSBC(string input)
	{
		char[] array = input.ToCharArray();
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] == ' ')
			{
				array[i] = '\u3000';
			}
			else if (array[i] < '\u007f' && array[i] > ' ')
			{
				array[i] = (char)(array[i] + 65248);
			}
		}
		return new string(array);
	}

	private static void InitValueInfo(string line, out string name, out string comment, out string alias)
	{
		int num = -1;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		name = string.Empty;
		comment = string.Empty;
		alias = string.Empty;
		while (num2 != line.Length + 1 && (num2 != line.Length || line.LastOrDefault() != ','))
		{
			switch ((num2 < line.Length) ? line[num2] : ',')
			{
			case ',':
				if (num3 % 2 == 0)
				{
					switch (num4)
					{
					case 0:
						name = line.Substring(num + 1, Math.Max(num2 - num - 1, 0));
						break;
					case 1:
						comment = line.Substring(num + 1, Math.Max(num2 - num - 1, 0));
						alias = ((num2 + 1 < line.Length) ? line.Substring(num2 + 1) : string.Empty);
						break;
					}
					num = num2;
					num3 = 0;
					num4++;
				}
				break;
			case '"':
				num3++;
				break;
			}
			num2++;
			if (num4 == 2)
			{
				break;
			}
		}
		if (name.Contains(Quotation))
		{
			name = name.Remove(0, 1);
			name = name.Remove(name.Length - 1, 1);
		}
		if (comment.Contains(Quotation))
		{
			comment = comment.Remove(0, 1);
			comment = comment.Remove(comment.Length - 1, 1);
		}
		if (alias.Contains(Quotation))
		{
			alias = alias.Remove(0, 1);
			alias = alias.Remove(alias.Length - 1, 1);
		}
		name = name.Replace(DQuotation, Quotation);
		comment = comment.Replace(DQuotation, Quotation);
		alias = alias.Replace(DQuotation, Quotation);
	}

	private static void InitLocalInfo(string line, out string name, out int keyid, out string comment)
	{
		int num = -1;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		name = string.Empty;
		keyid = 0;
		comment = string.Empty;
		while (num2 != line.Length + 1 && (num2 != line.Length || line.LastOrDefault() != ','))
		{
			switch ((num2 < line.Length) ? line[num2] : ',')
			{
			case ',':
				if (num3 % 2 == 0)
				{
					switch (num4)
					{
					case 0:
						name = line.Substring(num + 1, Math.Max(num2 - num - 1, 0));
						break;
					case 1:
						keyid = int.Parse(line.Substring(num + 1, Math.Max(num2 - num - 1, 0)));
						comment = ((num2 + 1 < line.Length) ? line.Substring(num2 + 1) : string.Empty);
						break;
					}
					num = num2;
					num3 = 0;
					num4++;
				}
				break;
			case '"':
				num3++;
				break;
			}
			num2++;
			if (num4 == 2)
			{
				break;
			}
		}
		if (name.Contains(Quotation))
		{
			name = name.Remove(0, 1);
			name = name.Remove(name.Length - 1, 1);
		}
		if (comment.Contains(Quotation))
		{
			comment = comment.Remove(0, 1);
			comment = comment.Remove(comment.Length - 1, 1);
		}
		name = name.Replace(DQuotation, Quotation);
		comment = comment.Replace(DQuotation, Quotation);
	}

	private static void InitElement(string line, out string eletype, out uint offset, out int datatype, out string showvalue)
	{
		int num = -1;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		string empty = string.Empty;
		eletype = string.Empty;
		offset = 0u;
		datatype = 0;
		showvalue = string.Empty;
		while (num2 != line.Length + 1 && (num2 != line.Length || line.LastOrDefault() != ','))
		{
			switch ((num2 < line.Length) ? line[num2] : ',')
			{
			case ',':
				if (num3 % 2 != 0)
				{
					break;
				}
				switch (num4)
				{
				case 0:
				{
					empty = line.Substring(num + 1, Math.Max(num2 - num - 1, 0));
					if (empty.Contains(Quotation))
					{
						empty = empty.Remove(0, 1);
						empty = empty.Remove(empty.Length - 1, 1);
					}
					empty = empty.Replace(DQuotation, Quotation);
					for (int i = 0; i < empty.Length - 1; i++)
					{
						if (!char.IsDigit(empty[i]) && char.IsDigit(empty[i + 1]))
						{
							eletype = empty.Substring(0, i + 1);
							offset = uint.Parse(empty.Substring(i + 1));
						}
					}
					break;
				}
				case 1:
					datatype = int.Parse(line.Substring(num + 1, Math.Max(num2 - num - 1, 0)));
					showvalue = ((num2 + 1 < line.Length) ? line.Substring(num2 + 1) : string.Empty);
					break;
				}
				num = num2;
				num3 = 0;
				num4++;
				break;
			case '"':
				num3++;
				break;
			}
			num2++;
			if (num4 == 2)
			{
				break;
			}
		}
		if (showvalue.Contains(Quotation))
		{
			showvalue = showvalue.Remove(0, 1);
			showvalue = showvalue.Remove(showvalue.Length - 1, 1);
		}
		showvalue = showvalue.Replace(DQuotation, Quotation);
	}
}
