using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class LINEIHeader : POLYLINEHeader
{
	public int lpLine;

	public override FileHeaderTypes HeaderType => FileHeaderTypes.LINEI;

	public override IFileHeader Create()
	{
		return new LINEIHeader();
	}
}
