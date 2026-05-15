using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class PolylineProjectHeader : BaseFileHeader
{
	public int dwHeaderSize;

	public int spName;

	public int spFilename;

	public int dwArgumentType;

	public int dwItemsCount;

	public int lpItems;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.PolylineProject;

	public override IFileHeader Create()
	{
		return new PolylineProjectHeader();
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
