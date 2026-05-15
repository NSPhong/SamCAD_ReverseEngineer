using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class LINEFHeader : POLYLINEHeader
{
	public int lpLine;

	public override FileHeaderTypes HeaderType => FileHeaderTypes.LINEF;

	public override IFileHeader Create()
	{
		return new LINEFHeader();
	}
}
