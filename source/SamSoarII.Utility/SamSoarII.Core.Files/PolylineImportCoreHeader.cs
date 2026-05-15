using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class PolylineImportCoreHeader : BaseFileHeader
{
	public int dwHeaderSize;

	public int dwColumnCount;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.PolylineImportCore;

	public override IFileHeader Create()
	{
		return new PolylineImportCoreHeader();
	}
}
