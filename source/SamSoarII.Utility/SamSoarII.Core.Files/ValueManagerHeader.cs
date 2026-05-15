using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class ValueManagerHeader : BaseFileHeader
{
	public int dwHeaderSize;

	public int dwValueCount;

	public int lpValue;

	public int dwLocalCount;

	public int lpLocal;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.ValueManager;

	public override IFileHeader Create()
	{
		return new ValueManagerHeader();
	}
}
