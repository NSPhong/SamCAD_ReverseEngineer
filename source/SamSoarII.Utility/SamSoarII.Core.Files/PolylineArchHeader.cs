using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class PolylineArchHeader : PolylineCircleHeader
{
	public float fTargetX;

	public float fTargetY;

	public override FileHeaderTypes HeaderType => FileHeaderTypes.PolylineArch;

	public override IFileHeader Create()
	{
		return new PolylineArchHeader();
	}
}
