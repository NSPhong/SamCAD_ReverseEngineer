using System;
using System.Windows.Media;
using System.Xml.Linq;
using SamSoarII.Shell.Managers;
using SamSoarII.Utility;

namespace SamSoarII.Global;

public class GlobalSetting
{
	public static string MainTitle;

	private const double ScaleMin = 0.6;

	private const double ScaleMax = 4.0;

	public const int LadderWidthUnit = 300;

	public const int LadderHeightUnit = 300;

	public const int LadderCommentModeHeightUnit = 500;

	private static int ladderxcapacity;

	private static int ladderycapacity;

	private static int networkcapacity;

	private static int sfcmaxcolumn;

	private static double _ladderOriginScaleX;

	private static double _ladderOriginScaleY;

	private static double _ladderScaleX;

	private static double _ladderScaleY;

	private const double SFCMinScale = 0.25;

	private const double SFCMaxScale = 1.0;

	private static bool _loadScaleSuccessFlag;

	private static byte _A;

	private static byte _R;

	private static byte _G;

	private static byte _B;

	public static int WholeWidth => 300 * LadderXCapacity;

	public static int LadderXCapacity
	{
		get
		{
			return Math.Max(8, ladderxcapacity);
		}
		set
		{
			if (value < 8 || value > 18)
			{
				value = 12;
			}
			if (ladderxcapacity != value && App.IFParent.ResizeXCapacity(value))
			{
				ladderxcapacity = value;
				App.IFParent.UpdateTextViewAll();
			}
		}
	}

	public static int LadderYCapacity => ladderycapacity;

	public static int NetworkCapacity => networkcapacity;

	public static int SFCMaxColumn => sfcmaxcolumn;

	public static string LanaguageName { get; set; }

	public static double LadderOriginScaleX
	{
		get
		{
			return _ladderOriginScaleX;
		}
		set
		{
			_ladderOriginScaleX = value;
			LadderScaleTransform.ScaleX = _ladderOriginScaleX * _ladderScaleX;
		}
	}

	public static double LadderOriginScaleY
	{
		get
		{
			return _ladderOriginScaleY;
		}
		set
		{
			_ladderOriginScaleY = value;
			LadderScaleTransform.ScaleY = _ladderScaleY * _ladderOriginScaleY;
		}
	}

	public static double LadderScaleX
	{
		get
		{
			return _ladderScaleX;
		}
		set
		{
			if (value > 0.6 && value < 4.0)
			{
				_ladderScaleX = value;
				LadderScaleTransform.ScaleX = _ladderScaleX * _ladderOriginScaleX;
			}
		}
	}

	public static double LadderScaleY
	{
		get
		{
			return _ladderScaleY;
		}
		set
		{
			if (value > 0.6 && value < 4.0)
			{
				_ladderScaleY = value;
				LadderScaleTransform.ScaleY = _ladderOriginScaleY * _ladderScaleY;
			}
		}
	}

	public static double SFCScaleX
	{
		get
		{
			return SFCScaleTransform.ScaleX;
		}
		set
		{
			value = Math.Max(value, 0.25);
			value = Math.Min(value, 1.0);
			SFCScaleTransform.ScaleX = value;
		}
	}

	public static double SFCScaleY
	{
		get
		{
			return SFCScaleTransform.ScaleY;
		}
		set
		{
			value = Math.Max(value, 0.25);
			value = Math.Min(value, 1.0);
			SFCScaleTransform.ScaleY = value;
		}
	}

	public static ScaleTransform LadderScaleTransform { get; private set; }

	public static ScaleTransform SFCScaleTransform { get; private set; }

	public static int FuncBlockFontSize { get; set; }

	public static int SelectedIndexOfFontSizeComboBox { get; set; }

	public static int SelectedIndexOfFontFamilyComboBox { get; set; }

	public static Color SelectColor { get; set; }

	public static bool IsSavedByTime { get; set; }

	public static int SaveTimeSpan { get; set; }

	public static bool IsInstByTime { get; set; }

