using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class TECAMFreeParamsHeader : TECAMParamsHeader
{
	public int dwKeyPointCount;

	public int lpKeyPoints;

	public float fXPower;

	public float fYPower;

	public int dwUnit;

	public override FileHeaderTypes HeaderType => FileHeaderTypes.TECAMParams_Free;

	public override IFileHeader Create()
	{
		return new TECAMFreeParamsHeader();
	}
}
