using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class MonitorHeader : BaseFileHeader
{
	public int dwHeaderSize;

	public int dwTableCount;

	public int lpTable;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.Monitor;

	public override IFileHeader Create()
	{
		return new MonitorHeader();
	}
}