	public static int InstTimeSpan { get; set; }

	public static bool IsCheckCoil { get; set; }

	public static bool IsCheckTimer { get; set; }

	public static bool IsCheckCounter { get; set; }

	public static bool ShowDoubleWord { get; set; }

	public static bool ShowAlias { get; set; }

	public static bool AutoGenerateLine { get; set; }

	public static bool AutoGenerateNetwork { get; set; }

	public static bool OptimizeCommentSpace { get; set; }

	public static string USBFilePath { get; set; }

	public static string USBDownName { get; set; }

	public static string USBDataName { get; set; }

	public static string USBDataNameNew { get; set; }

	public static string USBDataNameNewV3 { get; set; }

	public static SolidColorBrush FoldingBrush { get; private set; }

	public static SolidColorBrush SimulateBrush { get; private set; }

	public static SolidColorBrush MonitorBrush { get; private set; }

	public static SolidColorBrush LoadingBrush { get; private set; }

	public static event ColumsChangedEventHandler ColumnChanged;

	static GlobalSetting()
	{
		MainTitle = "SamSoarII";
		GlobalSetting.ColumnChanged = delegate
		{
		};
		ladderxcapacity = 12;
		ladderycapacity = 65536;
		networkcapacity = 1024;
		sfcmaxcolumn = 1024;
		_ladderScaleX = 1.0;
		_ladderScaleY = 1.0;
		USBDataNameNew = "data1.bin";
		USBDataNameNewV3 = "data2.bin";
		LadderScaleTransform = new ScaleTransform();
		SFCScaleTransform = new ScaleTransform();
		SFCScaleTransform.ScaleX = 0.55;
		SFCScaleTransform.ScaleY = 0.55;
		FoldingBrush = new SolidColorBrush(new Color
		{
			A = byte.MaxValue,
			R = 60,
			G = 58,
			B = 58
		});
		MonitorBrush = new SolidColorBrush(new Color
		{
			A = byte.MaxValue,
			R = 76,
			G = 178,
			B = 250
		});
		SimulateBrush = new SolidColorBrush(new Color
		{
			A = byte.MaxValue,
			R = 244,
			G = 102,
			B = 34
		});
		LoadingBrush = new SolidColorBrush(new Color
		{
			A = byte.MaxValue,
			R = 169,
			G = 42,
			B = 215
		});
	}

	public static void SetLadderXCapacity(int _ladderxcapacity)
	{
		ladderxcapacity = _ladderxcapacity;
	}

