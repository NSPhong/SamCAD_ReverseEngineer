using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public class IntLineHeader : LineHeader
{
	public override FileHeaderTypes HeaderType => FileHeaderTypes.IntLine;

	public override IFileHeader Create()
	{
		return new IntLineHeader();
	}

	public IntLineHeader()
	{
		bType = 1;
	}
}
