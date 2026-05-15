using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class PLSBlockHeader : BaseFileHeader
{
	public int dwHeaderSize;

	public int dwSystemID;

	public int spFilename;

	public int spName;

	public int spVelocity;

	public int spAcTime;

	public int spDcTime;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.PLSBlock;

	public override IFileHeader Create()
	{
		return new PLSBlockHeader();
	}

	public override void SetStrPtr(int id, int sp)
	{
		switch (id)
		{
		case 0:
			spFilename = sp;
			break;
		case 1:
			spName = sp;
			break;
		case 2:
			spVelocity = sp;
			break;
		case 3:
			spAcTime = sp;
			break;
		case 4:
			spDcTime = sp;
			break;
		}
	}
}
