using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class ValueLineHeader : BaseFileHeader
{
	public int dwHeaderSize;

	public int dwCount;

	public int lpChildren;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.ValueLine;

	public override IFileHeader Create()
	{
		return new ValueLineHeader();
	}
}
