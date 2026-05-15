using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class ARCFHeader : POLYLINEHeader
{
	public int lpArch;

	public override FileHeaderTypes HeaderType => FileHeaderTypes.ARCF;

	public override IFileHeader Create()
	{
		return new ARCFHeader();
	}
}
