using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace SamSoarII.Utility.Files.XD;

public class XDTranslator : IDisposable
{
	public static readonly List<XDDeviceSeries> DevSers;

	public static readonly List<XDDevice> Devs;

	private string filepath;

	private string plcname;

	private XDDevice device;

	private XDLadderDocument laddoc;

	private XDCommentDocument cmtdoc;

	private List<XDFuncBlockDocument> fbdocs;

	public static string TempPath => $"{FileHelper.AppRootPath}\\rar\\temp";

	public static string HaoZipCExePath => $"{FileHelper.AppRootPath}\\rar\\HaoZipC.exe";

	public static string XinJePath => $"{FileHelper.AppRootPath}\\converter\\xinje";

	public string FilePath => filepath;

	public string PLCName => plcname;

	public XDDevice Device => device;

	public XDLadderDocument LadDoc => laddoc;

	public XDCommentDocument CmtDoc => cmtdoc;

	public IList<XDFuncBlockDocument> FBDocs => fbdocs;

	static XDTranslator()
	{
		DevSers = (from Enum_XDDeviceSeries e in Enum.GetValues(typeof(Enum_XDDeviceSeries))
			select new XDDeviceSeries(e, e.ToString())).ToList();
		Devs = new List<XDDevice>();
		string path = $"{XinJePath}\\device_list.txt";
		StreamReader streamReader = null;
		XDDeviceSeries xDDeviceSeries = null;
		XDValueRange xDValueRange = null;
		List<XDDevice> list = new List<XDDevice>();
		string[] array = null;
		int num = 10;
		int num2 = 0;
		try
		{
			streamReader = new StreamReader(path);
		}
		catch (IOException)
		{
		}
		foreach (string item in from _a in (streamReader?.ReadToEnd() ?? string.Empty).Split(';')
			select _a.Trim())
		{
			switch (num2)
			{
			case 0:
			{
				if (Enum.TryParse<Enum_XDDeviceSeries>(item, out var result))
				{
					xDDeviceSeries = DevSers[(int)result];
					num2 = 1;
				}
				break;
			}
			case 1:
				num2 = 2;
				list.Clear();
				if (item.Equals("*"))
				{
					break;
				}
				foreach (string item2 in from _a in item.Split(',')
					select _a.Trim())
				{
					list.Add(new XDDevice(xDDeviceSeries, item2));
				}
				break;
			case 2:
				if (item.Equals("$END"))
				{
					Devs.AddRange(list);
					num2 = 0;
					break;
				}
				array = (from _a in item.Split(',')
					select _a.Trim()).ToArray();
				if (array.Length < 3)
				{
					break;
				}
				num = ((array[0].Equals("X") || array[0].Equals("Y")) ? 8 : 10);
				xDValueRange = new XDValueRange(array[0], ValueConverter.NBase_Parse(array[1], num), ValueConverter.NBase_Parse(array[2], num), num);
				if (list.Count() == 0)
				{
					xDDeviceSeries.VRs.Add(xDValueRange);
					break;
				}
				foreach (XDDevice item3 in list)
				{
					item3.VRs.Add(xDValueRange);
				}
				break;
			}
		}
		try
		{
			streamReader?.Close();
		}
		catch (IOException)
		{
		}
	}

	public XDTranslator(string _filepath)
	{
		filepath = _filepath;
		string text = Path.Combine(TempPath, "xdzip.zip");
		string text2 = Path.Combine(TempPath, "xdunzip");
		Process process = new Process
		{
			StartInfo = 
			{
				FileName = HaoZipCExePath,
				Arguments = $"x \"{text}\" -o\"{text2}\"",
				UseShellExecute = false,
				CreateNoWindow = true
			}
		};
		try
		{
			if (!Directory.Exists(TempPath))
			{
				Directory.CreateDirectory(TempPath);
			}
			if (!Directory.Exists(text2))
			{
				Directory.CreateDirectory(text2);
			}
			File.Copy(filepath, text, overwrite: true);
			process.Start();
			process.WaitForExit(5000);
		}
		finally
		{
			Process[] processesByName = Process.GetProcessesByName("HaoZipC.exe");
			foreach (Process process2 in processesByName)
			{
				process2.Kill();
			}
		}
		string fpprj = $"{text2}\\prjinfo.xmd";
		LoadPrjInfo(fpprj);
		string fpplc = $"{text2}\\{plcname}\\plcinfo.xmd";
		string fplad = $"{text2}\\{plcname}\\ladnodes_instlist.xmd";
		string fpfblist = $"{text2}\\funcblock\\xcp.fcblst";
		string fpcmt = $"{text2}\\{plcname}\\regcomment.xmd";
		LoadPlcInfo(fpplc);
		LoadLadder(fplad);
		LoadFuncBlock(fpfblist);
		LoadComment(fpcmt);
		Directory.Delete(text2, recursive: true);
		File.Delete(text);
	}

