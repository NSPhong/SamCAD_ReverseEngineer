using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public sealed class TypeHeader : BaseFileHeader
{
	public FileHeaderTypes dwType;

	public int lpHeader;

	public override int HeaderSize
	{
		get
		{
			return 8;
		}
		set
		{
		}
	}

	public override FileHeaderTypes HeaderType => FileHeaderTypes.TypeLP;

	public override IFileHeader Create()
	{
		return new TypeHeader();
	}

	public IFileHeader ToHeader()
	{
		return FileFormat.GetHeader(lpHeader, dwType);
	}
}
