using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class PolylineExportCoreHeader : BaseFileHeader
{
	public int dwHeaderSize;

	public int dwColumnCount;

	public int lpColumns;

	public int dwStartRow;

	public int dwEndRow;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.PolylineExportCore;

	public override IFileHeader Create()
	{
		return new PolylineExportCoreHeader();
	}
}
