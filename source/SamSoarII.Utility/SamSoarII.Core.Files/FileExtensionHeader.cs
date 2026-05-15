using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class FileExtensionHeader : BaseFileHeader
{
	public int dwHeaderSize;

	public int dwVersionTemporary;

	public override FileHeaderTypes HeaderType => FileHeaderTypes.FileExtension;

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

	public override IFileHeader Create()
	{
		return new FileExtensionHeader();
	}
}
