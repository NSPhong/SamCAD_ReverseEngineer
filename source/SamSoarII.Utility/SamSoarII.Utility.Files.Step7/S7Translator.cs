using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;

namespace SamSoarII.Utility.Files.Step7;

public class S7Translator : IDisposable
{
	public static readonly List<S7DeviceSeries> DeviceSeries;

	public static readonly List<S7Device> Devices;

	public static readonly Dictionary<int, S7UnitFormat> UnitFormats;

	public static readonly Dictionary<int, S7STLUnitFormat> STLFormats;

	public static readonly Dictionary<int, S7FBDUnitFormat> FBDFormats;

	private string filename;

	private IntPtr pdata;

	private IntPtr pin;

	private IntPtr pout;

	private IntPtr hdll;

	private S7DataStream s;

	public string FileName => filename;

	[DllImport("Converter\\step7\\Step7_Decryptor.dll")]
	public static extern int Decrypt_Start(IntPtr hdll, int szdll, IntPtr pin, int szin, IntPtr pout, int szout);

	[DllImport("Converter\\step7\\Step7_Decryptor.dll")]
	public static extern int Decrypt_Setup_Text(IntPtr ptxt, int sztxt);

	[DllImport("Converter\\step7\\Step7_Decryptor.dll")]
	public static extern int Decrypt_End();

