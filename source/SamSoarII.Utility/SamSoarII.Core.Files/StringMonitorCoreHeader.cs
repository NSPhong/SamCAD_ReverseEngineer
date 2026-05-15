using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class StringMonitorCoreHeader : BaseFileHeader
{
	public int dwHeaderSize;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.StringMonitorCore;

	public override IFileHeader Create()
	{
		return new StringMonitorCoreHeader();
	}
}
