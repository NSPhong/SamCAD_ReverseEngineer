using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class PolylineAxisHeader : BaseFileHeader
{
	public int dwHeaderSize;

	public int spPulse;

	public int spDirect;

	public int spWeight;

	public int spPosLim;

	public int spNegLim;

	public int spInterval;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.PolylineAxis;

	public override IFileHeader Create()
	{
		return new PolylineAxisHeader();
	}

	public override void SetStrPtr(int id, int sp)
	{
		switch (id)
		{
		case 0:
			spPulse = sp;
			break;
		case 1:
			spDirect = sp;
			break;
		case 2:
			spWeight = sp;
			break;
		case 3:
			spPosLim = sp;
			break;
		case 4:
			spNegLim = sp;
			break;
		case 5:
			spInterval = sp;
			break;
		}
	}
}
