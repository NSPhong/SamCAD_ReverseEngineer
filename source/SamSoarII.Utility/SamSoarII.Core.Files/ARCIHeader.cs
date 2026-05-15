using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class ARCIHeader : POLYLINEHeader
{
	public int lpArch;

	public override FileHeaderTypes HeaderType => FileHeaderTypes.ARCI;

	public override IFileHeader Create()
	{
		return new ARCIHeader();
	}
}
