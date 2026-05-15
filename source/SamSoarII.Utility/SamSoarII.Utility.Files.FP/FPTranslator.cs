using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SamSoarII.Utility.Files.FP;

public class FPTranslator : IDisposable
{
	private enum ListStatus
	{
		Root,
		POU0,
		POU1,
		POU2,
		POU3,
		NET0,
		NET1,
		NET2,
		NET3,
		CNV0,
		CNV1,
		CNV2,
		CNV3,
		U0,
		U1,
		U2,
		U3,
		C0,
		C1,
		C2,
		C3,
		C4,
		C5,
		C6,
		C7
	}

	private enum ConvStatus
	{
		None,
		SystemValue,
		Instruction,
		FuncBlock
	}

	private string filepath;

	private FPDataStream stream;

	public string FilePath => filepath;

	public FPDataStream Stream => stream;

	[DllImport("Converter\\fpwin\\FPWin_GX7_Encryptor.dll")]
	public static extern int Encrypt(IntPtr data, int size);

	static FPTranslator()
	{
		try
		{
			string text = $"{FileHelper.AppRootPath}\\Converter\\fpwin\\example_project_2.fpx";
			string path = $"{FileHelper.AppRootPath}\\Converter\\fpwin\\unit_list_2.txt";
			string path2 = $"{FileHelper.AppRootPath}\\Converter\\fpwin\\toex.txt";
			string path3 = $"{FileHelper.AppRootPath}\\Converter\\fpwin\\tofunc.c";
			using (FPTranslator fPTranslator = new FPTranslator(text))
			{
				FPProject fPProject = fPTranslator.Preload();
				FPLadder fPLadder = null;
				FPNetwork fPNetwork = null;
				ListStatus listStatus = ListStatus.Root;
				ConvStatus convStatus = ConvStatus.None;
				string text2 = null;
				string wd = null;
				int num = 0;
				string text3 = null;
				string text4 = null;
				string oldname = null;
				string newname = null;
				string text5 = null;
				string text6 = null;
				int num2 = 0;
				List<string> list = new List<string>();
				int result = 0;
				int result2 = 0;
				using (StreamReader streamReader = new StreamReader(path))
				{
					text2 = streamReader.ReadToEnd();
				}
				for (int i = 0; i < text2.Length; i++)
				{
					switch (text2[i])
					{
					case '{':
						switch (listStatus)
						{
						case ListStatus.POU2:
							listStatus = ListStatus.POU3;
							num = i + 1;
							break;
						case ListStatus.NET2:
							listStatus = ListStatus.NET3;
							num = i + 1;
							break;
						case ListStatus.CNV2:
							listStatus = ListStatus.CNV3;
							num = i + 1;
							break;
						}
						break;
					case '}':
						switch (listStatus)
						{
						case ListStatus.NET3:
							listStatus = ListStatus.POU3;
							num = i + 1;
							fPNetwork = null;
							break;
						case ListStatus.POU3:
							listStatus = ListStatus.Root;
							num = i + 1;
							fPLadder = null;
							break;
						case ListStatus.CNV3:
							listStatus = ListStatus.Root;
							num = i + 1;
							convStatus = ConvStatus.None;
							break;
						}
						break;
					case '(':
						switch (listStatus)
						{
						case ListStatus.Root:
							if (i >= 3 && text2[i - 3] == 'P' && text2[i - 2] == 'O' && text2[i - 1] == 'U')
							{
								listStatus = ListStatus.POU0;
								num = i + 1;
							}
							else if (i >= 4 && text2[i - 4] == 'C' && text2[i - 3] == 'O' && text2[i - 2] == 'N' && text2[i - 1] == 'V')
							{
								listStatus = ListStatus.CNV0;
								num = i + 1;
							}
							break;
						case ListStatus.POU3:
							if (i >= 3 && text2[i - 3] == 'N' && text2[i - 2] == 'E' && text2[i - 1] == 'T')
							{
								listStatus = ListStatus.NET0;
								num = i + 1;
							}
							break;
						case ListStatus.C0:
							if (i > num)
							{
								newname = text2.Substring(num, i - num);
							}
							listStatus = ListStatus.C1;
							num = i + 1;
							break;
						case ListStatus.C3:
							num2++;
							break;
						}
						break;
					case ')':
						switch (listStatus)
						{
						case ListStatus.POU0:
							if (i > num)
							{
								wd = text2.Substring(num, i - num);
								fPLadder = fPProject.Ladders.FirstOrDefault((FPLadder _pou) => _pou.Name.Equals(wd));
							}
							listStatus = ListStatus.POU2;
							break;
						case ListStatus.NET0:
							if (i > num && fPLadder != null)
							{
								wd = text2.Substring(num, i - num);
								int result3 = 0;
								if (int.TryParse(wd, out result3))
								{
									fPNetwork = fPLadder.Children[result3];
								}
							}
							listStatus = ListStatus.NET2;
							break;
						case ListStatus.CNV0:
							if (i > num)
							{
								wd = text2.Substring(num, i - num);
								wd = wd.Trim();
								string text7 = wd;
								string text8 = text7;
								if (!(text8 == "SystemValue"))
								{
									if (text8 == "Instruction")
									{
										convStatus = ConvStatus.Instruction;
									}
								}
								else
								{
									convStatus = ConvStatus.SystemValue;
								}
							}
							listStatus = ListStatus.CNV2;
							break;
						case ListStatus.C1:
							if (i > num)
							{
								if (text5 == null)
								{
									text5 = text2.Substring(num, i - num);
								}
								else if (text6 == null)
								{
									text6 = text2.Substring(num, i - num);
								}
							}
							listStatus = ListStatus.C2;
							num = i + 1;
							break;
						case ListStatus.C3:
							num2--;
							break;
						}
						break;
					case ',':
						switch (listStatus)
						{
						case ListStatus.NET3:
							if (i > num)
							{
								text3 = text2.Substring(num, i - num);
							}
							listStatus = ListStatus.U0;
							num = i + 1;
							break;
						case ListStatus.U0:
							if (i > num)
							{
								int.TryParse(text2.Substring(num, i - num), out result);
							}
							listStatus = ListStatus.U1;
							num = i + 1;
							break;
						case ListStatus.U1:
							if (i > num)
							{
								int.TryParse(text2.Substring(num, i - num), out result2);
							}
							listStatus = ListStatus.U2;
							num = i + 1;
							break;
						case ListStatus.U2:
							if (i > num)
							{
								text4 = text2.Substring(num, i - num);
							}
							listStatus = ListStatus.U3;
							num = i + 1;
							break;
						case ListStatus.CNV3:
							if (i > num)
							{
								oldname = text2.Substring(num, i - num);
							}
							listStatus = ListStatus.C0;
							num = i + 1;
							newname = (text5 = (text6 = null));
							list.Clear();
							break;
						case ListStatus.C0:
							if (i > num)
							{
								newname = text2.Substring(num, i - num);
							}
							listStatus = ListStatus.C3;
							num = i + 1;
							break;
						case ListStatus.C1:
							if (i > num)
							{
								if (text5 == null)
								{
									text5 = text2.Substring(num, i - num);
								}
								else if (text6 == null)
								{
									text6 = text2.Substring(num, i - num);
								}
							}
							num = i + 1;
							break;
						case ListStatus.C2:
							listStatus = ListStatus.C3;
							num = i + 1;
							break;
						case ListStatus.C3:
							if (num2 <= 0)
							{
								if (i > num)
								{
									list.Add(text2.Substring(num, i - num));
								}
								num = i + 1;
							}
							break;
						}
						break;
					case ';':
						switch (listStatus)
						{
						case ListStatus.NET3:
							if (i > num)
							{
								text3 = text2.Substring(num, i - num);
							}
							break;
						case ListStatus.U0:
							if (i > num)
							{
								int.TryParse(text2.Substring(num, i - num), out result);
							}
							break;
						case ListStatus.U1:
							if (i > num)
							{
								int.TryParse(text2.Substring(num, i - num), out result2);
							}
							break;
						case ListStatus.U2:
							if (i > num)
							{
								text4 = text2.Substring(num, i - num);
							}
							break;
						case ListStatus.C0:
						case ListStatus.C1:
						case ListStatus.C2:
						case ListStatus.C3:
							if (i > num)
							{
								switch (listStatus)
								{
								case ListStatus.C0:
									newname = text2.Substring(num, i - num);
									break;
								case ListStatus.C3:
									list.Add(text2.Substring(num, i - num));
									break;
								}
							}
							switch (convStatus)
							{
							case ConvStatus.SystemValue:
							{
								FPValueConvert fPValueConvert = new FPValueConvert(oldname, newname);
								if (!FPValueConvert.ItemOfNames.ContainsKey(fPValueConvert.OldName))
								{
									FPValueConvert.ItemOfNames.Add(fPValueConvert.OldName, fPValueConvert);
								}
								break;
							}
							case ConvStatus.Instruction:
							{
								FPUnitConvert fPUnitConvert = new FPUnitBaseConvert(oldname, newname, text5, text6, list.Count() == 0);
								if (!FPUnitConvert.ItemOfNames.ContainsKey(fPUnitConvert.OldName))
								{
									FPUnitConvert.ItemOfNames.Add(fPUnitConvert.OldName, fPUnitConvert);
								}
								foreach (string item in list)
								{
									fPUnitConvert.Add(item);
								}
								break;
							}
							}
							num = i + 1;
							break;
						}
						switch (listStatus)
						{
						case ListStatus.U0:
						case ListStatus.U1:
						case ListStatus.U2:
						case ListStatus.U3:
							if (fPNetwork != null && text3 != null && result >= 0 && result2 >= 0 && result < fPNetwork.Width && result2 < fPNetwork.Height)
							{
								FPUnit fPUnit = fPNetwork.Children[result, result2];
								if (fPUnit != null)
								{
									FPUnitFormat fPUnitFormat = new FPUnitFormat(fPUnit, text3);
									if (text4 != null && text4.Contains('R'))
									{
										fPUnitFormat.IsUseMainRegi = true;
									}
									if (text4 != null && text4.Contains('O'))
									{
										fPUnitFormat.IsUseOffset = true;
									}
									if (!FPFormat.ItemOfNames.ContainsKey(fPUnitFormat.Name))
									{
										FPFormat.ItemOfNames.Add(fPUnitFormat.Name, fPUnitFormat);
									}
									if (!fPUnitFormat.IsUseMainRegi)
									{
										FPFormat.Units.Set(fPUnitFormat);
									}
									else
									{
										foreach (FPFormat item2 in FPFormat.Values.Items)
										{
											uint regicode = item2.MainCode & 0xFF00;
											FPUnitFormat fmt = fPUnitFormat.ToRegi(regicode);
											FPFormat.Units.Set(fmt);
										}
									}
								}
							}
							listStatus = ListStatus.NET3;
							num = i + 1;
							text3 = null;
							text4 = null;
							result = (result2 = 0);
							break;
						case ListStatus.C0:
						case ListStatus.C1:
						case ListStatus.C2:
						case ListStatus.C3:
							listStatus = ListStatus.CNV3;
							num = i + 1;
							break;
						}
						break;
					case '\t':
					case '\n':
					case '\r':
					case ' ':
						num = i + 1;
						break;
					}
				}
			}
			using (StreamReader streamReader2 = new StreamReader(path2))
			{
				string text9 = streamReader2.ReadToEnd();
				string text10 = null;
				ListStatus listStatus2 = ListStatus.Root;
				int num3 = 0;
				FPUnitFuncConvert fPUnitFuncConvert = null;
				for (int num4 = 0; num4 < text9.Length; num4++)
				{
					switch (text9[num4])
					{
					case '(':
						switch (listStatus2)
						{
						case ListStatus.Root:
							if (num3 < num4)
							{
								text10 = text9.Substring(num3, num4 - num3);
							}
							num3 = num4 + 1;
							listStatus2 = ListStatus.CNV0;
							fPUnitFuncConvert = new FPUnitFuncConvert(text10);
							if (!FPUnitConvert.ItemOfNames.ContainsKey(text10))
							{
								FPUnitConvert.ItemOfNames.Add(text10, fPUnitFuncConvert);
							}
							break;
						case ListStatus.CNV1:
							num3 = num4 + 1;
							listStatus2 = ListStatus.CNV2;
							break;
						}
						break;
					case ')':
						switch (listStatus2)
						{
						case ListStatus.CNV0:
							if (num3 < num4)
							{
								fPUnitFuncConvert.Temps.Add(text9.Substring(num3, num4 - num3));
							}
							num3 = num4 + 1;
							listStatus2 = ListStatus.CNV1;
							break;
						case ListStatus.CNV2:
							if (num3 < num4)
							{
								fPUnitFuncConvert.VarTypes.Add(text9.Substring(num3, num4 - num3));
							}
							num3 = num4 + 1;
							listStatus2 = ListStatus.CNV3;
							break;
						}
						break;
					case '{':
					{
						ListStatus listStatus5 = listStatus2;
						ListStatus listStatus6 = listStatus5;
						if (listStatus6 == ListStatus.CNV3)
						{
							num3 = num4 + 1;
							listStatus2 = ListStatus.C0;
						}
						break;
					}
					case '}':
					{
						ListStatus listStatus7 = listStatus2;
						ListStatus listStatus8 = listStatus7;
						if (listStatus8 == ListStatus.C0)
						{
							if (num3 < num4)
							{
								fPUnitFuncConvert.Add(text9.Substring(num3, num4 - num3));
							}
							num3 = num4 + 1;
							listStatus2 = ListStatus.Root;
						}
						break;
					}
					case ',':
						switch (listStatus2)
						{
						case ListStatus.CNV0:
							if (num3 < num4)
							{
								fPUnitFuncConvert.Temps.Add(text9.Substring(num3, num4 - num3));
							}
							num3 = num4 + 1;
							break;
						case ListStatus.CNV2:
							if (num3 < num4)
							{
								fPUnitFuncConvert.VarTypes.Add(text9.Substring(num3, num4 - num3));
							}
							num3 = num4 + 1;
							break;
						}
						break;
					case ';':
					{
						ListStatus listStatus3 = listStatus2;
						ListStatus listStatus4 = listStatus3;
						if (listStatus4 == ListStatus.C0)
						{
							if (num3 < num4)
							{
								fPUnitFuncConvert.Add(text9.Substring(num3, num4 - num3));
							}
							num3 = num4 + 1;
						}
						break;
					}
					}
				}
			}
			using (StreamReader streamReader3 = new StreamReader(path3))
			{
				string text11 = streamReader3.ReadToEnd();
				string text12 = null;
				int num5 = 0;
				for (int num6 = 0; num6 < text11.Length; num6++)
				{
					if (text11[num6] != '#' || num6 + 7 > text11.Length || !text11.Substring(num6 + 1, 6).Equals("pragma", StringComparison.CurrentCultureIgnoreCase))
					{
						continue;
					}
					int num7;
					for (num7 = num6 + 6; num7 < text11.Length && (text11[num7] == ' ' || text11[num7] == '\t'); num7++)
					{
					}
					if (num7 + 6 <= text11.Length && text11.Substring(num7, 6).Equals("region", StringComparison.CurrentCultureIgnoreCase))
					{
						for (num7 += 6; num7 < text11.Length && (text11[num7] == ' ' || text11[num7] == '\t'); num7++)
						{
						}
						int num8;
						for (num8 = num7; num8 < text11.Length && text11[num8] != '\r' && text11[num8] != '\n'; num8++)
						{
						}
						text12 = text11.Substring(num7, num8 - num7);
						num6 = num8;
						num5 = num8 + 1;
					}
					else if (num7 + 9 < text11.Length && text11.Substring(num7, 9).Equals("endregion", StringComparison.CurrentCultureIgnoreCase))
					{
						string text13 = text11.Substring(num5, num6 - num5);
						FPFuncBlock value = new FPFuncBlock(text12, text13);
						if (text12.Equals("_Common_Defines"))
						{
							FPFuncBlock.CommonDefines = text13;
						}
						else if (!FPFuncBlock.ItemOfNames.ContainsKey(text12))
						{
							FPFuncBlock.ItemOfNames.Add(text12, value);
						}
					}
				}
			}
			foreach (FPFormat item3 in FPFormat.Units.Items)
			{
				if (item3 is FPUnitFormat)
				{
					FPUnitFormat fPUnitFormat2 = (FPUnitFormat)item3;
					FPUnitConvert value2 = null;
					if (FPUnitConvert.ItemOfNames.TryGetValue(fPUnitFormat2.Name, out value2))
					{
						fPUnitFormat2.Conv = value2;
					}
				}
				else
				{
					if (!(item3 is FPFormatGroup))
					{
						continue;
					}
					foreach (FPFormat item4 in ((FPFormatGroup)item3).Items)
					{
						if (item4 is FPUnitFormat)
						{
							FPUnitFormat fPUnitFormat3 = (FPUnitFormat)item4;
							FPUnitConvert value3 = null;
							if (FPUnitConvert.ItemOfNames.TryGetValue(fPUnitFormat3.Name, out value3))
							{
								fPUnitFormat3.Conv = value3;
							}
						}
					}
				}
			}
			foreach (FPUnitConvert value5 in FPUnitConvert.ItemOfNames.Values)
			{
				if (!(value5 is FPUnitBaseConvert))
				{
					continue;
				}
				FPUnitBaseConvert fPUnitBaseConvert = (FPUnitBaseConvert)value5;
				for (int num9 = 0; num9 < fPUnitBaseConvert.Items.Count(); num9++)
				{
					if (fPUnitBaseConvert.Items[num9] is FPUnitBaseConst)
					{
						FPUnitBaseConst fPUnitBaseConst = (FPUnitBaseConst)fPUnitBaseConvert.Items[num9];
						FPValueConvert value4 = null;
						if (FPValueConvert.ItemOfNames.TryGetValue(fPUnitBaseConst.Text, out value4))
						{
							fPUnitBaseConvert.Items[num9] = new FPUnitBaseVar(fPUnitBaseConst.Text, value4);
						}
					}
				}
			}
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	public FPTranslator(string _filepath)
	{
		filepath = _filepath;
		FileStream fileStream = null;
		IntPtr intPtr = IntPtr.Zero;
		int num = 0;
		byte[] array = null;
		try
		{
			fileStream = File.OpenRead(filepath);
			num = (((int)fileStream.Length - 1) / 16 + 1) * 16;
			array = new byte[num];
			fileStream.Read(array, 0, (int)fileStream.Length);
		}
		finally
		{
			fileStream?.Close();
		}
		try
		{
			intPtr = Marshal.AllocHGlobal(num);
			Marshal.Copy(array, 0, intPtr, num);
			Encrypt(intPtr + 448, num - 448);
			Marshal.Copy(intPtr, array, 0, num);
		}
		finally
		{
			if (intPtr != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}
		try
		{
			string path = filepath.Substring(0, filepath.Length - 4) + ".bin";
			fileStream = File.Create(path);
			fileStream.Write(array, 0, array.Length);
		}
		finally
		{
			fileStream?.Close();
		}
		stream = new FPDataStream(array);
	}

	public void Dispose()
	{
		stream?.Dispose();
	}

	private FPProject Preload()
	{
		stream.Offset = 448;
		FPRedirectList fPRedirectList = new FPRedirectList(stream);
		FPRedirectItem fPRedirectItem = null;
		FPRedirectItem fPRedirectItem2 = null;
		FPProject fPProject = null;
		foreach (FPRedirectItem item2 in fPRedirectList)
		{
			switch (item2.Code)
			{
			case 528:
				fPRedirectItem = item2;
				break;
			case 1552:
				fPRedirectItem2 = item2;
				break;
			}
		}
		if (fPRedirectItem != null)
		{
			stream.Offset = fPRedirectItem.Offset;
			stream.Offset++;
			int count = stream.ReadU16();
			string name = stream.ReadUnicode(count);
			fPProject = new FPProject();
			fPProject.Name = name;
			stream.Offset += 16;
			int num = stream.ReadI32();
			int[] array = new int[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = (int)stream.ReadU64();
			}
			for (int j = 0; j < num; j++)
			{
				stream.Offset = array[j];
				int count2 = stream.ReadU16();
				string name2 = stream.ReadUnicode(count2);
				FPLadder item = new FPLadder(j, name2);
				fPProject.Ladders.Add(item);
			}
		}
		stream.Offset = 8;
		uint key = stream.ReadU32();
		FPDevice value = null;
		if (FPDevice.ItemOfBins.TryGetValue(key, out value))
		{
			fPProject.Device = value;
		}
		if (fPRedirectItem2 != null && fPProject != null)
		{
			stream.Offset = fPRedirectItem2.Offset;
			if (fPProject.Device != null && fPProject.Device.Parent.Name.Equals("FP2"))
			{
				stream.Offset += 32;
			}
			int[] array2 = new int[fPProject.Ladders.Count()];
			for (int k = 0; k < array2.Length; k++)
			{
				array2[k] = (int)stream.ReadU64();
			}
			for (int l = 0; l < array2.Length; l++)
			{
				stream.Offset = array2[l];
				stream.Offset += 32;
				FPLadder fPLadder = fPProject.Ladders[l];
				int num2 = stream.ReadU16();
				int[] array3 = new int[num2];
				for (int m = 0; m < num2; m++)
				{
					array3[m] = (int)stream.ReadU64();
				}
				for (int n = 0; n < num2; n++)
				{
					stream.Offset = array3[n];
					ushort code = stream.ReadU16();
					int step = stream.ReadI32();
					int flag = stream.ReadI32();
					int num3 = stream.ReadU16();
					FPNetwork fPNetwork = new FPNetwork(n, code, step, flag, num3);
					fPLadder.Children.Add(fPNetwork);
					for (int num4 = 0; num4 < num3; num4++)
					{
						for (int num5 = 0; num5 < 10; num5++)
						{
							long offset = stream.ReadI64();
							uint num6 = stream.ReadU32();
							uint posicode = stream.ReadU32();
							uint idencode = stream.ReadU32();
							FPUnit value2 = new FPUnit(fPNetwork, num5, num4, offset, num6, posicode, idencode);
							fPNetwork.Children[num5, num4] = value2;
							if ((num6 & 8) != 0)
							{
								FPVLine value3 = new FPVLine(num5, num4);
								fPNetwork.VLines[num5, num4] = value3;
							}
						}
					}
				}
			}
		}
		return fPProject;
	}

	public FPProject Translate()
	{
		FPProject fPProject = Preload();
		if (fPProject != null)
		{
			string path = filepath.Substring(0, filepath.Length - 4) + "_report.txt";
			StreamWriter streamWriter = new StreamWriter(path);
			foreach (FPLadder ladder in fPProject.Ladders)
			{
				streamWriter.WriteLine("======== Ladder {0} ==================", ladder.Name);
				foreach (FPNetwork child in ladder.Children)
				{
					streamWriter.WriteLine("==== Net {0} ========", child.ID);
					for (int i = 0; i < child.Height; i++)
					{
						for (int j = 0; j < child.Width; j++)
						{
							FPUnit fPUnit = child.Children[j, i];
							FPVLine fPVLine = child.VLines[j, i];
							streamWriter.Write("(Y={0},X={1})", i, j);
							if (fPVLine != null)
							{
								streamWriter.Write("(V)");
							}
							if (fPUnit != null)
							{
								streamWriter.Write("(O=0x{0:X16},M=0x{1:X8},P=0x{2:X8},I=0x{3:X8})", fPUnit.Offset, fPUnit.MainCode, fPUnit.PosiCode, fPUnit.IdenCode);
							}
							streamWriter.WriteLine();
						}
					}
				}
			}
			streamWriter.Close();
		}
		if (fPProject != null)
		{
			foreach (FPLadder ladder2 in fPProject.Ladders)
			{
				foreach (FPNetwork child2 in ladder2.Children)
				{
					for (int k = 0; k < child2.Height; k++)
					{
						for (int l = 0; l < child2.Width; l++)
						{
							FPUnit fPUnit2 = child2.Children[l, k];
							if (fPUnit2 == null)
							{
								continue;
							}
							int num = l - fPUnit2.XOfWidth + 1;
							int num2 = k - fPUnit2.YOfHeight + 1;
							int num3 = num + fPUnit2.Width;
							int num4 = num2 + fPUnit2.Height;
							if (num3 < child2.Width && fPUnit2.Width == 3)
							{
								if (fPUnit2.XOfWidth == 2 && fPUnit2.YOfHeight == 1)
								{
									FPFormat format = FPFormat.Units.Get(fPUnit2);
									fPUnit2.Format = format;
									FPUnit fPUnit3 = child2.Children[num, num2];
									fPUnit3.Parent = fPUnit2;
								}
								else
								{
									FPUnit parent = child2.Children[num + 1, num2];
									fPUnit2.Parent = parent;
								}
							}
							else if (fPUnit2.XOfWidth == 1 && fPUnit2.YOfHeight == 1)
							{
								FPFormat format2 = FPFormat.Units.Get(fPUnit2);
								fPUnit2.Format = format2;
							}
							else if (fPUnit2.Width > 0 && fPUnit2.Height > 0 && num >= 0 && num < child2.Width && num2 >= 0 && num2 < child2.Height)
							{
								FPUnit parent2 = child2.Children[num, num2];
								fPUnit2.Parent = parent2;
							}
						}
					}
				}
			}
		}
		if (fPProject != null)
		{
			string path2 = filepath.Substring(0, filepath.Length - 4) + "_unit.txt";
			StreamWriter streamWriter2 = new StreamWriter(path2);
			StringBuilder stringBuilder = new StringBuilder();
			foreach (FPLadder ladder3 in fPProject.Ladders)
			{
				streamWriter2.WriteLine("======== Ladder {0} ==================", ladder3.Name);
				foreach (FPNetwork child3 in ladder3.Children)
				{
					streamWriter2.WriteLine("==== Net {0} ========", child3.ID);
					for (int m = 0; m < child3.Height; m++)
					{
						stringBuilder.Clear();
						for (int n = 0; n < child3.Width; n++)
						{
							FPUnit value = child3.Children[n, m];
							stringBuilder.Append(value);
							stringBuilder.Append(' ');
							while (stringBuilder.Length < (n + 1) * 8)
							{
								stringBuilder.Append(' ');
							}
						}
						streamWriter2.WriteLine(stringBuilder.ToString());
					}
				}
			}
			streamWriter2.Close();
		}
		return fPProject;
	}
}
