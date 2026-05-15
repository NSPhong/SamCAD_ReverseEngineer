using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class PolylineLineHeader : PolylineEntityHeader
{
	public float fTargetX;

	public float fTargetY;

	public override FileHeaderTypes HeaderType => FileHeaderTypes.PolylineLine;

	public override IFileHeader Create()
	{
		return new PolylineLineHeader();
	}
}
