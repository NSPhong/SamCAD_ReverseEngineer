using System.ComponentModel;
using SamSoarII.Core.Files;

namespace SamSoarII.Polyline.Arguments;

public class HMIPLINEImageArgument : ImageArgument, IHMIPLINEImageArgument, IImageArgument, INotifyPropertyChanged
{
	private int planeid;

	private StartupMode startupmode;

	public int PlaneID
	{
		get
		{
			return planeid;
		}
		set
		{
			planeid = value;
			InvokePropertyChanged("PlaneID");
		}
	}

	public StartupMode StartupMode
	{
		get
		{
			return startupmode;
		}
		set
		{
			startupmode = value;
			InvokePropertyChanged("StartupMode");
			InvokePropertyChanged("StartupModeIndex");
		}
	}

	public int StartupModeIndex
	{
		get
		{
			return (int)startupmode;
		}
		set
		{
			startupmode = (StartupMode)value;
			InvokePropertyChanged("StartupMode");
			InvokePropertyChanged("StartupModeIndex");
		}
	}

	public HMIPLINEImageArgument()
	{
		planeid = 1;
		startupmode = StartupMode.Trajectory;
	}

	public override LocatedFileHeader AllocHeader()
	{
		return FileFormat.AllocHeader(FileHeaderTypes.HMIPLINEImageArgument);
	}

	public override void Save(IFileHeader header)
	{
		base.Save(header);
		if (header is HMIPLINEImageArgumentHeader)
		{
			HMIPLINEImageArgumentHeader hMIPLINEImageArgumentHeader = (HMIPLINEImageArgumentHeader)header;
			hMIPLINEImageArgumentHeader.bStartupMode = (byte)startupmode;
			hMIPLINEImageArgumentHeader.wPlaneID = (short)planeid;
		}
	}

	public override void Load(IFileHeader header)
	{
		base.Load(header);
		if (header is HMIPLINEImageArgumentHeader)
		{
			HMIPLINEImageArgumentHeader hMIPLINEImageArgumentHeader = (HMIPLINEImageArgumentHeader)header;
			startupmode = (StartupMode)hMIPLINEImageArgumentHeader.bStartupMode;
			planeid = hMIPLINEImageArgumentHeader.wPlaneID;
		}
	}
}
