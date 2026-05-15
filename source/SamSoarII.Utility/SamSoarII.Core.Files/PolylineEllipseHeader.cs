using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class PolylineEllipseHeader : PolylineEntityHeader
{
	public float fCenterX;

	public float fCenterY;

	public byte bIsClockwise;

	public float fDirectX;

	public float fDirectY;

	public float fLongRadius;

	public float fShortRadius;

	public override FileHeaderTypes HeaderType => FileHeaderTypes.PolylineEllipse;

	public override IFileHeader Create()
	{
		return new PolylineEllipseHeader();
	}
}
