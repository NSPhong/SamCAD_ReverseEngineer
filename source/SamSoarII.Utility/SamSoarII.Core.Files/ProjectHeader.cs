using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class ProjectHeader : BaseFileHeader
{
	public int dwHeaderSize;

	public int spName;

	public int dwDeviceType;

	public int dwPasswordFlag;

	public int spPassword;

	public int dwLadderXCapacity;

	public int dwLadderYCapacity;

	public int dwNetworkCapacity;

	public int dwDiagramCount;

	public int lpDiagram;

	public int dwNoLibrary;

	public int dwFuncBlockCount;

	public int lpFuncBlock;

	public int dwModbusCount;

	public int lpModbus;

	public int dwPolylineSystemCount;

	public int lpPolylineSystem;

	public int dwPLSBlockCount;

	public int lpPLSBlock;

	public int lpParams;

	public int lpValueManager;

	public int lpMonitor;

	public int lpValueBrpo;

	public int lpPulseChart;

	public override int HeaderSize
	{
		get
		{
			return dwHeaderSize;
		}
		set
		{
			dwHeaderSize = value;
		}
	}

	public override FileHeaderTypes HeaderType => FileHeaderTypes.Project;

	public override IFileHeader Create()
	{
		return new ProjectHeader();
	}

	public override void SetStrPtr(int id, int sp)
	{
		switch (id)
		{
		case 0:
			spName = sp;
			break;
		case 1:
			spPassword = sp;
			break;
		}
	}

	public override void SetFreePtr(int id, int lp)
	{
		switch (id)
		{
		case 0:
			lpParams = lp;
			break;
		case 1:
			lpPulseChart = lp;
			break;
		}
	}
}