	public static XElement CreateXELementBySetting()
	{
		SaveColor();
		XElement xElement = new XElement("SystemSetting");
		xElement.Add(new XElement("LadderOriginScaleX", LadderOriginScaleX));
		xElement.Add(new XElement("LadderOriginScaleY", LadderOriginScaleY));
		xElement.Add(new XElement("LadderScaleX", LadderScaleX));
		xElement.Add(new XElement("LadderScaleY", LadderScaleY));
		xElement.Add(new XElement("FuncBlockFontSize", FuncBlockFontSize));
		xElement.Add(new XElement("_A", _A));
		xElement.Add(new XElement("_R", _R));
		xElement.Add(new XElement("_G", _G));
		xElement.Add(new XElement("_B", _B));
		xElement.Add(new XElement("IsSavedByTime", IsSavedByTime));
		xElement.Add(new XElement("SaveTimeSpan", SaveTimeSpan));
		xElement.Add(new XElement("IsInstByTime", IsInstByTime));
		xElement.Add(new XElement("InstTimeSpan", InstTimeSpan));
		xElement.Add(new XElement("LanaguageName", LanaguageName));
		xElement.Add(new XElement("IsCheckCoil", IsCheckCoil));
		xElement.Add(new XElement("IsCheckTimer", IsCheckTimer));
		xElement.Add(new XElement("IsCheckCounter", IsCheckCounter));
		xElement.Add(new XElement("ShowDoubleWord", ShowDoubleWord));
		xElement.Add(new XElement("ShowAlias", ShowAlias));
		xElement.Add(new XElement("OptimizeCommentSpace", OptimizeCommentSpace));
		xElement.Add(new XElement("AutoGenerateLine", AutoGenerateLine));
		xElement.Add(new XElement("AutoGenerateNetwork", AutoGenerateNetwork));
		xElement.Add(new XElement("LadderXCapacity", LadderXCapacity));
		xElement.Add(new XElement("USBFilePath", USBFilePath));
		xElement.Add(new XElement("USBDownName", USBDownName));
		xElement.Add(new XElement("USBDataName", USBDataName));
		xElement.Add(new XElement("Title", MainTitle));
		XElement xElement2 = new XElement("Font");
		xElement.Add(xElement2);
		XElement xElement3 = null;
		xElement3 = new XElement("Comment");
		FontManager.SaveFontDataToXElement(FontManager.GetComment(), xElement3);
		xElement2.Add(xElement3);
		xElement3 = new XElement("Func");
		FontManager.SaveFontDataToXElement(FontManager.GetFunc(), xElement3);
		xElement2.Add(xElement3);
		xElement3 = new XElement("Ladder");
		FontManager.SaveFontDataToXElement(FontManager.GetLadder(), xElement3);
		xElement2.Add(xElement3);
		xElement3 = new XElement("Title");
		FontManager.SaveFontDataToXElement(FontManager.GetTitle(), xElement3);
		xElement2.Add(xElement3);
		xElement3 = new XElement("TextComment");
		FontManager.SaveFontDataToXElement(FontManager.GetTextComment(), xElement3);
		xElement2.Add(xElement3);
		xElement3 = new XElement("TextLadder");
		FontManager.SaveFontDataToXElement(FontManager.GetTextLadder(), xElement3);
		xElement2.Add(xElement3);
		xElement3 = new XElement("TextTitle");
		FontManager.SaveFontDataToXElement(FontManager.GetTextTitle(), xElement3);
		xElement2.Add(xElement3);
		xElement3 = new XElement("FBDNetwork");
		FontManager.SaveFontDataToXElement(FontManager.GetFBDNetwork(), xElement3);
		xElement2.Add(xElement3);
		xElement3 = new XElement("FBDValue");
		FontManager.SaveFontDataToXElement(FontManager.GetFBDValue(), xElement3);
		xElement2.Add(xElement3);
		xElement3 = new XElement("FBDComment");
		FontManager.SaveFontDataToXElement(FontManager.GetFBDComment(), xElement3);
		xElement2.Add(xElement3);
		xElement3 = new XElement("FBDTable");
		FontManager.SaveFontDataToXElement(FontManager.GetFBDTable(), xElement3);
		xElement2.Add(xElement3);
		xElement3 = new XElement("FBDMonitor");
		FontManager.SaveFontDataToXElement(FontManager.GetFBDMonitor(), xElement3);
		xElement2.Add(xElement3);
		FontManager.SavePrintDataToXElement(xElement2);
		return xElement;
	}

