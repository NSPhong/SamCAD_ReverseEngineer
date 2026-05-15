using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class PolylineImageHeader : BaseFileHeader
{
	public int dwHeaderSize;

	public int spName;

	public int spFilename;

	public int dwItemsCount;

	public int lpItems;

	public int dwArgumentType;

	public int lpArgument;

	public int dwGroupCount;

	public int lpGroup;

	public int dwUserFmtsCount;

	public int lpUserFmts;

	public int lpUserData;

	public int lpImportCore;

	public int lpExportCore;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.PolylineImage;

	public override IFileHeader Create()
	{
		return new PolylineImageHeader();
	}

	public override void SetStrPtr(int id, int sp)
	{
		switch (id)
		{
		case 0:
			spName = sp;
			break;
		case 1:
			spFilename = sp;
			break;
		default:
			base.SetStrPtr(id, sp);
			break;
		}
	}
}
