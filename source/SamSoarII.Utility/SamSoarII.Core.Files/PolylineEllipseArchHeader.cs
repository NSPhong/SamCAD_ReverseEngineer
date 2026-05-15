using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class PolylineEllipseArchHeader : PolylineEllipseHeader
{
	public float fTargetX;

	public float fTargetY;

	public override FileHeaderTypes HeaderType => FileHeaderTypes.PolylineEllipseArch;

	public override IFileHeader Create()
	{
		return new PolylineEllipseArchHeader();
	}
}