	public static void LoadSystemSettingByXELement(XElement rootNode)
	{
		try
		{
			LadderOriginScaleX = double.Parse(rootNode.Element("LadderOriginScaleX").Value);
			LadderOriginScaleY = double.Parse(rootNode.Element("LadderOriginScaleY").Value);
			_loadScaleSuccessFlag = true;
		}
		catch (Exception)
		{
			_loadScaleSuccessFlag = false;
		}
		try
		{
			LadderScaleX = double.Parse(rootNode.Element("LadderScaleX").Value);
			LadderScaleY = double.Parse(rootNode.Element("LadderScaleY").Value);
		}
		catch (Exception)
		{
			LadderScaleX = 1.0;
			LadderScaleY = 1.0;
		}
		try
		{
			FuncBlockFontSize = int.Parse(rootNode.Element("FuncBlockFontSize").Value);
		}
		catch (Exception)
		{
			FuncBlockFontSize = 16;
		}
		try
		{
			XElement xElement = rootNode.Element("Font");
			FontManager.LoadFontDataByXElement(FontManager.GetTitle(), xElement.Element("Title"));
			FontManager.LoadFontDataByXElement(FontManager.GetLadder(), xElement.Element("Ladder"));
			FontManager.LoadFontDataByXElement(FontManager.GetComment(), xElement.Element("Comment"));
			FontManager.LoadFontDataByXElement(FontManager.GetFunc(), xElement.Element("Func"));
			FontManager.LoadFontDataByXElement(FontManager.GetTextTitle(), xElement.Element("TextTitle"));
			FontManager.LoadFontDataByXElement(FontManager.GetTextLadder(), xElement.Element("TextLadder"));
			FontManager.LoadFontDataByXElement(FontManager.GetTextComment(), xElement.Element("TextComment"));
			FontManager.LoadFontDataByXElement(FontManager.GetFBDComment(), xElement.Element("FBDComment"));
			FontManager.LoadFontDataByXElement(FontManager.GetFBDValue(), xElement.Element("FBDValue"));
			FontManager.LoadFontDataByXElement(FontManager.GetFBDNetwork(), xElement.Element("FBDNetwork"));
			FontManager.LoadFontDataByXElement(FontManager.GetFBDTable(), xElement.Element("FBDTable"));
			FontManager.LoadFontDataByXElement(FontManager.GetFBDMonitor(), xElement.Element("FBDMonitor"));
			FontManager.LoadPrintDataByXElement(xElement);
			XElement xElement2 = rootNode.Element("Color");
		}
		catch (Exception)
		{
			FontManager.GetTitle().FontSize = 45u;
			FontManager.GetTitle().FontFamily = new FontFamily("Consolas");
			FontManager.GetTitle().FontColor = ColorManager.Parse("255 0 0 0");
			FontManager.GetLadder().FontSize = 42u;
			FontManager.GetLadder().FontFamily = new FontFamily("Consolas");
			FontManager.GetLadder().FontColor = ColorManager.Parse("255 0 0 0");
			FontManager.GetComment().FontSize = 36u;
			FontManager.GetComment().FontFamily = new FontFamily("Consolas");
			FontManager.GetComment().FontColor = ColorManager.Parse("255 0 0 0");
			FontManager.GetFunc().FontSize = 16u;
			FontManager.GetFunc().FontFamily = new FontFamily("Courier New");
			FontManager.GetFunc().FontColor = ColorManager.Parse("255 0 0 0");
			FontManager.GetTextTitle().FontSize = 14u;
			FontManager.GetTextTitle().FontFamily = new FontFamily("Consolas");
			FontManager.GetTextTitle().FontColor = ColorManager.Parse("255 0 0 0");
			FontManager.GetTextLadder().FontSize = 12u;
			FontManager.GetTextLadder().FontFamily = new FontFamily("Consolas");
			FontManager.GetTextLadder().FontColor = ColorManager.Parse("255 0 0 0");
			FontManager.GetTextComment().FontSize = 12u;
			FontManager.GetTextComment().FontFamily = new FontFamily("Consolas");
			FontManager.GetTextComment().FontColor = ColorManager.Parse("255 0 0 0");
			FontManager.GetFBDValue().FontSize = 12u;
			FontManager.GetFBDValue().FontFamily = new FontFamily("Consolas");
			FontManager.GetFBDValue().FontColor = ColorManager.Parse("255 0 0 0");
			FontManager.GetFBDComment().FontSize = 12u;
			FontManager.GetFBDComment().FontFamily = new FontFamily("Consolas");
			FontManager.GetFBDComment().FontColor = ColorManager.Parse("255 0 128 0");
			FontManager.GetFBDNetwork().FontSize = 14u;
			FontManager.GetFBDNetwork().FontFamily = new FontFamily("Microsoft Yahei");
			FontManager.GetFBDNetwork().FontColor = ColorManager.Parse("255 0 0 0");
			FontManager.GetFBDTable().FontSize = 12u;
			FontManager.GetFBDTable().FontFamily = new FontFamily("Consolas");
			FontManager.GetFBDTable().FontColor = ColorManager.Parse("255 0 0 0");
			FontManager.GetFBDMonitor().FontSize = 12u;
			FontManager.GetFBDMonitor().FontFamily = new FontFamily("Consolas");
			FontManager.GetFBDMonitor().FontColor = ColorManager.Parse("255 24 65 142");
		}
		try
		{
			_A = byte.Parse(rootNode.Element("_A").Value);
			_R = byte.Parse(rootNode.Element("_R").Value);
			_G = byte.Parse(rootNode.Element("_G").Value);
			_B = byte.Parse(rootNode.Element("_B").Value);
			SelectColor = new Color
			{
				A = _A,
				R = _R,
				G = _G,
				B = _B
			};
		}
		catch (Exception)
		{
			SelectColor = Colors.Black;
		}
		try
		{
			IsSavedByTime = bool.Parse(rootNode.Element("IsSavedByTime").Value);
			SaveTimeSpan = int.Parse(rootNode.Element("SaveTimeSpan").Value);
		}
		catch (Exception)
		{
			IsSavedByTime = true;
			SaveTimeSpan = 1;
		}
		try
		{
			LanaguageName = rootNode.Element("LanaguageName").Value;
		}
		catch (Exception)
		{
			LanaguageName = string.Empty;
		}
		try
		{
			IsInstByTime = bool.Parse(rootNode.Element("IsInstByTime").Value);
			InstTimeSpan = int.Parse(rootNode.Element("InstTimeSpan").Value);
		}
		catch (Exception)
		{
			IsInstByTime = true;
			InstTimeSpan = 10;
		}
		try
		{
			IsCheckCoil = bool.Parse(rootNode.Element("IsCheckCoil").Value);
			IsCheckTimer = bool.Parse(rootNode.Element("IsCheckTimer").Value);
			IsCheckCounter = bool.Parse(rootNode.Element("IsCheckCounter").Value);
		}
		catch (Exception)
		{
			IsCheckCoil = false;
			IsCheckTimer = false;
			IsCheckCounter = false;
		}
		try
		{
			ShowDoubleWord = bool.Parse(rootNode.Element("ShowDoubleWord").Value);
			ShowAlias = bool.Parse(rootNode.Element("ShowAlias").Value);
		}
		catch (Exception)
		{
			ShowDoubleWord = false;
			ShowAlias = false;
		}
		try
		{
			ladderxcapacity = int.Parse(rootNode.Element("LadderXCapacity").Value);
		}
		catch (Exception)
		{
			ladderxcapacity = 12;
		}
		try
		{
			USBFilePath = rootNode.Element("USBFilePath").Value;
			USBDownName = rootNode.Element("USBDownName").Value;
			USBDataName = rootNode.Element("USBDataName").Value;
		}
		catch (Exception)
		{
			USBFilePath = FileHelper.AppRootPath;
			USBDownName = "downc.bin";
			USBDataName = "data.bin";
		}
		try
		{
			MainTitle = rootNode.Element("Title")?.Value ?? "SamSoarII";
		}
		catch (Exception)
		{
		}
		try
		{
			OptimizeCommentSpace = bool.Parse(rootNode.Element("OptimizeCommentSpace")?.Value ?? "false");
		}
		catch (Exception)
		{
			OptimizeCommentSpace = false;
		}
		try
		{
			AutoGenerateLine = bool.Parse(rootNode.Element("AutoGenerateLine")?.Value ?? "false");
			AutoGenerateNetwork = bool.Parse(rootNode.Element("AutoGenerateNetwork")?.Value ?? "false");
		}
		catch (Exception)
		{
			AutoGenerateLine = false;
			AutoGenerateNetwork = false;
		}
	}

	private static void SaveColor()
	{
		_A = SelectColor.A;
		_R = SelectColor.R;
		_G = SelectColor.G;
		_B = SelectColor.B;
	}

	public static bool LoadLadderScaleSuccess()
	{
		return _loadScaleSuccessFlag;
	}
}
