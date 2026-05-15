using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class EPIDItemHeader : BaseFileHeader
{
	public int dwHeaderSize;

	public int dwLoop;

	public int spName;

	public int dwTarget;

	public int dwMeasure;

	public int dwOutputType;

	public int dwOutputAddr;

	public int dwArgStart;

	public float fKB;

	public float fTI;

	public float fTD;

	public float fDZ;

	public int dwTS;

	public float fV0;

	public float fV1;

	public int dwDir;

	public int dwSelf;

	public int dwNSP;

	public int dwPOV;

	public int dwModifyMask;

	public int dwCalcVolume;

	public float fCalcRange;

	public float fSeOutAdj;

	public int dwCtrlMode;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.EPIDItem;

	public override IFileHeader Create()
	{
		return new EPIDItemHeader();
	}

	public override void SetStrPtr(int id, int sp)
	{
		if (id == 0)
		{
			spName = sp;
		}
		else
		{
			base.SetStrPtr(id, sp);
		}
	}
}