	public void Dispose()
	{
		fbdocs.Clear();
		laddoc = null;
		cmtdoc = null;
		fbdocs = null;
	}

	protected void LoadPrjInfo(string fpprj)
	{
		plcname = (((XDocument.Load(fpprj)?.Root)?.Element("PLCNames"))?.Element("string"))?.Value;
	}

	protected void LoadPlcInfo(string fpplc)
	{
		XElement xElement = ((XDocument.Load(fpplc)?.Root)?.Element("VerInfo"))?.Element("SelInfo");
		XElement xsername = xElement?.Element("SerialName");
		XElement xmodname = xElement?.Element("ModelName");
		device = Devs.FirstOrDefault((XDDevice dev) => dev.Name.Equals(xmodname?.Value)) ?? Devs.LastOrDefault((XDDevice dev) => dev.Parent.Name.Equals(xsername?.Value));
	}

	protected void LoadLadder(string fplad)
	{
		XElement xElement = (XDocument.Load(fplad)?.Root)?.Element("LadNodes");
		XElement xElement2 = xElement?.Element("CompressLines");
		XElement xElement3 = xElement?.Element("CompressLadNodes");
		laddoc = new XDLadderDocument();
		laddoc.LineText = xElement2?.Value;
		laddoc.UnitText = xElement3?.Value;
	}

	protected void LoadFuncBlock(string fpfblist)
	{
		string directoryName = Path.GetDirectoryName(fpfblist);
		fbdocs = new List<XDFuncBlockDocument>();
		XElement xElement = (XDocument.Load(fpfblist)?.Root)?.Element("FuncBlockNameArr");
		if (xElement == null)
		{
			return;
		}
		foreach (XElement item in xElement.Elements("string"))
		{
			string value = item.Value;
			string uri = $"{directoryName}\\{value}.fcb";
			string text = ((XDocument.Load(uri)?.Root)?.Element("SourceCode"))?.Value;
			XDFuncBlockDocument xDFuncBlockDocument = new XDFuncBlockDocument();
			xDFuncBlockDocument.Name = value;
			xDFuncBlockDocument.Text = text;
			fbdocs.Add(xDFuncBlockDocument);
		}
	}

	protected void LoadComment(string fpcmt)
	{
		XElement xElement = (XDocument.Load(fpcmt)?.Root)?.Element("CompressRegComments");
		cmtdoc = new XDCommentDocument();
		cmtdoc.Text = xElement?.Value;
	}

