using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class FuncBlockHeader : BaseFileHeader
{
	public int dwHeaderSize;

	public int dwFuncBlockFlag;

	public int spName;

	public int spPath;

	public int spCode;

	public int dwPasswordFlag;

	public int spPassword;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.FuncBlock;

	public override IFileHeader Create()
	{
		return new FuncBlockHeader();
	}

	public override void SetStrPtr(int id, int sp)
	{
		switch (id)
		{
		case 0:
			spName = sp;
			break;
		case 1:
			spPath = sp;
			break;
		case 2:
			spCode = sp;
			break;
		case 3:
			spPassword = sp;
			break;
		}
	}
}
