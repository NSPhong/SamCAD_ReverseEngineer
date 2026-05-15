using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public class FloatLineHeader : LineHeader
{
	public override FileHeaderTypes HeaderType => FileHeaderTypes.FloatLine;

	public override IFileHeader Create()
	{
		return new FloatLineHeader();
	}

	public FloatLineHeader()
	{
		bType = 2;
	}
}
