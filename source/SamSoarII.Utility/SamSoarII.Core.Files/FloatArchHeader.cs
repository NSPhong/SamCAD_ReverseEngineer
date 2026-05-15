using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public class FloatArchHeader : ArchHeader
{
	public override FileHeaderTypes HeaderType => FileHeaderTypes.FloatArch;

	public override IFileHeader Create()
	{
		return new FloatArchHeader();
	}

	public FloatArchHeader()
	{
		bType = 4;
	}
}
