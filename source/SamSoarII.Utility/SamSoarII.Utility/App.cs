using System.Threading;
using SamSoarII.Device;
using SamSoarII.Properties;

namespace SamSoarII.Utility;

public abstract class App
{
	private static IInteractionFacade ifParent;

	private static IPLCDevice plcdevice;

	private static Version version_App = new Version
	{
		Ver_Main = 2,
		Ver_Sub = 3,
		Ver_Modify = 24
	};

	private static Version version_Iap = new Version
	{
		Ver_Main = 2,
		Ver_Sub = 3,
		Ver_Modify = 24
	};

	private static int version_dev = 205;

	private static string version_dev_text = "205";

	public static readonly string VersionNote = "Updated 2021/12/23";

	public static IInteractionFacade IFParent
	{
		get
		{
			return ifParent;
		}
		set
		{
			ifParent = value;
		}
	}

	public static IPLCDevice PLCDevice
	{
		get
		{
			return plcdevice;
		}
		set
		{
			plcdevice = value;
		}
	}

	public static Version Version_App => version_App;

	public static Version Version_Iap => version_Iap;

	public static int Version_Dev => version_dev;

	public static string Version_Dev_Text => version_dev_text;

	public static string GetDeviceVersionText()
	{
		if (!string.IsNullOrEmpty(version_dev_text))
		{
			return version_dev_text;
		}
		return version_dev.ToString();
	}

	public static bool CultureIsZH_CN()
	{
		if (Resources.Culture == null)
		{
			return Thread.CurrentThread.CurrentUICulture.Name.Contains("zh");
		}
		return Resources.Culture.Name.Contains("zh");
	}
}
