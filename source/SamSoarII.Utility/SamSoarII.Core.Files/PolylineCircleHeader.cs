using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class PolylineCircleHeader : PolylineEntityHeader
{
	public float fCenterX;

	public float fCenterY;

	public byte bIsClockwise;

	public override FileHeaderTypes HeaderType => FileHeaderTypes.PolylineCircle;

	public override IFileHeader Create()
	{
		return new PolylineCircleHeader();
	}
}
