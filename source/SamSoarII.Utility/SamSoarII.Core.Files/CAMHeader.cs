using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class CAMHeader : BaseFileHeader
{
	public int dwHeaderSize;

	public int spNumStore;

	public int spMaxTarget;

	public int spRefAddr;

	public int dwItemCount;

	public int lpItem;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.CAM;

	public override IFileHeader Create()
	{
		return new CAMHeader();
	}

	public override void SetStrPtr(int id, int sp)
	{
		switch (id)
		{
		case 0:
			spNumStore = sp;
			break;
		case 1:
			spMaxTarget = sp;
			break;
		case 2:
			spRefAddr = sp;
			break;
		}
	}
}
