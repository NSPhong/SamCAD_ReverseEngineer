using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public class IntArchHeader : ArchHeader
{
	public override FileHeaderTypes HeaderType => FileHeaderTypes.IntArch;

	public override IFileHeader Create()
	{
		return new IntArchHeader();
	}

	public IntArchHeader()
	{
		bType = 3;
	}
}
