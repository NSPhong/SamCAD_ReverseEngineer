using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class TECAMFlyParamsHeader : TECAMParamsHeader
{
	public float fXTotalLen;

	public float fSyncDegree;

	public int dwKnifeNumber;

	public int dwMagic;

	public int dwCurveShape;

	public float fSyncStartAngle;

	public float fSyncX;

	public float fStartVelocity;

	public override FileHeaderTypes HeaderType => FileHeaderTypes.TECAMParams_Fly;

	public override IFileHeader Create()
	{
		return new TECAMFlyParamsHeader();
	}
}