	public XDProject Translate()
	{
		XDProject xDProject = new XDProject();
		XDLadder xDLadder = new XDLadder(xDProject, "Main");
		XDLadder xDLadder2 = null;
		int num = -1;
		int num2 = 1;
		xDProject.Name = Path.GetFileNameWithoutExtension(filepath);
		xDProject.Device = device;
		xDProject.Ladders.Add(xDLadder);
		string[] array = laddoc.LineText.Split(new string[1] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
		foreach (string text in array)
		{
			int j = 0;
			int result = 0;
			int result2 = 0;
			int num3 = 0;
			for (; j < text.Length && text[j] != ':'; j++)
			{
				if (text[j] >= '0' && text[j] <= '9')
				{
					num3 = num3 * 10 + (text[j] - 48);
				}
			}
			if (j >= text.Length)
			{
				continue;
			}
			num2 = System.Math.Max(num2, num3 + 1);
			string[] array2 = text.Substring(j + 1).Split(new string[1] { "," }, StringSplitOptions.RemoveEmptyEntries);
			for (j = 0; j + 1 < array2.Length; j += 2)
			{
				if (int.TryParse(array2[j].Trim(), out result2) && int.TryParse(array2[j + 1].Trim(), out result))
				{
					if (result >= 10)
					{
						xDLadder.Children[result2, num3] = new XDUnit(xDLadder)
						{
							X = result2,
							Y = num3,
							InstName = "HLINE"
						};
					}
					if (((result % 10) & 2) != 0)
					{
						xDLadder.VLines[result2 - 1, num3] = new XDUnit(xDLadder)
						{
							X = result2 - 1,
							Y = num3,
							InstName = "VLINE"
						};
					}
				}
			}
		}
		string[] array3 = laddoc.UnitText.Split(new string[1] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
		foreach (string text2 in array3)
		{
			int l = 0;
			int m = 0;
			int result3 = 0;
			int num4 = 0;
			for (; l < text2.Length && text2[l] != ':'; l++)
			{
				if (text2[l] >= '0' && text2[l] <= '9')
				{
					num4 = num4 * 10 + (text2[l] - 48);
				}
			}
			if (l >= text2.Length)
			{
				continue;
			}
			num2 = System.Math.Max(num2, num4 + 1);
			string[] array4 = text2.Substring(l + 1).Split(new string[1] { "," }, StringSplitOptions.RemoveEmptyEntries);
			for (l = 0; l + 3 < array4.Length; l += 4)
			{
				int result4 = 0;
				if (!int.TryParse(array4[l].Trim(), out result3) || !int.TryParse(array4[l + 1].Trim(), out m) || !int.TryParse(array4[l + 2].Trim(), out result4))
				{
					continue;
				}
				if (((m % 10) & 2) != 0)
				{
					xDLadder.VLines[result3 - 1, num4] = new XDUnit(xDLadder)
					{
						X = result3 - 1,
						Y = num4,
						InstName = "VLINE"
					};
				}
				string text3 = array4[l + 3].Trim();
				if (text3.StartsWith(";"))
				{
					XDLineComment xDLineComment = new XDLineComment(xDLadder);
					xDLineComment.Y = num4;
					xDLineComment.Comment = text3.Substring(1);
					xDLadder.Lines.Add(xDLineComment);
					continue;
				}
				string[] array5 = text3.Split(new string[2] { " ", "\t" }, StringSplitOptions.RemoveEmptyEntries);
				if (array5.Length >= 1)
				{
					XDUnit xDUnit = new XDUnit(xDLadder)
					{
						X = result3,
						Y = num4,
						InstName = array5[0],
						InstCode = result4
					};
					for (m = 1; m < array5.Length; m++)
					{
						xDUnit.Args.Add(array5[m]);
					}
					xDLadder.Children[result3, num4] = xDUnit;
				}
			}
		}
		xDLadder.Height = num2;
		foreach (XDUnit child in xDLadder.Children)
		{
			if (child.X < 0)
			{
				continue;
			}
			char c = child.InstName.LastOrDefault();
			if (c == '=' || c == '>' || c == '<')
			{
				XDUnit xDUnit2 = xDLadder.Children[child.X - 1, child.Y];
				if (xDUnit2 != null && xDUnit2.InstName.Equals(child.InstName) && xDUnit2.InstCode == child.InstCode - 1)
				{
					child.InstName = "HLINE";
				}
			}
		}
		for (int n = 0; n < num2; n++)
		{
			XDUnit xDUnit3 = xDLadder.Children[0, n];
			int sbrid = -1;
			int intid = -1;
			switch (xDUnit3?.InstName)
			{
			case "FEND":
				xDLadder.Height = n;
				continue;
			case "SRET":
			case "IRET":
				if (xDLadder2 != null)
				{
					xDLadder2.Load(xDLadder, num, n - num);
					xDLadder2 = null;
					num = -1;
				}
				continue;
			}
			if (xDUnit3 != null && xDUnit3.IsSBRHeader(out sbrid))
			{
				xDLadder2?.Load(xDLadder, num, n - num);
				num = n + 1;
				xDLadder2 = new XDLadder(xDProject, $"SBR_{sbrid}");
				xDLadder2.SBRID = sbrid;
				xDProject.Ladders.Add(xDLadder2);
			}
			else if (xDUnit3 != null && xDUnit3.IsINTHeader(out intid))
			{
				xDLadder2?.Load(xDLadder, num, n - num);
				num = n + 1;
				xDLadder2 = new XDLadder(xDProject, $"INT_{intid}");
				xDLadder2.INTID = intid;
				xDProject.Ladders.Add(xDLadder2);
			}
		}
		xDLadder2?.Load(xDLadder, num, num2 - num);
		foreach (XDFuncBlockDocument fbdoc in fbdocs)
		{
			XDFuncBlock xDFuncBlock = new XDFuncBlock(xDProject);
			xDFuncBlock.Name = fbdoc.Name;
			xDFuncBlock.Code = fbdoc.Text;
			xDProject.FuncBlocks.Add(xDFuncBlock);
		}
		string[] array6 = cmtdoc.Text.Split('\n');
		foreach (string text4 in array6)
		{
			int num6 = text4.IndexOf(':');
			if (num6 >= 0)
			{
				string text5 = text4.Substring(0, num6).Trim();
				string comment = text4.Substring(num6 + 1).Trim();
				XDValue value = null;
				if (!xDProject.Values.TryGetValue(text5, out value))
				{
					value = new XDValue(text5);
					xDProject.Values.Add(text5, value);
				}
				value.Comment = comment;
			}
		}
		foreach (XDLadder ladder in xDProject.Ladders)
		{
			foreach (XDUnit child2 in ladder.Children)
			{
				foreach (string arg in child2.Args)
				{
					XDValue value2 = null;
					if (!xDProject.Values.TryGetValue(arg, out value2))
					{
						value2 = new XDValue(arg);
						xDProject.Values.Add(arg, value2);
					}
				}
			}
		}
		xDProject.UpdateInteract();
		return xDProject;
	}
}
