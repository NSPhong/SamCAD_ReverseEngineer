using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;

namespace SamSoarII.Utility.Win32;

public class PCDeviceViewer : IDisposable
{
	[StructLayout(LayoutKind.Sequential)]
	public class SP_DEVINFO_DATA
	{
		public int cbsize;

		public Guid gclass;

		public int devinst;

		public ulong reserved;
	}

	public const int DIGCF_ALLCLASSES = 4;

	public const int DIGCF_PRESENT = 2;

	public const int SPDRP_DEVICEDESC = 0;

	public const int SPDRP_HARDWAREID = 1;

	public const int SPDRP_COMPATIBLEIDS = 2;

	public const int SPDRP_UNUSED0 = 3;

	public const int SPDRP_SERVICE = 4;

	public const int SPDRP_UNUSED1 = 5;

	public const int SPDRP_UNUSED2 = 6;

	public const int SPDRP_CLASS = 7;

	public const int SPDRP_CLASSGUID = 8;

	public const int SPDRP_DRIVER = 9;

	public const int SPDRP_CONFIGFLAGS = 10;

	public const int SPDRP_MFG = 11;

	public const int SPDRP_FRIENDLYNAME = 12;

	public const int SPDRP_LOCATION_INFORMATION = 13;

	public const int SPDRP_PHYSICAL_DEVICE_OBJECT_NAME = 14;

	public const int SPDRP_CAPABILITIES = 15;

	public const int SPDRP_UI_NUMBER = 16;

	public const int SPDRP_UPPERFILTERS = 17;

	public const int SPDRP_LOWERFILTERS = 18;

	public const int SPDRP_BUSTYPEGUID = 19;

	public const int SPDRP_LEGACYBUSTYPE = 20;

	public const int SPDRP_BUSNUMBER = 21;

	public const int SPDRP_ENUMERATOR_NAME = 22;

	public const int SPDRP_SECURITY = 23;

	public const int SPDRP_SECURITY_SDS = 24;

	public const int SPDRP_DEVTYPE = 25;

	public const int SPDRP_EXCLUSIVE = 26;

	public const int SPDRP_CHARACTERISTICS = 27;

	public const int SPDRP_ADDRESS = 28;

	public const int SPDRP_UI_NUMBER_DESC_FORMAT = 30;

	public const int SPDRP_MAXIMUM_PROPERTY = 31;

	public const int INVALID_HANDLE_VALUE = -1;

	public const int MAX_DEV_LEN = 1000;

	private List<PCDevicePropertyWork> propworks;

	private List<PCDeviceInfo> devinfos;

	public IList<PCDevicePropertyWork> PropWorks => propworks;

	public IList<PCDeviceInfo> DevInfos => devinfos;

	[DllImport("setupapi.dll", SetLastError = true)]
	public static extern IntPtr SetupDiGetClassDevs(ref Guid gcls, uint ienum, IntPtr hparent, uint nflags);

	[DllImport("setupapi.dll", SetLastError = true)]
	public static extern int SetupDiDestroyDeviceInfoList(IntPtr lp);

	[DllImport("setupapi.dll", SetLastError = true)]
	public static extern bool SetupDiEnumDeviceInfo(IntPtr lp, uint id, SP_DEVINFO_DATA data);

	[DllImport("setupapi.dll", CharSet = CharSet.Unicode, EntryPoint = "SetupDiGetDeviceRegistryPropertyW", SetLastError = true)]
	public static extern bool SetupDiGetDeviceRegistryProperty(IntPtr lp, SP_DEVINFO_DATA data, uint property, out uint proptype, StringBuilder propbuff, uint szbuff, out uint szrequired);

	[DllImport("kernel32.dll")]
	public static extern int GetLastError();

	public PCDeviceViewer()
	{
		propworks = new List<PCDevicePropertyWork>();
		devinfos = new List<PCDeviceInfo>();
		propworks.Add(new PCDevicePropertyWork("Equipment description", 0));
		propworks.Add(new PCDevicePropertyWork("Common name", 12));
		propworks.Add(new PCDevicePropertyWork("Type", 7));
		propworks.Add(new PCDevicePropertyWork("Hardware ID", 1));
	}

	public void Dispose()
	{
		propworks?.Clear();
		devinfos?.Clear();
		propworks = null;
		devinfos = null;
	}

	public void FindAll()
	{
		try
		{
			Guid gcls = Guid.Empty;
			IntPtr lp = SetupDiGetClassDevs(ref gcls, 0u, IntPtr.Zero, 6u);
			if (lp.ToInt32() == -1)
			{
				throw new Exception("SetupDiGetClassDevs : Invalid handle");
			}
			SP_DEVINFO_DATA data = new SP_DEVINFO_DATA
			{
				cbsize = 28,
				devinst = 0,
				gclass = Guid.Empty,
				reserved = 0uL
			};
			devinfos.Clear();
			for (uint num = 0u; SetupDiEnumDeviceInfo(lp, num, data); num++)
			{
				PCDeviceInfo pCDeviceInfo = new PCDeviceInfo("#Unknown Device");
				devinfos.Add(pCDeviceInfo);
				foreach (PCDevicePropertyWork propwork in propworks)
				{
					StringBuilder stringBuilder = new StringBuilder
					{
						Capacity = 1000
					};
					if (!SetupDiGetDeviceRegistryProperty(lp, data, (uint)propwork.APIID, out var _, stringBuilder, 1000u, out var _))
					{
						int lastError = GetLastError();
					}
					PCDeviceProperty pCDeviceProperty = new PCDeviceProperty(propwork.Name, propwork.APIID, stringBuilder.ToString());
					pCDeviceInfo.Props.Add(pCDeviceProperty);
					if (pCDeviceProperty.APIID == 0)
					{
						pCDeviceInfo.Name = pCDeviceProperty.Value;
					}
				}
			}
			SetupDiDestroyDeviceInfoList(lp);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}
}