	static S7Translator()
	{
		DeviceSeries = new List<S7DeviceSeries>();
		Devices = new List<S7Device>();
		UnitFormats = new Dictionary<int, S7UnitFormat>();
		STLFormats = new Dictionary<int, S7STLUnitFormat>();
		FBDFormats = new Dictionary<int, S7FBDUnitFormat>();
		string text = $"{FileHelper.AppRootPath}\\Converter\\step7\\example.smart";
		string path = $"{FileHelper.AppRootPath}\\Converter\\step7\\unit_list.txt";
		string path2 = $"{FileHelper.AppRootPath}\\Converter\\step7\\stl_list.txt";
		string path3 = $"{FileHelper.AppRootPath}\\Converter\\step7\\fbd_list.txt";
		string text2 = $"{FileHelper.AppRootPath}\\Converter\\step7\\unit_debug.txt";
		string path4 = $"{FileHelper.AppRootPath}\\Converter\\step7\\cpus.txt";
		string text3 = string.Empty;
		string text4 = string.Empty;
		string text5 = string.Empty;
		string text6 = string.Empty;
		StreamReader streamReader = null;
		StreamWriter streamWriter = null;
		List<S7UnitFormat> list = new List<S7UnitFormat>();
		List<S7STLUnitFormat> list2 = new List<S7STLUnitFormat>();
		List<S7FBDUnitFormat> list3 = new List<S7FBDUnitFormat>();
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		try
		{
			streamReader = new StreamReader(path);
			text3 = streamReader.ReadToEnd();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
		finally
		{
			streamReader?.Close();
			streamReader = null;
		}
		try
		{
			streamReader = new StreamReader(path2);
			text4 = streamReader.ReadToEnd();
		}
		catch (Exception ex2)
		{
			MessageBox.Show(ex2.Message);
		}
		finally
		{
			streamReader?.Close();
			streamReader = null;
		}
		try
		{
			streamReader = new StreamReader(path3);
			text5 = streamReader.ReadToEnd();
		}
		catch (Exception ex3)
		{
			MessageBox.Show(ex3.Message);
		}
		finally
		{
			streamReader?.Close();
			streamReader = null;
		}
		try
		{
			streamReader = new StreamReader(path4);
			text6 = streamReader.ReadToEnd();
		}
		catch (Exception ex4)
		{
			MessageBox.Show(ex4.Message);
		}
		finally
		{
			streamReader?.Close();
			streamReader = null;
		}
		string[] array = text3.Split(new char[1] { ';' }, StringSplitOptions.RemoveEmptyEntries);
		foreach (string text7 in array)
		{
			string[] array2 = text7.Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries);
			if (array2.Length >= 4)
			{
				int result = 0;
				int result2 = 0;
				int result3 = 0;
				S7UnitFormat s7UnitFormat = new S7UnitFormat();
				int.TryParse(array2[0], out result);
				int.TryParse(array2[1], out result3);
				int.TryParse(array2[2], out result2);
				s7UnitFormat.X = result2;
				s7UnitFormat.Y = result3;
				s7UnitFormat.Net = result;
				s7UnitFormat.Name = array2[3];
				list.Add(s7UnitFormat);
			}
		}
		string[] array3 = text4.Split(new char[1] { ';' }, StringSplitOptions.RemoveEmptyEntries);
		foreach (string text8 in array3)
		{
			string[] array4 = text8.Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries);
			if (array4.Length >= 3)
			{
				int result4 = 0;
				int result5 = 0;
				S7STLUnitFormat s7STLUnitFormat = new S7STLUnitFormat();
				int.TryParse(array4[0], out result4);
				int.TryParse(array4[1], out result5);
				s7STLUnitFormat.Y = result5;
				s7STLUnitFormat.Net = result4;
				s7STLUnitFormat.Name = array4[2];
				list2.Add(s7STLUnitFormat);
			}
		}
		string[] array5 = text5.Split(new char[1] { ';' }, StringSplitOptions.RemoveEmptyEntries);
		foreach (string text9 in array5)
		{
			string[] array6 = text9.Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries);
			if (array6.Length >= 4)
			{
				int result6 = 0;
				int result7 = 0;
				int result8 = 0;
				S7FBDUnitFormat s7FBDUnitFormat = new S7FBDUnitFormat();
				int.TryParse(array6[0], out result6);
				int.TryParse(array6[1], out result8);
				int.TryParse(array6[2], out result7);
				s7FBDUnitFormat.X = result7;
				s7FBDUnitFormat.Y = result8;
				s7FBDUnitFormat.Net = result6;
				s7FBDUnitFormat.Name = array6[3];
				list3.Add(s7FBDUnitFormat);
			}
		}
		string[] array7 = text6.Split(new char[1] { ';' }, StringSplitOptions.RemoveEmptyEntries);
		int num4 = 0;
		int num5 = 0;
		while (num4 + 2 < array7.Length)
		{
			string[] source = array7[num4].Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries);
			string sername = array7[num4 + 1];
			S7DeviceSeries s7DeviceSeries = null;
			S7Device[] array8 = null;
			if (sername.Equals("*"))
			{
				s7DeviceSeries = new S7DeviceSeries(sername);
				array8 = new S7Device[0];
			}
			else
			{
				s7DeviceSeries = DeviceSeries.FirstOrDefault((S7DeviceSeries _ser) => _ser.Name.Equals(sername));
				array8 = source.Select((string dn) => new S7Device(dn)).ToArray();
				S7Device[] array9 = array8;
				foreach (S7Device s7Device in array9)
				{
					s7Device.Series = s7DeviceSeries;
				}
			}
			for (num5 = num4 + 2; num5 < array7.Length && !array7[num5].Equals("$END"); num5++)
			{
				string[] array10 = array7[num5].Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries);
				if (array10.Length < 3 || !Enum.TryParse<Enum_S7BaseType>(array10[0], out var result9) || !int.TryParse(array10[1], out var result10) || !int.TryParse(array10[2], out var result11))
				{
					continue;
				}
				S7DeviceValueRange item = new S7DeviceValueRange(result9, result10, result11);
				if (array8.Length != 0)
				{
					S7Device[] array11 = array8;
					foreach (S7Device s7Device2 in array11)
					{
						s7Device2.Ranges.Add(item);
					}
				}
				else
				{
					s7DeviceSeries?.Ranges.Add(item);
				}
			}
			num4 = num5;
		}
		using (S7Translator s7Translator = new S7Translator(text))
		{
			S7Project s7Project = s7Translator.Read();
			foreach (S7Network net in s7Project.Main.Nets)
			{
				foreach (S7UnitBase unit in net.Units)
				{
					if (num >= list.Count())
					{
						break;
					}
					S7UnitFormat s7UnitFormat2 = list[num];
					if (unit is S7Unit)
					{
						S7Unit s7Unit = (S7Unit)unit;
						if (s7UnitFormat2.Net == net.ID && s7UnitFormat2.X == s7Unit.X && s7UnitFormat2.Y == s7Unit.Y)
						{
							s7UnitFormat2.Core = s7Unit;
							num++;
						}
					}
				}
			}
			foreach (S7Program prog in s7Project.Progs)
			{
				if (prog.E == Enum_Program.SBR)
				{
					foreach (S7Network net2 in prog.Nets)
					{
						foreach (S7STLStmt sTL in net2.STLs)
						{
							if (num2 >= list2.Count())
							{
								break;
							}
							S7STLUnitFormat s7STLUnitFormat2 = list2[num2];
							if (s7STLUnitFormat2.Net == net2.ID && s7STLUnitFormat2.Y == sTL.ID)
							{
								s7STLUnitFormat2.Core = sTL;
								num2++;
							}
						}
						foreach (S7UnitBase unit2 in net2.Units)
						{
							if (num2 >= list2.Count())
							{
								break;
							}
							S7STLUnitFormat s7STLUnitFormat3 = list2[num2];
							if (unit2 is S7STLUnit)
							{
								S7STLUnit s7STLUnit = (S7STLUnit)unit2;
								if (s7STLUnitFormat3.Net == net2.ID && s7STLUnitFormat3.Y == s7STLUnit.ID)
								{
									s7STLUnitFormat3.Core = new S7STLStmt(net2, s7STLUnit);
									num2++;
								}
							}
						}
					}
				}
				if (prog.E != Enum_Program.INT)
				{
					continue;
				}
				foreach (S7Network net3 in prog.Nets)
				{
					foreach (S7UnitBase unit3 in net3.Units)
					{
						if (num3 >= list3.Count())
						{
							break;
						}
						S7FBDUnitFormat s7FBDUnitFormat2 = list3[num3];
						S7FBDUnit s7FBDUnit = ((unit3 is S7FBDUnit) ? ((S7FBDUnit)unit3) : ((unit3 is S7Unit) ? new S7FBDUnit(net3, (S7Unit)unit3) : null));
						if (s7FBDUnit != null && s7FBDUnitFormat2.Net == net3.ID && s7FBDUnitFormat2.X == s7FBDUnit.X && s7FBDUnitFormat2.Y == s7FBDUnit.Y)
						{
							s7FBDUnitFormat2.Core = s7FBDUnit;
							num3++;
						}
					}
				}
			}
		}
		foreach (S7STLUnitFormat item2 in list2)
		{
			if (item2.Core != null && !STLFormats.ContainsKey(item2.Code))
			{
				STLFormats.Add(item2.Code, item2);
			}
		}
		foreach (S7FBDUnitFormat item3 in list3)
		{
			if (item3.Core != null && !FBDFormats.ContainsKey(item3.Code))
			{
				FBDFormats.Add(item3.Code, item3);
			}
		}
		foreach (S7UnitFormat item4 in list)
		{
			if (item4.Core != null)
			{
				if (!UnitFormats.ContainsKey(item4.Code))
				{
					UnitFormats.Add(item4.Code, item4);
				}
				if (!STLFormats.ContainsKey(item4.Code))
				{
					STLFormats.Add(item4.Code, new S7STLUnitFormat(item4));
				}
				if (!FBDFormats.ContainsKey(item4.Code))
				{
					FBDFormats.Add(item4.Code, new S7FBDUnitFormat(item4));
				}
			}
		}
	}

	public unsafe S7Translator(string _filename)
	{
		filename = _filename;
		pin = IntPtr.Zero;
		pout = IntPtr.Zero;
		pdata = IntPtr.Zero;
		hdll = IntPtr.Zero;
		FileStream fileStream = null;
		try
		{
			fileStream = File.OpenRead(filename);
		}
		catch (IOException ex)
		{
			MessageBox.Show(ex.Message);
			return;
		}
		byte[] array = new byte[fileStream.Length];
		fileStream.Read(array, 0, array.Length);
		fileStream.Close();
		pdata = Marshal.AllocHGlobal(array.Length);
		Marshal.Copy(array, 0, pdata, array.Length);
		int szin = array.Length - 112;
		int num = *(int*)(void*)(pdata + 108);
		pin = pdata + 112;
		pout = Marshal.AllocHGlobal(System.Math.Min(num, 1048575));
		string path = $"{FileHelper.AppRootPath}\\Converter\\step7\\module_data.bin";
		string path2 = $"{FileHelper.AppRootPath}\\Converter\\step7\\module_text.bin";
		try
		{
			fileStream = File.OpenRead(path);
		}
		catch (IOException ex2)
		{
			MessageBox.Show(ex2.Message);
			return;
		}
		array = new byte[fileStream.Length];
		fileStream.Read(array, 0, array.Length);
		fileStream.Close();
		int num2 = array.Length;
		hdll = Marshal.AllocHGlobal(num2);
		Marshal.Copy(array, 0, hdll, num2);
		Decrypt_Start(hdll, num2, pin, szin, pout, num);
		try
		{
			fileStream = File.OpenRead(path2);
		}
		catch (IOException ex3)
		{
			MessageBox.Show(ex3.Message);
			return;
		}
		array = new byte[fileStream.Length];
		fileStream.Read(array, 0, array.Length);
		fileStream.Close();
		fixed (byte* ptr = &array[0])
		{
			Decrypt_Setup_Text((IntPtr)ptr, array.Length);
		}
		Decrypt_End();
		s = new S7DataStream(pout, num);
		Print_Debug();
	}

	public void Dispose()
	{
		s?.Dispose();
		if (pdata != IntPtr.Zero)
		{
			Marshal.FreeHGlobal(pdata);
		}
		if (pout != IntPtr.Zero)
		{
			Marshal.FreeHGlobal(pout);
		}
		if (hdll != IntPtr.Zero)
		{
			Marshal.FreeHGlobal(hdll);
		}
		filename = null;
		pdata = IntPtr.Zero;
		pin = IntPtr.Zero;
		pout = IntPtr.Zero;
		s = null;
	}

	public void Print_Debug()
	{
		string directoryName = Path.GetDirectoryName(filename);
		string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filename);
		string fpbin = $"{directoryName}\\{fileNameWithoutExtension}_data.bin";
		string fptxt = $"{directoryName}\\{fileNameWithoutExtension}_text.txt";
		s.PrintData(fpbin);
		s.PrintText(fptxt);
	}

	public S7Project Read()
	{
		s.Start(0);
		return new S7Project(s);
	}
}
